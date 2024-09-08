module Tabs.TempConverter

open System
open FSharpQt
open BuilderNode
open FSharpQt.Widgets.MainWindow
open Reactor
open FSharpQt.Widgets
open BoxLayout
open LineEdit
open Label

type State = {
    CelsiusText: string
    FahrenText: string
} with
    static member Init = {
        CelsiusText = ""
        FahrenText = ""
    }
    
type Msg =
    | SetCelsius of text: string
    | SetFahrenheit of text: string

let init () =
    State.Init, Cmd.None
    
let tryParseFloat (str: string) =
    match System.Single.TryParse str with
    | true, value ->
        Some (float value)
    | _ ->
        None
        
let celsiusToFahren (cText: string) =
    if cText.Length > 0 then
        match tryParseFloat cText with
        | Some celsius ->
            let fahren =
                (celsius * 9.0) / 5.0 + 32.0
            sprintf "%.2f" fahren
        | None ->
            "<- invalid"
    else
        ""
        
let fahrenToCelsius (fText: string) =
    if fText.Length > 0 then
        match tryParseFloat fText with
        | Some fahren ->
            let celsius =
                (fahren - 32.0) * 5.0 / 9.0
            sprintf "%.2f" celsius
        | None ->
            "invalid ->"
    else
        ""
        
let update (state: State) (msg: Msg) =
    match msg with
    | SetCelsius text ->
        let nextState =
            { state with CelsiusText = text; FahrenText = celsiusToFahren text }
        nextState, Cmd.None
                
    | SetFahrenheit text ->
        let nextState =
            { state with FahrenText = text; CelsiusText = fahrenToCelsius text }
        nextState, Cmd.None
        
let view (state: State) =
    let celsiusText =
        LineEdit(Text = state.CelsiusText, OnTextChanged = SetCelsius)
    let celsiusLabel =
        Label(Text = "Celsius = ")
    let fahrenText =
        LineEdit(Text = state.FahrenText, OnTextChanged = SetFahrenheit)
    let fahrenLabel =
        Label(Text = "Fahrenheit")
    let hbox =
        HBoxLayout(
            Items = [
                BoxItem(celsiusText)
                BoxItem(celsiusLabel)
                BoxItem(fahrenText)
                BoxItem(fahrenLabel)
            ])
    MainWindow(WindowTitle = "Temperature Converter", CentralLayout = hbox)
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
