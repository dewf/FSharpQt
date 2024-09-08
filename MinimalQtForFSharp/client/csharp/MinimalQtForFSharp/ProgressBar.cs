using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Object;
using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Widget;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class ProgressBar
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setAlignment;
        internal static ModuleMethodHandle _handle_setFormat;
        internal static ModuleMethodHandle _handle_setInvertedAppearance;
        internal static ModuleMethodHandle _handle_setMaximum;
        internal static ModuleMethodHandle _handle_setMinimum;
        internal static ModuleMethodHandle _handle_setOrientation;
        internal static ModuleMethodHandle _handle_text;
        internal static ModuleMethodHandle _handle_setTextDirection;
        internal static ModuleMethodHandle _handle_setTextVisible;
        internal static ModuleMethodHandle _handle_setValue;
        internal static ModuleMethodHandle _handle_setRange;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_valueChanged;

        public static Handle Create(SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // Widget.SignalMask:
            CustomContextMenuRequested = 1 << 2,
            WindowIconChanged = 1 << 3,
            WindowTitleChanged = 1 << 4,
            // SignalMask:
            ValueChanged = 1 << 5
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SignalMask__Push(SignalMask value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignalMask SignalMask__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SignalMask)ret;
        }

        public interface SignalHandler : IDisposable
        {
            void IDisposable.Dispose()
            {
                // nothing by default
            }
            void Destroyed(Object.Handle obj);
            void ObjectNameChanged(string objectName);
            void CustomContextMenuRequested(Point pos);
            void WindowIconChanged(Icon.Handle icon);
            void WindowTitleChanged(string title);
            void ValueChanged(int value);
        }

        private static Dictionary<SignalHandler, IPushable> __SignalHandlerToPushable = new();
        internal class __SignalHandlerWrapper : ClientInterfaceWrapper<SignalHandler>
        {
            public __SignalHandlerWrapper(SignalHandler rawInterface) : base(rawInterface)
            {
            }
            protected override void ReleaseExtra()
            {
                // remove the raw interface from the lookup table, no longer needed
                __SignalHandlerToPushable.Remove(RawInterface);
            }
        }

        internal static void SignalHandler__Push(SignalHandler thing, bool isReturn)
        {
            if (thing != null)
            {
                if (__SignalHandlerToPushable.TryGetValue(thing, out var pushable))
                {
                    // either an already-known client thing, or a server thing
                    pushable.Push(isReturn);
                }
                else
                {
                    // as-yet-unknown client thing - wrap and add to lookup table
                    pushable = new __SignalHandlerWrapper(thing);
                    __SignalHandlerToPushable.Add(thing, pushable);
                }
                pushable.Push(isReturn);
            }
            else
            {
                NativeImplClient.PushNull();
            }
        }

        internal static SignalHandler SignalHandler__Pop()
        {
            NativeImplClient.PopInstanceId(out var id, out var isClientId);
            if (id != 0)
            {
                if (isClientId)
                {
                    // we must have sent it over originally, so wrapper must exist
                    var wrapper = (__SignalHandlerWrapper)ClientObject.GetById(id);
                    return wrapper.RawInterface;
                }
                else // server ID
                {
                    var thing = new ServerSignalHandler(id);
                    // add to lookup table before returning
                    __SignalHandlerToPushable.Add(thing, thing);
                    return thing;
                }
            }
            else
            {
                return null;
            }
        }

        private class ServerSignalHandler : ServerObject, SignalHandler
        {
            public ServerSignalHandler(int id) : base(id)
            {
            }

            public void Destroyed(Object.Handle obj)
            {
                Object.Handle__Push(obj);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_destroyed, Id);
            }

            public void ObjectNameChanged(string objectName)
            {
                NativeImplClient.PushString(objectName);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_objectNameChanged, Id);
            }

            public void CustomContextMenuRequested(Point pos)
            {
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_customContextMenuRequested, Id);
            }

            public void WindowIconChanged(Icon.Handle icon)
            {
                Icon.Handle__Push(icon);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_windowIconChanged, Id);
            }

            public void WindowTitleChanged(string title)
            {
                NativeImplClient.PushString(title);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_windowTitleChanged, Id);
            }

            public void ValueChanged(int value)
            {
                NativeImplClient.PushInt32(value);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_valueChanged, Id);
            }

            protected override void ReleaseExtra()
            {
                // remove from lookup table
                __SignalHandlerToPushable.Remove(this);
            }

            public void Dispose()
            {
                // will invoke ReleaseExtra() for us
                ServerDispose();
            }
        }
        public enum Direction
        {
            TopToBottom,
            BottomToTop
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Direction__Push(Direction value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Direction Direction__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Direction)ret;
        }
        public class Handle : Widget.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public override void Dispose()
            {
                if (!_disposed)
                {
                    Handle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_handle_dispose);
                    _disposed = true;
                }
            }
            public void SetAlignment(Alignment align)
            {
                Alignment__Push(align);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAlignment);
            }
            public void SetFormat(string format)
            {
                NativeImplClient.PushString(format);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFormat);
            }
            public void SetInvertedAppearance(bool invert)
            {
                NativeImplClient.PushBool(invert);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInvertedAppearance);
            }
            public void SetMaximum(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximum);
            }
            public void SetMinimum(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimum);
            }
            public void SetOrientation(Orientation orient)
            {
                Orientation__Push(orient);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOrientation);
            }
            public string Text()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_text);
                return NativeImplClient.PopString();
            }
            public void SetTextDirection(Direction direction)
            {
                Direction__Push(direction);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextDirection);
            }
            public void SetTextVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextVisible);
            }
            public void SetValue(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setValue);
            }
            public void SetRange(int min, int max)
            {
                NativeImplClient.PushInt32(max);
                NativeImplClient.PushInt32(min);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRange);
            }
            public void SetSignalMask(SignalMask mask)
            {
                SignalMask__Push(mask);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSignalMask);
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
            _module = NativeImplClient.GetModule("ProgressBar");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setAlignment = NativeImplClient.GetModuleMethod(_module, "Handle_setAlignment");
            _handle_setFormat = NativeImplClient.GetModuleMethod(_module, "Handle_setFormat");
            _handle_setInvertedAppearance = NativeImplClient.GetModuleMethod(_module, "Handle_setInvertedAppearance");
            _handle_setMaximum = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximum");
            _handle_setMinimum = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimum");
            _handle_setOrientation = NativeImplClient.GetModuleMethod(_module, "Handle_setOrientation");
            _handle_text = NativeImplClient.GetModuleMethod(_module, "Handle_text");
            _handle_setTextDirection = NativeImplClient.GetModuleMethod(_module, "Handle_setTextDirection");
            _handle_setTextVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setTextVisible");
            _handle_setValue = NativeImplClient.GetModuleMethod(_module, "Handle_setValue");
            _handle_setRange = NativeImplClient.GetModuleMethod(_module, "Handle_setRange");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_valueChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "valueChanged");
            NativeImplClient.SetClientMethodWrapper(_signalHandler_destroyed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var obj = Object.Handle__Pop();
                inst.Destroyed(obj);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_objectNameChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var objectName = NativeImplClient.PopString();
                inst.ObjectNameChanged(objectName);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_customContextMenuRequested, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                inst.CustomContextMenuRequested(pos);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_windowIconChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var icon = Icon.Handle__Pop();
                inst.WindowIconChanged(icon);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_windowTitleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var title = NativeImplClient.PopString();
                inst.WindowTitleChanged(title);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_valueChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var value = NativeImplClient.PopInt32();
                inst.ValueChanged(value);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
