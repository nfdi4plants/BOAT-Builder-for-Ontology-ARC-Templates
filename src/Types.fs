module Types

type Protocoltext = {
    Content: string list
}

type Annotation = {
    Key: string
}

type DropdownModal = {
    isActive: bool
    location: int * int
}

type ModalInfo = {
    modalState: DropdownModal
    setter: DropdownModal -> unit 
}

// type Annotations = {
//     Annotation: Annotation list
// }

[<RequireQualifiedAccess>]

type Page =
    |Builder
    |Contact
    |Help
