#include "generated/AbstractSlider.h"

#include <QObject>
#include <QAbstractSlider>

#define THIS ((QAbstractSlider*)_this)

namespace AbstractSlider
{
    void Handle_setInvertedAppearance(HandleRef _this, bool state) {
        THIS->setInvertedAppearance(state);
    }

    void Handle_setInvertedControls(HandleRef _this, bool state) {
        THIS->setInvertedControls(state);
    }

    void Handle_setMaximum(HandleRef _this, int32_t value) {
        THIS->setMaximum(value);
    }

    void Handle_setMinimum(HandleRef _this, int32_t value) {
        THIS->setMinimum(value);
    }

    void Handle_setOrientation(HandleRef _this, Orientation orient) {
        THIS->setOrientation((Qt::Orientation)orient);
    }

    void Handle_setPageStep(HandleRef _this, int32_t pageStep) {
        THIS->setPageStep(pageStep);
    }

    void Handle_setSingleStep(HandleRef _this, int32_t step) {
        THIS->setSingleStep(step);
    }

    void Handle_setSliderDown(HandleRef _this, bool state) {
        THIS->setSliderDown(state);
    }

    void Handle_setSliderPosition(HandleRef _this, int32_t pos) {
        THIS->setSliderPosition(pos);
    }

    void Handle_setTracking(HandleRef _this, bool value) {
        THIS->setTracking(value);
    }

    void Handle_setValue(HandleRef _this, int32_t value) {
        THIS->setValue(value);
    }

    void Handle_setRange(HandleRef _this, int32_t min, int32_t max) {
        THIS->setRange(min, max);
    }

    void Handle_dispose(HandleRef _this) {
        printf("AbstractSlider::Handle_dispose called - shouldn't happen\n");
    }
}
