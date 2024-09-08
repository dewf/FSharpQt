#include "../support/NativeImplServer.h"
#include "AbstractSlider_wrappers.h"
#include "AbstractSlider.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace AbstractSlider
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_actionTriggered;
    ni_InterfaceMethodRef signalHandler_rangeChanged;
    ni_InterfaceMethodRef signalHandler_sliderMoved;
    ni_InterfaceMethodRef signalHandler_sliderPressed;
    ni_InterfaceMethodRef signalHandler_sliderReleased;
    ni_InterfaceMethodRef signalHandler_valueChanged;
    void SignalMask__push(SignalMask value) {
        ni_pushInt32(value);
    }

    SignalMask SignalMask__pop() {
        return ni_popInt32();
    }
    void SliderAction__push(SliderAction value) {
        ni_pushInt32((int32_t)value);
    }

    SliderAction SliderAction__pop() {
        auto tag = ni_popInt32();
        return (SliderAction)tag;
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
        void actionTriggered(SliderAction action) override {
            SliderAction__push(action);
            invokeMethod(signalHandler_actionTriggered);
        }
        void rangeChanged(int32_t min, int32_t max) override {
            ni_pushInt32(max);
            ni_pushInt32(min);
            invokeMethod(signalHandler_rangeChanged);
        }
        void sliderMoved(int32_t value) override {
            ni_pushInt32(value);
            invokeMethod(signalHandler_sliderMoved);
        }
        void sliderPressed() override {
            invokeMethod(signalHandler_sliderPressed);
        }
        void sliderReleased() override {
            invokeMethod(signalHandler_sliderReleased);
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

    void SignalHandler_actionTriggered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto action = SliderAction__pop();
        inst->actionTriggered(action);
    }

    void SignalHandler_rangeChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto min = ni_popInt32();
        auto max = ni_popInt32();
        inst->rangeChanged(min, max);
    }

    void SignalHandler_sliderMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto value = ni_popInt32();
        inst->sliderMoved(value);
    }

    void SignalHandler_sliderPressed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->sliderPressed();
    }

    void SignalHandler_sliderReleased__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->sliderReleased();
    }

    void SignalHandler_valueChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto value = ni_popInt32();
        inst->valueChanged(value);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setInvertedAppearance__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setInvertedAppearance(_this, state);
    }

    void Handle_setInvertedControls__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setInvertedControls(_this, state);
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

    void Handle_setPageStep__wrapper() {
        auto _this = Handle__pop();
        auto pageStep = ni_popInt32();
        Handle_setPageStep(_this, pageStep);
    }

    void Handle_setSingleStep__wrapper() {
        auto _this = Handle__pop();
        auto step = ni_popInt32();
        Handle_setSingleStep(_this, step);
    }

    void Handle_setSliderDown__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setSliderDown(_this, state);
    }

    void Handle_setSliderPosition__wrapper() {
        auto _this = Handle__pop();
        auto pos = ni_popInt32();
        Handle_setSliderPosition(_this, pos);
    }

    void Handle_setTracking__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setTracking(_this, value);
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

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    int __register() {
        auto m = ni_registerModule("AbstractSlider");
        ni_registerModuleMethod(m, "Handle_setInvertedAppearance", &Handle_setInvertedAppearance__wrapper);
        ni_registerModuleMethod(m, "Handle_setInvertedControls", &Handle_setInvertedControls__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximum", &Handle_setMaximum__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimum", &Handle_setMinimum__wrapper);
        ni_registerModuleMethod(m, "Handle_setOrientation", &Handle_setOrientation__wrapper);
        ni_registerModuleMethod(m, "Handle_setPageStep", &Handle_setPageStep__wrapper);
        ni_registerModuleMethod(m, "Handle_setSingleStep", &Handle_setSingleStep__wrapper);
        ni_registerModuleMethod(m, "Handle_setSliderDown", &Handle_setSliderDown__wrapper);
        ni_registerModuleMethod(m, "Handle_setSliderPosition", &Handle_setSliderPosition__wrapper);
        ni_registerModuleMethod(m, "Handle_setTracking", &Handle_setTracking__wrapper);
        ni_registerModuleMethod(m, "Handle_setValue", &Handle_setValue__wrapper);
        ni_registerModuleMethod(m, "Handle_setRange", &Handle_setRange__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_actionTriggered = ni_registerInterfaceMethod(signalHandler, "actionTriggered", &SignalHandler_actionTriggered__wrapper);
        signalHandler_rangeChanged = ni_registerInterfaceMethod(signalHandler, "rangeChanged", &SignalHandler_rangeChanged__wrapper);
        signalHandler_sliderMoved = ni_registerInterfaceMethod(signalHandler, "sliderMoved", &SignalHandler_sliderMoved__wrapper);
        signalHandler_sliderPressed = ni_registerInterfaceMethod(signalHandler, "sliderPressed", &SignalHandler_sliderPressed__wrapper);
        signalHandler_sliderReleased = ni_registerInterfaceMethod(signalHandler, "sliderReleased", &SignalHandler_sliderReleased__wrapper);
        signalHandler_valueChanged = ni_registerInterfaceMethod(signalHandler, "valueChanged", &SignalHandler_valueChanged__wrapper);
        return 0; // = OK
    }
}
