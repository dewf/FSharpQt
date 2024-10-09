open System

open FSharpQt.BuilderNode
open FSharpQt.Reactor

open FSharpQt.Widgets
open FSharpQt.Widgets.BoxLayout
open FSharpQt.Widgets.LineEdit
open MainWindow

open SimpleAttrComponent
open FullAttrComponent

type State = {
    SimpleClickedFiveTimes: bool
    FullClickedFiveTimes: bool
}

type Msg =
    | SimpleClickedFiveTimes
    | FullClickedFiveTimes

let init _ =
    { SimpleClickedFiveTimes = false
      FullClickedFiveTimes = false }, Cmd.None
    
let update (state: State) (msg: Msg) =
    match msg with
    | SimpleClickedFiveTimes ->
        { state with SimpleClickedFiveTimes = true }, Cmd.None
    | FullClickedFiveTimes ->
        { state with FullClickedFiveTimes = true }, Cmd.None

let view (state: State) =
    let vbox =
        let thresholdReached =
            state.SimpleClickedFiveTimes && state.FullClickedFiveTimes
        let simple =
            let attrs = [
                Label "Simple Attrs Component!"
                Enabled (not thresholdReached)
            ]
            SimpleAttrComponent(Attrs = attrs, OnClickedFiveTimes = SimpleClickedFiveTimes)
        let full =
            FullAttrComponent(
                Label = "Full Attrs Component!",
                Enabled = not thresholdReached,
                OnClickedFiveTimes = FullClickedFiveTimes)
        let status =
            let text =
                if thresholdReached then
                    "Both clicked five times, we're done here!"
                else
                    "(please click each button 5 times)"
            LineEdit(Text = text, Enabled = false)
        VBoxLayout(Items = [
            BoxItem(simple)
            BoxItem(full)
            BoxItem(status)
        ])
    MainWindow(WindowTitle = "Component Example", CentralLayout = vbox)
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
