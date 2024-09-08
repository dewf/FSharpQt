#pragma once
#include "Widget.h"

namespace Widget
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_customContextMenuRequested__wrapper(int serverID);

    void SignalHandler_windowIconChanged__wrapper(int serverID);

    void SignalHandler_windowTitleChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAcceptDrops__wrapper();

    void Handle_setAccessibleDescription__wrapper();

    void Handle_setAccessibleName__wrapper();

    void Handle_setAutoFillBackground__wrapper();

    void Handle_setBaseSize__wrapper();

    void Handle_childrenRect__wrapper();

    void Handle_childrenRegion__wrapper();

    void Handle_setContextMenuPolicy__wrapper();

    void Handle_getCursor__wrapper();

    void Handle_setEnabled__wrapper();

    void Handle_hasFocus__wrapper();

    void Handle_setFocusPolicy__wrapper();

    void Handle_frameGeometry__wrapper();

    void Handle_frameSize__wrapper();

    void Handle_isFullscreen__wrapper();

    void Handle_setGeometry__wrapper();

    void Handle_setGeometry_overload1__wrapper();

    void Handle_height__wrapper();

    void Handle_setInputMethodHints__wrapper();

    void Handle_isActiveWindow__wrapper();

    void Handle_setLayoutDirection__wrapper();

    void Handle_isMaximized__wrapper();

    void Handle_setMaximumHeight__wrapper();

    void Handle_setMaximumWidth__wrapper();

    void Handle_setMaximumSize__wrapper();

    void Handle_isMinimized__wrapper();

    void Handle_setMinimumHeight__wrapper();

    void Handle_setMinimumSize__wrapper();

    void Handle_minimumSizeHint__wrapper();

    void Handle_setMinimumWidth__wrapper();

    void Handle_isModal__wrapper();

    void Handle_setMouseTracking__wrapper();

    void Handle_normalGeometry__wrapper();

    void Handle_move__wrapper();

    void Handle_move_overload1__wrapper();

    void Handle_rect__wrapper();

    void Handle_resize__wrapper();

    void Handle_resize_overload1__wrapper();

    void Handle_sizeHint__wrapper();

    void Handle_setSizeIncrement__wrapper();

    void Handle_setSizeIncrement_overload1__wrapper();

    void Handle_setSizePolicy__wrapper();

    void Handle_setSizePolicy_overload1__wrapper();

    void Handle_setStatusTip__wrapper();

    void Handle_setStyleSheet__wrapper();

    void Handle_setTabletTracking__wrapper();

    void Handle_setToolTip__wrapper();

    void Handle_setToolTipDuration__wrapper();

    void Handle_setUpdatesEnabled__wrapper();

    void Handle_setVisible__wrapper();

    void Handle_setWhatsThis__wrapper();

    void Handle_width__wrapper();

    void Handle_setWindowFilePath__wrapper();

    void Handle_setWindowFlags__wrapper();

    void Handle_setWindowIcon__wrapper();

    void Handle_setWindowModality__wrapper();

    void Handle_setWindowModified__wrapper();

    void Handle_setWindowOpacity__wrapper();

    void Handle_setWindowTitle__wrapper();

    void Handle_x__wrapper();

    void Handle_y__wrapper();

    void Handle_addAction__wrapper();

    void Handle_setParent__wrapper();

    void Handle_getWindow__wrapper();

    void Handle_updateGeometry__wrapper();

    void Handle_adjustSize__wrapper();

    void Handle_setFixedWidth__wrapper();

    void Handle_setFixedHeight__wrapper();

    void Handle_setFixedSize__wrapper();

    void Handle_show__wrapper();

    void Handle_hide__wrapper();

    void Handle_update__wrapper();

    void Handle_update_overload1__wrapper();

    void Handle_update_overload2__wrapper();

    void Handle_setLayout__wrapper();

    void Handle_getLayout__wrapper();

    void Handle_mapToGlobal__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    void createNoHandler__wrapper();

    void Event__push(EventRef value);
    EventRef Event__pop();

    void Event_accept__wrapper();

    void Event_ignore__wrapper();

    void MimeData__push(MimeDataRef value);
    MimeDataRef MimeData__pop();

    void MimeData_formats__wrapper();

    void MimeData_hasFormat__wrapper();

    void MimeData_text__wrapper();

    void MimeData_setText__wrapper();

    void MimeData_urls__wrapper();

    void MimeData_setUrls__wrapper();

    void createMimeData__wrapper();

    void Drag__push(DragRef value);
    DragRef Drag__pop();

    void Drag_setMimeData__wrapper();

    void Drag_exec__wrapper();

    void createDrag__wrapper();

    void DragMoveEvent__push(DragMoveEventRef value);
    DragMoveEventRef DragMoveEvent__pop();

    void DragMoveEvent_proposedAction__wrapper();

    void DragMoveEvent_acceptProposedAction__wrapper();

    void DragMoveEvent_possibleActions__wrapper();

    void DragMoveEvent_acceptDropAction__wrapper();

    void MethodMask__push(MethodMask value);
    MethodMask MethodMask__pop();

    void MethodDelegate__push(std::shared_ptr<MethodDelegate> inst, bool isReturn);
    std::shared_ptr<MethodDelegate> MethodDelegate__pop();

    void MethodDelegate_sizeHint__wrapper(int serverID);

    void MethodDelegate_paintEvent__wrapper(int serverID);

    void MethodDelegate_mousePressEvent__wrapper(int serverID);

    void MethodDelegate_mouseMoveEvent__wrapper(int serverID);

    void MethodDelegate_mouseReleaseEvent__wrapper(int serverID);

    void MethodDelegate_enterEvent__wrapper(int serverID);

    void MethodDelegate_leaveEvent__wrapper(int serverID);

    void MethodDelegate_resizeEvent__wrapper(int serverID);

    void MethodDelegate_dragMoveEvent__wrapper(int serverID);

    void MethodDelegate_dragLeaveEvent__wrapper(int serverID);

    void MethodDelegate_dropEvent__wrapper(int serverID);

    void createSubclassed__wrapper();

    int __register();
}
