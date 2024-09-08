module FSharpQt.Painting

open System
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp

type Color internal(qtColor: PaintResources.Color) =
    member val internal qtColor = qtColor

type Gradient internal(qtGradient: PaintResources.Gradient) =
    member val qtGradient = qtGradient
    member this.SetColorAt(location: double, color: Color) =
        qtGradient.SetColorAt(location, color.qtColor)
    
type RadialGradient internal(qtRadial: PaintResources.RadialGradient) =
    inherit Gradient(qtRadial)
    member val qtRadial = qtRadial
    
type LinearGradient internal(qtLinear: PaintResources.LinearGradient) =
    inherit Gradient(qtLinear)
    member val qtLinear = qtLinear
        
type BrushStyle =
    | NoBrush
with
    member internal this.QtValue =
        match this with
        | NoBrush -> PaintResources.Brush.Style.NoBrush
    
type Brush internal(qtBrush: PaintResources.Brush) =
    member val internal qtBrush = qtBrush

type PenStyle =
    | NoPen
    | SolidLine
    | DashLine
    | DotLine
    | DashDotLine
    | DashDotDotLine
    | CustomDashLine
with
    member internal this.QtValue =
        match this with
        | NoPen -> PaintResources.Pen.Style.NoPen
        | SolidLine -> PaintResources.Pen.Style.SolidLine
        | DashLine -> PaintResources.Pen.Style.DashLine
        | DotLine -> PaintResources.Pen.Style.DotLine
        | DashDotLine -> PaintResources.Pen.Style.DashDotLine
        | DashDotDotLine -> PaintResources.Pen.Style.DashDotDotLine
        | CustomDashLine -> PaintResources.Pen.Style.CustomDashLine
    
type CapStyle =
    | Flat
    | Square
    | Round
with
    member internal this.QtValue =
        match this with
        | Flat -> PaintResources.Pen.CapStyle.Flat
        | Square -> PaintResources.Pen.CapStyle.Square
        | Round -> PaintResources.Pen.CapStyle.Round
    
type JoinStyle =
    | Miter
    | Bevel
    | Round
    | SvgMiter
with
    member internal this.QtValue =
        match this with
        | Miter -> PaintResources.Pen.JoinStyle.Miter
        | Bevel -> PaintResources.Pen.JoinStyle.Bevel
        | Round -> PaintResources.Pen.JoinStyle.Round
        | SvgMiter -> PaintResources.Pen.JoinStyle.SvgMiter
        

type Pen internal(qtPen: PaintResources.Pen) =
    member val internal qtPen = qtPen
    member this.Width with set (value: int) = this.qtPen.SetWidth(value)
    member this.Width with set (value: double) = this.qtPen.SetWidth(value)
       
type Weight =
    | Thin
    | ExtraLight
    | Light
    | Normal
    | Medium
    | DemiBold
    | Bold
    | ExtraBold
    | BlackWeight
with
    member internal this.QtValue =
        match this with
        | Thin -> PaintResources.Font.Weight.Thin
        | ExtraLight -> PaintResources.Font.Weight.ExtraLight
        | Light -> PaintResources.Font.Weight.Light
        | Normal -> PaintResources.Font.Weight.Normal
        | Medium -> PaintResources.Font.Weight.Medium
        | DemiBold -> PaintResources.Font.Weight.DemiBold
        | Bold -> PaintResources.Font.Weight.Bold
        | ExtraBold -> PaintResources.Font.Weight.ExtraBold
        | BlackWeight -> PaintResources.Font.Weight.Black
    
type Font internal(qtFont: PaintResources.Font) =
    member val internal qtFont = qtFont
        
type PainterPath internal(qtPainterPath: PaintResources.PainterPath) =
    member val qtPainterPath = qtPainterPath
    member this.MoveTo(p: PointF) =
        qtPainterPath.MoveTo(p.QtValue)
    member this.LineTo(p: PointF) =
        qtPainterPath.Lineto(p.QtValue)
    member this.CubicTo(c1: PointF, c2: PointF, endPoint: PointF) =
        qtPainterPath.CubicTo(c1.QtValue, c2.QtValue, endPoint.QtValue)
        
