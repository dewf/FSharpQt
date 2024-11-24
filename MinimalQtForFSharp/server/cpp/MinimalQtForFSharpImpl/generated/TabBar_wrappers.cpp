#include "../support/NativeImplServer.h"
#include "TabBar_wrappers.h"
#include "TabBar.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace TabBar
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
    ni_InterfaceMethodRef signalHandler_tabMoved;
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
        void tabMoved(int32_t fromIndex, int32_t toIndex) override {
            ni_pushInt32(toIndex);
            ni_pushInt32(fromIndex);
            invokeMethod(signalHandler_tabMoved);
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

    void SignalHandler_tabMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto fromIndex = ni_popInt32();
        auto toIndex = ni_popInt32();
        inst->tabMoved(fromIndex, toIndex);
    }
    void ButtonPosition__push(ButtonPosition value) {
        ni_pushInt32((int32_t)value);
    }

    ButtonPosition ButtonPosition__pop() {
        auto tag = ni_popInt32();
        return (ButtonPosition)tag;
    }
    void SelectionBehavior__push(SelectionBehavior value) {
        ni_pushInt32((int32_t)value);
    }

    SelectionBehavior SelectionBehavior__pop() {
        auto tag = ni_popInt32();
        return (SelectionBehavior)tag;
    }
    void Shape__push(Shape value) {
        ni_pushInt32((int32_t)value);
    }

    Shape Shape__pop() {
        auto tag = ni_popInt32();
        return (Shape)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAutoHide__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setAutoHide(_this, value);
    }

    void Handle_setChangeCurrentOnDrag__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setChangeCurrentOnDrag(_this, value);
    }

    void Handle_count__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_count(_this));
    }

    void Handle_setCurrentIndex__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setCurrentIndex(_this, value);
    }

    void Handle_currentIndex__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_currentIndex(_this));
    }

    void Handle_setDocumentMode__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setDocumentMode(_this, value);
    }

    void Handle_setDrawBase__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setDrawBase(_this, value);
    }

    void Handle_setElideMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = TextElideMode__pop();
        Handle_setElideMode(_this, mode);
    }

    void Handle_setExpanding__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setExpanding(_this, value);
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

    void Handle_setSelectionBehaviorOnRemove__wrapper() {
        auto _this = Handle__pop();
        auto value = SelectionBehavior__pop();
        Handle_setSelectionBehaviorOnRemove(_this, value);
    }

    void Handle_setShape__wrapper() {
        auto _this = Handle__pop();
        auto shape = Shape__pop();
        Handle_setShape(_this, shape);
    }

    void Handle_setTabsClosable__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setTabsClosable(_this, value);
    }

    void Handle_setUsesScrollButtons__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setUsesScrollButtons(_this, value);
    }

    void Handle_removeAllTabs__wrapper() {
        auto _this = Handle__pop();
        Handle_removeAllTabs(_this);
    }

    void Handle_addTab__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        ni_pushInt32(Handle_addTab(_this, text));
    }

    void Handle_addTab_overload1__wrapper() {
        auto _this = Handle__pop();
        auto icon = Icon::Handle__pop();
        auto text = popStringInternal();
        ni_pushInt32(Handle_addTab(_this, icon, text));
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
        auto m = ni_registerModule("TabBar");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoHide", &Handle_setAutoHide__wrapper);
        ni_registerModuleMethod(m, "Handle_setChangeCurrentOnDrag", &Handle_setChangeCurrentOnDrag__wrapper);
        ni_registerModuleMethod(m, "Handle_count", &Handle_count__wrapper);
        ni_registerModuleMethod(m, "Handle_setCurrentIndex", &Handle_setCurrentIndex__wrapper);
        ni_registerModuleMethod(m, "Handle_currentIndex", &Handle_currentIndex__wrapper);
        ni_registerModuleMethod(m, "Handle_setDocumentMode", &Handle_setDocumentMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setDrawBase", &Handle_setDrawBase__wrapper);
        ni_registerModuleMethod(m, "Handle_setElideMode", &Handle_setElideMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setExpanding", &Handle_setExpanding__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setMovable", &Handle_setMovable__wrapper);
        ni_registerModuleMethod(m, "Handle_setSelectionBehaviorOnRemove", &Handle_setSelectionBehaviorOnRemove__wrapper);
        ni_registerModuleMethod(m, "Handle_setShape", &Handle_setShape__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabsClosable", &Handle_setTabsClosable__wrapper);
        ni_registerModuleMethod(m, "Handle_setUsesScrollButtons", &Handle_setUsesScrollButtons__wrapper);
        ni_registerModuleMethod(m, "Handle_removeAllTabs", &Handle_removeAllTabs__wrapper);
        ni_registerModuleMethod(m, "Handle_addTab", &Handle_addTab__wrapper);
        ni_registerModuleMethod(m, "Handle_addTab_overload1", &Handle_addTab_overload1__wrapper);
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
        signalHandler_tabMoved = ni_registerInterfaceMethod(signalHandler, "tabMoved", &SignalHandler_tabMoved__wrapper);
        return 0; // = OK
    }
}
