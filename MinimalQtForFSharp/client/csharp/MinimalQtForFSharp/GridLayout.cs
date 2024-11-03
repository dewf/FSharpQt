using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.Layout;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.Object;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class GridLayout
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setHorizontalSpacing;
        internal static ModuleMethodHandle _handle_setVerticalSpacing;
        internal static ModuleMethodHandle _handle_addWidget;
        internal static ModuleMethodHandle _handle_addWidget_overload1;
        internal static ModuleMethodHandle _handle_addWidget_overload2;
        internal static ModuleMethodHandle _handle_addWidget_overload3;
        internal static ModuleMethodHandle _handle_addLayout;
        internal static ModuleMethodHandle _handle_addLayout_overload1;
        internal static ModuleMethodHandle _handle_addLayout_overload2;
        internal static ModuleMethodHandle _handle_addLayout_overload3;
        internal static ModuleMethodHandle _handle_setRowMinimumHeight;
        internal static ModuleMethodHandle _handle_setRowStretch;
        internal static ModuleMethodHandle _handle_setColumnMinimumWidth;
        internal static ModuleMethodHandle _handle_setColumnStretch;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;

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
            // Layout.SignalMask:
            // SignalMask:
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
        public class Handle : Layout.Handle, IDisposable
        {
            protected bool _disposed;
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
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
            public void SetHorizontalSpacing(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setHorizontalSpacing);
            }
            public void SetVerticalSpacing(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setVerticalSpacing);
            }
            public void AddWidget(Widget.Handle widget, int row, int col)
            {
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Widget.Handle__Push(widget);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addWidget);
            }
            public void AddWidget(Widget.Handle widget, int row, int col, Alignment align)
            {
                Alignment__Push(align);
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Widget.Handle__Push(widget);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addWidget_overload1);
            }
            public void AddWidget(Widget.Handle widget, int row, int col, int rowSpan, int colSpan)
            {
                NativeImplClient.PushInt32(colSpan);
                NativeImplClient.PushInt32(rowSpan);
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Widget.Handle__Push(widget);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addWidget_overload2);
            }
            public void AddWidget(Widget.Handle widget, int row, int col, int rowSpan, int colSpan, Alignment align)
            {
                Alignment__Push(align);
                NativeImplClient.PushInt32(colSpan);
                NativeImplClient.PushInt32(rowSpan);
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Widget.Handle__Push(widget);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addWidget_overload3);
            }
            public void AddLayout(Layout.Handle layout, int row, int col)
            {
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Layout.Handle__Push(layout);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addLayout);
            }
            public void AddLayout(Layout.Handle layout, int row, int col, Alignment align)
            {
                Alignment__Push(align);
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Layout.Handle__Push(layout);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addLayout_overload1);
            }
            public void AddLayout(Layout.Handle layout, int row, int col, int rowSpan, int colSpan)
            {
                NativeImplClient.PushInt32(colSpan);
                NativeImplClient.PushInt32(rowSpan);
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Layout.Handle__Push(layout);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addLayout_overload2);
            }
            public void AddLayout(Layout.Handle layout, int row, int col, int rowSpan, int colSpan, Alignment align)
            {
                Alignment__Push(align);
                NativeImplClient.PushInt32(colSpan);
                NativeImplClient.PushInt32(rowSpan);
                NativeImplClient.PushInt32(col);
                NativeImplClient.PushInt32(row);
                Layout.Handle__Push(layout);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addLayout_overload3);
            }
            public void SetRowMinimumHeight(int row, int minHeight)
            {
                NativeImplClient.PushInt32(minHeight);
                NativeImplClient.PushInt32(row);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRowMinimumHeight);
            }
            public void SetRowStretch(int row, int stretch)
            {
                NativeImplClient.PushInt32(stretch);
                NativeImplClient.PushInt32(row);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRowStretch);
            }
            public void SetColumnMinimumWidth(int column, int minWidth)
            {
                NativeImplClient.PushInt32(minWidth);
                NativeImplClient.PushInt32(column);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setColumnMinimumWidth);
            }
            public void SetColumnStretch(int column, int stretch)
            {
                NativeImplClient.PushInt32(stretch);
                NativeImplClient.PushInt32(column);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setColumnStretch);
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
            _module = NativeImplClient.GetModule("GridLayout");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setHorizontalSpacing = NativeImplClient.GetModuleMethod(_module, "Handle_setHorizontalSpacing");
            _handle_setVerticalSpacing = NativeImplClient.GetModuleMethod(_module, "Handle_setVerticalSpacing");
            _handle_addWidget = NativeImplClient.GetModuleMethod(_module, "Handle_addWidget");
            _handle_addWidget_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_addWidget_overload1");
            _handle_addWidget_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_addWidget_overload2");
            _handle_addWidget_overload3 = NativeImplClient.GetModuleMethod(_module, "Handle_addWidget_overload3");
            _handle_addLayout = NativeImplClient.GetModuleMethod(_module, "Handle_addLayout");
            _handle_addLayout_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_addLayout_overload1");
            _handle_addLayout_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_addLayout_overload2");
            _handle_addLayout_overload3 = NativeImplClient.GetModuleMethod(_module, "Handle_addLayout_overload3");
            _handle_setRowMinimumHeight = NativeImplClient.GetModuleMethod(_module, "Handle_setRowMinimumHeight");
            _handle_setRowStretch = NativeImplClient.GetModuleMethod(_module, "Handle_setRowStretch");
            _handle_setColumnMinimumWidth = NativeImplClient.GetModuleMethod(_module, "Handle_setColumnMinimumWidth");
            _handle_setColumnStretch = NativeImplClient.GetModuleMethod(_module, "Handle_setColumnStretch");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
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

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
