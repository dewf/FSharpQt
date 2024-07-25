module FSharpQt.Widgets.AbstractSlider

open System
open FSharpQt.Attrs
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type SliderAction =
    | SliderNoAction
    | SliderSingleStepAdd
    | SliderSingleStepSub
    | SliderPageStepAdd
    | SliderPageStepSub
    | SliderToMinimum
    | SliderToMaximum
    | SliderMove
with
    static member internal FromQtValue (qtAction: AbstractSlider.SliderAction) =
        match qtAction with
        | AbstractSlider.SliderAction.SliderNoAction -> SliderNoAction
        | AbstractSlider.SliderAction.SliderSingleStepAdd -> SliderSingleStepAdd
        | AbstractSlider.SliderAction.SliderSingleStepSub -> SliderSingleStepSub
        | AbstractSlider.SliderAction.SliderPageStepAdd -> SliderPageStepAdd
        | AbstractSlider.SliderAction.SliderPageStepSub -> SliderPageStepSub
        | AbstractSlider.SliderAction.SliderToMinimum -> SliderToMinimum
        | AbstractSlider.SliderAction.SliderToMaximum -> SliderToMaximum
        | AbstractSlider.SliderAction.SliderMove -> SliderMove
        | _ -> failwith "SliderAction.FromQtValue: unknown input value"

type private Signal =
    | ActionTriggered of action: SliderAction
    | RangeChanged of min: int * max: int
    | SliderMoved of value: int
    | SliderPressed
    | SliderReleased
    | ValueChanged of value: int
    
