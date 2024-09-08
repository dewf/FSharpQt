using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Common
    {
        private static ModuleHandle _module;
        public struct Point {
            public int X;
            public int Y;
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        internal static void Point__Push(Point value, bool isReturn)
        {
            NativeImplClient.PushInt32(value.Y);
            NativeImplClient.PushInt32(value.X);
        }

        internal static Point Point__Pop()
        {
            var x = NativeImplClient.PopInt32();
            var y = NativeImplClient.PopInt32();
            return new Point(x, y);
        }
        public struct PointF {
            public double X;
            public double Y;
            public PointF(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        internal static void PointF__Push(PointF value, bool isReturn)
        {
            NativeImplClient.PushDouble(value.Y);
            NativeImplClient.PushDouble(value.X);
        }

        internal static PointF PointF__Pop()
        {
            var x = NativeImplClient.PopDouble();
            var y = NativeImplClient.PopDouble();
            return new PointF(x, y);
        }
        public struct Size {
            public int Width;
            public int Height;
            public Size(int width, int height)
            {
                this.Width = width;
                this.Height = height;
            }
        }

        internal static void Size__Push(Size value, bool isReturn)
        {
            NativeImplClient.PushInt32(value.Height);
            NativeImplClient.PushInt32(value.Width);
        }

        internal static Size Size__Pop()
        {
            var width = NativeImplClient.PopInt32();
            var height = NativeImplClient.PopInt32();
            return new Size(width, height);
        }
        public struct Rect {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public Rect(int x, int y, int width, int height)
            {
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
            }
        }

        internal static void Rect__Push(Rect value, bool isReturn)
        {
            NativeImplClient.PushInt32(value.Height);
            NativeImplClient.PushInt32(value.Width);
            NativeImplClient.PushInt32(value.Y);
            NativeImplClient.PushInt32(value.X);
        }

        internal static Rect Rect__Pop()
        {
            var x = NativeImplClient.PopInt32();
            var y = NativeImplClient.PopInt32();
            var width = NativeImplClient.PopInt32();
            var height = NativeImplClient.PopInt32();
            return new Rect(x, y, width, height);
        }
        public struct RectF {
            public double X;
            public double Y;
            public double Width;
            public double Height;
            public RectF(double x, double y, double width, double height)
            {
                this.X = x;
                this.Y = y;
                this.Width = width;
                this.Height = height;
            }
        }

        internal static void RectF__Push(RectF value, bool isReturn)
        {
            NativeImplClient.PushDouble(value.Height);
            NativeImplClient.PushDouble(value.Width);
            NativeImplClient.PushDouble(value.Y);
            NativeImplClient.PushDouble(value.X);
        }

        internal static RectF RectF__Pop()
        {
            var x = NativeImplClient.PopDouble();
            var y = NativeImplClient.PopDouble();
            var width = NativeImplClient.PopDouble();
            var height = NativeImplClient.PopDouble();
            return new RectF(x, y, width, height);
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Common");
            // assign module handles

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
