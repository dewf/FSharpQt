#include "../support/NativeImplServer.h"
#include "RegularExpression_wrappers.h"
#include "RegularExpression.h"

namespace RegularExpression
{
    void PatternOptions__push(PatternOptions value) {
        ni_pushInt32(value);
    }

    PatternOptions PatternOptions__pop() {
        return ni_popInt32();
    }

    class Deferred_PushVisitor : public Deferred::Visitor {
    private:
        bool isReturn;
    public:
        Deferred_PushVisitor(bool isReturn) : isReturn(isReturn) {}
        void onEmpty(const Deferred::Empty* emptyValue) override {
            // kind:
            ni_pushInt32(0);
        }
        void onRegex(const Deferred::Regex* regexValue) override {
            PatternOptions__push(regexValue->opts);
            pushStringInternal(regexValue->pattern);
            // kind:
            ni_pushInt32(1);
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
            __ret = new Deferred::Empty();
            break;
        }
        case 1: {
            auto pattern = popStringInternal();
            auto opts = PatternOptions__pop();
            __ret = new Deferred::Regex(pattern, opts);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("RegularExpression");
        return 0; // = OK
    }
}
