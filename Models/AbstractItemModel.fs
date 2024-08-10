module FSharpQt.Models.AbstractItemModel

open System
open FSharpQt.Attrs
open FSharpQt.MiscTypes
open FSharpQt.Reactor
open FSharpQt.Widgets
open Org.Whatever.MinimalQtForFSharp

type LayoutChangeHint =
    | NoLayoutChangeHint
    | VerticalSortHint
    | HorizontalSortHint
with
    static member internal From (qtValue: AbstractItemModel.LayoutChangeHint) =
        match qtValue with
        | AbstractItemModel.LayoutChangeHint.NoLayoutChangeHint -> NoLayoutChangeHint
        | AbstractItemModel.LayoutChangeHint.VerticalSortHint -> VerticalSortHint
        | AbstractItemModel.LayoutChangeHint.HorizontalSortHint -> HorizontalSortHint
        | _ -> failwith "LayoutChangeHint.From: unknown input value"
    member internal this.QtValue =
        match this with
        | NoLayoutChangeHint -> AbstractItemModel.LayoutChangeHint.NoLayoutChangeHint
        | VerticalSortHint -> AbstractItemModel.LayoutChangeHint.VerticalSortHint
        | HorizontalSortHint -> AbstractItemModel.LayoutChangeHint.HorizontalSortHint

type private Signal =
    | ColumnsAboutToBeInserted of parent: ModelIndexProxy * first: int * last: int
    | ColumnsAboutToBeMoved of sourceParent: ModelIndexProxy * sourceStart: int * sourceEnd: int * destinationParent: ModelIndexProxy * destinationColumn: int
    | ColumnsAboutToBeRemoved of parent: ModelIndexProxy * first: int * last: int
    | ColumnsInserted of parent: ModelIndexProxy * first: int * last: int
    | ColumnsMoved of sourceParent: ModelIndexProxy * sourceStart: int * sourceEnd: int * destinationParent: ModelIndexProxy * destinationColumn: int
    | ColumnsRemoved of parent: ModelIndexProxy * first: int * last: int
    | DataChanged of topLeft: ModelIndexProxy * bottomRight: ModelIndexProxy * roles: ItemDataRole list
    | HeaderDataChanged of orientation: Orientation * first: int * last: int
    | LayoutAboutToBeChanged of parents: PersistentModelIndexProxy list * hint: LayoutChangeHint
    | LayoutChanged of parents: PersistentModelIndexProxy list * hint: LayoutChangeHint
    | ModelAboutToBeReset
    | ModelReset
    | RowsAboutToBeInserted of parent: ModelIndexProxy * start: int * ``end``: int
    | RowsAboutToBeMoved of sourceParent: ModelIndexProxy * sourceStart: int * sourceEnd: int * destinationParent: ModelIndexProxy * destinationRow: int
    | RowsAboutToBeRemoved of parent: ModelIndexProxy * first: int * last: int
    | RowsInserted of parent: ModelIndexProxy * first: int * last: int
    | RowsMoved of sourceParent: ModelIndexProxy * sourceStart: int * sourceEnd: int * destinationParent: ModelIndexProxy * destinationRow: int
    | RowsRemoved of parent: ModelIndexProxy * first: int * last: int

