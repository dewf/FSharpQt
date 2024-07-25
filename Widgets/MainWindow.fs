module FSharpQt.Widgets.MainWindow

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | IconSizeChanged of size: Size
    | TabifiedDockWidgetActivated of dockWidget: DockWidgetProxy
    | ToolButtonStyleChanged of style: ToolButtonStyle
    | WindowClosed
    
type DockOptions =
    | AnimatedDocks
    | AllowNestedDocks
    | AllowTabbedDocks
    | ForceTabbedDocks
    | VerticalTabs
    | GroupedDragging
with
    static member QtSetFrom (opts: DockOptions seq) =
        (enum<MainWindow.DockOptions> 0, opts)
        ||> Seq.fold (fun acc opt ->
            let flag =
                match opt with
                | AnimatedDocks -> MainWindow.DockOptions.AnimatedDocks
                | AllowNestedDocks -> MainWindow.DockOptions.AllowNestedDocks
                | AllowTabbedDocks -> MainWindow.DockOptions.AllowTabbedDocks
                | ForceTabbedDocks -> MainWindow.DockOptions.ForceTabbedDocks
                | VerticalTabs -> MainWindow.DockOptions.VerticalTabs
                | GroupedDragging -> MainWindow.DockOptions.GroupedDragging
            acc ||| flag)
    
type internal Attr =
    | Animated of state: bool
    | DockNestingEnabled of state: bool
    | DockOptions of options: DockOptions list  // can't be seq I don't think, need to compare them without consuming
    | DocumentMode of state: bool
    | IconSize of size: Size
    | TabShape of shape: FSharpQt.Widgets.TabWidget.TabShape
    | ToolButtonStyle of style: ToolButtonStyle
    | UnifiedTitleAndToolBarOnMac of state: bool
