module Types

// open ARCtrl

type Protocoltext = {
    Content: string list
}

// type Annotation = {
//     Key: OntologyAnnotation option
//     Value: CompositeCell option
// }

type Annotation = {
    Key: string
    Value: string
}

type ModalInfo = {
    isActive: bool
    location: int * int
}

type DropdownModal = {
    modalState: ModalInfo
    setter: ModalInfo -> unit 
}

type ActiveField = 
    |Key
    |Value
    |Unset

[<RequireQualifiedAccess>]

type Page =
    |Builder
    |Contact
    |Help

type UploadFileType =
  | Docx
//   | PDF

type UploadedFile =
//   | PDF of string
  | Docx of string
  | Unset
