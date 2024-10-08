namespace Components

open Feliz
open Feliz.Bulma
open Browser.Dom
open Browser.Types
open Types
open Fable.SimpleJson
open Fable.Core.JS
open System
// open ARCtrl

module List =
  let rec removeAt index list =
      match index, list with
      | _, [] -> failwith "Index out of bounds"
      | 0, _ :: tail -> tail
      | _, head :: tail -> head :: removeAt (index - 1) tail

type Builder =
    [<ReactComponent>]
    static member Main (state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit, isLocalStorageClear: string -> unit -> bool) =

        // let annotationToResizeArray = state |> List.map (fun i -> i.Key ) |> List.toArray |> ResizeArray

        let initialInteraction (id: string) =
            if isLocalStorageClear id () = true then Unset
            else Json.parseAs<UploadedFile> (Browser.WebStorage.localStorage.getItem id)  

        let filehtml, setFilehtml = React.useState(initialInteraction "file")

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
                        column.isOneQuarter
                        prop.className "relative"
                        prop.children [
                            Html.div [
                                prop.className "fixed select-none"
                                prop.children [
                                    Bulma.block [
                                        prop.text "Navigation"
                                    ]
                                    Bulma.block [
                                        prop.text "Filename: ExamplePaper.docx" //exchange with uploaded file name
                                    ]
                                    Bulma.block [
                                        Components.UploadDisplay(filehtml,setFilehtml, setState, setLocal)
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
                                    Html.div [
                                        prop.className "field pt-1"
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
                            //exchange with uploaded string list, parsed from uploaded protocol
                        ]
                    ]
                    Bulma.column [
                        column.isOneQuarter
                        prop.className "relative"
                        prop.children [
                            Html.div [
                                prop.className "fixed select-none"
                                prop.children [
                                    Bulma.block [
                                        prop.text "Annotations"
                                        prop.className "pb-14"
                                        
                                    ]
                                    for a in 0 .. (state.Length - 1)  do
                                        Bulma.block [
                                            prop.className "border border-slate-400 bg-[#ffe699] p-3 text-black w-96"
                                            prop.children [
                                                Html.button [
                                                    prop.className "delete float-right m-0.5"
                                                    prop.onClick (fun _ -> 
                                                    let newAnno = List.removeAt a state 
                                                    newAnno
                                                    |> fun t ->
                                                    t |> setState 
                                                    t |> setLocal "Annotations"
                                                    )
                                                ]
                                                // Html.p (state.[a].Key |> Option.map (fun e -> e.NameText) |> Option.defaultValue "-")
                                                // Html.p (state.[a].Value |> Option.map (fun e -> e.ToString()) |> Option.defaultValue "-")
                                                Html.p ("Key: " + state.[a].Key)
                                                // Html.p (state.[a].Value)
                                                Bulma.block [
                                                    prop.className "space-x-4 pt-3"
                                                    prop.children [
                                                        Bulma.button.button [
                                                            Bulma.button.isSmall
                                                            prop.text "edit"
                                                            prop.className "is-info max-w-40"
                                                        ]
                                                        Bulma.button.button [
                                                            Bulma.button.isSmall
                                                            prop.text "ontologize"
                                                            prop.className "is-info max-w-40"
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
        
        

        
    
