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
        Variant::Owned__push(Handle_data(_this));
    }

    void Handle_data_overload1__wrapper() {
        auto _this = Handle__pop();
        auto role = ItemDataRole__pop();
        Variant::Owned__push(Handle_data(_this, role));
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

    int __register() {
        auto m = ni_registerModule("ModelIndex");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_isValid", &Handle_isValid__wrapper);
        ni_registerModuleMethod(m, "Handle_row", &Handle_row__wrapper);
        ni_registerModuleMethod(m, "Handle_column", &Handle_column__wrapper);
        ni_registerModuleMethod(m, "Handle_data", &Handle_data__wrapper);
        ni_registerModuleMethod(m, "Handle_data_overload1", &Handle_data_overload1__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
