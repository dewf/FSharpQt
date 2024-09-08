#pragma once
#include "TreeView.h"

namespace TreeView
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

    void SignalHandler_collapsed__wrapper(int serverID);

    void SignalHandler_expanded__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAllColumnsShowFocus__wrapper();

    void Handle_setAnimated__wrapper();

    void Handle_setAutoExpandDelay__wrapper();

    void Handle_setExpandsOnDoubleClick__wrapper();

    void Handle_setHeaderHidden__wrapper();

    void Handle_setIndentation__wrapper();

    void Handle_setItemsExpandable__wrapper();

    void Handle_setRootIsDecorated__wrapper();

    void Handle_setSortingEnabled__wrapper();

    void Handle_setUniformRowHeights__wrapper();

    void Handle_setWordWrap__wrapper();

    void Handle_resizeColumnToContents__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
