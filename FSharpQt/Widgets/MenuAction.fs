module FSharpQt.Widgets.MenuAction

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

open FSharpQt.Attrs

type private Signal =
    | Changed
    | CheckableChanged of checkable: bool
    | EnabledChanged of enabled: bool
    | Hovered
    | Toggled of checked_: bool
    | Triggered
    | TriggeredWithChecked of checked_: bool
    | VisibleChanged

type MenuRole =
    | NoRole
    | TextHeuristicRole
    | ApplicationSpecificRole
    | AboutQtRole
    | AboutRole
    | PreferencesRole
    | QuitRole
with
    member internal this.QtValue =
        match this with
        | NoRole -> Action.MenuRole.NoRole
        | TextHeuristicRole -> Action.MenuRole.TextHeuristicRole
        | ApplicationSpecificRole -> Action.MenuRole.ApplicationSpecificRole
        | AboutQtRole -> Action.MenuRole.AboutQtRole
        | AboutRole -> Action.MenuRole.AboutRole
        | PreferencesRole -> Action.MenuRole.PreferencesRole
        | QuitRole -> Action.MenuRole.QuitRole
        
type Priority =
    | LowPriority
    | NormalPriority
    | HighPriority
with
    member internal this.QtValue =
        match this with
        | LowPriority -> Action.Priority.LowPriority
        | NormalPriority -> Action.Priority.NormalPriority
        | HighPriority -> Action.Priority.HighPriority
    
type internal Attr =
    | AutoRepeat of state: bool
    | Checkable of state: bool
    | Checked of state: bool
    | Enabled of state: bool
    // | Font of font: ?
    | IconAttr of icon: Icon
    | IconText of text: string
    | IconVisibleInMenu of visible: bool
    | MenuRole of role: MenuRole
    | Priority of priority: Priority
    | Shortcut of shortcut: KeySequence
    | ShortcutContext of context: ShortcutContext
    | ShortcutVisibleInContextMenu of visible: bool
    | StatusTip of tip: string
    | Text of text: string
    | ToolTip of tip: string
    | Visible of state: bool
    | WhatsThis of text: string
with
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? Attr as otherAttr ->
                this = otherAttr
            | _ ->
                false
        override this.Key =
            match this with
            | AutoRepeat _ -> "menuaction:autorepeat"
            | Checkable _ -> "menuaction:checkable"
            | Checked _ -> "menuaction:checked"
            | Enabled _ -> "menuaction:enabled"
            | IconAttr _ -> "menuaction:iconattr"
            | IconText _ -> "menuaction:icontext"
            | IconVisibleInMenu _ -> "menuaction:iconvisibleinmenu"
            | MenuRole _ -> "menuaction:menurole"
            | Priority _ -> "menuaction:priority"
            | Shortcut _ -> "menuaction:shortcut"
            | ShortcutContext _ -> "menuaction:shortcutcontext"
            | ShortcutVisibleInContextMenu _ -> "menuaction:shortcutVisibleInContextMenu"
            | StatusTip _ -> "menuaction:statustip"
            | Text _ -> "menuaction:text"
            | ToolTip _ -> "menuaction:tooltip"
            | Visible _ -> "menuaction:visible"
            | WhatsThis _ -> "menuaction:whatsthis"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyMenuActionAttr(this)
            | _ ->
                printfn "warning: MenuAction.Attr couldn't ApplyTo() unknown object type [%A]" target
                
and internal AttrTarget =
    interface
        inherit QObject.AttrTarget
        abstract member ApplyMenuActionAttr: Attr -> unit
    end
                
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
                