type internal Attr =
    | InvertedAppearance of state: bool
    | InvertedControls of state: bool
    | Maximum of value: int
    | Minimum of value: int
    | Orientation of orient: Orientation
    | PageStep of step: int
    | SingleStep of step: int
    | SliderDown of state: bool
    | SliderPosition of pos: int
    | Tracking of state: bool
    | Value of value: int
    // custom:
    | Range of min: int * max: int
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
            | InvertedAppearance _ -> "abstractslider:invertedappearance"
            | InvertedControls _ -> "abstractslider:invertedcontrols"
            | Maximum _ -> "abstractslider:maximum"
            | Minimum _ -> "abstractslider:minimum"
            | Orientation _ -> "abstractslider:orientation"
            | PageStep _ -> "abstractslider:pagestep"
            | SingleStep _ -> "abstractslider:singlestep"
            | SliderDown _ -> "abstractslider:sliderdown"
            | SliderPosition _ -> "abstractslider:sliderposition"
            | Tracking _ -> "abstractslider:tracking"
            | Value _ -> "abstractslider:value"
            | Range _ -> "abstractslider:range"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyAbstractSliderAttr this
            | _ ->
                printfn "warning: AbstractSlider.Attr couldn't ApplyTo() unknown target type [%A]" target
    
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyAbstractSliderAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onActionTriggered: (SliderAction -> 'msg) option = None
    let mutable onRangeChanged: (int * int -> 'msg) option = None
    let mutable onSliderMoved: (int -> 'msg) option = None
    let mutable onSliderPressed: 'msg option = None
    let mutable onSliderReleased: 'msg option = None
    let mutable onValueChanged: (int -> 'msg) option = None

    // member internal this.SignalMask = enum<AbstractSlider.SignalMask> (int this._signalMask)

    member this.OnActionTriggered with set value =
        onActionTriggered <- Some value
        this.AddSignal(int AbstractSlider.SignalMask.ActionTriggered)

    member this.OnRangeChanged with set value =
        onRangeChanged <- Some value
        this.AddSignal(int AbstractSlider.SignalMask.RangeChanged)
        
    member this.OnSliderMoved with set value =
        onSliderMoved <- Some value
        this.AddSignal(int AbstractSlider.SignalMask.SliderMoved)
        
    member this.OnSliderPressed with set value =
        onSliderPressed <- Some value
        this.AddSignal(int AbstractSlider.SignalMask.SliderPressed)
        
    member this.OnSliderReleased with set value =
        onSliderReleased <- Some value
        this.AddSignal(int AbstractSlider.SignalMask.SliderReleased)
        
    member this.OnValueChanged with set value =
        onValueChanged <- Some value
        this.AddSignal(int AbstractSlider.SignalMask.ValueChanged)

    member internal this.SignalMapList =
        let thisFunc = function
            | ActionTriggered action ->
                onActionTriggered
                |> Option.map (fun f -> f action)
            | RangeChanged (min, max) ->
                onRangeChanged
                |> Option.map (fun f -> f (min, max))
            | SliderMoved value ->
                onSliderMoved
                |> Option.map (fun f -> f value)
            | SliderPressed ->
                onSliderPressed
            | SliderReleased ->
                onSliderReleased
            | ValueChanged value ->
                onValueChanged
                |> Option.map (fun f -> f value)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.InvertedAppearance with set value =
        this.PushAttr(InvertedAppearance value)

    member this.InvertedControls with set value =
        this.PushAttr(InvertedControls value)

    member this.Maximum with set value =
        this.PushAttr(Maximum value)

    member this.Minimum with set value =
        this.PushAttr(Minimum value)

    member this.Orientation with set value =
        this.PushAttr(Orientation value)

    member this.PageStep with set value =
        this.PushAttr(PageStep value)

    member this.SingleStep with set value =
        this.PushAttr(SingleStep value)

    member this.SliderDown with set value =
        this.PushAttr(SliderDown value)

    member this.SliderPosition with set value =
        this.PushAttr(SliderPosition value)

    member this.Tracking with set value =
        this.PushAttr(Tracking value)

    member this.Value with set value =
        this.PushAttr(Value value)

    member this.Range with set value =
        this.PushAttr(Range value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable abstractSlider: AbstractSlider.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // no mask because abstract
    
    // binding guards
    let mutable lastMinimum = Int32.MinValue
    let mutable lastMaximum = Int32.MinValue
    let mutable lastSliderPos = Int32.MinValue
    let mutable lastSliderDown = false
    let mutable lastValue = Int32.MinValue
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.AbstractSlider
        with get() = abstractSlider
        and set value =
            // assign to base
            this.Widget <- value
            abstractSlider <- value
    
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "AbstractSlider.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractSlider.ModelCore: signal map assignment didn't have a head element"
    
    // no signal mask setter because abstract
    interface AttrTarget with
        member this.ApplyAbstractSliderAttr attr =
            match attr with
            | InvertedAppearance state ->
                abstractSlider.SetInvertedAppearance(state)
            | InvertedControls state ->
                abstractSlider.SetInvertedControls(state)
            | Maximum value ->
                if value <> lastMaximum then
                    lastMaximum <- value
                    abstractSlider.SetMaximum(value)
            | Minimum value ->
                if value <> lastMinimum then
                    lastMinimum <- value
                    abstractSlider.SetMinimum(value)
            | Orientation orient ->
                abstractSlider.SetOrientation(orient.QtValue)
            | PageStep step ->
                abstractSlider.SetPageStep(step)
            | SingleStep step ->
                abstractSlider.SetSingleStep(step)
            | SliderDown state ->
                if state <> lastSliderDown then
                    lastSliderDown <- state
                    abstractSlider.SetSliderDown(state)
            | SliderPosition pos ->
                if pos <> lastSliderPos then
                    lastSliderPos <- pos
                    abstractSlider.SetSliderPosition(pos)
            | Tracking state ->
                abstractSlider.SetTracking(state)
            | Value value ->
                if value <> lastValue then
                    lastValue <- value
                    abstractSlider.SetValue(value)
            | Range (min, max) ->
                if min <> lastMinimum || max <> lastMaximum then
                    lastMinimum <- min
                    lastMaximum <- max
                    abstractSlider.SetRange(min, max)
                    
    interface AbstractSlider.SignalHandler with
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
        // AbstractSlider =================
        member this.ActionTriggered qtAction =
            signalDispatch (SliderAction.FromQtValue qtAction |> ActionTriggered)
        member this.RangeChanged (min, max) =
            lastMinimum <- min
            lastMaximum <- max
            signalDispatch (RangeChanged (min, max))
        member this.SliderMoved pos =
            lastSliderPos <- pos
            signalDispatch (SliderMoved pos)
        member this.SliderPressed() =
            lastSliderDown <- true
            signalDispatch SliderPressed
        member this.SliderReleased() =
            lastSliderDown <- false
            signalDispatch SliderReleased
        member this.ValueChanged value =
            lastValue <- value
            signalDispatch (ValueChanged value)

    interface IDisposable with
        member this.Dispose() =
            abstractSlider.Dispose()
