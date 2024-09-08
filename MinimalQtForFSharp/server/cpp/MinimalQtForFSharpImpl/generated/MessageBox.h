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
#include "Enums.h"
using namespace ::Enums;
#include "Dialog.h"
using namespace ::Dialog;
#include "AbstractButton.h"
using namespace ::AbstractButton;
#include "Widget.h"
using namespace ::Widget;

namespace MessageBox
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
        ButtonClicked = 1 << 8
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
        virtual void buttonClicked(AbstractButton::HandleRef button) = 0;
    };

    enum class StandardButton {
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
    };

    typedef int32_t StandardButtonSet;
    enum StandardButtonSetFlags : int32_t {
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
    };

    enum class MessageBoxIcon {
        NoIcon = 0,
        Information = 1,
        Warning = 2,
        Critical = 3,
        Question = 4
    };

    typedef int32_t Options;
    enum OptionsFlags : int32_t {
        DontUseNativeDialog = 0x00000001
    };

    void Handle_setDetailedText(HandleRef _this, std::string text);
    void Handle_setIcon(HandleRef _this, MessageBoxIcon icon);
    void Handle_setInformativeText(HandleRef _this, std::string text);
    void Handle_setOptions(HandleRef _this, Options opts);
    void Handle_setStandardButtons(HandleRef _this, StandardButtonSet buttons);
    void Handle_setText(HandleRef _this, std::string text);
    void Handle_setTextFormat(HandleRef _this, Enums::TextFormat format);
    void Handle_setTextInteractionFlags(HandleRef _this, Enums::TextInteractionFlags tiFlags);
    void Handle_setDefaultButton(HandleRef _this, StandardButton button);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(Widget::HandleRef parent, std::shared_ptr<SignalHandler> handler);
}
