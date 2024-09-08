#pragma once
#include "BoxLayout.h"

namespace BoxLayout
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void Direction__push(Direction value);
    Direction Direction__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setDirection__wrapper();

    void Handle_addSpacing__wrapper();

    void Handle_addStretch__wrapper();

    void Handle_addWidget__wrapper();

    void Handle_addWidget_overload1__wrapper();

    void Handle_addWidget_overload2__wrapper();

    void Handle_addLayout__wrapper();

    void Handle_addLayout_overload1__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    void createNoHandler__wrapper();

    int __register();
}
