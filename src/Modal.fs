namespace Components

open Feliz
open Feliz.Bulma

type DropdownModal = {
    isModalActive: bool
    setModalActive: bool -> unit


}

type Modal = 
    [<ReactComponent>]
    static member Main() =

        let (isModalActive: bool, setModalActive) = React.useState(false) 
        

        // isactive und location als state
        // setter als option definieren

        let setModal (isActive:bool) (location: int * int) =
            match isModalActive with
            |true -> 
            |false ->  
         
        let myDropdownModal = {
            isModalActive = isModalActive
            setModalActive = setModalActive 
            }

        let modalContext() = React.createContext(name="modal", defaultValue = myDropdownModal)

        let (modal:DropdownModal) = React.useContext(modalContext())

        Html.div []

        



