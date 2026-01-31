// These functions are supposed to be used from inside tcs templates

/**
 * replaces stuff according to replaceMap in template and saves it at savePath
 * 
 * @param {string} text - template text
 * @param {object} replaceMap - all key of replaceMap in text are replaced with coresponding values of replaceMap
 * @param {string} savePath - full path to save locaion
 */
export function TransformAndSave(text, replaceMap, savePath) {
  let result = text

  for (const key in replaceMap) {
    result = result.replaceAll(key, replaceMap[key])
  }

  lib.System.IO.File.WriteAllText(savePath, result)
}

/**
 * generates multiple files from template in a loop
 * @param {*} text - template text
 * @param {*} range - itteration range array [start, end]
 * @param {*} replaceFunc - (int i) => replaceMap
 * @param {*} savePathFunc - (int i) => savePath
 */
export function TransformTemplate(text, range, replaceFunc, savePathFunc) {
  for (let i = range[0]; i < range[1]; i++) {
    TransformAndSave(text, replaceFunc(i), savePathFunc(i))
  }
}