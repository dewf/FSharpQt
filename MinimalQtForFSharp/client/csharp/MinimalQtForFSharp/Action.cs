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
using static Org.Whatever.MinimalQtForFSharp.KeySequence;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Action
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setAutoRepeat;
        internal static ModuleMethodHandle _handle_setCheckable;
        internal static ModuleMethodHandle _handle_setChecked;
        internal static ModuleMethodHandle _handle_setEnabled;
        internal static ModuleMethodHandle _handle_setIcon;
        internal static ModuleMethodHandle _handle_setIconText;
        internal static ModuleMethodHandle _handle_setIconVisibleInMenu;
        internal static ModuleMethodHandle _handle_setMenuRole;
        internal static ModuleMethodHandle _handle_setPriority;
        internal static ModuleMethodHandle _handle_setShortcut;
        internal static ModuleMethodHandle _handle_setShortcutContext;
        internal static ModuleMethodHandle _handle_setShortcutVisibleInContextMenu;
        internal static ModuleMethodHandle _handle_setStatusTip;
        internal static ModuleMethodHandle _handle_setText;
        internal static ModuleMethodHandle _handle_setToolTip;
        internal static ModuleMethodHandle _handle_setVisible;
        internal static ModuleMethodHandle _handle_setWhatsThis;
        internal static ModuleMethodHandle _handle_setSeparator;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_changed;
        internal static InterfaceMethodHandle _signalHandler_checkableChanged;
        internal static InterfaceMethodHandle _signalHandler_enabledChanged;
        internal static InterfaceMethodHandle _signalHandler_hovered;
        internal static InterfaceMethodHandle _signalHandler_toggled;
        internal static InterfaceMethodHandle _signalHandler_triggered;
        internal static InterfaceMethodHandle _signalHandler_visibleChanged;

        public static Handle Create(Object.Handle owner, SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            Object.Handle__Push(owner);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // SignalMask:
            Changed = 1 << 2,
            CheckableChanged = 1 << 3,
            EnabledChanged = 1 << 4,
            Hovered = 1 << 5,
            Toggled = 1 << 6,
            Triggered = 1 << 7,
            VisibleChanged = 1 << 8
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
            void Changed();
            void CheckableChanged(bool checkable);
            void EnabledChanged(bool enabled);
            void Hovered();
            void Toggled(bool checked_);
            void Triggered(bool checked_);
            void VisibleChanged();
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

            public void Changed()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_changed, Id);
            }

            public void CheckableChanged(bool checkable)
            {
                NativeImplClient.PushBool(checkable);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_checkableChanged, Id);
            }

            public void EnabledChanged(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_enabledChanged, Id);
            }

            public void Hovered()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_hovered, Id);
            }

            public void Toggled(bool checked_)
            {
                NativeImplClient.PushBool(checked_);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_toggled, Id);
            }

            public void Triggered(bool checked_)
            {
                NativeImplClient.PushBool(checked_);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_triggered, Id);
            }

            public void VisibleChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_visibleChanged, Id);
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
        public enum MenuRole
        {
            NoRole = 0,
            TextHeuristicRole,
            ApplicationSpecificRole,
            AboutQtRole,
            AboutRole,
            PreferencesRole,
            QuitRole
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void MenuRole__Push(MenuRole value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static MenuRole MenuRole__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (MenuRole)ret;
        }
        public enum Priority
        {
            LowPriority = 0,
            NormalPriority = 128,
            HighPriority = 256
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Priority__Push(Priority value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Priority Priority__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Priority)ret;
        }
        public class Handle : Object.Handle, IDisposable
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
            public void SetAutoRepeat(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoRepeat);
            }
            public void SetCheckable(bool checkable)
            {
                NativeImplClient.PushBool(checkable);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setCheckable);
            }
            public void SetChecked(bool checked_)
            {
                NativeImplClient.PushBool(checked_);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setChecked);
            }
            public void SetEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setEnabled);
            }
            public void SetIcon(Icon.Deferred icon)
            {
                Icon.Deferred__Push(icon, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIcon);
            }
            public void SetIconText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconText);
            }
            public void SetIconVisibleInMenu(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIconVisibleInMenu);
            }
            public void SetMenuRole(MenuRole role)
            {
                MenuRole__Push(role);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMenuRole);
            }
            public void SetPriority(Priority priority)
            {
                Priority__Push(priority);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPriority);
            }
            public void SetShortcut(KeySequence.Deferred shortcut)
            {
                KeySequence.Deferred__Push(shortcut, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setShortcut);
            }
            public void SetShortcutContext(ShortcutContext context)
            {
                ShortcutContext__Push(context);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setShortcutContext);
            }
            public void SetShortcutVisibleInContextMenu(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setShortcutVisibleInContextMenu);
            }
            public void SetStatusTip(string tip)
            {
                NativeImplClient.PushString(tip);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setStatusTip);
            }
            public void SetText(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setText);
            }
            public void SetToolTip(string tip)
            {
                NativeImplClient.PushString(tip);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setToolTip);
            }
            public void SetVisible(bool visible)
            {
                NativeImplClient.PushBool(visible);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setVisible);
            }
            public void SetWhatsThis(string text)
            {
                NativeImplClient.PushString(text);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWhatsThis);
            }
            public void SetSeparator(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSeparator);
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
            _module = NativeImplClient.GetModule("Action");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setAutoRepeat = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoRepeat");
            _handle_setCheckable = NativeImplClient.GetModuleMethod(_module, "Handle_setCheckable");
            _handle_setChecked = NativeImplClient.GetModuleMethod(_module, "Handle_setChecked");
            _handle_setEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setEnabled");
            _handle_setIcon = NativeImplClient.GetModuleMethod(_module, "Handle_setIcon");
            _handle_setIconText = NativeImplClient.GetModuleMethod(_module, "Handle_setIconText");
            _handle_setIconVisibleInMenu = NativeImplClient.GetModuleMethod(_module, "Handle_setIconVisibleInMenu");
            _handle_setMenuRole = NativeImplClient.GetModuleMethod(_module, "Handle_setMenuRole");
            _handle_setPriority = NativeImplClient.GetModuleMethod(_module, "Handle_setPriority");
            _handle_setShortcut = NativeImplClient.GetModuleMethod(_module, "Handle_setShortcut");
            _handle_setShortcutContext = NativeImplClient.GetModuleMethod(_module, "Handle_setShortcutContext");
            _handle_setShortcutVisibleInContextMenu = NativeImplClient.GetModuleMethod(_module, "Handle_setShortcutVisibleInContextMenu");
            _handle_setStatusTip = NativeImplClient.GetModuleMethod(_module, "Handle_setStatusTip");
            _handle_setText = NativeImplClient.GetModuleMethod(_module, "Handle_setText");
            _handle_setToolTip = NativeImplClient.GetModuleMethod(_module, "Handle_setToolTip");
            _handle_setVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setVisible");
            _handle_setWhatsThis = NativeImplClient.GetModuleMethod(_module, "Handle_setWhatsThis");
            _handle_setSeparator = NativeImplClient.GetModuleMethod(_module, "Handle_setSeparator");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_changed = NativeImplClient.GetInterfaceMethod(_signalHandler, "changed");
            _signalHandler_checkableChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "checkableChanged");
            _signalHandler_enabledChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "enabledChanged");
            _signalHandler_hovered = NativeImplClient.GetInterfaceMethod(_signalHandler, "hovered");
            _signalHandler_toggled = NativeImplClient.GetInterfaceMethod(_signalHandler, "toggled");
            _signalHandler_triggered = NativeImplClient.GetInterfaceMethod(_signalHandler, "triggered");
            _signalHandler_visibleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "visibleChanged");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_changed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Changed();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_checkableChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var checkable = NativeImplClient.PopBool();
                inst.CheckableChanged(checkable);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_enabledChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var enabled = NativeImplClient.PopBool();
                inst.EnabledChanged(enabled);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_hovered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Hovered();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_toggled, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var checked_ = NativeImplClient.PopBool();
                inst.Toggled(checked_);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_triggered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var checked_ = NativeImplClient.PopBool();
                inst.Triggered(checked_);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_visibleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.VisibleChanged();
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
