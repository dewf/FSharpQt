module FSharpQt.Painting

open System
open CSharpFunctionalExtensions
open Microsoft.FSharp.Core
open Org.Whatever.MinimalQtForFSharp
open FSharpQt.MiscTypes
open Org.Whatever.MinimalQtForFSharp.Support

// type Color internal(qtColor: PaintResources.Color) =
//     member val internal qtColor = qtColor

type Gradient internal(qtGradient: PaintResources.Gradient) =
    member val qtGradient = qtGradient
    member this.SetColorAt(location: double, color: IColor) =
        qtGradient.SetColorAt(location, color.QtValue)
    
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
        
type ImageFormat =
    | Invalid
    | Mono
    | MonoLSB
    | Indexed8
    | RGB32
    | ARGB32
    | ARGB32_Premultiplied
    | RGB16
    | ARGB8565_Premultiplied
    | RGB666
    | ARGB6666_Premultiplied
    | RGB555
    | ARGB8555_Premultiplied
    | RGB888
    | RGB444
    | ARGB4444_Premultiplied
    | RGBX8888
    | RGBA8888
    | RGBA8888_Premultiplied
    | BGR30
    | A2BGR30_Premultiplied
    | RGB30
    | A2RGB30_Premultiplied
    | Alpha8
    | Grayscale8
    | RGBX64
    | RGBA64
    | RGBA64_Premultiplied
    | Grayscale16
    | BGR888
    | RGBX16FPx4
    | RGBA16FPx4
    | RGBA16FPx4_Premultiplied
    | RGBX32FPx4
    | RGBA32FPx4
    | RGBA32FPx4_Premultiplied
    | CMYK8888
with
    member this.QtValue =
        match this with
        | Invalid -> Image.Format.Invalid
        | Mono -> Image.Format.Mono
        | MonoLSB -> Image.Format.MonoLSB
        | Indexed8 -> Image.Format.Indexed8
        | RGB32 -> Image.Format.RGB32
        | ARGB32 -> Image.Format.ARGB32
        | ARGB32_Premultiplied -> Image.Format.ARGB32_Premultiplied
        | RGB16 -> Image.Format.RGB16
        | ARGB8565_Premultiplied -> Image.Format.ARGB8565_Premultiplied
        | RGB666 -> Image.Format.RGB666
        | ARGB6666_Premultiplied -> Image.Format.ARGB6666_Premultiplied
        | RGB555 -> Image.Format.RGB555
        | ARGB8555_Premultiplied -> Image.Format.ARGB8555_Premultiplied
        | RGB888 -> Image.Format.RGB888
        | RGB444 -> Image.Format.RGB444
        | ARGB4444_Premultiplied -> Image.Format.ARGB4444_Premultiplied
        | RGBX8888 -> Image.Format.RGBX8888
        | RGBA8888 -> Image.Format.RGBA8888
        | RGBA8888_Premultiplied -> Image.Format.RGBA8888_Premultiplied
        | BGR30 -> Image.Format.BGR30
        | A2BGR30_Premultiplied -> Image.Format.A2BGR30_Premultiplied
        | RGB30 -> Image.Format.RGB30
        | A2RGB30_Premultiplied -> Image.Format.A2RGB30_Premultiplied
        | Alpha8 -> Image.Format.Alpha8
        | Grayscale8 -> Image.Format.Grayscale8
        | RGBX64 -> Image.Format.RGBX64
        | RGBA64 -> Image.Format.RGBA64
        | RGBA64_Premultiplied -> Image.Format.RGBA64_Premultiplied
        | Grayscale16 -> Image.Format.Grayscale16
        | BGR888 -> Image.Format.BGR888
        | RGBX16FPx4 -> Image.Format.RGBX16FPx4
        | RGBA16FPx4 -> Image.Format.RGBA16FPx4
        | RGBA16FPx4_Premultiplied -> Image.Format.RGBA16FPx4_Premultiplied
        | RGBX32FPx4 -> Image.Format.RGBX32FPx4
        | RGBA32FPx4 -> Image.Format.RGBA32FPx4
        | RGBA32FPx4_Premultiplied -> Image.Format.RGBA32FPx4_Premultiplied
        | CMYK8888 -> Image.Format.CMYK8888
        
