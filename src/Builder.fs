namespace Components

open Feliz
open Feliz.Bulma
open Browser.Dom
open Browser.Types
open Types
open Fable.SimpleJson
open Fable.Core.JS
open System
open ARCtrl

module List =
  let rec removeAt index list =
      match index, list with
      | _, [] -> failwith "Index out of bounds"
      | 0, _ :: tail -> tail
      | _, head :: tail -> head :: removeAt (index - 1) tail

type Builder =
    [<ReactComponent>]
    static member Main (state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit, isLocalStorageClear: string -> unit -> bool) =

        let initialFile (id: string) =
            if isLocalStorageClear id () = true then Unset
            else Json.parseAs<UploadedFile> (Browser.WebStorage.localStorage.getItem id)  

        let filehtml, setFilehtml = React.useState(initialFile "file")

        let setLocalFileName (id: string)(nextNAme: string) =
            let JSONstring= 
                Json.stringify nextNAme 

            Browser.WebStorage.localStorage.setItem(id, JSONstring)

        let initialFileName (id: string) =
            if isLocalStorageClear id () = true then ""
            else Json.parseAs<string> (Browser.WebStorage.localStorage.getItem id)  

        let fileName, setFileName = React.useState(initialFileName "fileName")

        let (annoCheck: bool, setAnnoCheck) = React.useState(false)

        let initialModal = {
            isActive = false
            location = (0,0)
        }

        let modalContext = React.useContext (Contexts.ModalContext.createModalContext)
    

        let turnOffContext (event: Browser.Types.Event) = 
            modalContext.setter initialModal 
            
        React.useEffectOnce(fun () ->
            Browser.Dom.window.addEventListener ("resize", turnOffContext)
            {new IDisposable with member this.Dispose() = window.removeEventListener ("resize", turnOffContext) }    
        )
 
        Html.div [
            Bulma.columns [
                prop.className "z-0 py-5 px-5 text-white"
                prop.onClick (fun e -> modalContext.setter initialModal)
                prop.children [
                    Bulma.column [
                        column.isOneFifth
                        prop.className "relative"
                        prop.children [
                            Html.div [
                                prop.className "fixed select-none"
                                prop.children [
                                    Bulma.block [
                                        prop.text "Navigation"
                                    ]
                                    
                                    Bulma.block [
                                        Components.UploadDisplay(filehtml,setFilehtml, setState, setLocal, setFileName, setLocalFileName)
                                    ]
                                ]
                            ]
                        ]
                    ]
                    Bulma.column [
                        column.isHalf
                        prop.children [
                            Bulma.block [
                                prop.onContextMenu (fun e ->
                                    let term = window.getSelection().ToString().Trim() 
                                    if term.Length <> 0 then 
                                        modalContext.setter {
                                            isActive = true;
                                            location = int e.pageX, int e.pageY
                                        }
                                    else 
                                        ()
                                    e.stopPropagation()
                                    e.preventDefault()
                                )
                                prop.children [
                                    match filehtml with
                                    | Unset -> ()
                                    | Docx filehtml->
                                        Bulma.block [
                                            prop.text fileName
                                        ]
                                    Html.div [
                                        // prop.className "field"
                                        prop.children [
                                            match filehtml with
                                            | Unset -> Html.p [prop.text "Upload a file!"; prop.className "text-sky-400"]
                                            | Docx filehtml ->
                                                Components.DisplayHtml(filehtml)
                                            // | PDF pdfSource ->
                                            //   Components.DisplayPDF(pdfSource, modalContext)
                                        ]
                                    ]
                                //     // Highlighter.Highlighter.highlighter [
                                //     //     Highlighter.Highlighter.textToHighlight (HelperBui.testText.Replace("  ","" ))
                                //     //     Highlighter.Highlighter.searchWords (annotationToResizeArray) //replace with array of annotated words
                                //     //     // Highlighter.Highlighter.highlightClassName "highlight"
                                //     //     Highlighter.Highlighter.autoEscape true
                                //     // ]
                                ]
                            ]
                        ]
                    ]
                    Bulma.column [
                        prop.className "relative"
                        prop.children [
                            Html.div [
                                prop.className "fixed select-none "
                                if filehtml = Unset then prop.hidden (true)
                                prop.children [
                                    Bulma.block [
                                        prop.text "Annotations"
                                    ]
                                    Html.div [
                                        prop.className "overflow-x-hidden overflow-y-auto h-[49rem] pr-4"
                                        prop.children [
                                        for a in (state |> List.rev)  do
                                            if annoCheck = false then 
                                                Bulma.block [
                                                    Html.button [
                                                        Html.i [
                                                            prop.className "fa-solid fa-comment-dots"
                                                            prop.onClick (fun e -> setAnnoCheck true)
                                                        ]
                                                    ]    
                                                ]
                                            else //if true
                                                Bulma.block [
                                                    prop.className "bg-[#ffe699] p-3 text-black w-96"
                                                    prop.children [
                                                        Bulma.columns [
                                                            Bulma.column [
                                                                column.is1
                                                                prop.className "hover:bg-[#ffd966] cursor-pointer"
                                                                prop.onClick (fun e -> setAnnoCheck false)
                                                                prop.children [
                                                                    Html.span [
                                                                        Html.i [
                                                                            prop.className "fa-solid fa-chevron-left"
                                                                        ]
                                                                    ]
                                                                ]
                                                                
                                                            ]
                                                            Bulma.column [
                                                                prop.className "space-y-2"
                                                                prop.children [
                                                                    Html.span "Key: "
                                                                    Html.span [
                                                                        prop.className "delete float-right mt-0"
                                                                        prop.onClick (fun _ -> 
                                                                            let newAnno = state |> List.filter (fun x -> x = a |> not)  
                                                                            // List.removeAt (List.filter (fun x -> x = a) state) state
                                                                            newAnno
                                                                            |> fun t ->
                                                                            t |> setState 
                                                                            t |> setLocal "Annotations"
                                                                        )
                                                                    ]
                                                                    Bulma.input.text [
                                                                        input.isSmall
                                                                        prop.value (a.Key|> Option.map (fun e -> e.Name.Value) |> Option.defaultValue "")
                                                                        prop.className ""
                                                                        prop.onChange (fun (x: string)-> 
                                                                            let updatetedAnno = 
                                                                                {a with Key = OntologyAnnotation(name = x) |> Some}

                                                                            let newAnnoList =
                                                                                state
                                                                                |> List.map (fun elem -> if elem = a then updatetedAnno else elem)

                                                                            newAnnoList
                                                                            |> fun t ->
                                                                            t |> setState
                                                                            t |> setLocal "Annotations"
                                                                        )
                                                                    ]
                                                                    Html.p "Value: "
                                                                    Bulma.input.text [
                                                                        input.isSmall
                                                                        prop.value (a.Value|> Option.map (fun e -> e.ToString()) |> Option.defaultValue "" )
                                                                        prop.className ""
                                                                        prop.onChange (fun (x:string) -> 
                                                                            let updatetedAnno = 
                                                                                {a with Value = CompositeCell.createFreeText(x) |> Some}
                                                                                
                                                                            let newAnnoList =
                                                                                state
                                                                                |> List.map (fun elem -> if elem = a then updatetedAnno else elem)

                                                                            newAnnoList
                                                                            |> fun t ->
                                                                            t |> setState
                                                                            t |> setLocal "Annotations"
                                                                        )
                                                                    ]
                                                                ]
                                                            ]
                                                        ]
                                                    ]  
                                                ]
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
        
        

        
    
