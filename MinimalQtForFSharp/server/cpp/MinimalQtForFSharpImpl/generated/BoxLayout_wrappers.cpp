#include "../support/NativeImplServer.h"
#include "BoxLayout_wrappers.h"
#include "BoxLayout.h"

#include "Common_wrappers.h"
using namespace ::Common;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Layout_wrappers.h"
using namespace ::Layout;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Object_wrappers.h"
using namespace ::Object;

namespace BoxLayout
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
        void destroyed(Object::HandleRef obj) override {
            Object::Handle__push(obj);
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
        auto obj = Object::Handle__pop();
        inst->destroyed(obj);
    }

    void SignalHandler_objectNameChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto objectName = popStringInternal();
        inst->objectNameChanged(objectName);
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

    void Handle_setDirection__wrapper() {
        auto _this = Handle__pop();
        auto dir = Direction__pop();
        Handle_setDirection(_this, dir);
    }

    void Handle_addSpacing__wrapper() {
        auto _this = Handle__pop();
        auto size = ni_popInt32();
        Handle_addSpacing(_this, size);
    }

    void Handle_addStretch__wrapper() {
        auto _this = Handle__pop();
        auto stretch = ni_popInt32();
        Handle_addStretch(_this, stretch);
    }

    void Handle_addWidget__wrapper() {
        auto _this = Handle__pop();
        auto widget = Widget::Handle__pop();
        Handle_addWidget(_this, widget);
    }

    void Handle_addWidget_overload1__wrapper() {
        auto _this = Handle__pop();
        auto widget = Widget::Handle__pop();
        auto stretch = ni_popInt32();
        Handle_addWidget(_this, widget, stretch);
    }

    void Handle_addWidget_overload2__wrapper() {
        auto _this = Handle__pop();
        auto widget = Widget::Handle__pop();
        auto stretch = ni_popInt32();
        auto align = Alignment__pop();
        Handle_addWidget(_this, widget, stretch, align);
    }

    void Handle_addLayout__wrapper() {
        auto _this = Handle__pop();
        auto layout = Layout::Handle__pop();
        Handle_addLayout(_this, layout);
    }

    void Handle_addLayout_overload1__wrapper() {
        auto _this = Handle__pop();
        auto layout = Layout::Handle__pop();
        auto stretch = ni_popInt32();
        Handle_addLayout(_this, layout, stretch);
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
        auto dir = Direction__pop();
        auto handler = SignalHandler__pop();
        Handle__push(create(dir, handler));
    }

    void createNoHandler__wrapper() {
        auto dir = Direction__pop();
        Handle__push(createNoHandler(dir));
    }

    int __register() {
        auto m = ni_registerModule("BoxLayout");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "createNoHandler", &createNoHandler__wrapper);
        ni_registerModuleMethod(m, "Handle_setDirection", &Handle_setDirection__wrapper);
        ni_registerModuleMethod(m, "Handle_addSpacing", &Handle_addSpacing__wrapper);
        ni_registerModuleMethod(m, "Handle_addStretch", &Handle_addStretch__wrapper);
        ni_registerModuleMethod(m, "Handle_addWidget", &Handle_addWidget__wrapper);
        ni_registerModuleMethod(m, "Handle_addWidget_overload1", &Handle_addWidget_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_addWidget_overload2", &Handle_addWidget_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_addLayout", &Handle_addLayout__wrapper);
        ni_registerModuleMethod(m, "Handle_addLayout_overload1", &Handle_addLayout_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        return 0; // = OK
    }
}
