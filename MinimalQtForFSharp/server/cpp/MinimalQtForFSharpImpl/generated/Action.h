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
#include "KeySequence.h"
using namespace ::KeySequence;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;

namespace Action
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Object::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
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
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void changed() = 0;
        virtual void checkableChanged(bool checkable) = 0;
        virtual void enabledChanged(bool enabled) = 0;
        virtual void hovered() = 0;
        virtual void toggled(bool checked_) = 0;
        virtual void triggered(bool checked_) = 0;
        virtual void visibleChanged() = 0;
    };

    enum class MenuRole {
        NoRole = 0,
        TextHeuristicRole,
        ApplicationSpecificRole,
        AboutQtRole,
        AboutRole,
        PreferencesRole,
        QuitRole
    };

    enum class Priority {
        LowPriority = 0,
        NormalPriority = 128,
        HighPriority = 256
    };

    void Handle_setAutoRepeat(HandleRef _this, bool state);
    void Handle_setCheckable(HandleRef _this, bool checkable);
    void Handle_setChecked(HandleRef _this, bool checked_);
    void Handle_setEnabled(HandleRef _this, bool enabled);
    void Handle_setIcon(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon);
    void Handle_setIconText(HandleRef _this, std::string text);
    void Handle_setIconVisibleInMenu(HandleRef _this, bool visible);
    void Handle_setMenuRole(HandleRef _this, MenuRole role);
    void Handle_setPriority(HandleRef _this, Priority priority);
    void Handle_setShortcut(HandleRef _this, std::shared_ptr<KeySequence::Deferred::Base> shortcut);
    void Handle_setShortcutContext(HandleRef _this, Enums::ShortcutContext context);
    void Handle_setShortcutVisibleInContextMenu(HandleRef _this, bool visible);
    void Handle_setStatusTip(HandleRef _this, std::string tip);
    void Handle_setText(HandleRef _this, std::string text);
    void Handle_setToolTip(HandleRef _this, std::string tip);
    void Handle_setVisible(HandleRef _this, bool visible);
    void Handle_setWhatsThis(HandleRef _this, std::string text);
    void Handle_setSeparator(HandleRef _this, bool state);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(Object::HandleRef owner, std::shared_ptr<SignalHandler> handler);
}
