#pragma once
#include "Menu.h"

namespace Menu
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

    void SignalHandler_aboutToHide__wrapper(int serverID);

    void SignalHandler_aboutToShow__wrapper(int serverID);

    void SignalHandler_hovered__wrapper(int serverID);

    void SignalHandler_triggered__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setIcon__wrapper();

    void Handle_setSeparatorsCollapsible__wrapper();

    void Handle_setTearOffEnabled__wrapper();

    void Handle_setTitle__wrapper();

    void Handle_setToolTipsVisible__wrapper();

    void Handle_clear__wrapper();

    void Handle_addSeparator__wrapper();

    void Handle_popup__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
