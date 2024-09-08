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
using static Org.Whatever.MinimalQtForFSharp.KeySequence;
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.Icon;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class AbstractButton
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_setAutoExclusive;
        internal static ModuleMethodHandle _handle_setAutoRepeat;
        internal static ModuleMethodHandle _handle_setAutoRepeatDelay;
        internal static ModuleMethodHandle _handle_setAutoRepeatInterval;
        internal static ModuleMethodHandle _handle_setCheckable;
        internal static ModuleMethodHandle _handle_setChecked;
        internal static ModuleMethodHandle _handle_setDown;
        internal static ModuleMethodHandle _handle_setIcon;
        internal static ModuleMethodHandle _handle_setIconSize;
        internal static ModuleMethodHandle _handle_setShortcut;
        internal static ModuleMethodHandle _handle_setText;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_clicked;
        internal static InterfaceMethodHandle _signalHandler_pressed;
        internal static InterfaceMethodHandle _signalHandler_released;
        internal static InterfaceMethodHandle _signalHandler_toggled;
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
            Clicked = 1 << 5,
            Pressed = 1 << 6,
            Released = 1 << 7,
            Toggled = 1 << 8
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
            void Clicked(bool checkState);
            void Pressed();
            void Released();
            void Toggled(bool checkState);
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

            public void Clicked(bool checkState)
            {
                NativeImplClient.PushBool(checkState);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_clicked, Id);
            }

            public void Pressed()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_pressed, Id);
            }

            public void Released()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_released, Id);
            }

            public void Toggled(bool checkState)
            {
                NativeImplClient.PushBool(checkState);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_toggled, Id);
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
        public class Handle : Widget.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public void SetAutoExclusive(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoExclusive);
            }
            public void SetAutoRepeat(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoRepeat);
            }
            public void SetAutoRepeatDelay(int delay)
            {
                NativeImplClient.PushInt32(delay);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoRepeatDelay);
            }
            public void SetAutoRepeatInterval(int interval)
            {
                NativeImplClient.PushInt32(interval);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoRepeatInterval);
            }
            public void SetCheckable(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCheckable);
            }
            public void SetChecked(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setChecked);
            }
            public void SetDown(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDown);
            }
            public void SetIcon(Icon.Deferred icon)
            {
                Icon.Deferred__Push(icon, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIcon);
            }
            public void SetIconSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconSize);
            }
            public void SetShortcut(KeySequence.Deferred seq)
            {
                KeySequence.Deferred__Push(seq, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setShortcut);
            }
            public void SetText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setText);
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
            _module = NativeImplClient.GetModule("AbstractButton");
            // assign module handles
            _handle_setAutoExclusive = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoExclusive");
            _handle_setAutoRepeat = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoRepeat");
            _handle_setAutoRepeatDelay = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoRepeatDelay");
            _handle_setAutoRepeatInterval = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoRepeatInterval");
            _handle_setCheckable = NativeImplClient.GetModuleMethod(_module, "Handle_setCheckable");
            _handle_setChecked = NativeImplClient.GetModuleMethod(_module, "Handle_setChecked");
            _handle_setDown = NativeImplClient.GetModuleMethod(_module, "Handle_setDown");
            _handle_setIcon = NativeImplClient.GetModuleMethod(_module, "Handle_setIcon");
            _handle_setIconSize = NativeImplClient.GetModuleMethod(_module, "Handle_setIconSize");
            _handle_setShortcut = NativeImplClient.GetModuleMethod(_module, "Handle_setShortcut");
            _handle_setText = NativeImplClient.GetModuleMethod(_module, "Handle_setText");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_clicked = NativeImplClient.GetInterfaceMethod(_signalHandler, "clicked");
            _signalHandler_pressed = NativeImplClient.GetInterfaceMethod(_signalHandler, "pressed");
            _signalHandler_released = NativeImplClient.GetInterfaceMethod(_signalHandler, "released");
            _signalHandler_toggled = NativeImplClient.GetInterfaceMethod(_signalHandler, "toggled");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_clicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var checkState = NativeImplClient.PopBool();
                inst.Clicked(checkState);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_pressed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Pressed();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_released, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Released();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_toggled, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var checkState = NativeImplClient.PopBool();
                inst.Toggled(checkState);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
