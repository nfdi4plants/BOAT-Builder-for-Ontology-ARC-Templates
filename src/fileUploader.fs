namespace Components

open Feliz
open Feliz.Router
open Types


module private FileReaderHelper =
  open Fable.Core
  open Fable.Core.JsInterop

  [<Emit("new FileReader()")>]
  let newFileReader(): Browser.Types.FileReader = jsNative

  let readDocx (file: Browser.Types.File) setState = 
    let reader = newFileReader()
    reader.onload <- fun e ->
      let arrayBuffer = e.target?result
      promise {
        let! r = Mammoth.mammoth.convertToHtml({|arrayBuffer = arrayBuffer|})
        setState (Docx r.value)
      }
      |> Promise.start

    reader.onerror <- fun e ->
      Browser.Dom.console.error ("Error reading file", e)
    reader.readAsArrayBuffer(file)

  // let readPdf (file: Browser.Types.File) setState =
  //   let src = URL.createObjectURL(file)
  //   log ("Uploaded PDF:", src)
  //   setState (PDF src)

  let readFromFile (file: Browser.Types.File) setState (fileType: UploadFileType) =
    match fileType with
    | UploadFileType.Docx -> readDocx file setState
    // | UploadFileType.PDF -> readPdf file setState

    

type Components =

    static member private DisplayHtml(htmlString: string) = 
      Html.div [
        prop.className "prose lg:prose-xl"
        prop.children [
          Html.div [
            // Highlighter.Highlighter.highlighter [
            //   Highlighter.Highlighter.textToHighlight (htmlString.Replace("  ","" ))
            //   // Highlighter.Highlighter.searchWords (annotationToResizeArray) //replace with array of annotated words
            //   // Highlighter.Highlighter.highlightClassName "highlight"
            //   Highlighter.Highlighter.autoEscape true
            // ]
            prop.innerHtml htmlString
          ]
        ]
      ]

    /// https://stackoverflow.com/a/60539836/12858021
    static member private DisplayPDF(pdfSource: string, modalContext: DropdownModal) =
      Html.div [
        prop.className "content"
        prop.onContextMenu (fun e ->
            let term = Browser.Dom.window.getSelection().ToString().Trim() 
            if term.Length <> 0 then 
                modalContext.setter {
                    isActive = true;
                    location = int e.pageX, int e.pageY
                }
            else 
                ()
            e.stopPropagation()
            e.preventDefault()
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

    static member private FileUpload (ref: IRefValue<Browser.Types.HTMLInputElement option>) uploadFileType setUploadFileType setFilehtml =
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
                          FileReaderHelper.readFromFile f setFilehtml uploadFileType
                          if ref.current.IsSome then
                            ref.current.Value.value <- null
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

    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member UploadDisplay(modalContext: DropdownModal) =
        let uploadFileType, setUploadFileType = React.useState(UploadFileType.Docx)
        let filehtml, setFilehtml = React.useState(Unset)
        let ref = React.useInputRef()
        Html.div [
          prop.className "section" 
          prop.children [
              Html.div [
                  prop.className "container"
                  prop.children [
                    Components.FileUpload ref uploadFileType setUploadFileType setFilehtml
                    Html.div [
                      prop.className "field"
                      prop.children [
                        match filehtml with
                        | Unset -> Html.p "No file uploaded"
                        | Docx filehtml ->
                          Components.DisplayHtml(filehtml)
                        // | PDF pdfSource ->
                        //   Components.DisplayPDF(pdfSource, modalContext)
                      ]
                    ]
                  ]
              ]
          ]
        ]