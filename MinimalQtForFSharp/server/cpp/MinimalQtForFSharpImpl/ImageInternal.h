#pragma once

#include "generated/Image.h"
#include <QImage>
#include <utility>
#include "CommonInternal.h" // for PaintStackItem
#include "PaintDeviceInternal.h"

namespace Image {
    // we could probably avoid inheriting these from PaintStackItem,
    // since it would be a little crazy to be allocating them during a paint callback
    // It would be much more sensible for them to be created as part of the view resources,
    // (assuming we go through with that)
    // however, until then, and depending on whether we keep the current "paintresources" system
    // of custom widgets (where there's a long-lived paintstack that gets allocated once and freed once),
    // we'll inherit it for now
    struct __Handle : PaintDevice::__Handle, PaintStackItem {
        QImage qImage;
        explicit __Handle(QImage qImage) : qImage(std::move(qImage)) {}

        // needed for PaintDevice methods to be able to operate on these handles
        QPaintDevice * getPaintDevice() override {
            return &qImage;
        }
    };

    struct __Owned : __Handle {
        explicit __Owned(const QImage &qImage) : __Handle(qImage) {}
    };
}
