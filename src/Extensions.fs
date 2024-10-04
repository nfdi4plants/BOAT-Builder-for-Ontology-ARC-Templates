[<AutoOpen>]
module Extensions

open System
open Fable.Core
open Fable.Core.JsInterop

let log o = Browser.Dom.console.log o

type URL = 
  abstract member createObjectURL: Browser.Types.File -> string

[<Emit("URL")>]
let URL: URL = jsNative

[<RequireQualifiedAccess>]
module StaticFile =

    /// Function that imports a static file by it's relative path.
    let inline import (path: string) : string = importDefault<string> path

/// Stylesheet API
/// let private stylesheet = Stylesheet.load "./fancy.module.css"
/// stylesheet.["fancy-class-name"] which returns a string
module Stylesheet =

    type IStylesheet =
        [<Emit "$0[$1]">]
        abstract Item : className:string -> string

    /// Loads a CSS module and makes the classes within available
    let inline load (path: string) = importDefault<IStylesheet> path