#include "../support/NativeImplServer.h"
#include "Icon_wrappers.h"
#include "Icon.h"

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

    class Deferred_PushVisitor : public Deferred::Visitor {
    private:
        bool isReturn;
    public:
        Deferred_PushVisitor(bool isReturn) : isReturn(isReturn) {}
        void onEmpty(const Deferred::Empty* emptyValue) override {
            // kind:
            ni_pushInt32(0);
        }
        void onFromThemeIcon(const Deferred::FromThemeIcon* fromThemeIconValue) override {
            ThemeIcon__push(fromThemeIconValue->themeIcon);
            // kind:
            ni_pushInt32(1);
        }
        void onFromFilename(const Deferred::FromFilename* fromFilenameValue) override {
            pushStringInternal(fromFilenameValue->filename);
            // kind:
            ni_pushInt32(2);
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
            auto themeIcon = ThemeIcon__pop();
            __ret = new Deferred::FromThemeIcon(themeIcon);
            break;
        }
        case 2: {
            auto filename = popStringInternal();
            __ret = new Deferred::FromFilename(filename);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    int __register() {
        auto m = ni_registerModule("Icon");
        return 0; // = OK
    }
}
