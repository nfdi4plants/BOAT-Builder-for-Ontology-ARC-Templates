namespace Components

open Feliz
open Feliz.Bulma
open Browser.Dom
open Browser.Types
open Types
open Fable.SimpleJson
open Fable.Core.JS
open System

module List =
  let rec removeAt index list =
      match index, list with
      | _, [] -> failwith "Index out of bounds"
      | 0, _ :: tail -> tail
      | _, head :: tail -> head :: removeAt (index - 1) tail

type Builder =
    [<ReactComponent>]
    static member Main (state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit, isLocalStorageClear: string -> unit -> bool) =

        let annotationToResizeArray = state |> List.map (fun i -> i.Key ) |> List.toArray |> ResizeArray

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
                prop.className "py-5 px-5 text-white"
                prop.onClick (fun e -> modalContext.setter initialModal)
                prop.children [
                    Bulma.column [
                        column.isThreeFifths
                        prop.children [
                            Bulma.block [
                                prop.text "ProtocolExample.docx" //exchange with uploaded file name
                                prop.className "select-none"
                            ]
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
                                    Components.UploadDisplay( annotationToResizeArray, isLocalStorageClear)
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
                        prop.className "select-none"
                        prop.children [
                            Bulma.block [
                                prop.text "Annotations"
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
                                        Html.text (state.[a].Key)
                                        Bulma.columns [
                                            prop.className "pt-2 w-full"
                                            column.isHalf
                                            prop.children [
                                                Bulma.column [
                                                Bulma.button.button [
                                                    Bulma.button.isSmall
                                                    prop.text "edit"
                                                    prop.className "is-info w-full"
                                                ]
                                                ]
                                                Bulma.column [
                                                    Bulma.button.button [
                                                        Bulma.button.isSmall
                                                        prop.text "ontologize"
                                                        prop.className "is-info w-full"
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
        
        

        
    
