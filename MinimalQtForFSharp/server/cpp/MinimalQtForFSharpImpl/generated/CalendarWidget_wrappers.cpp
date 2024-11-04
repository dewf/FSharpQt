#include "../support/NativeImplServer.h"
#include "CalendarWidget_wrappers.h"
#include "CalendarWidget.h"

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Object_wrappers.h"
using namespace ::Object;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Date_wrappers.h"
using namespace ::Date;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace CalendarWidget
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_activated;
    ni_InterfaceMethodRef signalHandler_clicked;
    ni_InterfaceMethodRef signalHandler_currentPageChanged;
    ni_InterfaceMethodRef signalHandler_selectionChanged;
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
        void activated(Date::HandleRef date) override {
            Date::Handle__push(date);
            invokeMethod(signalHandler_activated);
        }
        void clicked(Date::HandleRef date) override {
            Date::Handle__push(date);
            invokeMethod(signalHandler_clicked);
        }
        void currentPageChanged(int32_t year, int32_t month) override {
            ni_pushInt32(month);
            ni_pushInt32(year);
            invokeMethod(signalHandler_currentPageChanged);
        }
        void selectionChanged() override {
            invokeMethod(signalHandler_selectionChanged);
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
        auto date = Date::Handle__pop();
        inst->activated(date);
    }

    void SignalHandler_clicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto date = Date::Handle__pop();
        inst->clicked(date);
    }

    void SignalHandler_currentPageChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto year = ni_popInt32();
        auto month = ni_popInt32();
        inst->currentPageChanged(year, month);
    }

    void SignalHandler_selectionChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->selectionChanged();
    }
    void HorizontalHeaderFormat__push(HorizontalHeaderFormat value) {
        ni_pushInt32((int32_t)value);
    }

    HorizontalHeaderFormat HorizontalHeaderFormat__pop() {
        auto tag = ni_popInt32();
        return (HorizontalHeaderFormat)tag;
    }
    void VerticalHeaderFormat__push(VerticalHeaderFormat value) {
        ni_pushInt32((int32_t)value);
    }

    VerticalHeaderFormat VerticalHeaderFormat__pop() {
        auto tag = ni_popInt32();
        return (VerticalHeaderFormat)tag;
    }
    void SelectionMode__push(SelectionMode value) {
        ni_pushInt32((int32_t)value);
    }

    SelectionMode SelectionMode__pop() {
        auto tag = ni_popInt32();
        return (SelectionMode)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setDateEditAcceptDelay__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setDateEditAcceptDelay(_this, value);
    }

    void Handle_setDateEditEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setDateEditEnabled(_this, enabled);
    }

    void Handle_setFirstDayOfWeek__wrapper() {
        auto _this = Handle__pop();
        auto value = QDayOfWeek__pop();
        Handle_setFirstDayOfWeek(_this, value);
    }

    void Handle_setGridVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setGridVisible(_this, visible);
    }

    void Handle_setHorizontalHeaderFormat__wrapper() {
        auto _this = Handle__pop();
        auto format = HorizontalHeaderFormat__pop();
        Handle_setHorizontalHeaderFormat(_this, format);
    }

    void Handle_setMaximumDate__wrapper() {
        auto _this = Handle__pop();
        auto value = Date::Deferred__pop();
        Handle_setMaximumDate(_this, value);
    }

    void Handle_setMinimumDate__wrapper() {
        auto _this = Handle__pop();
        auto value = Date::Deferred__pop();
        Handle_setMinimumDate(_this, value);
    }

    void Handle_setNavigationBarVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setNavigationBarVisible(_this, visible);
    }

    void Handle_selectedDate__wrapper() {
        auto _this = Handle__pop();
        Owned__push(Handle_selectedDate(_this));
    }

    void Handle_setSelectedDate__wrapper() {
        auto _this = Handle__pop();
        auto selected = Date::Deferred__pop();
        Handle_setSelectedDate(_this, selected);
    }

    void Handle_setSelectionMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = SelectionMode__pop();
        Handle_setSelectionMode(_this, mode);
    }

    void Handle_setVerticalHeaderFormat__wrapper() {
        auto _this = Handle__pop();
        auto format = VerticalHeaderFormat__pop();
        Handle_setVerticalHeaderFormat(_this, format);
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
        auto m = ni_registerModule("CalendarWidget");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setDateEditAcceptDelay", &Handle_setDateEditAcceptDelay__wrapper);
        ni_registerModuleMethod(m, "Handle_setDateEditEnabled", &Handle_setDateEditEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setFirstDayOfWeek", &Handle_setFirstDayOfWeek__wrapper);
        ni_registerModuleMethod(m, "Handle_setGridVisible", &Handle_setGridVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_setHorizontalHeaderFormat", &Handle_setHorizontalHeaderFormat__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximumDate", &Handle_setMaximumDate__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimumDate", &Handle_setMinimumDate__wrapper);
        ni_registerModuleMethod(m, "Handle_setNavigationBarVisible", &Handle_setNavigationBarVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_selectedDate", &Handle_selectedDate__wrapper);
        ni_registerModuleMethod(m, "Handle_setSelectedDate", &Handle_setSelectedDate__wrapper);
        ni_registerModuleMethod(m, "Handle_setSelectionMode", &Handle_setSelectionMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setVerticalHeaderFormat", &Handle_setVerticalHeaderFormat__wrapper);
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
        signalHandler_currentPageChanged = ni_registerInterfaceMethod(signalHandler, "currentPageChanged", &SignalHandler_currentPageChanged__wrapper);
        signalHandler_selectionChanged = ni_registerInterfaceMethod(signalHandler, "selectionChanged", &SignalHandler_selectionChanged__wrapper);
        return 0; // = OK
    }
}
