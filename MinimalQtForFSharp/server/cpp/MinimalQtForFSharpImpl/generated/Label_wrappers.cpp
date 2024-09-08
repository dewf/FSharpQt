#include "../support/NativeImplServer.h"
#include "Label_wrappers.h"
#include "Label.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Frame_wrappers.h"
using namespace ::Frame;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Pixmap_wrappers.h"
using namespace ::Pixmap;

namespace Label
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_linkActivated;
    ni_InterfaceMethodRef signalHandler_linkHovered;
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
        void linkActivated(std::string link) override {
            pushStringInternal(link);
            invokeMethod(signalHandler_linkActivated);
        }
        void linkHovered(std::string link) override {
            pushStringInternal(link);
            invokeMethod(signalHandler_linkHovered);
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

    void SignalHandler_linkActivated__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto link = popStringInternal();
        inst->linkActivated(link);
    }

    void SignalHandler_linkHovered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto link = popStringInternal();
        inst->linkHovered(link);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAlignment__wrapper() {
        auto _this = Handle__pop();
        auto align = Alignment__pop();
        Handle_setAlignment(_this, align);
    }

    void Handle_hasSelectedText__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_hasSelectedText(_this));
    }

    void Handle_setIndent__wrapper() {
        auto _this = Handle__pop();
        auto indent = ni_popInt32();
        Handle_setIndent(_this, indent);
    }

    void Handle_setMargin__wrapper() {
        auto _this = Handle__pop();
        auto margin = ni_popInt32();
        Handle_setMargin(_this, margin);
    }

    void Handle_setOpenExternalLinks__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setOpenExternalLinks(_this, state);
    }

    void Handle_setPixmap__wrapper() {
        auto _this = Handle__pop();
        auto pixmap = Pixmap::Deferred__pop();
        Handle_setPixmap(_this, pixmap);
    }

    void Handle_setScaledContents__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setScaledContents(_this, state);
    }

    void Handle_selectedText__wrapper() {
        auto _this = Handle__pop();
        pushStringInternal(Handle_selectedText(_this));
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
        auto interactionFlags = TextInteractionFlags__pop();
        Handle_setTextInteractionFlags(_this, interactionFlags);
    }

    void Handle_setWordWrap__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setWordWrap(_this, state);
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

    void createNoHandler__wrapper() {
        Handle__push(createNoHandler());
    }

    int __register() {
        auto m = ni_registerModule("Label");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "createNoHandler", &createNoHandler__wrapper);
        ni_registerModuleMethod(m, "Handle_setAlignment", &Handle_setAlignment__wrapper);
        ni_registerModuleMethod(m, "Handle_hasSelectedText", &Handle_hasSelectedText__wrapper);
        ni_registerModuleMethod(m, "Handle_setIndent", &Handle_setIndent__wrapper);
        ni_registerModuleMethod(m, "Handle_setMargin", &Handle_setMargin__wrapper);
        ni_registerModuleMethod(m, "Handle_setOpenExternalLinks", &Handle_setOpenExternalLinks__wrapper);
        ni_registerModuleMethod(m, "Handle_setPixmap", &Handle_setPixmap__wrapper);
        ni_registerModuleMethod(m, "Handle_setScaledContents", &Handle_setScaledContents__wrapper);
        ni_registerModuleMethod(m, "Handle_selectedText", &Handle_selectedText__wrapper);
        ni_registerModuleMethod(m, "Handle_setText", &Handle_setText__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextFormat", &Handle_setTextFormat__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextInteractionFlags", &Handle_setTextInteractionFlags__wrapper);
        ni_registerModuleMethod(m, "Handle_setWordWrap", &Handle_setWordWrap__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_linkActivated = ni_registerInterfaceMethod(signalHandler, "linkActivated", &SignalHandler_linkActivated__wrapper);
        signalHandler_linkHovered = ni_registerInterfaceMethod(signalHandler, "linkHovered", &SignalHandler_linkHovered__wrapper);
        return 0; // = OK
    }
}
