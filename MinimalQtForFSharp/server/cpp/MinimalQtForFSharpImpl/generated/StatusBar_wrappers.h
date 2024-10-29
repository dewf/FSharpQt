#pragma once
#include "StatusBar.h"

namespace StatusBar
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

    void SignalHandler_messageChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_isSizeGripEnabled__wrapper();

    void Handle_setSizeGripEnabled__wrapper();

    void Handle_clearMessage__wrapper();

    void Handle_showMessage__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}