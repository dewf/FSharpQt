module FSharpQt.Widgets.TabBar

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type private Signal =
    | CurrentChanged of index: int
    | TabBarClicked of index: int
    | TabBarDoubleClicked of index: int
    | TabCloseRequested of index: int
    | TabMoved of fromIndex: int * toIndex: int
    
type ButtonPosition =
    | LeftSide
    | RightSide
with
    member internal this.QtValue =
        match this with
        | LeftSide -> TabBar.ButtonPosition.LeftSide
        | RightSide -> TabBar.ButtonPosition.RightSide
    
type SelectionBehavior =
    | SelectLeftTab
    | SelectRightTab
    | SelectPreviousTab
with
    member internal this.QtValue =
        match this with
        | SelectLeftTab -> TabBar.SelectionBehavior.SelectLeftTab
        | SelectRightTab -> TabBar.SelectionBehavior.SelectRightTab
        | SelectPreviousTab -> TabBar.SelectionBehavior.SelectPreviousTab
    
type Shape =
    | RoundedNorth
    | RoundedSouth
    | RoundedWest
    | RoundedEast
    | TriangularNorth
    | TriangularSouth
    | TriangularWest
    | TriangularEast
with
    member internal this.QtValue =
        match this with
        | RoundedNorth -> TabBar.Shape.RoundedNorth
        | RoundedSouth -> TabBar.Shape.RoundedSouth
        | RoundedWest -> TabBar.Shape.RoundedWest
        | RoundedEast -> TabBar.Shape.RoundedEast
        | TriangularNorth -> TabBar.Shape.TriangularNorth
        | TriangularSouth -> TabBar.Shape.TriangularSouth
        | TriangularWest -> TabBar.Shape.TriangularWest
        | TriangularEast -> TabBar.Shape.TriangularEast

