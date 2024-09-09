module Contexts 

open Feliz
open Types

module ModalContextCreator =

    let createModalContext:Fable.React.IContext<ModalInfo> = React.createContext(name="modal") //makes context


