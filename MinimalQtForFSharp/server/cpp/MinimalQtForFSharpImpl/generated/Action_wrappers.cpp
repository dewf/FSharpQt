#include "../support/NativeImplServer.h"
#include "Action_wrappers.h"
#include "Action.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "KeySequence_wrappers.h"
using namespace ::KeySequence;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace Action
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_changed;
    ni_InterfaceMethodRef signalHandler_checkableChanged;
    ni_InterfaceMethodRef signalHandler_enabledChanged;
    ni_InterfaceMethodRef signalHandler_hovered;
    ni_InterfaceMethodRef signalHandler_toggled;
    ni_InterfaceMethodRef signalHandler_triggered;
    ni_InterfaceMethodRef signalHandler_visibleChanged;
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
        void changed() override {
            invokeMethod(signalHandler_changed);
        }
        void checkableChanged(bool checkable) override {
            ni_pushBool(checkable);
            invokeMethod(signalHandler_checkableChanged);
        }
        void enabledChanged(bool enabled) override {
            ni_pushBool(enabled);
            invokeMethod(signalHandler_enabledChanged);
        }
        void hovered() override {
            invokeMethod(signalHandler_hovered);
        }
        void toggled(bool checked_) override {
            ni_pushBool(checked_);
            invokeMethod(signalHandler_toggled);
        }
        void triggered(bool checked_) override {
            ni_pushBool(checked_);
            invokeMethod(signalHandler_triggered);
        }
        void visibleChanged() override {
            invokeMethod(signalHandler_visibleChanged);
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

    void SignalHandler_changed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->changed();
    }

    void SignalHandler_checkableChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto checkable = ni_popBool();
        inst->checkableChanged(checkable);
    }

    void SignalHandler_enabledChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto enabled = ni_popBool();
        inst->enabledChanged(enabled);
    }

    void SignalHandler_hovered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->hovered();
    }

    void SignalHandler_toggled__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto checked_ = ni_popBool();
        inst->toggled(checked_);
    }

    void SignalHandler_triggered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto checked_ = ni_popBool();
        inst->triggered(checked_);
    }

    void SignalHandler_visibleChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->visibleChanged();
    }
    void MenuRole__push(MenuRole value) {
        ni_pushInt32((int32_t)value);
    }

    MenuRole MenuRole__pop() {
        auto tag = ni_popInt32();
        return (MenuRole)tag;
    }
    void Priority__push(Priority value) {
        ni_pushInt32((int32_t)value);
    }

    Priority Priority__pop() {
        auto tag = ni_popInt32();
        return (Priority)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAutoRepeat__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAutoRepeat(_this, state);
    }

    void Handle_setCheckable__wrapper() {
        auto _this = Handle__pop();
        auto checkable = ni_popBool();
        Handle_setCheckable(_this, checkable);
    }

    void Handle_setChecked__wrapper() {
        auto _this = Handle__pop();
        auto checked_ = ni_popBool();
        Handle_setChecked(_this, checked_);
    }

    void Handle_setEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setEnabled(_this, enabled);
    }

    void Handle_setIcon__wrapper() {
        auto _this = Handle__pop();
        auto icon = Icon::Deferred__pop();
        Handle_setIcon(_this, icon);
    }

    void Handle_setIconText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setIconText(_this, text);
    }

    void Handle_setIconVisibleInMenu__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setIconVisibleInMenu(_this, visible);
    }

    void Handle_setMenuRole__wrapper() {
        auto _this = Handle__pop();
        auto role = MenuRole__pop();
        Handle_setMenuRole(_this, role);
    }

    void Handle_setPriority__wrapper() {
        auto _this = Handle__pop();
        auto priority = Priority__pop();
        Handle_setPriority(_this, priority);
    }

    void Handle_setShortcut__wrapper() {
        auto _this = Handle__pop();
        auto shortcut = KeySequence::Deferred__pop();
        Handle_setShortcut(_this, shortcut);
    }

    void Handle_setShortcutContext__wrapper() {
        auto _this = Handle__pop();
        auto context = ShortcutContext__pop();
        Handle_setShortcutContext(_this, context);
    }

    void Handle_setShortcutVisibleInContextMenu__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setShortcutVisibleInContextMenu(_this, visible);
    }

    void Handle_setStatusTip__wrapper() {
        auto _this = Handle__pop();
        auto tip = popStringInternal();
        Handle_setStatusTip(_this, tip);
    }

    void Handle_setText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setText(_this, text);
    }

    void Handle_setToolTip__wrapper() {
        auto _this = Handle__pop();
        auto tip = popStringInternal();
        Handle_setToolTip(_this, tip);
    }

    void Handle_setVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setVisible(_this, visible);
    }

    void Handle_setWhatsThis__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setWhatsThis(_this, text);
    }

    void Handle_setSeparator__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setSeparator(_this, state);
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
        auto owner = Object::Handle__pop();
        auto handler = SignalHandler__pop();
        Handle__push(create(owner, handler));
    }

    int __register() {
        auto m = ni_registerModule("Action");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoRepeat", &Handle_setAutoRepeat__wrapper);
        ni_registerModuleMethod(m, "Handle_setCheckable", &Handle_setCheckable__wrapper);
        ni_registerModuleMethod(m, "Handle_setChecked", &Handle_setChecked__wrapper);
        ni_registerModuleMethod(m, "Handle_setEnabled", &Handle_setEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setIcon", &Handle_setIcon__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconText", &Handle_setIconText__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconVisibleInMenu", &Handle_setIconVisibleInMenu__wrapper);
        ni_registerModuleMethod(m, "Handle_setMenuRole", &Handle_setMenuRole__wrapper);
        ni_registerModuleMethod(m, "Handle_setPriority", &Handle_setPriority__wrapper);
        ni_registerModuleMethod(m, "Handle_setShortcut", &Handle_setShortcut__wrapper);
        ni_registerModuleMethod(m, "Handle_setShortcutContext", &Handle_setShortcutContext__wrapper);
        ni_registerModuleMethod(m, "Handle_setShortcutVisibleInContextMenu", &Handle_setShortcutVisibleInContextMenu__wrapper);
        ni_registerModuleMethod(m, "Handle_setStatusTip", &Handle_setStatusTip__wrapper);
        ni_registerModuleMethod(m, "Handle_setText", &Handle_setText__wrapper);
        ni_registerModuleMethod(m, "Handle_setToolTip", &Handle_setToolTip__wrapper);
        ni_registerModuleMethod(m, "Handle_setVisible", &Handle_setVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_setWhatsThis", &Handle_setWhatsThis__wrapper);
        ni_registerModuleMethod(m, "Handle_setSeparator", &Handle_setSeparator__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_changed = ni_registerInterfaceMethod(signalHandler, "changed", &SignalHandler_changed__wrapper);
        signalHandler_checkableChanged = ni_registerInterfaceMethod(signalHandler, "checkableChanged", &SignalHandler_checkableChanged__wrapper);
        signalHandler_enabledChanged = ni_registerInterfaceMethod(signalHandler, "enabledChanged", &SignalHandler_enabledChanged__wrapper);
        signalHandler_hovered = ni_registerInterfaceMethod(signalHandler, "hovered", &SignalHandler_hovered__wrapper);
        signalHandler_toggled = ni_registerInterfaceMethod(signalHandler, "toggled", &SignalHandler_toggled__wrapper);
        signalHandler_triggered = ni_registerInterfaceMethod(signalHandler, "triggered", &SignalHandler_triggered__wrapper);
        signalHandler_visibleChanged = ni_registerInterfaceMethod(signalHandler, "visibleChanged", &SignalHandler_visibleChanged__wrapper);
        return 0; // = OK
    }
}
