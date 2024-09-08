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
    public static class LineEdit
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_hasAcceptableInput;
        internal static ModuleMethodHandle _handle_setAlignment;
        internal static ModuleMethodHandle _handle_setClearButtonEnabled;
        internal static ModuleMethodHandle _handle_setCursorMoveStyle;
        internal static ModuleMethodHandle _handle_setCursorPosition;
        internal static ModuleMethodHandle _handle_displayText;
        internal static ModuleMethodHandle _handle_setDragEnabled;
        internal static ModuleMethodHandle _handle_setEchoMode;
        internal static ModuleMethodHandle _handle_setFrame;
        internal static ModuleMethodHandle _handle_hasSelectedText;
        internal static ModuleMethodHandle _handle_setInputMask;
        internal static ModuleMethodHandle _handle_setMaxLength;
        internal static ModuleMethodHandle _handle_isModified;
        internal static ModuleMethodHandle _handle_setModified;
        internal static ModuleMethodHandle _handle_setPlaceholderText;
        internal static ModuleMethodHandle _handle_setReadOnly;
        internal static ModuleMethodHandle _handle_isRedoAvailable;
        internal static ModuleMethodHandle _handle_selectedText;
        internal static ModuleMethodHandle _handle_setText;
        internal static ModuleMethodHandle _handle_text;
        internal static ModuleMethodHandle _handle_isUndoAvailable;
        internal static ModuleMethodHandle _handle_clear;
        internal static ModuleMethodHandle _handle_copy;
        internal static ModuleMethodHandle _handle_cut;
        internal static ModuleMethodHandle _handle_paste;
        internal static ModuleMethodHandle _handle_redo;
        internal static ModuleMethodHandle _handle_selectAll;
        internal static ModuleMethodHandle _handle_undo;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_cursorPositionChanged;
        internal static InterfaceMethodHandle _signalHandler_editingFinished;
        internal static InterfaceMethodHandle _signalHandler_inputRejected;
        internal static InterfaceMethodHandle _signalHandler_returnPressed;
        internal static InterfaceMethodHandle _signalHandler_selectionChanged;
        internal static InterfaceMethodHandle _signalHandler_textChanged;
        internal static InterfaceMethodHandle _signalHandler_textEdited;

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
            CursorPositionChanged = 1 << 5,
            EditingFinished = 1 << 6,
            InputRejected = 1 << 7,
            ReturnPressed = 1 << 8,
            SelectionChanged = 1 << 9,
            TextChanged = 1 << 10,
            TextEdited = 1 << 11
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
            void CursorPositionChanged(int oldPos, int newPos);
            void EditingFinished();
            void InputRejected();
            void ReturnPressed();
            void SelectionChanged();
            void TextChanged(string text);
            void TextEdited(string text);
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

            public void CursorPositionChanged(int oldPos, int newPos)
            {
                NativeImplClient.PushInt32(newPos);
                NativeImplClient.PushInt32(oldPos);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_cursorPositionChanged, Id);
            }

            public void EditingFinished()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_editingFinished, Id);
            }

            public void InputRejected()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_inputRejected, Id);
            }

            public void ReturnPressed()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_returnPressed, Id);
            }

            public void SelectionChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_selectionChanged, Id);
            }

            public void TextChanged(string text)
            {
                NativeImplClient.PushString(text);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_textChanged, Id);
            }

            public void TextEdited(string text)
            {
                NativeImplClient.PushString(text);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_textEdited, Id);
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
        public enum EchoMode
        {
            Normal,
            NoEcho,
            Password,
            PasswordEchoOnEdit
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void EchoMode__Push(EchoMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static EchoMode EchoMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (EchoMode)ret;
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
            public bool HasAcceptableInput()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_hasAcceptableInput);
                return NativeImplClient.PopBool();
            }
            public void SetAlignment(Alignment align)
            {
                Alignment__Push(align);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAlignment);
            }
            public void SetClearButtonEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setClearButtonEnabled);
            }
            public void SetCursorMoveStyle(CursorMoveStyle style)
            {
                CursorMoveStyle__Push(style);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCursorMoveStyle);
            }
            public void SetCursorPosition(int pos)
            {
                NativeImplClient.PushInt32(pos);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCursorPosition);
            }
            public string DisplayText()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_displayText);
                return NativeImplClient.PopString();
            }
            public void SetDragEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDragEnabled);
            }
            public void SetEchoMode(EchoMode mode)
            {
                EchoMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setEchoMode);
            }
            public void SetFrame(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFrame);
            }
            public bool HasSelectedText()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_hasSelectedText);
                return NativeImplClient.PopBool();
            }
            public void SetInputMask(string mask)
            {
                NativeImplClient.PushString(mask);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInputMask);
            }
            public void SetMaxLength(int length)
            {
                NativeImplClient.PushInt32(length);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaxLength);
            }
            public bool IsModified()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isModified);
                return NativeImplClient.PopBool();
            }
            public void SetModified(bool modified)
            {
                NativeImplClient.PushBool(modified);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setModified);
            }
            public void SetPlaceholderText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPlaceholderText);
            }
            public void SetReadOnly(bool value)
            {
                NativeImplClient.PushBool(value);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setReadOnly);
            }
            public bool IsRedoAvailable()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isRedoAvailable);
                return NativeImplClient.PopBool();
            }
            public string SelectedText()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_selectedText);
                return NativeImplClient.PopString();
            }
            public void SetText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setText);
            }
            public string Text()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_text);
                return NativeImplClient.PopString();
            }
            public bool IsUndoAvailable()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isUndoAvailable);
                return NativeImplClient.PopBool();
            }
            public void Clear()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_clear);
            }
            public void Copy()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_copy);
            }
            public void Cut()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_cut);
            }
            public void Paste()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_paste);
            }
            public void Redo()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_redo);
            }
            public void SelectAll()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_selectAll);
            }
            public void Undo()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_undo);
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
            _module = NativeImplClient.GetModule("LineEdit");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_hasAcceptableInput = NativeImplClient.GetModuleMethod(_module, "Handle_hasAcceptableInput");
            _handle_setAlignment = NativeImplClient.GetModuleMethod(_module, "Handle_setAlignment");
            _handle_setClearButtonEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setClearButtonEnabled");
            _handle_setCursorMoveStyle = NativeImplClient.GetModuleMethod(_module, "Handle_setCursorMoveStyle");
            _handle_setCursorPosition = NativeImplClient.GetModuleMethod(_module, "Handle_setCursorPosition");
            _handle_displayText = NativeImplClient.GetModuleMethod(_module, "Handle_displayText");
            _handle_setDragEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setDragEnabled");
            _handle_setEchoMode = NativeImplClient.GetModuleMethod(_module, "Handle_setEchoMode");
            _handle_setFrame = NativeImplClient.GetModuleMethod(_module, "Handle_setFrame");
            _handle_hasSelectedText = NativeImplClient.GetModuleMethod(_module, "Handle_hasSelectedText");
            _handle_setInputMask = NativeImplClient.GetModuleMethod(_module, "Handle_setInputMask");
            _handle_setMaxLength = NativeImplClient.GetModuleMethod(_module, "Handle_setMaxLength");
            _handle_isModified = NativeImplClient.GetModuleMethod(_module, "Handle_isModified");
            _handle_setModified = NativeImplClient.GetModuleMethod(_module, "Handle_setModified");
            _handle_setPlaceholderText = NativeImplClient.GetModuleMethod(_module, "Handle_setPlaceholderText");
            _handle_setReadOnly = NativeImplClient.GetModuleMethod(_module, "Handle_setReadOnly");
            _handle_isRedoAvailable = NativeImplClient.GetModuleMethod(_module, "Handle_isRedoAvailable");
            _handle_selectedText = NativeImplClient.GetModuleMethod(_module, "Handle_selectedText");
            _handle_setText = NativeImplClient.GetModuleMethod(_module, "Handle_setText");
            _handle_text = NativeImplClient.GetModuleMethod(_module, "Handle_text");
            _handle_isUndoAvailable = NativeImplClient.GetModuleMethod(_module, "Handle_isUndoAvailable");
            _handle_clear = NativeImplClient.GetModuleMethod(_module, "Handle_clear");
            _handle_copy = NativeImplClient.GetModuleMethod(_module, "Handle_copy");
            _handle_cut = NativeImplClient.GetModuleMethod(_module, "Handle_cut");
            _handle_paste = NativeImplClient.GetModuleMethod(_module, "Handle_paste");
            _handle_redo = NativeImplClient.GetModuleMethod(_module, "Handle_redo");
            _handle_selectAll = NativeImplClient.GetModuleMethod(_module, "Handle_selectAll");
            _handle_undo = NativeImplClient.GetModuleMethod(_module, "Handle_undo");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_cursorPositionChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "cursorPositionChanged");
            _signalHandler_editingFinished = NativeImplClient.GetInterfaceMethod(_signalHandler, "editingFinished");
            _signalHandler_inputRejected = NativeImplClient.GetInterfaceMethod(_signalHandler, "inputRejected");
            _signalHandler_returnPressed = NativeImplClient.GetInterfaceMethod(_signalHandler, "returnPressed");
            _signalHandler_selectionChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "selectionChanged");
            _signalHandler_textChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "textChanged");
            _signalHandler_textEdited = NativeImplClient.GetInterfaceMethod(_signalHandler, "textEdited");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_cursorPositionChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var oldPos = NativeImplClient.PopInt32();
                var newPos = NativeImplClient.PopInt32();
                inst.CursorPositionChanged(oldPos, newPos);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_editingFinished, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.EditingFinished();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_inputRejected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.InputRejected();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_returnPressed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.ReturnPressed();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_selectionChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.SelectionChanged();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_textChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var text = NativeImplClient.PopString();
                inst.TextChanged(text);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_textEdited, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var text = NativeImplClient.PopString();
                inst.TextEdited(text);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
