#pragma once
#include "AbstractItemModel.h"

namespace AbstractItemModel
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void LayoutChangeHint__push(LayoutChangeHint value);
    LayoutChangeHint LayoutChangeHint__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_columnsAboutToBeInserted__wrapper(int serverID);

    void SignalHandler_columnsAboutToBeMoved__wrapper(int serverID);

    void SignalHandler_columnsAboutToBeRemoved__wrapper(int serverID);

    void SignalHandler_columnsInserted__wrapper(int serverID);

    void SignalHandler_columnsMoved__wrapper(int serverID);

    void SignalHandler_columnsRemoved__wrapper(int serverID);

    void SignalHandler_dataChanged__wrapper(int serverID);

    void SignalHandler_headerDataChanged__wrapper(int serverID);

    void SignalHandler_layoutAboutToBeChanged__wrapper(int serverID);

    void SignalHandler_layoutChanged__wrapper(int serverID);

    void SignalHandler_modelAboutToBeReset__wrapper(int serverID);

    void SignalHandler_modelReset__wrapper(int serverID);

    void SignalHandler_rowsAboutToBeInserted__wrapper(int serverID);

    void SignalHandler_rowsAboutToBeMoved__wrapper(int serverID);

    void SignalHandler_rowsAboutToBeRemoved__wrapper(int serverID);

    void SignalHandler_rowsInserted__wrapper(int serverID);

    void SignalHandler_rowsMoved__wrapper(int serverID);

    void SignalHandler_rowsRemoved__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_index__wrapper();

    void Handle_index_overload1__wrapper();

    void Handle_setData__wrapper();

    void Handle_setData_overload1__wrapper();

    void Handle_data__wrapper();

    void Handle_data_overload1__wrapper();

    void Handle_sort__wrapper();

    void Handle_sort_overload1__wrapper();

    int __register();
}
