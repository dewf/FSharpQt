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

namespace Dialog
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
        Accepted = 1 << 5,
        Finished = 1 << 6,
        Rejected = 1 << 7
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
    };

    void Handle_setModal(HandleRef _this, bool state);
    void Handle_setSizeGripEnabled(HandleRef _this, bool enabled);
    void Handle_setParentDialogFlags(HandleRef _this, Widget::HandleRef parent);
    void Handle_accept(HandleRef _this);
    void Handle_reject(HandleRef _this);
    int32_t Handle_exec(HandleRef _this);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(Widget::HandleRef parent, std::shared_ptr<SignalHandler> handler);
}
