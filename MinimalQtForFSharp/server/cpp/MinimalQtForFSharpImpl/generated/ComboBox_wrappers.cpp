#include "../support/NativeImplServer.h"
#include "ComboBox_wrappers.h"
#include "ComboBox.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Widget_wrappers.h"
using namespace ::Widget;

#include "Variant_wrappers.h"
using namespace ::Variant;

#include "AbstractItemModel_wrappers.h"
using namespace ::AbstractItemModel;

#include "Icon_wrappers.h"
using namespace ::Icon;

namespace ComboBox
{
    // built-in array type: std::vector<std::string>
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_activated;
    ni_InterfaceMethodRef signalHandler_currentIndexChanged;
    ni_InterfaceMethodRef signalHandler_currentTextChanged;
    ni_InterfaceMethodRef signalHandler_editTextChanged;
    ni_InterfaceMethodRef signalHandler_highlighted;
    ni_InterfaceMethodRef signalHandler_textActivated;
    ni_InterfaceMethodRef signalHandler_textHighlighted;
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
        void activated(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_activated);
        }
        void currentIndexChanged(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_currentIndexChanged);
        }
        void currentTextChanged(std::string text) override {
            pushStringInternal(text);
            invokeMethod(signalHandler_currentTextChanged);
        }
        void editTextChanged(std::string text) override {
            pushStringInternal(text);
            invokeMethod(signalHandler_editTextChanged);
        }
        void highlighted(int32_t index) override {
            ni_pushInt32(index);
            invokeMethod(signalHandler_highlighted);
        }
        void textActivated(std::string text) override {
            pushStringInternal(text);
            invokeMethod(signalHandler_textActivated);
        }
        void textHighlighted(std::string text) override {
            pushStringInternal(text);
            invokeMethod(signalHandler_textHighlighted);
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
        auto index = ni_popInt32();
        inst->activated(index);
    }

    void SignalHandler_currentIndexChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ni_popInt32();
        inst->currentIndexChanged(index);
    }

    void SignalHandler_currentTextChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto text = popStringInternal();
        inst->currentTextChanged(text);
    }

    void SignalHandler_editTextChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto text = popStringInternal();
        inst->editTextChanged(text);
    }

    void SignalHandler_highlighted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto index = ni_popInt32();
        inst->highlighted(index);
    }

    void SignalHandler_textActivated__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto text = popStringInternal();
        inst->textActivated(text);
    }

    void SignalHandler_textHighlighted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto text = popStringInternal();
        inst->textHighlighted(text);
    }
    void InsertPolicy__push(InsertPolicy value) {
        ni_pushInt32((int32_t)value);
    }

    InsertPolicy InsertPolicy__pop() {
        auto tag = ni_popInt32();
        return (InsertPolicy)tag;
    }
    void SizeAdjustPolicy__push(SizeAdjustPolicy value) {
        ni_pushInt32((int32_t)value);
    }

    SizeAdjustPolicy SizeAdjustPolicy__pop() {
        auto tag = ni_popInt32();
        return (SizeAdjustPolicy)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_count__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_count(_this));
    }

    void Handle_currentData__wrapper() {
        auto _this = Handle__pop();
        OwnedHandle__push(Handle_currentData(_this));
    }

    void Handle_currentData_overload1__wrapper() {
        auto _this = Handle__pop();
        auto role = ItemDataRole__pop();
        OwnedHandle__push(Handle_currentData(_this, role));
    }

    void Handle_currentIndex__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_currentIndex(_this));
    }

    void Handle_setCurrentIndex__wrapper() {
        auto _this = Handle__pop();
        auto index = ni_popInt32();
        Handle_setCurrentIndex(_this, index);
    }

    void Handle_setCurrentText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setCurrentText(_this, text);
    }

    void Handle_setDuplicatesEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setDuplicatesEnabled(_this, enabled);
    }

    void Handle_setEditable__wrapper() {
        auto _this = Handle__pop();
        auto editable = ni_popBool();
        Handle_setEditable(_this, editable);
    }

    void Handle_setFrame__wrapper() {
        auto _this = Handle__pop();
        auto hasFrame = ni_popBool();
        Handle_setFrame(_this, hasFrame);
    }

    void Handle_setIconSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setIconSize(_this, size);
    }

    void Handle_setInsertPolicy__wrapper() {
        auto _this = Handle__pop();
        auto policy = InsertPolicy__pop();
        Handle_setInsertPolicy(_this, policy);
    }

    void Handle_setMaxCount__wrapper() {
        auto _this = Handle__pop();
        auto count = ni_popInt32();
        Handle_setMaxCount(_this, count);
    }

    void Handle_setMaxVisibleItems__wrapper() {
        auto _this = Handle__pop();
        auto count = ni_popInt32();
        Handle_setMaxVisibleItems(_this, count);
    }

    void Handle_setMinimumContentsLength__wrapper() {
        auto _this = Handle__pop();
        auto length = ni_popInt32();
        Handle_setMinimumContentsLength(_this, length);
    }

    void Handle_setModelColumn__wrapper() {
        auto _this = Handle__pop();
        auto column = ni_popInt32();
        Handle_setModelColumn(_this, column);
    }

    void Handle_setPlaceholderText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setPlaceholderText(_this, text);
    }

    void Handle_setSizeAdjustPolicy__wrapper() {
        auto _this = Handle__pop();
        auto policy = SizeAdjustPolicy__pop();
        Handle_setSizeAdjustPolicy(_this, policy);
    }

    void Handle_clear__wrapper() {
        auto _this = Handle__pop();
        Handle_clear(_this);
    }

    void Handle_addItem__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        auto userData = Variant::Deferred__pop();
        Handle_addItem(_this, text, userData);
    }

    void Handle_addItem_overload1__wrapper() {
        auto _this = Handle__pop();
        auto icon = Icon::Deferred__pop();
        auto text = popStringInternal();
        auto userData = Variant::Deferred__pop();
        Handle_addItem(_this, icon, text, userData);
    }

    void Handle_addItems__wrapper() {
        auto _this = Handle__pop();
        auto texts = popStringArrayInternal();
        Handle_addItems(_this, texts);
    }

    void Handle_setModel__wrapper() {
        auto _this = Handle__pop();
        auto model = AbstractItemModel::Handle__pop();
        Handle_setModel(_this, model);
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

    void downcastFrom__wrapper() {
        auto widget = Widget::Handle__pop();
        Handle__push(downcastFrom(widget));
    }

    int __register() {
        auto m = ni_registerModule("ComboBox");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "downcastFrom", &downcastFrom__wrapper);
        ni_registerModuleMethod(m, "Handle_count", &Handle_count__wrapper);
        ni_registerModuleMethod(m, "Handle_currentData", &Handle_currentData__wrapper);
        ni_registerModuleMethod(m, "Handle_currentData_overload1", &Handle_currentData_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_currentIndex", &Handle_currentIndex__wrapper);
        ni_registerModuleMethod(m, "Handle_setCurrentIndex", &Handle_setCurrentIndex__wrapper);
        ni_registerModuleMethod(m, "Handle_setCurrentText", &Handle_setCurrentText__wrapper);
        ni_registerModuleMethod(m, "Handle_setDuplicatesEnabled", &Handle_setDuplicatesEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setEditable", &Handle_setEditable__wrapper);
        ni_registerModuleMethod(m, "Handle_setFrame", &Handle_setFrame__wrapper);
        ni_registerModuleMethod(m, "Handle_setIconSize", &Handle_setIconSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setInsertPolicy", &Handle_setInsertPolicy__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaxCount", &Handle_setMaxCount__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaxVisibleItems", &Handle_setMaxVisibleItems__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimumContentsLength", &Handle_setMinimumContentsLength__wrapper);
        ni_registerModuleMethod(m, "Handle_setModelColumn", &Handle_setModelColumn__wrapper);
        ni_registerModuleMethod(m, "Handle_setPlaceholderText", &Handle_setPlaceholderText__wrapper);
        ni_registerModuleMethod(m, "Handle_setSizeAdjustPolicy", &Handle_setSizeAdjustPolicy__wrapper);
        ni_registerModuleMethod(m, "Handle_clear", &Handle_clear__wrapper);
        ni_registerModuleMethod(m, "Handle_addItem", &Handle_addItem__wrapper);
        ni_registerModuleMethod(m, "Handle_addItem_overload1", &Handle_addItem_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_addItems", &Handle_addItems__wrapper);
        ni_registerModuleMethod(m, "Handle_setModel", &Handle_setModel__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_activated = ni_registerInterfaceMethod(signalHandler, "activated", &SignalHandler_activated__wrapper);
        signalHandler_currentIndexChanged = ni_registerInterfaceMethod(signalHandler, "currentIndexChanged", &SignalHandler_currentIndexChanged__wrapper);
        signalHandler_currentTextChanged = ni_registerInterfaceMethod(signalHandler, "currentTextChanged", &SignalHandler_currentTextChanged__wrapper);
        signalHandler_editTextChanged = ni_registerInterfaceMethod(signalHandler, "editTextChanged", &SignalHandler_editTextChanged__wrapper);
        signalHandler_highlighted = ni_registerInterfaceMethod(signalHandler, "highlighted", &SignalHandler_highlighted__wrapper);
        signalHandler_textActivated = ni_registerInterfaceMethod(signalHandler, "textActivated", &SignalHandler_textActivated__wrapper);
        signalHandler_textHighlighted = ni_registerInterfaceMethod(signalHandler, "textHighlighted", &SignalHandler_textHighlighted__wrapper);
        return 0; // = OK
    }
}
