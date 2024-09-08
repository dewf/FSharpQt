#include "../support/NativeImplServer.h"
#include "Layout_wrappers.h"
#include "Layout.h"

#include "Object_wrappers.h"
using namespace ::Object;

namespace Layout
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
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
        void destroyed(HandleRef obj) override {
            Handle__push(obj);
            invokeMethod(signalHandler_destroyed);
        }
        void objectNameChanged(std::string objectName) override {
            pushStringInternal(objectName);
            invokeMethod(signalHandler_objectNameChanged);
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
        auto obj = Handle__pop();
        inst->destroyed(obj);
    }

    void SignalHandler_objectNameChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto objectName = popStringInternal();
        inst->objectNameChanged(objectName);
    }
    void SizeConstraint__push(SizeConstraint value) {
        ni_pushInt32((int32_t)value);
    }

    SizeConstraint SizeConstraint__pop() {
        auto tag = ni_popInt32();
        return (SizeConstraint)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setEnabled(_this, enabled);
    }

    void Handle_setSpacing__wrapper() {
        auto _this = Handle__pop();
        auto spacing = ni_popInt32();
        Handle_setSpacing(_this, spacing);
    }

    void Handle_setContentsMargins__wrapper() {
        auto _this = Handle__pop();
        auto left = ni_popInt32();
        auto top = ni_popInt32();
        auto right = ni_popInt32();
        auto bottom = ni_popInt32();
        Handle_setContentsMargins(_this, left, top, right, bottom);
    }

    void Handle_setSizeConstraint__wrapper() {
        auto _this = Handle__pop();
        auto constraint = SizeConstraint__pop();
        Handle_setSizeConstraint(_this, constraint);
    }

    void Handle_removeAll__wrapper() {
        auto _this = Handle__pop();
        Handle_removeAll(_this);
    }

    void Handle_activate__wrapper() {
        auto _this = Handle__pop();
        Handle_activate(_this);
    }

    void Handle_update__wrapper() {
        auto _this = Handle__pop();
        Handle_update(_this);
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    int __register() {
        auto m = ni_registerModule("Layout");
        ni_registerModuleMethod(m, "Handle_setEnabled", &Handle_setEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setSpacing", &Handle_setSpacing__wrapper);
        ni_registerModuleMethod(m, "Handle_setContentsMargins", &Handle_setContentsMargins__wrapper);
        ni_registerModuleMethod(m, "Handle_setSizeConstraint", &Handle_setSizeConstraint__wrapper);
        ni_registerModuleMethod(m, "Handle_removeAll", &Handle_removeAll__wrapper);
        ni_registerModuleMethod(m, "Handle_activate", &Handle_activate__wrapper);
        ni_registerModuleMethod(m, "Handle_update", &Handle_update__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        return 0; // = OK
    }
}
