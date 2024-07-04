namespace App

open Feliz
open Feliz.Router
open Feliz.Bulma

type Components =
    [<ReactComponent>]
    static member Builder() =
        Bulma.columns [
            Bulma.column [
                Bulma.column.isTwoThirds
                prop.text "example text"
            ]
            Bulma.column [
                prop.text "annotation text"
            ]
        ]
        
    

// <div class="columns">
//   <div class="column is-two-thirds">is-two-thirds</div>
//   <div class="column">Auto</div>
//   <div class="column">Auto</div>
// </div>
