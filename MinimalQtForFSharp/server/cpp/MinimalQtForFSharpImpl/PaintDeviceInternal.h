#pragma once

#include "generated/PaintDevice.h"
#include <QPaintDevice>

namespace PaintDevice {
    struct __Handle {
        virtual ~__Handle() = default;
        virtual QPaintDevice* getPaintDevice() = 0; // inheritors must implement
    };
}
