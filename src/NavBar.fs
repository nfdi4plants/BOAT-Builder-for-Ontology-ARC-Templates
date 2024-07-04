namespace Components

open Feliz
open Feliz.Bulma


type NavBar =
    static member Subpagelink(setPage: Types.Page   -> unit, statePage: Types.Page) =
        Html.div [
            let logo = StaticFile.import "./img/DataPLANT_logo_bg_transparent.svg"
            Bulma.navbar [
                prop.className "navbar"
                prop.children [
                    Bulma.navbarBrand.div [
                        Bulma.navbarItem.a [
                            prop.href "https://www.nfdi4plants.de/"
                            prop.target.blank 
                            prop.children [
                                Html.img [ 
                                    prop.src logo
                                    prop.className "w-full h-full"
                                ]
                            ]
                        ]
                    ]
                    Bulma.navbarMenu [
                        Bulma.navbarStart.div [
                            Bulma.navbarItem.div [ prop.text "BOAT" ]
                        ]
                        Bulma.navbarEnd.div [
                            Bulma.navbarItem.div [
                                Bulma.navbarItem.a [ 
                                    prop.text "Builder"
                                    prop.onClick (fun _ -> setPage(Types.Page.Builder)) 
                                    ]
                                Bulma.navbarItem.a [ 
                                    prop.text "Restart"
                                    prop.onClick (fun _ -> ()) 
                                    ]
                                Bulma.navbarItem.a [ prop.text "Download" ]
                                Bulma.navbarItem.a [ 
                                    prop.text "Help"
                                    prop.onClick (fun _ -> setPage(Types.Page.Help)) 
                                    ]
                                Bulma.navbarItem.a [ 
                                    prop.text "Contact"
                                    prop.onClick (fun _ -> setPage(Types.Page.Contact)) 
                                    ]
                                Bulma.navbarItem.a [
                                    prop.href "https://github.com/Rookabu/Project-GTHelper"
                                    prop.target.blank 
                                    prop.className "flex"
                                    prop.style [style.paddingLeft 0; style.paddingRight 0] 
                                    prop.children [
                                        Html.i [prop.className "fa-brands fa-github"; prop.style [style.fontSize (length.rem 3)]]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]