open System

open FSharpQt
open FSharpQt.Widgets.MainWindow
open MiscTypes
open Painting
open EventDelegate

open BuilderNode
open Reactor

open FSharpQt.Widgets
open CustomWidget
open BoxLayout
open Dialog
open MenuAction
open Menu
open PushButton
open Slider

open Extensions

open FSharpQt.InputEnums

let dist (p1: Point) (p2: Point) =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    (dx * dx + dy * dy) |> float |> sqrt
    
let dist2 (p1: PointF) (p2: PointF) =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    (dx * dx + dy * dy) |> float |> sqrt

type Signal = unit

type Circle = {
    Location: Point
    Radius: int
}

type UndoItem =
    | CircleAdded of circle: Circle
    | RadiusChanged of index: int * oldRadius: int * newRadius: int
    
type State = {
    Circles: Circle list
    MaybeHoverIndex: int option
    NowEditing: bool
    EditingRadius: int
    UndoStack: UndoItem list
    RedoStack: UndoItem list
}

let circleAtIndex (index: int) (state: State) =
    state.Circles
    |> List.item index
        
type Msg =
    | AddCircle of loc: Point
    | ShowContext of loc: Point
    | ShowDialog
    | MouseMove of loc: Point
    | SetRadius of radius: int
    | ApplyEdit
    | CancelEdit
    | DialogClosed of accepted: bool
    | Undo
    | Redo
    
let init() =
    let state =
        { Circles = []
          MaybeHoverIndex = None
          NowEditing = false 
          EditingRadius = 0
          UndoStack = []
          RedoStack = [] }
    state, Cmd.None
    
let update (state: State) = function
    | AddCircle loc ->
        let circle =
            { Location = loc; Radius = 35 }
        let nextUndoStack =
            CircleAdded circle :: state.UndoStack
        let nextState =
            { state with
                Circles = circle :: state.Circles
                UndoStack = nextUndoStack
                RedoStack = []
                MaybeHoverIndex = Some 0 }
        nextState, Cmd.None
    | ShowContext loc ->
        match state.MaybeHoverIndex with
        | Some _ ->
            state, showMenuAtPoint "context" loc "canvas"
        | None ->
            state, Cmd.None
    | ShowDialog ->
        match state.MaybeHoverIndex with
        | Some index ->
            let circle =
                state |> circleAtIndex index
            let nextState =
                { state with NowEditing = true; EditingRadius = circle.Radius }
            let cmd =
                execDialogWithResultAtPoint "edit" DialogClosed circle.Location "canvas"
            nextState, cmd
        | None ->
            state, Cmd.None
    | SetRadius value ->
        { state with EditingRadius = value }, Cmd.None
    | MouseMove loc ->
        let nextHoverIndex =
            state.Circles
            |> List.tryFindIndex (fun circle -> dist circle.Location loc < circle.Radius)
        { state with MaybeHoverIndex = nextHoverIndex }, Cmd.None
    | ApplyEdit ->
        state, acceptDialog "edit"
    | CancelEdit ->
        state, rejectDialog "edit"
    | DialogClosed accepted ->
        // this also catches the case where the dialog is closed with the [X] and not via the cancel button
        // hence not changing any state in the CancelEdit handler
        // the other option would be to paramterize the CancelEdit msg to indicate its origin (cancel button vs. dialog [X]),
        // to avoid invoking a Cmd.Dialog:Reject which would create a feedback loop
        let nextState =
            match accepted, state.MaybeHoverIndex with
            | true, Some index ->
                // apply change ================
                let nextCircles, nextUndoStack =
                    let mutable oldRadius: int = -1 // 😮
                    let nextCircles =
                        state.Circles
                        |> List.replaceAtIndex index (fun cir ->
                            oldRadius <- cir.Radius
                            { cir with Radius = state.EditingRadius })
                    let nextUndoStack =
                        RadiusChanged (index, oldRadius, state.EditingRadius) :: state.UndoStack
                    nextCircles, nextUndoStack
                { state with
                    Circles = nextCircles
                    UndoStack = nextUndoStack
                    RedoStack = []
                    NowEditing = false }
            | _ ->
                // ignore/revert ================
                { state with NowEditing = false }
        nextState, Cmd.None
    | Undo ->
        let nextState =
            match state.UndoStack with
            | item :: etc ->
                match item with
                | CircleAdded _ ->
                    let nextCircles =
                        state.Circles |> List.skip 1
                    let nextRedoStack =
                        item :: state.RedoStack
                    { state with Circles = nextCircles; UndoStack = etc; RedoStack = nextRedoStack }
                | RadiusChanged (index, oldRadius, _) ->
                    let nextCircles =
                        state.Circles
                        |> List.replaceAtIndex index (fun cir -> { cir with Radius = oldRadius })
                    let nextRedoStack =
                        item :: state.RedoStack
                    { state with Circles = nextCircles; UndoStack = etc; RedoStack = nextRedoStack }
            | [] ->
                // nothing to undo 
                state
        nextState, Cmd.None
    | Redo ->
        let nextState =
            match state.RedoStack with
            | item :: etc ->
                match item with
                | CircleAdded circle ->
                    let nextCircles =
                        circle :: state.Circles
                    let nextUndoStack =
                        item :: state.UndoStack
                    { state with Circles = nextCircles; UndoStack = nextUndoStack; RedoStack = etc }
                | RadiusChanged (index, _, newRadius) ->
                    let nextCircles =
                        state.Circles
                        |> List.replaceAtIndex index (fun cir -> { cir with Radius = newRadius })
                    let nextUndoStack =
                        item :: state.UndoStack
                    { state with Circles = nextCircles; UndoStack = nextUndoStack; RedoStack = etc }
            | [] ->
                // nothing to redo
                state
        nextState, Cmd.None

