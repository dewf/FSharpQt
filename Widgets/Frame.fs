module FSharpQt.Widgets.Frame

open FSharpQt.BuilderNode
open System
open Org.Whatever.MinimalQtForFSharp

open FSharpQt.MiscTypes
open FSharpQt.Attrs

type private Signal = unit

type Shape =
    | NoFrame
    | Box
    | Panel
    | StyledPanel
    | HLine
    | VLine
    | WinPanel
with
    member this.QtValue =
        match this with
        | NoFrame -> Frame.Shape.NoFrame
        | Box -> Frame.Shape.Box
        | Panel -> Frame.Shape.Panel
        | StyledPanel -> Frame.Shape.StyledPanel
        | HLine -> Frame.Shape.HLine
        | VLine -> Frame.Shape.VLine
        | WinPanel -> Frame.Shape.WinPanel
    
type Shadow =
    | Plain
    | Raised
    | Sunken
with
    member this.QtValue =
        match this with
        | Plain -> Frame.Shadow.Plain
        | Raised -> Frame.Shadow.Raised
        | Sunken -> Frame.Shadow.Sunken
        
type internal Attr =
    | FrameRect of rect: Rect
    | FrameShadow of shadow: Shadow
    | FrameShape of shape: Shape
    | LineWidth of width: int
    | MidLineWidth of width: int
    | FrameStyle of shape: Shape * shadow: Shadow
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
            | FrameRect _ -> "frame:rect"
            | FrameShadow _ -> "frame:shadow"
            | FrameShape _ -> "frame:shape"
            | LineWidth _ -> "frame:linewidth"
            | MidLineWidth _ -> "frame:midlinewidth"
            | FrameStyle _ -> "frame:style"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyFrameAttr(this)
            | _ ->
                printfn "warning: Frame.Attr couldn't ApplyTo() unknown target type [%A]" target
    
and internal AttrTarget =
    interface
        inherit Widget.AttrTarget
        abstract member ApplyFrameAttr: Attr -> unit
    end
    
type Props<'msg>() =
    inherit Widget.Props<'msg>()
    
    member internal this.SignalMask = enum<Frame.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
    
    member this.FrameRect with set value =
        this.PushAttr(FrameRect value)
        
    member this.FrameShadow with set value =
        this.PushAttr(FrameShadow value)
        
    member this.FrameShape with set value =
        this.PushAttr(FrameShape value)
        
    member this.LineWidth with set value =
        this.PushAttr(LineWidth value)
        
    member this.MidLineWidth with set value =
        this.PushAttr(MidLineWidth value)
        
    member this.FrameStyle with set value =
        this.PushAttr(FrameStyle value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit Widget.ModelCore<'msg>(dispatch)
    let mutable frame: Frame.Handle = null
    let mutable currentMask = enum<Frame.SignalMask> 0
    
    member this.Frame
        with get() = frame
        and set value =
            // assign to base
            this.Widget <- value
            frame <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                // nothing to assign
                ()
            | _ ->
                failwith "Frame.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "Frame.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            frame.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyFrameAttr attr =
            match attr with
            | FrameRect rect ->
                frame.SetFrameRect(rect.QtValue)
            | FrameShadow shadow ->
                frame.SetFrameShadow(shadow.QtValue)
            | FrameShape shape ->
                frame.SetFrameShape(shape.QtValue)
            | LineWidth width ->
                frame.SetLineWidth(width)
            | MidLineWidth width ->
                frame.SetMidLineWidth(width)
            | FrameStyle(shape, shadow) ->
                frame.SetFrameStyle(shape.QtValue, shadow.QtValue)
                
    interface Frame.SignalHandler with
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
        
    interface IDisposable with
        member this.Dispose() =
            frame.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    do
        this.Frame <- Frame.Create(this)

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: Frame.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: Frame.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type Frame<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
        
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            ()

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> Frame<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()

        override this.Widget =
            this.model.Frame
            
        override this.ContentKey =
            this.model.Frame
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
