let splitAtTerm (annotation: string) (prototext: string) =
    prototext.Split([|annotation|], System.StringSplitOptions.None)
    |> Array.toList

let inputString = "This is a test string with a specific term followed by text."
let result = splitAtTerm "term" inputString
result

let splitAtTerm2 (annotation: string) (prototext: string) =
    prototext.Split " "
    |> Array.fold (fun (acc: (string list)[]) s ->
        if s <> annotation && acc.Length = 1 then
            acc.[0] <- s::acc.[0]
            acc
        elif s = annotation then
            Array.append acc [|[s]|]
        else
            if acc.Length = 2 then
                Array.append acc [|[s]|]
            else
                acc.[2] <- s::acc.[2]
                acc
    ) [|[]|]
    |> Array.map (List.rev >> String.concat " ")
        

let test = splitAtTerm2 "term" inputString


open System.Text.RegularExpressions

let pattern term = $"^(?<group1>.*?)(?<group2>{term})(?<group3>.*)$"

let match2 = Regex.Match(inputString,pattern "term")
match2.Groups.["group1"].Value
match2.Groups.["group2"].Value
match2.Groups.["group3"].Value
 