with
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? Attr as attrOther ->
                this = attrOther
            | _ ->
                false
        override this.Key =
            match this with
            | Animated _ -> "mainwindow:animated"
            | DockNestingEnabled _ -> "mainwindow:docknestingenabled"
            | DockOptions _ -> "mainwindow:dockoptions"
            | DocumentMode _ -> "mainwindow:documentmode"
            | IconSize _ -> "mainwindow:iconsize"
            | TabShape _ -> "mainwindow:tabshape"
            | ToolButtonStyle _ -> "mainwindow:toolbuttonstyle"
            | UnifiedTitleAndToolBarOnMac _ -> "mainwindow:unifiedtitleandtoolbaronmac"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyMainWindowAttr this
            | _ ->
                printfn "warning: Widget.Attr couldn't ApplyTo() unknown target type [%A]" target
            
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyMainWindowAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onIconSizeChanged: (Size -> 'msg) option = None
    let mutable onTabifiedDockWidgetActivated: (DockWidgetProxy -> 'msg) option = None
    let mutable onToolButtonStyleChanged: (ToolButtonStyle -> 'msg) option = None
    let mutable onWindowClosed: 'msg option = None
    
    member internal this.SignalMask = enum<MainWindow.SignalMask> (int this._signalMask)
    
    member this.OnIconSizeChanged with set value =
        onIconSizeChanged <- Some value
        this.AddSignal(int MainWindow.SignalMask.IconSizeChanged)
        
    member this.OnTabifiedDockWidgetActivated with set value =
        onTabifiedDockWidgetActivated <- Some value
        this.AddSignal(int MainWindow.SignalMask.TabifiedDockWidgetActivated)
        
    member this.OnToolButtonStylechanged with set value =
        onToolButtonStyleChanged <- Some value
        this.AddSignal(int MainWindow.SignalMask.ToolButtonStyleChanged)
        
    member this.OnWindowClosed with set value =
        onWindowClosed <- Some value
        this.AddSignal(int MainWindow.SignalMask.WindowClosed)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | IconSizeChanged size ->
                onIconSizeChanged
                |> Option.map (fun f -> f size)
            | TabifiedDockWidgetActivated widget ->
                onTabifiedDockWidgetActivated
                |> Option.map (fun f -> f widget)
            | ToolButtonStyleChanged style ->
                onToolButtonStyleChanged
                |> Option.map (fun f -> f style)
            | WindowClosed ->
                onWindowClosed
        // prepend to parent signalmap
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.Animated with set value =
        this.PushAttr(Animated value)
        
    member this.DockNestingEnabled with set value =
        this.PushAttr(DockNestingEnabled value)
        
    member this.DockOptions with set value =
        this.PushAttr(value |> Seq.toList |> DockOptions)
        
    member this.DocumentMode with set value =
        this.PushAttr(DocumentMode value)
        
    member this.IconSize with set value =
        this.PushAttr(IconSize value)
        
    member this.TabShape with set value =
        this.PushAttr(TabShape value)
    
    member this.ToolButtonStyle with set value =
        this.PushAttr(ToolButtonStyle value)
        
    member this.UnifiedTitleAndToolBarOnMac with set value =
        this.PushAttr(UnifiedTitleAndToolBarOnMac value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable mainWindow: MainWindow.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<MainWindow.SignalMask> 0
    // binding guards:
    let mutable lastIconSize = Size.Invalid
    let mutable lastToolButtonStyle = ToolButtonStyle.ToolButtonIconOnly
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch

    member this.MainWindow
        with get() = mainWindow
        and set value =
            // assign to base
            this.Widget <- value
            mainWindow <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "Widget.ModelCore.SignalMaps: wrong func type"
            // assign the remainder up the hierarchy
            base.SignalMaps <- etc
        | _ ->
            failwith "Widget.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            mainWindow.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyMainWindowAttr attr =
            match attr with
            | Animated state ->
                mainWindow.SetAnimated(state)
            | DockNestingEnabled state ->
                mainWindow.SetDockNestingEnabled(state)
            | DockOptions options ->
                mainWindow.SetDockOptions(DockOptions.QtSetFrom options)
            | DocumentMode state ->
                mainWindow.SetDocumentMode(state)
            | IconSize size ->
                if size <> lastIconSize then
                    lastIconSize <- size
                    mainWindow.SetIconSize(size.QtValue)
            | TabShape shape ->
                mainWindow.SetTabShape(shape.QtValue)
            | ToolButtonStyle style ->
                if style <> lastToolButtonStyle then
                    lastToolButtonStyle <- style
                    mainWindow.SetToolButtonStyle(style.QtValue)
            | UnifiedTitleAndToolBarOnMac state ->
                mainWindow.SetUnifiedTitleAndToolBarOnMac(state)
                
    interface MainWindow.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // widget =========================
        member this.CustomContextMenuRequested pos =
            (this :> Widget.SignalHandler).CustomContextMenuRequested pos
        member this.WindowIconChanged icon =
            (this :> Widget.SignalHandler).WindowIconChanged icon
        member this.WindowTitleChanged title =
            (this :> Widget.SignalHandler).WindowTitleChanged title
        // mainwindow =====================
        member this.IconSizeChanged qtSize =
            let size = Size.From qtSize
            lastIconSize <- size
            signalDispatch (IconSizeChanged size)
        member this.TabifiedDockWidgetActivated dockWidget =
            signalDispatch (DockWidgetProxy(dockWidget) |> TabifiedDockWidgetActivated)
        member this.ToolButtonStyleChanged qtStyle =
            let style = ToolButtonStyle.From qtStyle
            lastToolButtonStyle <- style
            signalDispatch (ToolButtonStyleChanged style)
        member this.WindowClosed() =
            signalDispatch WindowClosed
            
    interface IDisposable with
        member this.Dispose() =
            mainWindow.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let mainWindow = MainWindow.Create(this)
    do
        this.MainWindow <- mainWindow
        
    let mutable syntheticLayoutWidget: Widget.Handle option = None
    let mutable visible = true
            
    member this.AddMenuBar(menuBar: MenuBar.Handle) =
        mainWindow.SetMenuBar(menuBar)

    member this.RemoveMenuBar() =
        // should automatically be removed by widget disposal
        // and in any case we don't have a handle to do it ourselves ...
        ()
        
    member this.AddStatusBar (statusBar: StatusBar.Handle) =
        mainWindow.SetStatusBar(statusBar)
        
    member this.RemoveStatusBar() =
        // see note in .RemoveMenuBar() above
        ()
        
    member this.RemoveContent() =
        // TODO: need to do some serious testing with all this
        // scrollArea too
        // if we're doing this ... hasn't the content node actually been disposed? so why all the fuss?
        match syntheticLayoutWidget with
        | Some widget ->
            // widget.GetLayout().RemoveAll() // detach any children just in case
            // widget.SetLayout(null)
            // deleting should automatically remove from the parent mainWindow, right?
            widget.Dispose()
            syntheticLayoutWidget <- None
        | None ->
            ()
        
    member this.AddContent(node: IWidgetOrLayoutNode<'msg>) =
        match node with
        | :? IWidgetNode<'msg> as widgetNode ->
            mainWindow.SetCentralWidget(widgetNode.Widget)
        | :? ILayoutNode<'msg> as layout ->
            let widget = Widget.CreateNoHandler()
            widget.SetLayout(layout.Layout)
            mainWindow.SetCentralWidget(widget)
            syntheticLayoutWidget <- Some widget
        | _ ->
            failwith "MainWindow.Model.AddContent - unknown node type"
            
    member this.RefillActions (actions: Action.Handle list) =
        // TODO: some way to remove them all?
        for action in actions do
            mainWindow.AddAction(action)
            
    member this.AddToolBar (toolBar: ToolBar.Handle) =
        mainWindow.AddToolBar(toolBar)
        
    // no way (or need?) of removing toolbars - if their nodes get deleted, they should be removed
        
    member this.ShowIfVisible () =
        // long story short, this is our workaround for Qt's layout system which doesn't necessarily play nicely with how we want to create our widget trees
        // this is invoked in a special step in the builder diff, after .AttachDeps has occurred
        // because a window/widget needs all its content attached first, before the layout will work properly (on .show)
        // during migrations however we can change visibility just fine, the layout has already occurred of course
        if visible then
            mainWindow.Show()
            
let private create2 (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: MainWindow.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model
    
let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: MainWindow.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type MainWindow<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    member val Actions: IActionNode<'msg> list = [] with get, set
    member val ToolBars: (string * IToolBarNode<'msg>) list = [] with get, set

    let mutable maybeContent: IWidgetOrLayoutNode<'msg> option = None
    member this.CentralWidget with set (value: IWidgetNode<'msg>) = maybeContent <- Some value
    member this.CentralLayout with set (value: ILayoutNode<'msg>) = maybeContent <- Some value
    
    let mutable maybeMenuBar: IMenuBarNode<'msg> option = None
    member this.MenuBar with set value = maybeMenuBar <- Some value
    
    let mutable maybeStatusBar: IStatusBarNode<'msg> option = None
    member this.StatusBar with set value = maybeStatusBar <- Some value
    
    member private this.MigrateContent (left: MainWindow<'msg>) (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "menubar") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddMenuBar(maybeMenuBar.Value.MenuBar)
            | Removed ->
                this.model.RemoveMenuBar()
            | Swapped ->
                this.model.RemoveMenuBar()
                this.model.AddMenuBar(maybeMenuBar.Value.MenuBar)
        | None ->
            // neither side had a menubar
            ()
            
        this.ToolBars
        |> List.iter (fun (key, node) ->
            let key =
                StrStrKey("toolbar", key)
            match changeMap.TryFind key with
            | Some change ->
                match change with
                | Unchanged ->
                    ()
                | Added ->
                    this.model.AddToolBar(node.ToolBar)
                | Removed ->
                    // nothing to do, should be removed automatically
                    ()
                | Swapped ->
                    // removal should be automatic
                    this.model.AddToolBar(node.ToolBar)
            | None ->
                ())
        
        match changeMap.TryFind (StrKey "content") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddContent(maybeContent.Value)
            | Removed ->
                this.model.RemoveContent()
            | Swapped ->
                this.model.RemoveContent()
                this.model.AddContent(maybeContent.Value)
        | None ->
            // neither side had 'content'
            ()
            
        match changeMap.TryFind (StrKey "status") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddStatusBar(maybeStatusBar.Value.StatusBar)
            | Removed ->
                this.model.RemoveStatusBar()
            | Swapped ->
                this.model.RemoveStatusBar()
                this.model.AddStatusBar(maybeStatusBar.Value.StatusBar)
        | None ->
            // neither side had 'statusbar'
            ()
            
        let leftActionContents =
            left.Actions
            |> List.map (_.ContentKey)
        let thisActionContents =
            this.Actions
            |> List.map (_.ContentKey)
        if leftActionContents <> thisActionContents then
            this.model.RefillActions(this.Actions |> List.map (_.Action))
        else
            ()
        
    interface IWindowNode<'msg> with
        override this.Dependencies =
            let menuBarList =
                maybeMenuBar
                |> Option.map (fun menuBar -> StrKey "menubar", menuBar :> IBuilderNode<'msg>)
                |> Option.toList
            let toolBarList =
                this.ToolBars
                |> List.map (fun (key, toolBar) -> StrStrKey("toolbar", key), toolBar :> IBuilderNode<'msg>)
            let contentList =
                maybeContent
                |> Option.map (fun content -> StrKey "content", content :> IBuilderNode<'msg>)
                |> Option.toList
            let statusBarList =
                maybeStatusBar
                |> Option.map (fun status -> StrKey "status", status :> IBuilderNode<'msg>)
                |> Option.toList
            let actionList =
                this.Actions
                |> List.mapi (fun i action -> StrIntKey ("action", i), action :> IBuilderNode<'msg>)
            menuBarList @ toolBarList @ contentList @ statusBarList @ actionList
            
        override this.Create dispatch buildContext =
            this.model <- create2 this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeMenuBar
            |> Option.iter (fun node -> this.model.AddMenuBar node.MenuBar)
            this.ToolBars
            |> List.iter (fun (_, node) -> this.model.AddToolBar node.ToolBar)
            maybeContent
            |> Option.iter this.model.AddContent
            maybeStatusBar
            |> Option.iter (fun node -> this.model.AddStatusBar node.StatusBar)
            this.model.RefillActions(this.Actions |> List.map (_.Action))

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> MainWindow<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent left' (depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.WindowWidget =
            this.model.Widget
            
        override this.ContentKey =
            this.model.Widget
            
        override this.Attachments =
            this.Attachments
        
        override this.ShowIfVisible () =
            this.model.ShowIfVisible()

        override this.Binding = None
