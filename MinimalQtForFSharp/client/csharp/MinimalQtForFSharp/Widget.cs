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
using static Org.Whatever.MinimalQtForFSharp.Layout;
using static Org.Whatever.MinimalQtForFSharp.Painter;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.Action;
using static Org.Whatever.MinimalQtForFSharp.Region;
using static Org.Whatever.MinimalQtForFSharp.Cursor;
using static Org.Whatever.MinimalQtForFSharp.SizePolicy;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Widget
    {
        private static ModuleHandle _module;

        // built-in array type: string[]
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _createNoHandler;
        internal static ModuleMethodHandle _createMimeData;
        internal static ModuleMethodHandle _createDrag;
        internal static ModuleMethodHandle _createSubclassed;
        internal static ModuleMethodHandle _handle_setAcceptDrops;
        internal static ModuleMethodHandle _handle_setAccessibleDescription;
        internal static ModuleMethodHandle _handle_setAccessibleName;
        internal static ModuleMethodHandle _handle_setAutoFillBackground;
        internal static ModuleMethodHandle _handle_setBaseSize;
        internal static ModuleMethodHandle _handle_childrenRect;
        internal static ModuleMethodHandle _handle_childrenRegion;
        internal static ModuleMethodHandle _handle_setContextMenuPolicy;
        internal static ModuleMethodHandle _handle_getCursor;
        internal static ModuleMethodHandle _handle_setEnabled;
        internal static ModuleMethodHandle _handle_hasFocus;
        internal static ModuleMethodHandle _handle_setFocusPolicy;
        internal static ModuleMethodHandle _handle_frameGeometry;
        internal static ModuleMethodHandle _handle_frameSize;
        internal static ModuleMethodHandle _handle_isFullscreen;
        internal static ModuleMethodHandle _handle_setGeometry;
        internal static ModuleMethodHandle _handle_setGeometry_overload1;
        internal static ModuleMethodHandle _handle_height;
        internal static ModuleMethodHandle _handle_setInputMethodHints;
        internal static ModuleMethodHandle _handle_isActiveWindow;
        internal static ModuleMethodHandle _handle_setLayoutDirection;
        internal static ModuleMethodHandle _handle_isMaximized;
        internal static ModuleMethodHandle _handle_setMaximumHeight;
        internal static ModuleMethodHandle _handle_setMaximumWidth;
        internal static ModuleMethodHandle _handle_setMaximumSize;
        internal static ModuleMethodHandle _handle_isMinimized;
        internal static ModuleMethodHandle _handle_setMinimumHeight;
        internal static ModuleMethodHandle _handle_setMinimumSize;
        internal static ModuleMethodHandle _handle_minimumSizeHint;
        internal static ModuleMethodHandle _handle_setMinimumWidth;
        internal static ModuleMethodHandle _handle_isModal;
        internal static ModuleMethodHandle _handle_setMouseTracking;
        internal static ModuleMethodHandle _handle_normalGeometry;
        internal static ModuleMethodHandle _handle_move;
        internal static ModuleMethodHandle _handle_move_overload1;
        internal static ModuleMethodHandle _handle_rect;
        internal static ModuleMethodHandle _handle_resize;
        internal static ModuleMethodHandle _handle_resize_overload1;
        internal static ModuleMethodHandle _handle_sizeHint;
        internal static ModuleMethodHandle _handle_setSizeIncrement;
        internal static ModuleMethodHandle _handle_setSizeIncrement_overload1;
        internal static ModuleMethodHandle _handle_setSizePolicy;
        internal static ModuleMethodHandle _handle_setSizePolicy_overload1;
        internal static ModuleMethodHandle _handle_setStatusTip;
        internal static ModuleMethodHandle _handle_setStyleSheet;
        internal static ModuleMethodHandle _handle_setTabletTracking;
        internal static ModuleMethodHandle _handle_setToolTip;
        internal static ModuleMethodHandle _handle_setToolTipDuration;
        internal static ModuleMethodHandle _handle_setUpdatesEnabled;
        internal static ModuleMethodHandle _handle_setVisible;
        internal static ModuleMethodHandle _handle_setWhatsThis;
        internal static ModuleMethodHandle _handle_width;
        internal static ModuleMethodHandle _handle_setWindowFilePath;
        internal static ModuleMethodHandle _handle_setWindowFlags;
        internal static ModuleMethodHandle _handle_setWindowIcon;
        internal static ModuleMethodHandle _handle_setWindowModality;
        internal static ModuleMethodHandle _handle_setWindowModified;
        internal static ModuleMethodHandle _handle_setWindowOpacity;
        internal static ModuleMethodHandle _handle_setWindowTitle;
        internal static ModuleMethodHandle _handle_x;
        internal static ModuleMethodHandle _handle_y;
        internal static ModuleMethodHandle _handle_addAction;
        internal static ModuleMethodHandle _handle_setParent;
        internal static ModuleMethodHandle _handle_getWindow;
        internal static ModuleMethodHandle _handle_updateGeometry;
        internal static ModuleMethodHandle _handle_adjustSize;
        internal static ModuleMethodHandle _handle_setFixedWidth;
        internal static ModuleMethodHandle _handle_setFixedHeight;
        internal static ModuleMethodHandle _handle_setFixedSize;
        internal static ModuleMethodHandle _handle_setFixedSize_overload1;
        internal static ModuleMethodHandle _handle_show;
        internal static ModuleMethodHandle _handle_hide;
        internal static ModuleMethodHandle _handle_update;
        internal static ModuleMethodHandle _handle_update_overload1;
        internal static ModuleMethodHandle _handle_update_overload2;
        internal static ModuleMethodHandle _handle_setLayout;
        internal static ModuleMethodHandle _handle_getLayout;
        internal static ModuleMethodHandle _handle_mapToGlobal;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static ModuleMethodHandle _event_accept;
        internal static ModuleMethodHandle _event_ignore;
        internal static ModuleMethodHandle _mimeData_formats;
        internal static ModuleMethodHandle _mimeData_hasFormat;
        internal static ModuleMethodHandle _mimeData_text;
        internal static ModuleMethodHandle _mimeData_setText;
        internal static ModuleMethodHandle _mimeData_urls;
        internal static ModuleMethodHandle _mimeData_setUrls;
        internal static ModuleMethodHandle _drag_setMimeData;
        internal static ModuleMethodHandle _drag_exec;
        internal static ModuleMethodHandle _dragMoveEvent_proposedAction;
        internal static ModuleMethodHandle _dragMoveEvent_acceptProposedAction;
        internal static ModuleMethodHandle _dragMoveEvent_possibleActions;
        internal static ModuleMethodHandle _dragMoveEvent_acceptDropAction;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceHandle _methodDelegate;
        internal static InterfaceMethodHandle _methodDelegate_showEvent;
        internal static InterfaceMethodHandle _methodDelegate_paintEvent;
        internal static InterfaceMethodHandle _methodDelegate_mousePressEvent;
        internal static InterfaceMethodHandle _methodDelegate_mouseMoveEvent;
        internal static InterfaceMethodHandle _methodDelegate_mouseReleaseEvent;
        internal static InterfaceMethodHandle _methodDelegate_enterEvent;
        internal static InterfaceMethodHandle _methodDelegate_leaveEvent;
        internal static InterfaceMethodHandle _methodDelegate_sizeHint;
        internal static InterfaceMethodHandle _methodDelegate_resizeEvent;
        internal static InterfaceMethodHandle _methodDelegate_dragMoveEvent;
        internal static InterfaceMethodHandle _methodDelegate_dragLeaveEvent;
        internal static InterfaceMethodHandle _methodDelegate_dropEvent;
        public static int WIDGET_SIZE_MAX { get; internal set; }

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

        public static MimeData CreateMimeData()
        {
            NativeImplClient.InvokeModuleMethod(_createMimeData);
            return MimeData__Pop();
        }

        public static Drag CreateDrag(Handle parent)
        {
            Handle__Push(parent);
            NativeImplClient.InvokeModuleMethod(_createDrag);
            return Drag__Pop();
        }

        public static Handle CreateSubclassed(MethodDelegate methodDelegate, MethodMask methodMask, SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            MethodMask__Push(methodMask);
            MethodDelegate__Push(methodDelegate, false);
            NativeImplClient.InvokeModuleMethod(_createSubclassed);
            return Handle__Pop();
        }
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // SignalMask:
            CustomContextMenuRequested = 1 << 2,
            WindowIconChanged = 1 << 3,
            WindowTitleChanged = 1 << 4
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
            public void SetAcceptDrops(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAcceptDrops);
            }
            public void SetAccessibleDescription(string desc)
            {
                NativeImplClient.PushString(desc);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAccessibleDescription);
            }
            public void SetAccessibleName(string name)
            {
                NativeImplClient.PushString(name);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAccessibleName);
            }
            public void SetAutoFillBackground(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoFillBackground);
            }
            public void SetBaseSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setBaseSize);
            }
            public Rect ChildrenRect()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_childrenRect);
                return Rect__Pop();
            }
            public Region.Owned ChildrenRegion()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_childrenRegion);
                return Region.Owned__Pop();
            }
            public void SetContextMenuPolicy(ContextMenuPolicy policy)
            {
                ContextMenuPolicy__Push(policy);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setContextMenuPolicy);
            }
            public Cursor.Owned GetCursor()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_getCursor);
                return Cursor.Owned__Pop();
            }
            public void SetEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setEnabled);
            }
            public bool HasFocus()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_hasFocus);
                return NativeImplClient.PopBool();
            }
            public void SetFocusPolicy(FocusPolicy policy)
            {
                FocusPolicy__Push(policy);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFocusPolicy);
            }
            public Rect FrameGeometry()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_frameGeometry);
                return Rect__Pop();
            }
            public Size FrameSize()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_frameSize);
                return Size__Pop();
            }
            public bool IsFullscreen()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isFullscreen);
                return NativeImplClient.PopBool();
            }
            public void SetGeometry(Rect rect)
            {
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setGeometry);
            }
            public void SetGeometry(int x, int y, int width, int height)
            {
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setGeometry_overload1);
            }
            public int Height()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_height);
                return NativeImplClient.PopInt32();
            }
            public void SetInputMethodHints(InputMethodHints hints)
            {
                InputMethodHints__Push(hints);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setInputMethodHints);
            }
            public bool IsActiveWindow()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isActiveWindow);
                return NativeImplClient.PopBool();
            }
            public void SetLayoutDirection(LayoutDirection direction)
            {
                LayoutDirection__Push(direction);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setLayoutDirection);
            }
            public bool IsMaximized()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isMaximized);
                return NativeImplClient.PopBool();
            }
            public void SetMaximumHeight(int height)
            {
                NativeImplClient.PushInt32(height);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximumHeight);
            }
            public void SetMaximumWidth(int width)
            {
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximumWidth);
            }
            public void SetMaximumSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMaximumSize);
            }
            public bool IsMinimized()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isMinimized);
                return NativeImplClient.PopBool();
            }
            public void SetMinimumHeight(int height)
            {
                NativeImplClient.PushInt32(height);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimumHeight);
            }
            public void SetMinimumSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimumSize);
            }
            public Size MinimumSizeHint()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_minimumSizeHint);
                return Size__Pop();
            }
            public void SetMinimumWidth(int width)
            {
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMinimumWidth);
            }
            public bool IsModal()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_isModal);
                return NativeImplClient.PopBool();
            }
            public void SetMouseTracking(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMouseTracking);
            }
            public Rect NormalGeometry()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_normalGeometry);
                return Rect__Pop();
            }
            public void Move(Point p)
            {
                Point__Push(p, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_move);
            }
            public void Move(int x, int y)
            {
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_move_overload1);
            }
            public Rect Rect()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_rect);
                return Rect__Pop();
            }
            public void Resize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_resize);
            }
            public void Resize(int width, int height)
            {
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_resize_overload1);
            }
            public Size SizeHint()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_sizeHint);
                return Size__Pop();
            }
            public void SetSizeIncrement(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizeIncrement);
            }
            public void SetSizeIncrement(int w, int h)
            {
                NativeImplClient.PushInt32(h);
                NativeImplClient.PushInt32(w);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizeIncrement_overload1);
            }
            public void SetSizePolicy(SizePolicy.Deferred policy)
            {
                SizePolicy.Deferred__Push(policy, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizePolicy);
            }
            public void SetSizePolicy(Policy hPolicy, Policy vPolicy)
            {
                Policy__Push(vPolicy);
                Policy__Push(hPolicy);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSizePolicy_overload1);
            }
            public void SetStatusTip(string tip)
            {
                NativeImplClient.PushString(tip);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setStatusTip);
            }
            public void SetStyleSheet(string styles)
            {
                NativeImplClient.PushString(styles);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setStyleSheet);
            }
            public void SetTabletTracking(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setTabletTracking);
            }
            public void SetToolTip(string tip)
            {
                NativeImplClient.PushString(tip);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setToolTip);
            }
            public void SetToolTipDuration(int duration)
            {
                NativeImplClient.PushInt32(duration);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setToolTipDuration);
            }
            public void SetUpdatesEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setUpdatesEnabled);
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
            public int Width()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_width);
                return NativeImplClient.PopInt32();
            }
            public void SetWindowFilePath(string path)
            {
                NativeImplClient.PushString(path);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowFilePath);
            }
            public void SetWindowFlags(WindowFlags flags_)
            {
                WindowFlags__Push(flags_);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowFlags);
            }
            public void SetWindowIcon(Icon.Handle icon)
            {
                Icon.Handle__Push(icon);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowIcon);
            }
            public void SetWindowModality(WindowModality modality)
            {
                WindowModality__Push(modality);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowModality);
            }
            public void SetWindowModified(bool modified)
            {
                NativeImplClient.PushBool(modified);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowModified);
            }
            public void SetWindowOpacity(double opacity)
            {
                NativeImplClient.PushDouble(opacity);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowOpacity);
            }
            public void SetWindowTitle(string title)
            {
                NativeImplClient.PushString(title);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setWindowTitle);
            }
            public int X()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_x);
                return NativeImplClient.PopInt32();
            }
            public int Y()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_y);
                return NativeImplClient.PopInt32();
            }
            public void AddAction(Action.Handle action)
            {
                Action.Handle__Push(action);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_addAction);
            }
            public void SetParent(Handle parent)
            {
                Handle__Push(parent);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setParent);
            }
            public Handle GetWindow()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_getWindow);
                return Handle__Pop();
            }
            public void UpdateGeometry()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_updateGeometry);
            }
            public void AdjustSize()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_adjustSize);
            }
            public void SetFixedWidth(int width)
            {
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFixedWidth);
            }
            public void SetFixedHeight(int height)
            {
                NativeImplClient.PushInt32(height);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFixedHeight);
            }
            public void SetFixedSize(Size size)
            {
                Size__Push(size, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFixedSize);
            }
            public void SetFixedSize(int width, int height)
            {
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFixedSize_overload1);
            }
            public void Show()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_show);
            }
            public void Hide()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_hide);
            }
            public void Update()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_update);
            }
            public void Update(int x, int y, int width, int height)
            {
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                NativeImplClient.PushInt32(y);
                NativeImplClient.PushInt32(x);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_update_overload1);
            }
            public void Update(Rect rect)
            {
                Rect__Push(rect, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_update_overload2);
            }
            public void SetLayout(Layout.Handle layout)
            {
                Layout.Handle__Push(layout);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setLayout);
            }
            public Layout.Handle GetLayout()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_getLayout);
                return Layout.Handle__Pop();
            }
            public Point MapToGlobal(Point p)
            {
                Point__Push(p, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_mapToGlobal);
                return Point__Pop();
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
        public class Event : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Event(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Event other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public void Accept()
            {
                Event__Push(this);
                NativeImplClient.InvokeModuleMethod(_event_accept);
            }
            public void Ignore()
            {
                Event__Push(this);
                NativeImplClient.InvokeModuleMethod(_event_ignore);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Event__Push(Event thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Event Event__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Event(ptr) : null;
        }
        public class MimeData : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal MimeData(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is MimeData other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public string[] Formats()
            {
                MimeData__Push(this);
                NativeImplClient.InvokeModuleMethod(_mimeData_formats);
                return NativeImplClient.PopStringArray();
            }
            public bool HasFormat(string mimeType)
            {
                NativeImplClient.PushString(mimeType);
                MimeData__Push(this);
                NativeImplClient.InvokeModuleMethod(_mimeData_hasFormat);
                return NativeImplClient.PopBool();
            }
            public string Text()
            {
                MimeData__Push(this);
                NativeImplClient.InvokeModuleMethod(_mimeData_text);
                return NativeImplClient.PopString();
            }
            public void SetText(string text)
            {
                NativeImplClient.PushString(text);
                MimeData__Push(this);
                NativeImplClient.InvokeModuleMethod(_mimeData_setText);
            }
            public string[] Urls()
            {
                MimeData__Push(this);
                NativeImplClient.InvokeModuleMethod(_mimeData_urls);
                return NativeImplClient.PopStringArray();
            }
            public void SetUrls(string[] urls)
            {
                NativeImplClient.PushStringArray(urls);
                MimeData__Push(this);
                NativeImplClient.InvokeModuleMethod(_mimeData_setUrls);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void MimeData__Push(MimeData thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static MimeData MimeData__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new MimeData(ptr) : null;
        }
        public class Drag : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Drag(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Drag other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public void SetMimeData(MimeData data)
            {
                MimeData__Push(data);
                Drag__Push(this);
                NativeImplClient.InvokeModuleMethod(_drag_setMimeData);
            }
            public DropAction Exec(DropActionSet supportedActions, DropAction defaultAction)
            {
                DropAction__Push(defaultAction);
                DropActionSet__Push(supportedActions);
                Drag__Push(this);
                NativeImplClient.InvokeModuleMethod(_drag_exec);
                return DropAction__Pop();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Drag__Push(Drag thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Drag Drag__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Drag(ptr) : null;
        }
        public class DragMoveEvent : Event
        {
            internal DragMoveEvent(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public DropAction ProposedAction()
            {
                DragMoveEvent__Push(this);
                NativeImplClient.InvokeModuleMethod(_dragMoveEvent_proposedAction);
                return DropAction__Pop();
            }
            public void AcceptProposedAction()
            {
                DragMoveEvent__Push(this);
                NativeImplClient.InvokeModuleMethod(_dragMoveEvent_acceptProposedAction);
            }
            public DropActionSet PossibleActions()
            {
                DragMoveEvent__Push(this);
                NativeImplClient.InvokeModuleMethod(_dragMoveEvent_possibleActions);
                return DropActionSet__Pop();
            }
            public void AcceptDropAction(DropAction action)
            {
                DropAction__Push(action);
                DragMoveEvent__Push(this);
                NativeImplClient.InvokeModuleMethod(_dragMoveEvent_acceptDropAction);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DragMoveEvent__Push(DragMoveEvent thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static DragMoveEvent DragMoveEvent__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new DragMoveEvent(ptr) : null;
        }
        [Flags]
        public enum MethodMask
        {
            // MethodMask:
            ShowEvent = 1 << 0,
            PaintEvent = 1 << 1,
            MousePressEvent = 1 << 2,
            MouseMoveEvent = 1 << 3,
            MouseReleaseEvent = 1 << 4,
            EnterEvent = 1 << 5,
            LeaveEvent = 1 << 6,
            SizeHint = 1 << 7,
            ResizeEvent = 1 << 8,
            DropEvents = 1 << 9
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
            void ShowEvent(bool isSpontaneous);
            void PaintEvent(Painter.Handle painter, Rect updateRect);
            void MousePressEvent(Point pos, MouseButton button, Modifiers modifiers);
            void MouseMoveEvent(Point pos, MouseButtonSet buttons, Modifiers modifiers);
            void MouseReleaseEvent(Point pos, MouseButton button, Modifiers modifiers);
            void EnterEvent(Point pos);
            void LeaveEvent();
            Size SizeHint();
            void ResizeEvent(Size oldSize, Size newSize);
            void DragMoveEvent(Point pos, Modifiers modifiers, MimeData mimeData, DragMoveEvent moveEvent, bool isEnterEvent);
            void DragLeaveEvent();
            void DropEvent(Point pos, Modifiers modifiers, MimeData mimeData, DropAction action);
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

            public void ShowEvent(bool isSpontaneous)
            {
                NativeImplClient.PushBool(isSpontaneous);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_showEvent, Id);
            }

            public void PaintEvent(Painter.Handle painter, Rect updateRect)
            {
                Rect__Push(updateRect, false);
                Painter.Handle__Push(painter);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_paintEvent, Id);
            }

            public void MousePressEvent(Point pos, MouseButton button, Modifiers modifiers)
            {
                Modifiers__Push(modifiers);
                MouseButton__Push(button);
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_mousePressEvent, Id);
            }

            public void MouseMoveEvent(Point pos, MouseButtonSet buttons, Modifiers modifiers)
            {
                Modifiers__Push(modifiers);
                MouseButtonSet__Push(buttons);
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_mouseMoveEvent, Id);
            }

            public void MouseReleaseEvent(Point pos, MouseButton button, Modifiers modifiers)
            {
                Modifiers__Push(modifiers);
                MouseButton__Push(button);
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_mouseReleaseEvent, Id);
            }

            public void EnterEvent(Point pos)
            {
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_enterEvent, Id);
            }

            public void LeaveEvent()
            {
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_leaveEvent, Id);
            }

            public Size SizeHint()
            {
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_sizeHint, Id);
                return Size__Pop();
            }

            public void ResizeEvent(Size oldSize, Size newSize)
            {
                Size__Push(newSize, false);
                Size__Push(oldSize, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_resizeEvent, Id);
            }

            public void DragMoveEvent(Point pos, Modifiers modifiers, MimeData mimeData, DragMoveEvent moveEvent, bool isEnterEvent)
            {
                NativeImplClient.PushBool(isEnterEvent);
                DragMoveEvent__Push(moveEvent);
                MimeData__Push(mimeData);
                Modifiers__Push(modifiers);
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_dragMoveEvent, Id);
            }

            public void DragLeaveEvent()
            {
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_dragLeaveEvent, Id);
            }

            public void DropEvent(Point pos, Modifiers modifiers, MimeData mimeData, DropAction action)
            {
                DropAction__Push(action);
                MimeData__Push(mimeData);
                Modifiers__Push(modifiers);
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_dropEvent, Id);
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
            _module = NativeImplClient.GetModule("Widget");
            // assign module constants
            NativeImplClient.PushModuleConstants(_module);
            WIDGET_SIZE_MAX = NativeImplClient.PopInt32();
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _createNoHandler = NativeImplClient.GetModuleMethod(_module, "createNoHandler");
            _createMimeData = NativeImplClient.GetModuleMethod(_module, "createMimeData");
            _createDrag = NativeImplClient.GetModuleMethod(_module, "createDrag");
            _createSubclassed = NativeImplClient.GetModuleMethod(_module, "createSubclassed");
            _handle_setAcceptDrops = NativeImplClient.GetModuleMethod(_module, "Handle_setAcceptDrops");
            _handle_setAccessibleDescription = NativeImplClient.GetModuleMethod(_module, "Handle_setAccessibleDescription");
            _handle_setAccessibleName = NativeImplClient.GetModuleMethod(_module, "Handle_setAccessibleName");
            _handle_setAutoFillBackground = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoFillBackground");
            _handle_setBaseSize = NativeImplClient.GetModuleMethod(_module, "Handle_setBaseSize");
            _handle_childrenRect = NativeImplClient.GetModuleMethod(_module, "Handle_childrenRect");
            _handle_childrenRegion = NativeImplClient.GetModuleMethod(_module, "Handle_childrenRegion");
            _handle_setContextMenuPolicy = NativeImplClient.GetModuleMethod(_module, "Handle_setContextMenuPolicy");
            _handle_getCursor = NativeImplClient.GetModuleMethod(_module, "Handle_getCursor");
            _handle_setEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setEnabled");
            _handle_hasFocus = NativeImplClient.GetModuleMethod(_module, "Handle_hasFocus");
            _handle_setFocusPolicy = NativeImplClient.GetModuleMethod(_module, "Handle_setFocusPolicy");
            _handle_frameGeometry = NativeImplClient.GetModuleMethod(_module, "Handle_frameGeometry");
            _handle_frameSize = NativeImplClient.GetModuleMethod(_module, "Handle_frameSize");
            _handle_isFullscreen = NativeImplClient.GetModuleMethod(_module, "Handle_isFullscreen");
            _handle_setGeometry = NativeImplClient.GetModuleMethod(_module, "Handle_setGeometry");
            _handle_setGeometry_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_setGeometry_overload1");
            _handle_height = NativeImplClient.GetModuleMethod(_module, "Handle_height");
            _handle_setInputMethodHints = NativeImplClient.GetModuleMethod(_module, "Handle_setInputMethodHints");
            _handle_isActiveWindow = NativeImplClient.GetModuleMethod(_module, "Handle_isActiveWindow");
            _handle_setLayoutDirection = NativeImplClient.GetModuleMethod(_module, "Handle_setLayoutDirection");
            _handle_isMaximized = NativeImplClient.GetModuleMethod(_module, "Handle_isMaximized");
            _handle_setMaximumHeight = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximumHeight");
            _handle_setMaximumWidth = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximumWidth");
            _handle_setMaximumSize = NativeImplClient.GetModuleMethod(_module, "Handle_setMaximumSize");
            _handle_isMinimized = NativeImplClient.GetModuleMethod(_module, "Handle_isMinimized");
            _handle_setMinimumHeight = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimumHeight");
            _handle_setMinimumSize = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimumSize");
            _handle_minimumSizeHint = NativeImplClient.GetModuleMethod(_module, "Handle_minimumSizeHint");
            _handle_setMinimumWidth = NativeImplClient.GetModuleMethod(_module, "Handle_setMinimumWidth");
            _handle_isModal = NativeImplClient.GetModuleMethod(_module, "Handle_isModal");
            _handle_setMouseTracking = NativeImplClient.GetModuleMethod(_module, "Handle_setMouseTracking");
            _handle_normalGeometry = NativeImplClient.GetModuleMethod(_module, "Handle_normalGeometry");
            _handle_move = NativeImplClient.GetModuleMethod(_module, "Handle_move");
            _handle_move_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_move_overload1");
            _handle_rect = NativeImplClient.GetModuleMethod(_module, "Handle_rect");
            _handle_resize = NativeImplClient.GetModuleMethod(_module, "Handle_resize");
            _handle_resize_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_resize_overload1");
            _handle_sizeHint = NativeImplClient.GetModuleMethod(_module, "Handle_sizeHint");
            _handle_setSizeIncrement = NativeImplClient.GetModuleMethod(_module, "Handle_setSizeIncrement");
            _handle_setSizeIncrement_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_setSizeIncrement_overload1");
            _handle_setSizePolicy = NativeImplClient.GetModuleMethod(_module, "Handle_setSizePolicy");
            _handle_setSizePolicy_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_setSizePolicy_overload1");
            _handle_setStatusTip = NativeImplClient.GetModuleMethod(_module, "Handle_setStatusTip");
            _handle_setStyleSheet = NativeImplClient.GetModuleMethod(_module, "Handle_setStyleSheet");
            _handle_setTabletTracking = NativeImplClient.GetModuleMethod(_module, "Handle_setTabletTracking");
            _handle_setToolTip = NativeImplClient.GetModuleMethod(_module, "Handle_setToolTip");
            _handle_setToolTipDuration = NativeImplClient.GetModuleMethod(_module, "Handle_setToolTipDuration");
            _handle_setUpdatesEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setUpdatesEnabled");
            _handle_setVisible = NativeImplClient.GetModuleMethod(_module, "Handle_setVisible");
            _handle_setWhatsThis = NativeImplClient.GetModuleMethod(_module, "Handle_setWhatsThis");
            _handle_width = NativeImplClient.GetModuleMethod(_module, "Handle_width");
            _handle_setWindowFilePath = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowFilePath");
            _handle_setWindowFlags = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowFlags");
            _handle_setWindowIcon = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowIcon");
            _handle_setWindowModality = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowModality");
            _handle_setWindowModified = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowModified");
            _handle_setWindowOpacity = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowOpacity");
            _handle_setWindowTitle = NativeImplClient.GetModuleMethod(_module, "Handle_setWindowTitle");
            _handle_x = NativeImplClient.GetModuleMethod(_module, "Handle_x");
            _handle_y = NativeImplClient.GetModuleMethod(_module, "Handle_y");
            _handle_addAction = NativeImplClient.GetModuleMethod(_module, "Handle_addAction");
            _handle_setParent = NativeImplClient.GetModuleMethod(_module, "Handle_setParent");
            _handle_getWindow = NativeImplClient.GetModuleMethod(_module, "Handle_getWindow");
            _handle_updateGeometry = NativeImplClient.GetModuleMethod(_module, "Handle_updateGeometry");
            _handle_adjustSize = NativeImplClient.GetModuleMethod(_module, "Handle_adjustSize");
            _handle_setFixedWidth = NativeImplClient.GetModuleMethod(_module, "Handle_setFixedWidth");
            _handle_setFixedHeight = NativeImplClient.GetModuleMethod(_module, "Handle_setFixedHeight");
            _handle_setFixedSize = NativeImplClient.GetModuleMethod(_module, "Handle_setFixedSize");
            _handle_setFixedSize_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_setFixedSize_overload1");
            _handle_show = NativeImplClient.GetModuleMethod(_module, "Handle_show");
            _handle_hide = NativeImplClient.GetModuleMethod(_module, "Handle_hide");
            _handle_update = NativeImplClient.GetModuleMethod(_module, "Handle_update");
            _handle_update_overload1 = NativeImplClient.GetModuleMethod(_module, "Handle_update_overload1");
            _handle_update_overload2 = NativeImplClient.GetModuleMethod(_module, "Handle_update_overload2");
            _handle_setLayout = NativeImplClient.GetModuleMethod(_module, "Handle_setLayout");
            _handle_getLayout = NativeImplClient.GetModuleMethod(_module, "Handle_getLayout");
            _handle_mapToGlobal = NativeImplClient.GetModuleMethod(_module, "Handle_mapToGlobal");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _event_accept = NativeImplClient.GetModuleMethod(_module, "Event_accept");
            _event_ignore = NativeImplClient.GetModuleMethod(_module, "Event_ignore");
            _mimeData_formats = NativeImplClient.GetModuleMethod(_module, "MimeData_formats");
            _mimeData_hasFormat = NativeImplClient.GetModuleMethod(_module, "MimeData_hasFormat");
            _mimeData_text = NativeImplClient.GetModuleMethod(_module, "MimeData_text");
            _mimeData_setText = NativeImplClient.GetModuleMethod(_module, "MimeData_setText");
            _mimeData_urls = NativeImplClient.GetModuleMethod(_module, "MimeData_urls");
            _mimeData_setUrls = NativeImplClient.GetModuleMethod(_module, "MimeData_setUrls");
            _drag_setMimeData = NativeImplClient.GetModuleMethod(_module, "Drag_setMimeData");
            _drag_exec = NativeImplClient.GetModuleMethod(_module, "Drag_exec");
            _dragMoveEvent_proposedAction = NativeImplClient.GetModuleMethod(_module, "DragMoveEvent_proposedAction");
            _dragMoveEvent_acceptProposedAction = NativeImplClient.GetModuleMethod(_module, "DragMoveEvent_acceptProposedAction");
            _dragMoveEvent_possibleActions = NativeImplClient.GetModuleMethod(_module, "DragMoveEvent_possibleActions");
            _dragMoveEvent_acceptDropAction = NativeImplClient.GetModuleMethod(_module, "DragMoveEvent_acceptDropAction");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
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
            _methodDelegate = NativeImplClient.GetInterface(_module, "MethodDelegate");
            _methodDelegate_showEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "showEvent");
            _methodDelegate_paintEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "paintEvent");
            _methodDelegate_mousePressEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "mousePressEvent");
            _methodDelegate_mouseMoveEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "mouseMoveEvent");
            _methodDelegate_mouseReleaseEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "mouseReleaseEvent");
            _methodDelegate_enterEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "enterEvent");
            _methodDelegate_leaveEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "leaveEvent");
            _methodDelegate_sizeHint = NativeImplClient.GetInterfaceMethod(_methodDelegate, "sizeHint");
            _methodDelegate_resizeEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "resizeEvent");
            _methodDelegate_dragMoveEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "dragMoveEvent");
            _methodDelegate_dragLeaveEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "dragLeaveEvent");
            _methodDelegate_dropEvent = NativeImplClient.GetInterfaceMethod(_methodDelegate, "dropEvent");
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_showEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var isSpontaneous = NativeImplClient.PopBool();
                inst.ShowEvent(isSpontaneous);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_paintEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var painter = Painter.Handle__Pop();
                var updateRect = Rect__Pop();
                inst.PaintEvent(painter, updateRect);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_mousePressEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                var button = MouseButton__Pop();
                var modifiers = Modifiers__Pop();
                inst.MousePressEvent(pos, button, modifiers);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_mouseMoveEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                var buttons = MouseButtonSet__Pop();
                var modifiers = Modifiers__Pop();
                inst.MouseMoveEvent(pos, buttons, modifiers);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_mouseReleaseEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                var button = MouseButton__Pop();
                var modifiers = Modifiers__Pop();
                inst.MouseReleaseEvent(pos, button, modifiers);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_enterEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                inst.EnterEvent(pos);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_leaveEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                inst.LeaveEvent();
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_sizeHint, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                Size__Push(inst.SizeHint(), true);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_resizeEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var oldSize = Size__Pop();
                var newSize = Size__Pop();
                inst.ResizeEvent(oldSize, newSize);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_dragMoveEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                var modifiers = Modifiers__Pop();
                var mimeData = MimeData__Pop();
                var moveEvent = DragMoveEvent__Pop();
                var isEnterEvent = NativeImplClient.PopBool();
                inst.DragMoveEvent(pos, modifiers, mimeData, moveEvent, isEnterEvent);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_dragLeaveEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                inst.DragLeaveEvent();
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_dropEvent, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                var modifiers = Modifiers__Pop();
                var mimeData = MimeData__Pop();
                var action = DropAction__Pop();
                inst.DropEvent(pos, modifiers, mimeData, action);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
