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
#include "Widget.h"
using namespace ::Widget;
#include "Common.h"
using namespace ::Common;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;

namespace AbstractSlider
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
        ActionTriggered = 1 << 5,
        RangeChanged = 1 << 6,
        SliderMoved = 1 << 7,
        SliderPressed = 1 << 8,
        SliderReleased = 1 << 9,
        ValueChanged = 1 << 10
    };

    enum class SliderAction {
        SliderNoAction,
        SliderSingleStepAdd,
        SliderSingleStepSub,
        SliderPageStepAdd,
        SliderPageStepSub,
        SliderToMinimum,
        SliderToMaximum,
        SliderMove
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void actionTriggered(SliderAction action) = 0;
        virtual void rangeChanged(int32_t min, int32_t max) = 0;
        virtual void sliderMoved(int32_t value) = 0;
        virtual void sliderPressed() = 0;
        virtual void sliderReleased() = 0;
        virtual void valueChanged(int32_t value) = 0;
    };

    void Handle_setInvertedAppearance(HandleRef _this, bool state);
    void Handle_setInvertedControls(HandleRef _this, bool state);
    void Handle_setMaximum(HandleRef _this, int32_t value);
    void Handle_setMinimum(HandleRef _this, int32_t value);
    void Handle_setOrientation(HandleRef _this, Enums::Orientation orient);
    void Handle_setPageStep(HandleRef _this, int32_t pageStep);
    void Handle_setSingleStep(HandleRef _this, int32_t step);
    void Handle_setSliderDown(HandleRef _this, bool state);
    void Handle_setSliderPosition(HandleRef _this, int32_t pos);
    void Handle_setTracking(HandleRef _this, bool value);
    void Handle_setValue(HandleRef _this, int32_t value);
    void Handle_setRange(HandleRef _this, int32_t min, int32_t max);
    void Handle_dispose(HandleRef _this);
}
