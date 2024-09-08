#include "../support/NativeImplServer.h"
#include "TabWidget_wrappers.h"
#include "TabWidget.h"

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

namespace TabWidget
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_currentChanged;
    ni_InterfaceMethodRef signalHandler_tabBarClicked;
    ni_InterfaceMethodRef signalHandler_tabBarDoubleClicked;
    ni_InterfaceMethodRef signalHandler_tabCloseRequested;
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
        void currentChanged(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_currentChanged);
        }
        void tabBarClicked(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_tabBarClicked);
        }
        void tabBarDoubleClicked(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_tabBarDoubleClicked);
        }
        void tabCloseRequested(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_tabCloseRequested);
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

    void SignalHandler_currentChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ni_popInt32();
        inst->currentChanged(index);
    }

    void SignalHandler_tabBarClicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ni_popInt32();
        inst->tabBarClicked(index);
    }

    void SignalHandler_tabBarDoubleClicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ni_popInt32();
        inst->tabBarDoubleClicked(index);
    }

    void SignalHandler_tabCloseRequested__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ni_popInt32();
        inst->tabCloseRequested(index);
    }
    void TabShape__push(TabShape value) {
        ni_pushInt32((int32_t)value);
    }

    TabShape TabShape__pop() {
        auto tag = ni_popInt32();
        return (TabShape)tag;
    }
    void TabPosition__push(TabPosition value) {
        ni_pushInt32((int32_t)value);
    }

    TabPosition TabPosition__pop() {
        auto tag = ni_popInt32();
        return (TabPosition)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_count__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_count(_this));
    }

    void Handle_setCurrentIndex__wrapper() {
        auto _this = Handle__pop();
        auto index = ni_popInt32();
        Handle_setCurrentIndex(_this, index);
    }

    void Handle_setDocumentMode__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setDocumentMode(_this, state);
    }

    void Handle_setElideMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = TextElideMode__pop();
        Handle_setElideMode(_this, mode);
    }

    void Handle_setIconSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setIconSize(_this, size);
    }

    void Handle_setMovable__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setMovable(_this, state);
    }

    void Handle_setTabBarAutoHide__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setTabBarAutoHide(_this, state);
    }

    void Handle_setTabPosition__wrapper() {
        auto _this = Handle__pop();
        auto position = TabPosition__pop();
        Handle_setTabPosition(_this, position);
    }

    void Handle_setTabShape__wrapper() {
        auto _this = Handle__pop();
        auto shape = TabShape__pop();
        Handle_setTabShape(_this, shape);
    }

    void Handle_setTabsClosable__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setTabsClosable(_this, state);
    }

    void Handle_setUsesScrollButtons__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setUsesScrollButtons(_this, state);
    }

    void Handle_addTab__wrapper() {
        auto _this = Handle__pop();
        auto page = Widget::Handle__pop();
        auto label = popStringInternal();
        Handle_addTab(_this, page, label);
    }

    void Handle_insertTab__wrapper() {
        auto _this = Handle__pop();
        auto index = ni_popInt32();
        auto page = Widget::Handle__pop();
        auto label = popStringInternal();
        Handle_insertTab(_this, index, page, label);
    }

    void Handle_widgetAt__wrapper() {
        auto _this = Handle__pop();
        auto index = ni_popInt32();
        Widget::Handle__push(Handle_widgetAt(_this, index));
    }

    void Handle_clear__wrapper() {
        auto _this = Handle__pop();
        Handle_clear(_this);
    }

    void Handle_removeTab__wrapper() {
        auto _this = Handle__pop();
        auto index = ni_popInt32();
        Handle_removeTab(_this, index);
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
        auto m = ni_registerModule("TabWidget");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_count", &Handle_count__wrapper);
        ni_registerModuleMethod(m, "Handle_setCurrentIndex", &Handle_setCurrentIndex__wrapper);
        ni_registerModuleMethod(m, "Handle_setDocumentMode", &Handle_setDocumentMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setElideMode", &Handle_setElideMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setMovable", &Handle_setMovable__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabBarAutoHide", &Handle_setTabBarAutoHide__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabPosition", &Handle_setTabPosition__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabShape", &Handle_setTabShape__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabsClosable", &Handle_setTabsClosable__wrapper);
        ni_registerModuleMethod(m, "Handle_setUsesScrollButtons", &Handle_setUsesScrollButtons__wrapper);
        ni_registerModuleMethod(m, "Handle_addTab", &Handle_addTab__wrapper);
        ni_registerModuleMethod(m, "Handle_insertTab", &Handle_insertTab__wrapper);
        ni_registerModuleMethod(m, "Handle_widgetAt", &Handle_widgetAt__wrapper);
        ni_registerModuleMethod(m, "Handle_clear", &Handle_clear__wrapper);
        ni_registerModuleMethod(m, "Handle_removeTab", &Handle_removeTab__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_currentChanged = ni_registerInterfaceMethod(signalHandler, "currentChanged", &SignalHandler_currentChanged__wrapper);
        signalHandler_tabBarClicked = ni_registerInterfaceMethod(signalHandler, "tabBarClicked", &SignalHandler_tabBarClicked__wrapper);
        signalHandler_tabBarDoubleClicked = ni_registerInterfaceMethod(signalHandler, "tabBarDoubleClicked", &SignalHandler_tabBarDoubleClicked__wrapper);
        signalHandler_tabCloseRequested = ni_registerInterfaceMethod(signalHandler, "tabCloseRequested", &SignalHandler_tabCloseRequested__wrapper);
        return 0; // = OK
    }
}
