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
using static Org.Whatever.MinimalQtForFSharp.Layout;
using static Org.Whatever.MinimalQtForFSharp.MenuBar;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.DockWidget;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.ToolBar;
using static Org.Whatever.MinimalQtForFSharp.StatusBar;
using static Org.Whatever.MinimalQtForFSharp.TabWidget;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class MainWindow
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setAnimated;
        internal static ModuleMethodHandle _handle_setDockNestingEnabled;
        internal static ModuleMethodHandle _handle_setDockOptions;
        internal static ModuleMethodHandle _handle_setDocumentMode;
        internal static ModuleMethodHandle _handle_setIconSize;
        internal static ModuleMethodHandle _handle_setTabShape;
        internal static ModuleMethodHandle _handle_setToolButtonStyle;
        internal static ModuleMethodHandle _handle_setUnifiedTitleAndToolBarOnMac;
        internal static ModuleMethodHandle _handle_setCentralWidget;
        internal static ModuleMethodHandle _handle_setMenuBar;
        internal static ModuleMethodHandle _handle_setStatusBar;
        internal static ModuleMethodHandle _handle_addToolBar;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_iconSizeChanged;
        internal static InterfaceMethodHandle _signalHandler_tabifiedDockWidgetActivated;
        internal static InterfaceMethodHandle _signalHandler_toolButtonStyleChanged;
        internal static InterfaceMethodHandle _signalHandler_windowClosed;

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
            IconSizeChanged = 1 << 5,
            TabifiedDockWidgetActivated = 1 << 6,
            ToolButtonStyleChanged = 1 << 7,
            WindowClosed = 1 << 8
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
            void IconSizeChanged(Size iconSize);
            void TabifiedDockWidgetActivated(DockWidget.Handle dockWidget);
            void ToolButtonStyleChanged(ToolButtonStyle style);
            void WindowClosed();
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

            public void IconSizeChanged(Size iconSize)
            {
                Size__Push(iconSize, false);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_iconSizeChanged, Id);
            }

            public void TabifiedDockWidgetActivated(DockWidget.Handle dockWidget)
            {
                DockWidget.Handle__Push(dockWidget);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_tabifiedDockWidgetActivated, Id);
            }

            public void ToolButtonStyleChanged(ToolButtonStyle style)
            {
                ToolButtonStyle__Push(style);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_toolButtonStyleChanged, Id);
            }

            public void WindowClosed()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_windowClosed, Id);
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
        [Flags]
        public enum DockOptions
        {
            AnimatedDocks = 0x01,
            AllowNestedDocks = 0x02,
            AllowTabbedDocks = 0x04,
            ForceTabbedDocks = 0x08,
            VerticalTabs = 0x10,
            GroupedDragging = 0x20
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DockOptions__Push(DockOptions value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static DockOptions DockOptions__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (DockOptions)ret;
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
            public void SetAnimated(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAnimated);
            }
            public void SetDockNestingEnabled(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDockNestingEnabled);
            }
            public void SetDockOptions(DockOptions dockOptions)
            {
                DockOptions__Push(dockOptions);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDockOptions);
            }
            public void SetDocumentMode(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDocumentMode);
            }
            public void SetIconSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconSize);
            }
            public void SetTabShape(TabShape tabShape)
            {
                TabShape__Push(tabShape);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabShape);
            }
            public void SetToolButtonStyle(ToolButtonStyle style)
            {
                ToolButtonStyle__Push(style);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setToolButtonStyle);
            }
            public void SetUnifiedTitleAndToolBarOnMac(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setUnifiedTitleAndToolBarOnMac);
            }
            public void SetCentralWidget(Widget.Handle widget)
            {
                Widget.Handle__Push(widget);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCentralWidget);
            }
            public void SetMenuBar(MenuBar.Handle menubar)
            {
                MenuBar.Handle__Push(menubar);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMenuBar);
            }
            public void SetStatusBar(StatusBar.Handle statusbar)
            {
                StatusBar.Handle__Push(statusbar);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setStatusBar);
            }
            public void AddToolBar(ToolBar.Handle toolbar)
            {
                ToolBar.Handle__Push(toolbar);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addToolBar);
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
            _module = NativeImplClient.GetModule("MainWindow");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setAnimated = NativeImplClient.GetModuleMethod(_module, "Handle_setAnimated");
            _handle_setDockNestingEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setDockNestingEnabled");
            _handle_setDockOptions = NativeImplClient.GetModuleMethod(_module, "Handle_setDockOptions");
            _handle_setDocumentMode = NativeImplClient.GetModuleMethod(_module, "Handle_setDocumentMode");
            _handle_setIconSize = NativeImplClient.GetModuleMethod(_module, "Handle_setIconSize");
            _handle_setTabShape = NativeImplClient.GetModuleMethod(_module, "Handle_setTabShape");
            _handle_setToolButtonStyle = NativeImplClient.GetModuleMethod(_module, "Handle_setToolButtonStyle");
            _handle_setUnifiedTitleAndToolBarOnMac = NativeImplClient.GetModuleMethod(_module, "Handle_setUnifiedTitleAndToolBarOnMac");
            _handle_setCentralWidget = NativeImplClient.GetModuleMethod(_module, "Handle_setCentralWidget");
            _handle_setMenuBar = NativeImplClient.GetModuleMethod(_module, "Handle_setMenuBar");
            _handle_setStatusBar = NativeImplClient.GetModuleMethod(_module, "Handle_setStatusBar");
            _handle_addToolBar = NativeImplClient.GetModuleMethod(_module, "Handle_addToolBar");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_iconSizeChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "iconSizeChanged");
            _signalHandler_tabifiedDockWidgetActivated = NativeImplClient.GetInterfaceMethod(_signalHandler, "tabifiedDockWidgetActivated");
            _signalHandler_toolButtonStyleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "toolButtonStyleChanged");
            _signalHandler_windowClosed = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowClosed");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_iconSizeChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var iconSize = Size__Pop();
                inst.IconSizeChanged(iconSize);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_tabifiedDockWidgetActivated, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var dockWidget = DockWidget.Handle__Pop();
                inst.TabifiedDockWidgetActivated(dockWidget);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_toolButtonStyleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var style = ToolButtonStyle__Pop();
                inst.ToolButtonStyleChanged(style);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_windowClosed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.WindowClosed();
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
