namespace App

open Feliz
open Feliz.Router

type Components =
    [<ReactComponent>]
    static member Builder() =
        let (count, setCount) = React.useState(0)
        Html.div [
            Html.h1 count
            Html.button [
                prop.onClick (fun _ -> setCount(count + 1))
                prop.text "Increment"
            ]
        ]

