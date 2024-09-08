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

namespace PersistentModelIndex
{

    struct __Handle; typedef struct __Handle* HandleRef;

    void Handle_dispose(HandleRef _this);
}
