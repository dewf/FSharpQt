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
    public static class Color
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _owned_dispose;
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
        public class Owned : Handle, IDisposable
        {
            protected bool _disposed;
            internal Owned(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public virtual void Dispose()
            {
                if (!_disposed)
                {
                    Owned__Push(this);
                    NativeImplClient.InvokeModuleMethod(_owned_dispose);
                    _disposed = true;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Owned__Push(Owned thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Owned Owned__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Owned(ptr) : null;
        }
        public abstract record Deferred
        {
            internal abstract void Push(bool isReturn);
            internal static Deferred Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => FromConstant.PopDerived(),
                    1 => FromHandle.PopDerived(),
                    2 => FromRGB.PopDerived(),
                    3 => FromRGBA.PopDerived(),
                    4 => FromRGBF.PopDerived(),
                    5 => FromRGBAF.PopDerived(),
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
            public sealed record FromHandle(Handle Color) : Deferred
            {
                public Handle Color { get; } = Color;
                internal override void Push(bool isReturn)
                {
                    Handle__Push(Color);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromHandle PopDerived()
                {
                    var color = Handle__Pop();
                    return new FromHandle(color);
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
                    NativeImplClient.PushInt32(2);
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
                    NativeImplClient.PushInt32(3);
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
                    NativeImplClient.PushInt32(4);
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
                    NativeImplClient.PushInt32(5);
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

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Color");
            // assign module handles
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
