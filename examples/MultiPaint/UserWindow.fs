module UserWindow

open System
open FSharpQt
open FSharpQt.BuilderNode
open FSharpQt.EventDelegate
open FSharpQt.InputEnums
open FSharpQt.MiscTypes
open FSharpQt.Models.ListModelNode
open FSharpQt.Models.SimpleListModel
open FSharpQt.Models.TrackedRows
open FSharpQt.Reactor
open FSharpQt.Widgets
open BoxLayout
open CustomWidget
open FSharpQt.Widgets.ComboBox
open FSharpQt.Widgets.Label
open FSharpQt.Widgets.PushButton
open FSharpQt.Widgets.StyledItemDelegate
open LineEdit
open MainWindow
open TreeView

open Painting

type Stroke = Point list

type Signal =
    | LoggedIn of name: string * drawing: Stroke list
    | WindowClosedSignal
    | DrawingChanged of drawing: Stroke list
    
type OtherUser = {
    Id: int
    Name: string
    Drawing: Stroke list
}

type State = {
    OurName: string option
    OtherUsers: TrackedRows<OtherUser>
    // keep color preferences separately, so we can keep the other-users update/merge logic simple
    ColorMap: Map<int, int> // from userID to color index
    Drawing: Stroke list
    MouseDown: bool
}

let possibleColors =
    [| Red
       DarkRed
       Green
       DarkGreen
       Blue
       DarkBlue
       Cyan
       DarkCyan
       Magenta
       DarkMagenta
       Yellow
       DarkYellow |]
    
let randomColorIndex () =
    let r = Random()
    r.Next() % possibleColors.Length
    
let colorAtIndex i =
    possibleColors[i]

type Msg =
    | UsernameSubmitted             // for return/tab, requires cmd to get text value
    | GotUsername of name: string
    | WindowClosedMsg
    | BeginStroke of p: Point
    | ContinueStroke of p: Point
    | EndStroke
    | EraseDrawing
    | UserColorChanged of id: int * index: int
    
type OtherUsersAttr(value: OtherUser list) =
    inherit ComponentAttrBase<OtherUser list, State>(value)
    override this.Key = "userwindow:otherusers"
    override this.Update state =
        let nextUsers =
            state.OtherUsers
                .BeginChanges()
                .DiffUpdate(value, _.Id)
        let nextColorMap =
            (state.ColorMap, value)
            ||> List.fold (fun acc user ->
                if not (acc.ContainsKey user.Id) then
                    // new color for new as-yet-unseen users only
                    acc.Add(user.Id, randomColorIndex())
                else
                    acc)
        // in a serious program we would also remove unused mappings, not worth bothering with here
        { state with OtherUsers = nextUsers; ColorMap = nextColorMap }
    
let init() =
    let state = {
        OurName = None
        OtherUsers = TrackedRows.Init []
        ColorMap = Map.empty
        Drawing = List.Empty
        MouseDown = false
    }
    state, Cmd.None

let update (state: State) (msg: Msg) =
    match msg with
    | UsernameSubmitted ->
        state, cmdGetText "username-edit" GotUsername
    | GotUsername name ->
        { state with OurName = Some name }, Cmd.Signal (LoggedIn (name, state.Drawing))
    | WindowClosedMsg ->
        state, Cmd.Signal WindowClosedSignal
    | BeginStroke p ->
        let nextDrawing =
            match state.Drawing with
            | stroke :: etc ->
                (p :: stroke) :: etc
            | [] ->
                [ p ] :: []
        { state with
            MouseDown = true
            Drawing = nextDrawing }, Cmd.Signal (DrawingChanged nextDrawing)
    | ContinueStroke p ->
        let nextDrawing =
            match state.Drawing with
            | stroke :: etc ->
                (p :: stroke) :: etc
            | _ ->
                // BeginStroke ensures always one point/stroke to start with
                failwith "nope"
        { state with
            Drawing = nextDrawing }, Cmd.Signal (DrawingChanged nextDrawing)
    | EndStroke ->
        let nextDrawing =
            // make sure to terminate the recent stroke, otherwise it joins
            [] :: state.Drawing
        { state with
            MouseDown = false
            Drawing = nextDrawing }, Cmd.None
    | EraseDrawing ->
        { state with Drawing = [] }, Cmd.Signal (DrawingChanged [])
    | UserColorChanged(id, index) ->
        let nextOthers =
            state.OtherUsers
                .BeginChanges()
                .ReplaceWhere(fun user ->
                    if user.Id = id then
                        // not actually changing anything, we just want to trigger a redraw
                        // so that the color decoration will immediately update
                        Some user
                    else
                        None)
        let nextMap =
            state.ColorMap.Add(id, index)
        let nextState =
            { state with OtherUsers = nextOthers; ColorMap = nextMap }
        nextState, Cmd.None
    
