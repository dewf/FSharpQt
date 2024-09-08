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

namespace Object
{

    struct __Handle; typedef struct __Handle* HandleRef;

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1
    };

    class SignalHandler {
    public:
        virtual void destroyed(HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
    };

    void Handle_setObjectName(HandleRef _this, std::string name);
    void Handle_dumpObjectTree(HandleRef _this);
    void Handle_dispose(HandleRef _this);
}
