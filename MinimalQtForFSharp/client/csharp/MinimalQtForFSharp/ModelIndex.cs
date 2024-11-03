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
        internal static ModuleMethodHandle _handle_dispose;
        internal static ModuleMethodHandle _ownedHandle_dispose;
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
            public Variant.OwnedHandle Data()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_data);
                return Variant.OwnedHandle__Pop();
            }
            public Variant.OwnedHandle Data(ItemDataRole role)
            {
                ItemDataRole__Push(role);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_data_overload1);
                return Variant.OwnedHandle__Pop();
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
            public sealed record FromOwned(OwnedHandle Owned) : Deferred
            {
                public OwnedHandle Owned { get; } = Owned;
                internal override void Push(bool isReturn)
                {
                    OwnedHandle__Push(Owned);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromOwned PopDerived()
                {
                    var owned = OwnedHandle__Pop();
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
