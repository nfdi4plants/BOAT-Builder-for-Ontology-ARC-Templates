namespace Components

open Feliz
open Feliz.Bulma
open Browser.Dom
open Types

module private Helper =

    let addAnnotation (newAnnotation: Annotation, protoIndex: int, state: Map<int, Annotation>) = 
        state.Add (protoIndex, newAnnotation)    
        // |> updateAnnotations pubIndex 
        // reset()  //resets

    // let ontologylist: string list = [
    //             match (Map.tryFind pubIndex interactionState) with
    //             |Some interactions ->
    //                 for interaction in interactions do
    //                     yield! interaction.Partner1.Split([|' '|])
    //                     yield! interaction.Partner2.Split([|' '|])
    //             |None -> ()
    //         ]

type Builder =
    [<ReactComponent>]
    static member Main() =
        let (AnnotationState: Annotation list, setAnnotationState) = React.useState ([])
        // let (selectedText, setText) = React.useState ("")
        Html.div [
            Bulma.columns [
                prop.className "pt-16 px-5"
                prop.children [
                    Bulma.column [
                        column.isThreeFifths
                        prop.children [
                            Bulma.block [
                                prop.text "ProtocolExample.docx" //exchange with uploaded file name
                            ]
                            Bulma.block [
                                prop.className "text-justify bg-slate-100 border-[#10242b] border-4 p-3"
                                prop.text "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?
                                    At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat."                               
                            ] //exchange with uploaded string list, parsed from uploaded protocol 
                            Bulma.block [
                                Bulma.button.button [
                                    prop.text "Add selected"
                                    prop.onClick (fun e ->
                                        let term = window.getSelection().ToString()
                                        let newAnno = term::AnnotationState
                                        setAnnotationState newAnno
                                    )
                                ]
                            ]
                        ]
                    ]
                    Bulma.column [
                        prop.children [
                            Bulma.block [
                                prop.text "Annotations" //exchange with uploaded file name
                            ]
                            //for every annotation one block
                            Bulma.block [
                                prop.className "text-justify bg-[#ECBBC3] border-[#10242b] border-4 p-3"
                                prop.text (AnnotationState.ToString()) //
                            ] //exchange with uploaded string list, parsed from uploaded protocol 
                        ]
                    ]
                ]
            ]
        ]
        
    

// <div class="columns">
//   <div class="column is-two-thirds">is-two-thirds</div>
//   <div class="column">Auto</div>
//   <div class="column">Auto</div>
// </div>
