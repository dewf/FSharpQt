module FSharpQt.Widgets.Widget

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes
open FSharpQt.Attrs

type private Signal =
    | CustomContextMenuRequested of pos: Point
    | WindowIconChanged of icon: IconProxy
    | WindowTitleChanged of title: string

type FocusPolicy =
    | NoFocus
    | TabFocus
    | ClickFocus
    | StrongFocus
    | WheelFocus
with
    member internal this.QtValue =
        match this with
        | NoFocus -> Enums.FocusPolicy.NoFocus
        | TabFocus -> Enums.FocusPolicy.TabFocus
        | ClickFocus -> Enums.FocusPolicy.ClickFocus
        | StrongFocus -> Enums.FocusPolicy.StrongFocus
        | WheelFocus -> Enums.FocusPolicy.WheelFocus
        
type InputMethodHint =
    | HiddenText
    | SensitiveData
    | NoAutoUppercase
    | PreferNumbers
    | PreferUppercase
    | PreferLowercase
    | NoPredictiveText
    | Date
    | Time
    | PreferLatin
    | MultiLine
    | NoEditMenu
    | NoTextHandles
    | DigitsOnly
    | FormattedNumbersOnly
    | UppercaseOnly
    | LowercaseOnly
    | DialableCharactersOnly
    | EmailCharactersOnly
    | UrlCharactersOnly
    | LatinOnly
    | ExclusiveInputMask
with
    static member QtSetFrom (hints: InputMethodHint seq) =
        (LanguagePrimitives.EnumOfValue<uint, Enums.InputMethodHints> 0u, hints)
        ||> Seq.fold (fun acc hint ->
            let flag =
                match hint with
                | HiddenText -> Enums.InputMethodHints.ImhHiddenText
                | SensitiveData -> Enums.InputMethodHints.ImhSensitiveData
                | NoAutoUppercase -> Enums.InputMethodHints.ImhNoAutoUppercase
                | PreferNumbers -> Enums.InputMethodHints.ImhPreferNumbers
                | PreferUppercase -> Enums.InputMethodHints.ImhPreferUppercase
                | PreferLowercase -> Enums.InputMethodHints.ImhPreferLowercase
                | NoPredictiveText -> Enums.InputMethodHints.ImhNoPredictiveText
                | Date -> Enums.InputMethodHints.ImhDate
                | Time -> Enums.InputMethodHints.ImhTime
                | PreferLatin -> Enums.InputMethodHints.ImhPreferLatin
                | MultiLine -> Enums.InputMethodHints.ImhMultiLine
                | NoEditMenu -> Enums.InputMethodHints.ImhNoEditMenu
                | NoTextHandles -> Enums.InputMethodHints.ImhNoTextHandles
                | DigitsOnly -> Enums.InputMethodHints.ImhDigitsOnly
                | FormattedNumbersOnly -> Enums.InputMethodHints.ImhFormattedNumbersOnly
                | UppercaseOnly -> Enums.InputMethodHints.ImhUppercaseOnly
                | LowercaseOnly -> Enums.InputMethodHints.ImhLowercaseOnly
                | DialableCharactersOnly -> Enums.InputMethodHints.ImhDialableCharactersOnly
                | EmailCharactersOnly -> Enums.InputMethodHints.ImhEmailCharactersOnly
                | UrlCharactersOnly -> Enums.InputMethodHints.ImhUrlCharactersOnly
                | LatinOnly -> Enums.InputMethodHints.ImhLatinOnly
                | ExclusiveInputMask -> Enums.InputMethodHints.ImhExclusiveInputMask
            acc ||| flag)
   
type LayoutDirection =
    | LeftToRight
    | RightToLeft
    | LayoutDirectionAuto
with
    member this.QtValue =
        match this with
        | LeftToRight -> Enums.LayoutDirection.LeftToRight
        | RightToLeft -> Enums.LayoutDirection.RightToLeft
        | LayoutDirectionAuto -> Enums.LayoutDirection.LayoutDirectionAuto
        
