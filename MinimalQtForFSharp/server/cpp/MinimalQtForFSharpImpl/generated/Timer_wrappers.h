#pragma once
#include "Timer.h"

namespace Timer
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_timeout__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_isActive__wrapper();

    void Handle_setInterval__wrapper();

    void Handle_remainingTime__wrapper();

    void Handle_setSingleShot__wrapper();

    void Handle_setTimerType__wrapper();

    void Handle_start__wrapper();

    void Handle_start_overload1__wrapper();

    void Handle_stop__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}