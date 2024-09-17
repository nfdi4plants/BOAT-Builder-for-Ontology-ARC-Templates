namespace App

open Feliz
open Feliz.Bulma
open Types
open Components

type View =
    [<ReactComponent>]
    static member Main() =
        let (modalState: ModalInfo, setModal) =
            React.useState(

                
            )
            
        let myModalContext = { //makes setter and state in one record type
            modalState = modalState
            setter = setModal
            }
            
        match modalState.isActive with
        |true -> yield Contextmenu.onContextMenu
        |false -> _


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
                            |Types.Page.Builder -> Components.Builder.Main()
                            |Types.Page.Contact -> Components.Contact.Main()
                            |Types.Page.Help -> Components.Help.Main()
                        ]
                    ]
                    Components.Footer.Main

                ]
            ]
        ])