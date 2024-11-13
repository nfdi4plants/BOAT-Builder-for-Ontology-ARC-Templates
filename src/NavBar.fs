namespace Components

open Feliz
open Feliz.Bulma


type NavBar =
    static member Main(setPage: Types.Page -> unit, statePage: Types.Page) =
        let logo = StaticFile.import "./img/DataPLANT_logo_bg_transparent.svg"
        Bulma.navbar [
            navbar.isFixedTop
            prop.className "select-none"
            prop.children [
                Bulma.navbarBrand.div [
                    Bulma.navbarItem.a [
                        prop.href "https://www.nfdi4plants.de/"
                        prop.target.blank 
                        prop.children [
                            Html.img [ prop.src logo; prop.height 70; prop.width 70]
                        ]
                    ] 
                ]
                Bulma.navbarMenu [
                    Bulma.navbarStart.div [
                        Bulma.navbarItem.div [ prop.text "BOAT - Builder for Ontology ARC Templates"; prop.className "font-semibold" ]
                    ]
                    
                    Bulma.navbarEnd.div [
                        Bulma.navbarItem.a [ 
                            prop.text "Builder"
                            prop.className "hover:bg-[#3f8fae]"
                            prop.onClick (fun _ -> setPage(Types.Page.Builder)) 
                        ]
                        Bulma.navbarItem.a [ 
                            prop.text "Download"
                            prop.className "hover:bg-[#3f8fae]"
                            prop.onClick (fun _ -> ()) 
                        ]
                        Bulma.navbarItem.a [ 
                            prop.text "Help"
                            prop.className "hover:bg-[#3f8fae]"
                            prop.onClick (fun _ -> setPage(Types.Page.Help)) 
                            ]
                        Bulma.navbarItem.a [ 
                            prop.text "Contact"
                            prop.className "hover:bg-[#3f8fae]"
                            prop.onClick (fun _ -> setPage(Types.Page.Contact)) 
                            // prop.children [
                            //     Html.i [prop.className "fa-brands fa-github"; prop.style [style.fontSize (length.rem 3)]]
                            // ]
                        ]
                        Bulma.navbarItem.a [
                        prop.href "https://github.com/nfdi4plants/BOAT-Builder-for-Ontology-ARC-Templates"
                        prop.target.blank 
                        prop.children [
                            Html.img [ 
                                prop.src "./img/github-mark-white.png"
                                prop.className "w-full h-full"
                            ]
                        ]
                        ]
                    ]
                ]
            ]
        ] 
        
