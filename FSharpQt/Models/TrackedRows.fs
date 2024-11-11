module FSharpQt.Models.TrackedRows

open FSharpQt.Extensions

type RowChangeItem<'row> =
    | RowAdded of index: int * row: 'row
    | RangeAdded of index: int * rows: 'row list
    | RowDeleted of index: int
    | RangeDeleted of index: int * count: int
    | RowReplaced of index: int * newRow: 'row
    | IndicesDeleted of indices: int list

// so that the 'row doesn't poison our Attrs, leads to weird issues    
type ITrackedRows =
    interface
        abstract member Equals: System.Object -> bool
        abstract member GetHashCode: unit -> int
    end

[<CustomEquality>]
[<NoComparison>]
type TrackedRows<'row> = {
    Step: int                           // for attr diffing
    Rows: 'row list
    RowCount: int
    Changes: RowChangeItem<'row> list
} with
    interface ITrackedRows with
        override this.Equals other =
            match other with
            | :? TrackedRows<'row> as other' ->
                this.Step = other'.Step
            | _ ->
                false
        override this.GetHashCode() =
            this.Step.GetHashCode() // uhhhh
            
    member this.Item (i: int) =
        List.item i this.Rows
        
    member this.BeginChanges() =
        { this with Step = this.Step + 1; Changes = [] } // 1 step per group of changes, I guess
        
    member this.AddRow(row: 'row) =
        let index =
            this.Rows.Length
        let nextRows =
            this.Rows @ [ row ]
        let nextCount =
            this.RowCount + 1
        let nextChanges =
            this.Changes @ [ RowAdded (index, row) ]
        { this with Rows = nextRows; RowCount = nextCount; Changes = nextChanges }
        
    member this.AddRows(rowsSeq: 'row seq) =
        let rows =
            rowsSeq
            |> List.ofSeq
        if not rows.IsEmpty then
            let index =
                this.Rows.Length
            let nextRows =
                this.Rows @ rows
            let nextCount =
                this.RowCount + rows.Length
            let nextChanges =
                this.Changes @ [ RangeAdded(index, rows) ]
            { this with Rows = nextRows; RowCount = nextCount; Changes = nextChanges }
        else
            // avoid trouble, Qt fails on empty row insertions
            this
        
    member this.DeleteRow(index: int) =
        let nextRows =
            this.Rows
            |> List.removeAt index
        let nextCount =
            this.RowCount - 1
        let nextChanges =
            this.Changes @ [ RowDeleted(index) ]
        { this with Rows = nextRows; RowCount = nextCount; Changes = nextChanges }
        
    member this.DeleteRows(index: int, count: int) =
        if count > 0 then
            let nextRows =
                this.Rows
                |> List.removeManyAt index count
            let nextCount =
                this.RowCount - count
            let nextChanges =
                this.Changes @ [ RangeDeleted(index, count) ]
            { this with Rows = nextRows; RowCount = nextCount; Changes = nextChanges }
        else
            printfn "TrackedRows: attempted to delete < 1 rows"
            this
        
    member this.ReplaceAtIndex(index: int, row: 'row) =
        let nextRows =
            this.Rows
            |> List.replaceAtIndex index (fun _ -> row)
        let nextChanges =
            this.Changes @ [ RowReplaced(index, row) ]
        { this with Rows = nextRows; Changes = nextChanges }
        
    // member this.ReplaceSingleWhere(matchFunc: 'row -> bool, replaceFunc: 'row -> 'row) =
    //     let index =
    //         this.Rows
    //         |> List.findIndex matchFunc // must succeed!
    //     let nextRows, updated =
    //         List.replaceAtIndexWithChanged index replaceFunc this.Rows
    //     let nextChanges =
    //         this.Changes @ [ RowReplaced(index, updated) ]
    //     { this with Rows = nextRows; Changes = nextChanges }
        
    member this.TouchWhere(matchFunc: 'row -> bool) =
        // pretend a row changed to trigger a redraw
        // for when nothing in the data actually changed, but we need to trigger a redraw for some reason
        // (changed decoration etc)
        let nextChanges =
            let touchedRows =
                this.Rows
                |> List.zipWithIndex
                |> List.choose (fun (i, row) ->
                    if matchFunc row then
                        RowReplaced(i, row)
                        |> Some
                    else
                        None)
            this.Changes @ touchedRows
        { this with Changes = nextChanges }

    member this.ReplaceWhere(replaceFunc: 'row -> 'row option) =
        let reversed =
            ({| RevRows = []; RevChanges = [] |}, this.Rows |> List.zipWithIndex)
            ||> List.fold (fun acc (i, row) ->
                match replaceFunc row with
                | Some row' ->
                    let change =
                        RowReplaced(i, row')
                    {| RevRows = row' :: acc.RevRows
                       RevChanges = change :: acc.RevChanges |}
                | None ->
                    {| acc with RevRows = row :: acc.RevRows |})
        let nextRows =
            reversed.RevRows
            |> List.rev
        let nextChanges =
            this.Changes @ (reversed.RevChanges |> List.rev)
        { this with Rows = nextRows; Changes = nextChanges }
        
    member this.DeleteWhere(matchFunc: 'row -> bool) =
        let affected =
            this.Rows
            |> List.zipWithIndex
            |> List.choose (fun (i, row) ->
                match matchFunc row with
                | true -> Some i
                | false -> None)
        let nextChanges =
            this.Changes @ [ IndicesDeleted affected ]
        let nextRows =
            this.Rows
            |> List.filter (matchFunc >> not)
        let nextCount =
            this.RowCount - affected.Length // save a marginal amount of time counting it this way
        { this with Rows = nextRows; Changes = nextChanges; RowCount = nextCount }
        
    member this.DiffUpdate<'a when 'a : comparison>(nextRows: 'row list, keyFunc: 'row -> 'a) =
        let prevKeys =
            this.Rows
            |> List.map keyFunc
            |> Set.ofList
        let nextKeys =
            nextRows
            |> List.map keyFunc
            |> Set.ofList
        // which of the existing rows were deleted?
        let deletedIndices =
            this.Rows
            |> List.zipWithIndex
            |> List.filter (fun (_, row) ->
                keyFunc row
                |> nextKeys.Contains
                |> not)
            |> List.map fst
        // which of the next rows were added?
        let added =
            nextRows
            |> List.filter (keyFunc >> prevKeys.Contains >> not)
        let nextChanges =
            this.Changes @ [
                if deletedIndices.Length > 0 then
                    IndicesDeleted deletedIndices
                if added.Length > 0 then
                    let addFromIndex =
                        this.RowCount - deletedIndices.Length
                    RangeAdded(addFromIndex, added)
            ]
        { this with Rows = nextRows; Changes = nextChanges; RowCount = nextRows.Length }
        
    static member Init(rows: 'row seq) =
        { Step = 0; Rows = []; RowCount = 0; Changes = [] }.AddRows(rows)
