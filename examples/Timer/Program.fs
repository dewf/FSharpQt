open System
open FSharpQt

open BuilderNode
open FSharpQt.MiscTypes
open FSharpQt.Widgets.MainWindow
open Reactor
open FSharpQt.Widgets
open BoxLayout
open Label
open PushButton
open Slider
open ProgressBar
open Timer

type Signal = unit
let TIMER_INTERVAL = 1000 / 20

type State = {
    Duration: int
    Accumulated: int
}

type Msg =
    | Reset
    | SetDuration of value: int
    | TimerTick of elapsed: double

let init() =
    { Duration = 1000
      Accumulated = 0 }, Cmd.None

let update (state: State) (msg: Msg) =
    match msg with
    | Reset ->
        { state with Accumulated = 0 }, Cmd.None
    | SetDuration value ->
        let nextState =
            { state with Duration = value; Accumulated = min state.Accumulated value }
        nextState, Cmd.None
    | TimerTick elapsed ->
        let nextState =
            { state with Accumulated = min (state.Accumulated + int elapsed) state.Duration }
        nextState, Cmd.None
       
let view (state: State) =
    let progress =
        let value =
            (state.Accumulated * 1000) / state.Duration
        let text =
            sprintf "%.02fs" (float state.Accumulated / 1000.0)
        ProgressBar(
            Range = (0, 1000), // using 1000 divisions regardless ... using duration as upper limit seemed to cause flickering
            Value = value,
            InnerText = text)
        
    let hbox =
        let label =
            let text =
                sprintf "%.02fs" (float state.Duration / 1000.0)
            Label(Text = text)
        let slider =
            Slider(
                Orientation = Horizontal,
                Range = (100, 10_000),
                TickPosition = TicksBelow,
                TickInterval = 1000,
                Value = state.Duration,
                MinimumWidth = 250,
                OnValueChanged = SetDuration)
        HBoxLayout(Items = [
                      BoxItem(label)
                      BoxItem(slider)
                  ])
    let button =
        PushButton(Text = "Reset", OnClicked = Reset)
        
    let timer =
        Timer(Interval = TIMER_INTERVAL, Running = true, OnTimeoutWithElapsed = TimerTick)
        
    let hbox =
        VBoxLayout(Items = [
                      BoxItem(progress)
                      BoxItem(hbox)
                      BoxItem(button)
                  ], Attachments = [ Attachment("timer", timer) ])
        
    MainWindow(CentralLayout = hbox)
    :> IBuilderNode<Msg>
        
[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
