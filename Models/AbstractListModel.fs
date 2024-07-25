module FSharpQt.Models.AbstractListModel

open System
open FSharpQt.Attrs
open Org.Whatever.MinimalQtForFSharp

// no extra signals
type private Signal = unit

// no attributes, but for consistency with inherited stuff:
type internal AttrTarget =
    interface
        inherit AbstractItemModel.AttrTarget
        // abstract member ApplyAbstractListModelAttr: Attr -> unit
    end

type Props<'msg>() =
    inherit AbstractItemModel.Props<'msg>()
    
    member internal this.SignalMask = enum<AbstractListModel.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractItemModel.ModelCore<'msg>(dispatch)
    let mutable absListModel: AbstractListModel.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // let mutable currentMask = enum<AbstractListModel.SignalMask> 0
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
    
    member this.AbstractListModel
        with get() = absListModel
        and set value =
            this.AbstractItemModel <- value
            absListModel <- value
   
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                ()
            | _ ->
                failwith "AbstractListModel.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractListModel.ModelCore: signal map assignment didn't have a head element"

    // no signal mask setter, because abstract
    
    // "implemented" for inheritance purposes
    interface AttrTarget
    
    interface AbstractListModel.SignalHandler with
        // Object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // AbstractItemModel ==============
        member this.ColumnsAboutToBeInserted (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsAboutToBeInserted(parent, first, last)
        member this.ColumnsAboutToBeMoved (srcParent, srcStart, srcEnd, destParent, destColumn) =
            (this :> AbstractItemModel.SignalHandler).ColumnsAboutToBeMoved(srcParent, srcStart, srcEnd, destParent, destColumn)
        member this.ColumnsAboutToBeRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsAboutToBeRemoved(parent, first, last)
        member this.ColumnsInserted (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsInserted(parent, first, last)
        member this.ColumnsMoved (srcParent, srcStart, srcEnd, destParent, destColumn) =
            (this :> AbstractItemModel.SignalHandler).ColumnsMoved(srcParent, srcStart, srcEnd, destParent, destColumn)
        member this.ColumnsRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).ColumnsRemoved(parent, first, last)
        member this.DataChanged (topLeft, bottomRight, roles) =
            (this :> AbstractItemModel.SignalHandler).DataChanged(topLeft, bottomRight, roles)
        member this.HeaderDataChanged (orientation, first, last) =
            (this :> AbstractItemModel.SignalHandler).HeaderDataChanged(orientation, first, last)
        member this.LayoutAboutToBeChanged (parents, hint) =
            (this :> AbstractItemModel.SignalHandler).LayoutAboutToBeChanged(parents, hint)
        member this.LayoutChanged (parents, hint) =
            (this :> AbstractItemModel.SignalHandler).LayoutChanged(parents, hint)
        member this.ModelAboutToBeReset() =
            (this :> AbstractItemModel.SignalHandler).ModelAboutToBeReset()
        member this.ModelReset() =
            (this :> AbstractItemModel.SignalHandler).ModelReset()
        member this.RowsAboutToBeInserted (parent, start, ``end``) =
            (this :> AbstractItemModel.SignalHandler).RowsAboutToBeInserted(parent, start, ``end``)
        member this.RowsAboutToBeMoved (srcParent, srcStart, srcEnd, destParent, destRow) =
            (this :> AbstractItemModel.SignalHandler).RowsAboutToBeMoved(srcParent, srcStart, srcEnd, destParent, destRow)
        member this.RowsAboutToBeRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).RowsAboutToBeRemoved(parent, first, last)
        member this.RowsInserted (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).RowsInserted(parent, first, last)
        member this.RowsMoved (srcParent, srcStart, srcEnd, destParent, destRow) =
            (this :> AbstractItemModel.SignalHandler).RowsMoved(srcParent, srcStart, srcEnd, destParent, destRow)
        member this.RowsRemoved (parent, first, last) =
            (this :> AbstractItemModel.SignalHandler).RowsRemoved(parent, first, last)
        // AbstractListModel ==============
        // (none)

    interface IDisposable with
        member this.Dispose() =
            absListModel.Dispose()
