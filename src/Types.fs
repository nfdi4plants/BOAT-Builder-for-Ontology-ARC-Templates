module Types

type Protocoltext = {
    Content: string list
}

type Annotation = {
    Key: string
}

type Annotations = {
    Annotation: Annotation list
}

[<RequireQualifiedAccess>]

type Page =
    |Builder
    |Contact
    |Help
