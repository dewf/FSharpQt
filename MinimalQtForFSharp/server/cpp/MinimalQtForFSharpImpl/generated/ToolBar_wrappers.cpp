#include "../support/NativeImplServer.h"
#include "ToolBar_wrappers.h"
#include "ToolBar.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Action_wrappers.h"
using namespace ::Action;

namespace ToolBar
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_actionTriggered;
    ni_InterfaceMethodRef signalHandler_allowedAreasChanged;
    ni_InterfaceMethodRef signalHandler_iconSizeChanged;
    ni_InterfaceMethodRef signalHandler_movableChanged;
    ni_InterfaceMethodRef signalHandler_orientationChanged;
    ni_InterfaceMethodRef signalHandler_toolButtonStyleChanged;
    ni_InterfaceMethodRef signalHandler_topLevelChanged;
    ni_InterfaceMethodRef signalHandler_visibilityChanged;
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
        void actionTriggered(Action::HandleRef action) override {
            Action::Handle__push(action);
            invokeMethod(signalHandler_actionTriggered);
        }
        void allowedAreasChanged(ToolBarAreas allowed) override {
            ToolBarAreas__push(allowed);
            invokeMethod(signalHandler_allowedAreasChanged);
        }
        void iconSizeChanged(Size size) override {
            Size__push(size, false);
            invokeMethod(signalHandler_iconSizeChanged);
        }
        void movableChanged(bool movable) override {
            ni_pushBool(movable);
            invokeMethod(signalHandler_movableChanged);
        }
        void orientationChanged(Orientation value) override {
            Orientation__push(value);
            invokeMethod(signalHandler_orientationChanged);
        }
        void toolButtonStyleChanged(ToolButtonStyle style) override {
            ToolButtonStyle__push(style);
            invokeMethod(signalHandler_toolButtonStyleChanged);
        }
        void topLevelChanged(bool topLevel) override {
            ni_pushBool(topLevel);
            invokeMethod(signalHandler_topLevelChanged);
        }
        void visibilityChanged(bool visible) override {
            ni_pushBool(visible);
            invokeMethod(signalHandler_visibilityChanged);
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

    void SignalHandler_actionTriggered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto action = Action::Handle__pop();
        inst->actionTriggered(action);
    }

    void SignalHandler_allowedAreasChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto allowed = ToolBarAreas__pop();
        inst->allowedAreasChanged(allowed);
    }

    void SignalHandler_iconSizeChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto size = Size__pop();
        inst->iconSizeChanged(size);
    }

    void SignalHandler_movableChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto movable = ni_popBool();
        inst->movableChanged(movable);
    }

    void SignalHandler_orientationChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto value = Orientation__pop();
        inst->orientationChanged(value);
    }

    void SignalHandler_toolButtonStyleChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto style = ToolButtonStyle__pop();
        inst->toolButtonStyleChanged(style);
    }

    void SignalHandler_topLevelChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto topLevel = ni_popBool();
        inst->topLevelChanged(topLevel);
    }

    void SignalHandler_visibilityChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto visible = ni_popBool();
        inst->visibilityChanged(visible);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAllowedAreas__wrapper() {
        auto _this = Handle__pop();
        auto allowed = ToolBarAreas__pop();
        Handle_setAllowedAreas(_this, allowed);
    }

    void Handle_setFloatable__wrapper() {
        auto _this = Handle__pop();
        auto floatable = ni_popBool();
        Handle_setFloatable(_this, floatable);
    }

    void Handle_isFloating__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isFloating(_this));
    }

    void Handle_setIconSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setIconSize(_this, size);
    }

    void Handle_setMovable__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setMovable(_this, value);
    }

    void Handle_setOrientation__wrapper() {
        auto _this = Handle__pop();
        auto value = Orientation__pop();
        Handle_setOrientation(_this, value);
    }

    void Handle_setToolButtonStyle__wrapper() {
        auto _this = Handle__pop();
        auto style = ToolButtonStyle__pop();
        Handle_setToolButtonStyle(_this, style);
    }

    void Handle_addSeparator__wrapper() {
        auto _this = Handle__pop();
        Action::Handle__push(Handle_addSeparator(_this));
    }

    void Handle_addWidget__wrapper() {
        auto _this = Handle__pop();
        auto widget = Widget::Handle__pop();
        Handle_addWidget(_this, widget);
    }

    void Handle_clear__wrapper() {
        auto _this = Handle__pop();
        Handle_clear(_this);
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
        auto m = ni_registerModule("ToolBar");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAllowedAreas", &Handle_setAllowedAreas__wrapper);
        ni_registerModuleMethod(m, "Handle_setFloatable", &Handle_setFloatable__wrapper);
        ni_registerModuleMethod(m, "Handle_isFloating", &Handle_isFloating__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setMovable", &Handle_setMovable__wrapper);
        ni_registerModuleMethod(m, "Handle_setOrientation", &Handle_setOrientation__wrapper);
        ni_registerModuleMethod(m, "Handle_setToolButtonStyle", &Handle_setToolButtonStyle__wrapper);
        ni_registerModuleMethod(m, "Handle_addSeparator", &Handle_addSeparator__wrapper);
        ni_registerModuleMethod(m, "Handle_addWidget", &Handle_addWidget__wrapper);
        ni_registerModuleMethod(m, "Handle_clear", &Handle_clear__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_actionTriggered = ni_registerInterfaceMethod(signalHandler, "actionTriggered", &SignalHandler_actionTriggered__wrapper);
        signalHandler_allowedAreasChanged = ni_registerInterfaceMethod(signalHandler, "allowedAreasChanged", &SignalHandler_allowedAreasChanged__wrapper);
        signalHandler_iconSizeChanged = ni_registerInterfaceMethod(signalHandler, "iconSizeChanged", &SignalHandler_iconSizeChanged__wrapper);
        signalHandler_movableChanged = ni_registerInterfaceMethod(signalHandler, "movableChanged", &SignalHandler_movableChanged__wrapper);
        signalHandler_orientationChanged = ni_registerInterfaceMethod(signalHandler, "orientationChanged", &SignalHandler_orientationChanged__wrapper);
        signalHandler_toolButtonStyleChanged = ni_registerInterfaceMethod(signalHandler, "toolButtonStyleChanged", &SignalHandler_toolButtonStyleChanged__wrapper);
        signalHandler_topLevelChanged = ni_registerInterfaceMethod(signalHandler, "topLevelChanged", &SignalHandler_topLevelChanged__wrapper);
        signalHandler_visibilityChanged = ni_registerInterfaceMethod(signalHandler, "visibilityChanged", &SignalHandler_visibilityChanged__wrapper);
        return 0; // = OK
    }
}
