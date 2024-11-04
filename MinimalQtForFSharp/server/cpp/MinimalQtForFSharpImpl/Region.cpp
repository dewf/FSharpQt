#include "generated/Region.h"

#include <QRegion>

#define THIS ((QRegion*)_this)

namespace Region
{
    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }
}
