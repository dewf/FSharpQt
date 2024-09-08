#include "../support/NativeImplServer.h"
#include "MainWindow_wrappers.h"
#include "MainWindow.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Layout_wrappers.h"
using namespace ::Layout;

#include "MenuBar_wrappers.h"
using namespace ::MenuBar;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "DockWidget_wrappers.h"
using namespace ::DockWidget;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "ToolBar_wrappers.h"
using namespace ::ToolBar;

#include "StatusBar_wrappers.h"
using namespace ::StatusBar;

#include "TabWidget_wrappers.h"
using namespace ::TabWidget;

namespace MainWindow
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_iconSizeChanged;
    ni_InterfaceMethodRef signalHandler_tabifiedDockWidgetActivated;
    ni_InterfaceMethodRef signalHandler_toolButtonStyleChanged;
    ni_InterfaceMethodRef signalHandler_windowClosed;
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
        void iconSizeChanged(Size iconSize) override {
            Size__push(iconSize, false);
            invokeMethod(signalHandler_iconSizeChanged);
        }
        void tabifiedDockWidgetActivated(DockWidget::HandleRef dockWidget) override {
            DockWidget::Handle__push(dockWidget);
            invokeMethod(signalHandler_tabifiedDockWidgetActivated);
        }
        void toolButtonStyleChanged(ToolButtonStyle style) override {
            ToolButtonStyle__push(style);
            invokeMethod(signalHandler_toolButtonStyleChanged);
        }
        void windowClosed() override {
            invokeMethod(signalHandler_windowClosed);
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

    void SignalHandler_iconSizeChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto iconSize = Size__pop();
        inst->iconSizeChanged(iconSize);
    }

    void SignalHandler_tabifiedDockWidgetActivated__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto dockWidget = DockWidget::Handle__pop();
        inst->tabifiedDockWidgetActivated(dockWidget);
    }

    void SignalHandler_toolButtonStyleChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto style = ToolButtonStyle__pop();
        inst->toolButtonStyleChanged(style);
    }

    void SignalHandler_windowClosed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->windowClosed();
    }
    void DockOptions__push(DockOptions value) {
        ni_pushInt32(value);
    }

    DockOptions DockOptions__pop() {
        return ni_popInt32();
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAnimated__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAnimated(_this, state);
    }

    void Handle_setDockNestingEnabled__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setDockNestingEnabled(_this, state);
    }

    void Handle_setDockOptions__wrapper() {
        auto _this = Handle__pop();
        auto dockOptions = DockOptions__pop();
        Handle_setDockOptions(_this, dockOptions);
    }

    void Handle_setDocumentMode__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setDocumentMode(_this, state);
    }

    void Handle_setIconSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setIconSize(_this, size);
    }

    void Handle_setTabShape__wrapper() {
        auto _this = Handle__pop();
        auto tabShape = TabShape__pop();
        Handle_setTabShape(_this, tabShape);
    }

    void Handle_setToolButtonStyle__wrapper() {
        auto _this = Handle__pop();
        auto style = ToolButtonStyle__pop();
        Handle_setToolButtonStyle(_this, style);
    }

    void Handle_setUnifiedTitleAndToolBarOnMac__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setUnifiedTitleAndToolBarOnMac(_this, state);
    }

    void Handle_setCentralWidget__wrapper() {
        auto _this = Handle__pop();
        auto widget = Widget::Handle__pop();
        Handle_setCentralWidget(_this, widget);
    }

    void Handle_setMenuBar__wrapper() {
        auto _this = Handle__pop();
        auto menubar = MenuBar::Handle__pop();
        Handle_setMenuBar(_this, menubar);
    }

    void Handle_setStatusBar__wrapper() {
        auto _this = Handle__pop();
        auto statusbar = StatusBar::Handle__pop();
        Handle_setStatusBar(_this, statusbar);
    }

    void Handle_addToolBar__wrapper() {
        auto _this = Handle__pop();
        auto toolbar = ToolBar::Handle__pop();
        Handle_addToolBar(_this, toolbar);
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
        auto m = ni_registerModule("MainWindow");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAnimated", &Handle_setAnimated__wrapper);
        ni_registerModuleMethod(m, "Handle_setDockNestingEnabled", &Handle_setDockNestingEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setDockOptions", &Handle_setDockOptions__wrapper);
        ni_registerModuleMethod(m, "Handle_setDocumentMode", &Handle_setDocumentMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabShape", &Handle_setTabShape__wrapper);
        ni_registerModuleMethod(m, "Handle_setToolButtonStyle", &Handle_setToolButtonStyle__wrapper);
        ni_registerModuleMethod(m, "Handle_setUnifiedTitleAndToolBarOnMac", &Handle_setUnifiedTitleAndToolBarOnMac__wrapper);
        ni_registerModuleMethod(m, "Handle_setCentralWidget", &Handle_setCentralWidget__wrapper);
        ni_registerModuleMethod(m, "Handle_setMenuBar", &Handle_setMenuBar__wrapper);
        ni_registerModuleMethod(m, "Handle_setStatusBar", &Handle_setStatusBar__wrapper);
        ni_registerModuleMethod(m, "Handle_addToolBar", &Handle_addToolBar__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_iconSizeChanged = ni_registerInterfaceMethod(signalHandler, "iconSizeChanged", &SignalHandler_iconSizeChanged__wrapper);
        signalHandler_tabifiedDockWidgetActivated = ni_registerInterfaceMethod(signalHandler, "tabifiedDockWidgetActivated", &SignalHandler_tabifiedDockWidgetActivated__wrapper);
        signalHandler_toolButtonStyleChanged = ni_registerInterfaceMethod(signalHandler, "toolButtonStyleChanged", &SignalHandler_toolButtonStyleChanged__wrapper);
        signalHandler_windowClosed = ni_registerInterfaceMethod(signalHandler, "windowClosed", &SignalHandler_windowClosed__wrapper);
        return 0; // = OK
    }
}
