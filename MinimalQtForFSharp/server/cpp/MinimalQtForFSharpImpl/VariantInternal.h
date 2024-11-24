#pragma once

#include "generated/Variant.h"
#include <QVariant>
#include "IconInternal.h"

// create an owned heap-allocated copy of an existing QVariant value
#define VARIANT_HEAP_COPY(x) (reinterpret_cast<Variant::OwnedRef>(new QVariant(x)))

namespace Variant {
    QVariant fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
