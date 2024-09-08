#include "../support/NativeImplServer.h"
#include "DockWidget_wrappers.h"
#include "DockWidget.h"

#include "Widget_wrappers.h"
using namespace ::Widget;

namespace DockWidget
{
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_nothingYet__wrapper() {
        auto _this = Handle__pop();
        Handle_nothingYet(_this);
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    int __register() {
        auto m = ni_registerModule("DockWidget");
        ni_registerModuleMethod(m, "Handle_nothingYet", &Handle_nothingYet__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        return 0; // = OK
    }
}
