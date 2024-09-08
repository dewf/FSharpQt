open System

open FSharpQt.BuilderNode
open FSharpQt.Reactor
open FSharpQt.Widgets
open WindowSet

open DropWindow

type State = {
    NextWindowId: int
    OpenWindows: Set<int>
}

type Msg =
    | NewWindow
    | WindowClosed of id: int

let init _ =
    let state = {
        NextWindowId = 1
        OpenWindows = [ 0 ] |> Set.ofList
    }
    state, Cmd.None
    
let update (state: State) (msg: Msg) =
    match msg with
    | NewWindow ->
        let nextState = {
            NextWindowId = state.NextWindowId + 1
            OpenWindows = state.OpenWindows.Add state.NextWindowId
        }
        nextState, Cmd.None
    | WindowClosed id ->
        let nextState =
            { state with
                OpenWindows = state.OpenWindows.Remove id }
        nextState, Cmd.None

let view (state: State) =
    let windows =
        state.OpenWindows
        |> Set.toList
        |> List.map (fun id ->
            let window =
                DropWindow(OnNewWindow = NewWindow, OnWindowClosed = WindowClosed id)
                :> IWindowNode<Msg>
            IntKey id, window)
    WindowSet(Windows = windows)
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
