module Contexts 

open Feliz
open Types

module ModalContext=

    // let (modalState: ModalInfo, setModal) =
    //         React.useState({
    //             isActive = false;
    //             location = (0,0)
    //             }
    //         )
            
    // let myModalContext = { //makes setter and state in one record type
    //     modalState = modalState
    //     setter = setModal
    //     }

    let createModalContext:Fable.React.IContext<DropdownModal> = React.createContext(name="modal") //makes context


