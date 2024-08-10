module FSharpQt.Models.CustomSortFilterProxyModel

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.Reactor
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type IFilterDelegate =
    interface
        // internal use only
        abstract member SetInterior: SortFilterProxyModel.Interior -> unit
        // actual
        abstract member FilterAcceptsColumn: int -> ModelIndexProxy -> bool
        abstract member FilterAcceptsRow: int -> ModelIndexProxy -> bool
        abstract member LessThan: ModelIndexProxy -> ModelIndexProxy -> bool
    end

[<AbstractClass>]    
type FilterDelegateBase() =
    let mutable interior: SortFilterProxyModel.Interior = null

    // protected methods for outside use:
    member this.SourceModel =
        AbstractItemModelProxy(interior.SourceModel())
        
    abstract member FilterAcceptsColumn: int -> ModelIndexProxy -> bool
    default this.FilterAcceptsColumn sourceColumn sourceParent =
        true
        
    abstract member FilterAcceptsRow: int -> ModelIndexProxy -> bool
    default this.FilterAcceptsRow sourceRow sourceParent =
        true
        
    abstract member LessThan: ModelIndexProxy -> ModelIndexProxy -> bool
    default this.LessThan sourceLeft sourceRight =
        failwith "CustomSortFilterProxyModel.FilterDelegateBase.LessThan: must be implemented"
        
    interface IFilterDelegate with
        override this.SetInterior value =
            interior <- value
        override this.FilterAcceptsColumn sourceColumn sourceParent = this.FilterAcceptsColumn sourceColumn sourceParent
        override this.FilterAcceptsRow sourceRow sourceParent = this.FilterAcceptsRow sourceRow sourceParent
        override this.LessThan sourceLeft sourceRight = this.LessThan sourceLeft sourceRight
            
type FilterDelegateMethod =
    | FilterAcceptsColumn
    | FilterAcceptsRow
    | LessThan
with
    static member QtSetFrom (methods: FilterDelegateMethod seq) =
        (enum<SortFilterProxyModel.MethodMask> 0, methods)
        ||> Seq.fold (fun acc method ->
            let flag =
                match method with
                | FilterAcceptsColumn -> SortFilterProxyModel.MethodMask.FilterAcceptsColumn
                | FilterAcceptsRow -> SortFilterProxyModel.MethodMask.FilterAcceptsRow
                | LessThan -> SortFilterProxyModel.MethodMask.LessThan
            acc ||| flag)
        
type private Model<'msg>(dispatch: 'msg -> unit, filterDelegate: IFilterDelegate, methods: FilterDelegateMethod seq) as this =
    inherit SortFilterProxyModel.ModelCore<'msg>(dispatch)
    let mutable filterDelegate = filterDelegate
    let interior =
        let methodMask =
            methods |> FilterDelegateMethod.QtSetFrom
        SortFilterProxyModel.CreateSubclassed(this, this, methodMask)
    do
        this.SortFilterProxyModel <- interior
        filterDelegate.SetInterior interior // users of eg FilterDelegateBase will need access to various methods
        
    // expose interior (subclassed 'this') for binding
    member this.Interior = interior
    
    member this.FilterDelegate with set value =
        filterDelegate <- value
        filterDelegate.SetInterior interior
        
    interface SortFilterProxyModel.MethodDelegate with
        member this.FilterAcceptsColumn(sourceColumn: int, sourceParent: ModelIndex.Handle) =
            filterDelegate.FilterAcceptsColumn sourceColumn (new ModelIndexProxy(sourceParent))
        member this.FilterAcceptsRow(sourceRow: int, sourceParent: ModelIndex.Handle) =
            filterDelegate.FilterAcceptsRow sourceRow (new ModelIndexProxy(sourceParent))
        member this.LessThan(sourceLeft: ModelIndex.Handle, sourceRight: ModelIndex.Handle) =
            filterDelegate.LessThan (new ModelIndexProxy(sourceLeft)) (new ModelIndexProxy(sourceRight))
    
    member this.AddSourceModel (model: AbstractItemModel.Handle) =
        interior.SetSourceModel(model)
        
    member this.RemoveSourceModel () =
        // well if it gets deleted (as a dependency), won't that automagically remove it from us?
        ()

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: SortFilterProxyModel.SignalMask) (filterDelegate: IFilterDelegate) (methods: FilterDelegateMethod seq) =
    let model = new Model<'msg>(dispatch, filterDelegate, methods)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: SortFilterProxyModel.SignalMask) (filterDelegate: IFilterDelegate) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model.FilterDelegate <- filterDelegate
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type CustomSortFilterProxyModelBinding internal(interior: SortFilterProxyModel.Interior) =
    inherit SortFilterProxyModel.SortFilterProxyModelBinding(interior)
    member this.InvalidateColumnsFilter() =
        interior.InvalidateColumnsFilter()
    member this.InvalidateRowsFilter() =
        interior.InvalidateRowsFilter()
    member this.InvalidateFilter() =
        interior.InvalidateFilter()
        
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? CustomSortFilterProxyModelBinding as sortFilterProxyModel) ->
        sortFilterProxyModel
    | _ ->
        failwith "CustomSortFilterProxyModel.bindNode fail"

type CustomSortFilterProxyModel<'msg>(filterDelegate: IFilterDelegate, delegateMethods: FilterDelegateMethod seq) =
    inherit SortFilterProxyModel.Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeSourceModel: IModelNode<'msg> option = None
    member this.SourceModel with set value = maybeSourceModel <- Some value
    
    member this.MigrateDeps (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "source") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddSourceModel(maybeSourceModel.Value.QtModel)
            | Removed ->
                this.model.RemoveSourceModel()
            | Swapped ->
                this.model.RemoveSourceModel()
                this.model.AddSourceModel(maybeSourceModel.Value.QtModel)
        | None ->
            // neither side had one
            ()
    
    interface IModelNode<'msg> with
        override this.Dependencies =
            maybeSourceModel
            |> Option.map (fun node -> StrKey "source", node :> IBuilderNode<'msg>)
            |> Option.toList

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask filterDelegate delegateMethods
            
        override this.AttachDeps () =
            maybeSourceModel
            |> Option.iter (fun node ->
                this.model.AddSourceModel(node.QtModel))

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> CustomSortFilterProxyModel<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask filterDelegate
            this.MigrateDeps(depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.QtModel =
            this.model.SortFilterProxyModel
            
        override this.ContentKey =
            this.model.SortFilterProxyModel
            
        override this.Attachments =
            this.Attachments
            
        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, CustomSortFilterProxyModelBinding(this.model.Interior))
            
let cmdInvalidateColumnsFilter name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! proxyModel = bindNode name
            proxyModel.InvalidateColumnsFilter()
        })
    
let cmdInvalidateRowsFilter name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! proxyModel = bindNode name
            proxyModel.InvalidateRowsFilter()
        })

let cmdInvalidateFilter name =
    Cmd.ViewExec (fun bindings ->
        viewexec bindings {
            let! proxyModel = bindNode name
            proxyModel.InvalidateFilter()
        })

let cmdMapToSource =
    SortFilterProxyModel.cmdMapToSource

let cmdSort =
    SortFilterProxyModel.cmdSort

let cmdSortWithOrder =
    SortFilterProxyModel.cmdSortWithOrder
