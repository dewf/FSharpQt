module FSharpQt.Widgets.GridLayout

open System
open FSharpQt.Attrs
open Org.Whatever.MinimalQtForFSharp
open FSharpQt
open BuilderNode
open MiscTypes

type private Signal = unit

type internal RowColConfigInternal = {
    Index: int
    MinExt: int option
    Stretch: int option
} with
    member this.ApplyToRow(gridLayout: GridLayout.Handle) =
        this.MinExt
        |> Option.iter (fun height -> gridLayout.SetRowMinimumHeight(this.Index, height))
        this.Stretch
        |> Option.iter (fun stretch -> gridLayout.SetRowStretch(this.Index, stretch))
    member this.ApplyToCol(gridLayout: GridLayout.Handle) =
        this.MinExt
        |> Option.iter (fun width -> gridLayout.SetColumnMinimumWidth(this.Index, width))
        this.Stretch
        |> Option.iter (fun stretch -> gridLayout.SetColumnStretch(this.Index, stretch))
        
type RowConfig(row: int, ?minHeight: int, ?stretch: int) =
    member val internal Config = { Index = row; MinExt = minHeight; Stretch = stretch }
    
type ColConfig(col: int, ?minWidth: int, ?stretch: int) =
    member val internal Config = { Index = col; MinExt = minWidth; Stretch = stretch }

type internal Attr =
    | HorizontalSpacing of spacing: int
    | VerticalSpacing of spacing: int
    | RowConfigs of RowColConfigInternal list
    | ColConfigs of RowColConfigInternal list
with
    interface IAttr with
        override this.AttrEquals other =
            match other with
            | :? Attr as attrOther ->
                this = attrOther
            | _ ->
                false
        override this.Key =
            match this with
            | HorizontalSpacing _ -> "gridlayout:horizontalspacing"
            | VerticalSpacing _ -> "gridlayout:verticalspacing"
            | RowConfigs _ -> "gridlayout:rowconfigs"
            | ColConfigs _ -> "gridlayout:colconfigs"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyGridLayoutAttr(this)
            | _ ->
                printfn "warning: GridLayout.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Layout.AttrTarget
        abstract member ApplyGridLayoutAttr: Attr -> unit
    end
    
type Props<'msg>() =
    inherit Layout.Props<'msg>()
    
    member internal this.SignalMask = enum<GridLayout.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
        
    member this.HorizontalSpacing with set value =
        this.PushAttr(HorizontalSpacing value)
        
    member this.VerticalSpacing with set value =
        this.PushAttr(VerticalSpacing value)
        
    member this.RowConfigs with set (value: RowConfig seq) =
        let configs =
            value
            |> Seq.map (_.Config)
            |> Seq.toList
            |> RowConfigs
        this.PushAttr(configs)
        
    member this.ColumnConfigs with set (value: ColConfig seq) =
        let configs =
            value
            |> Seq.map (_.Config)
            |> Seq.toList
            |> ColConfigs
        this.PushAttr(configs)
    
type Location = {
    Row: int
    Col: int
    RowSpan: int option
    ColSpan: int option
    Align: Alignment option
} with
    static member Create(row: int, col: int, ?rowSpan: int, ?colSpan: int, ?align: Alignment) =
        { Row = row
          Col = col
          RowSpan = defaultArg (Some rowSpan) None
          ColSpan = defaultArg (Some colSpan) None
          Align = defaultArg (Some align) None }
    static member Default =
        { Row = 0; Col = 0; RowSpan = None; ColSpan = None; Align = None }

type internal InternalItem<'msg> =
    | WidgetItem of w: IWidgetNode<'msg> * loc: Location
    | LayoutItem of l: ILayoutNode<'msg> * loc: Location
        
