#include "../support/NativeImplServer.h"
#include "FileDialog_wrappers.h"
#include "FileDialog.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Dialog_wrappers.h"
using namespace ::Dialog;

#include "Widget_wrappers.h"
using namespace ::Widget;

namespace FileDialog
{
    // built-in array type: std::vector<std::string>
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef signalHandler_accepted;
    ni_InterfaceMethodRef signalHandler_finished;
    ni_InterfaceMethodRef signalHandler_rejected;
    ni_InterfaceMethodRef signalHandler_currentChanged;
    ni_InterfaceMethodRef signalHandler_currentUrlChanged;
    ni_InterfaceMethodRef signalHandler_directoryEntered;
    ni_InterfaceMethodRef signalHandler_directoryUrlEntered;
    ni_InterfaceMethodRef signalHandler_fileSelected;
    ni_InterfaceMethodRef signalHandler_filesSelected;
    ni_InterfaceMethodRef signalHandler_filterSelected;
    ni_InterfaceMethodRef signalHandler_urlSelected;
    ni_InterfaceMethodRef signalHandler_urlsSelected;
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
        void accepted() override {
            invokeMethod(signalHandler_accepted);
        }
        void finished(int32_t result) override {
            ni_pushInt32(result);
            invokeMethod(signalHandler_finished);
        }
        void rejected() override {
            invokeMethod(signalHandler_rejected);
        }
        void currentChanged(std::string path) override {
            pushStringInternal(path);
            invokeMethod(signalHandler_currentChanged);
        }
        void currentUrlChanged(std::string url) override {
            pushStringInternal(url);
            invokeMethod(signalHandler_currentUrlChanged);
        }
        void directoryEntered(std::string dir) override {
            pushStringInternal(dir);
            invokeMethod(signalHandler_directoryEntered);
        }
        void directoryUrlEntered(std::string url) override {
            pushStringInternal(url);
            invokeMethod(signalHandler_directoryUrlEntered);
        }
        void fileSelected(std::string file) override {
            pushStringInternal(file);
            invokeMethod(signalHandler_fileSelected);
        }
        void filesSelected(std::vector<std::string> selected) override {
            pushStringArrayInternal(selected);
            invokeMethod(signalHandler_filesSelected);
        }
        void filterSelected(std::string filter) override {
            pushStringInternal(filter);
            invokeMethod(signalHandler_filterSelected);
        }
        void urlSelected(std::string url) override {
            pushStringInternal(url);
            invokeMethod(signalHandler_urlSelected);
        }
        void urlsSelected(std::vector<std::string> urls) override {
            pushStringArrayInternal(urls);
            invokeMethod(signalHandler_urlsSelected);
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

    void SignalHandler_accepted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->accepted();
    }

