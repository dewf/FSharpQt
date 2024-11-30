module Renderer

open FSharpQt
open BuilderNode
open FSharpQt.Attrs
open Reactor

open FSharpQt.Widgets
open CustomWidget
open Timer

open MiscTypes
open Extensions
open EventDelegate
open Painting

let CONTROL_POINT_RADIUS = 10.0
let MAX_GRAB_DIST = 30.0
let TIMER_INTERVAL = 25 // millis

type LineStyle =
    | Curves
    | Lines

type Signal = unit

type PathPoint = {
    Position: PointF
    Velocity: PointF
}

type DragState =
    | NotDragging
    | Dragging of grabOffset: PointF

type State = {
    ViewRect: RectF
    ControlPoints: PathPoint array
    
    MousePos: PointF option         // changes via event handler
    MouseHoverIndex: int option     // checked after movement computation
    DragState: DragState
    
    CapStyle: CapStyle
    JoinStyle: JoinStyle
    PenStyle: PenStyle
    PenWidth: int
    LineStyle: LineStyle
    Animating: bool
}

type private Attr =
    | CapStyle of style: CapStyle
    | JoinStyle of style: JoinStyle
    | PenStyle of style: PenStyle
    | PenWidth of width: int
    | LineStyle of style: LineStyle
    | Animating of value: bool
    
let private keyFunc = function
    | CapStyle _ -> "pathstroking:renderer:capstyle"
    | JoinStyle _ -> "pathstroking:renderer:joinstyle"
    | PenStyle _ -> "pathstroking:renderer:penstyle"
    | PenWidth _ -> "pathstroking:renderer:penwidth"
    | LineStyle _ -> "pathstroking:renderer:linestyle"
    | Animating _ -> "pathstroking:renderer:animating"
    
let private attrUpdate state = function
    | CapStyle style -> { state with CapStyle = style }
    | JoinStyle style -> { state with JoinStyle = style }
    | PenStyle style -> { state with PenStyle = style }
    | PenWidth width -> { state with PenWidth = width }
    | LineStyle style -> { state with LineStyle = style }
    | Animating value -> { state with Animating = value }
    
type Msg =
    | Resized of rect: RectF
    | TimerTick of elapsed: double
    | MouseMove of pos: Point
    | MouseLeave
    | BeginDrag of grabOffs: PointF
    | EndDrag

let init() =
    let positions = [|
        (250.0, 453.0)
        (171.81, 415.34)
        (152.50, 330.74)
        (206.61, 262.90)
        (293.38, 262.90)
        (347.49, 330.74)
        (328.18, 415.34) |] |> Array.map PointF.From
    let velocities = [|
        (1.800, 0.449)
        (1.005, 1.101)
        (-0.546, 0.923)
        (-1.686, 0.050)
        (-1.556, -0.861)
        (-0.254, -1.123)
        (1.239, -0.540) |] |> Array.map PointF.From
    let controlPoints =
        Array.zip positions velocities
        |> Array.map (fun (pos, vel) -> { Position = pos; Velocity = vel })
    let state = {
        ViewRect = RectF.From(0, 0, 0, 0)
        ControlPoints = controlPoints
        MousePos = None
        MouseHoverIndex = None
        DragState = NotDragging
        CapStyle = Flat
        JoinStyle = Bevel
        PenStyle = SolidLine
        PenWidth = 5
        LineStyle = Curves
        Animating = true
    }
    state, Cmd.None

let stepSinglePoint elapsedMillis left right top bottom { Position = pos; Velocity = vel } =
    let xDeltaAdjusted =
        (vel.X * elapsedMillis) / (double TIMER_INTERVAL)
    let yDeltaAdjusted =
        (vel.Y * elapsedMillis) / (double TIMER_INTERVAL)
    let projected =
        { X = pos.X + xDeltaAdjusted
          Y = pos.Y + yDeltaAdjusted }
    let nextPoint, nextVector =
        if projected.X < left then
            { projected with X = left }, { vel with X = -vel.X }
        elif projected.X > right then
            { projected with X = right }, { vel with X = -vel.X }
        elif projected.Y < top then
            { projected with Y = top }, { vel with Y = -vel.Y }
        elif projected.Y > bottom then
            { projected with Y = bottom }, { vel with Y = -vel.Y }
        else
            projected, vel
    { Position = nextPoint; Velocity = nextVector }
    
