module Types

type Protocoltext = {
    Content: string list
}

type Annotation = {
    Key: string
}

type ModalInfo = {
    isActive: bool
    location: int * int
}

type DropdownModal = {
    modalState: ModalInfo
    setter: ModalInfo -> unit 
}

// type Annotations = {
//     Annotation: Annotation list
// }

[<RequireQualifiedAccess>]

type Page =
    |Builder
    |Contact
    |Help
