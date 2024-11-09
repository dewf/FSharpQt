#pragma once

#include "generated/Color.h"
#include <QColor>

#include "CommonInternal.h" // for PaintStackItem
using namespace Common;

namespace Color {
    struct __Handle : public PaintStackItem {
        QColor qColor;
        explicit __Handle(QColor qColor) : qColor(qColor) {}
    };
    struct __Owned : public __Handle {
        explicit __Owned(QColor qColor) : __Handle(qColor) {}
    };

    QColor fromConstant(Constant name);
    QColor fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
