namespace Components

open Feliz
open Feliz.Bulma
open Types
open Browser
open Browser.Types

module private Helper =

    // let rmv_element (rmv:unit) = 
    //     Html.div [
    //         prop.onClick (fun e -> rmv)
    //         prop.onContextMenu(fun e -> e.preventDefault(); rmv)
    //         prop.style [
    //             style.position.fixedRelativeToWindow
    //             style.backgroundColor.transparent
    //             style.left 0
    //             style.top 0
    //             style.right 0
    //             style.bottom 0
    //             style.display.block
    //         ]
    //     ]

    let preventDefault =
        prop.onContextMenu (fun e ->
            e.stopPropagation()
            e.preventDefault()
        ) 

    let button (name:string, func: MouseEvent -> unit, props) =
        Html.li [
            Bulma.button.button [
                prop.style [style.borderRadius 0; style.justifyContent.spaceBetween; style.fontSize (length.rem 0.9); style.backgroundColor "#cae8f4"]
                prop.onClick func
                preventDefault
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
            Html.div [ 
                prop.className "border border-dashed border-black"
                prop.style 
                    [style.margin(2,0); style.width (length.perc 100); style.marginLeft length.auto] 
                preventDefault
            ]
        ]

open Helper

module Contextmenu =
    let private contextmenu (mousex: int, mousey: int) (func: MouseEvent-> unit) =
        /// This element will remove the contextmenu when clicking anywhere else
        /// 
        let buttonList = [
            button ("Add Annotation", func, [])
            button ("Add Ontology", func, [])
            divider
            button ("Edit Annotation", func, [])
            button ("Edit Ontology", func, [])
        ]
        Html.div [
            prop.style [
                style.backgroundColor "#cae8f4"
                style.position.absolute
                style.left mousex
                style.top mousey
                style.width 150
                style.zIndex 40 // to overlap navbar
                style.border(1, borderStyle.solid, "black")
            ]
            prop.children [
                // rmv_element rmv
                Html.ul buttonList
            ]
        ]

    let initialModal = {
                isActive = false
                location = (0,0)
            }

    let onContextMenu (modalContext:DropdownModal) = 
        let func = (fun (e:MouseEvent) -> modalContext.setter initialModal) //add actual function
        // let rmv = modalContext.setter initialModal 
        contextmenu modalContext.modalState.location func



    
            
            

            
            



        


        




        



