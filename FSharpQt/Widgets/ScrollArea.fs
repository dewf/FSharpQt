module FSharpQt.Widgets.ScrollArea

open System
open FSharpQt.Attrs
open FSharpQt.BuilderNode
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type private Signal = unit

type internal Attr =
    | Alignment of align: Alignment
    | WidgetResizable of resizable: bool
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
            | Alignment _ -> "scrollarea:alignment"
            | WidgetResizable _ -> "scrollarea:widgetresizable"
        override this.ApplyTo (target: IAttrTarget, maybePrev: IAttr option) =
            match target with
            | :? AttrTarget as attrTarget ->
                attrTarget.ApplyScrollAreaAttr(this)
            | _ ->
                printfn "warning: ScrollArea.Attr couldn't ApplyTo() unknown target type [%A]" target
                
and internal AttrTarget =
    interface
        inherit AbstractScrollArea.AttrTarget
        abstract member ApplyScrollAreaAttr: Attr -> unit
    end

type Props<'msg>() =
    inherit AbstractScrollArea.Props<'msg>()
    
    member internal this.SignalMask = enum<ScrollArea.SignalMask> (int this._signalMask)
    
    member internal this.SignalMapList =
        NullSignalMapFunc() :> ISignalMapFunc :: base.SignalMapList
        
    member this.Alignment with set value =
        this.PushAttr(Alignment value)
        
    member this.WidgetResizable with set value =
        this.PushAttr(WidgetResizable value)
        
type ModelCore<'msg>(dispatch: 'msg -> unit) =
    inherit AbstractScrollArea.ModelCore<'msg>(dispatch)
    let mutable scrollArea: ScrollArea.Handle = null
    let mutable signalMap: Signal -> 'msg option = (fun _ -> None)
    let mutable currentMask = enum<ScrollArea.SignalMask> 0
    
    let signalDispatch (s: Signal) =
        signalMap s
        |> Option.iter dispatch
        
    member this.ScrollArea
        with get() = scrollArea
        and set value =
            this.AbstractScrollArea <- value
            scrollArea <- value
            
    member internal this.SignalMaps with set (mapFuncList: ISignalMapFunc list) =
        match mapFuncList with
        | h :: etc ->
            match h with
            | :? NullSignalMapFunc ->
                ()
            | _ ->
                failwith "ScrollArea.ModelCore.SignalMaps: wrong func type"
            // assign the remainder to parent class(es)
            base.SignalMaps <- etc
        | _ ->
            failwith "ScrollArea.ModelCore: signal map assignment didn't have a head element"
            
    member this.SignalMask with set value =
        if value <> currentMask then
            // we don't need to invoke the base version, the most derived widget handles the full signal stack from all super classes (at the C++/C# levels)
            scrollArea.SetSignalMask(value)
            currentMask <- value
            
    interface AttrTarget with
        member this.ApplyScrollAreaAttr attr =
            match attr with
            | Alignment align ->
                scrollArea.SetAlignment(align.QtValue)
            | WidgetResizable resizable ->
                scrollArea.SetWidgetResizable(resizable)
                
    interface ScrollArea.SignalHandler with
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
        // AbstractScrollArea =============
        // (none)
        // ScrollArea =====================
        // (none)
        
    interface IDisposable with
        member this.Dispose() =
            scrollArea.Dispose()
    
type private Model<'msg>(dispatch: 'msg -> unit) as this =
    inherit ModelCore<'msg>(dispatch)
    let scrollArea = ScrollArea.Create(this)
    let mutable syntheticLayoutWidget: Widget.Handle option = None
    do
        this.ScrollArea <- scrollArea
        
    member this.RemoveContent() =
        // TODO: need to do some serious testing with all this
        // MainWindow too
        match syntheticLayoutWidget with
        | Some widget ->
            widget.GetLayout().RemoveAll() // detach any children just in case
            widget.SetLayout(null)
            widget.Dispose()
            // deleting should automatically remove from the parent mainWindow, right?
            syntheticLayoutWidget <- None
        | None ->
            scrollArea.SetWidget(null) // sufficient?
        
    member this.AddContent(node: IWidgetOrLayoutNode<'msg>) =
        match node with
        | :? IWidgetNode<'msg> as widgetNode ->
            scrollArea.SetWidget(widgetNode.Widget)
        | :? ILayoutNode<'msg> as layout ->
            let widget = Widget.CreateNoHandler()
            widget.SetLayout(layout.Layout)
            scrollArea.SetWidget(widget)
            syntheticLayoutWidget <- Some widget
        | _ ->
            failwith "ScrollArea.Model.AddContent - unknown node type"

let private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (signalMask: ScrollArea.SignalMask) =
    let model = new Model<'msg>(dispatch)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (signalMask: ScrollArea.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()

type ScrollArea<'msg>() =
    inherit Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    let mutable maybeContent: IWidgetOrLayoutNode<'msg> option = None
    member this.Content with set value = maybeContent <- Some value

    member private this.MigrateContent (changeMap: Map<DepsKey, DepsChange>) =
        match changeMap.TryFind (StrKey "content") with
        | Some change ->
            match change with
            | Unchanged ->
                ()
            | Added ->
                this.model.AddContent(maybeContent.Value)
            | Removed ->
                this.model.RemoveContent()
            | Swapped ->
                this.model.RemoveContent()
                this.model.AddContent(maybeContent.Value)
        | None ->
            // neither side had 'content'
            ()
        
    interface IWidgetNode<'msg> with
        override this.Dependencies =
            let contentList =
                maybeContent
                |> Option.map (fun content -> (StrKey "content", content :> IBuilderNode<'msg>))
                |> Option.toList
            contentList

        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.SignalMask
            
        override this.AttachDeps () =
            maybeContent
            |> Option.iter this.model.AddContent

        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> ScrollArea<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList this.SignalMask
            this.MigrateContent (depsChanges |> Map.ofList)

        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.ScrollArea
            
        override this.ContentKey =
            this.model.ScrollArea
            
        override this.Attachments =
            this.Attachments

        override this.Binding = None
