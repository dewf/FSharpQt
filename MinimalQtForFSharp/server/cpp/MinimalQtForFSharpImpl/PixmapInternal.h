#pragma once

#include "generated/Pixmap.h"
#include <QPixmap>
#include "CommonInternal.h" // for PaintStackItem

namespace Pixmap {
    struct __Handle : PaintStackItem {
        QPixmap qPixmap;
        explicit __Handle(QPixmap qPixmap) : qPixmap(std::move(qPixmap)) {}
    };

    struct __Owned : __Handle {
        explicit __Owned(const QPixmap &qPixmap) : __Handle(qPixmap) {}
    };

    QPixmap fromDeferred(const std::shared_ptr<Pixmap::Deferred::Base>& deferred);
}
