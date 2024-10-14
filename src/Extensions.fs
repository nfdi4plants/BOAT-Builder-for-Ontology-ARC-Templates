[<AutoOpen>]
module Extensions

open System
open Fable.Core
open Fable.Core.JsInterop
open Types
open ARCtrl.Json
open Thoth.Json.Core

let log o = Browser.Dom.console.log o

let encoderAnno (anno: Annotation) =         
    [
        Encode.tryInclude "Key" OntologyAnnotation.encoder (anno.Key)
        Encode.tryInclude "Value" CompositeCell.encoder (anno.Value)
    ]
    |> Encode.choose
    |> Encode.object

let decoderAnno : Decoder<Annotation list> =
    Decode.list (
            Decode.object (fun get ->
            {
            Key = get.Optional.Field "Key" OntologyAnnotation.decoder 
            Value = get.Optional.Field "Value" CompositeCell.decoder
            }
        )
    )
    
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