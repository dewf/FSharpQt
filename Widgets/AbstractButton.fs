module FSharpQt.Widgets.AbstractButton

open System
open FSharpQt.Attrs
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes

type private Signal =
    | Clicked
    | ClickedWithChecked of checked_: bool
    | Pressed
    | Released
    | Toggled of checked_: bool

type internal Attr =
    | AutoExclusive of state: bool
    | AutoRepeat of state: bool
    | AutoRepeatDelay of delay: int
    | AutoRepeatInterval of interval: int
    | Checkable of state: bool
    | Checked of state: bool
    | Down of state: bool
    | IconAttr of icon: Icon
    | IconSize of size: Size
    | Shortcut of seq: KeySequence
    | Text of text: string
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
            | AutoExclusive _ -> "abstractbutton:autoexclusive"
            | AutoRepeat _ -> "abstractbutton:autorepeat"
            | AutoRepeatDelay _ -> "abstractbutton:repeatdelay"
            | AutoRepeatInterval _ -> "abstractbutton:repeatinterval"
            | Checkable _ -> "abstractbutton:checkable"
            | Checked _ -> "abstractbutton:checked"
            | Down _ -> "abstractbutton:down"
            | IconAttr _ -> "abstractbutton:iconattr"
            | IconSize _ -> "abstractbutton:iconsize"
            | Shortcut _ -> "abstractbutton:shortcut"
            | Text _ -> "abstractbutton:text"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyAbstractButtonAttr(this)
            | _ ->
                printfn "warning: AbstractButton.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyAbstractButtonAttr: Attr -> unit
    end
                
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
    
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onClicked: 'msg option = None
    let mutable onClickedWithChecked: (bool -> 'msg) option = None
    let mutable onPressed: 'msg option = None
    let mutable onReleased: 'msg option = None
    let mutable onToggled: (bool -> 'msg) option = None

    member internal this.SignalMask = enum<AbstractButton.SignalMask> (int this._signalMask)

    member this.OnClicked with set value =
        onClicked <- Some value
        this.AddSignal(int AbstractButton.SignalMask.Clicked)
        
    member this.OnClickedWithChecked with set value =
        onClickedWithChecked <- Some value
        this.AddSignal(int AbstractButton.SignalMask.Clicked)
        
    member this.OnPressed with set value =
        onPressed <- Some value
        this.AddSignal(int AbstractButton.SignalMask.Pressed)
        
    member this.OnReleased with set value =
        onReleased <- Some value
        this.AddSignal(int AbstractButton.SignalMask.Released)
        
    member this.OnToggled with set value =
        onToggled <- Some value
        this.AddSignal(int AbstractButton.SignalMask.Toggled)

    member internal this.SignalMapList =
        let thisFunc = function
            | Clicked -> onClicked
            | ClickedWithChecked checked_ ->
                onClickedWithChecked
                |> Option.map (fun f -> f checked_)
            | Pressed -> onPressed
            | Released -> onReleased
            | Toggled state ->
                onToggled
                |> Option.map (fun f -> f state)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
    
    member this.AutoExclusive with set value =
        this.PushAttr(AutoExclusive value)
        
    member this.AutoRepeat with set value =
        this.PushAttr(AutoRepeat value)
        
    member this.AutoRepeatDelay with set value =
        this.PushAttr(AutoRepeatDelay value)
        
    member this.AutoRepeatInterval with set value =
        this.PushAttr(AutoRepeatInterval value)

    member this.Checkable with set value =
        this.PushAttr(Checkable value)
        
    member this.Checked with set value =
        this.PushAttr(Checked value)
        
    member this.Down with set value =
        this.PushAttr(Down value)
        
    member this.Icon with set value =
        this.PushAttr(IconAttr value)
        
    member this.IconSize with set value =
        this.PushAttr(IconSize value)
        
    member this.Shortcut with set value =
        this.PushAttr(Shortcut value)
        
    member this.Text with set value =
        this.PushAttr(Text value)

type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable abstractButton: AbstractButton.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    // let mutable currentMask = enum<AbstractButton.SignalMask> 0

    // binding guards
    let mutable checked_ = false
    let mutable pressed = false
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
    
    member this.AbstractButton
        with get() = abstractButton
        and set value =
            // must assign to base
            this.Widget <- value
            abstractButton <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "AbstractButton.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "AbstractButton.ModelCore: signal map assignment didn't have a head element"
    
    // no SignalMask property needed, because AbstractButtons can't be instantiated
    // (therefore don't use their SignalMask/Handler directly)

    interface AttrTarget with
        member this.ApplyAbstractButtonAttr attr =
            match attr with
            | AutoExclusive state ->
                abstractButton.SetAutoExclusive(state)
            | AutoRepeat state ->
                abstractButton.SetAutoRepeat(state)
            | AutoRepeatDelay delay ->
                abstractButton.SetAutoRepeatDelay(delay)
            | AutoRepeatInterval interval ->
                abstractButton.SetAutoRepeatInterval(interval)
            | Checkable state ->
                abstractButton.SetCheckable(state)
            | Checked state ->
                if state <> checked_ then
                    checked_ <- state
                    abstractButton.SetChecked(state)
            | Down state ->
                if state <> pressed then
                    pressed <- state
                    abstractButton.SetDown(state)
            | IconAttr icon ->
                abstractButton.SetIcon(icon.QtValue)
            | IconSize size ->
                abstractButton.SetIconSize(size.QtValue)
            | Shortcut seq ->
                abstractButton.SetShortcut(seq.QtValue)
            | Text text ->
                abstractButton.SetText(text)

    interface AbstractButton.SignalHandler with
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
        // AbstractButton =================
        member this.Clicked checkState =
            checked_ <- checkState
            signalDispatch Clicked
            signalDispatch (ClickedWithChecked checkState)
        member this.Pressed() =
            pressed <- true
            signalDispatch Pressed
        member this.Released() =
            pressed <- false
            signalDispatch Released
        member this.Toggled checkState =
            checked_ <- checkState
            signalDispatch (Toggled checkState)

    interface IDisposable with
        member this.Dispose() =
            abstractButton.Dispose()
