#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

#include "Variant.h"
using namespace ::Variant;
#include "Enums.h"
using namespace ::Enums;

namespace ModelIndex
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    bool Handle_isValid(HandleRef _this);
    int32_t Handle_row(HandleRef _this);
    int32_t Handle_column(HandleRef _this);
    Variant::OwnedRef Handle_data(HandleRef _this);
    Variant::OwnedRef Handle_data(HandleRef _this, ItemDataRole role);

    void Owned_dispose(OwnedRef _this);
    OwnedRef create();
}
