#include "../support/NativeImplServer.h"
#include "ProgressBar_wrappers.h"
#include "ProgressBar.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Widget_wrappers.h"
using namespace ::Widget;

namespace ProgressBar
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_valueChanged;
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
        void valueChanged(int32_t value) override {
            ni_pushInt32(value);
            invokeMethod(signalHandler_valueChanged);
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

    void SignalHandler_valueChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto value = ni_popInt32();
        inst->valueChanged(value);
    }
    void Direction__push(Direction value) {
        ni_pushInt32((int32_t)value);
    }

    Direction Direction__pop() {
        auto tag = ni_popInt32();
        return (Direction)tag;
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

    void Handle_setFormat__wrapper() {
        auto _this = Handle__pop();
        auto format = popStringInternal();
        Handle_setFormat(_this, format);
    }

    void Handle_setInvertedAppearance__wrapper() {
        auto _this = Handle__pop();
        auto invert = ni_popBool();
        Handle_setInvertedAppearance(_this, invert);
    }

    void Handle_setMaximum__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setMaximum(_this, value);
    }

    void Handle_setMinimum__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setMinimum(_this, value);
    }

    void Handle_setOrientation__wrapper() {
        auto _this = Handle__pop();
        auto orient = Orientation__pop();
        Handle_setOrientation(_this, orient);
    }

    void Handle_text__wrapper() {
        auto _this = Handle__pop();
        pushStringInternal(Handle_text(_this));
    }

    void Handle_setTextDirection__wrapper() {
        auto _this = Handle__pop();
        auto direction = Direction__pop();
        Handle_setTextDirection(_this, direction);
    }

    void Handle_setTextVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setTextVisible(_this, visible);
    }

    void Handle_setValue__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popInt32();
        Handle_setValue(_this, value);
    }

    void Handle_setRange__wrapper() {
        auto _this = Handle__pop();
        auto min = ni_popInt32();
        auto max = ni_popInt32();
        Handle_setRange(_this, min, max);
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
        auto m = ni_registerModule("ProgressBar");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAlignment", &Handle_setAlignment__wrapper);
        ni_registerModuleMethod(m, "Handle_setFormat", &Handle_setFormat__wrapper);
        ni_registerModuleMethod(m, "Handle_setInvertedAppearance", &Handle_setInvertedAppearance__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximum", &Handle_setMaximum__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimum", &Handle_setMinimum__wrapper);
        ni_registerModuleMethod(m, "Handle_setOrientation", &Handle_setOrientation__wrapper);
        ni_registerModuleMethod(m, "Handle_text", &Handle_text__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextDirection", &Handle_setTextDirection__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextVisible", &Handle_setTextVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_setValue", &Handle_setValue__wrapper);
        ni_registerModuleMethod(m, "Handle_setRange", &Handle_setRange__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_valueChanged = ni_registerInterfaceMethod(signalHandler, "valueChanged", &SignalHandler_valueChanged__wrapper);
        return 0; // = OK
    }
}
