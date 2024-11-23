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
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _create_overload1;
        internal static ModuleMethodHandle _create_overload2;
        internal static ModuleMethodHandle _create_overload3;
        internal static ModuleMethodHandle _create_overload4;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Create(Constant name)
        {
            Constant__Push(name);
            NativeImplClient.InvokeModuleMethod(_create);
            return Owned__Pop();
        }

        public static Owned Create(int r, int g, int b)
        {
            NativeImplClient.PushInt32(b);
            NativeImplClient.PushInt32(g);
            NativeImplClient.PushInt32(r);
            NativeImplClient.InvokeModuleMethod(_create_overload1);
            return Owned__Pop();
        }

        public static Owned Create(int r, int g, int b, int a)
        {
            NativeImplClient.PushInt32(a);
            NativeImplClient.PushInt32(b);
            NativeImplClient.PushInt32(g);
            NativeImplClient.PushInt32(r);
            NativeImplClient.InvokeModuleMethod(_create_overload2);
            return Owned__Pop();
        }

        public static Owned Create(float r, float g, float b)
        {
            NativeImplClient.PushFloat(b);
            NativeImplClient.PushFloat(g);
            NativeImplClient.PushFloat(r);
            NativeImplClient.InvokeModuleMethod(_create_overload3);
            return Owned__Pop();
        }

        public static Owned Create(float r, float g, float b, float a)
        {
            NativeImplClient.PushFloat(a);
            NativeImplClient.PushFloat(b);
            NativeImplClient.PushFloat(g);
            NativeImplClient.PushFloat(r);
            NativeImplClient.InvokeModuleMethod(_create_overload4);
            return Owned__Pop();
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

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Color");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _create_overload1 = NativeImplClient.GetModuleMethod(_module, "create_overload1");
            _create_overload2 = NativeImplClient.GetModuleMethod(_module, "create_overload2");
            _create_overload3 = NativeImplClient.GetModuleMethod(_module, "create_overload3");
            _create_overload4 = NativeImplClient.GetModuleMethod(_module, "create_overload4");
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
