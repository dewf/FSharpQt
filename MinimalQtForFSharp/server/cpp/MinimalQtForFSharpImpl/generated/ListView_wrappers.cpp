#include "../support/NativeImplServer.h"
#include "ListView_wrappers.h"
#include "ListView.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "AbstractListModel_wrappers.h"
using namespace ::AbstractListModel;

#include "ModelIndex_wrappers.h"
using namespace ::ModelIndex;

#include "AbstractItemView_wrappers.h"
using namespace ::AbstractItemView;

namespace ListView
{
    void __ModelIndex_Handle_Array__push(std::vector<ModelIndex::HandleRef> items, bool isReturn) {
        ni_pushPtrArray((void**)items.data(), items.size());
    }

    std::vector<ModelIndex::HandleRef> __ModelIndex_Handle_Array__pop() {
        ModelIndex::HandleRef *values;
        size_t count;
        ni_popPtrArray((void***)&values, &count);
        return std::vector<ModelIndex::HandleRef>(values, values + count);
    }
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
    ni_InterfaceMethodRef signalHandler_indexesMoved;
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
        void indexesMoved(std::vector<ModelIndex::HandleRef> indexes) override {
            __ModelIndex_Handle_Array__push(indexes, false);
            invokeMethod(signalHandler_indexesMoved);
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

    void SignalHandler_indexesMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto indexes = __ModelIndex_Handle_Array__pop();
        inst->indexesMoved(indexes);
    }
    void Movement__push(Movement value) {
        ni_pushInt32((int32_t)value);
    }

    Movement Movement__pop() {
        auto tag = ni_popInt32();
        return (Movement)tag;
    }
    void Flow__push(Flow value) {
        ni_pushInt32((int32_t)value);
    }

    Flow Flow__pop() {
        auto tag = ni_popInt32();
        return (Flow)tag;
    }
    void ResizeMode__push(ResizeMode value) {
        ni_pushInt32((int32_t)value);
    }

    ResizeMode ResizeMode__pop() {
        auto tag = ni_popInt32();
        return (ResizeMode)tag;
    }
    void LayoutMode__push(LayoutMode value) {
        ni_pushInt32((int32_t)value);
    }

    LayoutMode LayoutMode__pop() {
        auto tag = ni_popInt32();
        return (LayoutMode)tag;
    }
    void ViewMode__push(ViewMode value) {
        ni_pushInt32((int32_t)value);
    }

    ViewMode ViewMode__pop() {
        auto tag = ni_popInt32();
        return (ViewMode)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setBatchSize__wrapper() {
        auto _this = Handle__pop();
        auto size = ni_popInt32();
        Handle_setBatchSize(_this, size);
    }

    void Handle_setFlow__wrapper() {
        auto _this = Handle__pop();
        auto flow = Flow__pop();
        Handle_setFlow(_this, flow);
    }

    void Handle_setGridSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setGridSize(_this, size);
    }

    void Handle_setWrapping__wrapper() {
        auto _this = Handle__pop();
        auto wrapping = ni_popBool();
        Handle_setWrapping(_this, wrapping);
    }

    void Handle_setItemAlignment__wrapper() {
        auto _this = Handle__pop();
        auto align = Alignment__pop();
        Handle_setItemAlignment(_this, align);
    }

    void Handle_setLayoutMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = LayoutMode__pop();
        Handle_setLayoutMode(_this, mode);
    }

    void Handle_setModelColumn__wrapper() {
        auto _this = Handle__pop();
        auto column = ni_popInt32();
        Handle_setModelColumn(_this, column);
    }

    void Handle_setMovement__wrapper() {
        auto _this = Handle__pop();
        auto value = Movement__pop();
        Handle_setMovement(_this, value);
    }

    void Handle_setResizeMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = ResizeMode__pop();
        Handle_setResizeMode(_this, mode);
    }

    void Handle_setSelectionRectVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setSelectionRectVisible(_this, visible);
    }

    void Handle_setSpacing__wrapper() {
        auto _this = Handle__pop();
        auto spacing = ni_popInt32();
        Handle_setSpacing(_this, spacing);
    }

    void Handle_setUniformItemSizes__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setUniformItemSizes(_this, state);
    }

    void Handle_setViewMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = ViewMode__pop();
        Handle_setViewMode(_this, mode);
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

    int __register() {
        auto m = ni_registerModule("ListView");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setBatchSize", &Handle_setBatchSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setFlow", &Handle_setFlow__wrapper);
        ni_registerModuleMethod(m, "Handle_setGridSize", &Handle_setGridSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setWrapping", &Handle_setWrapping__wrapper);
        ni_registerModuleMethod(m, "Handle_setItemAlignment", &Handle_setItemAlignment__wrapper);
        ni_registerModuleMethod(m, "Handle_setLayoutMode", &Handle_setLayoutMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setModelColumn", &Handle_setModelColumn__wrapper);
        ni_registerModuleMethod(m, "Handle_setMovement", &Handle_setMovement__wrapper);
        ni_registerModuleMethod(m, "Handle_setResizeMode", &Handle_setResizeMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setSelectionRectVisible", &Handle_setSelectionRectVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_setSpacing", &Handle_setSpacing__wrapper);
        ni_registerModuleMethod(m, "Handle_setUniformItemSizes", &Handle_setUniformItemSizes__wrapper);
        ni_registerModuleMethod(m, "Handle_setViewMode", &Handle_setViewMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setWordWrap", &Handle_setWordWrap__wrapper);
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
        signalHandler_indexesMoved = ni_registerInterfaceMethod(signalHandler, "indexesMoved", &SignalHandler_indexesMoved__wrapper);
        return 0; // = OK
    }
}
