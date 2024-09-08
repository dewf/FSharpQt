#pragma once
#include "ToolBar.h"

namespace ToolBar
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

    void SignalHandler_actionTriggered__wrapper(int serverID);

    void SignalHandler_allowedAreasChanged__wrapper(int serverID);

    void SignalHandler_iconSizeChanged__wrapper(int serverID);

    void SignalHandler_movableChanged__wrapper(int serverID);

    void SignalHandler_orientationChanged__wrapper(int serverID);

    void SignalHandler_toolButtonStyleChanged__wrapper(int serverID);

    void SignalHandler_topLevelChanged__wrapper(int serverID);

    void SignalHandler_visibilityChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAllowedAreas__wrapper();

    void Handle_setFloatable__wrapper();

    void Handle_isFloating__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setMovable__wrapper();

    void Handle_setOrientation__wrapper();

    void Handle_setToolButtonStyle__wrapper();

    void Handle_addSeparator__wrapper();

    void Handle_addWidget__wrapper();

    void Handle_clear__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
