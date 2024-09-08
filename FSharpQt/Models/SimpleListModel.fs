module FSharpQt.Models.SimpleListModel

open System
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type ISimpleListModelDelegate<'msg,'row> =
    interface
        abstract member Data: 'row -> int -> ItemDataRole -> Variant
        abstract member GetFlags: 'row -> int -> Set<ItemFlag> -> Set<ItemFlag>
        abstract member SetData: int -> 'row -> int -> VariantProxy -> ItemDataRole -> 'msg option
    end

[<AbstractClass>]
type AbstractSimpleListModelDelegate<'msg,'row>() =
    // must implement:
    abstract member Data: 'row -> int -> ItemDataRole -> Variant
    // optional:
    abstract member GetFlags: 'row -> int -> Set<ItemFlag> -> Set<ItemFlag>
    default this.GetFlags rowData col baseFlags =
        baseFlags
    abstract member SetData: int -> 'row -> int -> VariantProxy -> ItemDataRole -> 'msg option
    default this.SetData rowIndex rowData col value role = None
    // interface:
    interface ISimpleListModelDelegate<'msg,'row> with
        override this.Data rowData col role = this.Data rowData col role
        override this.GetFlags rowData col baseFlags = this.GetFlags rowData col baseFlags
        override this.SetData rowIndex rowData col value role = this.SetData rowIndex rowData col value role

type internal SimpleListModel<'msg,'row>(dispatch: 'msg -> unit, handler: AbstractListModel.SignalHandler, numColumns: int, simpleDelegate: ISimpleListModelDelegate<'msg,'row>) as this =
    let mutable rows = [||]
    let mutable simpleDelegate = simpleDelegate
    
    let emptyIndex =
        ModelIndex.Deferred.Empty()
        
    let interior =
        let methodMask =
            let zero =
                enum<AbstractListModel.MethodMask> 0
            AbstractListModel.MethodMask.HeaderData |||
            AbstractListModel.MethodMask.Flags |||
            AbstractListModel.MethodMask.SetData |||
            (if numColumns > 1 then AbstractListModel.MethodMask.ColumnCount else zero)
        AbstractListModel.CreateSubclassed(handler, this, methodMask)
            
    let mutable maybeHeaders: string array option = None
    
    // needs to be re-assignable in case it's "user" state-dependent
    member this.SimpleDelegate with set value =
        simpleDelegate <- value
        
    member this.Headers with set value =
        let newValue = Some (value |> List.toArray)
        if maybeHeaders <> newValue then
            maybeHeaders <- newValue
            interior.EmitHeaderDataChanged(Enums.Orientation.Horizontal, 0, numColumns - 1)
            
    member this.QtModel =
        interior :> AbstractListModel.Handle
        
    interface AbstractListModel.MethodDelegate with
        member this.RowCount(parent: ModelIndex.Handle) =
            rows.Length
            
        member this.Data(index: ModelIndex.Handle, role: Enums.ItemDataRole) =
            let value =
                if index.IsValid() then
                    let rowIndex = index.Row()
                    let colIndex = index.Column()
                    if rowIndex < rows.Length && colIndex < numColumns then
                        let row =
                            rows[rowIndex]
                        simpleDelegate.Data row colIndex (ItemDataRole.From role)
                    else
                        Variant.Empty
                else
                    Variant.Empty
            value.QtValue
                
        // optional depending on mask: ==================================================
        
        member this.HeaderData(section: int, orientation: Enums.Orientation, role: Enums.ItemDataRole) =
            let variant =
                if role = Enums.ItemDataRole.DisplayRole then
                    match maybeHeaders with
                    | Some headers ->
                        if section < headers.Length then
                            Variant.String headers[section]
                        else
                            Variant.String ""
                    | None ->
                        Variant.Empty
                else
                    Variant.Empty
            variant.QtValue
            
        member this.GetFlags(index: ModelIndex.Handle, baseFlags: AbstractListModel.ItemFlags) =
            let result =
                if index.IsValid() then
                    let rowIndex = index.Row()
                    let colIndex = index.Column()
                    if rowIndex < rows.Length && colIndex < numColumns then
                        let row =
                            rows[rowIndex]
                        simpleDelegate.GetFlags row colIndex (ItemFlag.SetFrom baseFlags)
                    else
                        Set.empty
                else
                    Set.empty
            ItemFlag.QtSetFrom result
            
        member this.SetData(index: ModelIndex.Handle, value: Org.Whatever.MinimalQtForFSharp.Variant.Handle, role: Enums.ItemDataRole) =
            let maybeMsg =
                if index.IsValid() then
                    let rowIndex = index.Row()
                    let colIndex = index.Column()
                    if rowIndex < rows.Length && colIndex < numColumns then
                        let row =
                            rows[rowIndex]
                        simpleDelegate.SetData rowIndex row colIndex (new VariantProxy(value)) (ItemDataRole.From role)
                    else
                        None
                else
                    None
            match maybeMsg with
            | Some msg ->
                dispatch msg
                true
            | None ->
                false
            
        member this.ColumnCount(parent: ModelIndex.Handle) =
            numColumns
            
    interface IDisposable with
        member this.Dispose() =
            interior.Dispose()
            
    member this.AddRowAt(index: int, row: 'row) =
        interior.BeginInsertRows(emptyIndex, index, index)
        rows <- Array.insertAt index row rows
        interior.EndInsertRows()
        
    member this.AddRowsAt(index: int, newRows: 'row list) =
        interior.BeginInsertRows(emptyIndex, index, index + newRows.Length - 1)
        rows <- Array.insertManyAt index newRows rows
        interior.EndInsertRows()
        
    member this.DeleteRowAt(index: int) =
        interior.BeginRemoveRows(emptyIndex, index, index)
        rows <- Array.removeAt index rows
        interior.EndRemoveRows()
        
    member this.DeleteRowsAt(index: int, count: int) =
        interior.BeginRemoveRows(emptyIndex, index, index + count - 1)
        rows <- Array.removeManyAt index count rows
        interior.EndRemoveRows()
        
    member this.ReplaceRowAt(index: int, row: 'row) =
        rows[index] <- row
        use topLeft =
            interior.Index(index, 0)
        use bottomRight =
            interior.Index(index, numColumns - 1)
        interior.EmitDataChanged(
            ModelIndex.Deferred.FromOwned(topLeft),
            ModelIndex.Deferred.FromOwned(bottomRight), [||])
        
    member this.DeleteIndices(indices: int list) =
        // over a certain threshold should probably just reset the model
        let reversed =
            indices |> List.sortDescending
        for index in reversed do
            interior.BeginRemoveRows(emptyIndex, index, index)
            rows <- Array.removeAt index rows
            interior.EndRemoveRows()
