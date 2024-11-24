#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

#include "Pixmap.h"
using namespace ::Pixmap;

namespace Icon
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    enum class Mode {
        Normal,
        Disabled,
        Active,
        Selected
    };

    enum class State {
        On,
        Off
    };

    enum class ThemeIcon {
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
    };


    void Owned_dispose(OwnedRef _this);
    OwnedRef create();
    OwnedRef create(ThemeIcon themeIcon);
    OwnedRef create(std::string filename);
    OwnedRef create(Pixmap::HandleRef pixmap);
}
