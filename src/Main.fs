module Main

open Feliz
open Browser.Dom
open Fable.Core.JsInterop
open Fable.React


// let Foo : ComponentClass<obj> = import "Foo" "react-bar"
// let inline foo (props : IFooProps list) elems =
//     Fable.Helpers.React.from Foo (keyValueList CaseRules.LowerFirst props) elems

importSideEffects "./tailwind.scss"
importSideEffects "./styling.scss"


let root = ReactDOM.createRoot(document.getElementById "feliz-app")
root.render(App.View.Main())