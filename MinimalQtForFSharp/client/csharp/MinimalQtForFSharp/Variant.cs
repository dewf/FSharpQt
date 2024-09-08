using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.PaintResources;
using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Variant
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_isValid;
        internal static ModuleMethodHandle _handle_toBool;
        internal static ModuleMethodHandle _handle_toString2;
        internal static ModuleMethodHandle _handle_toInt;
        internal static ModuleMethodHandle _handle_toSize;
        internal static ModuleMethodHandle _handle_toCheckState;
        internal static ModuleMethodHandle _handle_toServerValue;
        internal static ModuleMethodHandle _handle_dispose;
        internal static ModuleMethodHandle _ownedHandle_dispose;
        public abstract record ServerValue
        {
            internal abstract void Push(bool isReturn);
            internal static ServerValue Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => Bool.PopDerived(),
                    1 => String.PopDerived(),
                    2 => Int.PopDerived(),
                    3 => Unknown.PopDerived(),
                    _ => throw new Exception("ServerValue.Pop() - unknown tag!")
                };
            }
            public sealed record Bool(bool Value) : ServerValue
            {
                public bool Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushBool(Value);
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static Bool PopDerived()
                {
                    var value = NativeImplClient.PopBool();
                    return new Bool(value);
                }
            }
            public sealed record String(string Value) : ServerValue
            {
                public string Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushString(Value);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static String PopDerived()
                {
                    var value = NativeImplClient.PopString();
                    return new String(value);
                }
            }
            public sealed record Int(int Value) : ServerValue
            {
                public int Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushInt32(Value);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static Int PopDerived()
                {
                    var value = NativeImplClient.PopInt32();
                    return new Int(value);
                }
            }
            public sealed record Unknown : ServerValue
            {
                internal override void Push(bool isReturn)
                {
                    // kind
                    NativeImplClient.PushInt32(3);
                }
                internal static Unknown PopDerived()
                {
                    return new Unknown();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ServerValue__Push(ServerValue thing, bool isReturn)
        {
            thing.Push(isReturn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ServerValue ServerValue__Pop()
        {
            return ServerValue.Pop();
        }
        public class Handle : IDisposable, IComparable
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
            public bool ToBool()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toBool);
                return NativeImplClient.PopBool();
            }
            public string ToString2()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toString2);
                return NativeImplClient.PopString();
            }
            public int ToInt()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toInt);
                return NativeImplClient.PopInt32();
            }
            public Size ToSize()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toSize);
                return Size__Pop();
            }
            public CheckState ToCheckState()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toCheckState);
                return CheckState__Pop();
            }
            public ServerValue ToServerValue()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toServerValue);
                return ServerValue__Pop();
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
                    1 => FromBool.PopDerived(),
                    2 => FromString.PopDerived(),
                    3 => FromInt.PopDerived(),
                    4 => FromCheckState.PopDerived(),
                    5 => FromSize.PopDerived(),
                    6 => FromIcon.PopDerived(),
                    7 => FromColor.PopDerived(),
                    8 => FromAligment.PopDerived(),
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
            public sealed record FromBool(bool Value) : Deferred
            {
                public bool Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushBool(Value);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromBool PopDerived()
                {
                    var value = NativeImplClient.PopBool();
                    return new FromBool(value);
                }
            }
            public sealed record FromString(string Value) : Deferred
            {
                public string Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushString(Value);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromString PopDerived()
                {
                    var value = NativeImplClient.PopString();
                    return new FromString(value);
                }
            }
            public sealed record FromInt(int Value) : Deferred
            {
                public int Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushInt32(Value);
                    // kind
                    NativeImplClient.PushInt32(3);
                }
                internal static FromInt PopDerived()
                {
                    var value = NativeImplClient.PopInt32();
                    return new FromInt(value);
                }
            }
            public sealed record FromCheckState(CheckState Value) : Deferred
            {
                public CheckState Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    CheckState__Push(Value);
                    // kind
                    NativeImplClient.PushInt32(4);
                }
                internal static FromCheckState PopDerived()
                {
                    var value = CheckState__Pop();
                    return new FromCheckState(value);
                }
            }
            public sealed record FromSize(Size Size) : Deferred
            {
                public Size Size { get; } = Size;
                internal override void Push(bool isReturn)
                {
                    Size__Push(Size, isReturn);
                    // kind
                    NativeImplClient.PushInt32(5);
                }
                internal static FromSize PopDerived()
                {
                    var size = Size__Pop();
                    return new FromSize(size);
                }
            }
            public sealed record FromIcon(Icon.Deferred Value) : Deferred
            {
                public Icon.Deferred Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    Icon.Deferred__Push(Value, isReturn);
                    // kind
                    NativeImplClient.PushInt32(6);
                }
                internal static FromIcon PopDerived()
                {
                    var value = Icon.Deferred__Pop();
                    return new FromIcon(value);
                }
            }
            public sealed record FromColor(Color.Deferred Value) : Deferred
            {
                public Color.Deferred Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    Color.Deferred__Push(Value, isReturn);
                    // kind
                    NativeImplClient.PushInt32(7);
                }
                internal static FromColor PopDerived()
                {
                    var value = Color.Deferred__Pop();
                    return new FromColor(value);
                }
            }
            public sealed record FromAligment(Alignment Value) : Deferred
            {
                public Alignment Value { get; } = Value;
                internal override void Push(bool isReturn)
                {
                    Alignment__Push(Value);
                    // kind
                    NativeImplClient.PushInt32(8);
                }
                internal static FromAligment PopDerived()
                {
                    var value = Alignment__Pop();
                    return new FromAligment(value);
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
            _module = NativeImplClient.GetModule("Variant");
            // assign module handles
            _handle_isValid = NativeImplClient.GetModuleMethod(_module, "Handle_isValid");
            _handle_toBool = NativeImplClient.GetModuleMethod(_module, "Handle_toBool");
            _handle_toString2 = NativeImplClient.GetModuleMethod(_module, "Handle_toString2");
            _handle_toInt = NativeImplClient.GetModuleMethod(_module, "Handle_toInt");
            _handle_toSize = NativeImplClient.GetModuleMethod(_module, "Handle_toSize");
            _handle_toCheckState = NativeImplClient.GetModuleMethod(_module, "Handle_toCheckState");
            _handle_toServerValue = NativeImplClient.GetModuleMethod(_module, "Handle_toServerValue");
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
