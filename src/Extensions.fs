[<AutoOpen>]
module Extensions

open System
open Fable.Core
open Fable.Core.JsInterop
open Types
open ARCtrl.Json
open Thoth.Json.Core

let log o = Browser.Dom.console.log o

module Jsonkeys = 
    [<Literal>]
    let Key = "key"
    [<Literal>]
    let Value = "value"
    [<Literal>]
    let IsOpen = "isOpen"
    [<Literal>]
    let Height = "height"
    

let encoderAnno (anno: Annotation) = //encodes annotation to json         
    [
        Encode.tryInclude Jsonkeys.Key OntologyAnnotation.encoder (anno.Key)
        Encode.tryInclude Jsonkeys.Value CompositeCell.encoder (anno.Value)
        Encode.tryInclude Jsonkeys.IsOpen Encode.bool (Some anno.IsOpen)
        Encode.tryInclude Jsonkeys.Height Encode.int (Some anno.Height)
    ]
    |> Encode.choose //only chosse some
    |> Encode.object


let decoderAnno : Decoder<Annotation list> = //decodes json to annotation  
    Decode.list (
        Decode.object (fun get ->
            {
            Key = get.Optional.Field Jsonkeys.Key OntologyAnnotation.decoder 
            Value = get.Optional.Field Jsonkeys.Value CompositeCell.decoder
            IsOpen = get.Required.Field Jsonkeys.IsOpen Decode.bool
            Height = get.Required.Field Jsonkeys.Height Decode.int
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