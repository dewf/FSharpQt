#pragma once
#include "MainWindow.h"

namespace MainWindow
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

    void SignalHandler_iconSizeChanged__wrapper(int serverID);

    void SignalHandler_tabifiedDockWidgetActivated__wrapper(int serverID);

    void SignalHandler_toolButtonStyleChanged__wrapper(int serverID);

    void SignalHandler_windowClosed__wrapper(int serverID);

    void DockOptions__push(DockOptions value);
    DockOptions DockOptions__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAnimated__wrapper();

    void Handle_setDockNestingEnabled__wrapper();

    void Handle_setDockOptions__wrapper();

    void Handle_setDocumentMode__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setTabShape__wrapper();

    void Handle_setToolButtonStyle__wrapper();

    void Handle_setUnifiedTitleAndToolBarOnMac__wrapper();

    void Handle_setCentralWidget__wrapper();

    void Handle_setMenuBar__wrapper();

    void Handle_setStatusBar__wrapper();

    void Handle_addToolBar__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
