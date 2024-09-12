namespace Components

open Feliz
open Feliz.Bulma
open Browser.Dom
open Types
open Fable.SimpleJson

module List =
  let rec removeAt index list =
      match index, list with
      | _, [] -> failwith "Index out of bounds"
      | 0, _ :: tail -> tail
      | _, head :: tail -> head :: removeAt (index - 1) tail

module Helper =

    let testText = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.   

Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.   

Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.   

Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.   

Duis autem vel eum iriure"

type Builder =
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

        let annotationToResizeArray = AnnotationState |> List.map (fun i -> i.Key ) |> List.toArray |> ResizeArray

        let addAnotation =
            let term = window.getSelection().ToString().Trim()
            if term.Length <> 0 then 
                let newAnno = {Key = term}::AnnotationState
                newAnno
                |> fun t ->
                t |> setAnnotationState 
                t |> setLocalStorageAnnotation "Annotations"
            else 
                ()
            Browser.Dom.window.getSelection().removeAllRanges()

        Html.div [
            Bulma.columns [
                prop.className "py-5 px-5 text-black"
                prop.children [
                    Bulma.column [
                        column.isThreeFifths
                        prop.children [
                            Bulma.block [
                                prop.text "ProtocolExample.docx" //exchange with uploaded file name
                            ]
                            Bulma.block [
                                prop.className "text-justify bg-slate-100 border-[#10242b] border-4 p-3"
                                prop.children [
                                    Highlighter.Highlighter.highlighter [
                                        Highlighter.Highlighter.textToHighlight (Helper.testText.Replace("  ","" ))
                                        Highlighter.Highlighter.searchWords (annotationToResizeArray) //replace with array of annotated words
                                        // Highlighter.Highlighter.highlightClassName "highlight"
                                        Highlighter.Highlighter.autoEscape true
                                    ]
                                ]
                            ] //exchange with uploaded string list, parsed from uploaded protocol 
                            Bulma.block [
                                Bulma.button.button [
                                    prop.text "Add selected"
                                    prop.onClick (fun e ->
                                        addAnotation
                                    )
                                    
                                ]
                            ]
                            Modal.Main()
                        ]
                    ]
                    Bulma.column [
                        prop.children [
                            Bulma.block [
                                prop.text "Annotations" //exchange with uploaded file name
                            ]
                            for a in 0 .. (AnnotationState.Length - 1)  do
                                Bulma.block [
                                    prop.className "text-justify bg-[#ECBBC3] border-[#10242b] border-4 p-3"
                                    prop.children [
                                        Html.button [
                                            prop.className "delete float-right m-0.5"
                                            prop.onClick (fun _ -> 
                                            let newAnno = List.removeAt a AnnotationState 
                                            newAnno
                                            |> fun t ->
                                            t |> setAnnotationState 
                                            t |> setLocalStorageAnnotation "Annotations"
                                            )
                                        ]
                                        Html.text ( AnnotationState.[a].Key)
                                    ]
                                ]
                        ]
                    ]
                ]
            ]
        ]
        

        
    
