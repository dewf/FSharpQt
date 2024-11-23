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

namespace Color
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    enum class Constant {
        Black,
        White,
        DarkGray,
        Gray,
        LightGray,
        Red,
        Green,
        Blue,
        Cyan,
        Magenta,
        Yellow,
        DarkRed,
        DarkGreen,
        DarkBlue,
        DarkCyan,
        DarkMagenta,
        DarkYellow,
        Transparent
    };


    void Owned_dispose(OwnedRef _this);
    OwnedRef create(Constant name);
    OwnedRef create(int32_t r, int32_t g, int32_t b);
    OwnedRef create(int32_t r, int32_t g, int32_t b, int32_t a);
    OwnedRef create(float r, float g, float b);
    OwnedRef create(float r, float g, float b, float a);
}
