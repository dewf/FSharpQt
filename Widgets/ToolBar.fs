module FSharpQt.Widgets.ToolBar

open FSharpQt.Attrs
open FSharpQt.BuilderNode
open System
open FSharpQt.MiscTypes
open Microsoft.FSharp.Core
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Extensions

type private Signal =
    | ActionTriggered of action: ActionProxy
    | AllowedAreasChanged of allowed: Set<ToolBarArea>
    | IconSizeChanged of size: Size
    | MovableChanged of value: bool
    | OrientationChanged of orient: Orientation
    | ToolButtonStyleChanged of style: ToolButtonStyle
    | TopLevelChanged of topLevel: bool
    | VisibilityChanged of visible: bool
    
type internal Attr =
    | AllowedAreas of areas: Set<ToolBarArea> // must be set internally, outer setter can be seq 
    | Floatable of floatable: bool
    | IconSize of size: Size
    | Movable of value: bool
    | Orientation of value: Orientation
    | ToolButtonStyle of style: ToolButtonStyle
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
            | AllowedAreas _ -> "toolbar:allowedareas"
            | Floatable _ -> "toolbar:floatable"
            | IconSize _ -> "toolbar:iconsize"
            | Movable _ -> "toolbar:movable"
            | Orientation _ -> "toolbar:orienation"
            | ToolButtonStyle _ -> "toolbar:toolbuttonstyle"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyToolBarAttr(this)
            | _ ->
                printfn "warning: ToolBar.Attr couldn't ApplyTo() unknown target type [%A]" target

