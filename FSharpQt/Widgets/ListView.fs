module FSharpQt.Widgets.ListView

open FSharpQt.Attrs
open FSharpQt.BuilderNode
open System
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type private Signal =
    | IndexesMoved of indexes: ModelIndexProxy list

type Movement =
    | Static
    | Free
    | Snap
with
    member this.QtValue =
        match this with
        | Static -> ListView.Movement.Static
        | Free -> ListView.Movement.Free
        | Snap -> ListView.Movement.Snap
    
type Flow =
    | LeftToRight
    | TopToBottom
with
    member this.QtValue =
        match this with
        | LeftToRight -> ListView.Flow.LeftToRight
        | TopToBottom -> ListView.Flow.TopToBottom
    
type ResizeMode =
    | Fixed
    | Adjust
with
    member this.QtValue =
        match this with
        | Fixed -> ListView.ResizeMode.Fixed
        | Adjust -> ListView.ResizeMode.Adjust
    
type LayoutMode =
    | SinglePass
    | Batched
with
    member this.QtValue =
        match this with
        | SinglePass -> ListView.LayoutMode.SinglePass
        | Batched -> ListView.LayoutMode.Batched
    
type ViewMode =
    | ListMode
    | IconMode
with
    member this.QtValue =
        match this with
        | ListMode -> ListView.ViewMode.ListMode
        | IconMode -> ListView.ViewMode.IconMode
        
type internal Attr =
    | BatchSize of size: int
    | Flow of flow: Flow
    | GridSize of size: Size
    | Wrapping of state: bool
    | ItemAlignment of align: Alignment
    | LayoutMode of mode: LayoutMode
    | ModelColumn of column: int
    | Movement of value: Movement
    | ResizeMode of mode: ResizeMode
    | SelectionRectVisible of visible: bool
    | Spacing of spacing: int
    | UniformItemSizes of state: bool
    | ViewMode of mode: ViewMode
    | WordWrap of state: bool
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
            | BatchSize _ -> "listview:batchsize"
            | Flow _ -> "listview:flow"
            | GridSize _ -> "listview:gridsize"
            | Wrapping _ -> "listview:wrapping"
            | ItemAlignment _ -> "listview:itemalignment"
            | LayoutMode _ -> "listview:layoutmode"
            | ModelColumn _ -> "listview:modelcolumn"
            | Movement _ -> "listview:movement"
            | ResizeMode _ -> "listview:resizemode"
            | SelectionRectVisible _ -> "listview:selectionrectvisible"
            | Spacing _ -> "listview:spacing"
            | UniformItemSizes _ -> "listview:uniformitemsizes"
            | ViewMode _ -> "listview:viewmode"
            | WordWrap _ -> "listview:wordwrap"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyListViewAttr(this)
            | _ ->
                printfn "warning: ListView.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractItemView.AttrTarget
        abstract member ApplyListViewAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit AbstractItemView.Props<'msg>()
    
    let mutable onIndexesMoved: (ModelIndexProxy list -> 'msg) option = None
    
    member internal this.SignalMask = enum<ListView.SignalMask> (int this._signalMask)
    
    member this.OnIndexesMoved with set value =
        onIndexesMoved <- Some value
        this.AddSignal(int ListView.SignalMask.IndexesMoved)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | IndexesMoved indexes ->
                onIndexesMoved
                |> Option.map (fun f -> f indexes)
        // prepend to parent list
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.BatchSize with set value =
        this.PushAttr(BatchSize value)

    member this.Flow with set value =
        this.PushAttr(Flow value)

    member this.GridSize with set value =
        this.PushAttr(GridSize value)

    member this.Wrapping with set value =
        this.PushAttr(Wrapping value)

    member this.ItemAlignment with set value =
        this.PushAttr(ItemAlignment value)

    member this.LayoutMode with set value =
        this.PushAttr(LayoutMode value)

    member this.ModelColumn with set value =
        this.PushAttr(ModelColumn value)

    member this.Movement with set value =
        this.PushAttr(Movement value)

    member this.ResizeMode with set value =
        this.PushAttr(ResizeMode value)

    member this.SelectionRectVisible with set value =
        this.PushAttr(SelectionRectVisible value)

    member this.Spacing with set value =
        this.PushAttr(Spacing value)

    member this.UniformItemSizes with set value =
        this.PushAttr(UniformItemSizes value)

    member this.ViewMode with set value =
        this.PushAttr(ViewMode value)

    member this.WordWrap with set value =
        this.PushAttr(WordWrap value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractItemView.ModelCore<'msg>(dispatch)
    let mutable listView: ListView.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<ListView.SignalMask> 0
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.ListView
        with get() = listView
        and set value =
            this.AbstractItemView <- value
            listView <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "ListView.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "ListView.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            listView.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyListViewAttr attr =
            match attr with
            | BatchSize size ->
                listView.SetBatchSize(size)
            | Flow flow ->
                listView.SetFlow(flow.QtValue)
            | GridSize size ->
                listView.SetGridSize(size.QtValue)
            | Wrapping state ->
                listView.SetWrapping(state)
            | ItemAlignment align ->
                listView.SetItemAlignment(align.QtValue)
            | LayoutMode mode ->
                listView.SetLayoutMode(mode.QtValue)
            | ModelColumn column ->
                listView.SetModelColumn(column)
            | Movement value ->
                listView.SetMovement(value.QtValue)
            | ResizeMode mode ->
                listView.SetResizeMode(mode.QtValue)
            | SelectionRectVisible visible ->
                listView.SetSelectionRectVisible(visible)
            | Spacing spacing ->
                listView.SetSpacing(spacing)
            | UniformItemSizes state ->
                listView.SetUniformItemSizes(state)
            | ViewMode mode ->
                listView.SetViewMode(mode.QtValue)
            | WordWrap state ->
                listView.SetWordWrap(state)
                
    interface ListView.SignalHandler with
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
        // ListView =======================
        member this.IndexesMoved indexes =
            let indexes' =
                indexes
                |> Array.map (fun index -> new ModelIndexProxy(index))
                |> Array.toList
            signalDispatch (IndexesMoved indexes')

    interface IDisposable with
        member this.Dispose() =
            listView.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let listView = ListView.Create(this)
    do
        this.ListView <- listView
        
    member this.AddQtModel (model: AbstractItemModel.Handle) =
        listView.SetModel(model)
        
    member this.RemoveQtModel () =
        // well if it gets deleted (as a dependency), won't that delete from the listView automatically?
        ()
    
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: ListView.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: ListView.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type ListView<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeListModel: IModelNode<'msg> option = None
    member this.ListModel with set value = maybeListModel <- Some value
            
    member this.MigrateDeps (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "model") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddQtModel(maybeListModel.Value.QtModel)
            | Removed ->
                this.model.RemoveQtModel()
            | Swapped ->
                this.model.RemoveQtModel()
                this.model.AddQtModel(maybeListModel.Value.QtModel)
        | None ->
            // neither side had one
            ()
    
    interface IWidgetNode<'msg> with
        override this.Dependencies =
            maybeListModel
            |> Option.map (fun node -> StrKey "model", node :> IBuilderNode<'msg>)
            |> Option.toList

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeListModel
            |> Option.iter (fun node ->
                this.model.AddQtModel(node.QtModel))

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> ListView<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateDeps(depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.ListView
            
        override this.ContentKey =
            this.model.ListView
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
