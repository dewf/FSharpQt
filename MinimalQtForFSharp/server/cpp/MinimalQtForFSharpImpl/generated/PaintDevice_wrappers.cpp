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

    int __register() {
        auto m = ni_registerModule("PaintDevice");
        return 0; // = OK
    }
}
