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
using static Org.Whatever.MinimalQtForFSharp.Action;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class ToolBar
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setAllowedAreas;
        internal static ModuleMethodHandle _handle_setFloatable;
        internal static ModuleMethodHandle _handle_isFloating;
        internal static ModuleMethodHandle _handle_setIconSize;
        internal static ModuleMethodHandle _handle_setMovable;
        internal static ModuleMethodHandle _handle_setOrientation;
        internal static ModuleMethodHandle _handle_setToolButtonStyle;
        internal static ModuleMethodHandle _handle_addSeparator;
        internal static ModuleMethodHandle _handle_addWidget;
        internal static ModuleMethodHandle _handle_clear;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_actionTriggered;
        internal static InterfaceMethodHandle _signalHandler_allowedAreasChanged;
        internal static InterfaceMethodHandle _signalHandler_iconSizeChanged;
        internal static InterfaceMethodHandle _signalHandler_movableChanged;
        internal static InterfaceMethodHandle _signalHandler_orientationChanged;
        internal static InterfaceMethodHandle _signalHandler_toolButtonStyleChanged;
        internal static InterfaceMethodHandle _signalHandler_topLevelChanged;
        internal static InterfaceMethodHandle _signalHandler_visibilityChanged;

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
            ActionTriggered = 1 << 5,
            AllowedAreasChanged = 1 << 6,
            IconSizeChanged = 1 << 7,
            MovableChanged = 1 << 8,
            OrientationChanged = 1 << 9,
            ToolButtonStyleChanged = 1 << 10,
            TopLevelChanged = 1 << 11,
            VisibilityChanged = 1 << 12
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
            void ActionTriggered(Action.Handle action);
            void AllowedAreasChanged(ToolBarAreas allowed);
            void IconSizeChanged(Size size);
            void MovableChanged(bool movable);
            void OrientationChanged(Orientation value);
            void ToolButtonStyleChanged(ToolButtonStyle style);
            void TopLevelChanged(bool topLevel);
            void VisibilityChanged(bool visible);
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

            public void ActionTriggered(Action.Handle action)
            {
                Action.Handle__Push(action);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_actionTriggered, Id);
            }

            public void AllowedAreasChanged(ToolBarAreas allowed)
            {
                ToolBarAreas__Push(allowed);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_allowedAreasChanged, Id);
            }

            public void IconSizeChanged(Size size)
            {
                Size__Push(size, false);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_iconSizeChanged, Id);
            }

            public void MovableChanged(bool movable)
            {
                NativeImplClient.PushBool(movable);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_movableChanged, Id);
            }

            public void OrientationChanged(Orientation value)
            {
                Orientation__Push(value);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_orientationChanged, Id);
            }

            public void ToolButtonStyleChanged(ToolButtonStyle style)
            {
                ToolButtonStyle__Push(style);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_toolButtonStyleChanged, Id);
            }

            public void TopLevelChanged(bool topLevel)
            {
                NativeImplClient.PushBool(topLevel);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_topLevelChanged, Id);
            }

            public void VisibilityChanged(bool visible)
            {
                NativeImplClient.PushBool(visible);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_visibilityChanged, Id);
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
            public void SetAllowedAreas(ToolBarAreas allowed)
            {
                ToolBarAreas__Push(allowed);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAllowedAreas);
            }
            public void SetFloatable(bool floatable)
            {
                NativeImplClient.PushBool(floatable);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFloatable);
            }
            public bool IsFloating()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isFloating);
                return NativeImplClient.PopBool();
            }
            public void SetIconSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconSize);
            }
            public void SetMovable(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMovable);
            }
            public void SetOrientation(Orientation value)
            {
                Orientation__Push(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOrientation);
            }
            public void SetToolButtonStyle(ToolButtonStyle style)
            {
                ToolButtonStyle__Push(style);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setToolButtonStyle);
            }
            public Action.Handle AddSeparator()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addSeparator);
                return Action.Handle__Pop();
            }
            public void AddWidget(Widget.Handle widget)
            {
                Widget.Handle__Push(widget);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addWidget);
            }
            public void Clear()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_clear);
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
            _module = NativeImplClient.GetModule("ToolBar");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setAllowedAreas = NativeImplClient.GetModuleMethod(_module, "Handle_setAllowedAreas");
            _handle_setFloatable = NativeImplClient.GetModuleMethod(_module, "Handle_setFloatable");
            _handle_isFloating = NativeImplClient.GetModuleMethod(_module, "Handle_isFloating");
            _handle_setIconSize = NativeImplClient.GetModuleMethod(_module, "Handle_setIconSize");
            _handle_setMovable = NativeImplClient.GetModuleMethod(_module, "Handle_setMovable");
            _handle_setOrientation = NativeImplClient.GetModuleMethod(_module, "Handle_setOrientation");
            _handle_setToolButtonStyle = NativeImplClient.GetModuleMethod(_module, "Handle_setToolButtonStyle");
            _handle_addSeparator = NativeImplClient.GetModuleMethod(_module, "Handle_addSeparator");
            _handle_addWidget = NativeImplClient.GetModuleMethod(_module, "Handle_addWidget");
            _handle_clear = NativeImplClient.GetModuleMethod(_module, "Handle_clear");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_actionTriggered = NativeImplClient.GetInterfaceMethod(_signalHandler, "actionTriggered");
            _signalHandler_allowedAreasChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "allowedAreasChanged");
            _signalHandler_iconSizeChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "iconSizeChanged");
            _signalHandler_movableChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "movableChanged");
            _signalHandler_orientationChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "orientationChanged");
            _signalHandler_toolButtonStyleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "toolButtonStyleChanged");
            _signalHandler_topLevelChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "topLevelChanged");
            _signalHandler_visibilityChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "visibilityChanged");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_actionTriggered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var action = Action.Handle__Pop();
                inst.ActionTriggered(action);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_allowedAreasChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var allowed = ToolBarAreas__Pop();
                inst.AllowedAreasChanged(allowed);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_iconSizeChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var size = Size__Pop();
                inst.IconSizeChanged(size);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_movableChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var movable = NativeImplClient.PopBool();
                inst.MovableChanged(movable);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_orientationChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var value = Orientation__Pop();
                inst.OrientationChanged(value);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_toolButtonStyleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var style = ToolButtonStyle__Pop();
                inst.ToolButtonStyleChanged(style);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_topLevelChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var topLevel = NativeImplClient.PopBool();
                inst.TopLevelChanged(topLevel);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_visibilityChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var visible = NativeImplClient.PopBool();
                inst.VisibilityChanged(visible);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
