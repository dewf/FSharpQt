open System

open FSharpQt.BuilderNode
open FSharpQt.InputEnums
open FSharpQt.Reactor
open FSharpQt.MiscTypes

open FSharpQt.Widgets
open MainWindow
open LineEdit
open BoxLayout
open TreeView
open PushButton
open ComboBox
open StyledItemDelegate
open Label
open TabBar
open MenuAction
open Menu
open MenuBar

open FSharpQt.Models
open ListModelNode
open SimpleListModel
open TrackedRows
open CustomSortFilterProxyModel

type ActiveFilter =
    | ShowAll
    | Active
    | Completed
with
    member this.ToIndex() =
        match this with
        | ShowAll -> 0
        | Active -> 1
        | Completed -> 2
    static member FromIndex index =
        match index with
        | 0 -> ShowAll
        | 1 -> Active
        | 2 -> Completed
        | _ -> failwith "x"

type Priority =
    | Low
    | Normal
    | High
with
    override this.ToString() =
        match this with
        | Low -> "Low"
        | Normal -> "Normal"
        | High -> "High"
    member this.ToInt() =
        match this with
        | Low -> 0
        | Normal -> 1
        | High -> 2
    static member FromInt (i: int) =
        match i with
        | 0 -> Low
        | 1 -> Normal
        | 2 -> High
        | _ -> failwith "x"

type TodoItem = {
    Text: string
    Priority: Priority
    Done: bool
}

type State = {
    Items: TrackedRows<TodoItem>
    LineText: string
    AddPriority: Priority
    ActiveFilter: ActiveFilter
    ColumnSortingEnabled: bool
}

type Msg =
    | Quit
    | TextEdited of text: string
    | SetAddPriority of maybeIndex: int option
    | AddItem
    | RowEdited of index: int * row: TodoItem
    | ClearCompleted
    | FilterChanged of value: ActiveFilter
    | ToggleSorting of checked_: bool

let init _ =
    let state = {
        Items = TrackedRows.Init [
            { Text = "Take out the trash"; Priority = High; Done = false }
            { Text = "Walk the dog"; Priority = Normal; Done = false }
            { Text = "Return VHS tapes to Blockbuster"; Priority = Low; Done = false }
        ]
        LineText = ""
        AddPriority = Normal
        ActiveFilter = ShowAll
        ColumnSortingEnabled = false
    }
    state, cmdResizeAllColumnsToContents "tree" 3
    
let update (state: State) (msg: Msg): State * Cmd<Msg,AppSignal> =
    match msg with
    | Quit ->
        state, Cmd.Signal QuitApplication
    | TextEdited text ->
        { state with LineText = text }, Cmd.None
    | SetAddPriority maybeIndex ->
        let nextState =
            match maybeIndex with
            | Some index ->
                { state with AddPriority = Priority.FromInt index }
            | None ->
                failwith "wat"
        nextState, Cmd.None
    | AddItem ->
        if state.LineText <> "" then
            let nextItems =
                state.Items
                    .BeginChanges()
                    .AddRow { Text = state.LineText; Priority = state.AddPriority; Done = false }
            let nextState =
                { state with
                    Items = nextItems
                    LineText = ""
                    AddPriority = Normal }
            nextState, Cmd.None
        else
            state, Cmd.None
    | RowEdited (index, row) ->
        let nextItems =
            state.Items
                .BeginChanges()
                .ReplaceAtIndex(index, row)
        { state with Items = nextItems }, Cmd.None
    | ClearCompleted ->
        let nextItems =
            state.Items
                .BeginChanges()
                .DeleteWhere(_.Done)
        { state with Items = nextItems }, Cmd.None
    | FilterChanged value ->
        // need to invalidate the filter for the FilterDelegate to be re-evaluated again
        { state with ActiveFilter = value }, cmdInvalidateRowsFilter "proxymodel"
    | ToggleSorting newValue ->
        let cmd =
            match newValue with
            | true -> Cmd.None
            | false ->
                // when deactivating, we need to reset to "no column", otherwise it will still keep sorting despite the header buttons not working anymore
                cmdSort "proxymodel" -1
        { state with ColumnSortingEnabled = newValue }, cmd
        
type Column =
    | Done = 0
    | Task = 1
    | Priority = 2
    | NUM_COLUMNS = 3

let lowColor =
    Color("#b30000").Realize()
let normalColor =
    Color("#ff8000").Realize()
let highColor =
    Color("#ffff00").Realize()
    
type RowDelegate(state: State) =
    inherit AbstractSimpleListModelDelegate<Msg,TodoItem>()
    override this.Data rowData col role =
        match enum<Column> col, role with
        | Column.Done, SizeHintRole -> Variant.Size (Size.From (40, 24)) // give the checkbox a bit more space, and also make the rows taller (uniform heights are enabled)
        | Column.Done, CheckStateRole -> Variant.CheckState (if rowData.Done then Checked else Unchecked)
        | Column.Task, DisplayRole -> Variant.String rowData.Text
        | Column.Priority, DecorationRole ->
            match rowData.Priority with
            | Low -> lowColor :> Color
            | Normal -> normalColor
            | High -> highColor
            |> Variant.Color
        | Column.Priority, DisplayRole -> rowData.Priority.ToString() |> Variant.String
        | Column.Priority, EditRole -> rowData.Priority.ToInt() |> Variant.Int
        | _ -> Variant.Empty
    override this.GetFlags rowData col baseFlags =
        match enum<Column> col with
        | Column.Done -> baseFlags.Add(ItemIsUserCheckable)
        | Column.Priority -> baseFlags.Add(ItemIsEditable)
        | _ -> baseFlags
    override this.SetData rowIndex rowData col value role =
        match enum<Column> col, role with
        |  Column.Done, CheckStateRole ->
            let nextRow =
                let boolValue =
                    match value.ToCheckState() with
                    | Checked -> true
                    | _ -> false
                { rowData with Done = boolValue }
            RowEdited (rowIndex, nextRow)
            |> Some
        | Column.Priority, EditRole ->
            let nextRow =
                let priority =
                    value.ToInt() |> Priority.FromInt
                { rowData with Priority = priority }
            RowEdited (rowIndex, nextRow)
            |> Some
        | _ ->
            None
            
