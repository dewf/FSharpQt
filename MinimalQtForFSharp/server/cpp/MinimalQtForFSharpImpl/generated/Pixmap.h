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

#include "PaintDevice.h"
using namespace ::PaintDevice;
#include "Common.h"
using namespace ::Common;
#include "Enums.h"
using namespace ::Enums;

namespace Pixmap
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends PaintDevice::HandleRef
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    namespace Deferred {
        class Base;
    }


    void Owned_dispose(OwnedRef _this);

    namespace Deferred {
        class Empty;

        class Visitor {
        public:
            virtual void onEmpty(const Empty* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class Empty : public Base {
        public:
            Empty() {}
            void accept(Visitor* visitor) override {
                visitor->onEmpty(this);
            }
        };
    }
}
