module Types

open ARCtrl

type Protocoltext = {
    Content: string list
}

type Annotation = 
    {
        Key: OntologyAnnotation option
        Value: CompositeCell option
        IsOpen: bool 
        Height: int
    } 
    static member init (?key,?value,?isOpen, ?height) = 
        let isOpen = defaultArg isOpen true
        let height = 0
        {
            Key= key
            Value= value
            IsOpen= isOpen
            Height= height
        }
    member this.ToggleOpen () = {this with IsOpen = not this.IsOpen}

type ModalInfo = {
    isActive: bool
    location: int * int
}

type DropdownModal = {
    modalState: ModalInfo
    setter: ModalInfo -> unit 
}


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
