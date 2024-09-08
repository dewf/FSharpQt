#include "../support/NativeImplServer.h"
#include "Timer_wrappers.h"
#include "Timer.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace Timer
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_timeout;
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
        void timeout() override {
            invokeMethod(signalHandler_timeout);
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

    void SignalHandler_timeout__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->timeout();
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_isActive__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isActive(_this));
    }

    void Handle_setInterval__wrapper() {
        auto _this = Handle__pop();
        auto interval = ni_popInt32();
        Handle_setInterval(_this, interval);
    }

    void Handle_remainingTime__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_remainingTime(_this));
    }

    void Handle_setSingleShot__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setSingleShot(_this, state);
    }

    void Handle_setTimerType__wrapper() {
        auto _this = Handle__pop();
        auto type_ = TimerType__pop();
        Handle_setTimerType(_this, type_);
    }

    void Handle_start__wrapper() {
        auto _this = Handle__pop();
        auto msec = ni_popInt32();
        Handle_start(_this, msec);
    }

    void Handle_start_overload1__wrapper() {
        auto _this = Handle__pop();
        Handle_start(_this);
    }

    void Handle_stop__wrapper() {
        auto _this = Handle__pop();
        Handle_stop(_this);
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
        auto m = ni_registerModule("Timer");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_isActive", &Handle_isActive__wrapper);
        ni_registerModuleMethod(m, "Handle_setInterval", &Handle_setInterval__wrapper);
        ni_registerModuleMethod(m, "Handle_remainingTime", &Handle_remainingTime__wrapper);
        ni_registerModuleMethod(m, "Handle_setSingleShot", &Handle_setSingleShot__wrapper);
        ni_registerModuleMethod(m, "Handle_setTimerType", &Handle_setTimerType__wrapper);
        ni_registerModuleMethod(m, "Handle_start", &Handle_start__wrapper);
        ni_registerModuleMethod(m, "Handle_start_overload1", &Handle_start_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_stop", &Handle_stop__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_timeout = ni_registerInterfaceMethod(signalHandler, "timeout", &SignalHandler_timeout__wrapper);
        return 0; // = OK
    }
}
