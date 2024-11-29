#pragma once

#include "generated/Color.h"
#include <QColor>

#include "CommonInternal.h" // for PaintStackItem
using namespace Common;

namespace Color {
    struct __Handle : PaintStackItem {
        QColor qColor;
        explicit __Handle(const QColor qColor) : qColor(qColor) {}
    };

    struct __Owned : __Handle {
        explicit __Owned(const QColor qColor) : __Handle(qColor) {}
    };

    QColor fromConstant(Constant name);
    QColor fromDeferred(const std::shared_ptr<Deferred::Base> &deferred);
}
