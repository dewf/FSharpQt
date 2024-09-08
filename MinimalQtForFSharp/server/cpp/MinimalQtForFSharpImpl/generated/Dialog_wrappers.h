#pragma once
#include "Dialog.h"

namespace Dialog
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

    void SignalHandler_accepted__wrapper(int serverID);

    void SignalHandler_finished__wrapper(int serverID);

    void SignalHandler_rejected__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setModal__wrapper();

    void Handle_setSizeGripEnabled__wrapper();

    void Handle_setParentDialogFlags__wrapper();

    void Handle_accept__wrapper();

    void Handle_reject__wrapper();

    void Handle_exec__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
