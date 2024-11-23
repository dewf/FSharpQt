module FSharpQt.Widgets.Label

open System
open FSharpQt.BuilderNode
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.Attrs
open FSharpQt.MiscTypes

type private Signal =
    | LinkActivated of link: string
    | LinkHovered of link: string

type internal Attr =
    | Alignment of align: Alignment
    | Indent of indent: int
    | Margin of margin: int
    | OpenExternalLinks of state: bool
    | Pixmap of pixmap: Pixmap.Handle
    | ScaledContents of state: bool
    | Text of text: string
    | TextFormat of format: TextFormat
    | TextInteractionFlags of flags: TextInteractionFlag seq
    | WordWrap of state: bool
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
            | Alignment _ -> "label:alignment"
            | Indent _ -> "label:indent"
            | Margin _ -> "label:margin"
            | OpenExternalLinks _ -> "label:openexternallinks"
            | Pixmap _ -> "label:pixmap"
            | ScaledContents _ -> "label:scaledcontents"
            | Text _ -> "label:text"
            | TextFormat _ -> "label:textformat"
            | TextInteractionFlags _ -> "label:textinteractionflags"
            | WordWrap _ -> "label:wordwrap"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyLabelAttr(this)
            | _ ->
                printfn "warning: Label.Attr couldn't ApplyTo() unknown target type [%A]" target
                

and internal AttrTarget =
    interface
        inherit Frame.AttrTarget
        abstract member ApplyLabelAttr: Attr -> unit
    end

type private SignalMapFunc<'msg>(func) =
    inherit SignalMapFuncBase<Signal,'msg>(func)
                
type Props<'msg>() =
    inherit Frame.Props<'msg>()
    
    let mutable maybeOnLinkActivated: (string -> 'msg) option = None
    let mutable maybeOnLinkHovered: (string -> 'msg) option = None

    member internal this.SignalMask = enum<Label.SignalMask> (int this._signalMask)
    
    member this.OnLinkActivated with set value =
        maybeOnLinkActivated <- value
        this.AddSignal(int Label.SignalMask.LinkActivated)
        
    member this.OnLinkHovered with set value =
        maybeOnLinkHovered <- value
        this.AddSignal(int Label.SignalMask.LinkHovered)
        
    member internal this.SignalMapList =
        let thisFunc = function
            | LinkActivated link ->
                maybeOnLinkActivated
                |> Option.map (fun f -> f link)
            | LinkHovered link ->
                maybeOnLinkHovered
                |> Option.map (fun f -> f link)
        SignalMapFunc(thisFunc) :> ISignalMapFunc :: base.SignalMapList
        
    member this.Alignment with set value =
        this.PushAttr(Alignment value)
        
    member this.Indent with set value =
        this.PushAttr(Indent value)
        
    member this.Margin with set value =
        this.PushAttr(Margin value)
        
    member this.OpenExternalLinks with set value =
        this.PushAttr(OpenExternalLinks value)
        
    member this.Pixmap with set value =
        this.PushAttr(Pixmap value)

    member this.ScaledContents with set value =
        this.PushAttr(ScaledContents value)
        
    member this.Text with set value =
        this.PushAttr(Text value)
        
    member this.TextFormat with set value =
        this.PushAttr(TextFormat value)
        
    member this.TextInteractionFlags with set value =
        this.PushAttr(TextInteractionFlags value)
        
    member this.WordWrap with set value =
        this.PushAttr(WordWrap value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Frame.ModelCore<'msg>(dispatch)
    let mutable label: Label.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<Label.SignalMask> 0
    
    // no binding guards
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.Label
        with get() = label
        and set value =
            // assign to base
            this.Frame <- value
            label <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? SignalMapFunc<'msg> as smf ->
                signalMap <- smf.Func
            | _ ->
                failwith "Label.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "Label.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            label.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyLabelAttr attr =
            match attr with
            | Alignment align ->
                label.SetAlignment(align.QtValue)
            | Indent indent ->
                label.SetIndent(indent)
            | Margin margin ->
                label.SetMargin(margin)
            | OpenExternalLinks state ->
                label.SetOpenExternalLinks(state)
            | Pixmap handle ->
                label.SetPixmap(handle)
            | ScaledContents state ->
                label.SetScaledContents(state)
            | Text text ->
                label.SetText(text)
            | TextFormat format ->
                label.SetTextFormat(format.QtValue)
            | TextInteractionFlags flags ->
                label.SetTextInteractionFlags(flags |> TextInteractionFlag.QtSetFrom)
            | WordWrap state ->
                label.SetWordWrap(state)
                
    interface Label.SignalHandler with
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
        // Frame ==========================
        // (none)
        // Label ==========================
        member this.LinkActivated link =
            signalDispatch (LinkActivated link)
        member this.LinkHovered link =
            signalDispatch (LinkHovered link)
            
    interface IDisposable with
        member this.Dispose() =
            label.Dispose()

type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.Label <- Label.Create(this)
            
let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Label.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Label.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type Label<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps() =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> Label<'msg>)
            let nextAttrs = diffAttrs left'.Attrs this.Attrs |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.Label
            
        override this.ContentKey =
            this.model.Label
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
