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
#include "Enums.h"
using namespace ::Enums;

namespace Timer
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Object::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // SignalMask:
        Timeout = 1 << 2
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void timeout() = 0;
    };

    bool Handle_isActive(HandleRef _this);
    void Handle_setInterval(HandleRef _this, int32_t interval);
    int32_t Handle_remainingTime(HandleRef _this);
    void Handle_setSingleShot(HandleRef _this, bool state);
    void Handle_setTimerType(HandleRef _this, Enums::TimerType type_);
    void Handle_start(HandleRef _this, int32_t msec);
    void Handle_start(HandleRef _this);
    void Handle_stop(HandleRef _this);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
