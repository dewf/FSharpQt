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
    public static class SizePolicy
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_dispose;
        internal static ModuleMethodHandle _ownedHandle_dispose;
        [Flags]
        public enum Policy
        {
            GrowFlag = 1,
            ExpandFlag = 2,
            ShrinkFlag = 4,
            IgnoreFlag = 8,
            Fixed = 0,
            Minimum = GrowFlag,
            Maximum = ShrinkFlag,
            Preferred = GrowFlag | ShrinkFlag,
            MinimumExpanding = GrowFlag | ExpandFlag,
            Expanding = GrowFlag | ShrinkFlag | ExpandFlag,
            Ignored = ShrinkFlag | GrowFlag | IgnoreFlag
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Policy__Push(Policy value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Policy Policy__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Policy)ret;
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
        public class OwnedHandle : Handle
        {
            internal OwnedHandle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public override void Dispose()
            {
                if (!_disposed)
                {
                    OwnedHandle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_ownedHandle_dispose);
                    _disposed = true;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void OwnedHandle__Push(OwnedHandle thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static OwnedHandle OwnedHandle__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new OwnedHandle(ptr) : null;
        }
        public abstract record Deferred
        {
            internal abstract void Push(bool isReturn);
            internal static Deferred Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => Todo.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record Todo : Deferred
            {
                internal override void Push(bool isReturn)
                {
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static Todo PopDerived()
                {
                    return new Todo();
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
            _module = NativeImplClient.GetModule("SizePolicy");
            // assign module handles
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _ownedHandle_dispose = NativeImplClient.GetModuleMethod(_module, "OwnedHandle_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
