#pragma once
#include "ComboBox.h"

namespace ComboBox
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

    void SignalHandler_activated__wrapper(int serverID);

    void SignalHandler_currentIndexChanged__wrapper(int serverID);

    void SignalHandler_currentTextChanged__wrapper(int serverID);

    void SignalHandler_editTextChanged__wrapper(int serverID);

    void SignalHandler_highlighted__wrapper(int serverID);

    void SignalHandler_textActivated__wrapper(int serverID);

    void SignalHandler_textHighlighted__wrapper(int serverID);

    void InsertPolicy__push(InsertPolicy value);
    InsertPolicy InsertPolicy__pop();

    void SizeAdjustPolicy__push(SizeAdjustPolicy value);
    SizeAdjustPolicy SizeAdjustPolicy__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_count__wrapper();

    void Handle_currentData__wrapper();

    void Handle_currentData_overload1__wrapper();

    void Handle_currentIndex__wrapper();

    void Handle_setCurrentIndex__wrapper();

    void Handle_setCurrentText__wrapper();

    void Handle_setDuplicatesEnabled__wrapper();

    void Handle_setEditable__wrapper();

    void Handle_setFrame__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setInsertPolicy__wrapper();

    void Handle_setMaxCount__wrapper();

    void Handle_setMaxVisibleItems__wrapper();

    void Handle_setMinimumContentsLength__wrapper();

    void Handle_setModelColumn__wrapper();

    void Handle_setPlaceholderText__wrapper();

    void Handle_setSizeAdjustPolicy__wrapper();

    void Handle_clear__wrapper();

    void Handle_addItem__wrapper();

    void Handle_addItem_overload1__wrapper();

    void Handle_addItems__wrapper();

    void Handle_setModel__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    void downcastFrom__wrapper();

    int __register();
}
