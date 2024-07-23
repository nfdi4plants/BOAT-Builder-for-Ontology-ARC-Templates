namespace Components

open Feliz
open Feliz.Bulma


type NavBar =
    static member Subpagelink(setPage: Types.Page -> unit, statePage: Types.Page) =
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
                                prop.text "Restart"
                                prop.className "hover:bg-[#3f8fae]"
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
            Bulma.footer [
                prop.className "content has-text-centered footer"
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
        ]


//         <footer class="footer">
//   <div class="content has-text-centered">
//     <p>
//       <strong>Bulma</strong> by <a href="https://jgthms.com">Jeremy Thomas</a>.
//       The source code is licensed
//       <a href="https://opensource.org/license/mit">MIT</a>. The
//       website content is licensed
//       <a href="https://creativecommons.org/licenses/by-nc-sa/4.0//"
//         >CC BY NC SA 4.0</a
//       >.
//     </p>
//   </div>
// </footer>