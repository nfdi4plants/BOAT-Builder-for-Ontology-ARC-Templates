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
    let addAnnotation (state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit) =
        (fun (e:MouseEvent) ->
            let term = window.getSelection().ToString().Trim()
            if term.Length <> 0 then 
                let newAnno = (state)
                newAnno
                |> fun t ->

                t |> setState
                t |> setLocal "Annotations"
            else 
                ()
            Browser.Dom.window.getSelection().removeAllRanges()
        )

    let propPlaceHolder = fun e -> () //replace with other functions

open Helper

open Functions

module Contextmenu =
        
    let private contextmenu (mousex: int, mousey: int) (resetter: MouseEvent-> unit, state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit)=
        /// This element will remove the contextmenu when clicking anywhere else
        let buttonList = [
            button ("Add Annotation", resetter, addAnnotation(state, setState, setLocal), [])
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

    let onContextMenu (modalContext:DropdownModal, state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit) = 
        let resetter = fun (e:MouseEvent) -> modalContext.setter initialModal //add actual function
        // let rmv = modalContext.setter initialModal 
        contextmenu (modalContext.modalState.location) (resetter, state, setState, setLocal)



    
            
            

            
            



        


        




        



