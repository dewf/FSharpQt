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
using static Org.Whatever.MinimalQtForFSharp.Widget;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Dialog
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setModal;
        internal static ModuleMethodHandle _handle_setSizeGripEnabled;
        internal static ModuleMethodHandle _handle_setParentDialogFlags;
        internal static ModuleMethodHandle _handle_accept;
        internal static ModuleMethodHandle _handle_reject;
        internal static ModuleMethodHandle _handle_exec;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_accepted;
        internal static InterfaceMethodHandle _signalHandler_finished;
        internal static InterfaceMethodHandle _signalHandler_rejected;

        public static Handle Create(Widget.Handle parent, SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            Widget.Handle__Push(parent);
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
            Accepted = 1 << 5,
            Finished = 1 << 6,
            Rejected = 1 << 7
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
            void Accepted();
            void Finished(int result);
            void Rejected();
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

            public void Accepted()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_accepted, Id);
            }

            public void Finished(int result)
            {
                NativeImplClient.PushInt32(result);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_finished, Id);
            }

            public void Rejected()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rejected, Id);
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
            public void SetModal(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setModal);
            }
            public void SetSizeGripEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizeGripEnabled);
            }
            public void SetParentDialogFlags(Widget.Handle parent)
            {
                Widget.Handle__Push(parent);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setParentDialogFlags);
            }
            public void Accept()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_accept);
            }
            public void Reject()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_reject);
            }
            public int Exec()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_exec);
                return NativeImplClient.PopInt32();
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
            _module = NativeImplClient.GetModule("Dialog");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setModal = NativeImplClient.GetModuleMethod(_module, "Handle_setModal");
            _handle_setSizeGripEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setSizeGripEnabled");
            _handle_setParentDialogFlags = NativeImplClient.GetModuleMethod(_module, "Handle_setParentDialogFlags");
            _handle_accept = NativeImplClient.GetModuleMethod(_module, "Handle_accept");
            _handle_reject = NativeImplClient.GetModuleMethod(_module, "Handle_reject");
            _handle_exec = NativeImplClient.GetModuleMethod(_module, "Handle_exec");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_accepted = NativeImplClient.GetInterfaceMethod(_signalHandler, "accepted");
            _signalHandler_finished = NativeImplClient.GetInterfaceMethod(_signalHandler, "finished");
            _signalHandler_rejected = NativeImplClient.GetInterfaceMethod(_signalHandler, "rejected");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_accepted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Accepted();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_finished, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var result = NativeImplClient.PopInt32();
                inst.Finished(result);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rejected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Rejected();
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
