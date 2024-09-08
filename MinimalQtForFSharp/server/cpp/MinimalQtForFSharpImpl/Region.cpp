#include "generated/Region.h"

#include <QRegion>

#define THIS ((QRegion*)_this)

namespace Region
{
    void Handle_dispose(HandleRef _this) {
        printf("Region Handle_dispose: should never see this (unowned handle)\n");
    }

    void OwnedHandle_dispose(OwnedHandleRef _this) {
        delete THIS;
    }
}
