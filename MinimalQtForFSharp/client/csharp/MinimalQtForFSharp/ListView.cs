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
using static Org.Whatever.MinimalQtForFSharp.AbstractListModel;
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;
using static Org.Whatever.MinimalQtForFSharp.AbstractItemView;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class ListView
    {
        private static ModuleHandle _module;

        internal static void __ModelIndex_Handle_Array__Push(ModelIndex.Handle[] items)
        {
            var ptrs = items.Select(item => item.NativeHandle).ToArray();
            NativeImplClient.PushPtrArray(ptrs);
        }
        internal static ModelIndex.Handle[] __ModelIndex_Handle_Array__Pop()
        {
            return NativeImplClient.PopPtrArray()
                .Select(ptr => ptr != IntPtr.Zero ? new ModelIndex.Handle(ptr) : null)
                .ToArray();
        }
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setBatchSize;
        internal static ModuleMethodHandle _handle_setFlow;
        internal static ModuleMethodHandle _handle_setGridSize;
        internal static ModuleMethodHandle _handle_setWrapping;
        internal static ModuleMethodHandle _handle_setItemAlignment;
        internal static ModuleMethodHandle _handle_setLayoutMode;
        internal static ModuleMethodHandle _handle_setModelColumn;
        internal static ModuleMethodHandle _handle_setMovement;
        internal static ModuleMethodHandle _handle_setResizeMode;
        internal static ModuleMethodHandle _handle_setSelectionRectVisible;
        internal static ModuleMethodHandle _handle_setSpacing;
        internal static ModuleMethodHandle _handle_setUniformItemSizes;
        internal static ModuleMethodHandle _handle_setViewMode;
        internal static ModuleMethodHandle _handle_setWordWrap;
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
        internal static InterfaceMethodHandle _signalHandler_indexesMoved;

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
            IndexesMoved = 1 << 12
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
            void IndexesMoved(ModelIndex.Handle[] indexes);
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

            public void IndexesMoved(ModelIndex.Handle[] indexes)
            {
                __ModelIndex_Handle_Array__Push(indexes);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_indexesMoved, Id);
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
        public enum Movement
        {
            Static,
            Free,
            Snap
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Movement__Push(Movement value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Movement Movement__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Movement)ret;
        }
        public enum Flow
        {
            LeftToRight,
            TopToBottom
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Flow__Push(Flow value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Flow Flow__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Flow)ret;
        }
        public enum ResizeMode
        {
            Fixed,
            Adjust
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ResizeMode__Push(ResizeMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ResizeMode ResizeMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (ResizeMode)ret;
        }
        public enum LayoutMode
        {
            SinglePass,
            Batched
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LayoutMode__Push(LayoutMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static LayoutMode LayoutMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (LayoutMode)ret;
        }
        public enum ViewMode
        {
            ListMode,
            IconMode
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ViewMode__Push(ViewMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ViewMode ViewMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (ViewMode)ret;
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
            public void SetBatchSize(int size)
            {
                NativeImplClient.PushInt32(size);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setBatchSize);
            }
            public void SetFlow(Flow flow)
            {
                Flow__Push(flow);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFlow);
            }
            public void SetGridSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setGridSize);
            }
            public void SetWrapping(bool wrapping)
            {
                NativeImplClient.PushBool(wrapping);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWrapping);
            }
            public void SetItemAlignment(Alignment align)
            {
                Alignment__Push(align);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setItemAlignment);
            }
            public void SetLayoutMode(LayoutMode mode)
            {
                LayoutMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setLayoutMode);
            }
            public void SetModelColumn(int column)
            {
                NativeImplClient.PushInt32(column);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setModelColumn);
            }
            public void SetMovement(Movement value)
            {
                Movement__Push(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMovement);
            }
            public void SetResizeMode(ResizeMode mode)
            {
                ResizeMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setResizeMode);
            }
            public void SetSelectionRectVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSelectionRectVisible);
            }
            public void SetSpacing(int spacing)
            {
                NativeImplClient.PushInt32(spacing);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSpacing);
            }
            public void SetUniformItemSizes(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setUniformItemSizes);
            }
            public void SetViewMode(ViewMode mode)
            {
                ViewMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setViewMode);
            }
            public void SetWordWrap(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWordWrap);
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
            _module = NativeImplClient.GetModule("ListView");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setBatchSize = NativeImplClient.GetModuleMethod(_module, "Handle_setBatchSize");
            _handle_setFlow = NativeImplClient.GetModuleMethod(_module, "Handle_setFlow");
            _handle_setGridSize = NativeImplClient.GetModuleMethod(_module, "Handle_setGridSize");
            _handle_setWrapping = NativeImplClient.GetModuleMethod(_module, "Handle_setWrapping");
            _handle_setItemAlignment = NativeImplClient.GetModuleMethod(_module, "Handle_setItemAlignment");
            _handle_setLayoutMode = NativeImplClient.GetModuleMethod(_module, "Handle_setLayoutMode");
            _handle_setModelColumn = NativeImplClient.GetModuleMethod(_module, "Handle_setModelColumn");
            _handle_setMovement = NativeImplClient.GetModuleMethod(_module, "Handle_setMovement");
            _handle_setResizeMode = NativeImplClient.GetModuleMethod(_module, "Handle_setResizeMode");
            _handle_setSelectionRectVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setSelectionRectVisible");
            _handle_setSpacing = NativeImplClient.GetModuleMethod(_module, "Handle_setSpacing");
            _handle_setUniformItemSizes = NativeImplClient.GetModuleMethod(_module, "Handle_setUniformItemSizes");
            _handle_setViewMode = NativeImplClient.GetModuleMethod(_module, "Handle_setViewMode");
            _handle_setWordWrap = NativeImplClient.GetModuleMethod(_module, "Handle_setWordWrap");
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
            _signalHandler_indexesMoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "indexesMoved");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_indexesMoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var indexes = __ModelIndex_Handle_Array__Pop();
                inst.IndexesMoved(indexes);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
