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

namespace Cursor
{

    struct __Unowned; typedef struct __Unowned* UnownedRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends UnownedRef

    namespace Deferred {
        class Base;
    }


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
