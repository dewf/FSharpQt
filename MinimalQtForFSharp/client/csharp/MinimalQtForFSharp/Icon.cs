using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Icon
    {
        private static ModuleHandle _module;
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
        public abstract record Deferred
        {
            internal abstract void Push(bool isReturn);
            internal static Deferred Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => Empty.PopDerived(),
                    1 => FromThemeIcon.PopDerived(),
                    2 => FromFilename.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record Empty : Deferred
            {
                internal override void Push(bool isReturn)
                {
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static Empty PopDerived()
                {
                    return new Empty();
                }
            }
            public sealed record FromThemeIcon(ThemeIcon ThemeIcon) : Deferred
            {
                public ThemeIcon ThemeIcon { get; } = ThemeIcon;
                internal override void Push(bool isReturn)
                {
                    ThemeIcon__Push(ThemeIcon);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromThemeIcon PopDerived()
                {
                    var themeIcon = ThemeIcon__Pop();
                    return new FromThemeIcon(themeIcon);
                }
            }
            public sealed record FromFilename(string Filename) : Deferred
            {
                public string Filename { get; } = Filename;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushString(Filename);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromFilename PopDerived()
                {
                    var filename = NativeImplClient.PopString();
                    return new FromFilename(filename);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Deferred__Push(Deferred thing, bool isReturn)
        {
            thing.Push(isReturn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Deferred Deferred__Pop()
        {
            return Deferred.Pop();
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Icon");
            // assign module handles

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