type PaintDevice internal(handle: Org.Whatever.MinimalQtForFSharp.PaintDevice.Handle) =
    // as of right now, these can't be owned/disposed directly
    member val internal Handle = handle
    member this.Width =
        this.Handle.Width()
    member this.Height =
        this.Handle.Height()
        
type Image private(handle: Org.Whatever.MinimalQtForFSharp.Image.Handle, owned: bool) =
    inherit PaintDevice(handle)
    let mutable disposed = false
    member val internal Handle = handle
    
    interface IDisposable with
        member this.Dispose() =
            if owned && not disposed then
                (handle :?> Org.Whatever.MinimalQtForFSharp.Image.Owned).Dispose()
                disposed <- true
    override this.Finalize() =
        (this :> IDisposable).Dispose()
        
    internal new(handle: Org.Whatever.MinimalQtForFSharp.Image.Handle) =
        new Image(handle, false)
        
    internal new(owned: Org.Whatever.MinimalQtForFSharp.Image.Owned) =
        new Image(owned, true)
    
    new(width: int, height: int, format: ImageFormat) =
        let handle =
            Image.Create(width, height, format.QtValue)
        new Image(handle, true)
        
    new(filename: string, maybeFormat: string option) =
        let maybeString =
            match maybeFormat with
            | Some value -> Maybe.From(value)
            | None -> Maybe<string>.None
        let handle =
            Image.Create(filename, maybeString)
        new Image(handle, true)
        
    new(data: byte array, width: int, height: int, format: ImageFormat, bytesPerLine: int64 option) =
        let buffer = 
            let retBuffer = new ClientBuffer<byte>(data.Length)
            let mutable length = -1
            let retSpan = retBuffer.GetSpan(&length)
            Span(data).CopyTo(retSpan)
            retBuffer
        let maybeBytesPerLine =
            match bytesPerLine with
            | Some value -> Maybe<IntPtr>.From(IntPtr(value))
            | None -> Maybe<IntPtr>.None
        let handle =
            Image.Create(buffer, width, height, format.QtValue, maybeBytesPerLine)
        new Image(handle, true)
        
    member this.Scaled(width: int, height: int, ?aspectMode: AspectRatioMode, ?transformMode: TransformationMode) =
        let opts =
            let mutable ret = Image.ScaledOptions()
            if aspectMode.IsSome then
                ret.AspectMode <- aspectMode.Value.QtValue
            if transformMode.IsSome then
                ret.TransformMode <- transformMode.Value.QtValue
            ret
        let handle =
            this.Handle.Scaled(width, height, opts)
        new Image(handle, true)

type Pixmap private(handle: Org.Whatever.MinimalQtForFSharp.Pixmap.Handle, owned: bool) =
    inherit PaintDevice(handle)
    let mutable disposed = false
    member val internal Handle = handle
    
    interface IDisposable with
        member this.Dispose() =
            if owned && not disposed then
                (handle :?> Org.Whatever.MinimalQtForFSharp.Pixmap.Owned).Dispose()
                disposed <- true
    override this.Finalize() =
        (this :> IDisposable).Dispose()
        
    internal new(handle: Org.Whatever.MinimalQtForFSharp.Pixmap.Handle) =
        new Pixmap(handle, false)
        
    internal new(owned: Org.Whatever.MinimalQtForFSharp.Pixmap.Owned) =
        new Pixmap(owned, true)
        
    new(width: int, height: int) =
        let handle =
            Pixmap.Create(width, height)
        new Pixmap(handle, true)
        
    new(filename: string, ?format: string, ?flags: ImageConversionFlags seq) =
        let opts =
            let mutable ret = Org.Whatever.MinimalQtForFSharp.Pixmap.FilenameOptions()
            if format.IsSome then
                ret.Format <- format.Value
            if flags.IsSome then
                ret.ImageConversionFlags <- ImageConversionFlags.QtSetFrom flags.Value
            ret
        let handle =
            Pixmap.Create(filename, opts)
        new Pixmap(handle, true)
        
    static member FromImage(image: Image, ?conversionFlags: ImageConversionFlags seq) =
        // an optional set is kind of silly, since the flags 0-value on the Qt side is the same as the default value for the fromImage API,
        // but I don't want to assume that's always the case
        // I can stop such silliness when NI supports default values :(
        let maybeFlags =
            match conversionFlags with
            | Some value ->
                ImageConversionFlags.QtSetFrom value
                |> Maybe<Enums.ImageConversionFlags>.From
            | None ->
                Maybe<Enums.ImageConversionFlags>.None
        let handle =
            Org.Whatever.MinimalQtForFSharp.Pixmap.FromImage(image.Handle, maybeFlags)
        new Pixmap(handle, true)
        
