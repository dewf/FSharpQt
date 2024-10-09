module SimpleAttrComponent

open FSharpQt.BuilderNode
open FSharpQt.MiscTypes
open FSharpQt.Reactor
open FSharpQt.Widgets.PushButton

type Signal =
    | ClickedFiveTimes

type Attr =
    | Label of string
    | Enabled of bool
    
let attrKey = function
    | Label _ -> 0
    | Enabled _ -> 1
    
type State = {
    ClickCount: int
    Label: string
    Enabled: bool
}
    
type Msg =
    | Clicked
    
let init () =
    { ClickCount = 0; Label = "default"; Enabled = true }, Cmd.None
    
let attrUpdate (state: State) (attr: Attr) =
    match attr with
    | Label s ->
        { state with Label = s }
    | Enabled value ->
        { state with Enabled = value }
    
let update (state: State) (msg: Msg) =
    match msg with
    | Clicked ->
        let nextCount =
            state.ClickCount + 1
        let cmd =
            if nextCount = 5 then
                Cmd.Signal ClickedFiveTimes
            else
                Cmd.None
        { state with ClickCount = nextCount }, cmd
        
let view state =
    let label =
        sprintf "SimpleAttrComponent (%d)" state.ClickCount
    PushButton(Text = label,
               MinimumSize = Size.From(320, 64),
               Enabled = state.Enabled,
               OnClicked = Clicked)
    :> IWidgetNode<Msg>

type SimpleAttrComponent<'outerMsg>() =
    inherit WidgetReactorNode<'outerMsg, State, Msg, Signal>(init, update, view)
    
    let mutable onClickedFiveTimes: 'outerMsg option = None
    member this.OnClickedFiveTimes with set value = onClickedFiveTimes <- Some value
    
    override this.SignalMap (s: Signal) =
        match s with
        | ClickedFiveTimes -> onClickedFiveTimes
            
    member this.Attrs with set value =
        this.PushAttr(EasyAttrs(value, "attrs01", attrKey, attrUpdate)) // IIRC the text key would be used if we had multiple sets of easy attrs, to distinguish them during diffing
