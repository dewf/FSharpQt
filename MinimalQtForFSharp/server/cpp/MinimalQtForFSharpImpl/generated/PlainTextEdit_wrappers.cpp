#include "../support/NativeImplServer.h"
#include "PlainTextEdit_wrappers.h"
#include "PlainTextEdit.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "AbstractScrollArea_wrappers.h"
using namespace ::AbstractScrollArea;

#include "TextOption_wrappers.h"
using namespace ::TextOption;

namespace PlainTextEdit
{
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_blockCountChanged;
    ni_InterfaceMethodRef signalHandler_copyAvailable;
    ni_InterfaceMethodRef signalHandler_cursorPositionChanged;
    ni_InterfaceMethodRef signalHandler_modificationChanged;
    ni_InterfaceMethodRef signalHandler_redoAvailable;
    ni_InterfaceMethodRef signalHandler_selectionChanged;
    ni_InterfaceMethodRef signalHandler_textChanged;
    ni_InterfaceMethodRef signalHandler_undoAvailable;
    ni_InterfaceMethodRef signalHandler_updateRequest;
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
        void blockCountChanged(int32_t newBlockCount) override {
            ni_pushInt32(newBlockCount);
            invokeMethod(signalHandler_blockCountChanged);
        }
        void copyAvailable(bool yes) override {
            ni_pushBool(yes);
            invokeMethod(signalHandler_copyAvailable);
        }
        void cursorPositionChanged() override {
            invokeMethod(signalHandler_cursorPositionChanged);
        }
        void modificationChanged(bool changed) override {
            ni_pushBool(changed);
            invokeMethod(signalHandler_modificationChanged);
        }
        void redoAvailable(bool available) override {
            ni_pushBool(available);
            invokeMethod(signalHandler_redoAvailable);
        }
        void selectionChanged() override {
            invokeMethod(signalHandler_selectionChanged);
        }
        void textChanged() override {
            invokeMethod(signalHandler_textChanged);
        }
        void undoAvailable(bool available) override {
            ni_pushBool(available);
            invokeMethod(signalHandler_undoAvailable);
        }
        void updateRequest(Rect rect, int32_t dy) override {
            ni_pushInt32(dy);
            Rect__push(rect, false);
            invokeMethod(signalHandler_updateRequest);
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

    void SignalHandler_blockCountChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto newBlockCount = ni_popInt32();
        inst->blockCountChanged(newBlockCount);
    }

