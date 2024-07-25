module FSharpQt.Models.TrackedRows

open FSharpQt.Extensions

type RowChangeItem<'row> =
    | RowAdded of index: int * row: 'row
    | RangeAdded of index: int * rows: 'row list
    | RowDeleted of index: int
    | RangeDeleted of index: int * count: int
    | RowReplaced of index: int * newRow: 'row

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
        
    member this.AddRows(rows: 'row list) =
        let index =
            this.Rows.Length
        let nextRows =
            this.Rows @ rows
        let nextCount =
            this.RowCount + rows.Length
        let nextChanges =
            this.Changes @ [ RangeAdded(index, rows) ]
        { this with Rows = nextRows; RowCount = nextCount; Changes = nextChanges }
        
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
        let nextRows =
            this.Rows
            |> List.removeManyAt index count
        let nextCount =
            this.RowCount - count
        let nextChanges =
            this.Changes @ [ RangeDeleted(index, count) ]
        { this with Rows = nextRows; RowCount = nextCount; Changes = nextChanges }
        
    member this.ReplaceAtIndex(index: int, row: 'row) =
        let nextRows =
            this.Rows
            |> List.replaceAtIndex index (fun _ -> row)
        let nextChanges =
            this.Changes @ [ RowReplaced(index, row) ]
        { this with Rows = nextRows; Changes = nextChanges }
        
    static member Init(rows: 'row list) =
        { Step = 0; Rows = []; RowCount = 0; Changes = [] }.AddRows(rows)