type CanvasDelegate(state: State) =
    inherit EventDelegateBase<Msg, State>(state)
    
    override this.NeedsPaint prev =
        Everything
        
    override this.Paint stack painter widget updateRect =
        let bgBrush = stack.Brush(stack.Color(Gray))
        let blackPen = stack.Pen(stack.Color(Black), Width = 3)
        
        painter.SetRenderHint Antialiasing true
        painter.FillRect(widget.Rect, bgBrush)
        
        // draw other users in background
        for user in state.OtherUsers.Rows do
            let color =
                state.ColorMap[user.Id]
                |> colorAtIndex 
            painter.Pen <- stack.Pen(stack.Color(color), Width = 3)
            for stroke in user.Drawing do
                painter.DrawPolyline(stroke |> List.map PointF.From |> Array.ofList)
        
        // ours over the top
        painter.Pen <- blackPen
        for stroke in state.Drawing do
            painter.DrawPolyline(stroke |> List.map PointF.From |> Array.ofList)
        
    override this.MousePress loc button modifiers =
        match button with
        | LeftButton -> Some (BeginStroke loc)
        | _ -> None
        
    override this.MouseMove loc button modifiers =
        if state.MouseDown then
            Some (ContinueStroke loc)
        else
            None
            
    override this.MouseRelease _ _ _ =
        if state.MouseDown then
            Some EndStroke
        else
            None
            
    override this.SizeHint =
        Size.From(400, 400)
        
type Column =
    | UserName = 0
    | UserColor = 1     // editable
    | NUM_COLUMNS = 2
    
type RowDelegate(state: State) =
    inherit AbstractSimpleListModelDelegate<Msg, OtherUser>()
    override this.Data rowData col role =
        match enum<Column> col, role with
        | Column.UserName, DisplayRole ->
            Variant.String rowData.Name
        | Column.UserColor, role ->
            let colorIndex =
                state.ColorMap[rowData.Id]
            let colorConstant =
                colorAtIndex colorIndex
            match role with
            | DecorationRole ->
                // right now this sort of leaks the color, which is fine/safe (it will be remotely released when GC'ed),
                // but in the future we will figure out the best way to manage dynamically-created resource lifetimes without polluting 'state too much
                // in this particular program, it will just make the most sense to use a fixed list of Color instances (vs. constants)
                new Color(colorConstant)
                |> Variant.Color
            | DisplayRole ->
                colorConstant.ToString()
                |> Variant.String
            | EditRole ->
                Variant.Int colorIndex
            | _ ->
                // usercolor, any other role
                Variant.Empty
        | _ ->
            // any other column
            Variant.Empty
    override this.GetFlags rowData col baseFlags =
        match enum<Column> col with
        | Column.UserColor -> baseFlags.Add(ItemIsEditable)
        | _ -> baseFlags
    override this.SetData rowIndex rowData col value role =
        match enum<Column> col, role with
        | Column.UserColor, EditRole ->
            UserColorChanged(rowData.Id, value.ToInt())
            |> Some
        | _ ->
            None
            
type ColorEditorRow = {
    Index: int
    Color: ColorConstant
}

type EditorColumn =
    | Color = 0
    | NUM_COLUMNS = 1

type EditorRowDelegate(state: State) =
    inherit AbstractSimpleListModelDelegate<Msg, ColorEditorRow>()
    override this.Data rowData col role =
        match enum<EditorColumn> col, role with
        | EditorColumn.Color, DisplayRole ->
            rowData.Color.ToString()
            |> Variant.String
        | EditorColumn.Color, DecorationRole ->
            // see note in regular RowDelegate DecorationRole above
            new Color(rowData.Color)
            |> Variant.Color
        | _ ->
            Variant.Empty
        