    void SignalHandler_copyAvailable__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto yes = ni_popBool();
        inst->copyAvailable(yes);
    }

    void SignalHandler_cursorPositionChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->cursorPositionChanged();
    }

    void SignalHandler_modificationChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto changed = ni_popBool();
        inst->modificationChanged(changed);
    }

    void SignalHandler_redoAvailable__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto available = ni_popBool();
        inst->redoAvailable(available);
    }

    void SignalHandler_selectionChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->selectionChanged();
    }

    void SignalHandler_textChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->textChanged();
    }

    void SignalHandler_undoAvailable__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto available = ni_popBool();
        inst->undoAvailable(available);
    }

    void SignalHandler_updateRequest__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto rect = Rect__pop();
        auto dy = ni_popInt32();
        inst->updateRequest(rect, dy);
    }
    void LineWrapMode__push(LineWrapMode value) {
        ni_pushInt32((int32_t)value);
    }

    LineWrapMode LineWrapMode__pop() {
        auto tag = ni_popInt32();
        return (LineWrapMode)tag;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setBackgroundVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setBackgroundVisible(_this, visible);
    }

    void Handle_blockCount__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_blockCount(_this));
    }

    void Handle_setCenterOnScroll__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setCenterOnScroll(_this, state);
    }

    void Handle_setCursorWidth__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        Handle_setCursorWidth(_this, width);
    }

    void Handle_setDocumentTitle__wrapper() {
        auto _this = Handle__pop();
        auto title = popStringInternal();
        Handle_setDocumentTitle(_this, title);
    }

    void Handle_setLineWrapMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = LineWrapMode__pop();
        Handle_setLineWrapMode(_this, mode);
    }

    void Handle_setMaximumBlockCount__wrapper() {
        auto _this = Handle__pop();
        auto count = ni_popInt32();
        Handle_setMaximumBlockCount(_this, count);
    }

    void Handle_setOverwriteMode__wrapper() {
        auto _this = Handle__pop();
        auto overwrite = ni_popBool();
        Handle_setOverwriteMode(_this, overwrite);
    }

    void Handle_setPlaceholderText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setPlaceholderText(_this, text);
    }

    void Handle_setPlainText__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setPlainText(_this, text);
    }

    void Handle_setReadOnly__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setReadOnly(_this, state);
    }

    void Handle_setTabChangesFocus__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setTabChangesFocus(_this, state);
    }

    void Handle_setTabStopDistance__wrapper() {
        auto _this = Handle__pop();
        auto distance = ni_popDouble();
        Handle_setTabStopDistance(_this, distance);
    }

    void Handle_setTextInteractionFlags__wrapper() {
        auto _this = Handle__pop();
        auto tiFlags = TextInteractionFlags__pop();
        Handle_setTextInteractionFlags(_this, tiFlags);
    }

    void Handle_setUndoRedoEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setUndoRedoEnabled(_this, enabled);
    }

    void Handle_setWordWrapMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = WrapMode__pop();
        Handle_setWordWrapMode(_this, mode);
    }

    void Handle_toPlainText__wrapper() {
        auto _this = Handle__pop();
        pushStringInternal(Handle_toPlainText(_this));
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
        auto m = ni_registerModule("PlainTextEdit");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setBackgroundVisible", &Handle_setBackgroundVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_blockCount", &Handle_blockCount__wrapper);
        ni_registerModuleMethod(m, "Handle_setCenterOnScroll", &Handle_setCenterOnScroll__wrapper);
        ni_registerModuleMethod(m, "Handle_setCursorWidth", &Handle_setCursorWidth__wrapper);
        ni_registerModuleMethod(m, "Handle_setDocumentTitle", &Handle_setDocumentTitle__wrapper);
        ni_registerModuleMethod(m, "Handle_setLineWrapMode", &Handle_setLineWrapMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximumBlockCount", &Handle_setMaximumBlockCount__wrapper);
        ni_registerModuleMethod(m, "Handle_setOverwriteMode", &Handle_setOverwriteMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setPlaceholderText", &Handle_setPlaceholderText__wrapper);
        ni_registerModuleMethod(m, "Handle_setPlainText", &Handle_setPlainText__wrapper);
        ni_registerModuleMethod(m, "Handle_setReadOnly", &Handle_setReadOnly__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabChangesFocus", &Handle_setTabChangesFocus__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabStopDistance", &Handle_setTabStopDistance__wrapper);
        ni_registerModuleMethod(m, "Handle_setTextInteractionFlags", &Handle_setTextInteractionFlags__wrapper);
        ni_registerModuleMethod(m, "Handle_setUndoRedoEnabled", &Handle_setUndoRedoEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setWordWrapMode", &Handle_setWordWrapMode__wrapper);
        ni_registerModuleMethod(m, "Handle_toPlainText", &Handle_toPlainText__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_blockCountChanged = ni_registerInterfaceMethod(signalHandler, "blockCountChanged", &SignalHandler_blockCountChanged__wrapper);
        signalHandler_copyAvailable = ni_registerInterfaceMethod(signalHandler, "copyAvailable", &SignalHandler_copyAvailable__wrapper);
        signalHandler_cursorPositionChanged = ni_registerInterfaceMethod(signalHandler, "cursorPositionChanged", &SignalHandler_cursorPositionChanged__wrapper);
        signalHandler_modificationChanged = ni_registerInterfaceMethod(signalHandler, "modificationChanged", &SignalHandler_modificationChanged__wrapper);
        signalHandler_redoAvailable = ni_registerInterfaceMethod(signalHandler, "redoAvailable", &SignalHandler_redoAvailable__wrapper);
        signalHandler_selectionChanged = ni_registerInterfaceMethod(signalHandler, "selectionChanged", &SignalHandler_selectionChanged__wrapper);
        signalHandler_textChanged = ni_registerInterfaceMethod(signalHandler, "textChanged", &SignalHandler_textChanged__wrapper);
        signalHandler_undoAvailable = ni_registerInterfaceMethod(signalHandler, "undoAvailable", &SignalHandler_undoAvailable__wrapper);
        signalHandler_updateRequest = ni_registerInterfaceMethod(signalHandler, "updateRequest", &SignalHandler_updateRequest__wrapper);
        return 0; // = OK
    }
}
