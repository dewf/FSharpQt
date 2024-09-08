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
using static Org.Whatever.MinimalQtForFSharp.AbstractSlider;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Slider
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setTickInterval;
        internal static ModuleMethodHandle _handle_setTickPosition;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_actionTriggered;
        internal static InterfaceMethodHandle _signalHandler_rangeChanged;
        internal static InterfaceMethodHandle _signalHandler_sliderMoved;
        internal static InterfaceMethodHandle _signalHandler_sliderPressed;
        internal static InterfaceMethodHandle _signalHandler_sliderReleased;
        internal static InterfaceMethodHandle _signalHandler_valueChanged;

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
            // AbstractSlider.SignalMask:
            ActionTriggered = 1 << 5,
            RangeChanged = 1 << 6,
            SliderMoved = 1 << 7,
            SliderPressed = 1 << 8,
            SliderReleased = 1 << 9,
            ValueChanged = 1 << 10,
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
            void CustomContextMenuRequested(Point pos);
            void WindowIconChanged(Icon.Handle icon);
            void WindowTitleChanged(string title);
            void ActionTriggered(SliderAction action);
            void RangeChanged(int min, int max);
            void SliderMoved(int value);
            void SliderPressed();
            void SliderReleased();
            void ValueChanged(int value);
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

            public void ActionTriggered(SliderAction action)
            {
                SliderAction__Push(action);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_actionTriggered, Id);
            }

            public void RangeChanged(int min, int max)
            {
                NativeImplClient.PushInt32(max);
                NativeImplClient.PushInt32(min);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rangeChanged, Id);
            }

            public void SliderMoved(int value)
            {
                NativeImplClient.PushInt32(value);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sliderMoved, Id);
            }

            public void SliderPressed()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sliderPressed, Id);
            }

            public void SliderReleased()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sliderReleased, Id);
            }

            public void ValueChanged(int value)
            {
                NativeImplClient.PushInt32(value);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_valueChanged, Id);
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
        public enum TickPosition
        {
            NoTicks = 0,
            TicksAbove = 1,
            TicksLeft = TicksAbove,
            TicksBelow = 2,
            TicksRight = TicksBelow,
            TicksBothSides = 3
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void TickPosition__Push(TickPosition value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TickPosition TickPosition__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (TickPosition)ret;
        }
        public class Handle : AbstractSlider.Handle
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
            public void SetTickInterval(int interval)
            {
                NativeImplClient.PushInt32(interval);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTickInterval);
            }
            public void SetTickPosition(TickPosition tpos)
            {
                TickPosition__Push(tpos);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTickPosition);
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
            _module = NativeImplClient.GetModule("Slider");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setTickInterval = NativeImplClient.GetModuleMethod(_module, "Handle_setTickInterval");
            _handle_setTickPosition = NativeImplClient.GetModuleMethod(_module, "Handle_setTickPosition");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_actionTriggered = NativeImplClient.GetInterfaceMethod(_signalHandler, "actionTriggered");
            _signalHandler_rangeChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "rangeChanged");
            _signalHandler_sliderMoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "sliderMoved");
            _signalHandler_sliderPressed = NativeImplClient.GetInterfaceMethod(_signalHandler, "sliderPressed");
            _signalHandler_sliderReleased = NativeImplClient.GetInterfaceMethod(_signalHandler, "sliderReleased");
            _signalHandler_valueChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "valueChanged");
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
                var action = SliderAction__Pop();
                inst.ActionTriggered(action);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rangeChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var min = NativeImplClient.PopInt32();
                var max = NativeImplClient.PopInt32();
                inst.RangeChanged(min, max);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sliderMoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var value = NativeImplClient.PopInt32();
                inst.SliderMoved(value);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sliderPressed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.SliderPressed();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sliderReleased, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.SliderReleased();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_valueChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var value = NativeImplClient.PopInt32();
                inst.ValueChanged(value);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
