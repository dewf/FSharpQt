#include "../support/NativeImplServer.h"
#include "SizePolicy_wrappers.h"
#include "SizePolicy.h"

namespace SizePolicy
{
    void Policy__push(Policy value) {
        ni_pushInt32(value);
    }

    Policy Policy__pop() {
        return ni_popInt32();
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
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
        void onTodo(const Deferred::Todo* todoValue) override {
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
            __ret = new Deferred::Todo();
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("SizePolicy");
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
