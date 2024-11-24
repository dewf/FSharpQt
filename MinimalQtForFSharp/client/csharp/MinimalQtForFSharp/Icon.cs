using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Pixmap;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Icon
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _create_overload1;
        internal static ModuleMethodHandle _create_overload2;
        internal static ModuleMethodHandle _create_overload3;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Create()
        {
            NativeImplClient.InvokeModuleMethod(_create);
            return Owned__Pop();
        }

        public static Owned Create(ThemeIcon themeIcon)
        {
            ThemeIcon__Push(themeIcon);
            NativeImplClient.InvokeModuleMethod(_create_overload1);
            return Owned__Pop();
        }

        public static Owned Create(string filename)
        {
            NativeImplClient.PushString(filename);
            NativeImplClient.InvokeModuleMethod(_create_overload2);
            return Owned__Pop();
        }

        public static Owned Create(Pixmap.Handle pixmap)
        {
            Pixmap.Handle__Push(pixmap);
            NativeImplClient.InvokeModuleMethod(_create_overload3);
            return Owned__Pop();
        }
        public enum Mode
        {
            Normal,
            Disabled,
            Active,
            Selected
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Mode__Push(Mode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Mode Mode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Mode)ret;
        }
        public enum State
        {
            On,
            Off
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void State__Push(State value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static State State__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (State)ret;
        }
        public enum ThemeIcon
        {
            AddressBookNew,
            ApplicationExit,
            AppointmentNew,
            CallStart,
            CallStop,
            ContactNew,
            DocumentNew,
            DocumentOpen,
            DocumentOpenRecent,
            DocumentPageSetup,
            DocumentPrint,
            DocumentPrintPreview,
            DocumentProperties,
            DocumentRevert,
            DocumentSave,
            DocumentSaveAs,
            DocumentSend,
            EditClear,
            EditCopy,
            EditCut,
            EditDelete,
            EditFind,
            EditPaste,
            EditRedo,
            EditSelectAll,
            EditUndo,
            FolderNew,
            FormatIndentLess,
            FormatIndentMore,
            FormatJustifyCenter,
            FormatJustifyFill,
            FormatJustifyLeft,
            FormatJustifyRight,
            FormatTextDirectionLtr,
            FormatTextDirectionRtl,
            FormatTextBold,
            FormatTextItalic,
            FormatTextUnderline,
            FormatTextStrikethrough,
            GoDown,
            GoHome,
            GoNext,
            GoPrevious,
            GoUp,
            HelpAbout,
            HelpFaq,
            InsertImage,
            InsertLink,
            InsertText,
            ListAdd,
            ListRemove,
            MailForward,
            MailMarkImportant,
            MailMarkRead,
            MailMarkUnread,
            MailMessageNew,
            MailReplyAll,
            MailReplySender,
            MailSend,
            MediaEject,
            MediaPlaybackPause,
            MediaPlaybackStart,
            MediaPlaybackStop,
            MediaRecord,
            MediaSeekBackward,
            MediaSeekForward,
            MediaSkipBackward,
            MediaSkipForward,
            ObjectRotateLeft,
            ObjectRotateRight,
            ProcessStop,
            SystemLockScreen,
            SystemLogOut,
            SystemSearch,
            SystemReboot,
            SystemShutdown,
            ToolsCheckSpelling,
            ViewFullscreen,
            ViewRefresh,
            ViewRestore,
            WindowClose,
            WindowNew,
            ZoomFitBest,
            ZoomIn,
            ZoomOut,
            AudioCard,
            AudioInputMicrophone,
            Battery,
            CameraPhoto,
            CameraVideo,
            CameraWeb,
            Computer,
            DriveHarddisk,
            DriveOptical,
            InputGaming,
            InputKeyboard,
            InputMouse,
            InputTablet,
            MediaFlash,
            MediaOptical,
            MediaTape,
            MultimediaPlayer,
            NetworkWired,
            NetworkWireless,
            Phone,
            Printer,
            Scanner,
            VideoDisplay,
            AppointmentMissed,
            AppointmentSoon,
            AudioVolumeHigh,
            AudioVolumeLow,
            AudioVolumeMedium,
            AudioVolumeMuted,
            BatteryCaution,
            BatteryLow,
            DialogError,
            DialogInformation,
            DialogPassword,
            DialogQuestion,
            DialogWarning,
            FolderDragAccept,
            FolderOpen,
            FolderVisiting,
            ImageLoading,
            ImageMissing,
            MailAttachment,
            MailUnread,
            MailRead,
            MailReplied,
            MediaPlaylistRepeat,
            MediaPlaylistShuffle,
            NetworkOffline,
            PrinterPrinting,
            SecurityHigh,
            SecurityLow,
            SoftwareUpdateAvailable,
            SoftwareUpdateUrgent,
            SyncError,
            SyncSynchronizing,
            UserAvailable,
            UserOffline,
            WeatherClear,
            WeatherClearNight,
            WeatherFewClouds,
            WeatherFewCloudsNight,
            WeatherFog,
            WeatherShowers,
            WeatherSnow,
            WeatherStorm,
            NThemeIcons
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ThemeIcon__Push(ThemeIcon value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ThemeIcon ThemeIcon__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (ThemeIcon)ret;
        }
        public class Handle : IComparable
        {
            internal readonly IntPtr NativeHandle;
            internal Handle(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Handle other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
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
        public class Owned : Handle, IDisposable
        {
            protected bool _disposed;
            internal Owned(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public virtual void Dispose()
            {
                if (!_disposed)
                {
                    Owned__Push(this);
                    NativeImplClient.InvokeModuleMethod(_owned_dispose);
                    _disposed = true;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Owned__Push(Owned thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Owned Owned__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Owned(ptr) : null;
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Icon");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _create_overload1 = NativeImplClient.GetModuleMethod(_module, "create_overload1");
            _create_overload2 = NativeImplClient.GetModuleMethod(_module, "create_overload2");
            _create_overload3 = NativeImplClient.GetModuleMethod(_module, "create_overload3");
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
