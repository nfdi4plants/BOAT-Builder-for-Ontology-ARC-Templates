namespace App

open Feliz
open Types
open Components
open Fable.SimpleJson

type View =
    [<ReactComponent>]
    static member Main() =

        let isLocalStorageClear (key:string) () =
            match (Browser.WebStorage.localStorage.getItem key) with
            | null -> true // Local storage is clear if the item doesn't exist
            | _ -> false //if false then something exists and the else case gets started

        let initialInteraction (id: string) =
            if isLocalStorageClear id () = true then []
            else Json.parseAs<Annotation list> (Browser.WebStorage.localStorage.getItem id)  

        let (AnnotationState: Annotation list, setAnnotationState) = React.useState (initialInteraction "Annotations")

        let setLocalStorageAnnotation (id: string)(nextAnnos: Annotation list) =
            let JSONString = Json.stringify nextAnnos 
            Browser.WebStorage.localStorage.setItem(id, JSONString)

        let (modalState: ModalInfo, setModal) =
            React.useState(Contextmenu.initialModal)               
                       
        let myModalContext = { //makes setter and state in one record type
            modalState = modalState
            setter = setModal
            }

        let modalactivator = 
            match modalState.isActive with
                |true -> Contextmenu.onContextMenu (myModalContext, AnnotationState, setAnnotationState, setLocalStorageAnnotation)
                |false -> Html.none
        
        let currentpage,setpage = React.useState(Types.Page.Builder) 

        printfn "%A" currentpage
        React.contextProvider(Contexts.ModalContext.createModalContext, myModalContext, React.fragment [ //makes the context accesable for the whole project
            Html.div [
                prop.id "mainView"
                prop.className "flex h-full flex-col"
                prop.children [
                    Components.NavBar.Main(setpage,currentpage)
                    Html.div [
                        prop.id "contentView"
                        prop.className "grow"
                        prop.children [
                            match currentpage with
                            |Types.Page.Builder -> Components.Builder.Main( AnnotationState, setAnnotationState, setLocalStorageAnnotation, isLocalStorageClear)
                            |Types.Page.Contact -> Components.Contact.Main()
                            |Types.Page.Help -> Components.Help.Main()
                            modalactivator
                        ]
                    ]
                    Components.Footer.Main
                ]
            ]
        ])