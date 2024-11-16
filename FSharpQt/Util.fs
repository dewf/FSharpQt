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


// needed for situations with generics that cause compiler grief
// (eg 'state in Reactor)
let tryDispose (value: obj) =
    match value with
    | :? IDisposable as disposable ->
        disposable.Dispose()
    | _ ->
        ()

// save some annoying dynamic casting if we're sure about it        
let dispose (value: #IDisposable) =
    value.Dispose()
