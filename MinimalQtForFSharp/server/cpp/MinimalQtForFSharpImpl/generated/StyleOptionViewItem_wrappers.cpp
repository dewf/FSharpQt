#include "../support/NativeImplServer.h"
#include "StyleOptionViewItem_wrappers.h"
#include "StyleOptionViewItem.h"

#include "StyleOption_wrappers.h"
using namespace ::StyleOption;

namespace StyleOptionViewItem
{
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    int __register() {
        auto m = ni_registerModule("StyleOptionViewItem");
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        return 0; // = OK
    }
}
