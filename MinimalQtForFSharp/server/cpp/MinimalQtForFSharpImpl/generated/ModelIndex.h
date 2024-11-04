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

    namespace Deferred {
        class Base;
    }

    bool Handle_isValid(HandleRef _this);
    int32_t Handle_row(HandleRef _this);
    int32_t Handle_column(HandleRef _this);
    Variant::OwnedHandleRef Handle_data(HandleRef _this);
    Variant::OwnedHandleRef Handle_data(HandleRef _this, Enums::ItemDataRole role);

    void Owned_dispose(OwnedRef _this);

    namespace Deferred {
        class Empty;
        class FromHandle;
        class FromOwned;

        class Visitor {
        public:
            virtual void onEmpty(const Empty* value) = 0;
            virtual void onFromHandle(const FromHandle* value) = 0;
            virtual void onFromOwned(const FromOwned* value) = 0;
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

        class FromHandle : public Base {
        public:
            const HandleRef handle;
            FromHandle(HandleRef handle) : handle(handle) {}
            void accept(Visitor* visitor) override {
                visitor->onFromHandle(this);
            }
        };

        class FromOwned : public Base {
        public:
            const OwnedRef owned;
            FromOwned(OwnedRef owned) : owned(owned) {}
            void accept(Visitor* visitor) override {
                visitor->onFromOwned(this);
            }
        };
    }
}
