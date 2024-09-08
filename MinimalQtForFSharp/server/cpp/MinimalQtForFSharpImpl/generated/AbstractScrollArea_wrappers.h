#pragma once
#include "AbstractScrollArea.h"

namespace AbstractScrollArea
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

    void SizeAdjustPolicy__push(SizeAdjustPolicy value);
    SizeAdjustPolicy SizeAdjustPolicy__pop();

    void ScrollBarPolicy__push(ScrollBarPolicy value);
    ScrollBarPolicy ScrollBarPolicy__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setHorizontalScrollBarPolicy__wrapper();

    void Handle_setSizeAdjustPolicy__wrapper();

    void Handle_setVerticalScrollBarPolicy__wrapper();

    void Handle_dispose__wrapper();

    int __register();
}
