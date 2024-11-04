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
using static Org.Whatever.MinimalQtForFSharp.Widget;
using static Org.Whatever.MinimalQtForFSharp.Variant;
using static Org.Whatever.MinimalQtForFSharp.AbstractItemModel;
using static Org.Whatever.MinimalQtForFSharp.Icon;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class ComboBox
    {
        private static ModuleHandle _module;

        // built-in array type: string[]
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _downcastFrom;
        internal static ModuleMethodHandle _handle_count;
        internal static ModuleMethodHandle _handle_currentData;
        internal static ModuleMethodHandle _handle_currentData_overload1;
        internal static ModuleMethodHandle _handle_currentIndex;
        internal static ModuleMethodHandle _handle_setCurrentIndex;
        internal static ModuleMethodHandle _handle_setCurrentText;
        internal static ModuleMethodHandle _handle_setDuplicatesEnabled;
        internal static ModuleMethodHandle _handle_setEditable;
        internal static ModuleMethodHandle _handle_setFrame;
        internal static ModuleMethodHandle _handle_setIconSize;
        internal static ModuleMethodHandle _handle_setInsertPolicy;
        internal static ModuleMethodHandle _handle_setMaxCount;
        internal static ModuleMethodHandle _handle_setMaxVisibleItems;
        internal static ModuleMethodHandle _handle_setMinimumContentsLength;
        internal static ModuleMethodHandle _handle_setModelColumn;
        internal static ModuleMethodHandle _handle_setPlaceholderText;
        internal static ModuleMethodHandle _handle_setSizeAdjustPolicy;
        internal static ModuleMethodHandle _handle_clear;
        internal static ModuleMethodHandle _handle_addItem;
        internal static ModuleMethodHandle _handle_addItem_overload1;
        internal static ModuleMethodHandle _handle_addItems;
        internal static ModuleMethodHandle _handle_setModel;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_activated;
        internal static InterfaceMethodHandle _signalHandler_currentIndexChanged;
        internal static InterfaceMethodHandle _signalHandler_currentTextChanged;
        internal static InterfaceMethodHandle _signalHandler_editTextChanged;
        internal static InterfaceMethodHandle _signalHandler_highlighted;
        internal static InterfaceMethodHandle _signalHandler_textActivated;
        internal static InterfaceMethodHandle _signalHandler_textHighlighted;

        public static Handle Create(SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }

        public static Handle DowncastFrom(Widget.Handle widget)
        {
            Widget.Handle__Push(widget);
            NativeImplClient.InvokeModuleMethod(_downcastFrom);
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
            CurrentIndexChanged = 1 << 6,
            CurrentTextChanged = 1 << 7,
            EditTextChanged = 1 << 8,
            Highlighted = 1 << 9,
            TextActivated = 1 << 10,
            TextHighlighted = 1 << 11
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
            void Activated(int index);
            void CurrentIndexChanged(int index);
            void CurrentTextChanged(string text);
            void EditTextChanged(string text);
            void Highlighted(int index);
            void TextActivated(string text);
            void TextHighlighted(string text);
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

            public void Activated(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_activated, Id);
            }

            public void CurrentIndexChanged(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_currentIndexChanged, Id);
            }

            public void CurrentTextChanged(string text)
            {
                NativeImplClient.PushString(text);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_currentTextChanged, Id);
            }

            public void EditTextChanged(string text)
            {
                NativeImplClient.PushString(text);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_editTextChanged, Id);
            }

            public void Highlighted(int index)
            {
                NativeImplClient.PushInt32(index);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_highlighted, Id);
            }

            public void TextActivated(string text)
            {
                NativeImplClient.PushString(text);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_textActivated, Id);
            }

            public void TextHighlighted(string text)
            {
                NativeImplClient.PushString(text);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_textHighlighted, Id);
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
        public enum InsertPolicy
        {
            NoInsert,
            InsertAtTop,
            InsertAtCurrent,
            InsertAtBottom,
            InsertAfterCurrent,
            InsertBeforeCurrent,
            InsertAlphabetically
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void InsertPolicy__Push(InsertPolicy value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static InsertPolicy InsertPolicy__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (InsertPolicy)ret;
        }
        public enum SizeAdjustPolicy
        {
            AdjustToContents,
            AdjustToContentsOnFirstShow,
            AdjustToMinimumContentsLengthWithIcon
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SizeAdjustPolicy__Push(SizeAdjustPolicy value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static SizeAdjustPolicy SizeAdjustPolicy__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SizeAdjustPolicy)ret;
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
            public int Count()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_count);
                return NativeImplClient.PopInt32();
            }
            public Owned CurrentData()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_currentData);
                return Owned__Pop();
            }
            public Owned CurrentData(ItemDataRole role)
            {
                ItemDataRole__Push(role);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_currentData_overload1);
                return Owned__Pop();
            }
            public int CurrentIndex()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_currentIndex);
                return NativeImplClient.PopInt32();
            }
            public void SetCurrentIndex(int index)
            {
                NativeImplClient.PushInt32(index);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCurrentIndex);
            }
            public void SetCurrentText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCurrentText);
            }
            public void SetDuplicatesEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDuplicatesEnabled);
            }
            public void SetEditable(bool editable)
            {
                NativeImplClient.PushBool(editable);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setEditable);
            }
            public void SetFrame(bool hasFrame)
            {
                NativeImplClient.PushBool(hasFrame);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFrame);
            }
            public void SetIconSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconSize);
            }
            public void SetInsertPolicy(InsertPolicy policy)
            {
                InsertPolicy__Push(policy);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInsertPolicy);
            }
            public void SetMaxCount(int count)
            {
                NativeImplClient.PushInt32(count);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaxCount);
            }
            public void SetMaxVisibleItems(int count)
            {
                NativeImplClient.PushInt32(count);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaxVisibleItems);
            }
            public void SetMinimumContentsLength(int length)
            {
                NativeImplClient.PushInt32(length);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimumContentsLength);
            }
            public void SetModelColumn(int column)
            {
                NativeImplClient.PushInt32(column);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setModelColumn);
            }
            public void SetPlaceholderText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPlaceholderText);
            }
            public void SetSizeAdjustPolicy(SizeAdjustPolicy policy)
            {
                SizeAdjustPolicy__Push(policy);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizeAdjustPolicy);
            }
            public void Clear()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_clear);
            }
            public void AddItem(string text, Variant.Deferred userData)
            {
                Variant.Deferred__Push(userData, false);
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addItem);
            }
            public void AddItem(Icon.Deferred icon, string text, Variant.Deferred userData)
            {
                Variant.Deferred__Push(userData, false);
                NativeImplClient.PushString(text);
                Icon.Deferred__Push(icon, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addItem_overload1);
            }
            public void AddItems(string[] texts)
            {
                NativeImplClient.PushStringArray(texts);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addItems);
            }
            public void SetModel(AbstractItemModel.Handle model)
            {
                AbstractItemModel.Handle__Push(model);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setModel);
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
            _module = NativeImplClient.GetModule("ComboBox");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _downcastFrom = NativeImplClient.GetModuleMethod(_module, "downcastFrom");
            _handle_count = NativeImplClient.GetModuleMethod(_module, "Handle_count");
            _handle_currentData = NativeImplClient.GetModuleMethod(_module, "Handle_currentData");
            _handle_currentData_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_currentData_overload1");
            _handle_currentIndex = NativeImplClient.GetModuleMethod(_module, "Handle_currentIndex");
            _handle_setCurrentIndex = NativeImplClient.GetModuleMethod(_module, "Handle_setCurrentIndex");
            _handle_setCurrentText = NativeImplClient.GetModuleMethod(_module, "Handle_setCurrentText");
            _handle_setDuplicatesEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setDuplicatesEnabled");
            _handle_setEditable = NativeImplClient.GetModuleMethod(_module, "Handle_setEditable");
            _handle_setFrame = NativeImplClient.GetModuleMethod(_module, "Handle_setFrame");
            _handle_setIconSize = NativeImplClient.GetModuleMethod(_module, "Handle_setIconSize");
            _handle_setInsertPolicy = NativeImplClient.GetModuleMethod(_module, "Handle_setInsertPolicy");
            _handle_setMaxCount = NativeImplClient.GetModuleMethod(_module, "Handle_setMaxCount");
            _handle_setMaxVisibleItems = NativeImplClient.GetModuleMethod(_module, "Handle_setMaxVisibleItems");
            _handle_setMinimumContentsLength = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimumContentsLength");
            _handle_setModelColumn = NativeImplClient.GetModuleMethod(_module, "Handle_setModelColumn");
            _handle_setPlaceholderText = NativeImplClient.GetModuleMethod(_module, "Handle_setPlaceholderText");
            _handle_setSizeAdjustPolicy = NativeImplClient.GetModuleMethod(_module, "Handle_setSizeAdjustPolicy");
            _handle_clear = NativeImplClient.GetModuleMethod(_module, "Handle_clear");
            _handle_addItem = NativeImplClient.GetModuleMethod(_module, "Handle_addItem");
            _handle_addItem_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_addItem_overload1");
            _handle_addItems = NativeImplClient.GetModuleMethod(_module, "Handle_addItems");
            _handle_setModel = NativeImplClient.GetModuleMethod(_module, "Handle_setModel");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_activated = NativeImplClient.GetInterfaceMethod(_signalHandler, "activated");
            _signalHandler_currentIndexChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "currentIndexChanged");
            _signalHandler_currentTextChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "currentTextChanged");
            _signalHandler_editTextChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "editTextChanged");
            _signalHandler_highlighted = NativeImplClient.GetInterfaceMethod(_signalHandler, "highlighted");
            _signalHandler_textActivated = NativeImplClient.GetInterfaceMethod(_signalHandler, "textActivated");
            _signalHandler_textHighlighted = NativeImplClient.GetInterfaceMethod(_signalHandler, "textHighlighted");
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
                var index = NativeImplClient.PopInt32();
                inst.Activated(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_currentIndexChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = NativeImplClient.PopInt32();
                inst.CurrentIndexChanged(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_currentTextChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var text = NativeImplClient.PopString();
                inst.CurrentTextChanged(text);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_editTextChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var text = NativeImplClient.PopString();
                inst.EditTextChanged(text);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_highlighted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var index = NativeImplClient.PopInt32();
                inst.Highlighted(index);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_textActivated, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var text = NativeImplClient.PopString();
                inst.TextActivated(text);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_textHighlighted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var text = NativeImplClient.PopString();
                inst.TextHighlighted(text);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
