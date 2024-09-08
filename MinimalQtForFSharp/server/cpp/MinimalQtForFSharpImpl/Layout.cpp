#include "generated/Layout.h"

#include <QWidget>
#include <QLayout>

#define THIS ((QLayout*)_this)

namespace Layout
{
    void Handle_setEnabled(HandleRef _this, bool enabled) {
        THIS->setEnabled(enabled);
    }

    void Handle_setSpacing(HandleRef _this, int32_t spacing) {
        THIS->setSpacing(spacing);
    }

    void Handle_setContentsMargins(HandleRef _this, int32_t left, int32_t top, int32_t right, int32_t bottom) {
        THIS->setContentsMargins(left, top, right, bottom);
    }

    void Handle_setSizeConstraint(HandleRef _this, SizeConstraint constraint) {
        THIS->setSizeConstraint((QLayout::SizeConstraint)constraint);
    }

    void Handle_removeAll(HandleRef _this) {
        QLayoutItem *item;
        while ((item = THIS->takeAt(0)) != nullptr) {
            item->widget()->setParent(nullptr);
            delete item;
        }
    }

    void Handle_activate(HandleRef _this) {
        THIS->activate();
    }

    void Handle_update(HandleRef _this) {
        THIS->update();
    }

    void Handle_dispose(HandleRef _this) {
        delete (QLayout*)_this;
    }
}
