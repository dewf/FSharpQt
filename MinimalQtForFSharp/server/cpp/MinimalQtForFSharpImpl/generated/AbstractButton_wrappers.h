#pragma once
#include "AbstractButton.h"

namespace AbstractButton
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

    void SignalHandler_clicked__wrapper(int serverID);

    void SignalHandler_pressed__wrapper(int serverID);

    void SignalHandler_released__wrapper(int serverID);

    void SignalHandler_toggled__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAutoExclusive__wrapper();

    void Handle_setAutoRepeat__wrapper();

    void Handle_setAutoRepeatDelay__wrapper();

    void Handle_setAutoRepeatInterval__wrapper();

    void Handle_setCheckable__wrapper();

    void Handle_setChecked__wrapper();

    void Handle_setDown__wrapper();

    void Handle_setIcon__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setShortcut__wrapper();

    void Handle_setText__wrapper();

    int __register();
}
