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
using static Org.Whatever.MinimalQtForFSharp.AbstractScrollArea;
using static Org.Whatever.MinimalQtForFSharp.AbstractItemModel;
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;
using static Org.Whatever.MinimalQtForFSharp.AbstractItemDelegate;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class AbstractItemView
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_setAlternatingRowColors;
        internal static ModuleMethodHandle _handle_setAutoScroll;
        internal static ModuleMethodHandle _handle_setAutoScrollMargin;
        internal static ModuleMethodHandle _handle_setDefaultDropAction;
        internal static ModuleMethodHandle _handle_setDragDropMode;
        internal static ModuleMethodHandle _handle_setDragDropOverwriteMode;
        internal static ModuleMethodHandle _handle_setDragEnabled;
        internal static ModuleMethodHandle _handle_setEditTriggers;
        internal static ModuleMethodHandle _handle_setHorizontalScrollMode;
        internal static ModuleMethodHandle _handle_setIconSize;
        internal static ModuleMethodHandle _handle_setSelectionBehavior;
        internal static ModuleMethodHandle _handle_setSelectionMode;
        internal static ModuleMethodHandle _handle_setDropIndicatorShown;
        internal static ModuleMethodHandle _handle_setTabKeyNavigation;
        internal static ModuleMethodHandle _handle_setTextElideMode;
        internal static ModuleMethodHandle _handle_setVerticalScrollMode;
        internal static ModuleMethodHandle _handle_setModel;
        internal static ModuleMethodHandle _handle_setItemDelegate;
        internal static ModuleMethodHandle _handle_setItemDelegateForColumn;
        internal static ModuleMethodHandle _handle_setItemDelegateForRow;
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
            // SignalMask:
            Activated = 1 << 5,
            Clicked = 1 << 6,
            DoubleClickedBit = 1 << 7,
            Entered = 1 << 8,
            IconSizeChanged = 1 << 9,
            Pressed = 1 << 10,
            ViewportEntered = 1 << 11
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
        public enum DragDropMode
        {
            NoDragDrop,
            DragOnly,
            DropOnly,
            DragDrop,
            InternalMove
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DragDropMode__Push(DragDropMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static DragDropMode DragDropMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (DragDropMode)ret;
        }
        [Flags]
        public enum EditTriggers
        {
            NoEditTriggers = 0,
            CurrentChanged = 1,
            DoubleClicked = 2,
            SelectedClicked = 4,
            EditKeyPressed = 8,
            AnyKeyPressed = 16,
            AllEditTriggers = 31
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void EditTriggers__Push(EditTriggers value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static EditTriggers EditTriggers__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (EditTriggers)ret;
        }
        public enum ScrollMode
        {
            ScrollPerItem,
            ScrollPerPixel
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ScrollMode__Push(ScrollMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ScrollMode ScrollMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (ScrollMode)ret;
        }
        public enum SelectionBehavior
        {
            SelectItems,
            SelectRows,
            SelectColumns
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SelectionBehavior__Push(SelectionBehavior value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SelectionBehavior SelectionBehavior__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SelectionBehavior)ret;
        }
        public enum SelectionMode
        {
            NoSelection,
            SingleSelection,
            MultiSelection,
            ExtendedSelection,
            ContiguousSelection
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SelectionMode__Push(SelectionMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SelectionMode SelectionMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SelectionMode)ret;
        }
        public class Handle : AbstractScrollArea.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public void SetAlternatingRowColors(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAlternatingRowColors);
            }
            public void SetAutoScroll(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoScroll);
            }
            public void SetAutoScrollMargin(int margin)
            {
                NativeImplClient.PushInt32(margin);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoScrollMargin);
            }
            public void SetDefaultDropAction(DropAction action)
            {
                DropAction__Push(action);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDefaultDropAction);
            }
            public void SetDragDropMode(DragDropMode mode)
            {
                DragDropMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDragDropMode);
            }
            public void SetDragDropOverwriteMode(bool mode)
            {
                NativeImplClient.PushBool(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDragDropOverwriteMode);
            }
            public void SetDragEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDragEnabled);
            }
            public void SetEditTriggers(EditTriggers triggers)
            {
                EditTriggers__Push(triggers);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setEditTriggers);
            }
            public void SetHorizontalScrollMode(ScrollMode mode)
            {
                ScrollMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setHorizontalScrollMode);
            }
            public void SetIconSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconSize);
            }
            public void SetSelectionBehavior(SelectionBehavior behavior)
            {
                SelectionBehavior__Push(behavior);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSelectionBehavior);
            }
            public void SetSelectionMode(SelectionMode mode)
            {
                SelectionMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSelectionMode);
            }
            public void SetDropIndicatorShown(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDropIndicatorShown);
            }
            public void SetTabKeyNavigation(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabKeyNavigation);
            }
            public void SetTextElideMode(TextElideMode mode)
            {
                TextElideMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextElideMode);
            }
            public void SetVerticalScrollMode(ScrollMode mode)
            {
                ScrollMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setVerticalScrollMode);
            }
            public void SetModel(AbstractItemModel.Handle model)
            {
                AbstractItemModel.Handle__Push(model);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setModel);
            }
            public void SetItemDelegate(AbstractItemDelegate.Handle itemDelegate)
            {
                AbstractItemDelegate.Handle__Push(itemDelegate);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setItemDelegate);
            }
            public void SetItemDelegateForColumn(int column, AbstractItemDelegate.Handle itemDelegate)
            {
                AbstractItemDelegate.Handle__Push(itemDelegate);
                NativeImplClient.PushInt32(column);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setItemDelegateForColumn);
            }
            public void SetItemDelegateForRow(int row, AbstractItemDelegate.Handle itemDelegate)
            {
                AbstractItemDelegate.Handle__Push(itemDelegate);
                NativeImplClient.PushInt32(row);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setItemDelegateForRow);
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
            _module = NativeImplClient.GetModule("AbstractItemView");
            // assign module handles
            _handle_setAlternatingRowColors = NativeImplClient.GetModuleMethod(_module, "Handle_setAlternatingRowColors");
            _handle_setAutoScroll = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoScroll");
            _handle_setAutoScrollMargin = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoScrollMargin");
            _handle_setDefaultDropAction = NativeImplClient.GetModuleMethod(_module, "Handle_setDefaultDropAction");
            _handle_setDragDropMode = NativeImplClient.GetModuleMethod(_module, "Handle_setDragDropMode");
            _handle_setDragDropOverwriteMode = NativeImplClient.GetModuleMethod(_module, "Handle_setDragDropOverwriteMode");
            _handle_setDragEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setDragEnabled");
            _handle_setEditTriggers = NativeImplClient.GetModuleMethod(_module, "Handle_setEditTriggers");
            _handle_setHorizontalScrollMode = NativeImplClient.GetModuleMethod(_module, "Handle_setHorizontalScrollMode");
            _handle_setIconSize = NativeImplClient.GetModuleMethod(_module, "Handle_setIconSize");
            _handle_setSelectionBehavior = NativeImplClient.GetModuleMethod(_module, "Handle_setSelectionBehavior");
            _handle_setSelectionMode = NativeImplClient.GetModuleMethod(_module, "Handle_setSelectionMode");
            _handle_setDropIndicatorShown = NativeImplClient.GetModuleMethod(_module, "Handle_setDropIndicatorShown");
            _handle_setTabKeyNavigation = NativeImplClient.GetModuleMethod(_module, "Handle_setTabKeyNavigation");
            _handle_setTextElideMode = NativeImplClient.GetModuleMethod(_module, "Handle_setTextElideMode");
            _handle_setVerticalScrollMode = NativeImplClient.GetModuleMethod(_module, "Handle_setVerticalScrollMode");
            _handle_setModel = NativeImplClient.GetModuleMethod(_module, "Handle_setModel");
            _handle_setItemDelegate = NativeImplClient.GetModuleMethod(_module, "Handle_setItemDelegate");
            _handle_setItemDelegateForColumn = NativeImplClient.GetModuleMethod(_module, "Handle_setItemDelegateForColumn");
            _handle_setItemDelegateForRow = NativeImplClient.GetModuleMethod(_module, "Handle_setItemDelegateForRow");
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

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
