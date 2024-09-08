#pragma once
#include "TabBar.h"

namespace TabBar
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

    void SignalHandler_tabMoved__wrapper(int serverID);

    void ButtonPosition__push(ButtonPosition value);
    ButtonPosition ButtonPosition__pop();

    void SelectionBehavior__push(SelectionBehavior value);
    SelectionBehavior SelectionBehavior__pop();

    void Shape__push(Shape value);
    Shape Shape__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAutoHide__wrapper();

    void Handle_setChangeCurrentOnDrag__wrapper();

    void Handle_count__wrapper();

    void Handle_setCurrentIndex__wrapper();

    void Handle_currentIndex__wrapper();

    void Handle_setDocumentMode__wrapper();

    void Handle_setDrawBase__wrapper();

    void Handle_setElideMode__wrapper();

    void Handle_setExpanding__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setMovable__wrapper();

    void Handle_setSelectionBehaviorOnRemove__wrapper();

    void Handle_setShape__wrapper();

    void Handle_setTabsClosable__wrapper();

    void Handle_setUsesScrollButtons__wrapper();

    void Handle_removeAllTabs__wrapper();

    void Handle_addTab__wrapper();

    void Handle_addTab_overload1__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
