#pragma once

#include "generated/Pixmap.h"

namespace Pixmap {
    QPixmap fromDeferred(const std::shared_ptr<Pixmap::Deferred::Base>& deferred);
}
