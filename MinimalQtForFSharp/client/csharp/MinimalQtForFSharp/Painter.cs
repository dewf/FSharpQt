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
using static Org.Whatever.MinimalQtForFSharp.PaintResources;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.Color;
using static Org.Whatever.MinimalQtForFSharp.Pixmap;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Painter
    {
        private static ModuleHandle _module;

        // built-in array type: int[]

        internal static void __Point_Array__Push(Point[] items, bool isReturn)
        {
            var count = items.Length;
            var f0Values = new int[count];
            var f1Values = new int[count];
            for (var i = 0; i < count; i++)
            {
                f0Values[i] = items[i].X;
                f1Values[i] = items[i].Y;
            }
            NativeImplClient.PushInt32Array(f1Values);
            NativeImplClient.PushInt32Array(f0Values);
        }

        internal static Point[] __Point_Array__Pop()
        {
            var f0Values = NativeImplClient.PopInt32Array();
            var f1Values = NativeImplClient.PopInt32Array();
            var count = f0Values.Length;
            var ret = new Point[count];
            for (var i = 0; i < count; i++)
            {
                var f0 = f0Values[i];
                var f1 = f1Values[i];
                ret[i] = new Point(f0, f1);
            }
            return ret;
        }

        // built-in array type: double[]

        internal static void __PointF_Array__Push(PointF[] items, bool isReturn)
        {
            var count = items.Length;
            var f0Values = new double[count];
            var f1Values = new double[count];
            for (var i = 0; i < count; i++)
            {
                f0Values[i] = items[i].X;
                f1Values[i] = items[i].Y;
            }
            NativeImplClient.PushDoubleArray(f1Values);
            NativeImplClient.PushDoubleArray(f0Values);
        }

        internal static PointF[] __PointF_Array__Pop()
        {
            var f0Values = NativeImplClient.PopDoubleArray();
            var f1Values = NativeImplClient.PopDoubleArray();
            var count = f0Values.Length;
            var ret = new PointF[count];
            for (var i = 0; i < count; i++)
            {
                var f0 = f0Values[i];
                var f1 = f1Values[i];
                ret[i] = new PointF(f0, f1);
            }
            return ret;
        }
        internal static ModuleMethodHandle _handle_setRenderHint;
        internal static ModuleMethodHandle _handle_setRenderHints;
        internal static ModuleMethodHandle _handle_setPen;
        internal static ModuleMethodHandle _handle_setBrush;
        internal static ModuleMethodHandle _handle_setFont;
        internal static ModuleMethodHandle _handle_drawText;
        internal static ModuleMethodHandle _handle_fillPath;
        internal static ModuleMethodHandle _handle_strokePath;
        internal static ModuleMethodHandle _handle_fillRect;
        internal static ModuleMethodHandle _handle_fillRect_overload1;
        internal static ModuleMethodHandle _handle_drawRect;
        internal static ModuleMethodHandle _handle_drawRect_overload1;
        internal static ModuleMethodHandle _handle_drawRect_overload2;
        internal static ModuleMethodHandle _handle_drawEllipse;
        internal static ModuleMethodHandle _handle_drawEllipse_overload1;
        internal static ModuleMethodHandle _handle_drawEllipse_overload2;
        internal static ModuleMethodHandle _handle_drawEllipse_overload3;
        internal static ModuleMethodHandle _handle_drawEllipse_overload4;
        internal static ModuleMethodHandle _handle_drawPolyline;
        internal static ModuleMethodHandle _handle_drawPolyline_overload1;
        internal static ModuleMethodHandle _handle_drawPixmap;
        internal static ModuleMethodHandle _handle_drawPixmap_overload1;
        internal static ModuleMethodHandle _handle_drawPixmap_overload2;
        internal static ModuleMethodHandle _handle_drawPixmap_overload3;
        internal static ModuleMethodHandle _handle_drawPixmap_overload4;
        internal static ModuleMethodHandle _handle_drawPixmap_overload5;
        internal static ModuleMethodHandle _handle_drawPixmap_overload6;
        internal static ModuleMethodHandle _handle_drawPixmap_overload7;
        internal static ModuleMethodHandle _handle_drawPixmap_overload8;
        internal static ModuleMethodHandle _handle_drawPixmap_overload9;
        internal static ModuleMethodHandle _handle_drawPixmap_overload10;
        public enum RenderHint
        {
            Antialiasing = 0x01,
            TextAntialiasing = 0x02,
            SmoothPixmapTransform = 0x04,
            VerticalSubpixelPositioning = 0x08,
            LosslessImageRendering = 0x40,
            NonCosmeticBrushPatterns = 0x80
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void RenderHint__Push(RenderHint value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RenderHint RenderHint__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (RenderHint)ret;
        }
        [Flags]
        public enum RenderHintSet
        {
            Antialiasing = 0x01,
            TextAntialiasing = 0x02,
            SmoothPixmapTransform = 0x04,
            VerticalSubpixelPositioning = 0x08,
            LosslessImageRendering = 0x40,
            NonCosmeticBrushPatterns = 0x80
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void RenderHintSet__Push(RenderHintSet value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static RenderHintSet RenderHintSet__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (RenderHintSet)ret;
        }
        public class Handle : IComparable
        {
            internal readonly IntPtr NativeHandle;
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
            public void SetRenderHint(RenderHint hint, bool on)
            {
                NativeImplClient.PushBool(on);
                RenderHint__Push(hint);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRenderHint);
            }
            public void SetRenderHints(RenderHintSet hints, bool on)
            {
                NativeImplClient.PushBool(on);
                RenderHintSet__Push(hints);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRenderHints);
            }
            public void SetPen(Pen pen)
            {
                Pen__Push(pen);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPen);
            }
            public void SetBrush(Brush brush)
            {
                Brush__Push(brush);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setBrush);
            }
            public void SetFont(Font font)
            {
                Font__Push(font);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFont);
            }
            public void DrawText(Rect rect, Alignment align, string text)
            {
                NativeImplClient.PushString(text);
                Alignment__Push(align);
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawText);
            }
            public void FillPath(PainterPath path, Brush brush)
            {
                Brush__Push(brush);
                PainterPath__Push(path);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_fillPath);
            }
            public void StrokePath(PainterPath path, Pen pen)
            {
                Pen__Push(pen);
                PainterPath__Push(path);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_strokePath);
            }
            public void FillRect(Rect rect, Brush brush)
            {
                Brush__Push(brush);
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_fillRect);
            }
            public void FillRect(Rect rect, Deferred color)
            {
                Deferred__Push(color, false);
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_fillRect_overload1);
            }
            public void DrawRect(Rect rect)
            {
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawRect);
            }
            public void DrawRect(RectF rect)
            {
                RectF__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawRect_overload1);
            }
            public void DrawRect(int x, int y, int width, int height)
            {
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawRect_overload2);
            }
            public void DrawEllipse(RectF rectangle)
            {
                RectF__Push(rectangle, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawEllipse);
            }
            public void DrawEllipse(Rect rectangle)
            {
                Rect__Push(rectangle, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawEllipse_overload1);
            }
            public void DrawEllipse(int x, int y, int width, int height)
            {
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawEllipse_overload2);
            }
            public void DrawEllipse(PointF center, double rx, double ry)
            {
                NativeImplClient.PushDouble(ry);
                NativeImplClient.PushDouble(rx);
                PointF__Push(center, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawEllipse_overload3);
            }
            public void DrawEllipse(Point center, int rx, int ry)
            {
                NativeImplClient.PushInt32(ry);
                NativeImplClient.PushInt32(rx);
                Point__Push(center, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawEllipse_overload4);
            }
            public void DrawPolyline(PointF[] points)
            {
                __PointF_Array__Push(points, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPolyline);
            }
            public void DrawPolyline(Point[] points)
            {
                __Point_Array__Push(points, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPolyline_overload1);
            }
            public void DrawPixmap(RectF target, Pixmap.Handle pixmap, RectF source)
            {
                RectF__Push(source, false);
                Pixmap.Handle__Push(pixmap);
                RectF__Push(target, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap);
            }
            public void DrawPixmap(Point point, Pixmap.Handle pixmap)
            {
                Pixmap.Handle__Push(pixmap);
                Point__Push(point, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload1);
            }
            public void DrawPixmap(PointF point, Pixmap.Handle pixmap)
            {
                Pixmap.Handle__Push(pixmap);
                PointF__Push(point, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload2);
            }
            public void DrawPixmap(Rect rect, Pixmap.Handle pixmap)
            {
                Pixmap.Handle__Push(pixmap);
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload3);
            }
            public void DrawPixmap(Point point, Pixmap.Handle pixmap, Rect source)
            {
                Rect__Push(source, false);
                Pixmap.Handle__Push(pixmap);
                Point__Push(point, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload4);
            }
            public void DrawPixmap(PointF point, Pixmap.Handle pixmap, RectF source)
            {
                RectF__Push(source, false);
                Pixmap.Handle__Push(pixmap);
                PointF__Push(point, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload5);
            }
            public void DrawPixmap(Rect target, Pixmap.Handle pixmap, Rect source)
            {
                Rect__Push(source, false);
                Pixmap.Handle__Push(pixmap);
                Rect__Push(target, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload6);
            }
            public void DrawPixmap(int x, int y, Pixmap.Handle pixmap)
            {
                Pixmap.Handle__Push(pixmap);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload7);
            }
            public void DrawPixmap(int x, int y, int width, int height, Pixmap.Handle pixmap)
            {
                Pixmap.Handle__Push(pixmap);
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload8);
            }
            public void DrawPixmap(int x, int y, Pixmap.Handle pixmap, int sx, int sy, int sw, int sh)
            {
                NativeImplClient.PushInt32(sh);
                NativeImplClient.PushInt32(sw);
                NativeImplClient.PushInt32(sy);
                NativeImplClient.PushInt32(sx);
                Pixmap.Handle__Push(pixmap);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload9);
            }
            public void DrawPixmap(int x, int y, int w, int h, Pixmap.Handle pixmap, int sx, int sy, int sw, int sh)
            {
                NativeImplClient.PushInt32(sh);
                NativeImplClient.PushInt32(sw);
                NativeImplClient.PushInt32(sy);
                NativeImplClient.PushInt32(sx);
                Pixmap.Handle__Push(pixmap);
                NativeImplClient.PushInt32(h);
                NativeImplClient.PushInt32(w);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_drawPixmap_overload10);
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
            _module = NativeImplClient.GetModule("Painter");
            // assign module handles
            _handle_setRenderHint = NativeImplClient.GetModuleMethod(_module, "Handle_setRenderHint");
            _handle_setRenderHints = NativeImplClient.GetModuleMethod(_module, "Handle_setRenderHints");
            _handle_setPen = NativeImplClient.GetModuleMethod(_module, "Handle_setPen");
            _handle_setBrush = NativeImplClient.GetModuleMethod(_module, "Handle_setBrush");
            _handle_setFont = NativeImplClient.GetModuleMethod(_module, "Handle_setFont");
            _handle_drawText = NativeImplClient.GetModuleMethod(_module, "Handle_drawText");
            _handle_fillPath = NativeImplClient.GetModuleMethod(_module, "Handle_fillPath");
            _handle_strokePath = NativeImplClient.GetModuleMethod(_module, "Handle_strokePath");
            _handle_fillRect = NativeImplClient.GetModuleMethod(_module, "Handle_fillRect");
            _handle_fillRect_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_fillRect_overload1");
            _handle_drawRect = NativeImplClient.GetModuleMethod(_module, "Handle_drawRect");
            _handle_drawRect_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_drawRect_overload1");
            _handle_drawRect_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_drawRect_overload2");
            _handle_drawEllipse = NativeImplClient.GetModuleMethod(_module, "Handle_drawEllipse");
            _handle_drawEllipse_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_drawEllipse_overload1");
            _handle_drawEllipse_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_drawEllipse_overload2");
            _handle_drawEllipse_overload3 = NativeImplClient.GetModuleMethod(_module, "Handle_drawEllipse_overload3");
            _handle_drawEllipse_overload4 = NativeImplClient.GetModuleMethod(_module, "Handle_drawEllipse_overload4");
            _handle_drawPolyline = NativeImplClient.GetModuleMethod(_module, "Handle_drawPolyline");
            _handle_drawPolyline_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPolyline_overload1");
            _handle_drawPixmap = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap");
            _handle_drawPixmap_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload1");
            _handle_drawPixmap_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload2");
            _handle_drawPixmap_overload3 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload3");
            _handle_drawPixmap_overload4 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload4");
            _handle_drawPixmap_overload5 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload5");
            _handle_drawPixmap_overload6 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload6");
            _handle_drawPixmap_overload7 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload7");
            _handle_drawPixmap_overload8 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload8");
            _handle_drawPixmap_overload9 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload9");
            _handle_drawPixmap_overload10 = NativeImplClient.GetModuleMethod(_module, "Handle_drawPixmap_overload10");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
