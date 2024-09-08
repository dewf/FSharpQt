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
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.Widget;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class TabWidget
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_count;
        internal static ModuleMethodHandle _handle_setCurrentIndex;
        internal static ModuleMethodHandle _handle_setDocumentMode;
        internal static ModuleMethodHandle _handle_setElideMode;
        internal static ModuleMethodHandle _handle_setIconSize;
        internal static ModuleMethodHandle _handle_setMovable;
        internal static ModuleMethodHandle _handle_setTabBarAutoHide;
        internal static ModuleMethodHandle _handle_setTabPosition;
        internal static ModuleMethodHandle _handle_setTabShape;
        internal static ModuleMethodHandle _handle_setTabsClosable;
        internal static ModuleMethodHandle _handle_setUsesScrollButtons;
        internal static ModuleMethodHandle _handle_addTab;
        internal static ModuleMethodHandle _handle_insertTab;
        internal static ModuleMethodHandle _handle_widgetAt;
        internal static ModuleMethodHandle _handle_clear;
        internal static ModuleMethodHandle _handle_removeTab;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_currentChanged;
        internal static InterfaceMethodHandle _signalHandler_tabBarClicked;
        internal static InterfaceMethodHandle _signalHandler_tabBarDoubleClicked;
        internal static InterfaceMethodHandle _signalHandler_tabCloseRequested;

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
            CurrentChanged = 1 << 5,
            TabBarClicked = 1 << 6,
            TabBarDoubleClicked = 1 << 7,
            TabCloseRequested = 1 << 8
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
            void CurrentChanged(int index);
            void TabBarClicked(int index);
            void TabBarDoubleClicked(int index);
            void TabCloseRequested(int index);
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

            public void CurrentChanged(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_currentChanged, Id);
            }

            public void TabBarClicked(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_tabBarClicked, Id);
            }

            public void TabBarDoubleClicked(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_tabBarDoubleClicked, Id);
            }

            public void TabCloseRequested(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_tabCloseRequested, Id);
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
        public enum TabShape
        {
            Rounded,
            Triangular
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void TabShape__Push(TabShape value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TabShape TabShape__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (TabShape)ret;
        }
        public enum TabPosition
        {
            North,
            South,
            West,
            East
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void TabPosition__Push(TabPosition value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TabPosition TabPosition__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (TabPosition)ret;
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
            public int Count()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_count);
                return NativeImplClient.PopInt32();
            }
            public void SetCurrentIndex(int index)
            {
                NativeImplClient.PushInt32(index);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCurrentIndex);
            }
            public void SetDocumentMode(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDocumentMode);
            }
            public void SetElideMode(TextElideMode mode)
            {
                TextElideMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setElideMode);
            }
            public void SetIconSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconSize);
            }
            public void SetMovable(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMovable);
            }
            public void SetTabBarAutoHide(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabBarAutoHide);
            }
            public void SetTabPosition(TabPosition position)
            {
                TabPosition__Push(position);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabPosition);
            }
            public void SetTabShape(TabShape shape)
            {
                TabShape__Push(shape);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabShape);
            }
            public void SetTabsClosable(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabsClosable);
            }
            public void SetUsesScrollButtons(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setUsesScrollButtons);
            }
            public void AddTab(Widget.Handle page, string label)
            {
                NativeImplClient.PushString(label);
                Widget.Handle__Push(page);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addTab);
            }
            public void InsertTab(int index, Widget.Handle page, string label)
            {
                NativeImplClient.PushString(label);
                Widget.Handle__Push(page);
                NativeImplClient.PushInt32(index);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_insertTab);
            }
            public Widget.Handle WidgetAt(int index)
            {
                NativeImplClient.PushInt32(index);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_widgetAt);
                return Widget.Handle__Pop();
            }
            public void Clear()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_clear);
            }
            public void RemoveTab(int index)
            {
                NativeImplClient.PushInt32(index);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_removeTab);
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
            _module = NativeImplClient.GetModule("TabWidget");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_count = NativeImplClient.GetModuleMethod(_module, "Handle_count");
            _handle_setCurrentIndex = NativeImplClient.GetModuleMethod(_module, "Handle_setCurrentIndex");
            _handle_setDocumentMode = NativeImplClient.GetModuleMethod(_module, "Handle_setDocumentMode");
            _handle_setElideMode = NativeImplClient.GetModuleMethod(_module, "Handle_setElideMode");
            _handle_setIconSize = NativeImplClient.GetModuleMethod(_module, "Handle_setIconSize");
            _handle_setMovable = NativeImplClient.GetModuleMethod(_module, "Handle_setMovable");
            _handle_setTabBarAutoHide = NativeImplClient.GetModuleMethod(_module, "Handle_setTabBarAutoHide");
            _handle_setTabPosition = NativeImplClient.GetModuleMethod(_module, "Handle_setTabPosition");
            _handle_setTabShape = NativeImplClient.GetModuleMethod(_module, "Handle_setTabShape");
            _handle_setTabsClosable = NativeImplClient.GetModuleMethod(_module, "Handle_setTabsClosable");
            _handle_setUsesScrollButtons = NativeImplClient.GetModuleMethod(_module, "Handle_setUsesScrollButtons");
            _handle_addTab = NativeImplClient.GetModuleMethod(_module, "Handle_addTab");
            _handle_insertTab = NativeImplClient.GetModuleMethod(_module, "Handle_insertTab");
            _handle_widgetAt = NativeImplClient.GetModuleMethod(_module, "Handle_widgetAt");
            _handle_clear = NativeImplClient.GetModuleMethod(_module, "Handle_clear");
            _handle_removeTab = NativeImplClient.GetModuleMethod(_module, "Handle_removeTab");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_currentChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "currentChanged");
            _signalHandler_tabBarClicked = NativeImplClient.GetInterfaceMethod(_signalHandler, "tabBarClicked");
            _signalHandler_tabBarDoubleClicked = NativeImplClient.GetInterfaceMethod(_signalHandler, "tabBarDoubleClicked");
            _signalHandler_tabCloseRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "tabCloseRequested");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_currentChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = NativeImplClient.PopInt32();
                inst.CurrentChanged(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_tabBarClicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = NativeImplClient.PopInt32();
                inst.TabBarClicked(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_tabBarDoubleClicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = NativeImplClient.PopInt32();
                inst.TabBarDoubleClicked(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_tabCloseRequested, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = NativeImplClient.PopInt32();
                inst.TabCloseRequested(index);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
