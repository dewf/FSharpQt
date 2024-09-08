#pragma once
#include "TabWidget.h"

namespace TabWidget
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

    void SignalHandler_currentChanged__wrapper(int serverID);

    void SignalHandler_tabBarClicked__wrapper(int serverID);

    void SignalHandler_tabBarDoubleClicked__wrapper(int serverID);

    void SignalHandler_tabCloseRequested__wrapper(int serverID);

    void TabShape__push(TabShape value);
    TabShape TabShape__pop();

    void TabPosition__push(TabPosition value);
    TabPosition TabPosition__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_count__wrapper();

    void Handle_setCurrentIndex__wrapper();

    void Handle_setDocumentMode__wrapper();

    void Handle_setElideMode__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setMovable__wrapper();

    void Handle_setTabBarAutoHide__wrapper();

    void Handle_setTabPosition__wrapper();

    void Handle_setTabShape__wrapper();

    void Handle_setTabsClosable__wrapper();

    void Handle_setUsesScrollButtons__wrapper();

    void Handle_addTab__wrapper();

    void Handle_insertTab__wrapper();

    void Handle_widgetAt__wrapper();

    void Handle_clear__wrapper();

    void Handle_removeTab__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
