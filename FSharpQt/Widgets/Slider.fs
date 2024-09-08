module FSharpQt.Widgets.Slider

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

type Signal = unit

type TickPosition =
    | NoTicks
    | TicksAbove
    | TicksLeft
    | TicksBelow
    | TicksRight
    | TicksBothSides
with
    member internal this.QtValue =
        match this with
        | NoTicks -> Slider.TickPosition.NoTicks
        | TicksAbove -> Slider.TickPosition.TicksAbove
        | TicksLeft -> Slider.TickPosition.TicksLeft
        | TicksBelow -> Slider.TickPosition.TicksBelow
        | TicksRight -> Slider.TickPosition.TicksRight
        | TicksBothSides -> Slider.TickPosition.TicksBothSides

type internal Attr =
    | TickInterval of interval: int
    | TickPosition of pos: TickPosition
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
            | TickInterval _ -> "slider:tickinterval"
            | TickPosition _ -> "slider:tickposition"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplySliderAttr(this)
            | _ ->
                printfn "warning: LineEdit.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractSlider.AttrTarget
        abstract member ApplySliderAttr: Attr -> unit
    end

type Props<'msg>() =
    inherit AbstractSlider.Props<'msg>()
    
    member internal this.SignalMask = enum<Slider.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        // no signals of our own
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList

    member this.TickInterval with set value =
        this.PushAttr(TickInterval value)
        
    member this.TickPosition with set value =
        this.PushAttr(TickPosition value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractSlider.ModelCore<'msg>(dispatch)
    let mutable slider: Slider.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Slider.SignalMask> 0
    
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Slider
        with get() = slider
        and set value =
            // assign to base
            this.AbstractSlider <- value
            slider <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                // nothing to assign
                ()
            | _ ->
                failwith "Slider.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "Slider.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            slider.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplySliderAttr attr =
            match attr with
            | TickInterval interval ->
                slider.SetTickInterval(interval)
            | TickPosition pos ->
                slider.SetTickPosition(pos.QtValue)
                
    interface Slider.SignalHandler with
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
            (this :> AbstractSlider.SignalHandler).ActionTriggered qtAction
        member this.RangeChanged (min, max) =
            (this :> AbstractSlider.SignalHandler).RangeChanged (min, max)
        member this.SliderMoved pos =
            (this :> AbstractSlider.SignalHandler).SliderMoved(pos)
        member this.SliderPressed() =
            (this :> AbstractSlider.SignalHandler).SliderPressed()
        member this.SliderReleased() =
            (this :> AbstractSlider.SignalHandler).SliderReleased()
        member this.ValueChanged value =
            (this :> AbstractSlider.SignalHandler).ValueChanged value
        // Slider =========================
        // (none)
        
    interface IDisposable with
        member this.Dispose() =
            slider.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.Slider <- Slider.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Slider.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Slider.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type Slider<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
        
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> Slider<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.Slider
            
        override this.ContentKey =
            this.model.Slider
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
