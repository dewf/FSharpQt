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
#include "Widget.h"
using namespace ::Widget;
#include "Action.h"
using namespace ::Action;
#include "Icon.h"
using namespace ::Icon;

namespace Menu
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Widget::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // SignalMask:
        AboutToHide = 1 << 5,
        AboutToShow = 1 << 6,
        Hovered = 1 << 7,
        Triggered = 1 << 8
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void aboutToHide() = 0;
        virtual void aboutToShow() = 0;
        virtual void hovered(Action::HandleRef action) = 0;
        virtual void triggered(Action::HandleRef action) = 0;
    };

    void Handle_setIcon(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon);
    void Handle_setSeparatorsCollapsible(HandleRef _this, bool state);
    void Handle_setTearOffEnabled(HandleRef _this, bool state);
    void Handle_setTitle(HandleRef _this, std::string title);
    void Handle_setToolTipsVisible(HandleRef _this, bool visible);
    void Handle_clear(HandleRef _this);
    Action::HandleRef Handle_addSeparator(HandleRef _this);
    void Handle_popup(HandleRef _this, Common::Point p);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
