#pragma once

#include "generated/Variant.h"
#include <QVariant>
#include "IconInternal.h"

namespace Variant {
    QVariant fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
