module FSharpQt.Widgets.CalendarWidget

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Widgets
open FSharpQt.MiscTypes
open FSharpQt.Attrs

type private Signal =
    | Activated of date: DateOnly
    | Clicked of date: DateOnly
    | CurrentPageChanged of value: {| Year: int; Month: int |}
    | SelectionChanged
    
type HorizontalHeaderFormat =
    | NoHorizontalHeader
    | SingleLetterDayNames
    | ShortDayNames
    | LongDayNames
with
    member internal this.QtValue =
        match this with
        | NoHorizontalHeader -> CalendarWidget.HorizontalHeaderFormat.NoHorizontalHeader
        | SingleLetterDayNames -> CalendarWidget.HorizontalHeaderFormat.SingleLetterDayNames
        | ShortDayNames -> CalendarWidget.HorizontalHeaderFormat.ShortDayNames
        | LongDayNames -> CalendarWidget.HorizontalHeaderFormat.LongDayNames
    
type VerticalHeaderFormat =
    | NoVerticalHeader
    | ISOWeekNumbers
with
    member internal this.QtValue =
        match this with
        | NoVerticalHeader -> CalendarWidget.VerticalHeaderFormat.NoVerticalHeader
        | ISOWeekNumbers -> CalendarWidget.VerticalHeaderFormat.ISOWeekNumbers
    
type SelectionMode =
    | NoSelection
    | SingleSelection
with
    member internal this.QtValue =
        match this with
        | NoSelection -> CalendarWidget.SelectionMode.NoSelection
        | SingleSelection -> CalendarWidget.SelectionMode.SingleSelection

