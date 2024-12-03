namespace Components

open Feliz
open Fable.Core
open Fable.Core.JsInterop
open Feliz.Bulma


[<EmitConstructor; Global>]
type Range() =
  member this.setStart: Browser.Types.Node -> int -> unit = jsNative
  member this.setEnd: Browser.Types.Node -> int -> unit = jsNative

[<Global>]
type Highlight([<ParamSeq>] ranges: ResizeArray<Range>) =
  member this.Log() = failwith "This should never be matched"

type IHighlights =
  abstract member clear: unit -> unit
  abstract member set: name:string -> Highlight -> unit

[<Global>]
type CSS =
  static member highlights: IHighlights = jsNative

type PaperWithMarker =

  [<ReactComponent>]
  static member Main(htmlString: string, markedKeys: string [], markedValues: string [], elementID: string) =
    let ref = React.useElementRef()
    let markedNodes, setMarkedNodes = React.useState(ResizeArray())
    let APIwarningModalState, setwarningModal = React.useState(false)
    let hasClosed, setHasClosed = React.useState (false)
    
    React.useEffectOnce(fun _ -> 
      if ref.current.IsSome then
        // https://developer.mozilla.org/en-US/docs/Web/API/Document/createTreeWalker
        let treewalker = Browser.Dom.document.createTreeWalker(ref.current.Value, 0x4) // SHOW_TEXT
        let mutable currentNode = treewalker.nextNode()
        let nodes = ResizeArray()
        while isNullOrUndefined currentNode |> not do
          nodes.Add currentNode
          currentNode <- treewalker.nextNode()
        setMarkedNodes nodes
    )
    React.useEffect(
      (fun () ->
          if CSS.highlights.Equals(null) then setwarningModal true
          else         
            CSS.highlights.clear()
            let rangesKey =
              markedNodes
              |> Array.ofSeq
              |> Array.map (fun n -> {|Node = n; Text = n.textContent.ToLower()|})
              |> Array.collect (fun n ->
                let indices: ResizeArray<int * int> = ResizeArray()
                for phrase0 in markedKeys do 
                  let phrase = phrase0.Trim().ToLower()
                  let index = n.Text.IndexOf(phrase)
                  if index > -1 then
                    indices.Add(index, index + phrase.Length)
                [|
                  for startIndex, endIndex in indices do
                    let range = new Range()
                    range.setStart n.Node startIndex
                    range.setEnd n.Node endIndex
                    range
                |]
              )
              |> ResizeArray
            let highlightKeys = new Highlight(rangesKey)
            CSS.highlights.set "keyColor" highlightKeys; 
            // values
            let rangesValues=
              markedNodes
              |> Array.ofSeq
              |> Array.map (fun n -> {|Node = n; Text = n.textContent.ToLower()|})
              |> Array.collect (fun n ->
                let indices: ResizeArray<int * int> = ResizeArray()
                for phrase0 in markedValues do 
                  let phrase = phrase0.Trim().ToLower()
                  let index = n.Text.IndexOf(phrase)
                  if index > -1 then
                    indices.Add(index, index + phrase.Length)
                [|
                  for startIndex, endIndex in indices do
                    let range = new Range()
                    range.setStart n.Node startIndex
                    range.setEnd n.Node endIndex
                    range
                |]
              )
              |> ResizeArray
            let highlightValues = new Highlight(rangesValues)
            CSS.highlights.set "valueColor" highlightValues 
        )
    )
    Html.div [
      Bulma.modal [
        if APIwarningModalState = true && hasClosed = false then Bulma.modal.isActive
        else ()
        prop.children [
          Bulma.modalBackground []
          Bulma.modalContent [
            Bulma.box [
              Bulma.block [
                Html.p "Text highlighting is not compatible with your browser."
                Html.a [
                    prop.text "View compatible browsers."
                    prop.href "https://developer.mozilla.org/en-US/docs/Web/API/CSS_Custom_Highlight_API#browser_compatibility"
                    prop.target.blank 
                    prop.className "underline text-blue-400"
                ]
              ]
              Bulma.button.button [
                prop.text "Got it"
                prop.onClick (fun _ -> setHasClosed(true))
              ]
            ]
          ]
        ]
      ] 
      Html.div [    
        prop.dangerouslySetInnerHTML htmlString
        prop.className "prose bg-slate-100 p-3 text-black max-w-4xl"  
        prop.id elementID   
        prop.ref ref      
      ]
    ]

