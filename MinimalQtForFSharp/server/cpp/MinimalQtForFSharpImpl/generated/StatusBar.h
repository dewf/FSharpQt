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
#include "Widget.h"
using namespace ::Widget;

namespace StatusBar
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
        MessageChanged = 1 << 5
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void messageChanged(std::string message) = 0;
    };

    bool Handle_isSizeGripEnabled(HandleRef _this);
    void Handle_setSizeGripEnabled(HandleRef _this, bool enabled);
    void Handle_clearMessage(HandleRef _this);
    void Handle_showMessage(HandleRef _this, std::string message, int32_t timeout);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
