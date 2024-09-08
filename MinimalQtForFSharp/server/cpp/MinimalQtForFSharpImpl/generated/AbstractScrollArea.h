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
#include "Frame.h"
using namespace ::Frame;

namespace AbstractScrollArea
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Frame::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // Frame::SignalMask:
        // SignalMask:
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
    };

    enum class SizeAdjustPolicy {
        AdjustIgnored,
        AdjustToContentsOnFirstShow,
        AdjustToContents
    };

    enum class ScrollBarPolicy {
        ScrollBarAsNeeded,
        ScrollBarAlwaysOff,
        ScrollBarAlwaysOn
    };

    void Handle_setHorizontalScrollBarPolicy(HandleRef _this, ScrollBarPolicy policy);
    void Handle_setSizeAdjustPolicy(HandleRef _this, SizeAdjustPolicy policy);
    void Handle_setVerticalScrollBarPolicy(HandleRef _this, ScrollBarPolicy policy);
    void Handle_dispose(HandleRef _this);
}
