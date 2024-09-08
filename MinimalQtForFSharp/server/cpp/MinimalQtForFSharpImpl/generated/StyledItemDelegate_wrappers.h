#pragma once
#include "StyledItemDelegate.h"

namespace StyledItemDelegate
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_closeEditor__wrapper(int serverID);

    void SignalHandler_commitData__wrapper(int serverID);

    void SignalHandler_sizeHintChanged__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void MethodMask__push(MethodMask value);
    MethodMask MethodMask__pop();

    void MethodDelegate__push(std::shared_ptr<MethodDelegate> inst, bool isReturn);
    std::shared_ptr<MethodDelegate> MethodDelegate__pop();

    void MethodDelegate_createEditor__wrapper(int serverID);

    void MethodDelegate_setEditorData__wrapper(int serverID);

    void MethodDelegate_setModelData__wrapper(int serverID);

    void createdSubclassed__wrapper();

    int __register();
}
