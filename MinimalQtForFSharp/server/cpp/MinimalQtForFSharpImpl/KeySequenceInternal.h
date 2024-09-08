#pragma once

#include <QKeySequence>

namespace KeySequence {
    QKeySequence fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
