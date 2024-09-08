#pragma once
#include "CalendarWidget.h"

namespace CalendarWidget
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

    void SignalHandler_currentPageChanged__wrapper(int serverID);

    void SignalHandler_selectionChanged__wrapper(int serverID);

    void HorizontalHeaderFormat__push(HorizontalHeaderFormat value);
    HorizontalHeaderFormat HorizontalHeaderFormat__pop();

    void VerticalHeaderFormat__push(VerticalHeaderFormat value);
    VerticalHeaderFormat VerticalHeaderFormat__pop();

    void SelectionMode__push(SelectionMode value);
    SelectionMode SelectionMode__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setDateEditAcceptDelay__wrapper();

    void Handle_setDateEditEnabled__wrapper();

    void Handle_setFirstDayOfWeek__wrapper();

    void Handle_setGridVisible__wrapper();

    void Handle_setHorizontalHeaderFormat__wrapper();

    void Handle_setMaximumDate__wrapper();

    void Handle_setMinimumDate__wrapper();

    void Handle_setNavigationBarVisible__wrapper();

    void Handle_selectedDate__wrapper();

    void Handle_setSelectedDate__wrapper();

    void Handle_setSelectionMode__wrapper();

    void Handle_setVerticalHeaderFormat__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
