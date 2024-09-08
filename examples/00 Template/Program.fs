open System

open FSharpQt.BuilderNode
open FSharpQt.Reactor
open FSharpQt.MiscTypes

open FSharpQt.Widgets
open MainWindow

type State = {
    Nothing: int
}

type Msg =
    | DoNothing

let init _ =
    { Nothing = 0 }, Cmd.None
    
let update (state: State) (msg: Msg) =
    match msg with
    | DoNothing ->
        state, Cmd.None

let view (state: State) =
    MainWindow(WindowTitle = "Template", Size = Size.From (640, 480))
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
