#pragma once

#include "generated/Pixmap.h"
#include <QPixmap>
#include "CommonInternal.h" // for PaintStackItem
#include "PaintDeviceInternal.h"

namespace Pixmap {
    struct __Handle : PaintDevice::__Handle, PaintStackItem {
        QPixmap qPixmap;
        explicit __Handle(QPixmap qPixmap) : qPixmap(std::move(qPixmap)) {}

        // needed to call PaintDevice methods
        QPaintDevice * getPaintDevice() override {
            return &qPixmap;
        }
    };

    struct __Owned : __Handle {
        explicit __Owned(const QPixmap &qPixmap) : __Handle(qPixmap) {}
    };
}
