#pragma once
#include "Object.h"

namespace Object
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setObjectName__wrapper();

    void Handle_dumpObjectTree__wrapper();

    void Handle_dispose__wrapper();

    int __register();
}
