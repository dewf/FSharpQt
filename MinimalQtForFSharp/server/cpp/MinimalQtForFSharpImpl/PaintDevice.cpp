#include "generated/PaintDevice.h"

#include "PaintDeviceInternal.h"

namespace PaintDevice
{
    int32_t Handle_width(HandleRef _this) {
        return _this->getPaintDevice()->width();
    }

    int32_t Handle_height(HandleRef _this) {
        return _this->getPaintDevice()->height();
    }
}
