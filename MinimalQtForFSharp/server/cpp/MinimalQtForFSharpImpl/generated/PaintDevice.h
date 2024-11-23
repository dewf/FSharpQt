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

namespace PaintDevice
{

    struct __Handle; typedef struct __Handle* HandleRef;

    int32_t Handle_width(HandleRef _this);
    int32_t Handle_height(HandleRef _this);
}
