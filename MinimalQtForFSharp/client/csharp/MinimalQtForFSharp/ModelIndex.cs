using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Variant;
using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class ModelIndex
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_isValid;
        internal static ModuleMethodHandle _handle_row;
        internal static ModuleMethodHandle _handle_column;
        internal static ModuleMethodHandle _handle_data;
        internal static ModuleMethodHandle _handle_data_overload1;
        internal static ModuleMethodHandle _owned_dispose;
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
            public bool IsValid()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isValid);
                return NativeImplClient.PopBool();
            }
            public int Row()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_row);
                return NativeImplClient.PopInt32();
            }
            public int Column()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_column);
                return NativeImplClient.PopInt32();
            }
            public Variant.Owned Data()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_data);
                return Variant.Owned__Pop();
            }
            public Variant.Owned Data(ItemDataRole role)
            {
                ItemDataRole__Push(role);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_data_overload1);
                return Variant.Owned__Pop();
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
                    0 => Empty.PopDerived(),
                    1 => FromHandle.PopDerived(),
                    2 => FromOwned.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record Empty : Deferred
            {
                internal override void Push(bool isReturn)
                {
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static Empty PopDerived()
                {
                    return new Empty();
                }
            }
            public sealed record FromHandle(Handle Handle) : Deferred
            {
                public Handle Handle { get; } = Handle;
                internal override void Push(bool isReturn)
                {
                    Handle__Push(Handle);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromHandle PopDerived()
                {
                    var handle = Handle__Pop();
                    return new FromHandle(handle);
                }
            }
            public sealed record FromOwned(Owned Owned) : Deferred
            {
                public Owned Owned { get; } = Owned;
                internal override void Push(bool isReturn)
                {
                    Owned__Push(Owned);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromOwned PopDerived()
                {
                    var owned = Owned__Pop();
                    return new FromOwned(owned);
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
            _module = NativeImplClient.GetModule("ModelIndex");
            // assign module handles
            _handle_isValid = NativeImplClient.GetModuleMethod(_module, "Handle_isValid");
            _handle_row = NativeImplClient.GetModuleMethod(_module, "Handle_row");
            _handle_column = NativeImplClient.GetModuleMethod(_module, "Handle_column");
            _handle_data = NativeImplClient.GetModuleMethod(_module, "Handle_data");
            _handle_data_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_data_overload1");
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
