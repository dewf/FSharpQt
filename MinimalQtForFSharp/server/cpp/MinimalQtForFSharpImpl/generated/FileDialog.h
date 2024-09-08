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

#include "Object.h"
using namespace ::Object;
#include "Common.h"
using namespace ::Common;
#include "Icon.h"
using namespace ::Icon;
#include "Dialog.h"
using namespace ::Dialog;
#include "Widget.h"
using namespace ::Widget;

namespace FileDialog
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Dialog::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // Dialog::SignalMask:
        Accepted = 1 << 5,
        Finished = 1 << 6,
        Rejected = 1 << 7,
        // SignalMask:
        CurrentChanged = 1 << 8,
        CurrentUrlChanged = 1 << 9,
        DirectoryEntered = 1 << 10,
        DirectoryUrlEntered = 1 << 11,
        FileSelected = 1 << 12,
        FilesSelected = 1 << 13,
        FilterSelected = 1 << 14,
        UrlSelected = 1 << 15,
        UrlsSelected = 1 << 16
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void accepted() = 0;
        virtual void finished(int32_t result) = 0;
        virtual void rejected() = 0;
        virtual void currentChanged(std::string path) = 0;
        virtual void currentUrlChanged(std::string url) = 0;
        virtual void directoryEntered(std::string dir) = 0;
        virtual void directoryUrlEntered(std::string url) = 0;
        virtual void fileSelected(std::string file) = 0;
        virtual void filesSelected(std::vector<std::string> selected) = 0;
        virtual void filterSelected(std::string filter) = 0;
        virtual void urlSelected(std::string url) = 0;
        virtual void urlsSelected(std::vector<std::string> urls) = 0;
    };

    enum class FileMode {
        AnyFile,
        ExistingFile,
        Directory,
        ExistingFiles
    };

    enum class ViewMode {
        Detail,
        List
    };

    enum class AcceptMode {
        Open,
        Save
    };

    typedef int32_t Options;
    enum OptionsFlags : int32_t {
        ShowDirsOnly = 0x00000001,
        DontResolveSymlinks = 0x00000002,
        DontConfirmOverwrite = 0x00000004,
        DontUseNativeDialog = 0x00000008,
        ReadOnly = 0x00000010,
        HideNameFilterDetails = 0x00000020,
        DontUseCustomDirectoryIcons = 0x00000040
    };

    void Handle_setAcceptMode(HandleRef _this, AcceptMode mode);
    void Handle_setDefaultSuffix(HandleRef _this, std::string suffix);
    void Handle_setFileMode(HandleRef _this, FileMode mode);
    void Handle_setOptions(HandleRef _this, Options opts);
    void Handle_setSupportedSchemes(HandleRef _this, std::vector<std::string> schemes);
    void Handle_setViewMode(HandleRef _this, ViewMode mode);
    void Handle_setNameFilter(HandleRef _this, std::string filter);
    void Handle_setNameFilters(HandleRef _this, std::vector<std::string> filters);
    void Handle_setMimeTypeFilters(HandleRef _this, std::vector<std::string> filters);
    void Handle_setDirectory(HandleRef _this, std::string dir);
    void Handle_selectFile(HandleRef _this, std::string file);
    std::vector<std::string> Handle_selectedFiles(HandleRef _this);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(Widget::HandleRef parent, std::shared_ptr<SignalHandler> handler);
}
