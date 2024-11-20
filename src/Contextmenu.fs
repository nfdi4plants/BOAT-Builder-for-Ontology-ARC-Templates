namespace Components

open Feliz
open Feliz.Bulma
open Types
open Browser
open Browser.Types
open ARCtrl


module private Helper =

    let preventDefault =
        prop.onContextMenu (fun e ->
            e.stopPropagation()
            e.preventDefault()
        ) 



    let button (name:string, resetter: unit -> unit, state, func: unit -> unit, props) =
        Html.li [
            Html.div [
                prop.className "hover:bg-[#a7d9ec] justify-between text-sm text-black select-none p-2"
                prop.onMouseDown (fun e -> 
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
                [style.margin(2,0); style.width (length.perc 80); style.margin length.auto; style.margin (length.rem 0)]  
            preventDefault
        ]        

module private Functions =

    
    let addAnnotationKeyNew (state: Annotation list, setState: Annotation list -> unit, elementID: string) ()=       
        let term = window.getSelection().ToString().Trim() 
        let yCoordinateOfSelection  =
            match window.getSelection() with
            | (selection: Selection) when selection.rangeCount > 0 ->
                let range = selection.getRangeAt(0)
                let rect = range.getBoundingClientRect()
                let relativeParent = document.getElementById("Paper").getBoundingClientRect()
                rect.bottom - relativeParent.top
                
            | _ -> 0.0    

        if term.Length <> 0 then 
            let newAnnoList = Annotation.init(OntologyAnnotation(term), height = yCoordinateOfSelection)::state
            setState newAnnoList

        else 
            ()

        log yCoordinateOfSelection
            
        Browser.Dom.window.getSelection().removeAllRanges()  

        
    let addAnnotationValueNew (state: Annotation list, setState: Annotation list -> unit) ()=  
        let term = window.getSelection().ToString().Trim()      
        let yCoordinateOfSelection  =
            match window.getSelection() with
            | (selection: Selection) when selection.rangeCount > 0 ->
                let range = selection.getRangeAt(0)
                let rect = range.getBoundingClientRect()
                let relativeParent = document.getElementById("Paper").getBoundingClientRect()
                rect.bottom - relativeParent.top
                
            | _ -> 0.0    

        if term.Length <> 0 then 
            let newAnnoList = Annotation.init(value = CompositeCell.createFreeText(term), height = yCoordinateOfSelection)::state
            setState newAnnoList
            
        else 
            ()

        log yCoordinateOfSelection

        Browser.Dom.window.getSelection().removeAllRanges()   

    let propPlaceHolder() =  () //replace with other functions

    let addToLastAnnoAsKey(state: Annotation list, setState: Annotation list -> unit) () =
        let term = window.getSelection().ToString().Trim()
        if term.Length <> 0 then 
            let updatetedAnno = 
                {state.[0] with Key = OntologyAnnotation(name = term) |> Some}

            let newAnnoList =
                state
                |> List.mapi (fun i elem -> if i = 0 then updatetedAnno else elem)

            setState newAnnoList

            

    let addToLastAnnoAsValue(state: Annotation list, setState: Annotation list -> unit) () =
        let term = window.getSelection().ToString().Trim()
        if term.Length <> 0 then 
            let updatetedAnno = 
                {state.[0] with Value = CompositeCell.createFreeText(term) |> Some}

            let newAnnoList =
                state
                |> List.mapi (fun i elem -> if i = 0 then updatetedAnno else elem)

            setState newAnnoList

open Helper

open Functions

module Contextmenu =
    let private contextmenu (mousex: int, mousey: int) (resetter: unit -> unit, state: Annotation list, setState: Annotation list -> unit, elementID:string)=
        /// This element will remove the contextmenu when clicking anywhere else
        let buttonList = [
            button ("Add as new Key", resetter, state, addAnnotationKeyNew(state, setState, elementID), [])
            button ("Add as new Value", resetter,state, addAnnotationValueNew(state, setState), []) 
            divider
            Html.div [ 
                prop.className "text-gray-500 text-sm p-1"
                prop.text "Add to last annotation .."
                preventDefault
            ]
            button ("as Key", resetter,state, addToLastAnnoAsKey(state, setState),  [])
            button ("as Value", resetter,state, addToLastAnnoAsValue(state, setState),  [])
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

    let onContextMenu (modalContext:DropdownModal, state: Annotation list, setState: Annotation list -> unit, elementID:string) = 
        let resetter = fun () -> modalContext.setter initialModal //add actual function
        // let rmv = modalContext.setter initialModal 
        contextmenu (modalContext.modalState.location) (resetter, state, setState,elementID)



    
            
            

            
            



        


        




        



