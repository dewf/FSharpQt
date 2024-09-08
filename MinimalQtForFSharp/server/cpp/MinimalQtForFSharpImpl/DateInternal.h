#pragma once

#include "generated/Date.h"
#include <QDate>

namespace Date {
    QDate fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}
