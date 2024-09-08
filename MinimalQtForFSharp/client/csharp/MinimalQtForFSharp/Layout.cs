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

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Layout
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_setEnabled;
        internal static ModuleMethodHandle _handle_setSpacing;
        internal static ModuleMethodHandle _handle_setContentsMargins;
        internal static ModuleMethodHandle _handle_setSizeConstraint;
        internal static ModuleMethodHandle _handle_removeAll;
        internal static ModuleMethodHandle _handle_activate;
        internal static ModuleMethodHandle _handle_update;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
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
            void Destroyed(Handle obj);
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

            public void Destroyed(Handle obj)
            {
                Handle__Push(obj);
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
        public enum SizeConstraint
        {
            SetDefaultConstraint,
            SetNoConstraint,
            SetMinimumSize,
            SetFixedSize,
            SetMaximumSize,
            SetMinAndMaxSize
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SizeConstraint__Push(SizeConstraint value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SizeConstraint SizeConstraint__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SizeConstraint)ret;
        }
        public class Handle : Object.Handle
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
            public void SetEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setEnabled);
            }
            public void SetSpacing(int spacing)
            {
                NativeImplClient.PushInt32(spacing);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSpacing);
            }
            public void SetContentsMargins(int left, int top, int right, int bottom)
            {
                NativeImplClient.PushInt32(bottom);
                NativeImplClient.PushInt32(right);
                NativeImplClient.PushInt32(top);
                NativeImplClient.PushInt32(left);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setContentsMargins);
            }
            public void SetSizeConstraint(SizeConstraint constraint)
            {
                SizeConstraint__Push(constraint);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizeConstraint);
            }
            public void RemoveAll()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_removeAll);
            }
            public void Activate()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_activate);
            }
            public void Update()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_update);
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
            _module = NativeImplClient.GetModule("Layout");
            // assign module handles
            _handle_setEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setEnabled");
            _handle_setSpacing = NativeImplClient.GetModuleMethod(_module, "Handle_setSpacing");
            _handle_setContentsMargins = NativeImplClient.GetModuleMethod(_module, "Handle_setContentsMargins");
            _handle_setSizeConstraint = NativeImplClient.GetModuleMethod(_module, "Handle_setSizeConstraint");
            _handle_removeAll = NativeImplClient.GetModuleMethod(_module, "Handle_removeAll");
            _handle_activate = NativeImplClient.GetModuleMethod(_module, "Handle_activate");
            _handle_update = NativeImplClient.GetModuleMethod(_module, "Handle_update");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            NativeImplClient.SetClientMethodWrapper(_signalHandler_destroyed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var obj = Handle__Pop();
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
