open System

open FSharpQt.BuilderNode
open FSharpQt.Models.ListModelNode
open FSharpQt.Models.SimpleListModel
open FSharpQt.Models.TrackedRows
open FSharpQt.Reactor
open FSharpQt.MiscTypes

open FSharpQt.Widgets
open BoxLayout
open FSharpQt.Widgets.Label
open ListView
open PushButton
open WindowSet
open MainWindow

open UserWindow

type NamedUserInfo = {
    Name: string
    Drawing: Stroke list
}

type PaintUser = {
    Id: int
    Info: NamedUserInfo option  // None = not yet logged in
}

type State = {
    NextId: int
    Users: TrackedRows<PaintUser>
}

type Msg =
    | MainWindowClosed
    | NewUser
    | UserLoggedIn of id: int * name: string * drawing: Stroke list
    | UserLoggedOut of id: int
    | UserDrawingChanged of id: int * drawing: Stroke list

let init _ =
    let state = {
        NextId = 1
        Users = TrackedRows.Init []
    }
    state, Cmd.None
    
let update (state: State) (msg: Msg) =
    match msg with
    | MainWindowClosed ->
        state, Cmd.Signal QuitApplication
    | NewUser ->
        let nextUsers =
            let newUser =
                { Id = state.NextId; Info = None }
            state.Users
                .BeginChanges()
                .AddRow(newUser)
        let nextState = {
            Users = nextUsers
            NextId = state.NextId + 1
        }
        nextState, Cmd.None
    | UserLoggedIn(id, name, drawing) ->
        let nextUsers =
            state.Users
                .BeginChanges()
                .ReplaceWhere(fun user ->
                    if user.Id = id then
                        { user with Info = Some { Name = name; Drawing = drawing } }
                        |> Some
                    else
                        None)
        { state with Users = nextUsers }, Cmd.None
    | UserLoggedOut id ->
        let nextUsers =
            state.Users
                .BeginChanges()
                .DeleteWhere(fun user -> user.Id = id)
        { state with Users = nextUsers }, Cmd.None
    | UserDrawingChanged (id, drawing) ->
        let nextUsers =
            state.Users
                .BeginChanges()
                .ReplaceWhere(fun user ->
                    if user.Id = id then
                        match user.Info with
                        | Some info ->
                            { user with Info = Some { info with Drawing = drawing } }
                            |> Some
                        | None ->
                            // not logged in, ignore
                            None
                    else
                        None)
        { state with Users = nextUsers }, Cmd.None
        
type Column =
    | User = 0
    | NUM_COLUMNS = 1
        
type RowDelegate(state: State) =
    inherit AbstractSimpleListModelDelegate<Msg, PaintUser>()
    override this.Data rowData col role =
        match enum<Column> col, role with
        | Column.User, DisplayRole ->
            let text =
                match rowData.Info with
                | Some info -> $"{info.Name}"
                | None -> "(awaiting login)"
            Variant.String text
        | _ -> Variant.Empty

let view (state: State) =
    let vbox =
        let newUser =
            PushButton(Text = "New User", OnClicked = NewUser)
        let label =
            Label(Text = "Users:")
        let userList =
            let model =
                ListModelNode(RowDelegate(state), Rows = state.Users)
            ListView(SelectionMode = AbstractItemView.NoSelection,
                     FocusPolicy = Widget.NoFocus,
                     // FixedHeight = 120,
                     ListModel = model)
        VBoxLayout(Items = [
            BoxItem(newUser)
            BoxItem(label)
            BoxItem(userList)
        ])
    let mainWindow = 
        MainWindow(
            WindowTitle = "Multi-User Paint Demo",
            // FixedWidth = 320,
            Size = Size.From (320, 300),
            CentralLayout = vbox,
            OnWindowClosed = MainWindowClosed)
        :> IWindowNode<Msg>
    let allOtherUsers =
        state.Users.Rows
        |> List.choose (fun row ->
            match row.Info with
            | Some info ->
                Some { Id = row.Id; Name = info.Name; Drawing = info.Drawing }
            | None ->
                None)
    let userWindows =
        state.Users.Rows
        |> List.map (fun user ->
            // filter the user themselves from the other user list
            let otherUsers =
                allOtherUsers
                |> List.filter (fun other -> other.Id <> user.Id)
            let window =
                UserWindow(
                    OtherUsers = otherUsers,
                    OnLoggedIn = (fun (name, drawing) -> UserLoggedIn (user.Id, name, drawing)),
                    OnWindowClosed = UserLoggedOut user.Id,
                    OnDrawingChanged = (fun drawing -> UserDrawingChanged (user.Id, drawing))
                ) :> IWindowNode<Msg>
            IntKey(user.Id), window)
    let allWindows =
        (StrKey("main"), mainWindow) :: userWindows
    WindowSet(Windows = allWindows) :> IBuilderNode<Msg>
    
[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
