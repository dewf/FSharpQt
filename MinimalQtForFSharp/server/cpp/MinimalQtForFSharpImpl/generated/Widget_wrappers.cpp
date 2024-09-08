#include "../support/NativeImplServer.h"
#include "Widget_wrappers.h"
#include "Widget.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Layout_wrappers.h"
using namespace ::Layout;

#include "Painter_wrappers.h"
using namespace ::Painter;

#include "Icon_wrappers.h"
using namespace ::Icon;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Action_wrappers.h"
using namespace ::Action;

#include "Region_wrappers.h"
using namespace ::Region;

#include "Cursor_wrappers.h"
using namespace ::Cursor;

#include "SizePolicy_wrappers.h"
using namespace ::SizePolicy;

namespace Widget
{
    // built-in array type: std::vector<std::string>
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_customContextMenuRequested;
    ni_InterfaceMethodRef signalHandler_windowIconChanged;
    ni_InterfaceMethodRef signalHandler_windowTitleChanged;
    ni_InterfaceMethodRef methodDelegate_sizeHint;
    ni_InterfaceMethodRef methodDelegate_paintEvent;
    ni_InterfaceMethodRef methodDelegate_mousePressEvent;
    ni_InterfaceMethodRef methodDelegate_mouseMoveEvent;
    ni_InterfaceMethodRef methodDelegate_mouseReleaseEvent;
    ni_InterfaceMethodRef methodDelegate_enterEvent;
    ni_InterfaceMethodRef methodDelegate_leaveEvent;
    ni_InterfaceMethodRef methodDelegate_resizeEvent;
    ni_InterfaceMethodRef methodDelegate_dragMoveEvent;
    ni_InterfaceMethodRef methodDelegate_dragLeaveEvent;
    ni_InterfaceMethodRef methodDelegate_dropEvent;
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
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAcceptDrops__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAcceptDrops(_this, state);
    }

    void Handle_setAccessibleDescription__wrapper() {
        auto _this = Handle__pop();
        auto desc = popStringInternal();
        Handle_setAccessibleDescription(_this, desc);
    }

    void Handle_setAccessibleName__wrapper() {
        auto _this = Handle__pop();
        auto name = popStringInternal();
        Handle_setAccessibleName(_this, name);
    }

    void Handle_setAutoFillBackground__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAutoFillBackground(_this, state);
    }

    void Handle_setBaseSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setBaseSize(_this, size);
    }

    void Handle_childrenRect__wrapper() {
        auto _this = Handle__pop();
        Rect__push(Handle_childrenRect(_this), true);
    }

    void Handle_childrenRegion__wrapper() {
        auto _this = Handle__pop();
        Region::OwnedHandle__push(Handle_childrenRegion(_this));
    }

    void Handle_setContextMenuPolicy__wrapper() {
        auto _this = Handle__pop();
        auto policy = ContextMenuPolicy__pop();
        Handle_setContextMenuPolicy(_this, policy);
    }

    void Handle_getCursor__wrapper() {
        auto _this = Handle__pop();
        Cursor::OwnedHandle__push(Handle_getCursor(_this));
    }

    void Handle_setEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setEnabled(_this, enabled);
    }

    void Handle_hasFocus__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_hasFocus(_this));
    }

    void Handle_setFocusPolicy__wrapper() {
        auto _this = Handle__pop();
        auto policy = FocusPolicy__pop();
        Handle_setFocusPolicy(_this, policy);
    }

    void Handle_frameGeometry__wrapper() {
        auto _this = Handle__pop();
        Rect__push(Handle_frameGeometry(_this), true);
    }

    void Handle_frameSize__wrapper() {
        auto _this = Handle__pop();
        Size__push(Handle_frameSize(_this), true);
    }

    void Handle_isFullscreen__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isFullscreen(_this));
    }

    void Handle_setGeometry__wrapper() {
        auto _this = Handle__pop();
        auto rect = Rect__pop();
        Handle_setGeometry(_this, rect);
    }

    void Handle_setGeometry_overload1__wrapper() {
        auto _this = Handle__pop();
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Handle_setGeometry(_this, x, y, width, height);
    }

    void Handle_height__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_height(_this));
    }

    void Handle_setInputMethodHints__wrapper() {
        auto _this = Handle__pop();
        auto hints = InputMethodHints__pop();
        Handle_setInputMethodHints(_this, hints);
    }

    void Handle_isActiveWindow__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isActiveWindow(_this));
    }

    void Handle_setLayoutDirection__wrapper() {
        auto _this = Handle__pop();
        auto direction = LayoutDirection__pop();
        Handle_setLayoutDirection(_this, direction);
    }

    void Handle_isMaximized__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isMaximized(_this));
    }

    void Handle_setMaximumHeight__wrapper() {
        auto _this = Handle__pop();
        auto height = ni_popInt32();
        Handle_setMaximumHeight(_this, height);
    }

    void Handle_setMaximumWidth__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        Handle_setMaximumWidth(_this, width);
    }

    void Handle_setMaximumSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setMaximumSize(_this, size);
    }

    void Handle_isMinimized__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isMinimized(_this));
    }

    void Handle_setMinimumHeight__wrapper() {
        auto _this = Handle__pop();
        auto height = ni_popInt32();
        Handle_setMinimumHeight(_this, height);
    }

    void Handle_setMinimumSize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setMinimumSize(_this, size);
    }

    void Handle_minimumSizeHint__wrapper() {
        auto _this = Handle__pop();
        Size__push(Handle_minimumSizeHint(_this), true);
    }

    void Handle_setMinimumWidth__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        Handle_setMinimumWidth(_this, width);
    }

    void Handle_isModal__wrapper() {
        auto _this = Handle__pop();
        ni_pushBool(Handle_isModal(_this));
    }

    void Handle_setMouseTracking__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setMouseTracking(_this, state);
    }

    void Handle_normalGeometry__wrapper() {
        auto _this = Handle__pop();
        Rect__push(Handle_normalGeometry(_this), true);
    }

    void Handle_move__wrapper() {
        auto _this = Handle__pop();
        auto p = Point__pop();
        Handle_move(_this, p);
    }

    void Handle_move_overload1__wrapper() {
        auto _this = Handle__pop();
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        Handle_move(_this, x, y);
    }

    void Handle_rect__wrapper() {
        auto _this = Handle__pop();
        Rect__push(Handle_rect(_this), true);
    }

    void Handle_resize__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_resize(_this, size);
    }

    void Handle_resize_overload1__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Handle_resize(_this, width, height);
    }

    void Handle_sizeHint__wrapper() {
        auto _this = Handle__pop();
        Size__push(Handle_sizeHint(_this), true);
    }

    void Handle_setSizeIncrement__wrapper() {
        auto _this = Handle__pop();
        auto size = Size__pop();
        Handle_setSizeIncrement(_this, size);
    }

    void Handle_setSizeIncrement_overload1__wrapper() {
        auto _this = Handle__pop();
        auto w = ni_popInt32();
        auto h = ni_popInt32();
        Handle_setSizeIncrement(_this, w, h);
    }

    void Handle_setSizePolicy__wrapper() {
        auto _this = Handle__pop();
        auto policy = SizePolicy::Deferred__pop();
        Handle_setSizePolicy(_this, policy);
    }

    void Handle_setSizePolicy_overload1__wrapper() {
        auto _this = Handle__pop();
        auto hPolicy = Policy__pop();
        auto vPolicy = Policy__pop();
        Handle_setSizePolicy(_this, hPolicy, vPolicy);
    }

    void Handle_setStatusTip__wrapper() {
        auto _this = Handle__pop();
        auto tip = popStringInternal();
        Handle_setStatusTip(_this, tip);
    }

    void Handle_setStyleSheet__wrapper() {
        auto _this = Handle__pop();
        auto styles = popStringInternal();
        Handle_setStyleSheet(_this, styles);
    }

    void Handle_setTabletTracking__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setTabletTracking(_this, state);
    }

    void Handle_setToolTip__wrapper() {
        auto _this = Handle__pop();
        auto tip = popStringInternal();
        Handle_setToolTip(_this, tip);
    }

    void Handle_setToolTipDuration__wrapper() {
        auto _this = Handle__pop();
        auto duration = ni_popInt32();
        Handle_setToolTipDuration(_this, duration);
    }

    void Handle_setUpdatesEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setUpdatesEnabled(_this, enabled);
    }

    void Handle_setVisible__wrapper() {
        auto _this = Handle__pop();
        auto visible = ni_popBool();
        Handle_setVisible(_this, visible);
    }

    void Handle_setWhatsThis__wrapper() {
        auto _this = Handle__pop();
        auto text = popStringInternal();
        Handle_setWhatsThis(_this, text);
    }

    void Handle_width__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_width(_this));
    }

    void Handle_setWindowFilePath__wrapper() {
        auto _this = Handle__pop();
        auto path = popStringInternal();
        Handle_setWindowFilePath(_this, path);
    }

    void Handle_setWindowFlags__wrapper() {
        auto _this = Handle__pop();
        auto flags_ = WindowFlags__pop();
        Handle_setWindowFlags(_this, flags_);
    }

    void Handle_setWindowIcon__wrapper() {
        auto _this = Handle__pop();
        auto icon = Icon::Deferred__pop();
        Handle_setWindowIcon(_this, icon);
    }

    void Handle_setWindowModality__wrapper() {
        auto _this = Handle__pop();
        auto modality = WindowModality__pop();
        Handle_setWindowModality(_this, modality);
    }

    void Handle_setWindowModified__wrapper() {
        auto _this = Handle__pop();
        auto modified = ni_popBool();
        Handle_setWindowModified(_this, modified);
    }

    void Handle_setWindowOpacity__wrapper() {
        auto _this = Handle__pop();
        auto opacity = ni_popDouble();
        Handle_setWindowOpacity(_this, opacity);
    }

    void Handle_setWindowTitle__wrapper() {
        auto _this = Handle__pop();
        auto title = popStringInternal();
        Handle_setWindowTitle(_this, title);
    }

    void Handle_x__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_x(_this));
    }

    void Handle_y__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_y(_this));
    }

    void Handle_addAction__wrapper() {
        auto _this = Handle__pop();
        auto action = Action::Handle__pop();
        Handle_addAction(_this, action);
    }

    void Handle_setParent__wrapper() {
        auto _this = Handle__pop();
        auto parent = Handle__pop();
        Handle_setParent(_this, parent);
    }

    void Handle_getWindow__wrapper() {
        auto _this = Handle__pop();
        Handle__push(Handle_getWindow(_this));
    }

    void Handle_updateGeometry__wrapper() {
        auto _this = Handle__pop();
        Handle_updateGeometry(_this);
    }

    void Handle_adjustSize__wrapper() {
        auto _this = Handle__pop();
        Handle_adjustSize(_this);
    }

    void Handle_setFixedWidth__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        Handle_setFixedWidth(_this, width);
    }

    void Handle_setFixedHeight__wrapper() {
        auto _this = Handle__pop();
        auto height = ni_popInt32();
        Handle_setFixedHeight(_this, height);
    }

    void Handle_setFixedSize__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Handle_setFixedSize(_this, width, height);
    }

    void Handle_show__wrapper() {
        auto _this = Handle__pop();
        Handle_show(_this);
    }

    void Handle_hide__wrapper() {
        auto _this = Handle__pop();
        Handle_hide(_this);
    }

    void Handle_update__wrapper() {
        auto _this = Handle__pop();
        Handle_update(_this);
    }

    void Handle_update_overload1__wrapper() {
        auto _this = Handle__pop();
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Handle_update(_this, x, y, width, height);
    }

    void Handle_update_overload2__wrapper() {
        auto _this = Handle__pop();
        auto rect = Rect__pop();
        Handle_update(_this, rect);
    }

    void Handle_setLayout__wrapper() {
        auto _this = Handle__pop();
        auto layout = Layout::Handle__pop();
        Handle_setLayout(_this, layout);
    }

    void Handle_getLayout__wrapper() {
        auto _this = Handle__pop();
        Layout::Handle__push(Handle_getLayout(_this));
    }

    void Handle_mapToGlobal__wrapper() {
        auto _this = Handle__pop();
        auto p = Point__pop();
        Point__push(Handle_mapToGlobal(_this, p), true);
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

    void createNoHandler__wrapper() {
        Handle__push(createNoHandler());
    }
    void Event__push(EventRef value) {
        ni_pushPtr(value);
    }

    EventRef Event__pop() {
        return (EventRef)ni_popPtr();
    }

    void Event_accept__wrapper() {
        auto _this = Event__pop();
        Event_accept(_this);
    }

    void Event_ignore__wrapper() {
        auto _this = Event__pop();
        Event_ignore(_this);
    }
    void MimeData__push(MimeDataRef value) {
        ni_pushPtr(value);
    }

    MimeDataRef MimeData__pop() {
        return (MimeDataRef)ni_popPtr();
    }

    void MimeData_formats__wrapper() {
        auto _this = MimeData__pop();
        pushStringArrayInternal(MimeData_formats(_this));
    }

    void MimeData_hasFormat__wrapper() {
        auto _this = MimeData__pop();
        auto mimeType = popStringInternal();
        ni_pushBool(MimeData_hasFormat(_this, mimeType));
    }

    void MimeData_text__wrapper() {
        auto _this = MimeData__pop();
        pushStringInternal(MimeData_text(_this));
    }

    void MimeData_setText__wrapper() {
        auto _this = MimeData__pop();
        auto text = popStringInternal();
        MimeData_setText(_this, text);
    }

    void MimeData_urls__wrapper() {
        auto _this = MimeData__pop();
        pushStringArrayInternal(MimeData_urls(_this));
    }

    void MimeData_setUrls__wrapper() {
        auto _this = MimeData__pop();
        auto urls = popStringArrayInternal();
        MimeData_setUrls(_this, urls);
    }

    void createMimeData__wrapper() {
        MimeData__push(createMimeData());
    }
    void Drag__push(DragRef value) {
        ni_pushPtr(value);
    }

    DragRef Drag__pop() {
        return (DragRef)ni_popPtr();
    }

    void Drag_setMimeData__wrapper() {
        auto _this = Drag__pop();
        auto data = MimeData__pop();
        Drag_setMimeData(_this, data);
    }

    void Drag_exec__wrapper() {
        auto _this = Drag__pop();
        auto supportedActions = DropActionSet__pop();
        auto defaultAction = DropAction__pop();
        DropAction__push(Drag_exec(_this, supportedActions, defaultAction));
    }

    void createDrag__wrapper() {
        auto parent = Handle__pop();
        Drag__push(createDrag(parent));
    }
    void DragMoveEvent__push(DragMoveEventRef value) {
        ni_pushPtr(value);
    }

    DragMoveEventRef DragMoveEvent__pop() {
        return (DragMoveEventRef)ni_popPtr();
    }

    void DragMoveEvent_proposedAction__wrapper() {
        auto _this = DragMoveEvent__pop();
        DropAction__push(DragMoveEvent_proposedAction(_this));
    }

    void DragMoveEvent_acceptProposedAction__wrapper() {
        auto _this = DragMoveEvent__pop();
        DragMoveEvent_acceptProposedAction(_this);
    }

    void DragMoveEvent_possibleActions__wrapper() {
        auto _this = DragMoveEvent__pop();
        DropActionSet__push(DragMoveEvent_possibleActions(_this));
    }

    void DragMoveEvent_acceptDropAction__wrapper() {
        auto _this = DragMoveEvent__pop();
        auto action = DropAction__pop();
        DragMoveEvent_acceptDropAction(_this, action);
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
        Size sizeHint() override {
            invokeMethod(methodDelegate_sizeHint);
            return Size__pop();
        }
        void paintEvent(Painter::HandleRef painter, Rect updateRect) override {
            Rect__push(updateRect, false);
            Painter::Handle__push(painter);
            invokeMethod(methodDelegate_paintEvent);
        }
        void mousePressEvent(Point pos, MouseButton button, Modifiers modifiers) override {
            Modifiers__push(modifiers);
            MouseButton__push(button);
            Point__push(pos, false);
            invokeMethod(methodDelegate_mousePressEvent);
        }
        void mouseMoveEvent(Point pos, MouseButtonSet buttons, Modifiers modifiers) override {
            Modifiers__push(modifiers);
            MouseButtonSet__push(buttons);
            Point__push(pos, false);
            invokeMethod(methodDelegate_mouseMoveEvent);
        }
        void mouseReleaseEvent(Point pos, MouseButton button, Modifiers modifiers) override {
            Modifiers__push(modifiers);
            MouseButton__push(button);
            Point__push(pos, false);
            invokeMethod(methodDelegate_mouseReleaseEvent);
        }
        void enterEvent(Point pos) override {
            Point__push(pos, false);
            invokeMethod(methodDelegate_enterEvent);
        }
        void leaveEvent() override {
            invokeMethod(methodDelegate_leaveEvent);
        }
        void resizeEvent(Size oldSize, Size newSize) override {
            Size__push(newSize, false);
            Size__push(oldSize, false);
            invokeMethod(methodDelegate_resizeEvent);
        }
        void dragMoveEvent(Point pos, Modifiers modifiers, MimeDataRef mimeData, DragMoveEventRef moveEvent, bool isEnterEvent) override {
            ni_pushBool(isEnterEvent);
            DragMoveEvent__push(moveEvent);
            MimeData__push(mimeData);
            Modifiers__push(modifiers);
            Point__push(pos, false);
            invokeMethod(methodDelegate_dragMoveEvent);
        }
        void dragLeaveEvent() override {
            invokeMethod(methodDelegate_dragLeaveEvent);
        }
        void dropEvent(Point pos, Modifiers modifiers, MimeDataRef mimeData, DropAction action) override {
            DropAction__push(action);
            MimeData__push(mimeData);
            Modifiers__push(modifiers);
            Point__push(pos, false);
            invokeMethod(methodDelegate_dropEvent);
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

    void MethodDelegate_sizeHint__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        Size__push(inst->sizeHint(), true);
    }

    void MethodDelegate_paintEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto painter = Painter::Handle__pop();
        auto updateRect = Rect__pop();
        inst->paintEvent(painter, updateRect);
    }

    void MethodDelegate_mousePressEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        auto button = MouseButton__pop();
        auto modifiers = Modifiers__pop();
        inst->mousePressEvent(pos, button, modifiers);
    }

    void MethodDelegate_mouseMoveEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        auto buttons = MouseButtonSet__pop();
        auto modifiers = Modifiers__pop();
        inst->mouseMoveEvent(pos, buttons, modifiers);
    }

    void MethodDelegate_mouseReleaseEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        auto button = MouseButton__pop();
        auto modifiers = Modifiers__pop();
        inst->mouseReleaseEvent(pos, button, modifiers);
    }

    void MethodDelegate_enterEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        inst->enterEvent(pos);
    }

    void MethodDelegate_leaveEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->leaveEvent();
    }

    void MethodDelegate_resizeEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto oldSize = Size__pop();
        auto newSize = Size__pop();
        inst->resizeEvent(oldSize, newSize);
    }

    void MethodDelegate_dragMoveEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        auto modifiers = Modifiers__pop();
        auto mimeData = MimeData__pop();
        auto moveEvent = DragMoveEvent__pop();
        auto isEnterEvent = ni_popBool();
        inst->dragMoveEvent(pos, modifiers, mimeData, moveEvent, isEnterEvent);
    }

    void MethodDelegate_dragLeaveEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->dragLeaveEvent();
    }

    void MethodDelegate_dropEvent__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto pos = Point__pop();
        auto modifiers = Modifiers__pop();
        auto mimeData = MimeData__pop();
        auto action = DropAction__pop();
        inst->dropEvent(pos, modifiers, mimeData, action);
    }

    void createSubclassed__wrapper() {
        auto methodDelegate = MethodDelegate__pop();
        auto methodMask = MethodMask__pop();
        auto handler = SignalHandler__pop();
        Handle__push(createSubclassed(methodDelegate, methodMask, handler));
    }

    void __constantsFunc() {
        ni_pushInt32(WIDGET_SIZE_MAX);
    }

    int __register() {
        auto m = ni_registerModule("Widget");
        ni_registerModuleConstants(m, &__constantsFunc);
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "createNoHandler", &createNoHandler__wrapper);
        ni_registerModuleMethod(m, "createMimeData", &createMimeData__wrapper);
        ni_registerModuleMethod(m, "createDrag", &createDrag__wrapper);
        ni_registerModuleMethod(m, "createSubclassed", &createSubclassed__wrapper);
        ni_registerModuleMethod(m, "Handle_setAcceptDrops", &Handle_setAcceptDrops__wrapper);
        ni_registerModuleMethod(m, "Handle_setAccessibleDescription", &Handle_setAccessibleDescription__wrapper);
        ni_registerModuleMethod(m, "Handle_setAccessibleName", &Handle_setAccessibleName__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoFillBackground", &Handle_setAutoFillBackground__wrapper);
        ni_registerModuleMethod(m, "Handle_setBaseSize", &Handle_setBaseSize__wrapper);
        ni_registerModuleMethod(m, "Handle_childrenRect", &Handle_childrenRect__wrapper);
        ni_registerModuleMethod(m, "Handle_childrenRegion", &Handle_childrenRegion__wrapper);
        ni_registerModuleMethod(m, "Handle_setContextMenuPolicy", &Handle_setContextMenuPolicy__wrapper);
        ni_registerModuleMethod(m, "Handle_getCursor", &Handle_getCursor__wrapper);
        ni_registerModuleMethod(m, "Handle_setEnabled", &Handle_setEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_hasFocus", &Handle_hasFocus__wrapper);
        ni_registerModuleMethod(m, "Handle_setFocusPolicy", &Handle_setFocusPolicy__wrapper);
        ni_registerModuleMethod(m, "Handle_frameGeometry", &Handle_frameGeometry__wrapper);
        ni_registerModuleMethod(m, "Handle_frameSize", &Handle_frameSize__wrapper);
        ni_registerModuleMethod(m, "Handle_isFullscreen", &Handle_isFullscreen__wrapper);
        ni_registerModuleMethod(m, "Handle_setGeometry", &Handle_setGeometry__wrapper);
        ni_registerModuleMethod(m, "Handle_setGeometry_overload1", &Handle_setGeometry_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_height", &Handle_height__wrapper);
        ni_registerModuleMethod(m, "Handle_setInputMethodHints", &Handle_setInputMethodHints__wrapper);
        ni_registerModuleMethod(m, "Handle_isActiveWindow", &Handle_isActiveWindow__wrapper);
        ni_registerModuleMethod(m, "Handle_setLayoutDirection", &Handle_setLayoutDirection__wrapper);
        ni_registerModuleMethod(m, "Handle_isMaximized", &Handle_isMaximized__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximumHeight", &Handle_setMaximumHeight__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximumWidth", &Handle_setMaximumWidth__wrapper);
        ni_registerModuleMethod(m, "Handle_setMaximumSize", &Handle_setMaximumSize__wrapper);
        ni_registerModuleMethod(m, "Handle_isMinimized", &Handle_isMinimized__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimumHeight", &Handle_setMinimumHeight__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimumSize", &Handle_setMinimumSize__wrapper);
        ni_registerModuleMethod(m, "Handle_minimumSizeHint", &Handle_minimumSizeHint__wrapper);
        ni_registerModuleMethod(m, "Handle_setMinimumWidth", &Handle_setMinimumWidth__wrapper);
        ni_registerModuleMethod(m, "Handle_isModal", &Handle_isModal__wrapper);
        ni_registerModuleMethod(m, "Handle_setMouseTracking", &Handle_setMouseTracking__wrapper);
        ni_registerModuleMethod(m, "Handle_normalGeometry", &Handle_normalGeometry__wrapper);
        ni_registerModuleMethod(m, "Handle_move", &Handle_move__wrapper);
        ni_registerModuleMethod(m, "Handle_move_overload1", &Handle_move_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_rect", &Handle_rect__wrapper);
        ni_registerModuleMethod(m, "Handle_resize", &Handle_resize__wrapper);
        ni_registerModuleMethod(m, "Handle_resize_overload1", &Handle_resize_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_sizeHint", &Handle_sizeHint__wrapper);
        ni_registerModuleMethod(m, "Handle_setSizeIncrement", &Handle_setSizeIncrement__wrapper);
        ni_registerModuleMethod(m, "Handle_setSizeIncrement_overload1", &Handle_setSizeIncrement_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_setSizePolicy", &Handle_setSizePolicy__wrapper);
        ni_registerModuleMethod(m, "Handle_setSizePolicy_overload1", &Handle_setSizePolicy_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_setStatusTip", &Handle_setStatusTip__wrapper);
        ni_registerModuleMethod(m, "Handle_setStyleSheet", &Handle_setStyleSheet__wrapper);
        ni_registerModuleMethod(m, "Handle_setTabletTracking", &Handle_setTabletTracking__wrapper);
        ni_registerModuleMethod(m, "Handle_setToolTip", &Handle_setToolTip__wrapper);
        ni_registerModuleMethod(m, "Handle_setToolTipDuration", &Handle_setToolTipDuration__wrapper);
        ni_registerModuleMethod(m, "Handle_setUpdatesEnabled", &Handle_setUpdatesEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setVisible", &Handle_setVisible__wrapper);
        ni_registerModuleMethod(m, "Handle_setWhatsThis", &Handle_setWhatsThis__wrapper);
        ni_registerModuleMethod(m, "Handle_width", &Handle_width__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowFilePath", &Handle_setWindowFilePath__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowFlags", &Handle_setWindowFlags__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowIcon", &Handle_setWindowIcon__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowModality", &Handle_setWindowModality__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowModified", &Handle_setWindowModified__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowOpacity", &Handle_setWindowOpacity__wrapper);
        ni_registerModuleMethod(m, "Handle_setWindowTitle", &Handle_setWindowTitle__wrapper);
        ni_registerModuleMethod(m, "Handle_x", &Handle_x__wrapper);
        ni_registerModuleMethod(m, "Handle_y", &Handle_y__wrapper);
        ni_registerModuleMethod(m, "Handle_addAction", &Handle_addAction__wrapper);
        ni_registerModuleMethod(m, "Handle_setParent", &Handle_setParent__wrapper);
        ni_registerModuleMethod(m, "Handle_getWindow", &Handle_getWindow__wrapper);
        ni_registerModuleMethod(m, "Handle_updateGeometry", &Handle_updateGeometry__wrapper);
        ni_registerModuleMethod(m, "Handle_adjustSize", &Handle_adjustSize__wrapper);
        ni_registerModuleMethod(m, "Handle_setFixedWidth", &Handle_setFixedWidth__wrapper);
        ni_registerModuleMethod(m, "Handle_setFixedHeight", &Handle_setFixedHeight__wrapper);
        ni_registerModuleMethod(m, "Handle_setFixedSize", &Handle_setFixedSize__wrapper);
        ni_registerModuleMethod(m, "Handle_show", &Handle_show__wrapper);
        ni_registerModuleMethod(m, "Handle_hide", &Handle_hide__wrapper);
        ni_registerModuleMethod(m, "Handle_update", &Handle_update__wrapper);
        ni_registerModuleMethod(m, "Handle_update_overload1", &Handle_update_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_update_overload2", &Handle_update_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_setLayout", &Handle_setLayout__wrapper);
        ni_registerModuleMethod(m, "Handle_getLayout", &Handle_getLayout__wrapper);
        ni_registerModuleMethod(m, "Handle_mapToGlobal", &Handle_mapToGlobal__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        ni_registerModuleMethod(m, "Event_accept", &Event_accept__wrapper);
        ni_registerModuleMethod(m, "Event_ignore", &Event_ignore__wrapper);
        ni_registerModuleMethod(m, "MimeData_formats", &MimeData_formats__wrapper);
        ni_registerModuleMethod(m, "MimeData_hasFormat", &MimeData_hasFormat__wrapper);
        ni_registerModuleMethod(m, "MimeData_text", &MimeData_text__wrapper);
        ni_registerModuleMethod(m, "MimeData_setText", &MimeData_setText__wrapper);
        ni_registerModuleMethod(m, "MimeData_urls", &MimeData_urls__wrapper);
        ni_registerModuleMethod(m, "MimeData_setUrls", &MimeData_setUrls__wrapper);
        ni_registerModuleMethod(m, "Drag_setMimeData", &Drag_setMimeData__wrapper);
        ni_registerModuleMethod(m, "Drag_exec", &Drag_exec__wrapper);
        ni_registerModuleMethod(m, "DragMoveEvent_proposedAction", &DragMoveEvent_proposedAction__wrapper);
        ni_registerModuleMethod(m, "DragMoveEvent_acceptProposedAction", &DragMoveEvent_acceptProposedAction__wrapper);
        ni_registerModuleMethod(m, "DragMoveEvent_possibleActions", &DragMoveEvent_possibleActions__wrapper);
        ni_registerModuleMethod(m, "DragMoveEvent_acceptDropAction", &DragMoveEvent_acceptDropAction__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_customContextMenuRequested = ni_registerInterfaceMethod(signalHandler, "customContextMenuRequested", &SignalHandler_customContextMenuRequested__wrapper);
        signalHandler_windowIconChanged = ni_registerInterfaceMethod(signalHandler, "windowIconChanged", &SignalHandler_windowIconChanged__wrapper);
        signalHandler_windowTitleChanged = ni_registerInterfaceMethod(signalHandler, "windowTitleChanged", &SignalHandler_windowTitleChanged__wrapper);
        auto methodDelegate = ni_registerInterface(m, "MethodDelegate");
        methodDelegate_sizeHint = ni_registerInterfaceMethod(methodDelegate, "sizeHint", &MethodDelegate_sizeHint__wrapper);
        methodDelegate_paintEvent = ni_registerInterfaceMethod(methodDelegate, "paintEvent", &MethodDelegate_paintEvent__wrapper);
        methodDelegate_mousePressEvent = ni_registerInterfaceMethod(methodDelegate, "mousePressEvent", &MethodDelegate_mousePressEvent__wrapper);
        methodDelegate_mouseMoveEvent = ni_registerInterfaceMethod(methodDelegate, "mouseMoveEvent", &MethodDelegate_mouseMoveEvent__wrapper);
        methodDelegate_mouseReleaseEvent = ni_registerInterfaceMethod(methodDelegate, "mouseReleaseEvent", &MethodDelegate_mouseReleaseEvent__wrapper);
        methodDelegate_enterEvent = ni_registerInterfaceMethod(methodDelegate, "enterEvent", &MethodDelegate_enterEvent__wrapper);
        methodDelegate_leaveEvent = ni_registerInterfaceMethod(methodDelegate, "leaveEvent", &MethodDelegate_leaveEvent__wrapper);
        methodDelegate_resizeEvent = ni_registerInterfaceMethod(methodDelegate, "resizeEvent", &MethodDelegate_resizeEvent__wrapper);
        methodDelegate_dragMoveEvent = ni_registerInterfaceMethod(methodDelegate, "dragMoveEvent", &MethodDelegate_dragMoveEvent__wrapper);
        methodDelegate_dragLeaveEvent = ni_registerInterfaceMethod(methodDelegate, "dragLeaveEvent", &MethodDelegate_dragLeaveEvent__wrapper);
        methodDelegate_dropEvent = ni_registerInterfaceMethod(methodDelegate, "dropEvent", &MethodDelegate_dropEvent__wrapper);
        return 0; // = OK
    }
}
