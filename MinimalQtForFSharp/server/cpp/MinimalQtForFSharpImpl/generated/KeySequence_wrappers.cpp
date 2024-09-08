#include "../support/NativeImplServer.h"
#include "KeySequence_wrappers.h"
#include "KeySequence.h"

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace KeySequence
{
    void StandardKey__push(StandardKey value) {
        ni_pushInt32((int32_t)value);
    }

    StandardKey StandardKey__pop() {
        auto tag = ni_popInt32();
        return (StandardKey)tag;
    }

    class Deferred_PushVisitor : public Deferred::Visitor {
    private:
        bool isReturn;
    public:
        Deferred_PushVisitor(bool isReturn) : isReturn(isReturn) {}
        void onFromString(const Deferred::FromString* fromStringValue) override {
            pushStringInternal(fromStringValue->s);
            // kind:
            ni_pushInt32(0);
        }
        void onFromStandard(const Deferred::FromStandard* fromStandardValue) override {
            StandardKey__push(fromStandardValue->key);
            // kind:
            ni_pushInt32(1);
        }
        void onFromKey(const Deferred::FromKey* fromKeyValue) override {
            Modifiers__push(fromKeyValue->modifiers);
            Key__push(fromKeyValue->key);
            // kind:
            ni_pushInt32(2);
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
            auto s = popStringInternal();
            __ret = new Deferred::FromString(s);
            break;
        }
        case 1: {
            auto key = StandardKey__pop();
            __ret = new Deferred::FromStandard(key);
            break;
        }
        case 2: {
            auto key = Key__pop();
            auto modifiers = Modifiers__pop();
            __ret = new Deferred::FromKey(key, modifiers);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("KeySequence");
        return 0; // = OK
    }
}
