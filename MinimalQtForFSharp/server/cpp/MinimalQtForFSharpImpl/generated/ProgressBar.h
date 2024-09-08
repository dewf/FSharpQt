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
#include "Enums.h"
using namespace ::Enums;
#include "Icon.h"
using namespace ::Icon;
#include "Widget.h"
using namespace ::Widget;

namespace ProgressBar
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
        ValueChanged = 1 << 5
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void valueChanged(int32_t value) = 0;
    };

    enum class Direction {
        TopToBottom,
        BottomToTop
    };

    void Handle_setAlignment(HandleRef _this, Enums::Alignment align);
    void Handle_setFormat(HandleRef _this, std::string format);
    void Handle_setInvertedAppearance(HandleRef _this, bool invert);
    void Handle_setMaximum(HandleRef _this, int32_t value);
    void Handle_setMinimum(HandleRef _this, int32_t value);
    void Handle_setOrientation(HandleRef _this, Enums::Orientation orient);
    std::string Handle_text(HandleRef _this);
    void Handle_setTextDirection(HandleRef _this, Direction direction);
    void Handle_setTextVisible(HandleRef _this, bool visible);
    void Handle_setValue(HandleRef _this, int32_t value);
    void Handle_setRange(HandleRef _this, int32_t min, int32_t max);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
