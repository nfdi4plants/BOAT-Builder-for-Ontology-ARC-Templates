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

    let button (name:string, resetter: MouseEvent -> unit, func: MouseEvent -> unit, props) =
        Html.li [
            Html.div [
                prop.className "hover:bg-[#a7d9ec] justify-between text-sm text-black select-none p-2"
                prop.onClick func
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

module private Functions =
    let addAnnotation =
        (fun (e:MouseEvent) ->
            let term = window.getSelection().ToString().Trim()
            if term.Length <> 0 then 
                let newAnno = ({Key = term}::Builder.Main().State)
                newAnno
                |> fun t ->

                t |> Builder.Main().Setter 
                t |> Builder.Main().LocalSetter "Annotations"
            else 
                ()
            Browser.Dom.window.getSelection().removeAllRanges()
        )

    let propPlaceHolder = fun e -> () //replace with other functions

open Helper

open Functions

module Contextmenu =
        
    let private contextmenu (mousex: int, mousey: int) (resetter: MouseEvent-> unit) =
        /// This element will remove the contextmenu when clicking anywhere else
        let buttonList = [
            button ("Add Annotation", resetter, addAnnotation, [])
            button ("Add Ontology", propPlaceHolder, resetter, []) 
            divider
            button ("Edit Annotation", propPlaceHolder, resetter, [])
            button ("Edit Ontology", propPlaceHolder, resetter, [])
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

    let onContextMenu (modalContext:DropdownModal) = 
        let resetter = fun (e:MouseEvent) -> modalContext.setter initialModal //add actual function
        // let rmv = modalContext.setter initialModal 
        contextmenu modalContext.modalState.location resetter



    
            
            

            
            



        


        




        



