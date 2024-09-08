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
#include "AbstractSlider.h"
using namespace ::AbstractSlider;

namespace Slider
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractSlider::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // AbstractSlider::SignalMask:
        ActionTriggered = 1 << 5,
        RangeChanged = 1 << 6,
        SliderMoved = 1 << 7,
        SliderPressed = 1 << 8,
        SliderReleased = 1 << 9,
        ValueChanged = 1 << 10,
        // SignalMask:
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void actionTriggered(AbstractSlider::SliderAction action) = 0;
        virtual void rangeChanged(int32_t min, int32_t max) = 0;
        virtual void sliderMoved(int32_t value) = 0;
        virtual void sliderPressed() = 0;
        virtual void sliderReleased() = 0;
        virtual void valueChanged(int32_t value) = 0;
    };

    enum class TickPosition {
        NoTicks = 0,
        TicksAbove = 1,
        TicksLeft = TicksAbove,
        TicksBelow = 2,
        TicksRight = TicksBelow,
        TicksBothSides = 3
    };

    void Handle_setTickInterval(HandleRef _this, int32_t interval);
    void Handle_setTickPosition(HandleRef _this, TickPosition tpos);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
