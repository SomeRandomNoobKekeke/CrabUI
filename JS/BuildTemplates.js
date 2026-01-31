import * as Template from "TemplateFuncs.js"


let IO = lib.System.IO;
let File = IO.File
let Directory = IO.Directory
let Path = IO.Path

let modPath = JS.ModPackage.Dir

JS.Global.BuildTemplates = function () {
  let files = Directory.GetFiles(modPath, "*.tcs", IO.SearchOption.AllDirectories)

  for (let file of files) {
    Process(file)
  }
}



function Process(file) {
  let text = File.ReadAllText(file)
  let clearText = text.replace(/\/\*JS(.*)\*\//gsm, '')

  let context = {
    thisFile: {
      text: clearText,
      path: file,
      dir: Path.GetDirectoryName(file),
    },
    IO, File, Directory, Path, Template
  }

  for (let jscode of text.matchAll(/\/\*JS(.*)\*\//gsm)) {
    runWith(jscode[1], context)
  }
}