type WindowFlag =
    | Widget
    | Window
    | Dialog
    | Sheet
    | Drawer
    | Popup
    | Tool
    | ToolTip
    | SplashScreen
    | Desktop
    | SubWindow
    | ForeignWindow
    | CoverWindow
    | WindowType_Mask
    | MSWindowsFixedSizeDialogHint
    | MSWindowsOwnDC
    | BypassWindowManagerHint
    | X11BypassWindowManagerHint
    | FramelessWindowHint
    | WindowTitleHint
    | WindowSystemMenuHint
    | WindowMinimizeButtonHint
    | WindowMaximizeButtonHint
    | WindowMinMaxButtonsHint
    | WindowContextHelpButtonHint
    | WindowShadeButtonHint
    | WindowStaysOnTopHint
    | WindowTransparentForInput
    | WindowOverridesSystemGestures
    | WindowDoesNotAcceptFocus
    | MaximizeUsingFullscreenGeometryHint
    | CustomizeWindowHint
    | WindowStaysOnBottomHint
    | WindowCloseButtonHint
    | MacWindowToolBarButtonHint
    | BypassGraphicsProxyWidget
    | NoDropShadowWindowHint
    | WindowFullscreenButtonHint
with
    static member QtSetFrom (flags: WindowFlag seq) =
        (LanguagePrimitives.EnumOfValue<uint, Enums.WindowFlags> 0u, flags)
        ||> Seq.fold (fun acc wf ->
            let flag =
                match wf with
                | Widget -> Enums.WindowFlags.Widget
                | Window -> Enums.WindowFlags.Window
                | Dialog -> Enums.WindowFlags.Dialog
                | Sheet -> Enums.WindowFlags.Sheet
                | Drawer -> Enums.WindowFlags.Drawer
                | Popup -> Enums.WindowFlags.Popup
                | Tool -> Enums.WindowFlags.Tool
                | ToolTip -> Enums.WindowFlags.ToolTip
                | SplashScreen -> Enums.WindowFlags.SplashScreen
                | Desktop -> Enums.WindowFlags.Desktop
                | SubWindow -> Enums.WindowFlags.SubWindow
                | ForeignWindow -> Enums.WindowFlags.ForeignWindow
                | CoverWindow -> Enums.WindowFlags.CoverWindow
                | WindowType_Mask -> Enums.WindowFlags.WindowType_Mask
                | MSWindowsFixedSizeDialogHint -> Enums.WindowFlags.MSWindowsFixedSizeDialogHint
                | MSWindowsOwnDC -> Enums.WindowFlags.MSWindowsOwnDC
                | BypassWindowManagerHint -> Enums.WindowFlags.BypassWindowManagerHint
                | X11BypassWindowManagerHint -> Enums.WindowFlags.X11BypassWindowManagerHint
                | FramelessWindowHint -> Enums.WindowFlags.FramelessWindowHint
                | WindowTitleHint -> Enums.WindowFlags.WindowTitleHint
                | WindowSystemMenuHint -> Enums.WindowFlags.WindowSystemMenuHint
                | WindowMinimizeButtonHint -> Enums.WindowFlags.WindowMinimizeButtonHint
                | WindowMaximizeButtonHint -> Enums.WindowFlags.WindowMaximizeButtonHint
                | WindowMinMaxButtonsHint -> Enums.WindowFlags.WindowMinMaxButtonsHint
                | WindowContextHelpButtonHint -> Enums.WindowFlags.WindowContextHelpButtonHint
                | WindowShadeButtonHint -> Enums.WindowFlags.WindowShadeButtonHint
                | WindowStaysOnTopHint -> Enums.WindowFlags.WindowStaysOnTopHint
                | WindowTransparentForInput -> Enums.WindowFlags.WindowTransparentForInput
                | WindowOverridesSystemGestures -> Enums.WindowFlags.WindowOverridesSystemGestures
                | WindowDoesNotAcceptFocus -> Enums.WindowFlags.WindowDoesNotAcceptFocus
                | MaximizeUsingFullscreenGeometryHint -> Enums.WindowFlags.MaximizeUsingFullscreenGeometryHint
                | CustomizeWindowHint -> Enums.WindowFlags.CustomizeWindowHint
                | WindowStaysOnBottomHint -> Enums.WindowFlags.WindowStaysOnBottomHint
                | WindowCloseButtonHint -> Enums.WindowFlags.WindowCloseButtonHint
                | MacWindowToolBarButtonHint -> Enums.WindowFlags.MacWindowToolBarButtonHint
                | BypassGraphicsProxyWidget -> Enums.WindowFlags.BypassGraphicsProxyWidget
                | NoDropShadowWindowHint -> Enums.WindowFlags.NoDropShadowWindowHint
                | WindowFullscreenButtonHint -> Enums.WindowFlags.WindowFullscreenButtonHint
            acc ||| flag)

    