    void SignalHandler_finished__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto result = ni_popInt32();
        inst->finished(result);
    }

    void SignalHandler_rejected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->rejected();
    }

    void SignalHandler_currentChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto path = popStringInternal();
        inst->currentChanged(path);
    }

    void SignalHandler_currentUrlChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto url = popStringInternal();
        inst->currentUrlChanged(url);
    }

    void SignalHandler_directoryEntered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto dir = popStringInternal();
        inst->directoryEntered(dir);
    }

    void SignalHandler_directoryUrlEntered__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto url = popStringInternal();
        inst->directoryUrlEntered(url);
    }

    void SignalHandler_fileSelected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto file = popStringInternal();
        inst->fileSelected(file);
    }

    void SignalHandler_filesSelected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto selected = popStringArrayInternal();
        inst->filesSelected(selected);
    }

    void SignalHandler_filterSelected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto filter = popStringInternal();
        inst->filterSelected(filter);
    }

    void SignalHandler_urlSelected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto url = popStringInternal();
        inst->urlSelected(url);
    }

    void SignalHandler_urlsSelected__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto urls = popStringArrayInternal();
        inst->urlsSelected(urls);
    }
    void FileMode__push(FileMode value) {
        ni_pushInt32((int32_t)value);
    }

    FileMode FileMode__pop() {
        auto tag = ni_popInt32();
        return (FileMode)tag;
    }
    void ViewMode__push(ViewMode value) {
        ni_pushInt32((int32_t)value);
    }

    ViewMode ViewMode__pop() {
        auto tag = ni_popInt32();
        return (ViewMode)tag;
    }
    void AcceptMode__push(AcceptMode value) {
        ni_pushInt32((int32_t)value);
    }

    AcceptMode AcceptMode__pop() {
        auto tag = ni_popInt32();
        return (AcceptMode)tag;
    }
    void Options__push(Options value) {
        ni_pushInt32(value);
    }

    Options Options__pop() {
        return ni_popInt32();
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAcceptMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = AcceptMode__pop();
        Handle_setAcceptMode(_this, mode);
    }

    void Handle_setDefaultSuffix__wrapper() {
        auto _this = Handle__pop();
        auto suffix = popStringInternal();
        Handle_setDefaultSuffix(_this, suffix);
    }

    void Handle_setFileMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = FileMode__pop();
        Handle_setFileMode(_this, mode);
    }

    void Handle_setOptions__wrapper() {
        auto _this = Handle__pop();
        auto opts = Options__pop();
        Handle_setOptions(_this, opts);
    }

    void Handle_setSupportedSchemes__wrapper() {
        auto _this = Handle__pop();
        auto schemes = popStringArrayInternal();
        Handle_setSupportedSchemes(_this, schemes);
    }

    void Handle_setViewMode__wrapper() {
        auto _this = Handle__pop();
        auto mode = ViewMode__pop();
        Handle_setViewMode(_this, mode);
    }

    void Handle_setNameFilter__wrapper() {
        auto _this = Handle__pop();
        auto filter = popStringInternal();
        Handle_setNameFilter(_this, filter);
    }

    void Handle_setNameFilters__wrapper() {
        auto _this = Handle__pop();
        auto filters = popStringArrayInternal();
        Handle_setNameFilters(_this, filters);
    }

    void Handle_setMimeTypeFilters__wrapper() {
        auto _this = Handle__pop();
        auto filters = popStringArrayInternal();
        Handle_setMimeTypeFilters(_this, filters);
    }

    void Handle_setDirectory__wrapper() {
        auto _this = Handle__pop();
        auto dir = popStringInternal();
        Handle_setDirectory(_this, dir);
    }

    void Handle_selectFile__wrapper() {
        auto _this = Handle__pop();
        auto file = popStringInternal();
        Handle_selectFile(_this, file);
    }

    void Handle_selectedFiles__wrapper() {
        auto _this = Handle__pop();
        pushStringArrayInternal(Handle_selectedFiles(_this));
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
        auto parent = Widget::Handle__pop();
        auto handler = SignalHandler__pop();
        Handle__push(create(parent, handler));
    }

    int __register() {
        auto m = ni_registerModule("FileDialog");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Handle_setAcceptMode", &Handle_setAcceptMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setDefaultSuffix", &Handle_setDefaultSuffix__wrapper);
        ni_registerModuleMethod(m, "Handle_setFileMode", &Handle_setFileMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setOptions", &Handle_setOptions__wrapper);
        ni_registerModuleMethod(m, "Handle_setSupportedSchemes", &Handle_setSupportedSchemes__wrapper);
        ni_registerModuleMethod(m, "Handle_setViewMode", &Handle_setViewMode__wrapper);
        ni_registerModuleMethod(m, "Handle_setNameFilter", &Handle_setNameFilter__wrapper);
        ni_registerModuleMethod(m, "Handle_setNameFilters", &Handle_setNameFilters__wrapper);
        ni_registerModuleMethod(m, "Handle_setMimeTypeFilters", &Handle_setMimeTypeFilters__wrapper);
        ni_registerModuleMethod(m, "Handle_setDirectory", &Handle_setDirectory__wrapper);
        ni_registerModuleMethod(m, "Handle_selectFile", &Handle_selectFile__wrapper);
        ni_registerModuleMethod(m, "Handle_selectedFiles", &Handle_selectedFiles__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        signalHandler_accepted = ni_registerInterfaceMethod(signalHandler, "accepted", &SignalHandler_accepted__wrapper);
        signalHandler_finished = ni_registerInterfaceMethod(signalHandler, "finished", &SignalHandler_finished__wrapper);
        signalHandler_rejected = ni_registerInterfaceMethod(signalHandler, "rejected", &SignalHandler_rejected__wrapper);
        signalHandler_currentChanged = ni_registerInterfaceMethod(signalHandler, "currentChanged", &SignalHandler_currentChanged__wrapper);
        signalHandler_currentUrlChanged = ni_registerInterfaceMethod(signalHandler, "currentUrlChanged", &SignalHandler_currentUrlChanged__wrapper);
        signalHandler_directoryEntered = ni_registerInterfaceMethod(signalHandler, "directoryEntered", &SignalHandler_directoryEntered__wrapper);
        signalHandler_directoryUrlEntered = ni_registerInterfaceMethod(signalHandler, "directoryUrlEntered", &SignalHandler_directoryUrlEntered__wrapper);
        signalHandler_fileSelected = ni_registerInterfaceMethod(signalHandler, "fileSelected", &SignalHandler_fileSelected__wrapper);
        signalHandler_filesSelected = ni_registerInterfaceMethod(signalHandler, "filesSelected", &SignalHandler_filesSelected__wrapper);
        signalHandler_filterSelected = ni_registerInterfaceMethod(signalHandler, "filterSelected", &SignalHandler_filterSelected__wrapper);
        signalHandler_urlSelected = ni_registerInterfaceMethod(signalHandler, "urlSelected", &SignalHandler_urlSelected__wrapper);
        signalHandler_urlsSelected = ni_registerInterfaceMethod(signalHandler, "urlsSelected", &SignalHandler_urlsSelected__wrapper);
        return 0; // = OK
    }
}
