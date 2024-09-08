module DatePicker

open System
open FSharpQt
open BuilderNode
open FSharpQt.Attrs
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
    | Invalid
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

type Attr =
    | Value of value: Value
    | Enabled of value: bool
    | DialogTitle of title: string
    | MinDate of value: DateOnly
    | MaxDate of value: DateOnly
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
            | Value _ -> "datepicker:value"
            | Enabled _ -> "datepicker:enabled"
            | DialogTitle _ -> "datepicker:dialogtitle"
            | MinDate _ -> "datepicker:mindate"
            | MaxDate _ -> "datepicker:maxdate"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? ComponentStateTarget<State> as stateTarget ->
                let state =
                    stateTarget.State
                let nextState =
                    match this with
                    | Value value ->
                        if value <> state.Value then
                            match value with
                            | Empty ->
                                { state with Value = Empty; Raw = "" }
                            | Invalid ->
                                // unlikely to be assigned from outside except as a 2-way echo
                                // we'd only get here if the parent component explicitly changed the value to invalid
                                { state with Value = Invalid; Raw = "??invalid??" }
                            | Valid dt ->
                                { state with Value = Valid dt; Raw = dt.ToShortDateString() }
                        else
                            state
                    | Enabled value ->
                        { state with Enabled = value }
                    | DialogTitle title ->
                        { state with DialogTitle = title }
                    | MinDate date ->
                        { state with MaybeMinDate = Some date }
                    | MaxDate date ->
                        { state with MaybeMaxDate = Some date }
                stateTarget.Update(nextState)
            | _ ->
                failwith "nope"

type Msg =
    | EditChanged of str: string
    | EditSubmitted
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
    | EditChanged str ->
        let nextValue =
            match tryParseDate str with
            | Some value ->
                Valid value
            | None ->
                if str = "" then
                    Empty
                else
                    Invalid
        let cmd =
            if nextValue <> state.Value then
                Cmd.Signal (ValueChanged nextValue)
            else
                Cmd.None
        { state with Raw = str; Value = nextValue }, cmd
    | EditSubmitted ->
        state, Cmd.None
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
        LineEdit(Text = state.Raw, Enabled = state.Enabled, OnTextChanged = EditChanged, OnReturnPressed = EditSubmitted)
    let button =
        PushButton(Text = "Pick", Enabled = state.Enabled, OnClicked = ShowCalendar)
    let dialog =
        let calendar =
            let node = 
                CalendarWidget(Name = "calendarWidget")
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
        this.PushAttr(Value value)
        
    member this.Enabled with set value =
        this.PushAttr(Enabled value)
        
    member this.DialogTitle with set value =
        this.PushAttr(DialogTitle value)
        
    member this.MinDate with set value =
        this.PushAttr(MinDate value)
        
    member this.MaxDate with set value =
        this.PushAttr(MaxDate value)
