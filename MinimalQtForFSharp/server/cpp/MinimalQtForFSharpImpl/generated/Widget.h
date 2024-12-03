#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

#include "Object.h"
using namespace ::Object;
#include "Common.h"
using namespace ::Common;
#include "Layout.h"
using namespace ::Layout;
#include "Painter.h"
using namespace ::Painter;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;
#include "Action.h"
using namespace ::Action;
#include "Region.h"
using namespace ::Region;
#include "Cursor.h"
using namespace ::Cursor;
#include "SizePolicy.h"
using namespace ::SizePolicy;

namespace Widget
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Object::HandleRef
    struct __Event; typedef struct __Event* EventRef;
    struct __MimeData; typedef struct __MimeData* MimeDataRef;
    struct __Drag; typedef struct __Drag* DragRef;
    struct __DragMoveEvent; typedef struct __DragMoveEvent* DragMoveEventRef; // extends EventRef
    extern const int32_t WIDGET_SIZE_MAX;

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
    };

    void Handle_setAcceptDrops(HandleRef _this, bool state);
    void Handle_setAccessibleDescription(HandleRef _this, std::string desc);
    void Handle_setAccessibleName(HandleRef _this, std::string name);
    void Handle_setAutoFillBackground(HandleRef _this, bool state);
    void Handle_setBaseSize(HandleRef _this, Size size);
    Rect Handle_childrenRect(HandleRef _this);
    Region::OwnedRef Handle_childrenRegion(HandleRef _this);
    void Handle_setContextMenuPolicy(HandleRef _this, ContextMenuPolicy policy);
    Cursor::OwnedRef Handle_getCursor(HandleRef _this);
    void Handle_setEnabled(HandleRef _this, bool enabled);
    bool Handle_hasFocus(HandleRef _this);
    void Handle_setFocusPolicy(HandleRef _this, FocusPolicy policy);
    Rect Handle_frameGeometry(HandleRef _this);
    Size Handle_frameSize(HandleRef _this);
    bool Handle_isFullscreen(HandleRef _this);
    void Handle_setGeometry(HandleRef _this, Rect rect);
    void Handle_setGeometry(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height);
    int32_t Handle_height(HandleRef _this);
    void Handle_setInputMethodHints(HandleRef _this, InputMethodHints hints);
    bool Handle_isActiveWindow(HandleRef _this);
    void Handle_setLayoutDirection(HandleRef _this, LayoutDirection direction);
    bool Handle_isMaximized(HandleRef _this);
    void Handle_setMaximumHeight(HandleRef _this, int32_t height);
    void Handle_setMaximumWidth(HandleRef _this, int32_t width);
    void Handle_setMaximumSize(HandleRef _this, Size size);
    bool Handle_isMinimized(HandleRef _this);
    void Handle_setMinimumHeight(HandleRef _this, int32_t height);
    void Handle_setMinimumSize(HandleRef _this, Size size);
    Size Handle_minimumSizeHint(HandleRef _this);
    void Handle_setMinimumWidth(HandleRef _this, int32_t width);
    bool Handle_isModal(HandleRef _this);
    void Handle_setMouseTracking(HandleRef _this, bool state);
    Rect Handle_normalGeometry(HandleRef _this);
    void Handle_move(HandleRef _this, Point p);
    void Handle_move(HandleRef _this, int32_t x, int32_t y);
    Rect Handle_rect(HandleRef _this);
    void Handle_resize(HandleRef _this, Size size);
    void Handle_resize(HandleRef _this, int32_t width, int32_t height);
    Size Handle_sizeHint(HandleRef _this);
    void Handle_setSizeIncrement(HandleRef _this, Size size);
    void Handle_setSizeIncrement(HandleRef _this, int32_t w, int32_t h);
    void Handle_setSizePolicy(HandleRef _this, std::shared_ptr<SizePolicy::Deferred::Base> policy);
    void Handle_setSizePolicy(HandleRef _this, Policy hPolicy, Policy vPolicy);
    void Handle_setStatusTip(HandleRef _this, std::string tip);
    void Handle_setStyleSheet(HandleRef _this, std::string styles);
    void Handle_setTabletTracking(HandleRef _this, bool state);
    void Handle_setToolTip(HandleRef _this, std::string tip);
    void Handle_setToolTipDuration(HandleRef _this, int32_t duration);
    void Handle_setUpdatesEnabled(HandleRef _this, bool enabled);
    void Handle_setVisible(HandleRef _this, bool visible);
    void Handle_setWhatsThis(HandleRef _this, std::string text);
    int32_t Handle_width(HandleRef _this);
    void Handle_setWindowFilePath(HandleRef _this, std::string path);
    void Handle_setWindowFlags(HandleRef _this, WindowFlags flags_);
    void Handle_setWindowIcon(HandleRef _this, Icon::HandleRef icon);
    void Handle_setWindowModality(HandleRef _this, WindowModality modality);
    void Handle_setWindowModified(HandleRef _this, bool modified);
    void Handle_setWindowOpacity(HandleRef _this, double opacity);
    void Handle_setWindowTitle(HandleRef _this, std::string title);
    int32_t Handle_x(HandleRef _this);
    int32_t Handle_y(HandleRef _this);
    void Handle_addAction(HandleRef _this, Action::HandleRef action);
    void Handle_setParent(HandleRef _this, HandleRef parent);
    HandleRef Handle_getWindow(HandleRef _this);
    void Handle_updateGeometry(HandleRef _this);
    void Handle_adjustSize(HandleRef _this);
    void Handle_setFixedWidth(HandleRef _this, int32_t width);
    void Handle_setFixedHeight(HandleRef _this, int32_t height);
    void Handle_setFixedSize(HandleRef _this, Size size);
    void Handle_setFixedSize(HandleRef _this, int32_t width, int32_t height);
    void Handle_show(HandleRef _this);
    void Handle_hide(HandleRef _this);
    void Handle_update(HandleRef _this);
    void Handle_update(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height);
    void Handle_update(HandleRef _this, Rect rect);
    void Handle_setLayout(HandleRef _this, Layout::HandleRef layout);
    Layout::HandleRef Handle_getLayout(HandleRef _this);
    Point Handle_mapToGlobal(HandleRef _this, Point p);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
    HandleRef createNoHandler();

    void Event_accept(EventRef _this);
    void Event_ignore(EventRef _this);

    std::vector<std::string> MimeData_formats(MimeDataRef _this);
    bool MimeData_hasFormat(MimeDataRef _this, std::string mimeType);
    std::string MimeData_text(MimeDataRef _this);
    void MimeData_setText(MimeDataRef _this, std::string text);
    std::vector<std::string> MimeData_urls(MimeDataRef _this);
    void MimeData_setUrls(MimeDataRef _this, std::vector<std::string> urls);
    MimeDataRef createMimeData();

    void Drag_setMimeData(DragRef _this, MimeDataRef data);
    DropAction Drag_exec(DragRef _this, DropActionSet supportedActions, DropAction defaultAction);
    DragRef createDrag(HandleRef parent);

    DropAction DragMoveEvent_proposedAction(DragMoveEventRef _this);
    void DragMoveEvent_acceptProposedAction(DragMoveEventRef _this);
    DropActionSet DragMoveEvent_possibleActions(DragMoveEventRef _this);
    void DragMoveEvent_acceptDropAction(DragMoveEventRef _this, DropAction action);

    typedef int32_t MethodMask;
    enum MethodMaskFlags: int32_t {
        // MethodMask:
        ShowEvent = 1 << 0,
        PaintEvent = 1 << 1,
        MousePressEvent = 1 << 2,
        MouseMoveEvent = 1 << 3,
        MouseReleaseEvent = 1 << 4,
        EnterEvent = 1 << 5,
        LeaveEvent = 1 << 6,
        SizeHint = 1 << 7,
        ResizeEvent = 1 << 8,
        DropEvents = 1 << 9
    };

    class MethodDelegate {
    public:
        virtual void showEvent(bool isSpontaneous) = 0;
        virtual void paintEvent(Painter::HandleRef painter, Rect updateRect) = 0;
        virtual void mousePressEvent(Point pos, MouseButton button, Modifiers modifiers) = 0;
        virtual void mouseMoveEvent(Point pos, MouseButtonSet buttons, Modifiers modifiers) = 0;
        virtual void mouseReleaseEvent(Point pos, MouseButton button, Modifiers modifiers) = 0;
        virtual void enterEvent(Point pos) = 0;
        virtual void leaveEvent() = 0;
        virtual Size sizeHint() = 0;
        virtual void resizeEvent(Size oldSize, Size newSize) = 0;
        virtual void dragMoveEvent(Point pos, Modifiers modifiers, MimeDataRef mimeData, DragMoveEventRef moveEvent, bool isEnterEvent) = 0;
        virtual void dragLeaveEvent() = 0;
        virtual void dropEvent(Point pos, Modifiers modifiers, MimeDataRef mimeData, DropAction action) = 0;
    };
    HandleRef createSubclassed(std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask, std::shared_ptr<SignalHandler> handler);
}
