using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Common;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class PaintResources
    {
        private static ModuleHandle _module;

        // built-in array type: double[]
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _gradient_setColorAt;
        internal static ModuleMethodHandle _pen_setBrush;
        internal static ModuleMethodHandle _pen_setWidth;
        internal static ModuleMethodHandle _pen_setWidth_overload1;
        internal static ModuleMethodHandle _painterPath_moveTo;
        internal static ModuleMethodHandle _painterPath_moveTo_overload1;
        internal static ModuleMethodHandle _painterPath_lineto;
        internal static ModuleMethodHandle _painterPath_lineTo_overload1;
        internal static ModuleMethodHandle _painterPath_cubicTo;
        internal static ModuleMethodHandle _painterPath_cubicTo_overload1;
        internal static ModuleMethodHandle _painterPathStroker_setWidth;
        internal static ModuleMethodHandle _painterPathStroker_setJoinStyle;
        internal static ModuleMethodHandle _painterPathStroker_setCapStyle;
        internal static ModuleMethodHandle _painterPathStroker_setDashPattern;
        internal static ModuleMethodHandle _painterPathStroker_setDashPattern_overload1;
        internal static ModuleMethodHandle _painterPathStroker_createStroke;
        internal static ModuleMethodHandle _handle_createColor;
        internal static ModuleMethodHandle _handle_createColor_overload1;
        internal static ModuleMethodHandle _handle_createColor_overload2;
        internal static ModuleMethodHandle _handle_createColor_overload3;
        internal static ModuleMethodHandle _handle_createColor_overload4;
        internal static ModuleMethodHandle _handle_createRadialGradient;
        internal static ModuleMethodHandle _handle_createLinearGradient;
        internal static ModuleMethodHandle _handle_createLinearGradient_overload1;
        internal static ModuleMethodHandle _handle_createBrush;
        internal static ModuleMethodHandle _handle_createBrush_overload1;
        internal static ModuleMethodHandle _handle_createBrush_overload2;
        internal static ModuleMethodHandle _handle_createPen;
        internal static ModuleMethodHandle _handle_createPen_overload1;
        internal static ModuleMethodHandle _handle_createPen_overload2;
        internal static ModuleMethodHandle _handle_createPen_overload3;
        internal static ModuleMethodHandle _handle_createFont;
        internal static ModuleMethodHandle _handle_createFont_overload1;
        internal static ModuleMethodHandle _handle_createFont_overload2;
        internal static ModuleMethodHandle _handle_createPainterPath;
        internal static ModuleMethodHandle _handle_createPainterPathStroker;
        internal static ModuleMethodHandle _handle_createStrokeInternal;
        internal static ModuleMethodHandle _handle_dispose;

        public static Handle Create()
        {
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }
        public class Color : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Color(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Color other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public enum Constant
            {
                Black,
                White,
                DarkGray,
                Gray,
                LightGray,
                Red,
                Green,
                Blue,
                Cyan,
                Magenta,
                Yellow,
                DarkRed,
                DarkGreen,
                DarkBlue,
                DarkCyan,
                DarkMagenta,
                DarkYellow,
                Transparent
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void Constant__Push(Constant value)
            {
                NativeImplClient.PushInt32((int)value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Constant Constant__Pop()
            {
                var ret = NativeImplClient.PopInt32();
                return (Constant)ret;
            }
            public abstract record Deferred
            {
                internal abstract void Push(bool isReturn);
                internal static Deferred Pop()
                {
                    return NativeImplClient.PopInt32() switch
                    {
                        0 => FromConstant.PopDerived(),
                        1 => FromRGB.PopDerived(),
                        2 => FromRGBA.PopDerived(),
                        3 => FromRGBF.PopDerived(),
                        4 => FromRGBAF.PopDerived(),
                        _ => throw new Exception("Deferred.Pop() - unknown tag!")
                    };
                }
                public sealed record FromConstant(Constant Name) : Deferred
                {
                    public Constant Name { get; } = Name;
                    internal override void Push(bool isReturn)
                    {
                        Constant__Push(Name);
                        // kind
                        NativeImplClient.PushInt32(0);
                    }
                    internal static FromConstant PopDerived()
                    {
                        var name = Constant__Pop();
                        return new FromConstant(name);
                    }
                }
                public sealed record FromRGB(int R, int G, int B) : Deferred
                {
                    public int R { get; } = R;
                    public int G { get; } = G;
                    public int B { get; } = B;
                    internal override void Push(bool isReturn)
                    {
                        NativeImplClient.PushInt32(B);
                        NativeImplClient.PushInt32(G);
                        NativeImplClient.PushInt32(R);
                        // kind
                        NativeImplClient.PushInt32(1);
                    }
                    internal static FromRGB PopDerived()
                    {
                        var r = NativeImplClient.PopInt32();
                        var g = NativeImplClient.PopInt32();
                        var b = NativeImplClient.PopInt32();
                        return new FromRGB(r, g, b);
                    }
                }
                public sealed record FromRGBA(int R, int G, int B, int A) : Deferred
                {
                    public int R { get; } = R;
                    public int G { get; } = G;
                    public int B { get; } = B;
                    public int A { get; } = A;
                    internal override void Push(bool isReturn)
                    {
                        NativeImplClient.PushInt32(A);
                        NativeImplClient.PushInt32(B);
                        NativeImplClient.PushInt32(G);
                        NativeImplClient.PushInt32(R);
                        // kind
                        NativeImplClient.PushInt32(2);
                    }
                    internal static FromRGBA PopDerived()
                    {
                        var r = NativeImplClient.PopInt32();
                        var g = NativeImplClient.PopInt32();
                        var b = NativeImplClient.PopInt32();
                        var a = NativeImplClient.PopInt32();
                        return new FromRGBA(r, g, b, a);
                    }
                }
                public sealed record FromRGBF(float R, float G, float B) : Deferred
                {
                    public float R { get; } = R;
                    public float G { get; } = G;
                    public float B { get; } = B;
                    internal override void Push(bool isReturn)
                    {
                        NativeImplClient.PushFloat(B);
                        NativeImplClient.PushFloat(G);
                        NativeImplClient.PushFloat(R);
                        // kind
                        NativeImplClient.PushInt32(3);
                    }
                    internal static FromRGBF PopDerived()
                    {
                        var r = NativeImplClient.PopFloat();
                        var g = NativeImplClient.PopFloat();
                        var b = NativeImplClient.PopFloat();
                        return new FromRGBF(r, g, b);
                    }
                }
                public sealed record FromRGBAF(float R, float G, float B, float A) : Deferred
                {
                    public float R { get; } = R;
                    public float G { get; } = G;
                    public float B { get; } = B;
                    public float A { get; } = A;
                    internal override void Push(bool isReturn)
                    {
                        NativeImplClient.PushFloat(A);
                        NativeImplClient.PushFloat(B);
                        NativeImplClient.PushFloat(G);
                        NativeImplClient.PushFloat(R);
                        // kind
                        NativeImplClient.PushInt32(4);
                    }
                    internal static FromRGBAF PopDerived()
                    {
                        var r = NativeImplClient.PopFloat();
                        var g = NativeImplClient.PopFloat();
                        var b = NativeImplClient.PopFloat();
                        var a = NativeImplClient.PopFloat();
                        return new FromRGBAF(r, g, b, a);
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void Deferred__Push(Deferred thing, bool isReturn)
            {
                thing.Push(isReturn);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Deferred Deferred__Pop()
            {
                return Deferred.Pop();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Color__Push(Color thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Color Color__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Color(ptr) : null;
        }
        public class Gradient : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Gradient(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Gradient other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public void SetColorAt(double location, Color color)
            {
                Color__Push(color);
                NativeImplClient.PushDouble(location);
                Gradient__Push(this);
                NativeImplClient.InvokeModuleMethod(_gradient_setColorAt);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Gradient__Push(Gradient thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Gradient Gradient__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Gradient(ptr) : null;
        }
        public class RadialGradient : Gradient
        {
            internal RadialGradient(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void RadialGradient__Push(RadialGradient thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RadialGradient RadialGradient__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new RadialGradient(ptr) : null;
        }
        public class LinearGradient : Gradient
        {
            internal LinearGradient(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LinearGradient__Push(LinearGradient thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static LinearGradient LinearGradient__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new LinearGradient(ptr) : null;
        }
        public class Brush : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Brush(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Brush other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public enum Style
            {
                NoBrush = 0,
                SolidPattern = 1,
                Dense1Pattern = 2,
                Dense2Pattern = 3,
                Dense3Pattern = 4,
                Dense4Pattern = 5,
                Dense5Pattern = 6,
                Dense6Pattern = 7,
                Dense7Pattern = 8,
                HorPattern = 9,
                VerPattern = 10,
                CrossPattern = 11,
                BDiagPattern = 12,
                FDiagPattern = 13,
                DiagCrossPattern = 14,
                LinearGradientPattern = 15,
                ConicalGradientPattern = 17,
                RadialGradientPattern = 16,
                TexturePattern = 24
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void Style__Push(Style value)
            {
                NativeImplClient.PushInt32((int)value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Style Style__Pop()
            {
                var ret = NativeImplClient.PopInt32();
                return (Style)ret;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Brush__Push(Brush thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Brush Brush__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Brush(ptr) : null;
        }
        public class Pen : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Pen(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Pen other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public enum Style
            {
                NoPen,
                SolidLine,
                DashLine,
                DotLine,
                DashDotLine,
                DashDotDotLine,
                CustomDashLine
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void Style__Push(Style value)
            {
                NativeImplClient.PushInt32((int)value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Style Style__Pop()
            {
                var ret = NativeImplClient.PopInt32();
                return (Style)ret;
            }
            public enum CapStyle
            {
                Flat,
                Square,
                Round
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void CapStyle__Push(CapStyle value)
            {
                NativeImplClient.PushInt32((int)value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static CapStyle CapStyle__Pop()
            {
                var ret = NativeImplClient.PopInt32();
                return (CapStyle)ret;
            }
            public enum JoinStyle
            {
                Miter,
                Bevel,
                Round,
                SvgMiter
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void JoinStyle__Push(JoinStyle value)
            {
                NativeImplClient.PushInt32((int)value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static JoinStyle JoinStyle__Pop()
            {
                var ret = NativeImplClient.PopInt32();
                return (JoinStyle)ret;
            }
            public void SetBrush(Brush brush)
            {
                Brush__Push(brush);
                Pen__Push(this);
                NativeImplClient.InvokeModuleMethod(_pen_setBrush);
            }
            public void SetWidth(int width)
            {
                NativeImplClient.PushInt32(width);
                Pen__Push(this);
                NativeImplClient.InvokeModuleMethod(_pen_setWidth);
            }
            public void SetWidth(double width)
            {
                NativeImplClient.PushDouble(width);
                Pen__Push(this);
                NativeImplClient.InvokeModuleMethod(_pen_setWidth_overload1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Pen__Push(Pen thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Pen Pen__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Pen(ptr) : null;
        }
        public class Font : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Font(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Font other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public enum Weight
            {
                Thin = 100,
                ExtraLight = 200,
                Light = 300,
                Normal = 400,
                Medium = 500,
                DemiBold = 600,
                Bold = 700,
                ExtraBold = 800,
                Black = 900
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static void Weight__Push(Weight value)
            {
                NativeImplClient.PushInt32((int)value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Weight Weight__Pop()
            {
                var ret = NativeImplClient.PopInt32();
                return (Weight)ret;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Font__Push(Font thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Font Font__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Font(ptr) : null;
        }
        public class PainterPath : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal PainterPath(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is PainterPath other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public void MoveTo(PointF p)
            {
                PointF__Push(p, false);
                PainterPath__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPath_moveTo);
            }
            public void MoveTo(double x, double y)
            {
                NativeImplClient.PushDouble(y);
                NativeImplClient.PushDouble(x);
                PainterPath__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPath_moveTo_overload1);
            }
            public void Lineto(PointF p)
            {
                PointF__Push(p, false);
                PainterPath__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPath_lineto);
            }
            public void LineTo(double x, double y)
            {
                NativeImplClient.PushDouble(y);
                NativeImplClient.PushDouble(x);
                PainterPath__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPath_lineTo_overload1);
            }
            public void CubicTo(PointF c1, PointF c2, PointF endPoint)
            {
                PointF__Push(endPoint, false);
                PointF__Push(c2, false);
                PointF__Push(c1, false);
                PainterPath__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPath_cubicTo);
            }
            public void CubicTo(double c1X, double c1Y, double c2X, double c2Y, double endPointX, double endPointY)
            {
                NativeImplClient.PushDouble(endPointY);
                NativeImplClient.PushDouble(endPointX);
                NativeImplClient.PushDouble(c2Y);
                NativeImplClient.PushDouble(c2X);
                NativeImplClient.PushDouble(c1Y);
                NativeImplClient.PushDouble(c1X);
                PainterPath__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPath_cubicTo_overload1);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PainterPath__Push(PainterPath thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PainterPath PainterPath__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new PainterPath(ptr) : null;
        }
        public class PainterPathStroker : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal PainterPathStroker(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is PainterPathStroker other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public void SetWidth(double width)
            {
                NativeImplClient.PushDouble(width);
                PainterPathStroker__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPathStroker_setWidth);
            }
            public void SetJoinStyle(Pen.JoinStyle style)
            {
                Pen.JoinStyle__Push(style);
                PainterPathStroker__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPathStroker_setJoinStyle);
            }
            public void SetCapStyle(Pen.CapStyle style)
            {
                Pen.CapStyle__Push(style);
                PainterPathStroker__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPathStroker_setCapStyle);
            }
            public void SetDashPattern(Pen.Style style)
            {
                Pen.Style__Push(style);
                PainterPathStroker__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPathStroker_setDashPattern);
            }
            public void SetDashPattern(double[] dashPattern)
            {
                NativeImplClient.PushDoubleArray(dashPattern);
                PainterPathStroker__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPathStroker_setDashPattern_overload1);
            }
            public PainterPath CreateStroke(PainterPath path)
            {
                PainterPath__Push(path);
                PainterPathStroker__Push(this);
                NativeImplClient.InvokeModuleMethod(_painterPathStroker_createStroke);
                return PainterPath__Pop();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PainterPathStroker__Push(PainterPathStroker thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PainterPathStroker PainterPathStroker__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new PainterPathStroker(ptr) : null;
        }
        public class Handle : IComparable, IDisposable
        {
            internal readonly IntPtr NativeHandle;
            protected bool _disposed;
            internal Handle(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Handle other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public virtual void Dispose()
            {
                if (!_disposed)
                {
                    Handle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_handle_dispose);
                    _disposed = true;
                }
            }
            public Color CreateColor(Color.Constant name)
            {
                Color.Constant__Push(name);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createColor);
                return Color__Pop();
            }
            public Color CreateColor(int r, int g, int b)
            {
                NativeImplClient.PushInt32(b);
                NativeImplClient.PushInt32(g);
                NativeImplClient.PushInt32(r);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createColor_overload1);
                return Color__Pop();
            }
            public Color CreateColor(int r, int g, int b, int a)
            {
                NativeImplClient.PushInt32(a);
                NativeImplClient.PushInt32(b);
                NativeImplClient.PushInt32(g);
                NativeImplClient.PushInt32(r);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createColor_overload2);
                return Color__Pop();
            }
            public Color CreateColor(float r, float g, float b)
            {
                NativeImplClient.PushFloat(b);
                NativeImplClient.PushFloat(g);
                NativeImplClient.PushFloat(r);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createColor_overload3);
                return Color__Pop();
            }
            public Color CreateColor(float r, float g, float b, float a)
            {
                NativeImplClient.PushFloat(a);
                NativeImplClient.PushFloat(b);
                NativeImplClient.PushFloat(g);
                NativeImplClient.PushFloat(r);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createColor_overload4);
                return Color__Pop();
            }
            public RadialGradient CreateRadialGradient(PointF center, double radius)
            {
                NativeImplClient.PushDouble(radius);
                PointF__Push(center, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createRadialGradient);
                return RadialGradient__Pop();
            }
            public LinearGradient CreateLinearGradient(PointF start, PointF stop)
            {
                PointF__Push(stop, false);
                PointF__Push(start, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createLinearGradient);
                return LinearGradient__Pop();
            }
            public LinearGradient CreateLinearGradient(double x1, double y1, double x2, double y2)
            {
                NativeImplClient.PushDouble(y2);
                NativeImplClient.PushDouble(x2);
                NativeImplClient.PushDouble(y1);
                NativeImplClient.PushDouble(x1);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createLinearGradient_overload1);
                return LinearGradient__Pop();
            }
            public Brush CreateBrush(Brush.Style style)
            {
                Brush.Style__Push(style);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createBrush);
                return Brush__Pop();
            }
            public Brush CreateBrush(Color color)
            {
                Color__Push(color);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createBrush_overload1);
                return Brush__Pop();
            }
            public Brush CreateBrush(Gradient gradient)
            {
                Gradient__Push(gradient);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createBrush_overload2);
                return Brush__Pop();
            }
            public Pen CreatePen()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createPen);
                return Pen__Pop();
            }
            public Pen CreatePen(Pen.Style style)
            {
                Pen.Style__Push(style);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createPen_overload1);
                return Pen__Pop();
            }
            public Pen CreatePen(Color color)
            {
                Color__Push(color);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createPen_overload2);
                return Pen__Pop();
            }
            public Pen CreatePen(Brush brush, double width, Pen.Style style, Pen.CapStyle cap, Pen.JoinStyle join)
            {
                Pen.JoinStyle__Push(join);
                Pen.CapStyle__Push(cap);
                Pen.Style__Push(style);
                NativeImplClient.PushDouble(width);
                Brush__Push(brush);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createPen_overload3);
                return Pen__Pop();
            }
            public Font CreateFont(string family, int pointSize)
            {
                NativeImplClient.PushInt32(pointSize);
                NativeImplClient.PushString(family);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createFont);
                return Font__Pop();
            }
            public Font CreateFont(string family, int pointSize, Font.Weight weight)
            {
                Font.Weight__Push(weight);
                NativeImplClient.PushInt32(pointSize);
                NativeImplClient.PushString(family);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createFont_overload1);
                return Font__Pop();
            }
            public Font CreateFont(string family, int pointSize, Font.Weight weight, bool italic)
            {
                NativeImplClient.PushBool(italic);
                Font.Weight__Push(weight);
                NativeImplClient.PushInt32(pointSize);
                NativeImplClient.PushString(family);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createFont_overload2);
                return Font__Pop();
            }
            public PainterPath CreatePainterPath()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createPainterPath);
                return PainterPath__Pop();
            }
            public PainterPathStroker CreatePainterPathStroker()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createPainterPathStroker);
                return PainterPathStroker__Pop();
            }
            public PainterPath CreateStrokeInternal(PainterPathStroker stroker, PainterPath path)
            {
                PainterPath__Push(path);
                PainterPathStroker__Push(stroker);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_createStrokeInternal);
                return PainterPath__Pop();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Handle__Push(Handle thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Handle Handle__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Handle(ptr) : null;
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("PaintResources");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _gradient_setColorAt = NativeImplClient.GetModuleMethod(_module, "Gradient_setColorAt");
            _pen_setBrush = NativeImplClient.GetModuleMethod(_module, "Pen_setBrush");
            _pen_setWidth = NativeImplClient.GetModuleMethod(_module, "Pen_setWidth");
            _pen_setWidth_overload1 = NativeImplClient.GetModuleMethod(_module, "Pen_setWidth_overload1");
            _painterPath_moveTo = NativeImplClient.GetModuleMethod(_module, "PainterPath_moveTo");
            _painterPath_moveTo_overload1 = NativeImplClient.GetModuleMethod(_module, "PainterPath_moveTo_overload1");
            _painterPath_lineto = NativeImplClient.GetModuleMethod(_module, "PainterPath_lineto");
            _painterPath_lineTo_overload1 = NativeImplClient.GetModuleMethod(_module, "PainterPath_lineTo_overload1");
            _painterPath_cubicTo = NativeImplClient.GetModuleMethod(_module, "PainterPath_cubicTo");
            _painterPath_cubicTo_overload1 = NativeImplClient.GetModuleMethod(_module, "PainterPath_cubicTo_overload1");
            _painterPathStroker_setWidth = NativeImplClient.GetModuleMethod(_module, "PainterPathStroker_setWidth");
            _painterPathStroker_setJoinStyle = NativeImplClient.GetModuleMethod(_module, "PainterPathStroker_setJoinStyle");
            _painterPathStroker_setCapStyle = NativeImplClient.GetModuleMethod(_module, "PainterPathStroker_setCapStyle");
            _painterPathStroker_setDashPattern = NativeImplClient.GetModuleMethod(_module, "PainterPathStroker_setDashPattern");
            _painterPathStroker_setDashPattern_overload1 = NativeImplClient.GetModuleMethod(_module, "PainterPathStroker_setDashPattern_overload1");
            _painterPathStroker_createStroke = NativeImplClient.GetModuleMethod(_module, "PainterPathStroker_createStroke");
            _handle_createColor = NativeImplClient.GetModuleMethod(_module, "Handle_createColor");
            _handle_createColor_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_createColor_overload1");
            _handle_createColor_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_createColor_overload2");
            _handle_createColor_overload3 = NativeImplClient.GetModuleMethod(_module, "Handle_createColor_overload3");
            _handle_createColor_overload4 = NativeImplClient.GetModuleMethod(_module, "Handle_createColor_overload4");
            _handle_createRadialGradient = NativeImplClient.GetModuleMethod(_module, "Handle_createRadialGradient");
            _handle_createLinearGradient = NativeImplClient.GetModuleMethod(_module, "Handle_createLinearGradient");
            _handle_createLinearGradient_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_createLinearGradient_overload1");
            _handle_createBrush = NativeImplClient.GetModuleMethod(_module, "Handle_createBrush");
            _handle_createBrush_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_createBrush_overload1");
            _handle_createBrush_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_createBrush_overload2");
            _handle_createPen = NativeImplClient.GetModuleMethod(_module, "Handle_createPen");
            _handle_createPen_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_createPen_overload1");
            _handle_createPen_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_createPen_overload2");
            _handle_createPen_overload3 = NativeImplClient.GetModuleMethod(_module, "Handle_createPen_overload3");
            _handle_createFont = NativeImplClient.GetModuleMethod(_module, "Handle_createFont");
            _handle_createFont_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_createFont_overload1");
            _handle_createFont_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_createFont_overload2");
            _handle_createPainterPath = NativeImplClient.GetModuleMethod(_module, "Handle_createPainterPath");
            _handle_createPainterPathStroker = NativeImplClient.GetModuleMethod(_module, "Handle_createPainterPathStroker");
            _handle_createStrokeInternal = NativeImplClient.GetModuleMethod(_module, "Handle_createStrokeInternal");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
