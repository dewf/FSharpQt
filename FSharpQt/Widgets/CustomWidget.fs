module FSharpQt.Widgets.CustomWidget

open System

open FSharpQt.BuilderNode
open FSharpQt.InputEnums
open FSharpQt.Painting
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.EventDelegate

open FSharpQt.MiscTypes
open FSharpQt.Attrs

// uses the same Signal/Attrs as Widget

// this whole module is a bit exotic, we're sort of quasi-inheriting from Widget,
// even though we're technically also a C++ QWidget behind the scenes
// however all the custom event delegate stuff is complicated and noisy,
// so it's nice to keep it out of the Widget module

type private Model<'msg>(dispatch: 'msg -> unit, methodMask: Widget.MethodMask, eventDelegate: EventDelegateInterface<'msg>) as this =
    inherit Widget.ModelCore<'msg>(dispatch)
    let widget = Widget.CreateSubclassed(this, methodMask, this) 
    do
        this.Widget <- widget
    
    // this is for anything the widget needs to create just once at the beginning
    // basically a roundabout way of maintaning auxiliary state just for the EventDelegate, which otherwise isn't supposed to have any (separate from the reactor State that's provided to it)
    let lifetimeResources = new PaintStack()
    let mutable eventDelegate = eventDelegate
    do
        eventDelegate.CreateResourcesInternal(lifetimeResources)
            
    member this.EventDelegate with set (newDelegate: EventDelegateInterface<'msg>) =
        // for now just the widget, maybe 'this' (the entire Model) in the future?
        newDelegate.Widget <- widget
        
        // check if it needs painting (by comparing to previous - for now the implementer will have to extract the previous state themselves, but we'll get to it
        match newDelegate.NeedsPaintInternal(eventDelegate) with
        | NotRequired ->
            ()
        | Everything ->
            widget.Update()
        | Rects rects ->
            for rect in rects do
                widget.Update(rect.QtValue)
                
        // migrate paint resources from previous
        newDelegate.MigrateResources eventDelegate
                
        // update/overwrite value
        eventDelegate <- newDelegate
    
    interface Widget.MethodDelegate with
        override this.ShowEvent (isSpontaneous: bool) =
            eventDelegate.ShowEvent isSpontaneous (WidgetProxy(widget))
            |> Option.iter dispatch
            
        override this.PaintEvent(painter: Painter.Handle, updateRect: Common.Rect) =
            use stackResources = new PaintStack() // "stack" (local), vs. the 'lifetimeResources' declared above
            eventDelegate.PaintInternal stackResources (Painter(painter)) (WidgetProxy(widget)) (Rect.From(updateRect))
            
        override this.MousePressEvent(pos: Common.Point, button: Enums.MouseButton, modifiers: Enums.Modifiers) =
            eventDelegate.MousePress (Point.From pos) (MouseButton.From button) (Modifier.SetFrom modifiers)
            |> Option.iter dispatch
            
        override this.MouseMoveEvent(pos: Common.Point, buttons: Enums.MouseButtonSet, modifiers: Enums.Modifiers) =
            eventDelegate.MouseMove (Point.From pos) (MouseButton.SetFrom buttons) (Modifier.SetFrom modifiers)
            |> Option.iter dispatch
                
        override this.MouseReleaseEvent(pos: Common.Point, button: Enums.MouseButton, modifiers: Enums.Modifiers) =
            eventDelegate.MouseRelease (Point.From pos) (MouseButton.From button) (Modifier.SetFrom modifiers)
            |> Option.iter dispatch
                
        override this.EnterEvent(pos: Common.Point) =
            eventDelegate.Enter (Point.From pos)
            |> Option.iter dispatch
                
        override this.LeaveEvent() =
            eventDelegate.Leave()
            |> Option.iter dispatch
                
        override this.ResizeEvent(oldSize: Common.Size, newSize: Common.Size) =
            eventDelegate.Resize (Size.From oldSize) (Size.From newSize)
            |> Option.iter dispatch
            
        override this.SizeHint() =
            eventDelegate.SizeHint.QtValue
                
        override this.DragMoveEvent(pos: Common.Point, modifiers: Enums.Modifiers, mimeData: Widget.MimeData, moveEvent: Widget.DragMoveEvent, isEnterEvent: bool) =
            match eventDelegate.DragMove (Point.From pos) (Modifier.SetFrom modifiers) (MimeDataProxy(mimeData)) (moveEvent.ProposedAction() |> DropAction.From) (moveEvent.PossibleActions() |> DropAction.SetFrom) isEnterEvent with
            | Some (dropAction, msg) ->
                moveEvent.AcceptDropAction(dropAction.QtValue)
                dispatch msg
            | None ->
                moveEvent.Ignore()
                
        override this.DragLeaveEvent() =
            eventDelegate.DragLeave()
            |> Option.iter dispatch
            
        override this.DropEvent(pos: Common.Point, modifiers: Enums.Modifiers, mimeData: Widget.MimeData, dropAction: Enums.DropAction) =
            eventDelegate.Drop (Point.From pos) (Modifier.SetFrom modifiers) (MimeDataProxy(mimeData)) (DropAction.From dropAction)
            |> Option.iter dispatch
            
    interface IDisposable with
        member this.Dispose() =
            (lifetimeResources :> IDisposable).Dispose()
            // would be nice to do base.Dispose() but apparently not possible?
            // anyway, this is all the Widget.ModelCore does:
            widget.Dispose()

let rec private create (attrs: IAttr list) (signalMaps: ISignalMapFunc list) (dispatch: 'msg -> unit) (methodMask: Widget.MethodMask) (eventDelegate: EventDelegateInterface<'msg>) (signalMask: Widget.SignalMask) =
    let model = new Model<'msg>(dispatch, methodMask, eventDelegate)
    model.ApplyAttrs (attrs |> List.map (fun attr -> None, attr))
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    // can't assign eventDelegate as simply as signal map, requires different behavior on construction vs. migration
    // hence providing it as Model ctor argument
    // model.EventDelegate <- eventDelegate
    model

let private migrate (model: Model<'msg>) (attrs: (IAttr option * IAttr) list) (signalMaps: ISignalMapFunc list) (eventDelegate: EventDelegateInterface<'msg>) (signalMask: Widget.SignalMask) =
    model.ApplyAttrs attrs
    model.SignalMaps <- signalMaps
    model.SignalMask <- signalMask
    model.EventDelegate <- eventDelegate
    model

let private dispose (model: Model<'msg>) =
    (model :> IDisposable).Dispose()
    
type EventMaskItem =
    | ShowEvent
    | MousePressEvent
    | MouseMoveEvent
    | MouseReleaseEvent
    | EnterEvent
    | LeaveEvent
    | PaintEvent
    | SizeHint
    | ResizeEvent
    | DropEvents
with
    static member QtSetFrom (eventMaskItems: EventMaskItem seq) =
        (enum<Widget.MethodMask> 0, eventMaskItems)
        ||> Seq.fold (fun acc item ->
            let value =
                match item with
                | ShowEvent -> Widget.MethodMask.ShowEvent
                | MousePressEvent -> Widget.MethodMask.MousePressEvent
                | MouseMoveEvent -> Widget.MethodMask.MouseMoveEvent
                | MouseReleaseEvent -> Widget.MethodMask.MouseReleaseEvent
                | EnterEvent -> Widget.MethodMask.EnterEvent
                | LeaveEvent -> Widget.MethodMask.LeaveEvent
                | PaintEvent -> Widget.MethodMask.PaintEvent
                | SizeHint -> Widget.MethodMask.SizeHint
                | ResizeEvent -> Widget.MethodMask.ResizeEvent
                | DropEvents -> Widget.MethodMask.DropEvents
            acc ||| value)
        
type CustomWidgetBinding internal(handle: Widget.Handle) =
    inherit Widget.WidgetBinding(handle)
    
let bindNode (name: string) (map: Map<string, IViewBinding>) =
    match map.TryFind name with
    | Some (:? CustomWidgetBinding as widget) ->
        widget
    | _ ->
        failwith "CustomWidget.bindNode fail"

type CustomWidget<'msg>(eventDelegate: EventDelegateInterface<'msg>, eventMaskItems: EventMaskItem seq) =
    inherit Widget.Props<'msg>()
    [<DefaultValue>] val mutable private model: Model<'msg>
    
    member private this.MethodMask =
        EventMaskItem.QtSetFrom eventMaskItems
            
    interface IWidgetNode<'msg> with
        override this.Dependencies = []
            
        override this.Create dispatch buildContext =
            this.model <- create this.Attrs this.SignalMapList dispatch this.MethodMask eventDelegate this.SignalMask
            
        override this.AttachDeps () =
            ()
            
        override this.MigrateFrom (left: IBuilderNode<'msg>) (depsChanges: (DepsKey * DepsChange) list) =
            let left' = (left :?> CustomWidget<'msg>)
            let nextAttrs =
                diffAttrs left'.Attrs this.Attrs
                |> createdOrChanged
            this.model <- migrate left'.model nextAttrs this.SignalMapList eventDelegate this.SignalMask
            
        override this.Dispose() =
            (this.model :> IDisposable).Dispose()
            
        override this.Widget =
            this.model.Widget
            
        override this.ContentKey =
            this.model.Widget
            
        override this.Attachments =
            this.Attachments

        override this.Binding =
            this.MaybeBoundName
            |> Option.map (fun name ->
                name, CustomWidgetBinding(this.model.Widget))
