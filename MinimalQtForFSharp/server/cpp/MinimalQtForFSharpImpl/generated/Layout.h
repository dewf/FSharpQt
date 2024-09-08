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

#include "Object.h"
using namespace ::Object;

namespace Layout
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Object::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // SignalMask:
    };

    class SignalHandler {
    public:
        virtual void destroyed(HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
    };

    enum class SizeConstraint {
        SetDefaultConstraint,
        SetNoConstraint,
        SetMinimumSize,
        SetFixedSize,
        SetMaximumSize,
        SetMinAndMaxSize
    };

    void Handle_setEnabled(HandleRef _this, bool enabled);
    void Handle_setSpacing(HandleRef _this, int32_t spacing);
    void Handle_setContentsMargins(HandleRef _this, int32_t left, int32_t top, int32_t right, int32_t bottom);
    void Handle_setSizeConstraint(HandleRef _this, SizeConstraint constraint);
    void Handle_removeAll(HandleRef _this);
    void Handle_activate(HandleRef _this);
    void Handle_update(HandleRef _this);
    void Handle_dispose(HandleRef _this);
}
