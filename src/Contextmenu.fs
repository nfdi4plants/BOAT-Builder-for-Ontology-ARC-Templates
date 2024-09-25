namespace Components

open Feliz
open Feliz.Bulma
open Types
open Browser
open Browser.Types

module private Helper =

    let preventDefault =
        prop.onContextMenu (fun e ->
            e.stopPropagation()
            e.preventDefault()
        ) 

    let button (name:string, resetter: MouseEvent -> unit, props) =
        Html.li [
            Html.div [
                prop.className "hover:bg-[#a7d9ec] justify-between text-sm text-black select-none p-2"
                prop.onClick resetter
                preventDefault
                yield! props
                prop.text name
            ]
        ]

    let divider = 
        Html.div [ 
            prop.className "border border-slate-400"
            prop.style 
                [style.margin(2,0); style.width (length.perc 75); style.margin length.auto] 
            preventDefault
        ]        

open Helper

module Contextmenu =
    let private contextmenu (mousex: int, mousey: int) (resetter: MouseEvent-> unit) =
        /// This element will remove the contextmenu when clicking anywhere else
        let buttonList = [
            button ("Add Annotation", resetter, [])
            button ("Add Ontology", resetter, []) 
            divider
            button ("Edit Annotation", resetter, [])
            button ("Edit Ontology", resetter, [])
        ]
        Html.div [
            prop.tabIndex 0
            
            preventDefault
            prop.className "border-slate-400 border-solid border"
            prop.style [
                style.backgroundColor "#cae8f4"
                style.position.absolute
                style.left mousex
                style.top mousey
                style.width 150
                style.zIndex 40
            ]
            prop.children [
                Html.ul buttonList
            ]
        ]

    let initialModal = {
                isActive = false
                location = (0,0)
            }

    let onContextMenu (modalContext:DropdownModal)= 
        let resetter = (fun (e:MouseEvent) -> modalContext.setter initialModal) //add actual function
        // let rmv = modalContext.setter initialModal 
        contextmenu modalContext.modalState.location resetter



    
            
            

            
            



        


        




        



