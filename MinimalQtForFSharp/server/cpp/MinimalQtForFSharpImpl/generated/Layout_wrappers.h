#pragma once
#include "Layout.h"

namespace Layout
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SizeConstraint__push(SizeConstraint value);
    SizeConstraint SizeConstraint__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setEnabled__wrapper();

    void Handle_setSpacing__wrapper();

    void Handle_setContentsMargins__wrapper();

    void Handle_setSizeConstraint__wrapper();

    void Handle_removeAll__wrapper();

    void Handle_activate__wrapper();

    void Handle_update__wrapper();

    void Handle_dispose__wrapper();

    int __register();
}