and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyToolBarAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onActionTriggered: (ActionProxy -> 'msg) option = None
    let mutable onAllowedAreasChanged: (Set<ToolBarArea> -> 'msg) option = None
    let mutable onIconSizeChanged: (Size -> 'msg) option = None
    let mutable onMovableChanged: (bool -> 'msg) option = None
    let mutable onOrientationChanged: (Orientation -> 'msg) option = None
    let mutable onToolButtonStyleChanged: (ToolButtonStyle -> 'msg) option = None
    let mutable onTopLevelChanged: (bool -> 'msg) option = None
    let mutable onVisibilityChanged: (bool -> 'msg) option = None
    
    member internal this.SignalMask = enum<ToolBar.SignalMask> (int this._signalMask)
    
    member this.OnActionTriggered with set value =
        onActionTriggered <- Some value
        this.AddSignal(int ToolBar.SignalMask.ActionTriggered)
        
    member this.OnAllowedAreasChanged with set value =
        onAllowedAreasChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.AllowedAreasChanged)
        
    member this.OnIconSizeChanged with set value =
        onIconSizeChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.IconSizeChanged)
        
    member this.OnMovableChanged with set value =
        onMovableChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.MovableChanged)
        
    member this.OnOrientationChanged with set value =
        onOrientationChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.OrientationChanged)
        
    member this.OnToolButtonStyleChanged with set value =
        onToolButtonStyleChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.ToolButtonStyleChanged)
        
    member this.OnTopLevelChanged with set value =
        onTopLevelChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.TopLevelChanged)
        
    member this.OnVisibilityChanged with set value =
        onVisibilityChanged <- Some value
        this.AddSignal(int ToolBar.SignalMask.VisibilityChanged)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | ActionTriggered action ->
                onActionTriggered
                |> Option.map (fun f -> f action)
            | AllowedAreasChanged allowed ->
                onAllowedAreasChanged
                |> Option.map (fun f -> f allowed)
            | IconSizeChanged size ->
                onIconSizeChanged
                |> Option.map (fun f -> f size)
            | MovableChanged value ->
                onMovableChanged
                |> Option.map (fun f -> f value)
            | OrientationChanged orient ->
                onOrientationChanged
                |> Option.map (fun f -> f orient)
            | ToolButtonStyleChanged style ->
                onToolButtonStyleChanged
                |> Option.map (fun f -> f style)
            | TopLevelChanged topLevel ->
                onTopLevelChanged
                |> Option.map (fun f -> f topLevel)
            | VisibilityChanged visible ->
                onVisibilityChanged
                |> Option.map (fun f -> f visible)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.AllowedAreas with set value =
        this.PushAttr(value |> Set.ofSeq |> AllowedAreas)

    member this.Floatable with set value =
        this.PushAttr(Floatable value)

    member this.IconSize with set value =
        this.PushAttr(IconSize value)

    member this.Movable with set value =
        this.PushAttr(Movable value)

    member this.Orientation with set value =
        this.PushAttr(Orientation value)

    member this.ToolButtonStyle with set value =
        this.PushAttr(ToolButtonStyle value)
    
// [<RequireQualifiedAccess>]
// type internal ItemKey<'msg> =
//     | ActionItem of key: ContentKey
//     | WidgetItem of key: ContentKey
//     | Separator
//     | Space
//     | Nothing

type internal InternalItem<'msg> =
    | ActionItem of node: IActionNode<'msg>
    | WidgetItem of node: IWidgetNode<'msg>
    | Separator
    | ExpandingSpace
    | Nothing
    
type ToolBarItem<'msg> internal(item: InternalItem<'msg>) =
    new(node: IActionNode<'msg>) =
        ToolBarItem(InternalItem.ActionItem node)
    new(node: IWidgetNode<'msg>) =
        ToolBarItem(InternalItem.WidgetItem node)
    new(?separator: bool, ?expandingSpace: bool) =
        let item =
            match defaultArg separator false with
            | true -> InternalItem.Separator
            | false ->
                match defaultArg expandingSpace false with
                | true -> InternalItem.ExpandingSpace
                | false -> InternalItem.Nothing
        ToolBarItem(item)
    member internal this.MaybeNode =
        match item with
        | ActionItem node -> Some (node :> IBuilderNode<'msg>)
        | WidgetItem node -> Some node
        | Separator -> None
        | ExpandingSpace -> None
        | Nothing -> None
    member internal this.InternalKey: ContentKey =
        match item with
        | ActionItem node -> node.ContentKey
        | WidgetItem node -> node.ContentKey
        | Separator -> Separator
        | ExpandingSpace -> ExpandingSpace
        | Nothing -> Nothing
    member internal this.AddTo (toolBar: ToolBar.Handle) =
        match item with
        | ActionItem node ->
            toolBar.AddAction(node.Action)
        | WidgetItem node ->
            toolBar.AddWidget(node.Widget)
        | Separator ->
            toolBar.AddSeparator()
            |> ignore // we don't do anything with the returned action - hopefully Qt owns it and we're not leaking?
        | ExpandingSpace ->
            let w = Widget.CreateNoHandler()
            w.SetSizePolicy(Expanding.QtValue, Expanding.QtValue)
            toolBar.AddWidget(w)
        | Nothing ->
            ()

type Separator<'msg>() =
    inherit ToolBarItem<'msg>(InternalItem.Separator)
    
type ExpandingSpace<'msg>() =
    inherit ToolBarItem<'msg>(InternalItem.ExpandingSpace)
    
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable toolBar: ToolBar.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<ToolBar.SignalMask> 0
    
    // binding guards
    let mutable lastAllowedAreas = ToolBarArea.AllToolBarAreas
    let mutable lastIconSize = Size.Invalid
    let mutable lastMovable = true
    let mutable lastOrientation = Horizontal
    let mutable lastToolButtonStyle = ToolButtonIconOnly
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.ToolBar
        with get() = toolBar
        and set value =
            // assign to base
            this.Widget <- value
            toolBar <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "ToolBar.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "ToolBar.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            toolBar.SetSignalMask(value)
            currentMask <- value
    
    interface AttrTarget with
        member this.ApplyToolBarAttr attr =
            match attr with
            | AllowedAreas areas ->
                if areas <> lastAllowedAreas then
                    lastAllowedAreas <- areas
                    toolBar.SetAllowedAreas(ToolBarArea.QtSetFrom areas)
            | Floatable floatable ->
                toolBar.SetFloatable(floatable)
            | IconSize size ->
                if size <> lastIconSize then
                    lastIconSize <- size
                    toolBar.SetIconSize(size.QtValue)
            | Movable value ->
                if value <> lastMovable then
                    lastMovable <- value
                    toolBar.SetMovable(value)
            | Orientation value ->
                if value <> lastOrientation then
                    lastOrientation <- value
                    toolBar.SetOrientation(value.QtValue)
            | ToolButtonStyle style ->
                if style <> lastToolButtonStyle then
                    lastToolButtonStyle <- style
                    toolBar.SetToolButtonStyle(style.QtValue)
                    
    interface ToolBar.SignalHandler with
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
        // ToolBar ========================
        member this.ActionTriggered action =
            signalDispatch (ActionProxy(action) |> ActionTriggered)
        member this.AllowedAreasChanged allowed =
            let allowed' = ToolBarArea.SetFrom allowed
            lastAllowedAreas <- allowed'
            signalDispatch (AllowedAreasChanged allowed')
        member this.IconSizeChanged size =
            let size' = Size.From size
            lastIconSize <- size'
            signalDispatch (size' |> IconSizeChanged)
        member this.MovableChanged movable =
            lastMovable <- movable
            signalDispatch (MovableChanged movable)
        member this.OrientationChanged orient =
            let orient' = Orientation.From orient
            lastOrientation <- orient'
            signalDispatch (orient' |> OrientationChanged)
        member this.ToolButtonStyleChanged style =
            let style' = ToolButtonStyle.From style
            lastToolButtonStyle <- style'
            signalDispatch (style' |> ToolButtonStyleChanged)
        member this.TopLevelChanged topLevel =
            signalDispatch (TopLevelChanged topLevel)
        member this.VisibilityChanged visible =
            signalDispatch (VisibilityChanged visible)
            
    interface IDisposable with
        member this.Dispose() =
            toolBar.Dispose()
    

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let toolBar = ToolBar.Create(this)
    do
        this.ToolBar <- toolBar
            
    member this.Refill (items: ToolBarItem<'msg> list) =
        toolBar.Clear()
        for item in items do
            item.AddTo toolBar

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: ToolBar.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: ToolBar.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type ToolBar<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    member val Items: ToolBarItem<'msg> list = [] with get, set
    
    member private this.MigrateContent(leftToolBar: ToolBar<'msg>) =
        let leftContents =
            leftToolBar.Items
            |> List.map (_.InternalKey)
        let thisContents =
            this.Items
            |> List.map (_.InternalKey)
        if leftContents <> thisContents then
            this.model.Refill this.Items
        else
            ()
                
    interface IToolBarNode<'msg> with
        override this.Dependencies =
            this.Items
            |> List.zipWithIndex
            |> List.choose (fun (i, item) ->
                item.MaybeNode
                |> Option.map (fun node -> IntKey i, node))

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            this.model.Refill this.Items

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> ToolBar<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            // instead of complicated .MigrateContent,
            // why don't we just see if depsChanges is all Unchanged?
            // I guess .MigrateContent was originally from the VBox in the early days ...
            this.MigrateContent(left')

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.ToolBar =
            this.model.ToolBar
            
        override this.ContentKey =
            this.model.ToolBar
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
