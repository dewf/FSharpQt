module FSharpQt.Widgets.Timer

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | Timeout
    // custom:
    | TimeoutWithElapsed of elapsed: double // millis
    
type internal Attr =
    | Interval of millis: int
    | SingleShot of state: bool
    | TimerType of timerType: TimerType
    // custom:
    | Running of state: bool // start/stop methods
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
            | Interval _ -> "timer:interval"
            | SingleShot _ -> "timer:singleshot"
            | TimerType _ -> "timer:timertype"
            | Running _ -> "timer:running"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyTimerAttr(this)
            | _ ->
                printfn "warning: Timer.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit QObject.AttrTarget
        abstract member ApplyTimerAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit QObject.Props<'msg>()
    
    let mutable onTimeout: 'msg option = None
    let mutable onTimeoutWithElapsed: (double -> 'msg) option = None
    
    member internal this.SignalMask = enum<Timer.SignalMask> (int this._signalMask)
    
    member this.OnTimeout with set value =
        onTimeout <- Some value
        this.AddSignal(int Timer.SignalMask.Timeout) // timeout v1
        
    member this.OnTimeoutWithElapsed with set value =
        onTimeoutWithElapsed <- Some value
        this.AddSignal(int Timer.SignalMask.Timeout) // timeout v2
        
    member internal this.SignalMapList =
        let thisFunc = function
            | Timeout ->
                onTimeout
            | TimeoutWithElapsed elapsed ->
                onTimeoutWithElapsed
                |> Option.map (fun f -> f elapsed)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

    member this.Interval with set value =
        this.PushAttr(Interval value)
        
    member this.SingleShot with set value =
        this.PushAttr(SingleShot value)

    member this.TimerType with set value =
        this.PushAttr(TimerType value)

    member this.Running with set value =
        this.PushAttr(Running value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit QObject.ModelCore<'msg>(dispatch)
    let mutable timer: Timer.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Timer.SignalMask> 0
    
    let mutable lastTicks = 0L
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
    
    member this.Timer
        with get() = timer
        and set value =
            this.Object <- value
            timer <- value

    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "Timer.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "Timer.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            timer.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyTimerAttr attr =
            match attr with
            | Interval millis ->
                timer.SetInterval(millis)
            | SingleShot state ->
                timer.SetSingleShot(state)
            | TimerType timerType ->
                timer.SetTimerType(timerType.QtValue)
            | Running state ->
                if state then
                    timer.Start()
                    lastTicks <- DateTime.Now.Ticks
                else
                    timer.Stop()
                    
    interface Timer.SignalHandler with
        // Object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // Timer ==========================
        member this.Timeout () =
            let ticks =
                DateTime.Now.Ticks
            let elapsed =
                double (ticks - lastTicks) / double TimeSpan.TicksPerMillisecond
            lastTicks <- ticks
            signalDispatch Timeout
            signalDispatch (TimeoutWithElapsed elapsed)
                    
    interface IDisposable with
        member this.Dispose() =
            timer.Dispose()
            
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.Timer <- Timer.Create(this)
            
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Timer.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Timer.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type Timer<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface INonVisualNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> Timer<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.ContentKey =
            this.model.Timer
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
