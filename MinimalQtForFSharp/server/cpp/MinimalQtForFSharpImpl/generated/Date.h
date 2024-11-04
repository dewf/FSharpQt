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

namespace Date
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    namespace Deferred {
        class Base;
    }

    struct YearMonthDay {
        int32_t year;
        int32_t month;
        int32_t day;
    };

    YearMonthDay Handle_toYearMonthDay(HandleRef _this);

    void Owned_dispose(OwnedRef _this);

    namespace Deferred {
        class FromYearMonthDay;

        class Visitor {
        public:
            virtual void onFromYearMonthDay(const FromYearMonthDay* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class FromYearMonthDay : public Base {
        public:
            const int32_t year;
            const int32_t month;
            const int32_t day;
            FromYearMonthDay(int32_t year, int32_t month, int32_t day) : year(year), month(month), day(day) {}
            void accept(Visitor* visitor) override {
                visitor->onFromYearMonthDay(this);
            }
        };
    }
}
