module FSharpQt.Models.ListModelNode

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.Models.SimpleListModel
open FSharpQt.Models.TrackedRows
open FSharpQt.MiscTypes

type private Signal = unit

type internal Attr =
    | Rows of rows: ITrackedRows
    | Headers of names: string list
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
            | Rows _ -> "listmodelnode:rows"
            | Headers _ -> "listmodelnode:headers"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyListModelNodeAttr(this)
            | _ ->
                printfn "warning: ListModelNode.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractListModel.AttrTarget
        abstract member ApplyListModelNodeAttr: Attr -> unit
    end
    
type Props<'msg>() =
    inherit AbstractListModel.Props<'msg>()
    
    member this.Rows with set value =
        this.PushAttr(Rows value)
        
    member this.Headers with set value =
        this.PushAttr(Headers value)

type Model<'msg,'row>(dispatch: 'msg -> unit, numColumns: int) as this =
    inherit AbstractListModel.ModelCore<'msg>(dispatch)
    let listModel = new SimpleListModel<'row>(numColumns)
    let mutable headers: string list = []
    do
        this.AbstractListModel <- listModel.QtModel
    
    member this.QtModel =
        listModel.QtModel
        
    member this.DataFunc with set value =
        listModel.DataFunc <- value
        
    interface AttrTarget with
        member this.ApplyListModelNodeAttr attr =
            match attr with
            | Rows raw  ->
                match raw with
                | :? TrackedRows<'row> as rows ->
                    for change in rows.Changes do
                        match change with
                        | RowAdded(index, row) ->
                            listModel.AddRowAt(index, row)
                        | RangeAdded(index, rows) ->
                            listModel.AddRowsAt(index, rows)
                        | RowReplaced(index, newRow) ->
                            listModel.ReplaceRowAt(index, newRow)
                        | RowDeleted index ->
                            listModel.DeleteRowAt(index)
                        | RangeDeleted(index, count) ->
                            listModel.DeleteRowsAt(index, count)
                | _ ->
                    printfn "ListModelNode weirdness"
            | Headers names ->
                if headers <> names then
                    headers <- names
                    listModel.Headers <- names
            
    interface IDisposable with
        member this.Dispose() =
            (listModel :> IDisposable).Dispose()
            
let private create (attrs: IAttr list) (dispatch: 'msg -> unit) (numColumns: int) (dataFunc: 'row -> int -> ItemDataRole -> Variant) =
    let model = new Model<'msg, 'row>(dispatch, numColumns)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    // model.SignalMaps <- signalMaps
    // model.SignalMask <- signalMask
    model.DataFunc <- dataFunc
    model

let private migrate (model: Model<'msg,'row>) (attrs: (IAttr option * IAttr) list) (dataFunc: 'row -> int -> ItemDataRole -> Variant) =
    model.ApplyAttrs attrs
    // model.SignalMaps <- signalMaps
    // model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg,'row>) =
    (model :> IDisposable).Dispose()


type ListModelNode<'msg,'row>(dataFunc: 'row -> int -> ItemDataRole -> Variant, ?numColumns: int) =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable model: Model<'msg,'row>

    interface IModelNode<'msg> with
        override this.Dependencies = []

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs dispatch (defaultArg numColumns 1) dataFunc
            
        override this.AttachDeps () =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> ListModelNode<'msg,'row>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs dataFunc

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.QtModel =
            this.model.QtModel
            
        override this.ContentKey =
            this.model.QtModel
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
