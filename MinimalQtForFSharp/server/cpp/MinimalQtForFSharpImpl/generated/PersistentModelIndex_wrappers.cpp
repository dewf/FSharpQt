#include "../support/NativeImplServer.h"
#include "PersistentModelIndex_wrappers.h"
#include "PersistentModelIndex.h"

namespace PersistentModelIndex
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
        auto m = ni_registerModule("PersistentModelIndex");
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        return 0; // = OK
    }
}
