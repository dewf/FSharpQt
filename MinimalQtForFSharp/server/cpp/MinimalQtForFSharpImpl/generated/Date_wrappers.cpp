#include "../support/NativeImplServer.h"
#include "Date_wrappers.h"
#include "Date.h"

namespace Date
{
    void YearMonthDay__push(YearMonthDay value, bool isReturn) {
        ni_pushInt32(value.day);
        ni_pushInt32(value.month);
        ni_pushInt32(value.year);
    }

    YearMonthDay YearMonthDay__pop() {
        auto year = ni_popInt32();
        auto month = ni_popInt32();
        auto day = ni_popInt32();
        return YearMonthDay { year, month, day };
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_toYearMonthDay__wrapper() {
        auto _this = Handle__pop();
        YearMonthDay__push(Handle_toYearMonthDay(_this), true);
    }
    void Owned__push(OwnedRef value) {
        ni_pushPtr(value);
    }

    OwnedRef Owned__pop() {
        return (OwnedRef)ni_popPtr();
    }

    void Owned_dispose__wrapper() {
        auto _this = Owned__pop();
        Owned_dispose(_this);
    }

    class Deferred_PushVisitor : public Deferred::Visitor {
    private:
        bool isReturn;
    public:
        Deferred_PushVisitor(bool isReturn) : isReturn(isReturn) {}
        void onFromYearMonthDay(const Deferred::FromYearMonthDay* fromYearMonthDayValue) override {
            ni_pushInt32(fromYearMonthDayValue->day);
            ni_pushInt32(fromYearMonthDayValue->month);
            ni_pushInt32(fromYearMonthDayValue->year);
            // kind:
            ni_pushInt32(0);
        }
    };

    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn) {
        Deferred_PushVisitor v(isReturn);
        value->accept((Deferred::Visitor*)&v);
    }

    std::shared_ptr<Deferred::Base> Deferred__pop() {
        Deferred::Base* __ret = nullptr;
        switch (ni_popInt32()) {
        case 0: {
            auto year = ni_popInt32();
            auto month = ni_popInt32();
            auto day = ni_popInt32();
            __ret = new Deferred::FromYearMonthDay(year, month, day);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("Date");
        ni_registerModuleMethod(m, "Handle_toYearMonthDay", &Handle_toYearMonthDay__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
