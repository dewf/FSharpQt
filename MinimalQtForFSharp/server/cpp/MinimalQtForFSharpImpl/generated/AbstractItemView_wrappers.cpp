#include "../support/NativeImplServer.h"
#include "AbstractItemView_wrappers.h"
#include "AbstractItemView.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "AbstractScrollArea_wrappers.h"
using namespace ::AbstractScrollArea;

#include "AbstractItemModel_wrappers.h"
using namespace ::AbstractItemModel;

#include "ModelIndex_wrappers.h"
using namespace ::ModelIndex;

#include "AbstractItemDelegate_wrappers.h"
using namespace ::AbstractItemDelegate;

namespace AbstractItemView
{
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
    void DragDropMode__push(DragDropMode value) {
        ni_pushInt32((int32_t)value);
    }

    DragDropMode DragDropMode__pop() {
        auto tag = ni_popInt32();
        return (DragDropMode)tag;
    }
    void EditTriggers__push(EditTriggers value) {
        ni_pushInt32(value);
    }

    EditTriggers EditTriggers__pop() {
        return ni_popInt32();
    }
    void ScrollMode__push(ScrollMode value) {
        ni_pushInt32((int32_t)value);
    }

    ScrollMode ScrollMode__pop() {
        auto tag = ni_popInt32();
        return (ScrollMode)tag;
    }
    void SelectionBehavior__push(SelectionBehavior value) {
        ni_pushInt32((int32_t)value);
    }

    SelectionBehavior SelectionBehavior__pop() {
        auto tag = ni_popInt32();
        return (SelectionBehavior)tag;
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

    void Handle_setAlternatingRowColors__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAlternatingRowColors(_this, state);
    }

    void Handle_setAutoScroll__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAutoScroll(_this, state);
    }

    void Handle_setAutoScrollMargin__wrapper() {
        auto _this = Handle__pop();
        auto margin = ni_popInt32();
        Handle_setAutoScrollMargin(_this, margin);
    }

    void Handle_setDefaultDropAction__wrapper() {
        auto _this = Handle__pop();
        auto action = DropAction__pop();
        Handle_setDefaultDropAction(_this, action);
    }

    void Handle_setDragDropMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = DragDropMode__pop();
        Handle_setDragDropMode(_this, mode);
    }

    void Handle_setDragDropOverwriteMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = ni_popBool();
        Handle_setDragDropOverwriteMode(_this, mode);
    }

    void Handle_setDragEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setDragEnabled(_this, enabled);
    }

    void Handle_setEditTriggers__wrapper() {
        auto _this = Handle__pop();
        auto triggers = EditTriggers__pop();
        Handle_setEditTriggers(_this, triggers);
    }

    void Handle_setHorizontalScrollMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = ScrollMode__pop();
        Handle_setHorizontalScrollMode(_this, mode);
    }

    void Handle_setIconSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setIconSize(_this, size);
    }

    void Handle_setSelectionBehavior__wrapper() {
        auto _this = Handle__pop();
        auto behavior = SelectionBehavior__pop();
        Handle_setSelectionBehavior(_this, behavior);
    }

    void Handle_setSelectionMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = SelectionMode__pop();
        Handle_setSelectionMode(_this, mode);
    }

    void Handle_setDropIndicatorShown__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setDropIndicatorShown(_this, state);
    }

    void Handle_setTabKeyNavigation__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setTabKeyNavigation(_this, state);
    }

    void Handle_setTextElideMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = TextElideMode__pop();
        Handle_setTextElideMode(_this, mode);
    }

    void Handle_setVerticalScrollMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = ScrollMode__pop();
        Handle_setVerticalScrollMode(_this, mode);
    }

    void Handle_setModel__wrapper() {
        auto _this = Handle__pop();
        auto model = AbstractItemModel::Handle__pop();
        Handle_setModel(_this, model);
    }

    void Handle_setItemDelegate__wrapper() {
        auto _this = Handle__pop();
        auto itemDelegate = AbstractItemDelegate::Handle__pop();
        Handle_setItemDelegate(_this, itemDelegate);
    }

    void Handle_setItemDelegateForColumn__wrapper() {
        auto _this = Handle__pop();
        auto column = ni_popInt32();
        auto itemDelegate = AbstractItemDelegate::Handle__pop();
        Handle_setItemDelegateForColumn(_this, column, itemDelegate);
    }

    void Handle_setItemDelegateForRow__wrapper() {
        auto _this = Handle__pop();
        auto row = ni_popInt32();
        auto itemDelegate = AbstractItemDelegate::Handle__pop();
        Handle_setItemDelegateForRow(_this, row, itemDelegate);
    }

    int __register() {
        auto m = ni_registerModule("AbstractItemView");
        ni_registerModuleMethod(m, "Handle_setAlternatingRowColors", &Handle_setAlternatingRowColors__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoScroll", &Handle_setAutoScroll__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoScrollMargin", &Handle_setAutoScrollMargin__wrapper);
        ni_registerModuleMethod(m, "Handle_setDefaultDropAction", &Handle_setDefaultDropAction__wrapper);
        ni_registerModuleMethod(m, "Handle_setDragDropMode", &Handle_setDragDropMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setDragDropOverwriteMode", &Handle_setDragDropOverwriteMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setDragEnabled", &Handle_setDragEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setEditTriggers", &Handle_setEditTriggers__wrapper);
        ni_registerModuleMethod(m, "Handle_setHorizontalScrollMode", &Handle_setHorizontalScrollMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setSelectionBehavior", &Handle_setSelectionBehavior__wrapper);
        ni_registerModuleMethod(m, "Handle_setSelectionMode", &Handle_setSelectionMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setDropIndicatorShown", &Handle_setDropIndicatorShown__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabKeyNavigation", &Handle_setTabKeyNavigation__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextElideMode", &Handle_setTextElideMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setVerticalScrollMode", &Handle_setVerticalScrollMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setModel", &Handle_setModel__wrapper);
        ni_registerModuleMethod(m, "Handle_setItemDelegate", &Handle_setItemDelegate__wrapper);
        ni_registerModuleMethod(m, "Handle_setItemDelegateForColumn", &Handle_setItemDelegateForColumn__wrapper);
        ni_registerModuleMethod(m, "Handle_setItemDelegateForRow", &Handle_setItemDelegateForRow__wrapper);
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
        return 0; // = OK
    }
}
