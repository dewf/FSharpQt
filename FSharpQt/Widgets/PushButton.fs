module FSharpQt.Widgets.PushButton

open FSharpQt.BuilderNode
open System
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs

type private Signal = unit
    
type internal Attr =
    | AutoDefault of state: bool
    | Default of state: bool
    | Flat of state: bool
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
            | AutoDefault _ -> "pushbutton:autodefault"
            | Default _ -> "pushbutton:default"
            | Flat _ -> "pushbutton:flat"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyPushButtonAttr this
            | _ ->
                printfn "warning: PushButton.Attr couldn't ApplyTo() unknown target type [%A]" target
    
and internal AttrTarget =
    interface
        inherit AbstractButton.AttrTarget
        abstract member ApplyPushButtonAttr: Attr -> unit
    end
                
type Props<'msg>() =
    inherit AbstractButton.Props<'msg>()
    
    member internal this.SignalMask = enum<PushButton.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        // prepend to parent signal map funcs
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
    
    member this.AutoDefault with set value =
        this.PushAttr(AutoDefault value)
        
    member this.Default with set value =
        this.PushAttr(Default value)
        
    member this.Flat with set value =
        this.PushAttr(Flat value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractButton.ModelCore<'msg>(dispatch)
    let mutable pushButton: PushButton.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<PushButton.SignalMask> 0
    
    // no signals, so no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.PushButton
        with get() =
            pushButton
        and set value =
            // must assign to base
            this.AbstractButton <- value
            pushButton <- value
    
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                // no signals in PushButton
                ()
            | _ ->
                failwithf "PushButton.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "PushButton.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            pushButton.SetSignalMask(value)
            currentMask <- value

    interface AttrTarget with
        member this.ApplyPushButtonAttr attr =
            match attr with
            | AutoDefault state ->
                pushButton.SetAutoDefault(state)
            | Default state ->
                pushButton.SetDefault(state)
            | Flat state ->
                pushButton.SetFlat(state)
        
    interface PushButton.SignalHandler with
        // object =========================
        member this.Destroyed(obj: Object.Handle) =
            (this :> Object.SignalHandler).Destroyed(obj)
        member this.ObjectNameChanged(name: string) =
            (this :> Object.SignalHandler).ObjectNameChanged(name)
        // widget =========================
        member this.CustomContextMenuRequested pos =
            (this :> Widget.SignalHandler).CustomContextMenuRequested pos
        member this.WindowIconChanged icon =
            (this :> Widget.SignalHandler).WindowIconChanged icon
        member this.WindowTitleChanged title =
            (this :> Widget.SignalHandler).WindowTitleChanged title
        // AbstractButton:
        member this.Clicked checkState =
            (this :> AbstractButton.SignalHandler).Clicked checkState
        member this.Pressed() =
            (this :> AbstractButton.SignalHandler).Pressed()
        member this.Released() =
            (this :> AbstractButton.SignalHandler).Released()
        member this.Toggled checkState =
            (this :> AbstractButton.SignalHandler).Toggled checkState
        // none of our own
        
    interface IDisposable with
        member this.Dispose() =
            pushButton.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.PushButton <- PushButton.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: PushButton.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: PushButton.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type PushButton<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> PushButton<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.PushButton
            
        override this.ContentKey =
            this.model.PushButton
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
