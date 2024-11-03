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
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class AbstractSlider
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_setInvertedAppearance;
        internal static ModuleMethodHandle _handle_setInvertedControls;
        internal static ModuleMethodHandle _handle_setMaximum;
        internal static ModuleMethodHandle _handle_setMinimum;
        internal static ModuleMethodHandle _handle_setOrientation;
        internal static ModuleMethodHandle _handle_setPageStep;
        internal static ModuleMethodHandle _handle_setSingleStep;
        internal static ModuleMethodHandle _handle_setSliderDown;
        internal static ModuleMethodHandle _handle_setSliderPosition;
        internal static ModuleMethodHandle _handle_setTracking;
        internal static ModuleMethodHandle _handle_setValue;
        internal static ModuleMethodHandle _handle_setRange;
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
            RangeChanged = 1 << 6,
            SliderMoved = 1 << 7,
            SliderPressed = 1 << 8,
            SliderReleased = 1 << 9,
            ValueChanged = 1 << 10
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
        public enum SliderAction
        {
            SliderNoAction,
            SliderSingleStepAdd,
            SliderSingleStepSub,
            SliderPageStepAdd,
            SliderPageStepSub,
            SliderToMinimum,
            SliderToMaximum,
            SliderMove
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SliderAction__Push(SliderAction value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SliderAction SliderAction__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SliderAction)ret;
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
        public class Handle : Widget.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public void SetInvertedAppearance(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInvertedAppearance);
            }
            public void SetInvertedControls(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInvertedControls);
            }
            public void SetMaximum(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximum);
            }
            public void SetMinimum(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimum);
            }
            public void SetOrientation(Orientation orient)
            {
                Orientation__Push(orient);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOrientation);
            }
            public void SetPageStep(int pageStep)
            {
                NativeImplClient.PushInt32(pageStep);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPageStep);
            }
            public void SetSingleStep(int step)
            {
                NativeImplClient.PushInt32(step);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSingleStep);
            }
            public void SetSliderDown(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSliderDown);
            }
            public void SetSliderPosition(int pos)
            {
                NativeImplClient.PushInt32(pos);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSliderPosition);
            }
            public void SetTracking(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTracking);
            }
            public void SetValue(int value)
            {
                NativeImplClient.PushInt32(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setValue);
            }
            public void SetRange(int min, int max)
            {
                NativeImplClient.PushInt32(max);
                NativeImplClient.PushInt32(min);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRange);
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
            _module = NativeImplClient.GetModule("AbstractSlider");
            // assign module handles
            _handle_setInvertedAppearance = NativeImplClient.GetModuleMethod(_module, "Handle_setInvertedAppearance");
            _handle_setInvertedControls = NativeImplClient.GetModuleMethod(_module, "Handle_setInvertedControls");
            _handle_setMaximum = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximum");
            _handle_setMinimum = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimum");
            _handle_setOrientation = NativeImplClient.GetModuleMethod(_module, "Handle_setOrientation");
            _handle_setPageStep = NativeImplClient.GetModuleMethod(_module, "Handle_setPageStep");
            _handle_setSingleStep = NativeImplClient.GetModuleMethod(_module, "Handle_setSingleStep");
            _handle_setSliderDown = NativeImplClient.GetModuleMethod(_module, "Handle_setSliderDown");
            _handle_setSliderPosition = NativeImplClient.GetModuleMethod(_module, "Handle_setSliderPosition");
            _handle_setTracking = NativeImplClient.GetModuleMethod(_module, "Handle_setTracking");
            _handle_setValue = NativeImplClient.GetModuleMethod(_module, "Handle_setValue");
            _handle_setRange = NativeImplClient.GetModuleMethod(_module, "Handle_setRange");
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
