module FSharpQt.Util

open System
open System.Globalization

let tryParseHexStringUInt32 (text: string) (maybePrefix: string option) =
    let parseRaw (str: string) =
        match UInt32.TryParse(str, NumberStyles.HexNumber, CultureInfo.CurrentCulture) with
        | true, value -> Some value
        | false, _ -> None
    match maybePrefix with
    | Some prefix ->
        if text.StartsWith(prefix) then
            parseRaw (text.Substring(prefix.Length))
        else
            None
    | None ->
        parseRaw text
