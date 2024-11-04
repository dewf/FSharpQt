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

namespace SizePolicy
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    namespace Deferred {
        class Base;
    }

    typedef int32_t Policy;
    enum PolicyFlags : int32_t {
        GrowFlag = 1,
        ExpandFlag = 2,
        ShrinkFlag = 4,
        IgnoreFlag = 8,
        Fixed = 0,
        Minimum = GrowFlag,
        Maximum = ShrinkFlag,
        Preferred = GrowFlag | ShrinkFlag,
        MinimumExpanding = GrowFlag | ExpandFlag,
        Expanding = GrowFlag | ShrinkFlag | ExpandFlag,
        Ignored = ShrinkFlag | GrowFlag | IgnoreFlag
    };


    void Owned_dispose(OwnedRef _this);

    namespace Deferred {
        class Todo;

        class Visitor {
        public:
            virtual void onTodo(const Todo* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class Todo : public Base {
        public:
            Todo() {}
            void accept(Visitor* visitor) override {
                visitor->onTodo(this);
            }
        };
    }
}
