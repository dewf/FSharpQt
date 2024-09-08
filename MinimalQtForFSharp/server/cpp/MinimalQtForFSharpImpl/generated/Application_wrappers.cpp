#include "../support/NativeImplServer.h"
#include "Application_wrappers.h"
#include "Application.h"

namespace Application
{
    // built-in array type: std::vector<std::string>
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setQuitOnLastWindowClosed__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setQuitOnLastWindowClosed(_this, state);
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    void setStyle__wrapper() {
        auto name = popStringInternal();
        setStyle(name);
    }

    void exec__wrapper() {
        ni_pushInt32(exec());
    }

    void quit__wrapper() {
        quit();
    }

    void availableStyles__wrapper() {
        pushStringArrayInternal(availableStyles());
    }

    void create__wrapper() {
        auto args = popStringArrayInternal();
        Handle__push(create(args));
    }
    void MainThreadFunc__push(std::function<MainThreadFunc> f) {
        size_t uniqueKey = 0;
        if (f) {
            MainThreadFunc* ptr_fun = f.target<MainThreadFunc>();
            if (ptr_fun != nullptr) {
                uniqueKey = (size_t)ptr_fun;
            }
        }
        auto wrapper = [f]() {
            f();
        };
        pushServerFuncVal(wrapper, uniqueKey);
    }

    std::function<MainThreadFunc> MainThreadFunc__pop() {
        auto id = ni_popClientFunc();
        auto cf = std::shared_ptr<ClientFuncVal>(new ClientFuncVal(id));
        auto wrapper = [cf]() {
            cf->remoteExec();
        };
        return wrapper;
    }

    void executeOnMainThread__wrapper() {
        auto func = MainThreadFunc__pop();
        executeOnMainThread(func);
    }

    int __register() {
        auto m = ni_registerModule("Application");
        ni_registerModuleMethod(m, "setStyle", &setStyle__wrapper);
        ni_registerModuleMethod(m, "exec", &exec__wrapper);
        ni_registerModuleMethod(m, "quit", &quit__wrapper);
        ni_registerModuleMethod(m, "availableStyles", &availableStyles__wrapper);
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "executeOnMainThread", &executeOnMainThread__wrapper);
        ni_registerModuleMethod(m, "Handle_setQuitOnLastWindowClosed", &Handle_setQuitOnLastWindowClosed__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        return 0; // = OK
    }
}