type Props<'msg>() =
    inherit QObject.Props<'msg>()
    
    let mutable onChanged: 'msg option = None
    let mutable onCheckableChanged: (bool -> 'msg) option = None
    let mutable onEnabledChanged: (bool -> 'msg) option = None
    let mutable onHovered: 'msg option = None
    let mutable onToggled: (bool -> 'msg) option = None
    let mutable onTriggered: 'msg option = None
    let mutable onTriggeredWithChecked: (bool -> 'msg) option = None
    let mutable onVisibleChanged: 'msg option = None
    
    member internal this.SignalMask = enum<Action.SignalMask> (int this._signalMask)
    
    member this.OnChanged with set value =
        onChanged <- Some value
        this.AddSignal(int Action.SignalMask.Changed)
        
    member this.OnCheckableChanged with set value =
        onCheckableChanged <- Some value
        this.AddSignal(int Action.SignalMask.CheckableChanged)
        
    member this.OnEnabledChanged with set value =
        onEnabledChanged <- Some value
        this.AddSignal(int Action.SignalMask.EnabledChanged)
        
    member this.OnHovered with set value =
        onHovered <- Some value
        this.AddSignal(int Action.SignalMask.Hovered)
        
    member this.OnToggled with set value =
        onToggled <- Some value
        this.AddSignal(int Action.SignalMask.Toggled)
        
    member this.OnTriggered with set value = // triggered #1, no 'checked' param
        onTriggered <- Some value
        this.AddSignal(int Action.SignalMask.Triggered) 
        
    member this.OnTriggeredWithChecked with set value = // triggered #2, with 'checked' param
        onTriggeredWithChecked <- Some value
        this.AddSignal(int Action.SignalMask.Triggered)
        
    member this.OnVisibleChanged with set value =
        onVisibleChanged <- Some value
        this.AddSignal(int Action.SignalMask.VisibleChanged)
    
    member internal this.SignalMapList =
        let thisFunc = function
            | Changed ->
                onChanged
            | CheckableChanged value ->
                onCheckableChanged
                |> Option.map (fun f -> f value)
            | EnabledChanged value ->
                onEnabledChanged
                |> Option.map (fun f -> f value)
            | Hovered ->
                onHovered
            | Toggled value ->
                onToggled
                |> Option.map (fun f -> f value)
            | Triggered ->
                onTriggered
            | TriggeredWithChecked value ->
                onTriggeredWithChecked
                |> Option.map (fun f -> f value)
            | VisibleChanged ->
                onVisibleChanged
        // we inherit from Object, so prepend to its signals
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.AutoRepeat with set value =
        this.PushAttr(AutoRepeat value)

    member this.Checkable with set value =
        this.PushAttr(Checkable value)

    member this.Checked with set value =
        this.PushAttr(Checked value)

    member this.Enabled with set value =
        this.PushAttr(Enabled value)

    // member this.Font with set value =
    //     this.PushAttr(Font value)

    member this.Icon with set value =
        this.PushAttr(IconAttr value)

    member this.IconText with set value =
        this.PushAttr(IconText value)

    member this.IconVisibleInMenu with set value =
        this.PushAttr(IconVisibleInMenu value)

    member this.MenuRole with set value =
        this.PushAttr(MenuRole value)

    member this.Priority with set value =
        this.PushAttr(Priority value)

    member this.Shortcut with set value =
        this.PushAttr(Shortcut value)

    member this.ShortcutContext with set value =
        this.PushAttr(ShortcutContext value)

    member this.ShortcutVisibleInContextMenu with set value =
        this.PushAttr(ShortcutVisibleInContextMenu value)

    member this.StatusTip with set value =
        this.PushAttr(StatusTip value)

    member this.Text with set value =
        this.PushAttr(Text value)

    member this.ToolTip with set value =
        this.PushAttr(ToolTip value)

    member this.Visible with set value =
        this.PushAttr(Visible value)

    member this.WhatsThis with set value =
        this.PushAttr(WhatsThis value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit QObject.ModelCore<'msg>(dispatch)
    let mutable action: Action.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Action.SignalMask> 0
    // 2-way binding guards:
    let mutable lastEnabled = true
    let mutable lastCheckable = false
    let mutable lastChecked = false
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Action
        with get() = action
        and set value =
            this.Object <- value
            action <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "MenuAction.ModelCore.SignalMaps: wrong func type"
            // assign the remainder up the hierarchy
            base.SignalMaps <- etc
        | _ ->
            failwith "MenuAction.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            action.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyMenuActionAttr attr =
            match attr with
            | AutoRepeat state ->
                action.SetAutoRepeat(state)
            | Checkable state ->
                if state <> lastCheckable then
                    lastCheckable <- state
                    action.SetCheckable(state)
            | Checked state ->
                if state <> lastChecked then
                    lastChecked <- state
                    action.SetChecked(state)
            | Enabled state ->
                if state <> lastEnabled then
                    lastEnabled <- state
                    action.SetEnabled(state)
            | IconAttr icon ->
                action.SetIcon(icon.QtValue)
            | IconText text ->
                action.SetIconText(text)
            | IconVisibleInMenu visible ->
                action.SetIconVisibleInMenu(visible)
            | MenuRole role ->
                action.SetMenuRole(role.QtValue)
            | Priority priority ->
                action.SetPriority(priority.QtValue)
            | Shortcut shortcut ->
                action.SetShortcut(shortcut.QtValue)
            | ShortcutContext context ->
                action.SetShortcutContext(context.QtValue)
            | ShortcutVisibleInContextMenu visible ->
                action.SetShortcutVisibleInContextMenu(visible)
            | StatusTip tip ->
                action.SetStatusTip(tip)
            | Text text ->
                action.SetText(text)
            | ToolTip tip ->
                action.SetToolTip(tip)
            | Visible state ->
                action.SetVisible(state)
            | WhatsThis text ->
                action.SetWhatsThis(text)
                
    interface Action.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // action =========================
        override this.Changed () =
            signalDispatch Changed
        override this.CheckableChanged newState =
            lastCheckable <- newState
            signalDispatch (CheckableChanged newState)
        override this.EnabledChanged newState =
            lastEnabled <- newState
            signalDispatch (EnabledChanged newState)
        override this.Hovered () =
            signalDispatch Hovered
        override this.Toggled newState =
            lastChecked <- newState
            signalDispatch (Toggled newState)
        override this.Triggered checked_ =
            lastChecked <- checked_
            signalDispatch Triggered
            signalDispatch (TriggeredWithChecked checked_)
        override this.VisibleChanged () =
            signalDispatch VisibleChanged
            
    interface IDisposable with
        member this.Dispose() =
            action.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit, maybeContainingWindow: Widget.Handle option) as this =
    inherit ModelCore<'msg>(dispatch)
    let owner =
        match maybeContainingWindow with
        | Some handle ->
            // printfn "MenuAction being created with owner [%A]" handle
            handle
        | None ->
            printfn "MenuAction created with no owner"
            null
    let mutable action = Action.Create(owner, this)
    do
        this.Action <- action

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Action.SignalMask) (maybeContainingWindow: Widget.Handle option) =
    let model = new Model<'msg>(dispatch, maybeContainingWindow)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Action.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type BuildState =
    | Init
    | Created
    | DepsAttached
    | Migrated
    | Disposed
            
type MenuAction<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    let mutable buildState = Init
    
    interface IActionNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            // since Actions can be dependencies of multiple nodes (eg MainWindow, menus, context menus, toolbars, etc),
            // we need to protect against rebuilding the model each time
            match buildState with
            | Init ->
                this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask buildContext.ContainingWindow
                buildState <- Created
            | _ ->
                // ignore
                ()
            
        override this.AttachDeps () =
            // see .Create node
            match buildState with
            | Created ->
                // not that we're using this, but for any future use ...
                buildState <- DepsAttached
            | _ ->
                // ignore
                ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            // see .Create node
            match buildState with
            | Init ->
                // note migration always occurs straight from Init
                let left' = (left :?> MenuAction<'msg>)
                let nextAttrs =
                    diffAttrs left'.Attrs this.Attrs
                    |> createdOrChanged
                this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
                buildState <- Migrated
            | _ ->
                // ignore
                ()
                
        override this.Dispose() =
            match buildState with
            | Disposed ->
                // already disposed, do nothing
                ()
            | _ ->
                dispose this.model
                buildState <- Disposed
            
        override this.Action =
            this.model.Action
            
        override this.ContentKey =
            this.model.Action
            
        override this.Attachments =
            this.Attachments
            
        override this.Binding = None
