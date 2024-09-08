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
using static Org.Whatever.MinimalQtForFSharp.Frame;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.Pixmap;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Label
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _createNoHandler;
        internal static ModuleMethodHandle _handle_setAlignment;
        internal static ModuleMethodHandle _handle_hasSelectedText;
        internal static ModuleMethodHandle _handle_setIndent;
        internal static ModuleMethodHandle _handle_setMargin;
        internal static ModuleMethodHandle _handle_setOpenExternalLinks;
        internal static ModuleMethodHandle _handle_setPixmap;
        internal static ModuleMethodHandle _handle_setScaledContents;
        internal static ModuleMethodHandle _handle_selectedText;
        internal static ModuleMethodHandle _handle_setText;
        internal static ModuleMethodHandle _handle_setTextFormat;
        internal static ModuleMethodHandle _handle_setTextInteractionFlags;
        internal static ModuleMethodHandle _handle_setWordWrap;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_linkActivated;
        internal static InterfaceMethodHandle _signalHandler_linkHovered;

        public static Handle Create(SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }

        public static Handle CreateNoHandler()
        {
            NativeImplClient.InvokeModuleMethod(_createNoHandler);
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
            // SignalMask:
            LinkActivated = 1 << 5,
            LinkHovered = 1 << 6
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
            void LinkActivated(string link);
            void LinkHovered(string link);
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

            public void LinkActivated(string link)
            {
                NativeImplClient.PushString(link);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_linkActivated, Id);
            }

            public void LinkHovered(string link)
            {
                NativeImplClient.PushString(link);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_linkHovered, Id);
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
        public class Handle : Frame.Handle
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
            public void SetAlignment(Alignment align)
            {
                Alignment__Push(align);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAlignment);
            }
            public bool HasSelectedText()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_hasSelectedText);
                return NativeImplClient.PopBool();
            }
            public void SetIndent(int indent)
            {
                NativeImplClient.PushInt32(indent);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setIndent);
            }
            public void SetMargin(int margin)
            {
                NativeImplClient.PushInt32(margin);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMargin);
            }
            public void SetOpenExternalLinks(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOpenExternalLinks);
            }
            public void SetPixmap(Pixmap.Deferred pixmap)
            {
                Pixmap.Deferred__Push(pixmap, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setPixmap);
            }
            public void SetScaledContents(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setScaledContents);
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
            public void SetTextFormat(TextFormat format)
            {
                TextFormat__Push(format);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextFormat);
            }
            public void SetTextInteractionFlags(TextInteractionFlags interactionFlags)
            {
                TextInteractionFlags__Push(interactionFlags);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTextInteractionFlags);
            }
            public void SetWordWrap(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWordWrap);
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
            _module = NativeImplClient.GetModule("Label");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _createNoHandler = NativeImplClient.GetModuleMethod(_module, "createNoHandler");
            _handle_setAlignment = NativeImplClient.GetModuleMethod(_module, "Handle_setAlignment");
            _handle_hasSelectedText = NativeImplClient.GetModuleMethod(_module, "Handle_hasSelectedText");
            _handle_setIndent = NativeImplClient.GetModuleMethod(_module, "Handle_setIndent");
            _handle_setMargin = NativeImplClient.GetModuleMethod(_module, "Handle_setMargin");
            _handle_setOpenExternalLinks = NativeImplClient.GetModuleMethod(_module, "Handle_setOpenExternalLinks");
            _handle_setPixmap = NativeImplClient.GetModuleMethod(_module, "Handle_setPixmap");
            _handle_setScaledContents = NativeImplClient.GetModuleMethod(_module, "Handle_setScaledContents");
            _handle_selectedText = NativeImplClient.GetModuleMethod(_module, "Handle_selectedText");
            _handle_setText = NativeImplClient.GetModuleMethod(_module, "Handle_setText");
            _handle_setTextFormat = NativeImplClient.GetModuleMethod(_module, "Handle_setTextFormat");
            _handle_setTextInteractionFlags = NativeImplClient.GetModuleMethod(_module, "Handle_setTextInteractionFlags");
            _handle_setWordWrap = NativeImplClient.GetModuleMethod(_module, "Handle_setWordWrap");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_linkActivated = NativeImplClient.GetInterfaceMethod(_signalHandler, "linkActivated");
            _signalHandler_linkHovered = NativeImplClient.GetInterfaceMethod(_signalHandler, "linkHovered");
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
            NativeImplClient.SetClientMethodWrapper(_signalHandler_linkActivated, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var link = NativeImplClient.PopString();
                inst.LinkActivated(link);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_linkHovered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var link = NativeImplClient.PopString();
                inst.LinkHovered(link);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
