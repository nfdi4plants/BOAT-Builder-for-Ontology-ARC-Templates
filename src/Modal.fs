namespace Components

open Feliz
open Feliz.Bulma
open Types
open Browser
open Browser.Types

// module private functions =

    // let addAnotation =
    //     let term = window.getSelection().ToString().Trim()
    //     if term.Length <> 0 then 
    //         let newAnno = {Key = term}::AnnotationState
    //         newAnno
    //         |> fun t ->
    //         t |> setAnnotationState 
    //         t |> setLocalStorageAnnotation "Annotations"
    //     else 
    //         ()
    //     Browser.Dom.window.getSelection().removeAllRanges()

module private Helper =

    let isHeader (rowIndex: int) = rowIndex < 0

    let rmv_element  = 
        Html.div [
            // prop.onClick rmv
            prop.onContextMenu(fun e -> e.preventDefault())
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
    let button (name:string) = 
        Html.li [
            Bulma.button.button [
                prop.style [style.borderRadius 0; style.justifyContent.spaceBetween; style.fontSize (length.rem 0.9)]
                // prop.onClick func
                prop.className "py-1"
                Bulma.button.isFullWidth
                Bulma.color.isBlack
                Bulma.button.isInverted
                
                prop.children [
                    Html.span name
                ]
            ]
        ]

    let divider = 
        Html.li [
            Html.div [ prop.style [style.border(1, borderStyle.solid, "black"); style.margin(2,0); style.width (length.perc 75); style.marginLeft length.auto] ]
        ]

    let contextmenu (mouseX: int, mouseY: int) = //add functions
        /// This element will remove the contextmenu when clicking anywhere else

        let buttonList = [
            //button ("Edit Column", "fa-solid fa-table-columns", funcs.EditColumn rmv, [])
            button ("Add Term")
            button ("Add Annotation")
            divider
            button ("Edit Term")
            button ("Edit Annotation")
        ]
        Html.div [
            prop.style [
                style.backgroundColor "white"
                style.position.absolute
                style.left mouseX
                style.top (mouseY - 40)
                style.width 150
                style.zIndex 40 // to overlap navbar
                style.border(1, borderStyle.solid, "black")
            ]
            prop.children [
                rmv_element
                Html.ul buttonList
            ]
        ]

type Modal = 
    [<ReactComponent>]
    static member Main() = 
        let (modal:DropdownModal) = React.useContext(Contexts.ModalContextCreator.createModalContext) //Main is able to use context

        let mouseX (event: Browser.Types.MouseEvent)  = int event.pageX
        let mouseY (event: Browser.Types.MouseEvent) = int event.pageY
         
        Helper.contextmenu 
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

        


        




        



