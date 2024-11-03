using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.AbstractItemDelegate;
using static Org.Whatever.MinimalQtForFSharp.Object;
using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.AbstractItemModel;
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;
using static Org.Whatever.MinimalQtForFSharp.StyleOptionViewItem;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class StyledItemDelegate
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _createdSubclassed;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_closeEditor;
        internal static InterfaceMethodHandle _signalHandler_commitData;
        internal static InterfaceMethodHandle _signalHandler_sizeHintChanged;
        internal static InterfaceHandle _methodDelegate;
        internal static InterfaceMethodHandle _methodDelegate_createEditor;
        internal static InterfaceMethodHandle _methodDelegate_setEditorData;
        internal static InterfaceMethodHandle _methodDelegate_setModelData;

        public static Handle CreatedSubclassed(MethodDelegate methodDelegate, MethodMask methodMask, SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            MethodMask__Push(methodMask);
            MethodDelegate__Push(methodDelegate, false);
            NativeImplClient.InvokeModuleMethod(_createdSubclassed);
            return Handle__Pop();
        }
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // AbstractItemDelegate.SignalMask:
            CloseEditor = 1 << 2,
            CommitData = 1 << 3,
            SizeHintChanged = 1 << 4,
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
        public class Handle : AbstractItemDelegate.Handle, IDisposable
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
        [Flags]
        public enum MethodMask
        {
            // MethodMask:
            CreateEditor = 1 << 0,
            SetEditorData = 1 << 1,
            SetModelData = 1 << 2
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MethodMask__Push(MethodMask value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodMask MethodMask__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (MethodMask)ret;
        }

        public interface MethodDelegate : IDisposable
        {
            void IDisposable.Dispose()
            {
                // nothing by default
            }
            Widget.Handle CreateEditor(Widget.Handle parent, StyleOptionViewItem.Handle option, ModelIndex.Handle index);
            void SetEditorData(Widget.Handle editor, ModelIndex.Handle index);
            void SetModelData(Widget.Handle editor, AbstractItemModel.Handle model, ModelIndex.Handle index);
        }

        private static Dictionary<MethodDelegate, IPushable> __MethodDelegateToPushable = new();
        internal class __MethodDelegateWrapper : ClientInterfaceWrapper<MethodDelegate>
        {
            public __MethodDelegateWrapper(MethodDelegate rawInterface) : base(rawInterface)
            {
            }
            protected override void ReleaseExtra()
            {
                // remove the raw interface from the lookup table, no longer needed
                __MethodDelegateToPushable.Remove(RawInterface);
            }
        }

        internal static void MethodDelegate__Push(MethodDelegate thing, bool isReturn)
        {
            if (thing != null)
            {
                if (__MethodDelegateToPushable.TryGetValue(thing, out var pushable))
                {
                    // either an already-known client thing, or a server thing
                    pushable.Push(isReturn);
                }
                else
                {
                    // as-yet-unknown client thing - wrap and add to lookup table
                    pushable = new __MethodDelegateWrapper(thing);
                    __MethodDelegateToPushable.Add(thing, pushable);
                }
                pushable.Push(isReturn);
            }
            else
            {
                NativeImplClient.PushNull();
            }
        }

        internal static MethodDelegate MethodDelegate__Pop()
        {
            NativeImplClient.PopInstanceId(out var id, out var isClientId);
            if (id != 0)
            {
                if (isClientId)
                {
                    // we must have sent it over originally, so wrapper must exist
                    var wrapper = (__MethodDelegateWrapper)ClientObject.GetById(id);
                    return wrapper.RawInterface;
                }
                else // server ID
                {
                    var thing = new ServerMethodDelegate(id);
                    // add to lookup table before returning
                    __MethodDelegateToPushable.Add(thing, thing);
                    return thing;
                }
            }
            else
            {
                return null;
            }
        }

        private class ServerMethodDelegate : ServerObject, MethodDelegate
        {
            public ServerMethodDelegate(int id) : base(id)
            {
            }

            public Widget.Handle CreateEditor(Widget.Handle parent, StyleOptionViewItem.Handle option, ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                StyleOptionViewItem.Handle__Push(option);
                Widget.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_createEditor, Id);
                return Widget.Handle__Pop();
            }

            public void SetEditorData(Widget.Handle editor, ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                Widget.Handle__Push(editor);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_setEditorData, Id);
            }

            public void SetModelData(Widget.Handle editor, AbstractItemModel.Handle model, ModelIndex.Handle index)
            {
                ModelIndex.Handle__Push(index);
                AbstractItemModel.Handle__Push(model);
                Widget.Handle__Push(editor);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_setModelData, Id);
            }

            protected override void ReleaseExtra()
            {
                // remove from lookup table
                __MethodDelegateToPushable.Remove(this);
            }

            public void Dispose()
            {
                // will invoke ReleaseExtra() for us
                ServerDispose();
            }
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("StyledItemDelegate");
            // assign module handles
            _createdSubclassed = NativeImplClient.GetModuleMethod(_module, "createdSubclassed");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
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
            _methodDelegate = NativeImplClient.GetInterface(_module, "MethodDelegate");
            _methodDelegate_createEditor = NativeImplClient.GetInterfaceMethod(_methodDelegate, "createEditor");
            _methodDelegate_setEditorData = NativeImplClient.GetInterfaceMethod(_methodDelegate, "setEditorData");
            _methodDelegate_setModelData = NativeImplClient.GetInterfaceMethod(_methodDelegate, "setModelData");
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_createEditor, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var parent = Widget.Handle__Pop();
                var option = StyleOptionViewItem.Handle__Pop();
                var index = ModelIndex.Handle__Pop();
                Widget.Handle__Push(inst.CreateEditor(parent, option, index));
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_setEditorData, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var editor = Widget.Handle__Pop();
                var index = ModelIndex.Handle__Pop();
                inst.SetEditorData(editor, index);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_setModelData, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var editor = Widget.Handle__Pop();
                var model = AbstractItemModel.Handle__Pop();
                var index = ModelIndex.Handle__Pop();
                inst.SetModelData(editor, model, index);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