type internal Attr =
    | AutoHide of value: bool
    | ChangeCurrentOnDrag of value: bool
    | CurrentIndex of index: int
    | DocumentMode of value: bool
    | DrawBase of value: bool
    | ElideMode of mode: TextElideMode
    | Expanding of value: bool
    | IconSize of size: Size
    | Movable of value: bool
    | SelectionBehaviorOnRemove of behavior: SelectionBehavior
    | Shape of shape: Shape
    | TabsClosable of value: bool
    | UsesScrollButtons of value: bool
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
            | AutoHide _ -> "tabbar:autohide"
            | ChangeCurrentOnDrag _ -> "tabbar:changecurrentondrag"
            | CurrentIndex _ -> "tabbar:currentindex"
            | DocumentMode _ -> "tabbar:documentmode"
            | DrawBase _ -> "tabbar:drawbase"
            | ElideMode _ -> "tabbar:elidemode"
            | Expanding _ -> "tabbar:expanding"
            | IconSize _ -> "tabbar:iconsize"
            | Movable _ -> "tabbar:movable"
            | SelectionBehaviorOnRemove _ -> "tabbar:selectionbehavioronremove"
            | Shape _ -> "tabbar:shape"
            | TabsClosable _ -> "tabbar:tabsclosable"
            | UsesScrollButtons _ -> "tabbar:usesscrollbuttons"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyTabBarAttr(this)
            | _ ->
                printfn "warning: TabBar.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyTabBarAttr: Attr -> unit
    end
          
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onCurrentChanged: (int -> 'msg) option = None
    let mutable onTabBarClicked: (int -> 'msg) option = None
    let mutable onTabBarDoubleClicked: (int -> 'msg) option = None
    let mutable onTabCloseRequested: (int -> 'msg) option = None
    let mutable onTabMoved: (int * int -> 'msg) option = None
    
    member internal this.SignalMask = enum<TabBar.SignalMask> (int this._signalMask)

    member this.OnCurrentChanged with set value =
        onCurrentChanged <- Some value
        this.AddSignal(int TabBar.SignalMask.CurrentChanged)
        
    member this.OnTabBarClicked with set value =
        onTabBarClicked <- Some value
        this.AddSignal(int TabBar.SignalMask.TabBarClicked)
        
    member this.OnTabBarDoubleClicked with set value =
        onTabBarDoubleClicked <- Some value
        this.AddSignal(int TabBar.SignalMask.TabBarDoubleClicked)
        
    member this.OnTabCloseRequested with set value =
        onTabCloseRequested <- Some value
        this.AddSignal(int TabBar.SignalMask.TabCloseRequested)
        
    member this.OnTabMoved with set value =
        onTabMoved <- Some value
        this.AddSignal(int TabBar.SignalMask.TabMoved)
  
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
            | TabMoved (fromIndex, toIndex) ->
                onTabMoved
                |> Option.map (fun f -> f (fromIndex, toIndex))
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

    member this.AutoHide with set value =
        this.PushAttr(AutoHide value)
        
    member this.ChangeCurrentOnDrag with set value =
        this.PushAttr(ChangeCurrentOnDrag value)
        
    member this.CurrentIndex with set value =
        this.PushAttr(CurrentIndex value)
        
    member this.DocumentMode with set value =
        this.PushAttr(DocumentMode value)
        
    member this.DrawBase with set value =
        this.PushAttr(DrawBase value)
        
    member this.ElideMode with set value =
        this.PushAttr(ElideMode value)
        
    member this.Expanding with set value =
        this.PushAttr(Expanding value)
        
    member this.IconSize with set value =
        this.PushAttr(IconSize value)
        
    member this.Movable with set value =
        this.PushAttr(Movable value)
        
    member this.SelectionBehaviorOnRemove with set value =
        this.PushAttr(SelectionBehaviorOnRemove value)
        
    member this.Shape with set value =
        this.PushAttr(Shape value)
        
    member this.TabsClosable with set value =
        this.PushAttr(TabsClosable value)
        
    member this.UsesScrollButtons with set value =
        this.PushAttr(UsesScrollButtons value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable tabBar: TabBar.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<TabBar.SignalMask> 0
    
    // binding guards:
    let mutable lastCurrentIndex = -1
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.TabBar
        with get() = tabBar
        and set value =
            // assign to base
            this.Widget <- value
            tabBar <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "TabBar.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "TabBar.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            tabBar.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyTabBarAttr attr =
            match attr with
            | AutoHide value ->
                tabBar.SetAutoHide(value)
            | ChangeCurrentOnDrag value ->
                tabBar.SetChangeCurrentOnDrag(value)
            | CurrentIndex index ->
                if index <> lastCurrentIndex then
                    lastCurrentIndex <- index
                    tabBar.SetCurrentIndex(index)
            | DocumentMode value ->
                tabBar.SetDocumentMode(value)
            | DrawBase value ->
                tabBar.SetDrawBase(value)
            | ElideMode mode ->
                tabBar.SetElideMode(mode.QtValue)
            | Expanding value ->
                tabBar.SetExpanding(value)
            | IconSize size ->
                tabBar.SetIconSize(size.QtValue)
            | Movable value ->
                tabBar.SetMovable(value)
            | SelectionBehaviorOnRemove behavior ->
                tabBar.SetSelectionBehaviorOnRemove(behavior.QtValue)
            | Shape shape ->
                tabBar.SetShape(shape.QtValue)
            | TabsClosable value ->
                tabBar.SetTabsClosable(value)
            | UsesScrollButtons value ->
                tabBar.SetUsesScrollButtons(value)
                
    interface TabBar.SignalHandler with
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
        // TabBar =========================
        member this.CurrentChanged index =
            lastCurrentIndex <- index
            signalDispatch (CurrentChanged index)
        member this.TabBarClicked index =
            signalDispatch (TabBarClicked index)
        member this.TabBarDoubleClicked index =
            signalDispatch (TabBarDoubleClicked index)
        member this.TabCloseRequested index =
            signalDispatch (TabCloseRequested index)
        member this.TabMoved(fromIndex, toIndex) =
            signalDispatch (TabMoved (fromIndex, toIndex))
            
    interface IDisposable with
        member this.Dispose() =
            tabBar.Dispose()
            
type internal TabItemInternal =
    | TextOnly of text: string
    | WithIcon of icon: Icon * text: string

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let tabBar = TabBar.Create(this)
    do
        this.TabBar <- tabBar
    
    member this.Refill(items: TabItemInternal list) =
        tabBar.RemoveAllTabs()
        for item in items do
            match item with
            | TextOnly text ->
                tabBar.AddTab(text) |> ignore
            | WithIcon(icon, text) ->
                tabBar.AddTab(icon.Handle, text) |> ignore
                
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: TabBar.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: TabBar.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type TabItem private(value: TabItemInternal) =
    member val internal Value = value
    new(text: string) =
        TabItem(TextOnly text)
    new(icon: Icon, text: string) =
        TabItem(WithIcon(icon, text))

type TabBar<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>

    // page label doubles as string dependency key
    // probably need to make that more apparent ...
    member val Items: TabItem list = [] with get, set
    
    member private this.MigrateContent(leftTabBar: TabBar<'msg>) =
        let leftValues =
            leftTabBar.Items
            |> List.map (_.Value)
        let rightValues =
            this.Items
            |> List.map (_.Value)
        if leftValues <> rightValues then
            this.model.Refill(rightValues)
        else
            ()
            
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            this.model.Refill(this.Items |> List.map (_.Value))
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> TabBar<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent(left')
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.TabBar
            
        override this.ContentKey =
            this.model.TabBar
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
