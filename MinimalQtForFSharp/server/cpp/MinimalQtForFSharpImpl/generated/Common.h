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

namespace Common
{

    struct Point {
        int32_t x;
        int32_t y;
    };

    struct PointF {
        double x;
        double y;
    };

    struct Size {
        int32_t width;
        int32_t height;
    };

    struct Rect {
        int32_t x;
        int32_t y;
        int32_t width;
        int32_t height;
    };

    struct RectF {
        double x;
        double y;
        double width;
        double height;
    };
}
