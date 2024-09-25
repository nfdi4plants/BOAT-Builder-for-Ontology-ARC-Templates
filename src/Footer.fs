namespace Components

open Feliz
open Feliz.Bulma


type Footer =
    static member Main =
        Bulma.footer [
            prop.className "content has-text-centered h-16 select-none"
            prop.children [
                Html.div [
                    prop.className "inline-flex"
                    prop.children [
                        Html.p [
                            prop.text "This is a footer. By"
                            prop.className "pr-1" 
                        ]
                        Html.a [
                            prop.text "ndfdi4plants"
                            prop.href "https://www.nfdi4plants.de/"
                            prop.target.blank 
                            prop.className "underline text-blue-400"
                        ]
                    ]
                ]
            ]
        ]