#pragma once
#include "AbstractItemView.h"

namespace AbstractItemView
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

    void SignalHandler_clicked__wrapper(int serverID);

    void SignalHandler_doubleClicked__wrapper(int serverID);

    void SignalHandler_entered__wrapper(int serverID);

    void SignalHandler_iconSizeChanged__wrapper(int serverID);

    void SignalHandler_pressed__wrapper(int serverID);

    void SignalHandler_viewportEntered__wrapper(int serverID);

    void DragDropMode__push(DragDropMode value);
    DragDropMode DragDropMode__pop();

    void EditTriggers__push(EditTriggers value);
    EditTriggers EditTriggers__pop();

    void ScrollMode__push(ScrollMode value);
    ScrollMode ScrollMode__pop();

    void SelectionBehavior__push(SelectionBehavior value);
    SelectionBehavior SelectionBehavior__pop();

    void SelectionMode__push(SelectionMode value);
    SelectionMode SelectionMode__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAlternatingRowColors__wrapper();

    void Handle_setAutoScroll__wrapper();

    void Handle_setAutoScrollMargin__wrapper();

    void Handle_setDefaultDropAction__wrapper();

    void Handle_setDragDropMode__wrapper();

    void Handle_setDragDropOverwriteMode__wrapper();

    void Handle_setDragEnabled__wrapper();

    void Handle_setEditTriggers__wrapper();

    void Handle_setHorizontalScrollMode__wrapper();

    void Handle_setIconSize__wrapper();

    void Handle_setSelectionBehavior__wrapper();

    void Handle_setSelectionMode__wrapper();

    void Handle_setDropIndicatorShown__wrapper();

    void Handle_setTabKeyNavigation__wrapper();

    void Handle_setTextElideMode__wrapper();

    void Handle_setVerticalScrollMode__wrapper();

    void Handle_setModel__wrapper();

    void Handle_setItemDelegate__wrapper();

    void Handle_setItemDelegateForColumn__wrapper();

    void Handle_setItemDelegateForRow__wrapper();

    int __register();
}
