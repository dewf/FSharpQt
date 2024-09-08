#include "../support/NativeImplServer.h"
#include "LineEdit_wrappers.h"
#include "LineEdit.h"

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

namespace LineEdit
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_cursorPositionChanged;
    ni_InterfaceMethodRef signalHandler_editingFinished;
    ni_InterfaceMethodRef signalHandler_inputRejected;
    ni_InterfaceMethodRef signalHandler_returnPressed;
    ni_InterfaceMethodRef signalHandler_selectionChanged;
    ni_InterfaceMethodRef signalHandler_textChanged;
    ni_InterfaceMethodRef signalHandler_textEdited;
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
        void cursorPositionChanged(int32_t oldPos, int32_t newPos) override {
            ni_pushInt32(newPos);
            ni_pushInt32(oldPos);
            invokeMethod(signalHandler_cursorPositionChanged);
        }
        void editingFinished() override {
            invokeMethod(signalHandler_editingFinished);
        }
        void inputRejected() override {
            invokeMethod(signalHandler_inputRejected);
        }
        void returnPressed() override {
            invokeMethod(signalHandler_returnPressed);
        }
        void selectionChanged() override {
            invokeMethod(signalHandler_selectionChanged);
        }
        void textChanged(std::string text) override {
            pushStringInternal(text);
            invokeMethod(signalHandler_textChanged);
        }
        void textEdited(std::string text) override {
            pushStringInternal(text);
            invokeMethod(signalHandler_textEdited);
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

    void SignalHandler_cursorPositionChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto oldPos = ni_popInt32();
        auto newPos = ni_popInt32();
        inst->cursorPositionChanged(oldPos, newPos);
    }

    void SignalHandler_editingFinished__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->editingFinished();
    }

    void SignalHandler_inputRejected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->inputRejected();
    }

    void SignalHandler_returnPressed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->returnPressed();
    }

    void SignalHandler_selectionChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->selectionChanged();
    }

    void SignalHandler_textChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto text = popStringInternal();
        inst->textChanged(text);
    }

    void SignalHandler_textEdited__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto text = popStringInternal();
        inst->textEdited(text);
    }
    void EchoMode__push(EchoMode value) {
        ni_pushInt32((int32_t)value);
    }

    EchoMode EchoMode__pop() {
        auto tag = ni_popInt32();
        return (EchoMode)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_hasAcceptableInput__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_hasAcceptableInput(_this));
    }

    void Handle_setAlignment__wrapper() {
        auto _this = Handle__pop();
        auto align = Alignment__pop();
        Handle_setAlignment(_this, align);
    }

    void Handle_setClearButtonEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setClearButtonEnabled(_this, enabled);
    }

    void Handle_setCursorMoveStyle__wrapper() {
        auto _this = Handle__pop();
        auto style = CursorMoveStyle__pop();
        Handle_setCursorMoveStyle(_this, style);
    }

    void Handle_setCursorPosition__wrapper() {
        auto _this = Handle__pop();
        auto pos = ni_popInt32();
        Handle_setCursorPosition(_this, pos);
    }

    void Handle_displayText__wrapper() {
        auto _this = Handle__pop();
        pushStringInternal(Handle_displayText(_this));
    }

    void Handle_setDragEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setDragEnabled(_this, enabled);
    }

    void Handle_setEchoMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = EchoMode__pop();
        Handle_setEchoMode(_this, mode);
    }

    void Handle_setFrame__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setFrame(_this, enabled);
    }

    void Handle_hasSelectedText__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_hasSelectedText(_this));
    }

    void Handle_setInputMask__wrapper() {
        auto _this = Handle__pop();
        auto mask = popStringInternal();
        Handle_setInputMask(_this, mask);
    }

    void Handle_setMaxLength__wrapper() {
        auto _this = Handle__pop();
        auto length = ni_popInt32();
        Handle_setMaxLength(_this, length);
    }

    void Handle_isModified__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isModified(_this));
    }

    void Handle_setModified__wrapper() {
        auto _this = Handle__pop();
        auto modified = ni_popBool();
        Handle_setModified(_this, modified);
    }

    void Handle_setPlaceholderText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setPlaceholderText(_this, text);
    }

    void Handle_setReadOnly__wrapper() {
        auto _this = Handle__pop();
        auto value = ni_popBool();
        Handle_setReadOnly(_this, value);
    }

    void Handle_isRedoAvailable__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isRedoAvailable(_this));
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

    void Handle_text__wrapper() {
        auto _this = Handle__pop();
        pushStringInternal(Handle_text(_this));
    }

    void Handle_isUndoAvailable__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isUndoAvailable(_this));
    }

    void Handle_clear__wrapper() {
        auto _this = Handle__pop();
        Handle_clear(_this);
    }

    void Handle_copy__wrapper() {
        auto _this = Handle__pop();
        Handle_copy(_this);
    }

    void Handle_cut__wrapper() {
        auto _this = Handle__pop();
        Handle_cut(_this);
    }

    void Handle_paste__wrapper() {
        auto _this = Handle__pop();
        Handle_paste(_this);
    }

    void Handle_redo__wrapper() {
        auto _this = Handle__pop();
        Handle_redo(_this);
    }

    void Handle_selectAll__wrapper() {
        auto _this = Handle__pop();
        Handle_selectAll(_this);
    }

    void Handle_undo__wrapper() {
        auto _this = Handle__pop();
        Handle_undo(_this);
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
        auto m = ni_registerModule("LineEdit");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_hasAcceptableInput", &Handle_hasAcceptableInput__wrapper);
        ni_registerModuleMethod(m, "Handle_setAlignment", &Handle_setAlignment__wrapper);
        ni_registerModuleMethod(m, "Handle_setClearButtonEnabled", &Handle_setClearButtonEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setCursorMoveStyle", &Handle_setCursorMoveStyle__wrapper);
        ni_registerModuleMethod(m, "Handle_setCursorPosition", &Handle_setCursorPosition__wrapper);
        ni_registerModuleMethod(m, "Handle_displayText", &Handle_displayText__wrapper);
        ni_registerModuleMethod(m, "Handle_setDragEnabled", &Handle_setDragEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setEchoMode", &Handle_setEchoMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setFrame", &Handle_setFrame__wrapper);
        ni_registerModuleMethod(m, "Handle_hasSelectedText", &Handle_hasSelectedText__wrapper);
        ni_registerModuleMethod(m, "Handle_setInputMask", &Handle_setInputMask__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaxLength", &Handle_setMaxLength__wrapper);
        ni_registerModuleMethod(m, "Handle_isModified", &Handle_isModified__wrapper);
        ni_registerModuleMethod(m, "Handle_setModified", &Handle_setModified__wrapper);
        ni_registerModuleMethod(m, "Handle_setPlaceholderText", &Handle_setPlaceholderText__wrapper);
        ni_registerModuleMethod(m, "Handle_setReadOnly", &Handle_setReadOnly__wrapper);
        ni_registerModuleMethod(m, "Handle_isRedoAvailable", &Handle_isRedoAvailable__wrapper);
        ni_registerModuleMethod(m, "Handle_selectedText", &Handle_selectedText__wrapper);
        ni_registerModuleMethod(m, "Handle_setText", &Handle_setText__wrapper);
        ni_registerModuleMethod(m, "Handle_text", &Handle_text__wrapper);
        ni_registerModuleMethod(m, "Handle_isUndoAvailable", &Handle_isUndoAvailable__wrapper);
        ni_registerModuleMethod(m, "Handle_clear", &Handle_clear__wrapper);
        ni_registerModuleMethod(m, "Handle_copy", &Handle_copy__wrapper);
        ni_registerModuleMethod(m, "Handle_cut", &Handle_cut__wrapper);
        ni_registerModuleMethod(m, "Handle_paste", &Handle_paste__wrapper);
        ni_registerModuleMethod(m, "Handle_redo", &Handle_redo__wrapper);
        ni_registerModuleMethod(m, "Handle_selectAll", &Handle_selectAll__wrapper);
        ni_registerModuleMethod(m, "Handle_undo", &Handle_undo__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_cursorPositionChanged = ni_registerInterfaceMethod(signalHandler, "cursorPositionChanged", &SignalHandler_cursorPositionChanged__wrapper);
        signalHandler_editingFinished = ni_registerInterfaceMethod(signalHandler, "editingFinished", &SignalHandler_editingFinished__wrapper);
        signalHandler_inputRejected = ni_registerInterfaceMethod(signalHandler, "inputRejected", &SignalHandler_inputRejected__wrapper);
        signalHandler_returnPressed = ni_registerInterfaceMethod(signalHandler, "returnPressed", &SignalHandler_returnPressed__wrapper);
        signalHandler_selectionChanged = ni_registerInterfaceMethod(signalHandler, "selectionChanged", &SignalHandler_selectionChanged__wrapper);
        signalHandler_textChanged = ni_registerInterfaceMethod(signalHandler, "textChanged", &SignalHandler_textChanged__wrapper);
        signalHandler_textEdited = ni_registerInterfaceMethod(signalHandler, "textEdited", &SignalHandler_textEdited__wrapper);
        return 0; // = OK
    }
}
