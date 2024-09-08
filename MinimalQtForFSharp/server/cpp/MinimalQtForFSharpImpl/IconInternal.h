#pragma once

#include <QIcon>

namespace Icon {
    QIcon fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
