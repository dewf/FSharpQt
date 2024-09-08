#include "../support/NativeImplServer.h"
#include "MessageBox_wrappers.h"
#include "MessageBox.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Dialog_wrappers.h"
using namespace ::Dialog;

#include "AbstractButton_wrappers.h"
using namespace ::AbstractButton;

#include "Widget_wrappers.h"
using namespace ::Widget;

namespace MessageBox
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_accepted;
    ni_InterfaceMethodRef signalHandler_finished;
    ni_InterfaceMethodRef signalHandler_rejected;
    ni_InterfaceMethodRef signalHandler_buttonClicked;
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
        void accepted() override {
            invokeMethod(signalHandler_accepted);
        }
        void finished(int32_t result) override {
            ni_pushInt32(result);
            invokeMethod(signalHandler_finished);
        }
        void rejected() override {
            invokeMethod(signalHandler_rejected);
        }
        void buttonClicked(AbstractButton::HandleRef button) override {
            AbstractButton::Handle__push(button);
            invokeMethod(signalHandler_buttonClicked);
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

    void SignalHandler_accepted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->accepted();
    }

    void SignalHandler_finished__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto result = ni_popInt32();
        inst->finished(result);
    }

    void SignalHandler_rejected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->rejected();
    }

    void SignalHandler_buttonClicked__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto button = AbstractButton::Handle__pop();
        inst->buttonClicked(button);
    }
    void StandardButton__push(StandardButton value) {
        ni_pushInt32((int32_t)value);
    }

    StandardButton StandardButton__pop() {
        auto tag = ni_popInt32();
        return (StandardButton)tag;
    }
    void StandardButtonSet__push(StandardButtonSet value) {
        ni_pushInt32(value);
    }

    StandardButtonSet StandardButtonSet__pop() {
        return ni_popInt32();
    }
    void MessageBoxIcon__push(MessageBoxIcon value) {
        ni_pushInt32((int32_t)value);
    }

    MessageBoxIcon MessageBoxIcon__pop() {
        auto tag = ni_popInt32();
        return (MessageBoxIcon)tag;
    }
    void Options__push(Options value) {
        ni_pushInt32(value);
    }

    Options Options__pop() {
        return ni_popInt32();
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setDetailedText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setDetailedText(_this, text);
    }

    void Handle_setIcon__wrapper() {
        auto _this = Handle__pop();
        auto icon = MessageBoxIcon__pop();
        Handle_setIcon(_this, icon);
    }

    void Handle_setInformativeText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setInformativeText(_this, text);
    }

    void Handle_setOptions__wrapper() {
        auto _this = Handle__pop();
        auto opts = Options__pop();
        Handle_setOptions(_this, opts);
    }

    void Handle_setStandardButtons__wrapper() {
        auto _this = Handle__pop();
        auto buttons = StandardButtonSet__pop();
        Handle_setStandardButtons(_this, buttons);
    }

    void Handle_setText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setText(_this, text);
    }

    void Handle_setTextFormat__wrapper() {
        auto _this = Handle__pop();
        auto format = TextFormat__pop();
        Handle_setTextFormat(_this, format);
    }

    void Handle_setTextInteractionFlags__wrapper() {
        auto _this = Handle__pop();
        auto tiFlags = TextInteractionFlags__pop();
        Handle_setTextInteractionFlags(_this, tiFlags);
    }

    void Handle_setDefaultButton__wrapper() {
        auto _this = Handle__pop();
        auto button = StandardButton__pop();
        Handle_setDefaultButton(_this, button);
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
        auto parent = Widget::Handle__pop();
        auto handler = SignalHandler__pop();
        Handle__push(create(parent, handler));
    }

    int __register() {
        auto m = ni_registerModule("MessageBox");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setDetailedText", &Handle_setDetailedText__wrapper);
        ni_registerModuleMethod(m, "Handle_setIcon", &Handle_setIcon__wrapper);
        ni_registerModuleMethod(m, "Handle_setInformativeText", &Handle_setInformativeText__wrapper);
        ni_registerModuleMethod(m, "Handle_setOptions", &Handle_setOptions__wrapper);
        ni_registerModuleMethod(m, "Handle_setStandardButtons", &Handle_setStandardButtons__wrapper);
        ni_registerModuleMethod(m, "Handle_setText", &Handle_setText__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextFormat", &Handle_setTextFormat__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextInteractionFlags", &Handle_setTextInteractionFlags__wrapper);
        ni_registerModuleMethod(m, "Handle_setDefaultButton", &Handle_setDefaultButton__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_accepted = ni_registerInterfaceMethod(signalHandler, "accepted", &SignalHandler_accepted__wrapper);
        signalHandler_finished = ni_registerInterfaceMethod(signalHandler, "finished", &SignalHandler_finished__wrapper);
        signalHandler_rejected = ni_registerInterfaceMethod(signalHandler, "rejected", &SignalHandler_rejected__wrapper);
        signalHandler_buttonClicked = ni_registerInterfaceMethod(signalHandler, "buttonClicked", &SignalHandler_buttonClicked__wrapper);
        return 0; // = OK
    }
}
