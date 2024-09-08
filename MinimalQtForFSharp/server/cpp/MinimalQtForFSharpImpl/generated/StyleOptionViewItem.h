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

#include "StyleOption.h"
using namespace ::StyleOption;

namespace StyleOptionViewItem
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends StyleOption::HandleRef

    void Handle_dispose(HandleRef _this);
}
