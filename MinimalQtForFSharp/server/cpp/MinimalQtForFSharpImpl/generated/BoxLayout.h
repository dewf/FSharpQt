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

#include "Common.h"
using namespace ::Common;
#include "Widget.h"
using namespace ::Widget;
#include "Layout.h"
using namespace ::Layout;
#include "Enums.h"
using namespace ::Enums;
#include "Object.h"
using namespace ::Object;

namespace BoxLayout
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Layout::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Layout::SignalMask:
        // SignalMask:
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
    };

    enum class Direction {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    };

    void Handle_setDirection(HandleRef _this, Direction dir);
    void Handle_addSpacing(HandleRef _this, int32_t size);
    void Handle_addStretch(HandleRef _this, int32_t stretch);
    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget);
    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget, int32_t stretch);
    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget, int32_t stretch, Enums::Alignment align);
    void Handle_addLayout(HandleRef _this, Layout::HandleRef layout);
    void Handle_addLayout(HandleRef _this, Layout::HandleRef layout, int32_t stretch);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(Direction dir, std::shared_ptr<SignalHandler> handler);
    HandleRef createNoHandler(Direction dir);
}
