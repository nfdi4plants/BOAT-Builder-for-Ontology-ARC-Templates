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

        let (modalState: DropdownModal, setModalActive) = 
            React.useState({
                isActive = false; 
                location = (0,0) 
                }
            )           
         
        let myDropdownContext = {
            modalState = modalState
            setter = setModalActive 
            }

        let modalContext() = React.createContext(name="modal", defaultValue = myDropdownContext)

        let (modal:ModalContext) = React.useContext(modalContext())

        Html.div []

        



