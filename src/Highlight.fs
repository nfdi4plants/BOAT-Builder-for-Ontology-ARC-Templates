module Highlighter

open Feliz
open Fable.Core.JsInterop 

module HighlighterInterop =

    let highlighter: obj = import "default" "react-highlight-words"

type Highlighter =

    static member inline highlighter props = Interop.reactApi.createElement (HighlighterInterop.highlighter, createObj !!props)
    static member inline highlightClassName (className: string) = prop.custom ("highlightClassName" , className)
    static member inline searchWords (target: ResizeArray<string>) = prop.custom ("searchWords" , target)

    static member inline activeIndex (number: int) = prop.custom ("activeIndex" , number)
    static member inline textToHighlight (txt: string) = prop.custom ("textToHighlight" , txt)
    
    static member inline autoEscape (bool: bool) = prop.custom ("autoEscape" , bool)


    
    
