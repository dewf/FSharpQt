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
using static Org.Whatever.MinimalQtForFSharp.AbstractItemView;
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class TreeView
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setAllColumnsShowFocus;
        internal static ModuleMethodHandle _handle_setAnimated;
        internal static ModuleMethodHandle _handle_setAutoExpandDelay;
        internal static ModuleMethodHandle _handle_setExpandsOnDoubleClick;
        internal static ModuleMethodHandle _handle_setHeaderHidden;
        internal static ModuleMethodHandle _handle_setIndentation;
        internal static ModuleMethodHandle _handle_setItemsExpandable;
        internal static ModuleMethodHandle _handle_setRootIsDecorated;
        internal static ModuleMethodHandle _handle_setSortingEnabled;
        internal static ModuleMethodHandle _handle_setUniformRowHeights;
        internal static ModuleMethodHandle _handle_setWordWrap;
        internal static ModuleMethodHandle _handle_resizeColumnToContents;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_activated;
        internal static InterfaceMethodHandle _signalHandler_clicked;
        internal static InterfaceMethodHandle _signalHandler_doubleClicked;
        internal static InterfaceMethodHandle _signalHandler_entered;
        internal static InterfaceMethodHandle _signalHandler_iconSizeChanged;
        internal static InterfaceMethodHandle _signalHandler_pressed;
        internal static InterfaceMethodHandle _signalHandler_viewportEntered;
        internal static InterfaceMethodHandle _signalHandler_collapsed;
        internal static InterfaceMethodHandle _signalHandler_expanded;

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
            // Frame.SignalMask:
            // AbstractScrollArea.SignalMask:
            // AbstractItemView.SignalMask:
            Activated = 1 << 5,
            Clicked = 1 << 6,
            DoubleClickedBit = 1 << 7,
            Entered = 1 << 8,
            IconSizeChanged = 1 << 9,
            Pressed = 1 << 10,
            ViewportEntered = 1 << 11,
            // SignalMask:
            Collapsed = 1 << 12,
            Expanded = 1 << 13
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
            void Activated(ModelIndex.Handle index);
            void Clicked(ModelIndex.Handle index);
            void DoubleClicked(ModelIndex.Handle index);
            void Entered(ModelIndex.Handle index);
            void IconSizeChanged(Size size);
            void Pressed(ModelIndex.Handle index);
            void ViewportEntered();
            void Collapsed(ModelIndex.Handle index);
            void Expanded(ModelIndex.Handle index);
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

            public void Activated(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_activated, Id);
            }

            public void Clicked(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_clicked, Id);
            }

            public void DoubleClicked(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_doubleClicked, Id);
            }

            public void Entered(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_entered, Id);
            }

            public void IconSizeChanged(Size size)
            {
                Size__Push(size, false);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_iconSizeChanged, Id);
            }

            public void Pressed(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_pressed, Id);
            }

            public void ViewportEntered()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_viewportEntered, Id);
            }

            public void Collapsed(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_collapsed, Id);
            }

            public void Expanded(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_expanded, Id);
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
        public class Handle : AbstractItemView.Handle
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
            public void SetAllColumnsShowFocus(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAllColumnsShowFocus);
            }
            public void SetAnimated(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAnimated);
            }
            public void SetAutoExpandDelay(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoExpandDelay);
            }
            public void SetExpandsOnDoubleClick(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setExpandsOnDoubleClick);
            }
            public void SetHeaderHidden(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setHeaderHidden);
            }
            public void SetIndentation(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIndentation);
            }
            public void SetItemsExpandable(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setItemsExpandable);
            }
            public void SetRootIsDecorated(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRootIsDecorated);
            }
            public void SetSortingEnabled(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSortingEnabled);
            }
            public void SetUniformRowHeights(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setUniformRowHeights);
            }
            public void SetWordWrap(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWordWrap);
            }
            public void ResizeColumnToContents(int column)
            {
                NativeImplClient.PushInt32(column);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_resizeColumnToContents);
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
            _module = NativeImplClient.GetModule("TreeView");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setAllColumnsShowFocus = NativeImplClient.GetModuleMethod(_module, "Handle_setAllColumnsShowFocus");
            _handle_setAnimated = NativeImplClient.GetModuleMethod(_module, "Handle_setAnimated");
            _handle_setAutoExpandDelay = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoExpandDelay");
            _handle_setExpandsOnDoubleClick = NativeImplClient.GetModuleMethod(_module, "Handle_setExpandsOnDoubleClick");
            _handle_setHeaderHidden = NativeImplClient.GetModuleMethod(_module, "Handle_setHeaderHidden");
            _handle_setIndentation = NativeImplClient.GetModuleMethod(_module, "Handle_setIndentation");
            _handle_setItemsExpandable = NativeImplClient.GetModuleMethod(_module, "Handle_setItemsExpandable");
            _handle_setRootIsDecorated = NativeImplClient.GetModuleMethod(_module, "Handle_setRootIsDecorated");
            _handle_setSortingEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setSortingEnabled");
            _handle_setUniformRowHeights = NativeImplClient.GetModuleMethod(_module, "Handle_setUniformRowHeights");
            _handle_setWordWrap = NativeImplClient.GetModuleMethod(_module, "Handle_setWordWrap");
            _handle_resizeColumnToContents = NativeImplClient.GetModuleMethod(_module, "Handle_resizeColumnToContents");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_activated = NativeImplClient.GetInterfaceMethod(_signalHandler, "activated");
            _signalHandler_clicked = NativeImplClient.GetInterfaceMethod(_signalHandler, "clicked");
            _signalHandler_doubleClicked = NativeImplClient.GetInterfaceMethod(_signalHandler, "doubleClicked");
            _signalHandler_entered = NativeImplClient.GetInterfaceMethod(_signalHandler, "entered");
            _signalHandler_iconSizeChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "iconSizeChanged");
            _signalHandler_pressed = NativeImplClient.GetInterfaceMethod(_signalHandler, "pressed");
            _signalHandler_viewportEntered = NativeImplClient.GetInterfaceMethod(_signalHandler, "viewportEntered");
            _signalHandler_collapsed = NativeImplClient.GetInterfaceMethod(_signalHandler, "collapsed");
            _signalHandler_expanded = NativeImplClient.GetInterfaceMethod(_signalHandler, "expanded");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_activated, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.Activated(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_clicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.Clicked(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_doubleClicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.DoubleClicked(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_entered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.Entered(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_iconSizeChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var size = Size__Pop();
                inst.IconSizeChanged(size);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_pressed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.Pressed(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_viewportEntered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.ViewportEntered();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_collapsed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.Collapsed(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_expanded, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.Expanded(index);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