type ColorColumnItemDelegate(state: State) =
    inherit ComboBoxItemDelegateBase<Msg>()
    override this.CreateEditor option index =
        let rows =
            [0 .. possibleColors.Length - 1]
            |> List.map (fun i -> { Index = i; Color = colorAtIndex i })
            |> TrackedRows.Init
        let model =
            ListModelNode(EditorRowDelegate(state), int EditorColumn.NUM_COLUMNS, Rows = rows)
        ComboBox(Model = model)
    override this.SetEditorData combo index =
        // edit role data (from UserColor column of OtherUsers table) is an int index into color table
        use value = index.Data(EditRole)
        combo.CurrentIndex <- value.ToInt()
    override this.SetModelData combo model index =
        // pretty simple, just grab the current combo index and set it on the model
        // again, this applies to the UserColor (index) of an OtherUser
        model.SetData(index, Variant.Int combo.CurrentIndex)
        |> ignore

let view (state: State) =
    let vbox =
        let edit =
            let text =
                match state.OurName with
                | Some name -> $"Logged in as '{name}'"
                | None -> ""
            LineEdit(
                Name = "username-edit",
                Text = text,
                Enabled = state.OurName.IsNone,
                PlaceholderText = "Username please?",
                OnReturnPressed = UsernameSubmitted)
        let label =
            Label(Text = "Other users:")
        let userList =
            let model =
                ListModelNode(RowDelegate(state), int Column.NUM_COLUMNS,
                              Headers = [ "User"; "Color" ],
                              Rows = state.OtherUsers)
            let itemDelegate =
                StyledItemDelegate(ColorColumnItemDelegate(state))
            // treeview (vs. listview) needed for column headers
            TreeView(SelectionMode = AbstractItemView.NoSelection,
                     FocusPolicy = Widget.NoFocus,
                     FixedHeight = 80,
                     TreeModel = model)
                            .SetItemDelegateForColumn(int Column.UserColor, itemDelegate)
        let canvas =
            let events = [
                PaintEvent
                SizeHint
                MousePressEvent
                MouseMoveEvent
                MouseReleaseEvent
            ]
            CustomWidget(CanvasDelegate(state), events, Name = "canvas")
        let bottomHBox =
            let erase =
                PushButton(Text = "Erase", FixedWidth = 80, OnClicked = EraseDrawing)
            HBoxLayout(Items = [
                BoxItem(stretch = 1)
                BoxItem(erase)
            ])
        VBoxLayout(Items = [
            BoxItem(edit)
            BoxItem(label)
            BoxItem(userList)
            BoxItem(canvas, stretch = 1)
            BoxItem(bottomHBox)
        ])
    let title =
        match state.OurName with
        | Some name -> $"User: %s{name}"
        | None -> "(awaiting login)"
    MainWindow(WindowTitle = title, CentralLayout = vbox, OnWindowClosed = WindowClosedMsg)
    :> IWindowNode<Msg>

type UserWindow<'outerMsg>() =
    inherit WindowReactorNode<'outerMsg, State, Msg, Signal>(init, update, view)
    let mutable onLoggedIn: (string * Stroke list -> 'outerMsg) option = None
    let mutable onWindowClosed: 'outerMsg option = None
    let mutable onDrawingChanged: (Stroke list -> 'outerMsg) option = None
    member this.OnLoggedIn with set value = onLoggedIn <- Some value
    member this.OnWindowClosed with set value = onWindowClosed <- Some value
    member this.OnDrawingChanged with set value = onDrawingChanged <- Some value
    override this.SignalMap (s: Signal) =
        match s with
        | LoggedIn (name, drawing) ->
            onLoggedIn
            |> Option.map (fun f -> f (name, drawing))
        | WindowClosedSignal ->
            onWindowClosed
        | DrawingChanged strokes ->
            onDrawingChanged
            |> Option.map (fun f -> f strokes)
    member this.OtherUsers with set value =
        this.PushAttr(OtherUsersAttr(value))