let comboStrings =
    [ Low; Normal; High ]
    |> List.map (_.ToString())
            
type PriorityColumnDelegate(state: State) =
    inherit ComboBoxItemDelegateBase<Msg>()
    override this.CreateEditor option index =
        ComboBox(StringItems = comboStrings)
    override this.SetEditorData combo index =
        use value = index.Data(EditRole) // default is display role! no no no!
        combo.CurrentIndex <- value.ToInt()
    override this.SetModelData combo model index =
        model.SetData(index, Variant.Int combo.CurrentIndex)
        |> ignore
        
type FilterDelegate(state: State) =
    inherit FilterDelegateBase()
    override this.FilterAcceptsRow sourceRow sourceParent =
        use index = this.SourceModel.Index(sourceRow, int Column.Done)
        use value = index.Data(CheckStateRole)
        match state.ActiveFilter, value.ToCheckState() with
        | ShowAll, _ ->
            true
        | Active, Unchecked ->
            true
        | Completed, Checked ->
            true
        | _ ->
            false
    override this.LessThan left right =
        match enum<Column> left.Column with
        | Column.Done ->
            left.Data(CheckStateRole).ToInt() < right.Data(CheckStateRole).ToInt()
        | Column.Task ->
            left.Data().ToStringValue().ToLower() < right.Data().ToStringValue().ToLower()
        | Column.Priority ->
            left.Data(EditRole).ToInt() < right.Data(EditRole).ToInt()
        | _ ->
            failwith "FilterDelegate.LessThan wat"
    
let view (state: State) =
    let model =
        ListModelNode(RowDelegate(state), int Column.NUM_COLUMNS,
                      Headers = [ "Done"; "Task"; "Priority" ],
                      Rows = state.Items)
    let sortFilterProxy =
        CustomSortFilterProxyModel(
            FilterDelegate(state), [FilterAcceptsRow; LessThan],
            Name = "proxymodel",
            SourceModel = model)
    let topSection =
        let tabs =
            TabBar(Items = [
                TabItem("All")
                TabItem("Active")
                TabItem("Completed")
            ],
            Expanding = false,
            OnCurrentChanged = (ActiveFilter.FromIndex >> FilterChanged),
            CurrentIndex = state.ActiveFilter.ToIndex())
        let clear =
            let enabled =
                state.Items.Rows
                |> List.exists (_.Done)
            PushButton(Text = "Clear Completed", Enabled = enabled, OnClicked = ClearCompleted)
        HBoxLayout(Items = [
            BoxItem(tabs, stretch = 1)
            BoxItem(clear)
        ])
    let list =
        let priorityDelegate =
            StyledItemDelegate(PriorityColumnDelegate(state))
        TreeView(
            Name = "tree",
            UniformRowHeights = true,
            SelectionMode = AbstractItemView.NoSelection,
            FocusPolicy = Widget.NoFocus,
            SortingEnabled = state.ColumnSortingEnabled,
            TreeModel = sortFilterProxy)
                .SetItemDelegateForColumn(int Column.Priority, priorityDelegate)
    let inputSection =
        let label =
            Label(Text = "New Task: ")
        let lineEdit =
            LineEdit(Text = state.LineText,
                     PlaceholderText = "what needs doing today?",
                     OnTextEdited = TextEdited,
                     OnReturnPressed = AddItem)
        let priority =
            let selected =
                state.AddPriority.ToInt()
                |> Some
            ComboBox(StringItems = comboStrings, CurrentIndex = selected, OnCurrentIndexChanged = SetAddPriority)
        let addButton =
            PushButton(
                Text = "Add",
                Enabled = (state.LineText <> ""),
                OnClicked = AddItem)
        HBoxLayout(Items = [
            BoxItem(label)
            BoxItem(lineEdit, stretch = 1)
            BoxItem(priority)
            BoxItem(addButton)
        ])
    let vbox =
        VBoxLayout(Items = [
            BoxItem(topSection)
            BoxItem(list)
            BoxItem(inputSection)
        ])
    let viewMenu =
        let enableItemSorting =
            MenuAction(Text = "Enable column sorting", Checkable = true, Checked = state.ColumnSortingEnabled, OnToggled = ToggleSorting)
        Menu(Title = "&View", Items = [
            MenuItem(enableItemSorting)
        ])
    let fileMenu =
        let quit =
            MenuAction(
                Text = "&Quit",
                Shortcut = KeySequence(Key.Q, [ Modifier.Control ]),
                OnTriggered = Quit)
        Menu(Title = "&File", Items = [
            MenuItem(quit)
        ])
    let menuBar =
        MenuBar(Menus = [
            fileMenu
            viewMenu
        ])
    MainWindow(WindowTitle = "TodoMVC", Size = Size.From (500, 330), CentralLayout = vbox, MenuBar = menuBar)
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
