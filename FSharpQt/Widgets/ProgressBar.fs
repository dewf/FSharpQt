module FSharpQt.Widgets.ProgressBar

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes

type private Signal =
    | ValueChanged of value: int
    
type Direction =
    | TopToBottom
    | BottomToTop
with
    member internal this.QtValue =
        match this with
        | TopToBottom -> ProgressBar.Direction.TopToBottom
        | BottomToTop -> ProgressBar.Direction.BottomToTop
    
type internal Attr =
    | Alignment of align: Alignment
    | Format of format: string
    | InvertedAppearance of state: bool
    | Maximum of value: int
    | Minimum of value: int
    | Orientation of orient: Orientation
    | TextDirection of dir: Direction
    | TextVisible of state: bool
    | Value of value: int
    | Range of min: int * max: int
    // our own:
    | InnerText of text: string
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
            | Alignment _ -> "progressbar:alignment"
            | Format _ -> "progressbar:format"
            | InvertedAppearance _ -> "progressbar:invertedappearance"
            | Maximum _ -> "progressbar:maximum"
            | Minimum _ -> "progressbar:minimum"
            | Orientation _ -> "progressbar:orientation"
            | TextDirection _ -> "progressbar:textdirection"
            | TextVisible _ -> "progressbar:textvisible"
            | Value _ -> "progressbar:value"
            | Range _ -> "progressbar:range"
            | InnerText _ -> "progressbar:innertext"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyProgressBarAttr(this)
            | _ ->
                printfn "warning: ProgressBar.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyProgressBarAttr: Attr -> unit
    end
    
type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)

type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    let mutable onValueChanged: (int -> 'msg) option = None
    
    member internal this.SignalMask = enum<ProgressBar.SignalMask> (int this._signalMask)
    
    member this.OnValueChanged with set value =
        onValueChanged <- Some value
        this.AddSignal(int ProgressBar.SignalMask.ValueChanged)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | ValueChanged value ->
                onValueChanged
                |> Option.map (fun f -> f value)
        // prepend to parent signal map funcs
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList

    member this.Alignment with set value =
        this.PushAttr(Alignment value)

    member this.Format with set value =
        this.PushAttr(Format value)

    member this.InvertedAppearance with set value =
        this.PushAttr(InvertedAppearance value)

    member this.Maximum with set value =
        this.PushAttr(Maximum value)

    member this.Minimum with set value =
        this.PushAttr(Minimum value)

    member this.Orientation with set value =
        this.PushAttr(Orientation value)

    member this.TextDirection with set value =
        this.PushAttr(TextDirection value)

    member this.TextVisible with set value =
        this.PushAttr(TextVisible value)

    member this.Value with set value =
        this.PushAttr(Value value)

    member this.Range with set value =
        this.PushAttr(Range value)
        
    member this.InnerText with set value =
        this.PushAttr(InnerText value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable progressBar: ProgressBar.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<ProgressBar.SignalMask> 0
    
    // label for use with InnerText
    let mutable innerLabel: Label.Handle = null

    // binding guards:
    let mutable lastValue = Int32.MinValue
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.ProgressBar
        with get() = progressBar
        and set value =
            // assign to base
            this.Widget <- value
            progressBar <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "ProgressBar.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "ProgressBar.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            progressBar.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyProgressBarAttr attr =
            match attr with
            | Alignment align ->
                progressBar.SetAlignment(align.QtValue)
            | Format format ->
                progressBar.SetFormat(format)
            | InvertedAppearance state ->
                progressBar.SetInvertedAppearance(state)
            | Maximum value ->
                progressBar.SetMaximum(value)
            | Minimum value ->
                progressBar.SetMinimum(value)
            | Orientation orient ->
                progressBar.SetOrientation(orient.QtValue)
            | TextDirection dir ->
                progressBar.SetTextDirection(dir.QtValue)
            | TextVisible state ->
                progressBar.SetTextVisible(state)
            | Value value ->
                if value <> lastValue then
                    lastValue <- value
                    progressBar.SetValue(value)
            | Range(min, max) ->
                progressBar.SetRange(min, max)
            | InnerText text ->
                if innerLabel = null then
                    let layout =
                        BoxLayout.CreateNoHandler(Org.Whatever.MinimalQtForFSharp.BoxLayout.Direction.TopToBottom) // not sure Org.Whatever.MinimalQtForFSharp prefix is required here, IDE seems to think it's not, but won't compile without it
                    // layout.SetDirection(Org.Whatever.MinimalQtForFSharp.BoxLayout.Direction.TopToBottom)
                    layout.SetContentsMargins(0, 0, 0, 0)
                    innerLabel <- Label.CreateNoHandler()
                    innerLabel.SetAlignment(Enums.Alignment.AlignCenter)
                    layout.AddWidget(innerLabel)
                    progressBar.SetLayout(layout)
                    progressBar.SetTextVisible(false) // you probably want this ...
                innerLabel.SetText(text)
                
    interface ProgressBar.SignalHandler with
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
        // ProgressBar ====================
        member this.ValueChanged value =
            lastValue <- value
            signalDispatch (ValueChanged value)
            
    interface IDisposable with
        member this.Dispose() =
            progressBar.Dispose()
            
            
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.ProgressBar <- ProgressBar.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: ProgressBar.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: ProgressBar.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type ProgressBar<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContextr =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> ProgressBar<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <-
                migrate left'.model nextAttrs this.SignalMapList this.SignalMask
                
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.ProgressBar
            
        override this.ContentKey =
            this.model.ProgressBar
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
