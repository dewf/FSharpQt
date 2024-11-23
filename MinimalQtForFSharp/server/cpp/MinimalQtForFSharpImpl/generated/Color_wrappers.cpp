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

    void create__wrapper() {
        auto name = Constant__pop();
        Owned__push(create(name));
    }

    void create_overload1__wrapper() {
        auto r = ni_popInt32();
        auto g = ni_popInt32();
        auto b = ni_popInt32();
        Owned__push(create(r, g, b));
    }

    void create_overload2__wrapper() {
        auto r = ni_popInt32();
        auto g = ni_popInt32();
        auto b = ni_popInt32();
        auto a = ni_popInt32();
        Owned__push(create(r, g, b, a));
    }

    void create_overload3__wrapper() {
        auto r = ni_popFloat();
        auto g = ni_popFloat();
        auto b = ni_popFloat();
        Owned__push(create(r, g, b));
    }

    void create_overload4__wrapper() {
        auto r = ni_popFloat();
        auto g = ni_popFloat();
        auto b = ni_popFloat();
        auto a = ni_popFloat();
        Owned__push(create(r, g, b, a));
    }

    int __register() {
        auto m = ni_registerModule("Color");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "create_overload1", &create_overload1__wrapper);
        ni_registerModuleMethod(m, "create_overload2", &create_overload2__wrapper);
        ni_registerModuleMethod(m, "create_overload3", &create_overload3__wrapper);
        ni_registerModuleMethod(m, "create_overload4", &create_overload4__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
