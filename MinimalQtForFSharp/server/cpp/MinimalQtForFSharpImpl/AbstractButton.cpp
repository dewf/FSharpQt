#include "generated/AbstractButton.h"

#include <QAbstractButton>

#include "IconInternal.h"
#include "KeySequenceInternal.h"
#include "util/convert.h"

#define THIS ((QAbstractButton*)_this)

namespace AbstractButton
{
    void Handle_setAutoExclusive(HandleRef _this, bool state) {
        THIS->setAutoExclusive(state);
    }

    void Handle_setAutoRepeat(HandleRef _this, bool state) {
        THIS->setAutoRepeat(state);
    }

    void Handle_setAutoRepeatDelay(HandleRef _this, int32_t delay) {
        THIS->setAutoRepeatDelay(delay);
    }

    void Handle_setAutoRepeatInterval(HandleRef _this, int32_t interval) {
        THIS->setAutoRepeatInterval(interval);
    }

    void Handle_setCheckable(HandleRef _this, bool state) {
        THIS->setCheckable(state);
    }

    void Handle_setChecked(HandleRef _this, bool state) {
        THIS->setChecked(state);
    }

    void Handle_setDown(HandleRef _this, bool state) {
        THIS->setDown(state);
    }

    void Handle_setIcon(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon) {
        THIS->setIcon(Icon::fromDeferred(icon));
    }

    void Handle_setIconSize(HandleRef _this, Size size) {
        THIS->setIconSize(toQSize(size));
    }

    void Handle_setShortcut(HandleRef _this, std::shared_ptr<KeySequence::Deferred::Base> seq) {
        THIS->setShortcut(KeySequence::fromDeferred(seq));
    }

    void Handle_setText(HandleRef _this, std::string text) {
        THIS->setText(QString::fromStdString(text));
    }
}
