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
#include "KeySequence.h"
using namespace ::KeySequence;
#include "Widget.h"
using namespace ::Widget;
#include "Icon.h"
using namespace ::Icon;

namespace AbstractButton
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
        Clicked = 1 << 5,
        Pressed = 1 << 6,
        Released = 1 << 7,
        Toggled = 1 << 8
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void clicked(bool checkState) = 0;
        virtual void pressed() = 0;
        virtual void released() = 0;
        virtual void toggled(bool checkState) = 0;
    };

    void Handle_setAutoExclusive(HandleRef _this, bool state);
    void Handle_setAutoRepeat(HandleRef _this, bool state);
    void Handle_setAutoRepeatDelay(HandleRef _this, int32_t delay);
    void Handle_setAutoRepeatInterval(HandleRef _this, int32_t interval);
    void Handle_setCheckable(HandleRef _this, bool state);
    void Handle_setChecked(HandleRef _this, bool state);
    void Handle_setDown(HandleRef _this, bool state);
    void Handle_setIcon(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setShortcut(HandleRef _this, std::shared_ptr<KeySequence::Deferred::Base> seq);
    void Handle_setText(HandleRef _this, std::string text);
}