let stepPoints (elapsed: double) (points: PathPoint array) (bounds: RectF) (skipIndex: int option) =
    let pad = float CONTROL_POINT_RADIUS
    let left = pad
    let right = bounds.Width - pad
    let top = pad
    let bottom = bounds.Height - pad

    points
    |> Array.mapi (fun i point ->
        match skipIndex with
        | Some index when i = index ->
            // manual dragging, don't advance
            point
        | _ ->
            stepSinglePoint elapsed left right top bottom point)
    
let dist (p1: Point) (p2: Point) =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    (dx * dx + dy * dy) |> float |> sqrt
    
let dist2 (p1: PointF) (p2: PointF) =
    let dx = p1.X - p2.X
    let dy = p1.Y - p2.Y
    (dx * dx + dy * dy) |> float |> sqrt
    
let nearestIndex (points: PathPoint array) (mousePos: PointF) =
    // of all the points within MAX_HOVER_DIST pixels (might be none),
    // we want the nearest one
    let withinRange =
        points
        |> Array.mapi (fun i pp -> i, dist2 pp.Position mousePos)
        |> Array.filter (fun (_, dist) -> dist <= MAX_GRAB_DIST)
        |> Array.sortBy snd
    if withinRange.Length > 0 then
        Some (fst withinRange[0])
    else
        None

let update (state: State) (msg: Msg) =
    match msg with
    | Resized rect ->
        { state with ViewRect = rect }, Cmd.None
    | TimerTick elapsed ->
        let nextPoints =
            if state.Animating then
                let skipIndex =
                    match state.DragState, state.MouseHoverIndex with
                    | Dragging _, Some index ->
                        Some index
                    | _ ->
                        None
                stepPoints elapsed state.ControlPoints state.ViewRect skipIndex
            else
                state.ControlPoints
        // if not in an active drag, check to see if anything slipped under the mouse
        let maybeIndex =
            match state.DragState with
            | Dragging _ ->
                // don't touch, already dragging
                state.MouseHoverIndex
            | NotDragging ->
                match state.MousePos with
                | Some pointF ->
                    nearestIndex nextPoints pointF
                | None ->
                    None
        let nextState =
            { state with ControlPoints = nextPoints; MouseHoverIndex = maybeIndex }
        nextState, Cmd.None
    | MouseMove pos ->
        let pointF =
            PointF.From(pos)
        let nextState =
            match state.DragState, state.MouseHoverIndex with
            | Dragging grabOffs, Some hoverIndex ->
                // moved indexed control point to current pos (taking account of the original grab offset)
                let nextPoints =
                    state.ControlPoints
                    |> Array.replaceAtIndex hoverIndex (fun p ->
                        let adjusted =
                            { X = pointF.X - grabOffs.X; Y = pointF.Y - grabOffs.Y }
                        { p with Position = adjusted })
                { state with ControlPoints = nextPoints }
            | _ ->
                // not dragging, just update the hover index if we're over something
                let maybeIndex =
                    nearestIndex state.ControlPoints pointF
                { state with
                    MousePos = Some pointF
                    MouseHoverIndex = maybeIndex }
        nextState, Cmd.None
    | MouseLeave ->
        { state with MouseHoverIndex = None; MousePos = None }, Cmd.None
    | BeginDrag grabOffs ->
        { state with DragState = Dragging grabOffs }, Cmd.None
    | EndDrag ->
        { state with DragState = NotDragging }, Cmd.None
        
type PaintResources = {
    BgColor: IColor
    LineColorBrush: Brush
    NoPen: Pen
    ControlPointPen: Pen
    ControlPointBrush: Brush
    HoverPointBrush: Brush
    LightGrayPen: Pen
    NoBrush: Brush
}
        
