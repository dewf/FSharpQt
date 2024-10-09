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

type private Attr =
    | Label of string
    | Enabled of bool
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
            | Label _ -> "fullattrcomponent:label"
            | Enabled _ -> "fullattrcomponent:enabled"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? ComponentStateTarget<State> as stateTarget ->
                let state =
                    stateTarget.State
                let nextState =
                    match this with
                    | Label text ->
                        if text <> state.Label then
                            { state with Label = text }
                        else
                            state
                    | Enabled value ->
                        if value <> state.Enabled then
                            { state with Enabled = value }
                        else
                            state
                stateTarget.Update(nextState)
            | _ ->
                failwith "nope"
    
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
        this.PushAttr(Label value)
        
    member this.Enabled with set value =
        this.PushAttr(Enabled value)
