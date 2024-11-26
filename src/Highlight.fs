namespace Components

open Feliz
open Fable.Core
open Fable.Core.JsInterop


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
    React.useEffect( //highlight keys
      (fun () ->
        CSS.highlights.clear()
        let ranges =
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
        let highlight = new Highlight(ranges)
        CSS.highlights.set "keyColor" highlight 
      )
    )
    // React.useEffect( //highlight values
    //   (fun () ->
    //     CSS.highlights.clear()
    //     let ranges =
    //       markedNodes
    //       |> Array.ofSeq
    //       |> Array.map (fun n -> {|Node = n; Text = n.textContent.ToLower()|})
    //       |> Array.collect (fun n ->
    //         let indices: ResizeArray<int * int> = ResizeArray()
    //         for phrase0 in markedValues do 
    //           let phrase = phrase0.Trim()
    //           let index = n.Text.IndexOf(phrase)
    //           if index > -1 then
    //             indices.Add(index, index + phrase.Length)
    //         [|
    //           for startIndex, endIndex in indices do
    //             let range = new Range()
    //             range.setStart n.Node startIndex
    //             range.setEnd n.Node endIndex
    //             range
    //         |]
    //       )
    //       |> ResizeArray
    //     let highlight = new Highlight(ranges)
    //     CSS.highlights.set "valueColor" highlight
    //   )
    // )
    Html.div [    
        prop.dangerouslySetInnerHTML htmlString
        prop.className "prose bg-slate-100 p-3 text-black max-w-4xl"  
        prop.id elementID   
        prop.ref ref      
    ]
    
