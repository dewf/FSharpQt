open System
open FSharpQt
open FSharpQt.BuilderNode
open FSharpQt.Reactor
open FSharpQt.Widgets.BoxLayout
open FSharpQt.Widgets.GroupBox
open FSharpQt.Widgets.MainWindow
open FSharpQt.Widgets.PushButton
open FSharpQt.Widgets
open FSharpQt.Widgets.RadioButton
open FSharpQt.Widgets.Slider
open FSharpQt.Widgets.ScrollArea

open Painting
open Renderer
open MiscTypes

type Signal = unit

type State = {
    CapStyle: CapStyle
    JoinStyle: JoinStyle
    PenStyle: PenStyle
    PenWidth: int
    LineStyle: LineStyle
    Animating: bool
}

type Msg =
    | SetCapStyle of style: CapStyle
    | SetJoinStyle of style: JoinStyle
    | SetPenStyle of style: PenStyle
    | SetPenWidth of width: int
    | SetLineStyle of style: LineStyle
    | SetAnimating of value: bool

let init() =
    let state = {
        CapStyle = Flat
        JoinStyle = Bevel
        PenStyle = SolidLine
        PenWidth = 2
        LineStyle = Curves
        Animating = true
    }
    state, Cmd.None

let update (state: State) (msg: Msg) =
    let nextState =
        match msg with
        | SetCapStyle style ->
            { state with CapStyle = style }
        | SetJoinStyle style ->
            { state with JoinStyle = style }
        | SetPenStyle style ->
            { state with PenStyle = style }
        | SetPenWidth width ->
            { state with PenWidth = width }
        | SetLineStyle style ->
            { state with LineStyle = style }
        | SetAnimating value ->
            { state with Animating = value }
    nextState, Cmd.None
    
let radioGroup (title: string) (items: (string * 'a) list) (setterMsgFunc: 'a -> Msg) (selected: 'a) =
    let buttons =
        items
        |> List.map (fun (label, value) ->
            RadioButton(
                Text = label,
                Checked = (value = selected),
                OnClicked = setterMsgFunc value))
    let vbox =
        let items =
            buttons
            |> List.map BoxItem
        VBoxLayout(Items = items)
    GroupBox(Title = title, Layout = vbox)
        
    
let view (state: State) =
    let capStyleGroup =
        let items =
            [ "Flat", Flat
              "Square", Square
              "Round", CapStyle.Round ]
        radioGroup "Cap Style" items SetCapStyle state.CapStyle
        
    let joinStyleGroup =
        let items =
            [ "Bevel", Bevel
              "Miter", Miter
              "SvgMiter", SvgMiter
              "Round", JoinStyle.Round ]
        radioGroup "Join Style" items SetJoinStyle state.JoinStyle
        
    let penStyleGroup =
        let items =
            [ "Solid", SolidLine
              "Dash", DashLine
              "Dot", DotLine
              "DashDot", DashDotLine
              "DashDotDot", DashDotDotLine
              "Custom", CustomDashLine ]
        radioGroup "Pen Style" items SetPenStyle state.PenStyle
        
    let penWidthGroup =
        let slider =
            Slider(Orientation = Horizontal, Range = (1, 20), Value = state.PenWidth, OnValueChanged = SetPenWidth)
        let vbox =
            VBoxLayout(Items = [ BoxItem(slider) ])
        GroupBox(Title = "Pen Width", Layout = vbox)
        
    let lineStyleGroup =
        let items =
            [ "Curves", Curves
              "Lines", Lines ]
        radioGroup "Line Style" items SetLineStyle state.LineStyle
        
    let animateButton =
        PushButton(
                Checkable = true,
                Checked = state.Animating,
                Text = (if state.Animating then "Animating" else "Not Animating"),
                OnToggled = SetAnimating)
        
    let rightPanel =
        VBoxLayout(Items = [
                      BoxItem(capStyleGroup)
                      BoxItem(joinStyleGroup)
                      BoxItem(penStyleGroup)
                      BoxItem(penWidthGroup)
                      BoxItem(lineStyleGroup)
                      BoxItem(animateButton)
                      BoxItem(stretch = 1)
                  ])
        
    let scrollArea =
        ScrollArea(VerticalScrollBarPolicy = AbstractScrollArea.ScrollBarAlwaysOn, Content = rightPanel)
        
    let renderer =
        PathStrokeRenderer(
            CapStyle = state.CapStyle,
            JoinStyle = state.JoinStyle,
            PenStyle = state.PenStyle,
            PenWidth = state.PenWidth,
            LineStyle = state.LineStyle,
            Animating = state.Animating)

    let hbox =
        HBoxLayout(Items = [
                      BoxItem(renderer, 1)
                      BoxItem(scrollArea)
                  ])
        
    MainWindow(WindowTitle = "Path Stroking", CentralLayout = hbox)
    :> IBuilderNode<Msg>

[<EntryPoint>]
[<STAThread>]
let main argv =
    use app =
        createApplication init update view
    app.SetStyle(Fusion)
    app.Run argv
