module FSharpQt.Widgets.TabWidget

open System
open FSharpQt
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | CurrentChanged of index: int
    | TabBarClicked of index: int
    | TabBarDoubleClicked of index: int
    | TabCloseRequested of index: int

type TabShape =
    | Rounded
    | Triangular
with
    member internal this.QtValue =
        match this with
        | Rounded -> TabWidget.TabShape.Rounded
        | Triangular -> TabWidget.TabShape.Triangular
    
type TabPosition =
    | North
    | South
    | East
    | West
with
    member internal this.QtValue =
        match this with
        | North -> TabWidget.TabPosition.North
        | South -> TabWidget.TabPosition.South
        | East -> TabWidget.TabPosition.East
        | West -> TabWidget.TabPosition.West
    
type internal Attr =
    | CurrentIndex of index: int
    | DocumentMode of state: bool
    | ElideMode of mode: TextElideMode
    | IconSize of size: Size
    | Movable of state: bool
    | TabBarAutoHide of state: bool
    | TabPosition of position: TabPosition
    | TabShape of shape: TabShape
    | TabsClosable of state: bool
    | UsesScrollButtons of state: bool
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
            | CurrentIndex index -> "tabwidget:currentindex"
            | DocumentMode state -> "tabwidget:documentmode"
            | ElideMode mode -> "tabwidget:elidemode"
            | IconSize size -> "tabwidget:iconsize"
            | Movable state -> "tabwidget:movable"
            | TabBarAutoHide state -> "tabwidget:tabbarautohide"
            | TabPosition position -> "tabwidget:tabposition"
            | TabShape shape -> "tabwidget:tabshape"
            | TabsClosable state -> "tabwidget:tabsclosable"
            | UsesScrollButtons state -> "tabwidget:scrollbuttons"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyTabWidgetAttr(this)
            | _ ->
                printfn "warning: TabWidget.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyTabWidgetAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onCurrentChanged: (int -> 'msg) option = None
    let mutable onTabBarClicked: (int -> 'msg) option = None
    let mutable onTabBarDoubleClicked: (int -> 'msg) option = None
    let mutable onTabCloseRequested: (int -> 'msg) option = None
    
    member internal this.SignalMask = enum<TabWidget.SignalMask> (int this._signalMask)
        
    member this.OnCurrentChanged with set value =
        onCurrentChanged <- Some value
        this.AddSignal(int TabWidget.SignalMask.CurrentChanged)
        
    member this.OnTabBarClicked with set value =
        onTabBarClicked <- Some value
        this.AddSignal(int TabWidget.SignalMask.TabBarClicked)
        
    member this.OnTabBarDoubleClicked with set value =
        onTabBarDoubleClicked <- Some value
        this.AddSignal(int TabWidget.SignalMask.TabBarDoubleClicked)
        
    member this.OnTabCloseRequested with set value =
        onTabCloseRequested <- Some value
        this.AddSignal(int TabWidget.SignalMask.TabCloseRequested)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | CurrentChanged index ->
                onCurrentChanged
                |> Option.map (fun f -> f index)
            | TabBarClicked index ->
                onTabBarClicked
                |> Option.map (fun f -> f index)
            | TabBarDoubleClicked index ->
                onTabBarDoubleClicked
                |> Option.map (fun f -> f index)
            | TabCloseRequested index ->
                onTabCloseRequested
                |> Option.map (fun f -> f index)
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.CurrentIndex with set value =
        this.PushAttr(CurrentIndex value)
        
    member this.DocumentMode with set value =
        this.PushAttr(DocumentMode value)
        
    member this.ElideMode with set value =
        this.PushAttr(ElideMode value)
        
    member this.IconSize with set value =
        this.PushAttr(IconSize value)
        
    member this.Movable with set value =
        this.PushAttr(Movable value)
        
    member this.TabBarAutoHide with set value =
        this.PushAttr(TabBarAutoHide value)
        
    member this.TabPosition with set value =
        this.PushAttr(TabPosition value)
        
    member this.TabShape with set value =
        this.PushAttr(TabShape value)
        
    member this.TabsClosable with set value =
        this.PushAttr(TabsClosable value)
        
    member this.UsesScrollButtons with set value =
        this.PushAttr(UsesScrollButtons value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable tabWidget: TabWidget.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<TabWidget.SignalMask> 0
    
    // binding guards:
    let mutable lastCurrentIndex = -1
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.TabWidget
        with get() = tabWidget
        and set value =
            // assign to base
            this.Widget <- value
            tabWidget <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "TabWidget.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "TabWidget.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            tabWidget.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyTabWidgetAttr attr =
            match attr with
            | CurrentIndex index ->
                if index <> lastCurrentIndex then
                    lastCurrentIndex <- index
                    tabWidget.SetCurrentIndex(index)
            | DocumentMode state ->
                tabWidget.SetDocumentMode(state)
            | ElideMode mode ->
                tabWidget.SetElideMode(mode.QtValue)
            | IconSize size ->
                tabWidget.SetIconSize(size.QtValue)
            | Movable state ->
                tabWidget.SetMovable(state)
            | TabBarAutoHide state ->
                tabWidget.SetTabBarAutoHide(state)
            | TabPosition position ->
                tabWidget.SetTabPosition(position.QtValue)
            | TabShape shape ->
                tabWidget.SetTabShape(shape.QtValue)
            | TabsClosable state ->
                tabWidget.SetTabsClosable(state)
            | UsesScrollButtons state ->
                tabWidget.SetUsesScrollButtons(state)
    
    interface TabWidget.SignalHandler with
        // Object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // Widget =========================
        member this.CustomContextMenuRequested pos =
            (this :> Widget.SignalHandler).CustomContextMenuRequested pos
        member this.WindowIconChanged icon =
            (this :> Widget.SignalHandler).WindowIconChanged icon
        member this.WindowTitleChanged title =
            (this :> Widget.SignalHandler).WindowTitleChanged title
        // TabWidget ======================
        member this.CurrentChanged index =
            signalDispatch (CurrentChanged index)
        member this.TabBarClicked index =
            signalDispatch (TabBarClicked index)
        member this.TabBarDoubleClicked index =
            signalDispatch (TabBarDoubleClicked index)
        member this.TabCloseRequested index =
            signalDispatch (TabCloseRequested index)
            
    interface IDisposable with
        member this.Dispose() =
            tabWidget.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let tabWidget = TabWidget.Create(this)
    do
        this.TabWidget <- tabWidget
    
    member this.Refill(pages: (string * Widget.Handle) list) =
        tabWidget.Clear()
        for label, widget in pages do
            tabWidget.AddTab(widget, label)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: TabWidget.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: TabWidget.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type TabWidget<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>

    // page label doubles as string dependency key
    // probably need to make that more apparent ...
    member val Pages: (string * IWidgetNode<'msg>) list = [] with get, set
    
    member private this.MigrateContent(leftTabWidget: TabWidget<'msg>) =
        let leftContents =
            leftTabWidget.Pages
            |> List.map (fun (label, node) -> label, node.ContentKey)
        let thisContents =
            this.Pages
            |> List.map (fun (label, node) -> label, node.ContentKey)
        if leftContents <> thisContents then
            let pageLabelsAndHandles =
                this.Pages
                |> List.map (fun (label, node) -> label, node.Widget)
            this.model.Refill(pageLabelsAndHandles)
        else
            ()
        
    interface IWidgetNode<'msg> with
        override this.Dependencies =
            this.Pages
            |> List.map (fun (name, node) -> StrKey name, node :> IBuilderNode<'msg>)
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            let pageLabelsAndHandles =
                this.Pages
                |> List.map (fun (label, widget) -> label, widget.Widget)
            this.model.Refill(pageLabelsAndHandles)
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> TabWidget<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent(left')
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.TabWidget
            
        override this.ContentKey =
            this.model.TabWidget
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
