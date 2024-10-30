module FullAttrComponent

open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.MiscTypes
open FSharpQt.Reactor
open FSharpQt.Widgets.PushButton

type Signal =
    | ClickedFiveTimes
    
type State = {
    ClickCount: int
    Label: string
    Enabled: bool
}

type LabelAttr(value: string) =
    inherit ComponentAttrBase<string, State>(value)
    override this.Key = "fullattrcomponent:label"
    override this.Update state =
        { state with Label = value }
        
type EnabledAttr(value: bool) =
    inherit ComponentAttrBase<bool, State>(value)
    override this.Key = "fullattrcomponent:enabled"
    override this.Update state =
        { state with Enabled = value }
        
// // if you have a lot of attributes, it's probably easier to declare them this way:
// type private Attr =
//     | Label of string
//     | Enabled of bool
//
// let private keyFunc = function
//     | Label _ -> "fullattrcomponent:label"
//     | Enabled _ -> "fullattrcomponent:enabled"
//
// let private updateFunc state = function
//     | Label text ->
//         { state with Label = text }
//     | Enabled value ->
//         { state with Enabled = value }
//         
// // use with: ComponentAttr(value, keyFunc, updateFunc)

type Msg =
    | Clicked
    
let init () =
    { ClickCount = 0; Label = "default"; Enabled = true }, Cmd.None
    
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
        sprintf "FullAttrComponent (%d)" state.ClickCount
    PushButton(Text = label,
               MinimumSize = Size.From(320, 64),
               Enabled = state.Enabled,
               OnClicked = Clicked)
    :> IWidgetNode<Msg>

type FullAttrComponent<'outerMsg>() =
    inherit WidgetReactorNode<'outerMsg, State, Msg, Signal>(init, update, view)
    
    let mutable onClickedFiveTimes: 'outerMsg option = None
    member this.OnClickedFiveTimes with set value = onClickedFiveTimes <- Some value
    
    override this.SignalMap (s: Signal) =
        match s with
        | ClickedFiveTimes -> onClickedFiveTimes
        
    member this.Label with set value =
        // per-attribute class:
        this.PushAttr(LabelAttr(value))
        // using a single DU with key/update functions:
        // this.PushAttr(ComponentAttr(Label value, keyFunc, updateFunc))
        
    member this.Enabled with set value =
        // per-attribute class:
        this.PushAttr(EnabledAttr(value))
        // using a single DU with key/update functions:
        // this.PushAttr(ComponentAttr(Enabled value, keyFunc, updateFunc))
