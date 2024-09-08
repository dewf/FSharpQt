#include "../support/NativeImplServer.h"
#include "Menu_wrappers.h"
#include "Menu.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Action_wrappers.h"
using namespace ::Action;

#include "Icon_wrappers.h"
using namespace ::Icon;

namespace Menu
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_aboutToHide;
    ni_InterfaceMethodRef signalHandler_aboutToShow;
    ni_InterfaceMethodRef signalHandler_hovered;
    ni_InterfaceMethodRef signalHandler_triggered;
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
        void aboutToHide() override {
            invokeMethod(signalHandler_aboutToHide);
        }
        void aboutToShow() override {
            invokeMethod(signalHandler_aboutToShow);
        }
        void hovered(Action::HandleRef action) override {
            Action::Handle__push(action);
            invokeMethod(signalHandler_hovered);
        }
        void triggered(Action::HandleRef action) override {
            Action::Handle__push(action);
            invokeMethod(signalHandler_triggered);
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

    void SignalHandler_aboutToHide__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->aboutToHide();
    }

    void SignalHandler_aboutToShow__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->aboutToShow();
    }

    void SignalHandler_hovered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto action = Action::Handle__pop();
        inst->hovered(action);
    }

    void SignalHandler_triggered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto action = Action::Handle__pop();
        inst->triggered(action);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setIcon__wrapper() {
        auto _this = Handle__pop();
        auto icon = Deferred__pop();
        Handle_setIcon(_this, icon);
    }

    void Handle_setSeparatorsCollapsible__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setSeparatorsCollapsible(_this, state);
    }

    void Handle_setTearOffEnabled__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setTearOffEnabled(_this, state);
    }

    void Handle_setTitle__wrapper() {
        auto _this = Handle__pop();
        auto title = popStringInternal();
        Handle_setTitle(_this, title);
    }

    void Handle_setToolTipsVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setToolTipsVisible(_this, visible);
    }

    void Handle_clear__wrapper() {
        auto _this = Handle__pop();
        Handle_clear(_this);
    }

    void Handle_addSeparator__wrapper() {
        auto _this = Handle__pop();
        Action::Handle__push(Handle_addSeparator(_this));
    }

    void Handle_popup__wrapper() {
        auto _this = Handle__pop();
        auto p = Point__pop();
        Handle_popup(_this, p);
    }

    void Handle_setSignalMask__wrapper() {
        auto _this = Handle__pop();
        auto mask = SignalMask__pop();
        Handle_setSignalMask(_this, mask);
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    void create__wrapper() {
        auto handler = SignalHandler__pop();
        Handle__push(create(handler));
    }

    int __register() {
        auto m = ni_registerModule("Menu");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setIcon", &Handle_setIcon__wrapper);
        ni_registerModuleMethod(m, "Handle_setSeparatorsCollapsible", &Handle_setSeparatorsCollapsible__wrapper);
        ni_registerModuleMethod(m, "Handle_setTearOffEnabled", &Handle_setTearOffEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setTitle", &Handle_setTitle__wrapper);
        ni_registerModuleMethod(m, "Handle_setToolTipsVisible", &Handle_setToolTipsVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_clear", &Handle_clear__wrapper);
        ni_registerModuleMethod(m, "Handle_addSeparator", &Handle_addSeparator__wrapper);
        ni_registerModuleMethod(m, "Handle_popup", &Handle_popup__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_aboutToHide = ni_registerInterfaceMethod(signalHandler, "aboutToHide", &SignalHandler_aboutToHide__wrapper);
        signalHandler_aboutToShow = ni_registerInterfaceMethod(signalHandler, "aboutToShow", &SignalHandler_aboutToShow__wrapper);
        signalHandler_hovered = ni_registerInterfaceMethod(signalHandler, "hovered", &SignalHandler_hovered__wrapper);
        signalHandler_triggered = ni_registerInterfaceMethod(signalHandler, "triggered", &SignalHandler_triggered__wrapper);
        return 0; // = OK
    }
}
