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

    let button (name:string, resetter: unit -> unit, func: unit -> unit, props) =
        Html.li [
            Html.div [
                prop.className "hover:bg-[#a7d9ec] justify-between text-sm text-black select-none p-2"
                prop.onMouseDown (fun e -> 
                    console.log "clicked"
                    func()
                    resetter()
                    ()
                )   
                preventDefault
                prop.onBlur (fun _ -> resetter() )
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
        fun () ->
            let term = window.getSelection().ToString().Trim()
            if term.Length <> 0 then 
                let newAnno = {Key = term}::state
                newAnno
                |> fun t ->
                t |> setState
                t |> setLocal "Annotations"
            else 
                ()
            Browser.Dom.window.getSelection().removeAllRanges()               

    let propPlaceHolder = fun () ->  () //replace with other functions

    let scrollToTop =
        fun () -> Browser.Dom.window.scrollTo(0,0)

open Helper

open Functions

module Contextmenu =
        
    let private contextmenu (mousex: int, mousey: int) (resetter: unit -> unit, state: Annotation list, setState: Annotation list -> unit, setLocal: string -> list<Annotation> -> unit)=
        /// This element will remove the contextmenu when clicking anywhere else
        let buttonList = [
            button ("Add as Key", resetter, addAnnotation(state, setState, setLocal), [])
            button ("Add as Value", resetter, propPlaceHolder , []) 
            divider
            button ("Ontologize as Key", resetter, propPlaceHolder,  [])
            button ("Ontologze as Value", resetter, propPlaceHolder,  [])
            button ("Scroll to top", resetter, scrollToTop,  [])
        ]
        Html.div [
            prop.tabIndex 0
            preventDefault
            prop.className "bg-[#cae8f4] border-slate-400 border-solid border"
            prop.style [
                style.backgroundColor " "
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
        let resetter = fun () -> modalContext.setter initialModal //add actual function
        // let rmv = modalContext.setter initialModal 
        contextmenu (modalContext.modalState.location) (resetter, state, setState, setLocal)



    
            
            

            
            



        


        




        



