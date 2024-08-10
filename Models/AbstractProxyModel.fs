module FSharpQt.Models.AbstractProxyModel

open System
open FSharpQt.Attrs
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | SourceModelChanged
    
type internal Attr = unit
    // technically this has a 'SourceModel' on the C++ side
    // but that can't be a simple attribute for us, it's a node dependency (of whatever inherits from this - we don't currently have any kind of dependency inheritance)
    // luckily it will appear to the developer as just another property :)
    
and internal AttrTarget = // for inheritance purposes only
    interface
        inherit AbstractItemModel.AttrTarget
        // abstract member ApplyAbstractProxyModelAttr: Attr<'msg> -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit AbstractItemModel.Props<'msg>()
    
    let mutable onSourceModelChanged: 'msg option = None
    
    member internal this.SignalMask = enum<AbstractProxyModel.SignalMask> (int this._signalMask)
    
    member this.OnSourceModelChanged with set value =
        onSourceModelChanged <- Some value
        this.AddSignal(int AbstractProxyModel.SignalMask.SourceModelChanged)

    member internal this.SignalMapList =
        let thisFunc = function
            | SourceModelChanged ->
                onSourceModelChanged
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractItemModel.ModelCore<'msg>(dispatch)
    let mutable absProxyModel: AbstractProxyModel.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.AbstractProxyModel
        with get() = absProxyModel
        and set value =
            // assign to base
            this.AbstractItemModel <- value
            absProxyModel <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "AbstractProxyModel.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractProxyModel.ModelCore: signal map assignment didn't have a head element"
            
    // no signal mask setter, abstract
    
    // "implemented" for inheritance purposes
    interface AttrTarget
                    
    interface AbstractProxyModel.SignalHandler with
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
        // AbstractProxyModel
        member this.SourceModelChanged() =
            signalDispatch SourceModelChanged

    interface IDisposable with
        member this.Dispose() =
            absProxyModel.Dispose()

type AbstractProxyModelBinding internal(handle: AbstractProxyModel.Handle) =
    inherit AbstractItemModel.AbstractItemModelBinding(handle)
    member this.MapToSource (proxyIndex: ModelIndexProxy) =
        let ret = handle.MapToSource(proxyIndex.AsDeferred)
        new ModelIndexProxy(ret)
