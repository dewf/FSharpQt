#pragma once

#include "generated/SizePolicy.h"
#include <QSizePolicy>

namespace SizePolicy {
    QSizePolicy fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