type EventDelegate(state: State) =
    inherit EventDelegateBase<Msg, State>(state)
    
    override this.NeedsPaint prev =
        // we could compare states here, to determine smaller (or no) update regions, if we wanted
        Everything
        
    override this.Paint stack painter widget updateRect =
        let bgBrush = stack.Brush(Color(DarkBlue))
        let hoverBrush = stack.Brush(Color(Magenta))
        let pen = stack.Pen(Color(Yellow), Width = 2)
        
        painter.SetRenderHint Antialiasing true
        painter.FillRect(widget.Rect, bgBrush)
        
        painter.Pen <- pen
        for i, circle in state.Circles |> List.zipWithIndex |> List.rev do
            let brush, radius =
                match state.MaybeHoverIndex with
                | Some index when i = index ->
                    let radius =
                        if state.NowEditing then
                            state.EditingRadius
                        else
                            circle.Radius
                    hoverBrush, radius
                | _ ->
                    bgBrush, circle.Radius
            painter.Brush <- brush
            painter.DrawEllipse(circle.Location, radius, radius)
        
    override this.MousePress loc button modifiers =
        match button with
        | LeftButton -> Some (AddCircle loc)
        | RightButton -> Some (ShowContext loc)
        | _ -> None
    override this.MouseMove loc buttons modifiers =
        Some (MouseMove loc)
    override this.SizeHint =
        Size.From(400, 300)

let view (state: State) =
    let undoRedoButtons =
        let undo =
            let enabled =
                not state.UndoStack.IsEmpty
            PushButton(Text = "Undo", Enabled = enabled, OnClicked = Undo)
        let redo =
            let enabled =
                not state.RedoStack.IsEmpty
            PushButton(Text = "Redo", Enabled = enabled, OnClicked = Redo)
        HBoxLayout(
            Items = [
                BoxItem(stretch = 1)
                BoxItem(undo)
                BoxItem(redo)
                BoxItem(stretch = 1)
            ])
        
    let dialog =
        let slider =
            Slider(
                Orientation = Horizontal,
                Range = (5, 100),
                Value = state.EditingRadius,
                OnValueChanged = SetRadius)
        let cancel =
            PushButton(Text = "Cancel", OnClicked = CancelEdit)
        let apply =
            PushButton(Text = "OK", OnClicked = ApplyEdit)
        let vbox =
            let hbox =
                HBoxLayout(
                    Items = [
                        BoxItem(stretch = 1)
                        BoxItem(cancel)
                        BoxItem(apply)
                        BoxItem(stretch = 1)
                    ])
            VBoxLayout(Items = [
                BoxItem(slider)
                BoxItem(spacer = 10)
                BoxItem(hbox)
            ])
        Dialog(
            Name = "edit",
            WindowTitle = "Edit Radius",
            Layout = vbox) // if using the Cmd.Dialog ExecWithResult, don't use the OnClosed signal here - I presume it would be sent twice ... probably need to settle on a single manner of handling dialog close events
        
    let canvas =
        let contextMenu =
            let action =
                MenuAction(Text = "Edit Radius", OnTriggered = ShowDialog)
            Menu(Name = "context", Items = [
                MenuItem(action)
            ])
        CustomWidget(
            // first 2 args required
            EventDelegate(state), [ PaintEvent; MousePressEvent; MouseMoveEvent; SizeHint ],
            Name = "canvas",
            MouseTracking = true, // tracking needed for move events without mouse down
            Attachments = [
                Attachment("context", contextMenu)
                Attachment("edit", dialog)
            ])
        
    let vbox =
        VBoxLayout(Items = [
            BoxItem(undoRedoButtons)
            BoxItem(canvas)
        ])
        
    MainWindow(WindowTitle = "CircleDrawer", CentralLayout = vbox)
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
