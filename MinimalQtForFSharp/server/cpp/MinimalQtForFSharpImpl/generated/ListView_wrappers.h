#pragma once
#include "ListView.h"

namespace ListView
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

    void SignalHandler_activated__wrapper(int serverID);

    void SignalHandler_clicked__wrapper(int serverID);

    void SignalHandler_doubleClicked__wrapper(int serverID);

    void SignalHandler_entered__wrapper(int serverID);

    void SignalHandler_iconSizeChanged__wrapper(int serverID);

    void SignalHandler_pressed__wrapper(int serverID);

    void SignalHandler_viewportEntered__wrapper(int serverID);

    void SignalHandler_indexesMoved__wrapper(int serverID);

    void Movement__push(Movement value);
    Movement Movement__pop();

    void Flow__push(Flow value);
    Flow Flow__pop();

    void ResizeMode__push(ResizeMode value);
    ResizeMode ResizeMode__pop();

    void LayoutMode__push(LayoutMode value);
    LayoutMode LayoutMode__pop();

    void ViewMode__push(ViewMode value);
    ViewMode ViewMode__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setBatchSize__wrapper();

    void Handle_setFlow__wrapper();

    void Handle_setGridSize__wrapper();

    void Handle_setWrapping__wrapper();

    void Handle_setItemAlignment__wrapper();

    void Handle_setLayoutMode__wrapper();

    void Handle_setModelColumn__wrapper();

    void Handle_setMovement__wrapper();

    void Handle_setResizeMode__wrapper();

    void Handle_setSelectionRectVisible__wrapper();

    void Handle_setSpacing__wrapper();

    void Handle_setUniformItemSizes__wrapper();

    void Handle_setViewMode__wrapper();

    void Handle_setWordWrap__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
