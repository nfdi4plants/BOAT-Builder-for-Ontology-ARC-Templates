namespace Components

open Feliz
open Types
open Fable.SimpleJson
open Feliz.Bulma
open System.Text.RegularExpressions


module private FileReaderHelper =
  open Fable.Core
  open Fable.Core.JsInterop

  [<Emit("new FileReader()")>]
  let newFileReader(): Browser.Types.FileReader = jsNative

  let readDocx (file: Browser.Types.File) setState setLocalFile = 
    let reader = newFileReader()
    reader.onload <- fun e ->
      let arrayBuffer = e.target?result
      promise {
        let! r = Mammoth.mammoth.convertToHtml({|arrayBuffer = arrayBuffer|})
        (Docx r.value)
        |> fun t ->
          t |> setState
          t |> setLocalFile "file"
      }
      |> Promise.start

    reader.onerror <- fun e ->
      Browser.Dom.console.error ("Error reading file", e)
    reader.readAsArrayBuffer(file)

  // let readPdf (file: Browser.Types.File) setState =
  //   let src = URL.createObjectURL(file)
  //   log ("Uploaded PDF:", src)
  //   setState (PDF src)

  let readFromFile (file: Browser.Types.File) setState (fileType: UploadFileType) setLocalFile =
    match fileType with
    | UploadFileType.Docx -> readDocx file setState setLocalFile
    // | UploadFileType.PDF -> readPdf file setState

module Highlight =

  let keyList(annoList: Annotation list) = 
    [|
        for a in annoList do
            (a.Key |> Option.map (fun e -> e.Name.Value)|> Option.defaultValue "")
    |]
  let valuelist(annoList: Annotation list) = 
    [|
        for a in annoList do
            (a.Value |> Option.map (fun e -> e.ToString()) |> Option.defaultValue "" )
    |]

  // let allList (annoList: Annotation list) =
  //   keyList(annoList) @ valuelist(annoList)


  let highlightAnnos(text: string, list: string list) =
      // let keyHighlighttext =
        List.fold (fun (acc: string) key -> 
          acc.Replace(key, $"<mark>{key}</mark>")
        ) text list


        

      // List.fold (fun (acc: string) value -> 
      //   acc.Replace(value, $"<mark>{value}</mark>")
      // ) keyHighlighttext values


type Components =
    static member DisplayHtml(htmlString: string, annoList: Annotation list, elementID: string) = 
      // Html.div [    
      //       // prop.innerHtml (Highlight.highlightAnnos (htmlString, Highlight.keyList (annoList)))
      //       prop.dangerouslySetInnerHTML htmlString
      //       prop.className "prose bg-slate-100 p-3 text-black max-w-4xl"  
      //       prop.id elementID        
      // ]
      Html.div [
        Components.PaperWithMarker.Main(htmlString, Highlight.keyList(annoList), Highlight.valuelist(annoList), elementID)
      ]


    /// https://stackoverflow.com/a/60539836/12858021
    static member DisplayPDF(pdfSource: string, modalContext: DropdownModal) =
      Html.div [
        prop.className "content"
        prop.onContextMenu (fun e ->
            let term = Browser.Dom.window.getSelection().ToString().Trim() 
            if term.Length <> 0 then 
                modalContext.setter {
                    isActive = true;
                    location = int e.pageX, int e.pageY
                }
                e.stopPropagation() 
                e.preventDefault()
            else 
                ()
        )
        prop.children [
          Html.embed [
            prop.src pdfSource
            prop.type' "application/pdf"
            prop.style [
              style.minHeight (length.perc 100)
              style.width (length.perc 100)
              style.height 600
            ]
          ]
        ]
      ]


    static member private FileUpload (ref: IRefValue<Browser.Types.HTMLInputElement option>) filehtml uploadFileType setUploadFileType setFilehtml setLocalFile setState setFileName setLocalFileName modalState toggleState=
      Html.div [
        Bulma.block [
          Html.div [
            prop.className "field has-addons"
            prop.children [
              // upload select
              Html.p [
                prop.className "control"
                prop.children [
                  Html.span [
                    prop.className "select"
                    prop.children [
                      Html.select [
                        prop.onChange (fun (e: string) -> 
                          match e with
                          | "Docx" -> setUploadFileType(UploadFileType.Docx)
                          // | "PDF" -> setUploadFileType(UploadFileType.PDF)
                          | _ -> ()
                        )
                        prop.children [
                          Html.option [
                            prop.value "Docx"
                            prop.text "Docx"
                          ]
                          // Html.option [
                          //   prop.value "PDF"
                          //   prop.text "PDF"
                          // ]
                        ]
                      ]
                    ]
                  ]
                ]
              ]
              
              // file upload input
              Html.div [
                prop.className "control"
                prop.children [
                  Html.div [
                    prop.className "file"
                    prop.children [
                      Html.label [
                        prop.className "file-label"
                        prop.children [
                          Html.input [
                            prop.className "file-input"
                            prop.ref ref
                            prop.type'.file
                            prop.onChange (fun (f: Browser.Types.File) -> 
                              FileReaderHelper.readFromFile f setFilehtml uploadFileType setLocalFile
                              if ref.current.IsSome then
                                ref.current.Value.value <- null

                              f.name
                              |> fun t ->
                              t |> setFileName
                              t |> setLocalFileName "fileName"
                            )
                          ]
                          Html.span [
                            prop.className "file-cta"
                            prop.style [style.borderRadius(0, 6, 6, 0)]
                            prop.children [
                              Html.span [
                                prop.className "file-icon"
                                prop.children [
                                  Html.i [
                                    prop.className "fa-solid fa-upload"
                                  ]
                                ]
                              ]
                              Html.span [
                                prop.className "file-label"
                                prop.text "Choose a file…"
                              ]
                            ]
                          ]
                          
                        ]
                      ]
                    ]
                  ]
                ]
              ]
            ]
          ]
        ]
        Html.button [
          if filehtml = Unset then prop.hidden (true)
          prop.className "pl-1"
          prop.children [
            Html.span [
              Html.i [
                prop.className "fa-solid fa-trash-can"
                prop.onClick (fun e -> 
                  Unset
                  |> fun t ->
                  t |> setFilehtml
                  t |> setLocalFile "file"

                  [] |> setState

                  ""   
                  |> fun t ->
                  t |> setFileName
                  t |> setLocalFileName "fileName"                     
                )
              ]
            ]
          ]
        ]

      ]

      

    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member UploadDisplay(filehtml, setFilehtml, setState, setFileName, setLocalFileName) =
    
        let uploadFileType, setUploadFileType = React.useState(UploadFileType.Docx)

        let modalState, toggleState = React.useState(false)

        let setLocalFile (id: string)(nextFile: UploadedFile) =
            let JSONString = Json.stringify nextFile 
            Browser.WebStorage.localStorage.setItem(id, JSONString)

        let ref = React.useInputRef()
        Html.div [
          prop.className "section p-0 space-y-4" 
          prop.children [
              Html.div [
                  prop.className "container"
                  prop.children [
                    Components.FileUpload ref filehtml uploadFileType setUploadFileType setFilehtml setLocalFile setState setFileName setLocalFileName modalState toggleState 
                  ]
              ]
          ]
        ]