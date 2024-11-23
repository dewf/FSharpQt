#include "../support/NativeImplServer.h"
#include "PaintDevice_wrappers.h"
#include "PaintDevice.h"

namespace PaintDevice
{
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_width__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_width(_this));
    }

    void Handle_height__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_height(_this));
    }

    int __register() {
        auto m = ni_registerModule("PaintDevice");
        ni_registerModuleMethod(m, "Handle_width", &Handle_width__wrapper);
        ni_registerModuleMethod(m, "Handle_height", &Handle_height__wrapper);
        return 0; // = OK
    }
}
