#pragma once
#include "GridLayout.h"

namespace GridLayout
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setHorizontalSpacing__wrapper();

    void Handle_setVerticalSpacing__wrapper();

    void Handle_addWidget__wrapper();

    void Handle_addWidget_overload1__wrapper();

    void Handle_addWidget_overload2__wrapper();

    void Handle_addWidget_overload3__wrapper();

    void Handle_addLayout__wrapper();

    void Handle_addLayout_overload1__wrapper();

    void Handle_addLayout_overload2__wrapper();

    void Handle_addLayout_overload3__wrapper();

    void Handle_setRowMinimumHeight__wrapper();

    void Handle_setRowStretch__wrapper();

    void Handle_setColumnMinimumWidth__wrapper();

    void Handle_setColumnStretch__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
