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
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.AbstractScrollArea;
using static Org.Whatever.MinimalQtForFSharp.TextOption;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class PlainTextEdit
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setBackgroundVisible;
        internal static ModuleMethodHandle _handle_blockCount;
        internal static ModuleMethodHandle _handle_setCenterOnScroll;
        internal static ModuleMethodHandle _handle_setCursorWidth;
        internal static ModuleMethodHandle _handle_setDocumentTitle;
        internal static ModuleMethodHandle _handle_setLineWrapMode;
        internal static ModuleMethodHandle _handle_setMaximumBlockCount;
        internal static ModuleMethodHandle _handle_setOverwriteMode;
        internal static ModuleMethodHandle _handle_setPlaceholderText;
        internal static ModuleMethodHandle _handle_setPlainText;
        internal static ModuleMethodHandle _handle_setReadOnly;
        internal static ModuleMethodHandle _handle_setTabChangesFocus;
        internal static ModuleMethodHandle _handle_setTabStopDistance;
        internal static ModuleMethodHandle _handle_setTextInteractionFlags;
        internal static ModuleMethodHandle _handle_setUndoRedoEnabled;
        internal static ModuleMethodHandle _handle_setWordWrapMode;
        internal static ModuleMethodHandle _handle_toPlainText;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_blockCountChanged;
        internal static InterfaceMethodHandle _signalHandler_copyAvailable;
        internal static InterfaceMethodHandle _signalHandler_cursorPositionChanged;
        internal static InterfaceMethodHandle _signalHandler_modificationChanged;
        internal static InterfaceMethodHandle _signalHandler_redoAvailable;
        internal static InterfaceMethodHandle _signalHandler_selectionChanged;
        internal static InterfaceMethodHandle _signalHandler_textChanged;
        internal static InterfaceMethodHandle _signalHandler_undoAvailable;
        internal static InterfaceMethodHandle _signalHandler_updateRequest;

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
            // SignalMask:
            BlockCountChanged = 1 << 5,
            CopyAvailable = 1 << 6,
            CursorPositionChanged = 1 << 7,
            ModificationChanged = 1 << 8,
            RedoAvailable = 1 << 9,
            SelectionChanged = 1 << 10,
            TextChanged = 1 << 11,
            UndoAvailable = 1 << 12,
            UpdateRequest = 1 << 13
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
            void BlockCountChanged(int newBlockCount);
            void CopyAvailable(bool yes);
            void CursorPositionChanged();
            void ModificationChanged(bool changed);
            void RedoAvailable(bool available);
            void SelectionChanged();
            void TextChanged();
            void UndoAvailable(bool available);
            void UpdateRequest(Rect rect, int dy);
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

            public void BlockCountChanged(int newBlockCount)
            {
                NativeImplClient.PushInt32(newBlockCount);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_blockCountChanged, Id);
            }

            public void CopyAvailable(bool yes)
            {
                NativeImplClient.PushBool(yes);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_copyAvailable, Id);
            }

            public void CursorPositionChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_cursorPositionChanged, Id);
            }

            public void ModificationChanged(bool changed)
            {
                NativeImplClient.PushBool(changed);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_modificationChanged, Id);
            }

            public void RedoAvailable(bool available)
            {
                NativeImplClient.PushBool(available);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_redoAvailable, Id);
            }

            public void SelectionChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_selectionChanged, Id);
            }

            public void TextChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_textChanged, Id);
            }

            public void UndoAvailable(bool available)
            {
                NativeImplClient.PushBool(available);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_undoAvailable, Id);
            }

            public void UpdateRequest(Rect rect, int dy)
            {
                NativeImplClient.PushInt32(dy);
                Rect__Push(rect, false);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_updateRequest, Id);
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
        public enum LineWrapMode
        {
            NoWrap,
            WidgetWidth
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void LineWrapMode__Push(LineWrapMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static LineWrapMode LineWrapMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (LineWrapMode)ret;
        }
        public class Handle : AbstractScrollArea.Handle
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
            public void SetBackgroundVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setBackgroundVisible);
            }
            public int BlockCount()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_blockCount);
                return NativeImplClient.PopInt32();
            }
            public void SetCenterOnScroll(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCenterOnScroll);
            }
            public void SetCursorWidth(int width)
            {
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCursorWidth);
            }
            public void SetDocumentTitle(string title)
            {
                NativeImplClient.PushString(title);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDocumentTitle);
            }
            public void SetLineWrapMode(LineWrapMode mode)
            {
                LineWrapMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setLineWrapMode);
            }
            public void SetMaximumBlockCount(int count)
            {
                NativeImplClient.PushInt32(count);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximumBlockCount);
            }
            public void SetOverwriteMode(bool overwrite)
            {
                NativeImplClient.PushBool(overwrite);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOverwriteMode);
            }
            public void SetPlaceholderText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPlaceholderText);
            }
            public void SetPlainText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPlainText);
            }
            public void SetReadOnly(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setReadOnly);
            }
            public void SetTabChangesFocus(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabChangesFocus);
            }
            public void SetTabStopDistance(double distance)
            {
                NativeImplClient.PushDouble(distance);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabStopDistance);
            }
            public void SetTextInteractionFlags(TextInteractionFlags tiFlags)
            {
                TextInteractionFlags__Push(tiFlags);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextInteractionFlags);
            }
            public void SetUndoRedoEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setUndoRedoEnabled);
            }
            public void SetWordWrapMode(WrapMode mode)
            {
                WrapMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWordWrapMode);
            }
            public string ToPlainText()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toPlainText);
                return NativeImplClient.PopString();
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
            _module = NativeImplClient.GetModule("PlainTextEdit");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setBackgroundVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setBackgroundVisible");
            _handle_blockCount = NativeImplClient.GetModuleMethod(_module, "Handle_blockCount");
            _handle_setCenterOnScroll = NativeImplClient.GetModuleMethod(_module, "Handle_setCenterOnScroll");
            _handle_setCursorWidth = NativeImplClient.GetModuleMethod(_module, "Handle_setCursorWidth");
            _handle_setDocumentTitle = NativeImplClient.GetModuleMethod(_module, "Handle_setDocumentTitle");
            _handle_setLineWrapMode = NativeImplClient.GetModuleMethod(_module, "Handle_setLineWrapMode");
            _handle_setMaximumBlockCount = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximumBlockCount");
            _handle_setOverwriteMode = NativeImplClient.GetModuleMethod(_module, "Handle_setOverwriteMode");
            _handle_setPlaceholderText = NativeImplClient.GetModuleMethod(_module, "Handle_setPlaceholderText");
            _handle_setPlainText = NativeImplClient.GetModuleMethod(_module, "Handle_setPlainText");
            _handle_setReadOnly = NativeImplClient.GetModuleMethod(_module, "Handle_setReadOnly");
            _handle_setTabChangesFocus = NativeImplClient.GetModuleMethod(_module, "Handle_setTabChangesFocus");
            _handle_setTabStopDistance = NativeImplClient.GetModuleMethod(_module, "Handle_setTabStopDistance");
            _handle_setTextInteractionFlags = NativeImplClient.GetModuleMethod(_module, "Handle_setTextInteractionFlags");
            _handle_setUndoRedoEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setUndoRedoEnabled");
            _handle_setWordWrapMode = NativeImplClient.GetModuleMethod(_module, "Handle_setWordWrapMode");
            _handle_toPlainText = NativeImplClient.GetModuleMethod(_module, "Handle_toPlainText");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_blockCountChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "blockCountChanged");
            _signalHandler_copyAvailable = NativeImplClient.GetInterfaceMethod(_signalHandler, "copyAvailable");
            _signalHandler_cursorPositionChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "cursorPositionChanged");
            _signalHandler_modificationChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "modificationChanged");
            _signalHandler_redoAvailable = NativeImplClient.GetInterfaceMethod(_signalHandler, "redoAvailable");
            _signalHandler_selectionChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "selectionChanged");
            _signalHandler_textChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "textChanged");
            _signalHandler_undoAvailable = NativeImplClient.GetInterfaceMethod(_signalHandler, "undoAvailable");
            _signalHandler_updateRequest = NativeImplClient.GetInterfaceMethod(_signalHandler, "updateRequest");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_blockCountChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var newBlockCount = NativeImplClient.PopInt32();
                inst.BlockCountChanged(newBlockCount);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_copyAvailable, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var yes = NativeImplClient.PopBool();
                inst.CopyAvailable(yes);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_cursorPositionChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.CursorPositionChanged();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_modificationChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var changed = NativeImplClient.PopBool();
                inst.ModificationChanged(changed);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_redoAvailable, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var available = NativeImplClient.PopBool();
                inst.RedoAvailable(available);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_selectionChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.SelectionChanged();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_textChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.TextChanged();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_undoAvailable, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var available = NativeImplClient.PopBool();
                inst.UndoAvailable(available);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_updateRequest, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var rect = Rect__Pop();
                var dy = NativeImplClient.PopInt32();
                inst.UpdateRequest(rect, dy);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
