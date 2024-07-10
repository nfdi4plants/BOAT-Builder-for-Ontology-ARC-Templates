namespace Components

open Feliz
open Feliz.Bulma


type NavBar =
    static member Subpagelink(setPage: Types.Page -> unit, statePage: Types.Page) =
        Html.div [
            let logo = StaticFile.import "./img/DataPLANT_logo_bg_transparent.svg"
            Bulma.navbar [
                prop.className "navbar py-0"
                prop.children [
                    Bulma.navbarBrand.div [
                        // Bulma.navbarItem.a [
                        //     prop.href "https://www.nfdi4plants.de/"
                        //     prop.target.blank 
                        //     prop.className "bg-transparent border-0 hover:bg-[#222a35]"
                        //     prop.children [
                        //         Html.img [ 
                        //             prop.src logo
                        //             prop.className "w-full h-full"
                        //         ]
                        //     ]
                        // ]
                        Bulma.navbarItem.a [
                            Html.img [ prop.src logo; prop.height 40; prop.width 150]
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
            //replace this with navbar from home
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