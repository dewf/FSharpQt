#include "../support/NativeImplServer.h"
#include "AbstractButton_wrappers.h"
#include "AbstractButton.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "KeySequence_wrappers.h"
using namespace ::KeySequence;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Icon_wrappers.h"
using namespace ::Icon;

namespace AbstractButton
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_clicked;
    ni_InterfaceMethodRef signalHandler_pressed;
    ni_InterfaceMethodRef signalHandler_released;
    ni_InterfaceMethodRef signalHandler_toggled;
    void SignalMask__push(SignalMask value) {
        ni_pushInt32(value);
    }

    SignalMask SignalMask__pop() {
        return ni_popInt32();
    }
    static std::map<SignalHandler*, std::weak_ptr<Pushable>> __signalHandlerToPushable;

    class ServerSignalHandlerWrapper : public ServerObject {
    public:
        std::shared_ptr<SignalHandler> rawInterface;
    private:
        ServerSignalHandlerWrapper(std::shared_ptr<SignalHandler> raw) {
            this->rawInterface = raw;
        }
        void releaseExtra() override {
            __signalHandlerToPushable.erase(rawInterface.get());
        }
    public:
        static std::shared_ptr<ServerSignalHandlerWrapper> wrapAndRegister(std::shared_ptr<SignalHandler> raw) {
            auto ret = std::shared_ptr<ServerSignalHandlerWrapper>(new ServerSignalHandlerWrapper(raw));
            __signalHandlerToPushable[raw.get()] = ret;
            return ret;
        }
    };
    class ClientSignalHandler : public ClientObject, public SignalHandler {
    public:
        ClientSignalHandler(int id) : ClientObject(id) {}
        ~ClientSignalHandler() override {
            __signalHandlerToPushable.erase(this);
        }
        void destroyed(Object::HandleRef obj) override {
            Object::Handle__push(obj);
            invokeMethod(signalHandler_destroyed);
        }
        void objectNameChanged(std::string objectName) override {
            pushStringInternal(objectName);
            invokeMethod(signalHandler_objectNameChanged);
        }
        void customContextMenuRequested(Point pos) override {
            Point__push(pos, false);
            invokeMethod(signalHandler_customContextMenuRequested);
        }
        void windowIconChanged(Icon::HandleRef icon) override {
            Icon::Handle__push(icon);
            invokeMethod(signalHandler_windowIconChanged);
        }
        void windowTitleChanged(std::string title) override {
            pushStringInternal(title);
            invokeMethod(signalHandler_windowTitleChanged);
        }
        void clicked(bool checkState) override {
            ni_pushBool(checkState);
            invokeMethod(signalHandler_clicked);
        }
        void pressed() override {
            invokeMethod(signalHandler_pressed);
        }
        void released() override {
            invokeMethod(signalHandler_released);
        }
        void toggled(bool checkState) override {
            ni_pushBool(checkState);
            invokeMethod(signalHandler_toggled);
        }
    };

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn) {
        if (inst != nullptr) {
            auto found = __signalHandlerToPushable.find(inst.get());
            if (found != __signalHandlerToPushable.end()) {
                auto pushable = found->second.lock();
                pushable->push(pushable, isReturn);
            }
            else {
                auto pushable = ServerSignalHandlerWrapper::wrapAndRegister(inst);
                pushable->push(pushable, isReturn);
            }
        }
        else {
            ni_pushNull();
        }
    }

    std::shared_ptr<SignalHandler> SignalHandler__pop() {
        bool isClientID;
        auto id = ni_popInstance(&isClientID);
        if (id != 0) {
            if (isClientID) {
                auto ret = std::shared_ptr<SignalHandler>(new ClientSignalHandler(id));
                __signalHandlerToPushable[ret.get()] = std::dynamic_pointer_cast<Pushable>(ret);
                return ret;
            }
            else {
                auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(id));
                return wrapper->rawInterface;
            }
        }
        else {
            return std::shared_ptr<SignalHandler>();
        }
    }

    void SignalHandler_destroyed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto obj = Object::Handle__pop();
        inst->destroyed(obj);
    }

    void SignalHandler_objectNameChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto objectName = popStringInternal();
        inst->objectNameChanged(objectName);
    }

    void SignalHandler_customContextMenuRequested__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        inst->customContextMenuRequested(pos);
    }

    void SignalHandler_windowIconChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto icon = Icon::Handle__pop();
        inst->windowIconChanged(icon);
    }

    void SignalHandler_windowTitleChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto title = popStringInternal();
        inst->windowTitleChanged(title);
    }

    void SignalHandler_clicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto checkState = ni_popBool();
        inst->clicked(checkState);
    }

    void SignalHandler_pressed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->pressed();
    }

    void SignalHandler_released__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->released();
    }

    void SignalHandler_toggled__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto checkState = ni_popBool();
        inst->toggled(checkState);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAutoExclusive__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAutoExclusive(_this, state);
    }

    void Handle_setAutoRepeat__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAutoRepeat(_this, state);
    }

    void Handle_setAutoRepeatDelay__wrapper() {
        auto _this = Handle__pop();
        auto delay = ni_popInt32();
        Handle_setAutoRepeatDelay(_this, delay);
    }

    void Handle_setAutoRepeatInterval__wrapper() {
        auto _this = Handle__pop();
        auto interval = ni_popInt32();
        Handle_setAutoRepeatInterval(_this, interval);
    }

    void Handle_setCheckable__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setCheckable(_this, state);
    }

    void Handle_setChecked__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setChecked(_this, state);
    }

    void Handle_setDown__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setDown(_this, state);
    }

    void Handle_setIcon__wrapper() {
        auto _this = Handle__pop();
        auto icon = Icon::Deferred__pop();
        Handle_setIcon(_this, icon);
    }

    void Handle_setIconSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setIconSize(_this, size);
    }

    void Handle_setShortcut__wrapper() {
        auto _this = Handle__pop();
        auto seq = KeySequence::Deferred__pop();
        Handle_setShortcut(_this, seq);
    }

    void Handle_setText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setText(_this, text);
    }

    int __register() {
        auto m = ni_registerModule("AbstractButton");
        ni_registerModuleMethod(m, "Handle_setAutoExclusive", &Handle_setAutoExclusive__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoRepeat", &Handle_setAutoRepeat__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoRepeatDelay", &Handle_setAutoRepeatDelay__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoRepeatInterval", &Handle_setAutoRepeatInterval__wrapper);
        ni_registerModuleMethod(m, "Handle_setCheckable", &Handle_setCheckable__wrapper);
        ni_registerModuleMethod(m, "Handle_setChecked", &Handle_setChecked__wrapper);
        ni_registerModuleMethod(m, "Handle_setDown", &Handle_setDown__wrapper);
        ni_registerModuleMethod(m, "Handle_setIcon", &Handle_setIcon__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setShortcut", &Handle_setShortcut__wrapper);
        ni_registerModuleMethod(m, "Handle_setText", &Handle_setText__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_clicked = ni_registerInterfaceMethod(signalHandler, "clicked", &SignalHandler_clicked__wrapper);
        signalHandler_pressed = ni_registerInterfaceMethod(signalHandler, "pressed", &SignalHandler_pressed__wrapper);
        signalHandler_released = ni_registerInterfaceMethod(signalHandler, "released", &SignalHandler_released__wrapper);
        signalHandler_toggled = ni_registerInterfaceMethod(signalHandler, "toggled", &SignalHandler_toggled__wrapper);
        return 0; // = OK
    }
}
