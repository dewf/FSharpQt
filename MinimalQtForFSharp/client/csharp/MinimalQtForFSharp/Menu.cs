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
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.Action;
using static Org.Whatever.MinimalQtForFSharp.Icon;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Menu
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setIcon;
        internal static ModuleMethodHandle _handle_setSeparatorsCollapsible;
        internal static ModuleMethodHandle _handle_setTearOffEnabled;
        internal static ModuleMethodHandle _handle_setTitle;
        internal static ModuleMethodHandle _handle_setToolTipsVisible;
        internal static ModuleMethodHandle _handle_clear;
        internal static ModuleMethodHandle _handle_addSeparator;
        internal static ModuleMethodHandle _handle_popup;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_aboutToHide;
        internal static InterfaceMethodHandle _signalHandler_aboutToShow;
        internal static InterfaceMethodHandle _signalHandler_hovered;
        internal static InterfaceMethodHandle _signalHandler_triggered;

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
            AboutToHide = 1 << 5,
            AboutToShow = 1 << 6,
            Hovered = 1 << 7,
            Triggered = 1 << 8
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
            void AboutToHide();
            void AboutToShow();
            void Hovered(Action.Handle action);
            void Triggered(Action.Handle action);
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

            public void AboutToHide()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_aboutToHide, Id);
            }

            public void AboutToShow()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_aboutToShow, Id);
            }

            public void Hovered(Action.Handle action)
            {
                Action.Handle__Push(action);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_hovered, Id);
            }

            public void Triggered(Action.Handle action)
            {
                Action.Handle__Push(action);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_triggered, Id);
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
            public override void Dispose()
            {
                if (!_disposed)
                {
                    Handle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_handle_dispose);
                    _disposed = true;
                }
            }
            public void SetIcon(Deferred icon)
            {
                Deferred__Push(icon, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIcon);
            }
            public void SetSeparatorsCollapsible(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSeparatorsCollapsible);
            }
            public void SetTearOffEnabled(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTearOffEnabled);
            }
            public void SetTitle(string title)
            {
                NativeImplClient.PushString(title);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTitle);
            }
            public void SetToolTipsVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setToolTipsVisible);
            }
            public void Clear()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_clear);
            }
            public Action.Handle AddSeparator()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addSeparator);
                return Action.Handle__Pop();
            }
            public void Popup(Point p)
            {
                Point__Push(p, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_popup);
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
            _module = NativeImplClient.GetModule("Menu");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setIcon = NativeImplClient.GetModuleMethod(_module, "Handle_setIcon");
            _handle_setSeparatorsCollapsible = NativeImplClient.GetModuleMethod(_module, "Handle_setSeparatorsCollapsible");
            _handle_setTearOffEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setTearOffEnabled");
            _handle_setTitle = NativeImplClient.GetModuleMethod(_module, "Handle_setTitle");
            _handle_setToolTipsVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setToolTipsVisible");
            _handle_clear = NativeImplClient.GetModuleMethod(_module, "Handle_clear");
            _handle_addSeparator = NativeImplClient.GetModuleMethod(_module, "Handle_addSeparator");
            _handle_popup = NativeImplClient.GetModuleMethod(_module, "Handle_popup");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_aboutToHide = NativeImplClient.GetInterfaceMethod(_signalHandler, "aboutToHide");
            _signalHandler_aboutToShow = NativeImplClient.GetInterfaceMethod(_signalHandler, "aboutToShow");
            _signalHandler_hovered = NativeImplClient.GetInterfaceMethod(_signalHandler, "hovered");
            _signalHandler_triggered = NativeImplClient.GetInterfaceMethod(_signalHandler, "triggered");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_aboutToHide, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.AboutToHide();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_aboutToShow, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.AboutToShow();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_hovered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var action = Action.Handle__Pop();
                inst.Hovered(action);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_triggered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var action = Action.Handle__Pop();
                inst.Triggered(action);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
