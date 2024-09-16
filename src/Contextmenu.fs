namespace Components

open Feliz
open Feliz.Bulma
open Types
open Browser
open Browser.Types

module private Helper =

    let rmv_element rmv= 
        Html.div [
            prop.onClick rmv
            prop.onContextMenu(fun e -> e.preventDefault(); rmv e)
            prop.style [
                style.position.fixedRelativeToWindow
                style.backgroundColor.transparent
                style.left 0
                style.top 0
                style.right 0
                style.bottom 0
                style.display.block
            ]
        ]

    let button (name:string, func, props) =
        Html.li [
            Bulma.button.button [
                prop.style [style.borderRadius 0; style.justifyContent.spaceBetween; style.fontSize (length.rem 0.9)]
                prop.onClick func
                prop.className "py-1"
                Bulma.button.isFullWidth
                Bulma.color.isBlack
                Bulma.button.isInverted
                yield! props
                prop.children [
                    Html.span name
                ]
            ]
        ]

    let divider = 
        Html.li [
            Html.div [ prop.style [style.border(1, borderStyle.solid, "black"); style.margin(2,0); style.width (length.perc 75); style.marginLeft length.auto] ]
        ]

open Helper

module Contextmenu =
    let private contextmenu (mousex: int, mousey: int) (func: MouseEvent -> unit) (rmv: _ -> unit) =
        /// This element will remove the contextmenu when clicking anywhere else
        let buttonList = [
            //button ("Edit Column", "fa-solid fa-table-columns", funcs.EditColumn rmv, [])
            button ("Add Term", func, [])
            button ("Add Annotation", func, [])
            divider
            button ("Edit Term", func, [])
            button ("Edit Annotation", func, [])
        ]
        Html.div [
            prop.style [
                style.backgroundColor "white"
                style.position.absolute
                style.left mousex
                style.top (mousey - 40)
                style.width 150
                style.zIndex 40 // to overlap navbar
                style.border(1, borderStyle.solid, "black")
            ]
            prop.children [
                rmv_element rmv
                Html.ul buttonList
            ]
        ]

    let onContextMenu = 
        fun (e: MouseEvent) ->
            e.stopPropagation()
            e.preventDefault()
            let mousePosition = int e.pageX, int e.pageY
            let func = (fun (e: MouseEvent) -> ())
            let child = contextmenu mousePosition func
            let name = $"context_{mousePosition}"
            Modals.Controller.renderModal(name, child)

// type Modal = 
//     [<ReactComponent>]
//     static member Main() = 
//         let (modal:DropdownModal) = React.useContext(Contexts.ModalContextCreator.createModalContext) //Main is able to use context
         
        
        
        // // Get mouse coordinates as int * int            
        // Html.div [
        //     Bulma.button.button [
        //         prop.ariaHasPopup true
        //         prop.target "modal-sample"
        //         prop.text "Create Modal"
        //         prop.onClick (fun e -> modal.setter({
        //             isActive = true;
        //             location = mouseLocation e
        //         }))
        //     ]
        //     Bulma.modal [
        //         prop.id "modal-sample"
        //         if modal.modalState.isActive then Bulma.modal.isActive
        //         prop.children [
        //             Bulma.modalContent [
        //                 Bulma.box [
        //                     Html.h1 "Yey!"
        //                 ]
        //             ]
        //         ]
        //     ]
        // ]

        


        




        



