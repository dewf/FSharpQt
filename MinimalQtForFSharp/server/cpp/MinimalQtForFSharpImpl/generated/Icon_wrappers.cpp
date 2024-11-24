#include "../support/NativeImplServer.h"
#include "Icon_wrappers.h"
#include "Icon.h"

#include "Pixmap_wrappers.h"
using namespace ::Pixmap;

namespace Icon
{
    void Mode__push(Mode value) {
        ni_pushInt32((int32_t)value);
    }

    Mode Mode__pop() {
        auto tag = ni_popInt32();
        return (Mode)tag;
    }
    void State__push(State value) {
        ni_pushInt32((int32_t)value);
    }

    State State__pop() {
        auto tag = ni_popInt32();
        return (State)tag;
    }
    void ThemeIcon__push(ThemeIcon value) {
        ni_pushInt32((int32_t)value);
    }

    ThemeIcon ThemeIcon__pop() {
        auto tag = ni_popInt32();
        return (ThemeIcon)tag;
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
        Owned__push(create());
    }

    void create_overload1__wrapper() {
        auto themeIcon = ThemeIcon__pop();
        Owned__push(create(themeIcon));
    }

    void create_overload2__wrapper() {
        auto filename = popStringInternal();
        Owned__push(create(filename));
    }

    void create_overload3__wrapper() {
        auto pixmap = Pixmap::Handle__pop();
        Owned__push(create(pixmap));
    }

    int __register() {
        auto m = ni_registerModule("Icon");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "create_overload1", &create_overload1__wrapper);
        ni_registerModuleMethod(m, "create_overload2", &create_overload2__wrapper);
        ni_registerModuleMethod(m, "create_overload3", &create_overload3__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
