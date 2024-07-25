module FSharpQt.Widgets.StatusBar

open FSharpQt.Attrs
open FSharpQt.BuilderNode
open System
open FSharpQt.ModelBindings
open FSharpQt.Reactor
open Org.Whatever.MinimalQtForFSharp

type private Signal =
    | MessageChanged of text: string option     // hopefully this fires when it disappears?
    
type internal Attr =
    | SizeGripEnabled of enabled: bool
    | Message of maybeText: string option
    | MessageWithTimeout of maybeTextAndTimeout: (string * int) option
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
            | SizeGripEnabled _ -> "statusbar:sizegripenabled"
            | Message _ -> "statusbar:message"
            | MessageWithTimeout _ -> "statusbar:messagewithtimeout"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyStatusBarAttr(this)
            | _ ->
                printfn "warning: StatusBar.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyStatusBarAttr: Attr -> unit
    end
            
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onMessageChanged: (string option -> 'msg) option = None
    
    member internal this.SignalMask = enum<StatusBar.SignalMask> (int this._signalMask)
    
    member this.OnMessageChanged with set value =
        onMessageChanged <- Some value
        this.AddSignal(int StatusBar.SignalMask.MessageChanged)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | MessageChanged maybeMsg ->
                onMessageChanged
                |> Option.map (fun f -> f maybeMsg)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.SizeGripEnabled with set value =
        this.PushAttr(SizeGripEnabled value)
        
    member this.Message with set value =
        this.PushAttr(Message value)
        
    member this.MessageWithTimeout with set value =
        this.PushAttr(MessageWithTimeout value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable statusBar: StatusBar.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<StatusBar.SignalMask> 0
    
    // binding guards
    let mutable lastMessage: string option = None
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.StatusBar
        with get() = statusBar
        and set value =
            // assign to base
            this.Widget <- value
            statusBar <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "StatusBar.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "StatusBar.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            statusBar.SetSignalMask(value)
            currentMask <- value
        
    interface AttrTarget with
        member this.ApplyStatusBarAttr attr =
            let updateMessage (maybeText: string option) (timeout: int) =
                if maybeText <> lastMessage then
                    lastMessage <- maybeText
                    match maybeText with
                    | Some text ->
                        statusBar.ShowMessage(text, timeout)
                    | None ->
                        statusBar.ClearMessage()
            match attr with
            | SizeGripEnabled enabled ->
                statusBar.SetSizeGripEnabled(enabled)
            | Message maybeText ->
                updateMessage maybeText 0
            | MessageWithTimeout maybeTextAndTimeout ->
                let maybeText, timeout =
                    match maybeTextAndTimeout with
                    | Some (text, timeout) ->
                        Some text, timeout
                    | None ->
                        None, 0
                updateMessage maybeText timeout
                        
    interface StatusBar.SignalHandler with
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
        // StatusBar ======================
        member this.MessageChanged text =
            let msg = if text = "" then None else Some text
            lastMessage <- msg
            signalDispatch (MessageChanged msg)
            
    interface IDisposable with
        member this.Dispose() =
            statusBar.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.StatusBar <- StatusBar.Create(this)
            
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: StatusBar.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: StatusBar.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type StatusBarBinding internal(handle: StatusBar.Handle) =
    interface IViewBinding
    member this.ShowMessage(message: string, ?timeout: int) =
        handle.ShowMessage(message, timeout |> Option.defaultValue 0)
    member this.IsSizeGripEnabled =
        handle.IsSizeGripEnabled()
        
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? StatusBarBinding as statusBar) ->
        statusBar
    | _ ->
        failwith "StatusBar.bindNode fail"
    
type StatusBar<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IStatusBarNode<'msg> with
        override this.Dependencies = []

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> StatusBar<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.StatusBar =
            this.model.StatusBar
            
        override this.ContentKey =
            this.model.StatusBar
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, StatusBarBinding(this.model.StatusBar))
