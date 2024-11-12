#include "../support/NativeImplServer.h"
#include "StyledItemDelegate_wrappers.h"
#include "StyledItemDelegate.h"

#include "AbstractItemDelegate_wrappers.h"
using namespace ::AbstractItemDelegate;

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "AbstractItemModel_wrappers.h"
using namespace ::AbstractItemModel;

#include "ModelIndex_wrappers.h"
using namespace ::ModelIndex;

#include "StyleOptionViewItem_wrappers.h"
using namespace ::StyleOptionViewItem;

namespace StyledItemDelegate
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_closeEditor;
    ni_InterfaceMethodRef signalHandler_commitData;
    ni_InterfaceMethodRef signalHandler_sizeHintChanged;
    ni_InterfaceMethodRef methodDelegate_createEditor;
    ni_InterfaceMethodRef methodDelegate_setEditorData;
    ni_InterfaceMethodRef methodDelegate_setModelData;
    ni_InterfaceMethodRef methodDelegate_destroyEditor;
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
        void closeEditor(Widget::HandleRef editor, EndEditHint hint) override {
            EndEditHint__push(hint);
            Widget::Handle__push(editor);
            invokeMethod(signalHandler_closeEditor);
        }
        void commitData(Widget::HandleRef editor) override {
            Widget::Handle__push(editor);
            invokeMethod(signalHandler_commitData);
        }
        void sizeHintChanged(ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            invokeMethod(signalHandler_sizeHintChanged);
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

    void SignalHandler_closeEditor__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto editor = Widget::Handle__pop();
        auto hint = EndEditHint__pop();
        inst->closeEditor(editor, hint);
    }

    void SignalHandler_commitData__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto editor = Widget::Handle__pop();
        inst->commitData(editor);
    }

    void SignalHandler_sizeHintChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ModelIndex::Handle__pop();
        inst->sizeHintChanged(index);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
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
    void MethodMask__push(MethodMask value) {
        ni_pushInt32(value);
    }

    MethodMask MethodMask__pop() {
        return ni_popInt32();
    }
    static std::map<MethodDelegate*, std::weak_ptr<Pushable>> __methodDelegateToPushable;

    class ServerMethodDelegateWrapper : public ServerObject {
    public:
        std::shared_ptr<MethodDelegate> rawInterface;
    private:
        ServerMethodDelegateWrapper(std::shared_ptr<MethodDelegate> raw) {
            this->rawInterface = raw;
        }
        void releaseExtra() override {
            __methodDelegateToPushable.erase(rawInterface.get());
        }
    public:
        static std::shared_ptr<ServerMethodDelegateWrapper> wrapAndRegister(std::shared_ptr<MethodDelegate> raw) {
            auto ret = std::shared_ptr<ServerMethodDelegateWrapper>(new ServerMethodDelegateWrapper(raw));
            __methodDelegateToPushable[raw.get()] = ret;
            return ret;
        }
    };
    class ClientMethodDelegate : public ClientObject, public MethodDelegate {
    public:
        ClientMethodDelegate(int id) : ClientObject(id) {}
        ~ClientMethodDelegate() override {
            __methodDelegateToPushable.erase(this);
        }
        Widget::HandleRef createEditor(Widget::HandleRef parent, StyleOptionViewItem::HandleRef option, ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            StyleOptionViewItem::Handle__push(option);
            Widget::Handle__push(parent);
            invokeMethod(methodDelegate_createEditor);
            return Widget::Handle__pop();
        }
        void setEditorData(Widget::HandleRef editor, ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            Widget::Handle__push(editor);
            invokeMethod(methodDelegate_setEditorData);
        }
        void setModelData(Widget::HandleRef editor, AbstractItemModel::HandleRef model, ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            AbstractItemModel::Handle__push(model);
            Widget::Handle__push(editor);
            invokeMethod(methodDelegate_setModelData);
        }
        void destroyEditor(Widget::HandleRef editor, ModelIndex::HandleRef index) override {
            ModelIndex::Handle__push(index);
            Widget::Handle__push(editor);
            invokeMethod(methodDelegate_destroyEditor);
        }
    };

    void MethodDelegate__push(std::shared_ptr<MethodDelegate> inst, bool isReturn) {
        if (inst != nullptr) {
            auto found = __methodDelegateToPushable.find(inst.get());
            if (found != __methodDelegateToPushable.end()) {
                auto pushable = found->second.lock();
                pushable->push(pushable, isReturn);
            }
            else {
                auto pushable = ServerMethodDelegateWrapper::wrapAndRegister(inst);
                pushable->push(pushable, isReturn);
            }
        }
        else {
            ni_pushNull();
        }
    }

    std::shared_ptr<MethodDelegate> MethodDelegate__pop() {
        bool isClientID;
        auto id = ni_popInstance(&isClientID);
        if (id != 0) {
            if (isClientID) {
                auto ret = std::shared_ptr<MethodDelegate>(new ClientMethodDelegate(id));
                __methodDelegateToPushable[ret.get()] = std::dynamic_pointer_cast<Pushable>(ret);
                return ret;
            }
            else {
                auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(id));
                return wrapper->rawInterface;
            }
        }
        else {
            return std::shared_ptr<MethodDelegate>();
        }
    }

    void MethodDelegate_createEditor__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = Widget::Handle__pop();
        auto option = StyleOptionViewItem::Handle__pop();
        auto index = ModelIndex::Handle__pop();
        Widget::Handle__push(inst->createEditor(parent, option, index));
    }

    void MethodDelegate_setEditorData__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto editor = Widget::Handle__pop();
        auto index = ModelIndex::Handle__pop();
        inst->setEditorData(editor, index);
    }

    void MethodDelegate_setModelData__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto editor = Widget::Handle__pop();
        auto model = AbstractItemModel::Handle__pop();
        auto index = ModelIndex::Handle__pop();
        inst->setModelData(editor, model, index);
    }

    void MethodDelegate_destroyEditor__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto editor = Widget::Handle__pop();
        auto index = ModelIndex::Handle__pop();
        inst->destroyEditor(editor, index);
    }

    void createdSubclassed__wrapper() {
        auto methodDelegate = MethodDelegate__pop();
        auto methodMask = MethodMask__pop();
        auto handler = SignalHandler__pop();
        Handle__push(createdSubclassed(methodDelegate, methodMask, handler));
    }

    int __register() {
        auto m = ni_registerModule("StyledItemDelegate");
        ni_registerModuleMethod(m, "createdSubclassed", &createdSubclassed__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_closeEditor = ni_registerInterfaceMethod(signalHandler, "closeEditor", &SignalHandler_closeEditor__wrapper);
        signalHandler_commitData = ni_registerInterfaceMethod(signalHandler, "commitData", &SignalHandler_commitData__wrapper);
        signalHandler_sizeHintChanged = ni_registerInterfaceMethod(signalHandler, "sizeHintChanged", &SignalHandler_sizeHintChanged__wrapper);
        auto methodDelegate = ni_registerInterface(m, "MethodDelegate");
        methodDelegate_createEditor = ni_registerInterfaceMethod(methodDelegate, "createEditor", &MethodDelegate_createEditor__wrapper);
        methodDelegate_setEditorData = ni_registerInterfaceMethod(methodDelegate, "setEditorData", &MethodDelegate_setEditorData__wrapper);
        methodDelegate_setModelData = ni_registerInterfaceMethod(methodDelegate, "setModelData", &MethodDelegate_setModelData__wrapper);
        methodDelegate_destroyEditor = ni_registerInterfaceMethod(methodDelegate, "destroyEditor", &MethodDelegate_destroyEditor__wrapper);
        return 0; // = OK
    }
}