type GridItem<'msg> private(item: InternalItem<'msg>) =
    member val internal Item = item
    new(w: IWidgetNode<'msg>, row: int, col: int, ?rowSpan: int, ?colSpan: int, ?align: Alignment) =
        let loc =
            { Row = row
              Col = col
              RowSpan = defaultArg (Some rowSpan) None
              ColSpan = defaultArg (Some colSpan) None
              Align = defaultArg (Some align) None }
        GridItem(WidgetItem (w, loc))
    new(l: ILayoutNode<'msg>, row: int, col: int, ?rowSpan: int, ?colSpan: int, ?align: Alignment) =
        let loc =
            { Row = row
              Col = col
              RowSpan = defaultArg (Some rowSpan) None
              ColSpan = defaultArg (Some colSpan) None
              Align = defaultArg (Some align) None }
        GridItem(LayoutItem (l, loc))
    member this.Node =
        match item with
        | WidgetItem(w, _) -> w :> IBuilderNode<'msg>
        | LayoutItem(l, _) -> l :> IBuilderNode<'msg>
    member this.Key =
        match item with
        | WidgetItem(w, loc) -> w.ContentKey, loc
        | LayoutItem(l, loc) -> l.ContentKey, loc
        
type private Method =
    | Normal
    | WithAlignment of align: Alignment
    | WithSpans of rowSpan: int * colSpan: int
    | WithSpansAlignment of rowSpan: int * colSpan: int * align: Alignment
    
let private whichMethod (loc: Location) =
    match loc.Align with
    | Some align ->
        match loc.RowSpan, loc.ColSpan with
        | None, None ->
            WithAlignment align
        | None, Some colSpan ->
            WithSpansAlignment (1, colSpan, align)
        | Some rowSpan, None ->
            WithSpansAlignment (rowSpan, 1, align)
        | Some rowSpan, Some colSpan ->
            WithSpansAlignment (rowSpan, colSpan, align)
    | None ->
        match loc.RowSpan, loc.ColSpan with
        | None, None ->
            Normal
        | None, Some colSpan ->
            WithSpans (1, colSpan)
        | Some rowSpan, None ->
            WithSpans (rowSpan, 1)
        | Some rowSpan, Some colSpan ->
            WithSpans (rowSpan, colSpan)
    
let private addItem (grid: GridLayout.Handle) = function
    | WidgetItem(w, loc) ->
        match whichMethod loc with
        | Normal ->
            grid.AddWidget(w.Widget, loc.Row, loc.Col)
        | WithAlignment align ->
            grid.AddWidget(w.Widget, loc.Row, loc.Col, align.QtValue)
        | WithSpans(rowSpan, colSpan) ->
            grid.AddWidget(w.Widget, loc.Row, loc.Col, rowSpan, colSpan)
        | WithSpansAlignment(rowSpan, colSpan, align) ->
            grid.AddWidget(w.Widget, loc.Row, loc.Col, rowSpan, colSpan, align.QtValue)
    | LayoutItem(l, loc) ->
        match whichMethod loc with
        | Normal ->
            grid.AddLayout(l.Layout, loc.Row, loc.Col)
        | WithAlignment align ->
            grid.AddLayout(l.Layout, loc.Row, loc.Col, align.QtValue)
        | WithSpans(rowSpan, colSpan) ->
            grid.AddLayout(l.Layout, loc.Row, loc.Col, rowSpan, colSpan)
        | WithSpansAlignment(rowSpan, colSpan, align) ->
            grid.AddLayout(l.Layout, loc.Row, loc.Col, rowSpan, colSpan, align.QtValue)
            
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Layout.ModelCore<'msg>(dispatch: 'msg -> unit)
    let mutable gridLayout: GridLayout.Handle = null
    let mutable currentMask = enum<GridLayout.SignalMask> 0
    
    // no dispatch because no signals of our own
    
    member this.GridLayout
        with get() =
            gridLayout
        and set value =
            this.Layout <- value
            gridLayout <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                // nothing to do
                ()
            | _ ->
                failwith "GridLayout.ModelCore.SignalMaps: wrong func type"
            // assign the remainder
            base.SignalMaps <- etc
        | _ ->
            failwith "GridLayout.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            gridLayout.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyGridLayoutAttr attr =
            match attr with
            | HorizontalSpacing spacing ->
                gridLayout.SetHorizontalSpacing(spacing)
            | VerticalSpacing spacing ->
                gridLayout.SetVerticalSpacing(spacing)
            | RowConfigs configs ->
                for config in configs do
                    config.ApplyToRow(gridLayout)
            | ColConfigs configs ->
                for config in configs do
                    config.ApplyToCol(gridLayout)
                
    interface GridLayout.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // layout (none) ==================
        // gridlayout (none) ===============
        
    interface IDisposable with
        member this.Dispose() =
            gridLayout.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let gridLayout = GridLayout.Create(this)
    do
        this.GridLayout <- gridLayout
        
    member this.Refill(items: GridItem<'msg> list) =
        gridLayout.RemoveAll()
        for item in items do
            addItem gridLayout item.Item

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: GridLayout.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: GridLayout.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type GridLayout<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    member val Items: GridItem<'msg> list = [] with get, set
    
    member private this.MigrateContent(leftBox: GridLayout<'msg>) =
        let leftContents =
            leftBox.Items
            |> List.map (_.Key)
        let thisContents =
            this.Items
            |> List.map (_.Key)
        if leftContents <> thisContents then
            this.model.Refill(this.Items)
        else
            ()
        
    interface ILayoutNode<'msg> with
        override this.Dependencies =
            // because the indices are generated here, based on items order,
            // it prevents the possibility of the "user" (app developer) from being able to reorder existing items without them being destroyed/recreated entirely
            // but I don't think that's a very common use case, to be reordering anything in a vbox/hbox, except maybe adding things at the end (which should work fine)
            // if user-reordering was a common use case, then the user would have to provide item keys / IDs as part of the item list
            // we'll do that for example with top-level windows in the app window order, so that windows can be added/removed without forcing a rebuild of existing windows
            this.Items
            |> List.mapi (fun i item -> IntKey i, item.Node)
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            this.model.Refill(this.Items)
        
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> GridLayout<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent(left')
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Layout =
            this.model.GridLayout
            
        override this.ContentKey =
            this.model.GridLayout
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
