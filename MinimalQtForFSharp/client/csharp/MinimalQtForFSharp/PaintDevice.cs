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
    public static class PaintDevice
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_width;
        internal static ModuleMethodHandle _handle_height;
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
            public int Width()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_width);
                return NativeImplClient.PopInt32();
            }
            public int Height()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_height);
                return NativeImplClient.PopInt32();
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
            _module = NativeImplClient.GetModule("PaintDevice");
            // assign module handles
            _handle_width = NativeImplClient.GetModuleMethod(_module, "Handle_width");
            _handle_height = NativeImplClient.GetModuleMethod(_module, "Handle_height");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
