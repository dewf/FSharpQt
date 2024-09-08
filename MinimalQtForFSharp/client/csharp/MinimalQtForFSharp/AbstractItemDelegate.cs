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
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class AbstractItemDelegate
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_closeEditor;
        internal static InterfaceMethodHandle _signalHandler_commitData;
        internal static InterfaceMethodHandle _signalHandler_sizeHintChanged;
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // SignalMask:
            CloseEditor = 1 << 2,
            CommitData = 1 << 3,
            SizeHintChanged = 1 << 4
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
        public enum EndEditHint
        {
            NoHint,
            EditNextItem,
            EditPreviousItem,
            SubmitModelCache,
            RevertModelCache
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void EndEditHint__Push(EndEditHint value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static EndEditHint EndEditHint__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (EndEditHint)ret;
        }

        public interface SignalHandler : IDisposable
        {
            void IDisposable.Dispose()
            {
                // nothing by default
            }
            void Destroyed(Object.Handle obj);
            void ObjectNameChanged(string objectName);
            void CloseEditor(Widget.Handle editor, EndEditHint hint);
            void CommitData(Widget.Handle editor);
            void SizeHintChanged(ModelIndex.Handle index);
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

            public void CloseEditor(Widget.Handle editor, EndEditHint hint)
            {
                EndEditHint__Push(hint);
                Widget.Handle__Push(editor);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_closeEditor, Id);
            }

            public void CommitData(Widget.Handle editor)
            {
                Widget.Handle__Push(editor);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_commitData, Id);
            }

            public void SizeHintChanged(ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sizeHintChanged, Id);
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
            _module = NativeImplClient.GetModule("AbstractItemDelegate");
            // assign module handles
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_closeEditor = NativeImplClient.GetInterfaceMethod(_signalHandler, "closeEditor");
            _signalHandler_commitData = NativeImplClient.GetInterfaceMethod(_signalHandler, "commitData");
            _signalHandler_sizeHintChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "sizeHintChanged");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_closeEditor, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var editor = Widget.Handle__Pop();
                var hint = EndEditHint__Pop();
                inst.CloseEditor(editor, hint);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_commitData, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var editor = Widget.Handle__Pop();
                inst.CommitData(editor);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sizeHintChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                inst.SizeHintChanged(index);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
