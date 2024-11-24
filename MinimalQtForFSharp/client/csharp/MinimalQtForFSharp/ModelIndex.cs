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
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_isValid;
        internal static ModuleMethodHandle _handle_row;
        internal static ModuleMethodHandle _handle_column;
        internal static ModuleMethodHandle _handle_data;
        internal static ModuleMethodHandle _handle_data_overload1;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Create()
        {
            NativeImplClient.InvokeModuleMethod(_create);
            return Owned__Pop();
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

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("ModelIndex");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
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
