#pragma once
#include "AbstractItemDelegate.h"

namespace AbstractItemDelegate
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void EndEditHint__push(EndEditHint value);
    EndEditHint EndEditHint__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_closeEditor__wrapper(int serverID);

    void SignalHandler_commitData__wrapper(int serverID);

    void SignalHandler_sizeHintChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    int __register();
}
