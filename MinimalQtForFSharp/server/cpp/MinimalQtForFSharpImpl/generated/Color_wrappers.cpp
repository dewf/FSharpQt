#include "../support/NativeImplServer.h"
#include "Color_wrappers.h"
#include "Color.h"

namespace Color
{
    void Constant__push(Constant value) {
        ni_pushInt32((int32_t)value);
    }

    Constant Constant__pop() {
        auto tag = ni_popInt32();
        return (Constant)tag;
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
        void onFromHandle(const Deferred::FromHandle* fromHandleValue) override {
            Handle__push(fromHandleValue->value);
            // kind:
            ni_pushInt32(0);
        }
        void onFromConstant(const Deferred::FromConstant* fromConstantValue) override {
            Constant__push(fromConstantValue->name);
            // kind:
            ni_pushInt32(1);
        }
        void onFromRGB(const Deferred::FromRGB* fromRGBValue) override {
            ni_pushInt32(fromRGBValue->b);
            ni_pushInt32(fromRGBValue->g);
            ni_pushInt32(fromRGBValue->r);
            // kind:
            ni_pushInt32(2);
        }
        void onFromRGBA(const Deferred::FromRGBA* fromRGBAValue) override {
            ni_pushInt32(fromRGBAValue->a);
            ni_pushInt32(fromRGBAValue->b);
            ni_pushInt32(fromRGBAValue->g);
            ni_pushInt32(fromRGBAValue->r);
            // kind:
            ni_pushInt32(3);
        }
        void onFromFloatRGB(const Deferred::FromFloatRGB* fromFloatRGBValue) override {
            ni_pushFloat(fromFloatRGBValue->b);
            ni_pushFloat(fromFloatRGBValue->g);
            ni_pushFloat(fromFloatRGBValue->r);
            // kind:
            ni_pushInt32(4);
        }
        void onFromFloatRGBA(const Deferred::FromFloatRGBA* fromFloatRGBAValue) override {
            ni_pushFloat(fromFloatRGBAValue->a);
            ni_pushFloat(fromFloatRGBAValue->b);
            ni_pushFloat(fromFloatRGBAValue->g);
            ni_pushFloat(fromFloatRGBAValue->r);
            // kind:
            ni_pushInt32(5);
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
            auto value = Handle__pop();
            __ret = new Deferred::FromHandle(value);
            break;
        }
        case 1: {
            auto name = Constant__pop();
            __ret = new Deferred::FromConstant(name);
            break;
        }
        case 2: {
            auto r = ni_popInt32();
            auto g = ni_popInt32();
            auto b = ni_popInt32();
            __ret = new Deferred::FromRGB(r, g, b);
            break;
        }
        case 3: {
            auto r = ni_popInt32();
            auto g = ni_popInt32();
            auto b = ni_popInt32();
            auto a = ni_popInt32();
            __ret = new Deferred::FromRGBA(r, g, b, a);
            break;
        }
        case 4: {
            auto r = ni_popFloat();
            auto g = ni_popFloat();
            auto b = ni_popFloat();
            __ret = new Deferred::FromFloatRGB(r, g, b);
            break;
        }
        case 5: {
            auto r = ni_popFloat();
            auto g = ni_popFloat();
            auto b = ni_popFloat();
            auto a = ni_popFloat();
            __ret = new Deferred::FromFloatRGBA(r, g, b, a);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    void create__wrapper() {
        auto deferred = Deferred__pop();
        Owned__push(create(deferred));
    }

    int __register() {
        auto m = ni_registerModule("Color");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
