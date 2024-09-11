module Contexts 

open Feliz
open Types

module ModalContextCreator =

    let createModalContext:Fable.React.IContext<DropdownModal> = React.createContext(name="modal") //makes context