// no attributes, but for consistency with inherited stuff:
type internal AttrTarget =
    interface
        inherit QObject.AttrTarget
        // abstract member ApplyAbstractItemModelAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit QObject.Props<'msg>()
    
    let mutable onColumnsAboutToBeInserted: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onColumnsAboutToBeMoved: (ModelIndexProxy * int * int * ModelIndexProxy * int -> 'msg) option = None
    let mutable onColumnsAboutToBeRemoved: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onColumnsInserted: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onColumnsMoved: (ModelIndexProxy * int * int * ModelIndexProxy * int -> 'msg) option = None
    let mutable onColumnsRemoved: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onDataChanged: (ModelIndexProxy * ModelIndexProxy * ItemDataRole list -> 'msg) option = None
    let mutable onHeaderDataChanged: (Orientation * int * int -> 'msg) option = None
    let mutable onLayoutAboutToBeChanged: (PersistentModelIndexProxy list * LayoutChangeHint -> 'msg) option = None
    let mutable onLayoutChanged: (PersistentModelIndexProxy list * LayoutChangeHint -> 'msg) option = None
    let mutable onModelAboutToBeReset: 'msg option = None
    let mutable onModelReset: 'msg option = None
    let mutable onRowsAboutToBeInserted: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onRowsAboutToBeMoved: (ModelIndexProxy * int * int * ModelIndexProxy * int -> 'msg) option = None
    let mutable onRowsAboutToBeRemoved: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onRowsInserted: (ModelIndexProxy * int * int -> 'msg) option = None
    let mutable onRowsMoved: (ModelIndexProxy * int * int * ModelIndexProxy * int -> 'msg) option = None
    let mutable onRowsRemoved: (ModelIndexProxy * int * int -> 'msg) option = None
    
    member internal this.SignalMask = enum<AbstractItemModel.SignalMask> (int this._signalMask)
    
    member this.ColumnsAboutToBeInserted with set value =
        onColumnsAboutToBeInserted <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ColumnsAboutToBeInserted)

    member this.ColumnsAboutToBeMoved with set value =
        onColumnsAboutToBeMoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ColumnsAboutToBeMoved)

    member this.ColumnsAboutToBeRemoved with set value =
        onColumnsAboutToBeRemoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ColumnsAboutToBeRemoved)

    member this.ColumnsInserted with set value =
        onColumnsInserted <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ColumnsInserted)

    member this.ColumnsMoved with set value =
        onColumnsMoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ColumnsMoved)

    member this.ColumnsRemoved with set value =
        onColumnsRemoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ColumnsRemoved)

    member this.DataChanged with set value =
        onDataChanged <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.DataChanged)

    member this.HeaderDataChanged with set value =
        onHeaderDataChanged <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.HeaderDataChanged)

    member this.LayoutAboutToBeChanged with set value =
        onLayoutAboutToBeChanged <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.LayoutAboutToBeChanged)

    member this.LayoutChanged with set value =
        onLayoutChanged <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.LayoutChanged)

    member this.ModelAboutToBeReset with set value =
        onModelAboutToBeReset <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ModelAboutToBeReset)

    member this.ModelReset with set value =
        onModelReset <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.ModelReset)

    member this.RowsAboutToBeInserted with set value =
        onRowsAboutToBeInserted <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.RowsAboutToBeInserted)

    member this.RowsAboutToBeMoved with set value =
        onRowsAboutToBeMoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.RowsAboutToBeMoved)

    member this.RowsAboutToBeRemoved with set value =
        onRowsAboutToBeRemoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.RowsAboutToBeRemoved)

    member this.RowsInserted with set value =
        onRowsInserted <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.RowsInserted)

    member this.RowsMoved with set value =
        onRowsMoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.RowsMoved)

    member this.RowsRemoved with set value =
        onRowsRemoved <- Some value
        this.AddSignal(int AbstractItemModel.SignalMask.RowsRemoved)

    member internal this.SignalMapList =
        let thisFunc = function
            | ColumnsAboutToBeInserted(parent, first, last) ->
                onColumnsAboutToBeInserted
                |> Option.map (fun f -> f (parent, first, last))
            | ColumnsAboutToBeMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn) ->
                onColumnsAboutToBeMoved
                |> Option.map (fun f -> f (sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn))
            | ColumnsAboutToBeRemoved(parent, first, last) ->
                onColumnsAboutToBeRemoved
                |> Option.map (fun f -> f (parent, first, last))
            | ColumnsInserted(parent, first, last) ->
                onColumnsInserted
                |> Option.map (fun f -> f (parent, first, last))
            | ColumnsMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn) ->
                onColumnsMoved
                |> Option.map (fun f -> f (sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn))
            | ColumnsRemoved(parent, first, last) ->
                onColumnsRemoved
                |> Option.map (fun f -> f (parent, first, last))
            | DataChanged(topLeft, bottomRight, roles) ->
                onDataChanged
                |> Option.map (fun f -> f (topLeft, bottomRight, roles))
            | HeaderDataChanged(orientation, first, last) ->
                onHeaderDataChanged
                |> Option.map (fun f -> f (orientation, first, last))
            | LayoutAboutToBeChanged(parents, hint) ->
                onLayoutAboutToBeChanged
                |> Option.map (fun f -> f (parents, hint))
            | LayoutChanged(parents, hint) ->
                onLayoutChanged
                |> Option.map (fun f -> f (parents, hint))
            | ModelAboutToBeReset ->
                onModelAboutToBeReset
            | ModelReset ->
                onModelReset
            | RowsAboutToBeInserted(parent, start, ``end``) ->
                onRowsAboutToBeInserted
                |> Option.map (fun f -> f (parent, start, ``end``))
            | RowsAboutToBeMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow) ->
                onRowsAboutToBeMoved
                |> Option.map (fun f -> f (sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow))
            | RowsAboutToBeRemoved(parent, first, last) ->
                onRowsAboutToBeRemoved
                |> Option.map (fun f -> f (parent, first, last))
            | RowsInserted(parent, first, last) ->
                onRowsInserted
                |> Option.map (fun f -> f (parent, first, last))
            | RowsMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow) ->
                onRowsMoved
                |> Option.map (fun f -> f (sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow))
            | RowsRemoved(parent, first, last) ->
                onRowsRemoved
                |> Option.map (fun f -> f (parent, first, last))
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit QObject.ModelCore<'msg>(dispatch)
    let mutable absItemModel: AbstractItemModel.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // let mutable currentMask = enum<AbstractItemModel.SignalMask> 0
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
    
    member this.AbstractItemModel
        with get() = absItemModel
        and set value =
            this.Object <- value
            absItemModel <- value

    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "AbstractItemModel.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractItemModel.ModelCore: signal map assignment didn't have a head element"

    // no signal mask setter, because abstract
    
    // "implemented" for inheritance purposes
    interface AttrTarget
    
    interface AbstractItemModel.SignalHandler with
        // Object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // AbstractItemModel ==============
        member this.ColumnsAboutToBeInserted (parent, first, last) =
            signalDispatch (ColumnsAboutToBeInserted (new ModelIndexProxy(parent), first, last))
        member this.ColumnsAboutToBeMoved (srcParent, srcStart, srcEnd, destParent, destColumn) =
            signalDispatch (ColumnsAboutToBeMoved (new ModelIndexProxy(srcParent), srcStart, srcEnd, new ModelIndexProxy(destParent), destColumn))
        member this.ColumnsAboutToBeRemoved (parent, first, last) =
            signalDispatch (ColumnsAboutToBeRemoved (new ModelIndexProxy(parent), first, last))
        member this.ColumnsInserted (parent, first, last) =
            signalDispatch (ColumnsInserted (new ModelIndexProxy(parent), first, last))
        member this.ColumnsMoved (srcParent, srcStart, srcEnd, destParent, destColumn) =
            signalDispatch (ColumnsMoved (new ModelIndexProxy(srcParent), srcStart, srcEnd, new ModelIndexProxy(destParent), destColumn))
        member this.ColumnsRemoved (parent, first, last) =
            signalDispatch (ColumnsRemoved (new ModelIndexProxy(parent), first, last))
        member this.DataChanged (topLeft, bottomRight, roles) =
            let roles' =
                roles
                |> Array.map ItemDataRole.From
                |> Array.toList
            signalDispatch (DataChanged (new ModelIndexProxy(topLeft), new ModelIndexProxy(bottomRight), roles'))
        member this.HeaderDataChanged (orientation, first, last) =
            signalDispatch (HeaderDataChanged (Orientation.From orientation, first, last))
        member this.LayoutAboutToBeChanged (parents, hint) =
            let parents' =
                parents
                |> Array.map PersistentModelIndexProxy
                |> Array.toList
            signalDispatch (LayoutAboutToBeChanged (parents', LayoutChangeHint.From hint))
        member this.LayoutChanged (parents, hint) =
            let parents' =
                parents
                |> Array.map PersistentModelIndexProxy
                |> Array.toList
            signalDispatch (LayoutChanged (parents', LayoutChangeHint.From hint))
        member this.ModelAboutToBeReset() =
            signalDispatch ModelAboutToBeReset
        member this.ModelReset() =
            signalDispatch ModelReset
        member this.RowsAboutToBeInserted (parent, start, ``end``) =
            signalDispatch (RowsAboutToBeInserted (new ModelIndexProxy(parent), start, ``end``))
        member this.RowsAboutToBeMoved (srcParent, srcStart, srcEnd, destParent, destRow) =
            signalDispatch (RowsAboutToBeMoved (new ModelIndexProxy(srcParent), srcStart, srcEnd, new ModelIndexProxy(destParent), destRow))
        member this.RowsAboutToBeRemoved (parent, first, last) =
            signalDispatch (RowsAboutToBeRemoved (new ModelIndexProxy(parent), first, last))
        member this.RowsInserted (parent, first, last) =
            signalDispatch (RowsInserted (new ModelIndexProxy(parent), first, last))
        member this.RowsMoved (srcParent, srcStart, srcEnd, destParent, destRow) =
            signalDispatch (RowsMoved (new ModelIndexProxy(srcParent), srcStart, srcEnd, new ModelIndexProxy(destParent), destRow))
        member this.RowsRemoved (parent, first, last) =
            signalDispatch (RowsRemoved (new ModelIndexProxy(parent), first, last))

    interface IDisposable with
        member this.Dispose() =
            absItemModel.Dispose()

type AbstractItemModelBinding internal(handle: AbstractItemModel.Handle) =
    inherit QObject.QObjectBinding(handle)
    member this.Sort(column: int) =
        handle.Sort(column)
    member this.Sort(column: int, order: SortOrder) =
        handle.Sort(column, order.QtValue)
