#include "../support/NativeImplServer.h"
#include "Variant_wrappers.h"
#include "Variant.h"

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "PaintResources_wrappers.h"
using namespace ::PaintResources;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace Variant
{

    class ServerValue_PushVisitor : public ServerValue::Visitor {
    private:
        bool isReturn;
    public:
        ServerValue_PushVisitor(bool isReturn) : isReturn(isReturn) {}
        void onBool(const ServerValue::Bool* boolValue) override {
            ni_pushBool(boolValue->value);
            // kind:
            ni_pushInt32(0);
        }
        void onString(const ServerValue::String* stringValue) override {
            pushStringInternal(stringValue->value);
            // kind:
            ni_pushInt32(1);
        }
        void onInt(const ServerValue::Int* intValue) override {
            ni_pushInt32(intValue->value);
            // kind:
            ni_pushInt32(2);
        }
        void onUnknown(const ServerValue::Unknown* unknownValue) override {
            // kind:
            ni_pushInt32(3);
        }
    };

    void ServerValue__push(std::shared_ptr<ServerValue::Base> value, bool isReturn) {
        ServerValue_PushVisitor v(isReturn);
        value->accept((ServerValue::Visitor*)&v);
    }

    std::shared_ptr<ServerValue::Base> ServerValue__pop() {
        ServerValue::Base* __ret = nullptr;
        switch (ni_popInt32()) {
        case 0: {
            auto value = ni_popBool();
            __ret = new ServerValue::Bool(value);
            break;
        }
        case 1: {
            auto value = popStringInternal();
            __ret = new ServerValue::String(value);
            break;
        }
        case 2: {
            auto value = ni_popInt32();
            __ret = new ServerValue::Int(value);
            break;
        }
        case 3: {
            __ret = new ServerValue::Unknown();
            break;
        }
        default:
            printf("C++ ServerValue__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<ServerValue::Base>(__ret);
    }
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

    void Handle_toBool__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_toBool(_this));
    }

    void Handle_toString2__wrapper() {
        auto _this = Handle__pop();
        pushStringInternal(Handle_toString2(_this));
    }

    void Handle_toInt__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_toInt(_this));
    }

    void Handle_toSize__wrapper() {
        auto _this = Handle__pop();
        Size__push(Handle_toSize(_this), true);
    }

    void Handle_toCheckState__wrapper() {
        auto _this = Handle__pop();
        CheckState__push(Handle_toCheckState(_this));
    }

    void Handle_toServerValue__wrapper() {
        auto _this = Handle__pop();
        ServerValue__push(Handle_toServerValue(_this), true);
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
        void onFromBool(const Deferred::FromBool* fromBoolValue) override {
            ni_pushBool(fromBoolValue->value);
            // kind:
            ni_pushInt32(1);
        }
        void onFromString(const Deferred::FromString* fromStringValue) override {
            pushStringInternal(fromStringValue->value);
            // kind:
            ni_pushInt32(2);
        }
        void onFromInt(const Deferred::FromInt* fromIntValue) override {
            ni_pushInt32(fromIntValue->value);
            // kind:
            ni_pushInt32(3);
        }
        void onFromCheckState(const Deferred::FromCheckState* fromCheckStateValue) override {
            CheckState__push(fromCheckStateValue->value);
            // kind:
            ni_pushInt32(4);
        }
        void onFromSize(const Deferred::FromSize* fromSizeValue) override {
            Size__push(fromSizeValue->size, isReturn);
            // kind:
            ni_pushInt32(5);
        }
        void onFromIcon(const Deferred::FromIcon* fromIconValue) override {
            Icon::Deferred__push(fromIconValue->value, isReturn);
            // kind:
            ni_pushInt32(6);
        }
        void onFromColor(const Deferred::FromColor* fromColorValue) override {
            Color::Deferred__push(fromColorValue->value, isReturn);
            // kind:
            ni_pushInt32(7);
        }
        void onFromAligment(const Deferred::FromAligment* fromAligmentValue) override {
            Alignment__push(fromAligmentValue->value);
            // kind:
            ni_pushInt32(8);
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
            auto value = ni_popBool();
            __ret = new Deferred::FromBool(value);
            break;
        }
        case 2: {
            auto value = popStringInternal();
            __ret = new Deferred::FromString(value);
            break;
        }
        case 3: {
            auto value = ni_popInt32();
            __ret = new Deferred::FromInt(value);
            break;
        }
        case 4: {
            auto value = CheckState__pop();
            __ret = new Deferred::FromCheckState(value);
            break;
        }
        case 5: {
            auto size = Size__pop();
            __ret = new Deferred::FromSize(size);
            break;
        }
        case 6: {
            auto value = Icon::Deferred__pop();
            __ret = new Deferred::FromIcon(value);
            break;
        }
        case 7: {
            auto value = Color::Deferred__pop();
            __ret = new Deferred::FromColor(value);
            break;
        }
        case 8: {
            auto value = Alignment__pop();
            __ret = new Deferred::FromAligment(value);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("Variant");
        ni_registerModuleMethod(m, "Handle_isValid", &Handle_isValid__wrapper);
        ni_registerModuleMethod(m, "Handle_toBool", &Handle_toBool__wrapper);
        ni_registerModuleMethod(m, "Handle_toString2", &Handle_toString2__wrapper);
        ni_registerModuleMethod(m, "Handle_toInt", &Handle_toInt__wrapper);
        ni_registerModuleMethod(m, "Handle_toSize", &Handle_toSize__wrapper);
        ni_registerModuleMethod(m, "Handle_toCheckState", &Handle_toCheckState__wrapper);
        ni_registerModuleMethod(m, "Handle_toServerValue", &Handle_toServerValue__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        ni_registerModuleMethod(m, "OwnedHandle_dispose", &OwnedHandle_dispose__wrapper);
        return 0; // = OK
    }
}
