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

module Helper =

    let testText = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, 
        totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. 
        Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui 
        ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci 
        velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad
        minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? 
        Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem 
        eum fugiat quo voluptas nulla pariatur? At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium
        voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique 
        sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita 
        distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere 
        possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum 
        necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a 
        sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat."

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
        
        {|State = AnnotationState; Setter = setAnnotationState; LocalSetter =setLocalStorageAnnotation|}
 
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
                                prop.className "border border-slate-400 text-justify bg-slate-100 p-3 text-black"
                                prop.children [
                                    Highlighter.Highlighter.highlighter [
                                        Highlighter.Highlighter.textToHighlight (Helper.testText.Replace("  ","" ))
                                        Highlighter.Highlighter.searchWords (annotationToResizeArray) //replace with array of annotated words
                                        // Highlighter.Highlighter.highlightClassName "highlight"
                                        Highlighter.Highlighter.autoEscape true
                                    ]
                                ]
                            ]
                            //exchange with uploaded string list, parsed from uploaded protocol
                            Bulma.block [
                                Bulma.button.button [
                                    prop.text "Add selected"
                                    prop.onClick (fun e ->
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
                                    )
                                ]
                            ]
                        ]
                    ]
                    Bulma.column [
                        prop.className "select-none"
                        prop.children [
                            Bulma.block [
                                prop.text "Annotations"
                            ]
                            for a in 0 .. (AnnotationState.Length - 1)  do
                                Bulma.block [
                                    prop.className "border border-slate-400 text-justify bg-[#E6A5B0] p-3 text-black "
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
        
        

        
    
