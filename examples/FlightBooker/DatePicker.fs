module DatePicker

open System
open FSharpQt
open BuilderNode
open FSharpQt.Widgets.Dialog
open Reactor
open FSharpQt.Widgets
open BoxLayout

open LineEdit
open PushButton
open CalendarWidget

open MiscTypes

type Value =
    | Empty
    | Invalid of text: string
    | Valid of date: DateOnly
    
type Signal =
    | ValueChanged of value: Value
    
type State = {
    Enabled: bool
    Raw: string
    Value: Value
    DialogTitle: string
    MaybeMinDate: DateOnly option
    MaybeMaxDate: DateOnly option
}

type private Attr =
    | Value of value: Value
    | Enabled of value: bool
    | DialogTitle of title: string
    | MinDate of value: DateOnly
    | MaxDate of value: DateOnly
    
let private keyFunc = function
    | Value _ -> "datepicker:value"
    | Enabled _ -> "datepicker:enabled"
    | DialogTitle _ -> "datepicker:dialogtitle"
    | MinDate _ -> "datepicker:mindate"
    | MaxDate _ -> "datepicker:maxdate"
    
let private attrUpdate state = function
    | Value value ->
        match value with
        | Empty ->
            { state with Value = Empty; Raw = "" }
        | Invalid text ->
            { state with Value = Invalid text; Raw = text } // "??invalid??"
        | Valid dt ->
            { state with Value = Valid dt; Raw = dt.ToShortDateString() }
    | Enabled value ->
        { state with Enabled = value }
    | DialogTitle title ->
        { state with DialogTitle = title }
    | MinDate date ->
        { state with MaybeMinDate = Some date }
    | MaxDate date ->
        { state with MaybeMaxDate = Some date }
        
type Msg =
    | EditingFinished           // from return or tab                   
    | Submitted of str: string  // our synthetic event with the text payload, via viewexec
    | ShowCalendar
    | AcceptDialog
    | RejectDialog
    | ValueChosen of date: DateOnly

let init () =
    let state = {
        Enabled = true
        Raw = ""
        Value = Empty
        DialogTitle = "Choose Date"
        MaybeMinDate = None
        MaybeMaxDate = None
    }
    state, Cmd.None
        
let tryParseDate (str: string) =
    match DateOnly.TryParse(str) with // .TryParseExact(str, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None) with
    | true, dt -> Some dt
    | _ -> None
    
let update (state: State) (msg: Msg) =
    match msg with
    | EditingFinished ->
        // get the content of the text field - we either tabbed or returned out of it
        state, cmdGetText "edit" Submitted
    | Submitted str ->
        // synthetic event created from EditingFinished above
        let nextValue =
            match tryParseDate str with
            | Some value ->
                Valid value
            | None ->
                if str = "" then
                    Empty
                else
                    Invalid str
        let cmd =
            if nextValue <> state.Value then
                Cmd.Signal (ValueChanged nextValue)
            else
                Cmd.None
        { state with Raw = str; Value = nextValue }, cmd
    | ShowCalendar ->
        state, execDialog "pickerDialog"
    | AcceptDialog ->
        let getSelectedDate =
            Cmd.ViewExec (fun bindings ->
                viewexec bindings {
                    let! widget = CalendarWidget.bindNode "calendarWidget"
                    return ValueChosen widget.SelectedDate
                })
        state, Cmd.Batch [ acceptDialog "pickerDialog"; getSelectedDate ]
    | RejectDialog ->
        state, rejectDialog "pickerDialog"
    | ValueChosen date ->
        let nextValue =
            Valid date
        let nextState =
            { state with Value = nextValue; Raw = date.ToShortDateString() }
        let cmd =
            if nextValue <> state.Value then
                Cmd.Signal (ValueChanged nextValue)
            else
                Cmd.None
        nextState, cmd

let view (state: State) =
    let edit =
        LineEdit(
            Name = "edit",
            Text = state.Raw,
            Enabled = state.Enabled,
            OnEditingFinished = EditingFinished,
            OnReturnPressed = EditingFinished)
    let button =
        PushButton(Text = "Pick", Enabled = state.Enabled, OnClicked = ShowCalendar)
    let dialog =
        let calendar =
            let date =
                match state.Value with
                | Valid date -> date
                | _ ->  DateOnly.FromDateTime(DateTime.Now)
            let node = 
                CalendarWidget(Name = "calendarWidget", SelectedDate = date)
            // optional settings ...
            state.MaybeMinDate
            |> Option.iter (fun minDate -> node.MinimumDate <- minDate)
            state.MaybeMaxDate
            |> Option.iter (fun maxDate -> node.MaximumDate <- maxDate)
            node
        let cancel =
            PushButton(Text = "Cancel", OnClicked = RejectDialog)
        let accept =
            PushButton(Text = "OK", OnClicked = AcceptDialog)
        let layout =
            let hbox =
                HBoxLayout(Items = [
                    BoxItem(cancel)
                    BoxItem(accept)
                ])
            VBoxLayout(Items = [
                BoxItem(calendar)
                BoxItem(hbox)
            ])
        Dialog(
            Name = "pickerDialog",
            Size = Size.From(320, 200),
            WindowTitle = state.DialogTitle, 
            Layout = layout)
    let hbox =
        BoxLayout(
            Direction = LeftToRight,
            Spacing = 4,
            ContentsMargins = (0, 0, 0, 0),
            Items = [
                BoxItem(edit)
                BoxItem(button)
            ],
            Attachments = [
                Attachment("pickerDialog", dialog)
            ])
    hbox :> ILayoutNode<Msg>
    
type DatePicker<'outerMsg>() =
    inherit LayoutReactorNode<'outerMsg, State, Msg, Signal>(init, update, view)
    
    let mutable onValueChanged: (Value -> 'outerMsg) option = None
    member this.OnValueChanged with set value = onValueChanged <- Some value
    
    override this.SignalMap (s: Signal) =
        match s with
        | ValueChanged value ->
            onValueChanged
            |> Option.map (fun f -> f value)
            
    member this.Value with set value =
        this.PushAttr(ComponentAttr(Value value, keyFunc, attrUpdate))
        
    member this.Enabled with set value =
        this.PushAttr(ComponentAttr(Enabled value, keyFunc, attrUpdate))
        
    member this.DialogTitle with set value =
        this.PushAttr(ComponentAttr(DialogTitle value, keyFunc, attrUpdate))
        
    member this.MinDate with set value =
        this.PushAttr(ComponentAttr(MinDate value, keyFunc, attrUpdate))
        
    member this.MaxDate with set value =
        this.PushAttr(ComponentAttr(MaxDate value, keyFunc, attrUpdate))
