module FSharpQt.Widgets.TreeView

open FSharpQt.Attrs
open FSharpQt.BuilderNode
open System
open FSharpQt.Reactor
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type private Signal =
    | Collapsed of index: ModelIndexProxy
    | Expanded of index: ModelIndexProxy
    
type internal Attr =
    | AllColumnsShowFocus of enabled: bool
    | Animated of enabled: bool
    | AutoExpandDelay of delay: int
    | ExpandsOnDoubleClick of enabled: bool
    | HeaderHidden of hidden: bool
    | Indentation of indent: int
    | ItemsExpandable of enabled: bool
    | RootIsDecorated of show: bool
    | SortingEnabled of enabled: bool
    | UniformRowHeights of uniform: bool
    | WordWrap of enabled: bool
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
            | AllColumnsShowFocus _ ->"treeview:allcolumnsshowfocus"
            | Animated _ ->"treeview:animated"
            | AutoExpandDelay _ ->"treeview:autoexpanddelay"
            | ExpandsOnDoubleClick _ ->"treeview:expandsondoubleclick"
            | HeaderHidden _ ->"treeview:headerhidden"
            | Indentation _ ->"treeview:indentation"
            | ItemsExpandable _ ->"treeview:itemsexpandable"
            | RootIsDecorated _ ->"treeview:rootisdecorated"
            | SortingEnabled _ ->"treeview:sortingenabled"
            | UniformRowHeights _ ->"treeview:uniformrowheights"
            | WordWrap _ ->"treeview:wordwrap"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyTreeViewAttr(this)
            | _ ->
                printfn "warning: TreeView.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractItemView.AttrTarget
        abstract member ApplyTreeViewAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit AbstractItemView.Props<'msg>()
    
    let mutable onCollapsed: (ModelIndexProxy -> 'msg) option = None
    let mutable onExpanded: (ModelIndexProxy -> 'msg) option = None
    
    member internal this.SignalMask = enum<TreeView.SignalMask> (int this._signalMask)
    
    member this.OnCollapsed with set value =
        onCollapsed <- Some value
        this.AddSignal(int TreeView.SignalMask.Collapsed)
        
    member this.OnExpanded with set value =
        onExpanded <- Some value
        this.AddSignal(int TreeView.SignalMask.Expanded)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Collapsed index ->
                onCollapsed
                |> Option.map (fun f -> f index)
            | Expanded index ->
                onExpanded
                |> Option.map (fun f -> f index)
        // prepend to parent list
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.AllColumnsShowFocus with set value =
        this.PushAttr(AllColumnsShowFocus value)
        
    member this.Animated with set value =
        this.PushAttr(Animated value)
    
    member this.AutoExpandDelay with set value =
        this.PushAttr(AutoExpandDelay value)
    
    member this.ExpandsOnDoubleClick with set value =
        this.PushAttr(ExpandsOnDoubleClick value)
    
    member this.HeaderHidden with set value =
        this.PushAttr(HeaderHidden value)
    
    member this.Indentation with set value =
        this.PushAttr(Indentation value)
    
    member this.ItemsExpandable with set value =
        this.PushAttr(ItemsExpandable value)
    
    member this.RootIsDecorated with set value =
        this.PushAttr(RootIsDecorated value)
    
    member this.SortingEnabled with set value =
        this.PushAttr(SortingEnabled value)
    
    member this.UniformRowHeights with set value =
        this.PushAttr(UniformRowHeights value)
    
    member this.WordWrap with set value =
        this.PushAttr(WordWrap value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractItemView.ModelCore<'msg>(dispatch)
    let mutable treeView: TreeView.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<TreeView.SignalMask> 0
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.TreeView
        with get() = treeView
        and set value =
            this.AbstractItemView <- value
            treeView <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "TreeView.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "TreeView.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            treeView.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyTreeViewAttr attr =
            match attr with
            | AllColumnsShowFocus enabled ->
                treeView.SetAllColumnsShowFocus(enabled)
            | Animated enabled ->
                treeView.SetAnimated(enabled)
            | AutoExpandDelay delay ->
                treeView.SetAutoExpandDelay(delay)
            | ExpandsOnDoubleClick enabled ->
                treeView.SetExpandsOnDoubleClick(enabled)
            | HeaderHidden hidden ->
                treeView.SetHeaderHidden(hidden)
            | Indentation indent ->
                treeView.SetIndentation(indent)
            | ItemsExpandable enabled ->
                treeView.SetItemsExpandable(enabled)
            | RootIsDecorated show ->
                treeView.SetRootIsDecorated(show)
            | SortingEnabled enabled ->
                treeView.SetSortingEnabled(enabled)
            | UniformRowHeights uniform ->
                treeView.SetUniformRowHeights(uniform)
            | WordWrap enabled ->
                treeView.SetWordWrap(enabled)
                
    interface TreeView.SignalHandler with
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
        // Frame ==========================
        // (none)
        // AbstractScrollArea =============
        // (none)
        // AbstractItemView ===============
        member this.Activated index =
            (this :> AbstractItemView.SignalHandler).Activated index
        member this.Clicked index =
            (this :> AbstractItemView.SignalHandler).Clicked index
        member this.DoubleClicked index =
            (this :> AbstractItemView.SignalHandler).DoubleClicked index
        member this.Entered index =
            (this :> AbstractItemView.SignalHandler).Entered index
        member this.IconSizeChanged size =
            (this :> AbstractItemView.SignalHandler).IconSizeChanged size
        member this.Pressed index =
            (this :> AbstractItemView.SignalHandler).Pressed index
        member this.ViewportEntered() =
            (this :> AbstractItemView.SignalHandler).ViewportEntered()
        // TreeView =======================
        member this.Collapsed index =
            signalDispatch (ModelIndexProxy(index) |> Collapsed)
        member this.Expanded index =
            signalDispatch (ModelIndexProxy(index) |> Expanded)
            
    interface IDisposable with
        member this.Dispose() =
            treeView.Dispose()
            
type private ItemDelegateLocation =
    | LocationAll
    | LocationRow of row: int
    | LocationCol of col: int
    // not yet for ModelIndex
            
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let treeView = TreeView.Create(this)
    do
        this.TreeView <- treeView
        
    member this.AddQtModel (model: AbstractItemModel.Handle) =
        treeView.SetModel(model)
        
    member this.RemoveQtModel () =
        // well if it gets deleted (as a dependency), won't that delete from the view automatically?
        ()
        
    member this.AddItemDelegate (itemDelegate: AbstractItemDelegate.Handle) (loc: ItemDelegateLocation) =
        match loc with
        | LocationAll ->
            treeView.SetItemDelegate(itemDelegate)
        | LocationRow row ->
            treeView.SetItemDelegateForRow(row, itemDelegate)
        | LocationCol col ->
            treeView.SetItemDelegateForColumn(col, itemDelegate)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: TreeView.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: TreeView.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type TreeViewBinding internal(handle: TreeView.Handle) =
    inherit AbstractItemView.AbstractItemViewBinding(handle)
    member this.ResizeColumnToContents(column: int) =
        handle.ResizeColumnToContents(column)
        
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? TreeViewBinding as treeView) ->
        treeView
    | _ ->
        failwith "TreeView.bindNode fail"
    
type TreeView<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeTreeModel: IModelNode<'msg> option = None
    member this.TreeModel with set value = maybeTreeModel <- Some value
    
    let mutable itemDelegates: (IAbstractItemDelegateNode<'msg> * ItemDelegateLocation) list = []
    member this.SetItemDelegateForColumn(column: int, itemDelegate: IAbstractItemDelegateNode<'msg>) =
        itemDelegates <- (itemDelegate, LocationCol column) :: itemDelegates
        this
            
    member this.MigrateDeps (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "model") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddQtModel(maybeTreeModel.Value.QtModel)
            | Removed ->
                this.model.RemoveQtModel()
            | Swapped ->
                this.model.RemoveQtModel()
                this.model.AddQtModel(maybeTreeModel.Value.QtModel)
        | None ->
            // neither side had one
            ()
    
    interface IWidgetNode<'msg> with
        override this.Dependencies =
            let modelList =
                maybeTreeModel
                |> Option.map (fun node -> StrKey "model", node :> IBuilderNode<'msg>)
                |> Option.toList
            let itemDelegates =
                itemDelegates
                |> List.mapi (fun i (node, _) -> StrIntKey("item-delegate", i), node :> IBuilderNode<'msg>)
            modelList @ itemDelegates

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeTreeModel
            |> Option.iter (fun node ->
                this.model.AddQtModel(node.QtModel))
            itemDelegates
            |> List.iter (fun (node, loc) ->
                this.model.AddItemDelegate node.AbstractItemDelegate loc)

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> TreeView<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateDeps(depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.TreeView
            
        override this.ContentKey =
            this.model.TreeView
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, TreeViewBinding(this.model.TreeView))

let cmdResizeAllColumnsToContents (name: string) (numColumns: int) =
    Cmd.ViewExec (fun bindings ->
        let treeView = bindNode name bindings
        for i in 0 .. numColumns - 1 do
            treeView.ResizeColumnToContents(i)
        None)
