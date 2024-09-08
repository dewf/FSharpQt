#pragma once
#include "RadioButton.h"

namespace RadioButton
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

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
