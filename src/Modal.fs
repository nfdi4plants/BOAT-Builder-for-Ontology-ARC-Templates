namespace Components

open Feliz
open Feliz.Bulma

type DropdownModal = {
    isActive: bool
    location: int * int
}

type ModalContext = {
    modalState: DropdownModal
    setter: DropdownModal -> unit 
}
type Modal = 
    [<ReactComponent>]
    static member Main() =

        let renderModal =
            Bulma.modal [
                prop.text "modaö"
            ]

        let (modalState: DropdownModal, setModal) = 
            React.useState({
                isActive = false; 
                location = (0,0) 
                }
            )           
         
        let myDropdownContext = {
            modalState = modalState
            setter = setModal
            }

        let modalContext() = React.createContext(name="modal", defaultValue = myDropdownContext)

        let (modal:ModalContext) = React.useContext(modalContext())

        let render (state: ModalContext, dispatch: Msg -> unit) =
            React.contextProvider(modalContext, state, React.fragment [
                RenderContent()
            ])

        Html.div []

        



