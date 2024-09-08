#include "../support/NativeImplServer.h"
#include "TreeView_wrappers.h"
#include "TreeView.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "AbstractItemView_wrappers.h"
using namespace ::AbstractItemView;

#include "ModelIndex_wrappers.h"
using namespace ::ModelIndex;

namespace TreeView
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_activated;
    ni_InterfaceMethodRef signalHandler_clicked;
    ni_InterfaceMethodRef signalHandler_doubleClicked;
    ni_InterfaceMethodRef signalHandler_entered;
    ni_InterfaceMethodRef signalHandler_iconSizeChanged;
    ni_InterfaceMethodRef signalHandler_pressed;
    ni_InterfaceMethodRef signalHandler_viewportEntered;
    ni_InterfaceMethodRef signalHandler_collapsed;
    ni_InterfaceMethodRef signalHandler_expanded;
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
        void activated(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_activated);
        }
        void clicked(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_clicked);
        }
        void doubleClicked(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_doubleClicked);
        }
        void entered(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_entered);
        }
        void iconSizeChanged(Size size) override {
            Size__push(size, false);
            invokeMethod(signalHandler_iconSizeChanged);
        }
        void pressed(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_pressed);
        }
        void viewportEntered() override {
            invokeMethod(signalHandler_viewportEntered);
        }
        void collapsed(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_collapsed);
        }
        void expanded(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_expanded);
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

    void SignalHandler_activated__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->activated(index);
    }

    void SignalHandler_clicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->clicked(index);
    }

    void SignalHandler_doubleClicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->doubleClicked(index);
    }

    void SignalHandler_entered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->entered(index);
    }

    void SignalHandler_iconSizeChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto size = Size__pop();
        inst->iconSizeChanged(size);
    }

    void SignalHandler_pressed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->pressed(index);
    }

    void SignalHandler_viewportEntered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->viewportEntered();
    }

    void SignalHandler_collapsed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->collapsed(index);
    }

    void SignalHandler_expanded__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->expanded(index);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAllColumnsShowFocus__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setAllColumnsShowFocus(_this, value);
    }

    void Handle_setAnimated__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setAnimated(_this, value);
    }

    void Handle_setAutoExpandDelay__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setAutoExpandDelay(_this, value);
    }

    void Handle_setExpandsOnDoubleClick__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setExpandsOnDoubleClick(_this, value);
    }

    void Handle_setHeaderHidden__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setHeaderHidden(_this, value);
    }

    void Handle_setIndentation__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setIndentation(_this, value);
    }

    void Handle_setItemsExpandable__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setItemsExpandable(_this, value);
    }

    void Handle_setRootIsDecorated__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setRootIsDecorated(_this, value);
    }

    void Handle_setSortingEnabled__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setSortingEnabled(_this, value);
    }

    void Handle_setUniformRowHeights__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setUniformRowHeights(_this, value);
    }

    void Handle_setWordWrap__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setWordWrap(_this, value);
    }

    void Handle_resizeColumnToContents__wrapper() {
        auto _this = Handle__pop();
        auto column = ni_popInt32();
        Handle_resizeColumnToContents(_this, column);
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
        auto m = ni_registerModule("TreeView");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAllColumnsShowFocus", &Handle_setAllColumnsShowFocus__wrapper);
        ni_registerModuleMethod(m, "Handle_setAnimated", &Handle_setAnimated__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoExpandDelay", &Handle_setAutoExpandDelay__wrapper);
        ni_registerModuleMethod(m, "Handle_setExpandsOnDoubleClick", &Handle_setExpandsOnDoubleClick__wrapper);
        ni_registerModuleMethod(m, "Handle_setHeaderHidden", &Handle_setHeaderHidden__wrapper);
        ni_registerModuleMethod(m, "Handle_setIndentation", &Handle_setIndentation__wrapper);
        ni_registerModuleMethod(m, "Handle_setItemsExpandable", &Handle_setItemsExpandable__wrapper);
        ni_registerModuleMethod(m, "Handle_setRootIsDecorated", &Handle_setRootIsDecorated__wrapper);
        ni_registerModuleMethod(m, "Handle_setSortingEnabled", &Handle_setSortingEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setUniformRowHeights", &Handle_setUniformRowHeights__wrapper);
        ni_registerModuleMethod(m, "Handle_setWordWrap", &Handle_setWordWrap__wrapper);
        ni_registerModuleMethod(m, "Handle_resizeColumnToContents", &Handle_resizeColumnToContents__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_activated = ni_registerInterfaceMethod(signalHandler, "activated", &SignalHandler_activated__wrapper);
        signalHandler_clicked = ni_registerInterfaceMethod(signalHandler, "clicked", &SignalHandler_clicked__wrapper);
        signalHandler_doubleClicked = ni_registerInterfaceMethod(signalHandler, "doubleClicked", &SignalHandler_doubleClicked__wrapper);
        signalHandler_entered = ni_registerInterfaceMethod(signalHandler, "entered", &SignalHandler_entered__wrapper);
        signalHandler_iconSizeChanged = ni_registerInterfaceMethod(signalHandler, "iconSizeChanged", &SignalHandler_iconSizeChanged__wrapper);
        signalHandler_pressed = ni_registerInterfaceMethod(signalHandler, "pressed", &SignalHandler_pressed__wrapper);
        signalHandler_viewportEntered = ni_registerInterfaceMethod(signalHandler, "viewportEntered", &SignalHandler_viewportEntered__wrapper);
        signalHandler_collapsed = ni_registerInterfaceMethod(signalHandler, "collapsed", &SignalHandler_collapsed__wrapper);
        signalHandler_expanded = ni_registerInterfaceMethod(signalHandler, "expanded", &SignalHandler_expanded__wrapper);
        return 0; // = OK
    }
}
