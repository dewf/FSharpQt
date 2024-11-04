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
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Object;
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.Date;
using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class CalendarWidget
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setDateEditAcceptDelay;
        internal static ModuleMethodHandle _handle_setDateEditEnabled;
        internal static ModuleMethodHandle _handle_setFirstDayOfWeek;
        internal static ModuleMethodHandle _handle_setGridVisible;
        internal static ModuleMethodHandle _handle_setHorizontalHeaderFormat;
        internal static ModuleMethodHandle _handle_setMaximumDate;
        internal static ModuleMethodHandle _handle_setMinimumDate;
        internal static ModuleMethodHandle _handle_setNavigationBarVisible;
        internal static ModuleMethodHandle _handle_selectedDate;
        internal static ModuleMethodHandle _handle_setSelectedDate;
        internal static ModuleMethodHandle _handle_setSelectionMode;
        internal static ModuleMethodHandle _handle_setVerticalHeaderFormat;
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
        internal static InterfaceMethodHandle _signalHandler_currentPageChanged;
        internal static InterfaceMethodHandle _signalHandler_selectionChanged;

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
            Activated = 1 << 5,
            Clicked = 1 << 6,
            CurrentPageChanged = 1 << 7,
            SelectionChanged = 1 << 8
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
            void Activated(Date.Handle date);
            void Clicked(Date.Handle date);
            void CurrentPageChanged(int year, int month);
            void SelectionChanged();
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

            public void Activated(Date.Handle date)
            {
                Date.Handle__Push(date);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_activated, Id);
            }

            public void Clicked(Date.Handle date)
            {
                Date.Handle__Push(date);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_clicked, Id);
            }

            public void CurrentPageChanged(int year, int month)
            {
                NativeImplClient.PushInt32(month);
                NativeImplClient.PushInt32(year);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_currentPageChanged, Id);
            }

            public void SelectionChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_selectionChanged, Id);
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
        public enum HorizontalHeaderFormat
        {
            NoHorizontalHeader,
            SingleLetterDayNames,
            ShortDayNames,
            LongDayNames
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void HorizontalHeaderFormat__Push(HorizontalHeaderFormat value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static HorizontalHeaderFormat HorizontalHeaderFormat__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (HorizontalHeaderFormat)ret;
        }
        public enum VerticalHeaderFormat
        {
            NoVerticalHeader,
            ISOWeekNumbers
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void VerticalHeaderFormat__Push(VerticalHeaderFormat value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static VerticalHeaderFormat VerticalHeaderFormat__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (VerticalHeaderFormat)ret;
        }
        public enum SelectionMode
        {
            NoSelection,
            SingleSelection
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
            public void SetDateEditAcceptDelay(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDateEditAcceptDelay);
            }
            public void SetDateEditEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDateEditEnabled);
            }
            public void SetFirstDayOfWeek(QDayOfWeek value)
            {
                QDayOfWeek__Push(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFirstDayOfWeek);
            }
            public void SetGridVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setGridVisible);
            }
            public void SetHorizontalHeaderFormat(HorizontalHeaderFormat format)
            {
                HorizontalHeaderFormat__Push(format);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setHorizontalHeaderFormat);
            }
            public void SetMaximumDate(Date.Deferred value)
            {
                Date.Deferred__Push(value, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximumDate);
            }
            public void SetMinimumDate(Date.Deferred value)
            {
                Date.Deferred__Push(value, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimumDate);
            }
            public void SetNavigationBarVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setNavigationBarVisible);
            }
            public Owned SelectedDate()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_selectedDate);
                return Owned__Pop();
            }
            public void SetSelectedDate(Date.Deferred selected)
            {
                Date.Deferred__Push(selected, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSelectedDate);
            }
            public void SetSelectionMode(SelectionMode mode)
            {
                SelectionMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSelectionMode);
            }
            public void SetVerticalHeaderFormat(VerticalHeaderFormat format)
            {
                VerticalHeaderFormat__Push(format);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setVerticalHeaderFormat);
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
            _module = NativeImplClient.GetModule("CalendarWidget");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setDateEditAcceptDelay = NativeImplClient.GetModuleMethod(_module, "Handle_setDateEditAcceptDelay");
            _handle_setDateEditEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setDateEditEnabled");
            _handle_setFirstDayOfWeek = NativeImplClient.GetModuleMethod(_module, "Handle_setFirstDayOfWeek");
            _handle_setGridVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setGridVisible");
            _handle_setHorizontalHeaderFormat = NativeImplClient.GetModuleMethod(_module, "Handle_setHorizontalHeaderFormat");
            _handle_setMaximumDate = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximumDate");
            _handle_setMinimumDate = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimumDate");
            _handle_setNavigationBarVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setNavigationBarVisible");
            _handle_selectedDate = NativeImplClient.GetModuleMethod(_module, "Handle_selectedDate");
            _handle_setSelectedDate = NativeImplClient.GetModuleMethod(_module, "Handle_setSelectedDate");
            _handle_setSelectionMode = NativeImplClient.GetModuleMethod(_module, "Handle_setSelectionMode");
            _handle_setVerticalHeaderFormat = NativeImplClient.GetModuleMethod(_module, "Handle_setVerticalHeaderFormat");
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
            _signalHandler_currentPageChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "currentPageChanged");
            _signalHandler_selectionChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "selectionChanged");
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
                var date = Date.Handle__Pop();
                inst.Activated(date);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_clicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var date = Date.Handle__Pop();
                inst.Clicked(date);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_currentPageChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var year = NativeImplClient.PopInt32();
                var month = NativeImplClient.PopInt32();
                inst.CurrentPageChanged(year, month);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_selectionChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.SelectionChanged();
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
