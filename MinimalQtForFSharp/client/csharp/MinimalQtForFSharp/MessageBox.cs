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
using static Org.Whatever.MinimalQtForFSharp.Dialog;
using static Org.Whatever.MinimalQtForFSharp.AbstractButton;
using static Org.Whatever.MinimalQtForFSharp.Widget;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class MessageBox
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setDetailedText;
        internal static ModuleMethodHandle _handle_setIcon;
        internal static ModuleMethodHandle _handle_setInformativeText;
        internal static ModuleMethodHandle _handle_setOptions;
        internal static ModuleMethodHandle _handle_setStandardButtons;
        internal static ModuleMethodHandle _handle_setText;
        internal static ModuleMethodHandle _handle_setTextFormat;
        internal static ModuleMethodHandle _handle_setTextInteractionFlags;
        internal static ModuleMethodHandle _handle_setDefaultButton;
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
        internal static InterfaceMethodHandle _signalHandler_buttonClicked;

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
            // Dialog.SignalMask:
            Accepted = 1 << 5,
            Finished = 1 << 6,
            Rejected = 1 << 7,
            // SignalMask:
            ButtonClicked = 1 << 8
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
            void ButtonClicked(AbstractButton.Handle button);
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

            public void ButtonClicked(AbstractButton.Handle button)
            {
                AbstractButton.Handle__Push(button);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_buttonClicked, Id);
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
        public enum StandardButton
        {
            NoButton = 0x00000000,
            Ok = 0x00000400,
            Save = 0x00000800,
            SaveAll = 0x00001000,
            Open = 0x00002000,
            Yes = 0x00004000,
            YesToAll = 0x00008000,
            No = 0x00010000,
            NoToAll = 0x00020000,
            Abort = 0x00040000,
            Retry = 0x00080000,
            Ignore = 0x00100000,
            Close = 0x00200000,
            Cancel = 0x00400000,
            Discard = 0x00800000,
            Help = 0x01000000,
            Apply = 0x02000000,
            Reset = 0x04000000,
            RestoreDefaults = 0x08000000
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void StandardButton__Push(StandardButton value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static StandardButton StandardButton__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (StandardButton)ret;
        }
        [Flags]
        public enum StandardButtonSet
        {
            NoButton = 0x00000000,
            Ok = 0x00000400,
            Save = 0x00000800,
            SaveAll = 0x00001000,
            Open = 0x00002000,
            Yes = 0x00004000,
            YesToAll = 0x00008000,
            No = 0x00010000,
            NoToAll = 0x00020000,
            Abort = 0x00040000,
            Retry = 0x00080000,
            Ignore = 0x00100000,
            Close = 0x00200000,
            Cancel = 0x00400000,
            Discard = 0x00800000,
            Help = 0x01000000,
            Apply = 0x02000000,
            Reset = 0x04000000,
            RestoreDefaults = 0x08000000
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void StandardButtonSet__Push(StandardButtonSet value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static StandardButtonSet StandardButtonSet__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (StandardButtonSet)ret;
        }
        public enum MessageBoxIcon
        {
            NoIcon = 0,
            Information = 1,
            Warning = 2,
            Critical = 3,
            Question = 4
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void MessageBoxIcon__Push(MessageBoxIcon value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static MessageBoxIcon MessageBoxIcon__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (MessageBoxIcon)ret;
        }
        [Flags]
        public enum Options
        {
            DontUseNativeDialog = 0x00000001
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Options__Push(Options value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Options Options__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Options)ret;
        }
        public class Handle : Dialog.Handle
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
            public void SetDetailedText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDetailedText);
            }
            public void SetIcon(MessageBoxIcon icon)
            {
                MessageBoxIcon__Push(icon);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIcon);
            }
            public void SetInformativeText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInformativeText);
            }
            public void SetOptions(Options opts)
            {
                Options__Push(opts);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOptions);
            }
            public void SetStandardButtons(StandardButtonSet buttons)
            {
                StandardButtonSet__Push(buttons);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setStandardButtons);
            }
            public void SetText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setText);
            }
            public void SetTextFormat(TextFormat format)
            {
                TextFormat__Push(format);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextFormat);
            }
            public void SetTextInteractionFlags(TextInteractionFlags tiFlags)
            {
                TextInteractionFlags__Push(tiFlags);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextInteractionFlags);
            }
            public void SetDefaultButton(StandardButton button)
            {
                StandardButton__Push(button);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDefaultButton);
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
            _module = NativeImplClient.GetModule("MessageBox");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setDetailedText = NativeImplClient.GetModuleMethod(_module, "Handle_setDetailedText");
            _handle_setIcon = NativeImplClient.GetModuleMethod(_module, "Handle_setIcon");
            _handle_setInformativeText = NativeImplClient.GetModuleMethod(_module, "Handle_setInformativeText");
            _handle_setOptions = NativeImplClient.GetModuleMethod(_module, "Handle_setOptions");
            _handle_setStandardButtons = NativeImplClient.GetModuleMethod(_module, "Handle_setStandardButtons");
            _handle_setText = NativeImplClient.GetModuleMethod(_module, "Handle_setText");
            _handle_setTextFormat = NativeImplClient.GetModuleMethod(_module, "Handle_setTextFormat");
            _handle_setTextInteractionFlags = NativeImplClient.GetModuleMethod(_module, "Handle_setTextInteractionFlags");
            _handle_setDefaultButton = NativeImplClient.GetModuleMethod(_module, "Handle_setDefaultButton");
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
            _signalHandler_buttonClicked = NativeImplClient.GetInterfaceMethod(_signalHandler, "buttonClicked");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_buttonClicked, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var button = AbstractButton.Handle__Pop();
                inst.ButtonClicked(button);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