type PaintStack() =
    member val qtResources = PaintResources.Create()
   
    interface IDisposable with
        member this.Dispose() =
            this.qtResources.Dispose()
            
    member this.Color(constant: ColorConstant) =
        let handle =
            this.qtResources.CreateColor(constant.QtValue)
        Color.Unowned(handle)
        
    member this.Color(r: int, g: int, b: int) =
        let handle =
            this.qtResources.CreateColor(r, g, b)
        Color.Unowned(handle)
        
    member this.Color(r: int, g: int, b: int, a: int) =
        let handle =
            this.qtResources.CreateColor(r, g, b, a)
        Color.Unowned(handle)
        
    member this.Color(r: float, g: float, b: float) =
        let handle =
            this.qtResources.CreateColor(float32 r, float32 g, float32 b)
        Color.Unowned(handle)
        
    member this.Color(r: float, g: float, b: float, a: float) =
        let handle =
            this.qtResources.CreateColor(float32 r, float32 g, float32 b, float32 a)
        Color.Unowned(handle)
        
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
        
    member this.Brush(color: IColor) =
        this.qtResources.CreateBrush(color.QtValue)
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
        
    member this.Pen(color: IColor) =
        this.qtResources.CreatePen(color.QtValue)
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
        
    member this.Pen(color: IColor, width: double, ?style: PenStyle, ?cap: CapStyle, ?join: JoinStyle) =
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
        
    member this.FillRect(rect: Rect, color: IColor) =
        qtPainter.FillRect(rect.QtValue, color.QtValue)

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
        
    member this.DrawPixmap(target: RectF, pixmap: Pixmap, source: RectF) =
        qtPainter.DrawPixmap(target.QtValue, pixmap.Handle, source.QtValue)

    member this.DrawPixmap(point: Point, pixmap: Pixmap) =
        qtPainter.DrawPixmap(point.QtValue, pixmap.Handle)

    member this.DrawPixmap(point: PointF, pixmap: Pixmap) =
        qtPainter.DrawPixmap(point.QtValue, pixmap.Handle)

    member this.DrawPixmap(rect: Rect, pixmap: Pixmap) =
        qtPainter.DrawPixmap(rect.QtValue, pixmap.Handle)

    member this.DrawPixmap(point: Point, pixmap: Pixmap, source: Rect) =
        qtPainter.DrawPixmap(point.QtValue, pixmap.Handle, source.QtValue)

    member this.DrawPixmap(point: PointF, pixmap: Pixmap, source: RectF) =
        qtPainter.DrawPixmap(point.QtValue, pixmap.Handle, source.QtValue)

    member this.DrawPixmap(target: Rect, pixmap: Pixmap, source: Rect) =
        qtPainter.DrawPixmap(target.QtValue, pixmap.Handle, source.QtValue)

    member this.DrawPixmap(x: int, y: int, pixmap: Pixmap) =
        qtPainter.DrawPixmap(x, y, pixmap.Handle)

    member this.DrawPixmap(x: int, y: int, width: int, height: int, pixmap: Pixmap) =
        qtPainter.DrawPixmap(x, y, width, height, pixmap.Handle)

    member this.DrawPixmap(x: int, y: int, pixmap: Pixmap, sx: int, sy: int, sw: int, sh: int) =
        qtPainter.DrawPixmap(x, y, pixmap.Handle, sx, sy, sw, sh)

    member this.DrawPixmap(x: int, y: int, w: int, h: int, pixmap: Pixmap, sx: int, sy: int, sw: int, sh: int) =
        qtPainter.DrawPixmap(x, y, w, h, pixmap.Handle, sx, sy, sw, sh)