type PainterPathStroker internal(qtStroker: PaintResources.PainterPathStroker) =
    member val qtStroker = qtStroker
    member this.Width with set value = qtStroker.SetWidth(value)
    member this.JoinStyle with set (value: JoinStyle) = qtStroker.SetJoinStyle(value.QtValue)
    member this.CapStyle with set (value: CapStyle) = qtStroker.SetCapStyle(value.QtValue)
    member this.DashPattern with set (value: double array) = qtStroker.SetDashPattern(value)
    member this.CreateStroke(path: PainterPath) =
        PainterPath(qtStroker.CreateStroke(path.qtPainterPath))
        
type PaintStack() =
    member val qtResources = PaintResources.Create()
   
    interface IDisposable with
        member this.Dispose() =
            this.qtResources.Dispose()
            
    member this.Color(constant: ColorConstant) =
        this.qtResources.CreateColor(constant.QtValue)
        |> Color
        
    member this.Color(r: int, g: int, b: int) =
        this.qtResources.CreateColor(r, g, b)
        |> Color
        
    member this.Color(r: int, g: int, b: int, a: int) =
        this.qtResources.CreateColor(r, g, b, a)
        |> Color
        
    member this.Color(r: float, g: float, b: float) =
        this.qtResources.CreateColor(float32 r, float32 g, float32 b)
        |> Color
        
    member this.Color(r: float, g: float, b: float, a: float) =
        this.qtResources.CreateColor(float32 r, float32 g, float32 b, float32 a)
        |> Color
        
    member this.RadialGradient(center: PointF, radius: double) =
        this.qtResources.CreateRadialGradient(center.QtValue, radius)
        |> RadialGradient
        
    member this.LinearGradient(p1: PointF, p2: PointF) =
        this.qtResources.CreateLinearGradient(p1.QtValue, p2.QtValue)
        |> LinearGradient
        
    member this.LinearGradient(x1: double, y1: double, x2: double, y2: double) =
        this.qtResources.CreateLinearGradient(x1, y1, x2, y2)
        |> LinearGradient
        
    member this.Brush(style: BrushStyle) =
        this.qtResources.CreateBrush(style.QtValue)
        |> Brush
        
    member this.Brush(color: Color) =
        this.qtResources.CreateBrush(color.qtColor)
        |> Brush
        
    member this.Brush(grad: Gradient) =
        this.qtResources.CreateBrush(grad.qtGradient)
        |> Brush
        
    member this.Pen() =
        this.qtResources.CreatePen()
        |> Pen
        
    member this.Pen(style: PenStyle) =
        this.qtResources.CreatePen(style.QtValue)
        |> Pen
        
    member this.Pen(color: Color) =
        this.qtResources.CreatePen(color.qtColor)
        |> Pen
        
    member this.Pen(brush: Brush, width: double, ?style: PenStyle, ?cap: CapStyle, ?join: JoinStyle) =
        let useStyle =
            defaultArg style SolidLine
        let useCap =
            defaultArg cap Square
        let useJoin =
            defaultArg join Bevel
        this.qtResources.CreatePen(brush.qtBrush, width, useStyle.QtValue, useCap.QtValue, useJoin.QtValue)
        |> Pen
        
    member this.Pen(color: Color, width: double, ?style: PenStyle, ?cap: CapStyle, ?join: JoinStyle) =
        let useStyle =
            defaultArg style SolidLine
        let useCap =
            defaultArg cap Square
        let useJoin =
            defaultArg join Bevel
        let tempBrush =
            // brush will be tracked + later freed like anything else
            this.Brush(color)
        this.qtResources.CreatePen(tempBrush.qtBrush, width, useStyle.QtValue, useCap.QtValue, useJoin.QtValue)
        |> Pen
    
    member this.Font(family: string, pointSize: int) =
        this.qtResources.CreateFont(family, pointSize)
        |> Font
        
    member this.Font(family: string, pointSize: int, weight: Weight) =
        this.qtResources.CreateFont(family, pointSize, weight.QtValue)
        |> Font
        
    member this.Font(family: string, pointSize: int, weight: Weight, italic: bool) =
        this.qtResources.CreateFont(family, pointSize, weight.QtValue, italic)
        |> Font
        
    member this.PainterPath() =
        this.qtResources.CreatePainterPath()
        |> PainterPath
        
    member this.PainterPathStroker() =
        this.qtResources.CreatePainterPathStroker()
        |> PainterPathStroker
        
