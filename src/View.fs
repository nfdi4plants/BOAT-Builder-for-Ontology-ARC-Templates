namespace App

open Feliz
open Feliz.Bulma

type View =
    [<ReactComponent>]
    static member Main() =
        let currentpage,setpage = React.useState(Types.Page.Builder) //Reagiert beim clicken. Start state ist der Counter ->noch in Srat menü umändern
        printfn "%A" currentpage 
        Html.div [
            prop.id "mainView"
            prop.className "flex h-full flex-col"
            prop.children [    
                Components.NavBar.Main(setpage,currentpage)
                Html.div [
                    prop.id "contentView"
                    prop.className "grow"
                    prop.children [
                        match currentpage with
                        |Types.Page.Builder -> Components.Builder.Main()
                        |Types.Page.Contact -> Components.Contact.Main() 
                        |Types.Page.Help -> Components.Help.Main() 
                    ]
                ]
                Components.Footer.Main
               //wird gemachted da die jeweilige component aufgerufen werden soll je nach page (application state)
            ]
        ]