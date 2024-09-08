#include "../support/NativeImplServer.h"
#include "ModelIndex_wrappers.h"
#include "ModelIndex.h"

#include "Variant_wrappers.h"
using namespace ::Variant;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace ModelIndex
{
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_isValid__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isValid(_this));
    }

    void Handle_row__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_row(_this));
    }

    void Handle_column__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_column(_this));
    }

    void Handle_data__wrapper() {
        auto _this = Handle__pop();
        Variant::OwnedHandle__push(Handle_data(_this));
    }

    void Handle_data_overload1__wrapper() {
        auto _this = Handle__pop();
        auto role = ItemDataRole__pop();
        Variant::OwnedHandle__push(Handle_data(_this, role));
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }
    void OwnedHandle__push(OwnedHandleRef value) {
        ni_pushPtr(value);
    }

    OwnedHandleRef OwnedHandle__pop() {
        return (OwnedHandleRef)ni_popPtr();
    }

    void OwnedHandle_dispose__wrapper() {
        auto _this = OwnedHandle__pop();
        OwnedHandle_dispose(_this);
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
        void onFromHandle(const Deferred::FromHandle* fromHandleValue) override {
            Handle__push(fromHandleValue->handle);
            // kind:
            ni_pushInt32(1);
        }
        void onFromOwned(const Deferred::FromOwned* fromOwnedValue) override {
            OwnedHandle__push(fromOwnedValue->owned);
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
            __ret = new Deferred::Empty();
            break;
        }
        case 1: {
            auto handle = Handle__pop();
            __ret = new Deferred::FromHandle(handle);
            break;
        }
        case 2: {
            auto owned = OwnedHandle__pop();
            __ret = new Deferred::FromOwned(owned);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("ModelIndex");
        ni_registerModuleMethod(m, "Handle_isValid", &Handle_isValid__wrapper);
        ni_registerModuleMethod(m, "Handle_row", &Handle_row__wrapper);
        ni_registerModuleMethod(m, "Handle_column", &Handle_column__wrapper);
        ni_registerModuleMethod(m, "Handle_data", &Handle_data__wrapper);
        ni_registerModuleMethod(m, "Handle_data_overload1", &Handle_data_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        ni_registerModuleMethod(m, "OwnedHandle_dispose", &OwnedHandle_dispose__wrapper);
        return 0; // = OK
    }
}