type RenderHint =
    | Antialiasing
    | TextAntialiasing
    | SmoothPixmapTransform
    | VerticalSubpixelPositioning
    | LosslessImageRendering
    | NonCosmeticBrushPatterns
with
    member this.QtValue =
        match this with
        | Antialiasing -> Painter.RenderHint.Antialiasing
        | TextAntialiasing -> Painter.RenderHint.TextAntialiasing
        | SmoothPixmapTransform -> Painter.RenderHint.SmoothPixmapTransform
        | VerticalSubpixelPositioning -> Painter.RenderHint.VerticalSubpixelPositioning
        | LosslessImageRendering -> Painter.RenderHint.LosslessImageRendering
        | NonCosmeticBrushPatterns -> Painter.RenderHint.NonCosmeticBrushPatterns
    static member QtSetFrom (hints: RenderHint seq) =
        (enum<Painter.RenderHintSet> 0, hints)
        ||> Seq.fold (fun acc hint ->
            let flag =
                match hint with
                | Antialiasing -> Painter.RenderHintSet.Antialiasing
                | TextAntialiasing -> Painter.RenderHintSet.TextAntialiasing
                | SmoothPixmapTransform -> Painter.RenderHintSet.SmoothPixmapTransform
                | VerticalSubpixelPositioning -> Painter.RenderHintSet.VerticalSubpixelPositioning
                | LosslessImageRendering -> Painter.RenderHintSet.LosslessImageRendering
                | NonCosmeticBrushPatterns -> Painter.RenderHintSet.NonCosmeticBrushPatterns
            acc ||| flag)

type Painter internal(qtPainter: Org.Whatever.MinimalQtForFSharp.Painter.Handle) =
    // not disposable (for now) because we don't create them (for now)
    member val qtPainter = qtPainter
    
    member this.Pen with set (value: Pen) = qtPainter.SetPen(value.qtPen)
    member this.Brush with set (value: Brush) = qtPainter.SetBrush(value.qtBrush)
    member this.Font with set (value: Font) = qtPainter.SetFont(value.qtFont)
    
    member this.SetRenderHint (hint: RenderHint) (state: bool) =
        qtPainter.SetRenderHint(hint.QtValue, state)
        
    member this.SetRenderHints (hints: RenderHint seq) (state: bool) =
        qtPainter.SetRenderHints(hints |> RenderHint.QtSetFrom, state)
    
    member this.DrawText(rect: Rect, align: Alignment, text: string) =
        qtPainter.DrawText(rect.QtValue, align.QtValue, text)
        
    member this.FillRect(rect: Rect, brush: Brush) =
        qtPainter.FillRect(rect.QtValue, brush.qtBrush)
        
    member this.FillRect(rect: Rect, color: Color) =
        qtPainter.FillRect(rect.QtValue, color.qtColor)

    member this.DrawRect(rect: Rect) =
        qtPainter.DrawRect(rect.QtValue)
        
    member this.DrawRect(rect: RectF) =
        qtPainter.DrawRect(rect.QtValue)
        
    member this.DrawRect(x: int, y: int, width: int, height: int) =
        qtPainter.DrawRect(x, y, width, height)
        
    member this.DrawEllipse(rect: RectF) =
        qtPainter.DrawEllipse(rect.QtValue)

    member this.DrawEllipse(rect: Rect) =
        qtPainter.DrawEllipse(rect.QtValue)
        
    member this.DrawEllipse(x: int, y: int, width: int, height: int) =
        qtPainter.DrawEllipse(x, y, width, height)
        
    member this.DrawEllipse(center: PointF, rx: double, ry: double) =
        qtPainter.DrawEllipse(center.QtValue, rx, ry)
        
    member this.DrawEllipse(center: Point, rx: int, ry: int) =
        qtPainter.DrawEllipse(center.QtValue, rx, ry)
        
    member this.FillPath(path: PainterPath, brush: Brush) =
        qtPainter.FillPath(path.qtPainterPath, brush.qtBrush)
        
    member this.StrokePath(path: PainterPath, pen: Pen) =
        qtPainter.StrokePath(path.qtPainterPath, pen.qtPen)
        
    member this.DrawPolyline(points: PointF array) =
        qtPainter.DrawPolyline(points |> Array.map (_.QtValue))