type internal Attr =
    | AcceptDrops of accept: bool
    | AccessibleDescription of desc: string
    | AccessibleName of name: string
    | AutoFillBackground of state: bool
    | BaseSize of size: Size
    | ContextMenuPolicy of policy: ContextMenuPolicy
    | Enabled of enabled: bool
    | FocusPolicy of policy: FocusPolicy
    | Geometry of rect: Rect
    | InputMethodHints of hints: InputMethodHint seq
    | LayoutDirection of direction: LayoutDirection
    | MaximumHeight of height: int
    | MaximumWidth of width: int
    | MaximumSize of size: Size
    | MinimumHeight of height: int
    | MinimumSize of size: Size
    | MinimumWidth of width: int
    | MouseTracking of enabled: bool
    | Position of pos: Point            // move
    | Size of size: Size                // resize
    | SizeIncrement of size: Size
    | SizePolicy of policy: SizePolicyDeferred
    | SizePolicy2 of hPolicy: SizePolicy * vPolicy: SizePolicy
    | StatusTip of tip: string
    | StyleSheet of styles: string
    | TabletTracking of enabled: bool
    | ToolTip of tip: string
    | ToolTipDuration of msecs: int
    | UpdatesEnabled of enabled: bool
    | Visible of visible: bool
    | WhatsThis of text: string
    | WindowFilePath of path: string
    | WindowFlags of flags: WindowFlag seq
    | WindowIcon of icon: Icon
    | WindowModality of modality: WindowModality
    | WindowModified of modified: bool
    | WindowOpacity of opacity: double
    | WindowTitle of title: string
    // non-properties/convenience methods (not sure the distinction even matters in this context)
    | FixedWidth of width: int
    | FixedHeight of height: int
    | FixedSize of size: Size
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
            | AcceptDrops _ -> "widget:AcceptDrops"
            | AccessibleDescription _ -> "widget:AccessibleDescription"
            | AccessibleName _ -> "widget:AccessibleName"
            | AutoFillBackground _ -> "widget:AutoFillBackground"
            | BaseSize _ -> "widget:BaseSize"
            | ContextMenuPolicy _ -> "widget:ContextMenuPolicy"
            | Enabled _ -> "widget:Enabled"
            | FocusPolicy _ -> "widget:FocusPolicy"
            | Geometry _ -> "widget:Geometry"
            | InputMethodHints _ -> "widget:InputMethodHints"
            | LayoutDirection _ -> "widget:LayoutDirection"
            | MaximumHeight _ -> "widget:MaximumHeight"
            | MaximumWidth _ -> "widget:MaximumWidth"
            | MaximumSize _ -> "widget:MaximumSize"
            | MinimumHeight _ -> "widget:MinimumHeight"
            | MinimumSize _ -> "widget:MinimumSize"
            | MinimumWidth _ -> "widget:MinimumWidth"
            | MouseTracking _ -> "widget:MouseTracking"
            | Position _ -> "widget:Position"
            | Size _ -> "widget:Size"
            | SizeIncrement _ -> "widget:SizeIncrement"
            | SizePolicy _ -> "widget:SizePolicy"
            | SizePolicy2 _ -> "widget:SizePolicy2"
            | StatusTip _ -> "widget:StatusTip"
            | StyleSheet _ -> "widget:StyleSheet"
            | TabletTracking _ -> "widget:TabletTracking"
            | ToolTip _ -> "widget:ToolTip"
            | ToolTipDuration _ -> "widget:ToolTipDuration"
            | UpdatesEnabled _ -> "widget:UpdatesEnabled"
            | Visible _ -> "widget:Visible"
            | WhatsThis _ -> "widget:WhatsThis"
            | WindowFilePath _ -> "widget:WindowFilePath"
            | WindowFlags _ -> "widget:WindowFlags"
            | WindowIcon _ -> "widget:WindowIcon"
            | WindowModality _ -> "widget:WindowModality"
            | WindowModified _ -> "widget:WindowModified"
            | WindowOpacity _ -> "widget:WindowOpacity"
            | WindowTitle _ -> "widget:WindowTitle"
            // setter methods:
            | FixedWidth _ -> "widget:FixedWidth"
            | FixedHeight _ -> "widget:FixedHeight"
            | FixedSize _ -> "widget:FixedSize"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyWidgetAttr this
            | _ ->
                printfn "warning: Widget.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit QObject.AttrTarget
        abstract member ApplyWidgetAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit QObject.Props<'msg>()
    
    let mutable onCustomContextMenuRequested: (Point -> 'msg) option = None
    let mutable onWindowIconChanged: (IconProxy -> 'msg) option = None
    let mutable onWindowTitleChanged: (string -> 'msg) option = None

    member internal this.SignalMask = enum<Widget.SignalMask> (int this._signalMask)
    
    member this.OnCustomContextMenuRequested with set value =
        onCustomContextMenuRequested <- Some value
        this.AddSignal(int Widget.SignalMask.CustomContextMenuRequested)
        
    member this.OnWindowIconChanged with set value =
        onWindowIconChanged <- Some value
        this.AddSignal(int Widget.SignalMask.WindowIconChanged)
        
    member this.OnWindowTitleChanged with set value =
        onWindowTitleChanged <- Some value
        this.AddSignal(int Widget.SignalMask.WindowTitleChanged)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | CustomContextMenuRequested pos ->
                onCustomContextMenuRequested
                |> Option.map (fun f -> f pos)
            | WindowIconChanged icon ->
                onWindowIconChanged
                |> Option.map (fun f -> f icon)
            | WindowTitleChanged title ->
                onWindowTitleChanged
                |> Option.map (fun f -> f title)
        // we inherit from something (QObject), so must prepend to its signals
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.AcceptDrops with set value =
        this.PushAttr(AcceptDrops value)
        
    member this.AccessibleDescription with set value =
        this.PushAttr(AccessibleDescription value)
        
    member this.AccessibleName with set value =
        this.PushAttr(AccessibleName value)
        
    member this.AutoFillBackground with set value =
        this.PushAttr(AutoFillBackground value)
        
    member this.BaseSize with set value =
        this.PushAttr(BaseSize value)
        
    member this.ContextMenuPolicy with set value =
        this.PushAttr(ContextMenuPolicy value)
        
    member this.Enabled with set value =
        this.PushAttr(Enabled value)
        
    member this.FocusPolicy with set value =
        this.PushAttr(FocusPolicy value)
        
    member this.Geometry with set value =
        this.PushAttr(Geometry value)
        
    member this.InputMethodHints with set value =
        this.PushAttr(InputMethodHints value)
        
    member this.LayoutDirection with set value =
        this.PushAttr(LayoutDirection value)
        
    member this.MaximumHeight with set value =
        this.PushAttr(MaximumHeight value)
        
    member this.MaximumWidth with set value =
        this.PushAttr(MaximumWidth value)
        
    member this.MaximumSize with set value =
        this.PushAttr(MaximumSize value)
        
    member this.MinimumHeight with set value =
        this.PushAttr(MinimumHeight value)
        
    member this.MinimumSize with set value =
        this.PushAttr(MinimumSize value)
        
    member this.MinimumWidth with set value =
        this.PushAttr(MinimumWidth value)
        
    member this.MouseTracking with set value =
        this.PushAttr(MouseTracking value)
        
    member this.Position with set value =
        this.PushAttr(Position value)
        
    member this.Size with set value =
        this.PushAttr(Size value)
        
    member this.SizeIncrement with set value =
        this.PushAttr(SizeIncrement value)
        
    member this.SizePolicy with set value =
        this.PushAttr(SizePolicy value)
        
    member this.SizePolicy2 with set value =
        this.PushAttr(SizePolicy2 value)
                      
    member this.StatusTip with set value =
        this.PushAttr(StatusTip value)
        
    member this.StyleSheet with set value =
        this.PushAttr(StyleSheet value)
        
    member this.TabletTracking with set value =
        this.PushAttr(TabletTracking value)
        
    member this.ToolTip with set value =
        this.PushAttr(ToolTip value)
        
    member this.ToolTipDuration with set value =
        this.PushAttr(ToolTipDuration value)
        
    member this.UpdatesEnabled with set value =
        this.PushAttr(UpdatesEnabled value)
        
    member this.Visible with set value =
        this.PushAttr(Visible value)
        
    member this.WhatsThis with set value =
        this.PushAttr(WhatsThis value)
        
    member this.WindowFilePath with set value =
        this.PushAttr(WindowFilePath value)
        
    member this.WindowFlags with set value =
        this.PushAttr(WindowFlags value)
        
    member this.WindowIcon with set value =
        this.PushAttr(WindowIcon value)
        
    member this.WindowModality with set value =
        this.PushAttr(WindowModality value)
        
    member this.WindowModified with set value =
        this.PushAttr(WindowModified value)
        
    member this.WindowOpacity with set value =
        this.PushAttr(WindowOpacity value)
        
    member this.WindowTitle with set value =
        this.PushAttr(WindowTitle value)
        
    // setter methods:
    member this.FixedWidth with set value =
        this.PushAttr(FixedWidth value)
        
    member this.FixedHeight with set value =
        this.PushAttr(FixedHeight value)
        
    member this.FixedSize with set value =
        this.PushAttr(FixedSize value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit QObject.ModelCore<'msg>(dispatch)
    let mutable widget: Widget.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Widget.SignalMask> 0
    // binding guards
    let mutable lastWindowTitle = ""
    let mutable lastWindowIcon = new Icon()

    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Widget
        with get() = widget
        and set value =
            // must assign to base as well
            this.Object <- value
            widget <- value
            
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
            widget.SetSignalMask(value)
            currentMask <- value
    
    interface AttrTarget with
        member this.ApplyWidgetAttr attr =
            match attr with
            | AcceptDrops accept ->
                widget.SetAcceptDrops(accept)
            | AccessibleDescription desc ->
                widget.SetAccessibleDescription(desc)
            | AccessibleName name ->
                widget.SetAccessibleName(name)
            | AutoFillBackground state ->
                widget.SetAutoFillBackground(state)
            | BaseSize size ->
                widget.SetBaseSize(size.QtValue)
            | ContextMenuPolicy policy ->
                widget.SetContextMenuPolicy(policy.QtValue)
            | Enabled enabled ->
                widget.SetEnabled(enabled)
            | FocusPolicy policy ->
                widget.SetFocusPolicy(policy.QtValue)
            | Geometry rect ->
                widget.SetGeometry(rect.QtValue)
            | InputMethodHints hints ->
                widget.SetInputMethodHints(hints |> InputMethodHint.QtSetFrom)
            | LayoutDirection direction ->
                widget.SetLayoutDirection(direction.QtValue)
            | MaximumHeight height ->
                widget.SetMaximumHeight(height)
            | MaximumWidth width ->
                widget.SetMaximumWidth(width)
            | MaximumSize size ->
                widget.SetMaximumSize(size.QtValue)
            | MinimumHeight height ->
                widget.SetMaximumHeight(height)
            | MinimumSize size ->
                widget.SetMinimumSize(size.QtValue)
            | MinimumWidth width ->
                widget.SetMinimumWidth(width)
            | MouseTracking enabled ->
                widget.SetMouseTracking(enabled)
            | Position pos ->
                widget.Move(pos.QtValue)
            | Size size ->
                widget.Resize(size.QtValue)
            | SizeIncrement size ->
                widget.SetSizeIncrement(size.QtValue)
            | SizePolicy policy ->
                widget.SetSizePolicy(policy.QtValue)
            | SizePolicy2(hPolicy, vPolicy) ->
                widget.SetSizePolicy(hPolicy.QtValue, vPolicy.QtValue)
            | StatusTip tip ->
                widget.SetStatusTip(tip)
            | StyleSheet styles ->
                widget.SetStyleSheet(styles)
            | TabletTracking enabled ->
                widget.SetTabletTracking(enabled)
            | ToolTip tip ->
                widget.SetToolTip(tip)
            | ToolTipDuration msecs ->
                widget.SetToolTipDuration(msecs)
            | UpdatesEnabled enabled ->
                widget.SetUpdatesEnabled(enabled)
            | Visible visible ->
                widget.SetVisible(visible)
            | WhatsThis text ->
                widget.SetWhatsThis(text)
            | WindowFilePath path ->
                widget.SetWindowFilePath(path)
            | WindowFlags flags ->
                widget.SetWindowFlags(flags |> WindowFlag.QtSetFrom)
            | WindowIcon icon ->
                if icon <> lastWindowIcon then
                    FSharpQt.Util.dispose lastWindowIcon
                    lastWindowIcon <- icon
                    widget.SetWindowIcon(icon.Handle)
            | WindowModality modality ->
                widget.SetWindowModality(modality.QtValue)
            | WindowModified modified ->
                widget.SetWindowModified(modified)
            | WindowOpacity opacity ->
                widget.SetWindowOpacity(opacity)
            | WindowTitle title ->
                if title <> lastWindowTitle then
                    lastWindowTitle <- title
                    widget.SetWindowTitle(title)
            // setter methods
            | FixedWidth width ->
                widget.SetFixedWidth(width)
            | FixedHeight height ->
                widget.SetFixedHeight(height)
            | FixedSize size ->
                widget.SetFixedSize(size.QtValue)
        
    interface Widget.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // widget =========================
        member this.CustomContextMenuRequested pos =
            signalDispatch (Point.From pos |> CustomContextMenuRequested)
        member this.WindowIconChanged icon =
            // TODO: lastWindowIcon <- ???
            // hmm, how are we going to do this? incoming handle (unowned pointer),
            // but stored value is a deferred icon
            // but the pointer values are temporary and the icon itself is on a soon-to-be-destroyed stack
            // of the top of my head I'd say "never 2-way bind this value" ...
            // for that matter, do we really need a signal for it? won't it always be property-driven?
            signalDispatch (IconProxy(icon) |> WindowIconChanged)
        member this.WindowTitleChanged title =
            lastWindowTitle <- title
            signalDispatch (WindowTitleChanged title)
            
    interface IDisposable with
        member this.Dispose() =
            widget.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    // tried supplying this as a ModelCore ctor parameter but caused issues :(
    // (.NET won't allow us to pass 'this' if it's uninitialized,
    //  but it's necessary for Widget.Create(this))
    let widget = Widget.Create(this)
    do
        this.Widget <- widget
    
    member this.RemoveLayout() =
        // the only way the layout's going to change is if it's deleted as a dependency
        // ... so is any of this even necessary? won't the layout remove itself in its dtor?
        let existing =
            widget.GetLayout()
        existing.RemoveAll()
        widget.SetLayout(null)
        
    member this.AddLayout(layout: Layout.Handle) =
        widget.SetLayout(layout)
        
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Widget.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Widget.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type WidgetBinding internal(handle: Widget.Handle) =
    interface IViewBinding
    member this.MapToGlobal (loc: Point) =
        handle.MapToGlobal(loc.QtValue)
        |> Point.From
    member this.Update() =
        handle.Update()
    member this.Update(x: int, y: int, width: int, height: int) =
        handle.Update(x, y, width, height)
    member this.Update(rect: Rect) =
        handle.Update(rect.QtValue)
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? WidgetBinding as widget) ->
        widget
    | _ ->
        failwith "Widget.bindNode fail"

type Widget<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeLayout: ILayoutNode<'msg> option = None
    member this.Layout with set value = maybeLayout <- Some value
    
    member private this.MigrateContent (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "layout") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddLayout(maybeLayout.Value.Layout)
            | Removed ->
                this.model.RemoveLayout()
            | Swapped ->
                this.model.RemoveLayout()
                this.model.AddLayout(maybeLayout.Value.Layout)
        | None ->
            // neither side had a layout
            ()
    
    interface IWidgetNode<'msg> with
        override this.Dependencies =
            maybeLayout
            |> Option.map (fun content -> (StrKey "layout", content :> IBuilderNode<'msg>))
            |> Option.toList
  
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeLayout
            |> Option.iter (fun node -> this.model.AddLayout(node.Layout))
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> Widget<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent (depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.Widget
            
        override this.ContentKey =
            this.model.Widget
            
        override this.Attachments =
            this.Attachments
            
        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, WidgetBinding(this.model.Widget))
