module DropWindow

open FSharpQt
open BuilderNode
open FSharpQt.Widgets.MainWindow
open FSharpQt.Widgets.Menu
open FSharpQt.Widgets.MenuAction
open FSharpQt.Widgets.MenuBar
open MiscTypes

open Painting
open Reactor
open EventDelegate

open FSharpQt.Widgets
open BoxLayout
open CustomWidget

type Signal =
    | NewWindow
    | WindowClosed

let DRAG_SOURCE_RECT =
    Rect.From(20, 20, 100, 100)

let DRAG_THRESH_PIXELS = 5

[<RequireQualifiedAccess>]
type Payload =
    | Text of string
    | Files of string list
    
type Fragment = {
    Location: Point
    Payload: Payload
}

type State = {
    MaybeDropPosition: Point option
    Fragments: Fragment list
    PotentiallyDraggingFrom: Point option
}

type Msg =
    | NewWindowMsg
    | WindowClosedMsg
    | DropPreview of loc: Point
    | PerformDrop of fragment: Fragment
    | DropCanceled
    | BeginPotentialDrag of loc: Point
    | EndDrag

let init() =
    { MaybeDropPosition = None
      Fragments =  []
      PotentiallyDraggingFrom = None
    }, Cmd.None
    
let update (state: State) (msg: Msg) =
    match msg with
    | NewWindowMsg ->
        state, Cmd.Signal NewWindow
    | WindowClosedMsg ->
        state, Cmd.Signal WindowClosed
    | DropPreview loc ->
        { state with MaybeDropPosition = Some loc }, Cmd.None
    | PerformDrop fragment ->
        let nextFragments =
            fragment :: state.Fragments
        { state with Fragments = nextFragments; MaybeDropPosition = None }, Cmd.None
    | DropCanceled ->
        { state with MaybeDropPosition = None }, Cmd.None
    | BeginPotentialDrag loc ->
        { state with PotentiallyDraggingFrom = Some loc }, Cmd.None
    | EndDrag ->
        { state with PotentiallyDraggingFrom = None }, Cmd.None
        
let rectContains (r: Rect) (p: Point) =
    p.X >= r.X && p.X < (r.X + r.Width) && p.Y >= r.Y && p.Y < (r.Y + r.Height)
    
let dist (p1: Point) (p2: Point) =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    (dx * dx + dy * dy) |> float |> sqrt
    
type EventDelegate(state: State) =
    inherit EventDelegateBase<Msg,State>(state)
    
    override this.SizeHint =
        Size.From(640, 480)
    
    override this.MousePress loc button modifiers =
        if rectContains DRAG_SOURCE_RECT loc then
            Some (BeginPotentialDrag loc)
        else
            None
        
    override this.MouseMove loc buttons modifiers =
        // we don't have tracking enabled so move events will only be received when a button is held
        match state.PotentiallyDraggingFrom with
        | Some p ->
            if dist loc p > DRAG_THRESH_PIXELS then
                printfn "beginning drag!"
                match this.BeginDrag (Text "WOOOOOOOOOOOOOOOOOOOOT") [Copy; Move] Copy with
                | Copy ->
                    printfn "data copied"
                | Move ->
                    printfn "data moved"
                | _ ->
                    printfn "(some other outcome)"
                Some EndDrag
            else
                None
        | None ->
            None
        
    override this.MouseRelease loc button modifiers =
        state.MaybeDropPosition
        |> Option.map (fun _ -> EndDrag)
        
    override this.NeedsPaint prev =
        Everything
        
    override this.Paint stack painter widget updateRect =
        let darkBlue = Color(DarkBlue)
        let orangeBrush = stack.Brush(Color(1, 0.5, 0.5, 0.25))
        let yellowPen = stack.Pen(Color(Yellow))
        let noPen = stack.Pen(NoPen)
        
        painter.FillRect(widget.Rect, darkBlue)
        painter.Pen <- yellowPen
        // painter.Font <- font
        // drag source rect
        painter.DrawRect(DRAG_SOURCE_RECT)
        painter.DrawText(DRAG_SOURCE_RECT, Alignment.Center, "Drag from Me")
        // existing fragments
        for fragment in state.Fragments do
            let rect =
                Rect.From(fragment.Location.X, fragment.Location.Y, 1000, 1000)
            match fragment.Payload with
            | Payload.Text text ->
                painter.DrawText(rect, Alignment.Left, text)
            | Payload.Files files ->
                let text =
                    files
                    |> String.concat "\n"
                painter.DrawText(rect, Alignment.Leading, text)
        // preview pos
        match state.MaybeDropPosition with
        | Some pos ->
            painter.Pen <- noPen
            painter.Brush <- orangeBrush
            painter.DrawEllipse(pos, 20, 20)
        | None ->
            ()
            
    // drop stuff -------------------------------------------------
            
    override this.DragMove loc modifiers mimeData proposedAction possibleActions isEnterEvent =
        if mimeData.HasFormat("text/plain") && possibleActions.Contains(Copy) then
            Some (Copy, DropPreview loc)
        elif mimeData.HasFormat("text/uri-list") && possibleActions.Contains(Copy) then
            Some (Copy, DropPreview loc)
        else
            None
            
    override this.DragLeave() =
        Some DropCanceled
        
    override this.Drop loc modifiers mimeData dropAction =
        let payload =
            if mimeData.HasFormat("text/plain") then
                mimeData.Text
                |> Payload.Text
            else
                mimeData.Urls
                |> Array.toList
                |> Payload.Files
        let fragment =
            { Location = loc; Payload = payload }
        Some (PerformDrop fragment)
            
let view (state: State) =
    let custom =
        CustomWidget(
            EventDelegate(state), [ PaintEvent; DropEvents; SizeHint; MousePressEvent; MouseMoveEvent ],
            AcceptDrops = true)
    let layout = 
        VBoxLayout(Items = [
            BoxItem(custom)
        ])
    let menuBar =
        let file =
            let newWindow =
                MenuAction(Text = "New Window", OnTriggered = NewWindowMsg)
            Menu(Title = "&File", Items = [
                MenuItem(newWindow)
            ])
        MenuBar(Menus = [ file ])
    MainWindow(
        WindowTitle = "Drop Window",
        MenuBar = menuBar,
        CentralLayout = layout,
        OnWindowClosed = WindowClosedMsg)
    :> IWindowNode<Msg>
    
// component definition
    
type DropWindow<'outerMsg>() =
    inherit WindowReactorNode<'outerMsg, State, Msg, Signal>(init, update, view)
    
    let mutable onNewWindow: 'outerMsg option = None
    let mutable onWindowClosed: 'outerMsg option = None
    
    member this.OnNewWindow with set value = onNewWindow <- Some value
    member this.OnWindowClosed with set value = onWindowClosed <- Some value
    
    override this.SignalMap (s: Signal) =
        match s with
        | NewWindow -> onNewWindow
        | WindowClosed -> onWindowClosed