type internal Attr =
    | DateEditAcceptDelay of delay: int
    | DateEditEnabled of enabled: bool
    | FirstDayOfWeek of value: DayOfWeek    // use System value for now
    | GridVisible of visible: bool
    | HorizontalHeaderFormat of format: HorizontalHeaderFormat
    | MaximumDate of date: DateOnly
    | MinimumDate of date: DateOnly
    | NavigationBarVisible of visible: bool
    | SelectedDate of selected: DateOnly
    | SelectionMode of mode: SelectionMode
    | VerticalHeaderFormat of format: VerticalHeaderFormat
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
            | DateEditAcceptDelay _ -> "calendarwidget:DateEditAcceptDelay"
            | DateEditEnabled _ -> "calendarwidget:DateEditEnabled"
            | FirstDayOfWeek _ -> "calendarwidget:FirstDayOfWeek"
            | GridVisible _ -> "calendarwidget:GridVisible"
            | HorizontalHeaderFormat _ -> "calendarwidget:HorizontalHeaderFormat"
            | MaximumDate _ -> "calendarwidget:MaximumDate"
            | MinimumDate _ -> "calendarwidget:MinimumDate"
            | NavigationBarVisible _ -> "calendarwidget:NavigationBarVisible"
            | SelectedDate _ -> "calendarwidget:SelectedDate"
            | SelectionMode _ -> "calendarwidget:SelectionMode"
            | VerticalHeaderFormat _ -> "calendarwidget:VerticalHeaderFormat"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyCalendarWidgetAttr(this)
            | _ ->
                printfn "warning: CalendarWidget.Attr couldn't ApplyTo() unknown target type [%A]" target
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyCalendarWidgetAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onActivated: (DateOnly -> 'msg) option = None
    let mutable onClicked: (DateOnly -> 'msg) option = None
    let mutable onCurrentPageChanged: ({| Year: int; Month: int |} -> 'msg) option = None
    let mutable onSelectionChanged: 'msg option = None

    member internal this.SignalMask = enum<CalendarWidget.SignalMask> (int this._signalMask)
    
    member this.OnActivated with set value =
        onActivated <- Some value
        this.AddSignal(int CalendarWidget.SignalMask.Activated)
        
    member this.OnClicked with set value =
        onClicked <- Some value
        this.AddSignal(int CalendarWidget.SignalMask.Clicked)

    member this.OnCurrentPageChanged with set value =
        onCurrentPageChanged <- Some value
        this.AddSignal(int CalendarWidget.SignalMask.CurrentPageChanged)

    member this.OnSelectionChanged with set value =
        onSelectionChanged <- Some value
        this.AddSignal(int CalendarWidget.SignalMask.SelectionChanged)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Activated date ->
                onActivated
                |> Option.map (fun f -> f date)
            | Clicked date ->
                onClicked
                |> Option.map (fun f -> f date)
            | CurrentPageChanged value ->
                onCurrentPageChanged
                |> Option.map (fun f -> f value)
            | SelectionChanged ->
                onSelectionChanged
        // prepend to parent's
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.DateEditAcceptDelay with set value =
        this.PushAttr(DateEditAcceptDelay value)
        
    member this.DateEditEnabled with set value =
        this.PushAttr(DateEditEnabled value)

    member this.FirstDayOfWeek with set value =
        this.PushAttr(FirstDayOfWeek value)

    member this.GridVisible with set value =
        this.PushAttr(GridVisible value)

    member this.HorizontalHeaderFormat with set value =
        this.PushAttr(HorizontalHeaderFormat value)

    member this.MaximumDate with set value =
        this.PushAttr(MaximumDate value)

    member this.MinimumDate with set value =
        this.PushAttr(MinimumDate value)

    member this.NavigationBarVisible with set value =
        this.PushAttr(NavigationBarVisible value)

    member this.SelectedDate with set value =
        this.PushAttr(SelectedDate value)

    member this.SelectionMode with set value =
        this.PushAttr(SelectionMode value)

    member this.VerticalHeaderFormat with set value =
        this.PushAttr(VerticalHeaderFormat value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable calendarWidget: CalendarWidget.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<CalendarWidget.SignalMask> 0
    
    // no binding guards (perhaps iffy - there would seem to be an indirect connection between signal values and properties)
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.CalendarWidget
        with get() = calendarWidget
        and set value =
            // assign to base
            this.Widget <- value
            calendarWidget <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "CalendarWidget.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "CalendarWidget.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            calendarWidget.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyCalendarWidgetAttr attr =
            match attr with
            | DateEditAcceptDelay delay ->
                calendarWidget.SetDateEditAcceptDelay(delay)
            | DateEditEnabled enabled ->
                calendarWidget.SetDateEditEnabled(enabled)
            | FirstDayOfWeek value ->
                let qtValue =
                    match value with
                    | DayOfWeek.Sunday -> Enums.QDayOfWeek.Sunday
                    | DayOfWeek.Monday -> Enums.QDayOfWeek.Monday
                    | DayOfWeek.Tuesday -> Enums.QDayOfWeek.Tuesday
                    | DayOfWeek.Wednesday -> Enums.QDayOfWeek.Wednesday
                    | DayOfWeek.Thursday -> Enums.QDayOfWeek.Thursday
                    | DayOfWeek.Friday -> Enums.QDayOfWeek.Friday
                    | DayOfWeek.Saturday -> Enums.QDayOfWeek.Saturday
                    | _ -> failwithf "CalendarWidget setting FirstDayOfWeek: bad input value (%A)" value
                calendarWidget.SetFirstDayOfWeek(qtValue)
            | GridVisible visible ->
                calendarWidget.SetGridVisible(visible)
            | HorizontalHeaderFormat format ->
                calendarWidget.SetHorizontalHeaderFormat(format.QtValue)
            | MaximumDate date ->
                calendarWidget.SetMaximumDate(date |> qtDateFromDateOnly)
            | MinimumDate date ->
                calendarWidget.SetMinimumDate(date |> qtDateFromDateOnly)
            | NavigationBarVisible visible ->
                calendarWidget.SetNavigationBarVisible(visible)
            | SelectedDate selected ->
                calendarWidget.SetSelectedDate(selected |> qtDateFromDateOnly)
            | SelectionMode mode ->
                calendarWidget.SetSelectionMode(mode.QtValue)
            | VerticalHeaderFormat format ->
                calendarWidget.SetVerticalHeaderFormat(format.QtValue)

    interface CalendarWidget.SignalHandler with
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
        // CalendarWidget =======================
        member this.Activated qDate =
            signalDispatch (qDate |> dateOnlyFromQtDate |> Activated)
        member this.Clicked qDate =
            signalDispatch (qDate |> dateOnlyFromQtDate |> Clicked)
        member this.CurrentPageChanged (year, month) =
            signalDispatch (CurrentPageChanged {| Year = year; Month = month |})
        member this.SelectionChanged() =
            signalDispatch SelectionChanged
            
    interface IDisposable with
        member this.Dispose() =
            calendarWidget.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.CalendarWidget <- CalendarWidget.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: CalendarWidget.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: CalendarWidget.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type CalendarWidgetBinding internal(handle: CalendarWidget.Handle) =
    inherit Widget.WidgetBinding(handle)
    member this.SelectedDate =
        use qDate = handle.SelectedDate()
        let value = qDate.ToYearMonthDay()
        DateOnly(value.Year, value.Month, value.Day)
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? CalendarWidgetBinding as calendarWidget) ->
        calendarWidget
    | _ ->
        failwith "CalendarWidget.bindNode fail"

type CalendarWidget<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> CalendarWidget<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.CalendarWidget
            
        override this.ContentKey =
            this.model.CalendarWidget
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, CalendarWidgetBinding(this.model.CalendarWidget))