type EventDelegate(state: State) =
    inherit EventDelegateBaseWithResources<Msg,State,PaintResources>(state)
    
    override this.CreateResources res =
        // a smattering of different Color creation methods here, should all be interchangeable
        { BgColor = new Color.Owned(DarkGray)
          LineColorBrush = res.Brush(res.Color(Red))
          NoPen = res.Pen(NoPen)
          ControlPointPen = res.Pen(Color(50, 100, 120, 200))
          ControlPointBrush = res.Brush(Color(200, 200, 210, 120))
          HoverPointBrush = res.Brush(Color.Yellow)
          LightGrayPen = res.Pen(Color.LightGray, 0, SolidLine)
          NoBrush = res.Brush(NoBrush) }
    
    override this.SizeHint =
        Size.From(600, 700)
    
    override this.NeedsPaint _ =
        Everything

    override this.Resize _ newSize =
        RectF.From newSize
        |> (Resized >> Some)
        
    override this.MousePress pos button modifiers =
        state.MouseHoverIndex
        |> Option.map (fun index ->
            let grabOffs =
                let p =
                    state.ControlPoints[index].Position
                let pointF =
                    PointF.From(pos)
                { X = pointF.X - p.X; Y = pointF.Y - p.Y }
            BeginDrag grabOffs)
        
    override this.MouseRelease pos button modifiers =
        match state.DragState with
        | Dragging _ ->
            Some EndDrag
        | _ ->
            None
        
    override this.MouseMove pos buttons modifiers =
        Some (MouseMove pos)
        
    override this.Leave() =
        Some MouseLeave
    
    override this.Paint res stack painter widget updateRect =
        painter.SetRenderHint Antialiasing true
        painter.FillRect(widget.Rect, res.BgColor)
        
        // draw control points
        painter.Pen <- res.ControlPointPen
        for i, point in state.ControlPoints |> Array.zipWithIndex do
            let brush =
                match state.MouseHoverIndex with
                | Some index when i = index ->
                    res.HoverPointBrush
                | _ ->
                    res.ControlPointBrush
            painter.Brush <- brush
            painter.DrawEllipse(point.Position, CONTROL_POINT_RADIUS, CONTROL_POINT_RADIUS)
        painter.Pen <- res.LightGrayPen
        painter.Brush <- res.NoBrush
        let points =
            state.ControlPoints
            |> Array.map (_.Position)
        painter.DrawPolyline(points)

        // construct path
        let path = stack.PainterPath()
        path.MoveTo(state.ControlPoints[0].Position)
        match state.LineStyle with
        | Lines ->
            seq { 1 .. state.ControlPoints.Length - 1 }
            |> Seq.iter (fun i ->
                path.LineTo(state.ControlPoints[i].Position))
        | Curves ->
            let mutable i = 1
            while i + 2 < state.ControlPoints.Length do
                path.CubicTo(state.ControlPoints[i].Position, state.ControlPoints[i+1].Position, state.ControlPoints[i+2].Position)
                i <- i + 3
            while i < state.ControlPoints.Length do
                path.LineTo(state.ControlPoints[i].Position)
                i <- i + 1

        // draw path
        painter.Pen <- res.NoPen
        match state.PenStyle with
        | CustomDashLine ->
            let stroker =
                stack.PainterPathStroker(Width = state.PenWidth, JoinStyle = state.JoinStyle, CapStyle = state.CapStyle)
            let dashes =
                let space = 4
                [| 1.0; space; 3; space; 9; space; 27; space; 9; space; 3; space |]
            stroker.DashPattern <- dashes
            let stroke = stroker.CreateStroke(path)
            painter.FillPath(stroke, res.LineColorBrush)
        | _ ->
            let pen = stack.Pen(res.LineColorBrush, state.PenWidth, state.PenStyle, state.CapStyle, state.JoinStyle)
            painter.StrokePath(path, pen)
        
let view (state: State) =
    let timer =
        Timer(Interval = TIMER_INTERVAL, Running = true, OnTimeoutWithElapsed = TimerTick)
    let custom =
        let events = [
            PaintEvent
            SizeHint
            ResizeEvent
            MouseMoveEvent
            LeaveEvent
            MousePressEvent
            MouseReleaseEvent
        ]
        CustomWidget(EventDelegate(state), events,
                     MouseTracking = true,
                     Attachments = [ Attachment("timer", timer) ])
    custom :> IWidgetNode<Msg>

type PathStrokeRenderer<'outerMsg>() =
    inherit WidgetReactorNode<'outerMsg, State, Msg, Signal>(init, update, view)

    member this.CapStyle with set value =
        this.PushAttr(ComponentAttr(CapStyle value, keyFunc, attrUpdate))
        
    member this.JoinStyle with set value =
        this.PushAttr(ComponentAttr(JoinStyle value, keyFunc, attrUpdate))

    member this.PenStyle with set value =
        this.PushAttr(ComponentAttr(PenStyle value, keyFunc, attrUpdate))
        
    member this.PenWidth with set value =
        this.PushAttr(ComponentAttr(PenWidth value, keyFunc, attrUpdate))
        
    member this.LineStyle with set value =
        this.PushAttr(ComponentAttr(LineStyle value, keyFunc, attrUpdate))
        
    member this.Animating with set value =
        this.PushAttr(ComponentAttr(Animating value, keyFunc, attrUpdate))
