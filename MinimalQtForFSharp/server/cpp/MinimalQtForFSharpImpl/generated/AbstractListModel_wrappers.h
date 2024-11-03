#pragma once
#include "AbstractListModel.h"

namespace AbstractListModel
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

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

    void Handle_setSignalMask__wrapper();

    void Interior__push(InteriorRef value);
    InteriorRef Interior__pop();

    void Interior_emitDataChanged__wrapper();

    void Interior_emitHeaderDataChanged__wrapper();

    void Interior_beginInsertRows__wrapper();

    void Interior_endInsertRows__wrapper();

    void Interior_beginRemoveRows__wrapper();

    void Interior_endRemoveRows__wrapper();

    void Interior_beginResetModel__wrapper();

    void Interior_endResetModel__wrapper();

    void Interior_dispose__wrapper();

    void ItemFlags__push(ItemFlags value);
    ItemFlags ItemFlags__pop();

    void MethodMask__push(MethodMask value);
    MethodMask MethodMask__pop();

    void MethodDelegate__push(std::shared_ptr<MethodDelegate> inst, bool isReturn);
    std::shared_ptr<MethodDelegate> MethodDelegate__pop();

    void MethodDelegate_rowCount__wrapper(int serverID);

    void MethodDelegate_data__wrapper(int serverID);

    void MethodDelegate_headerData__wrapper(int serverID);

    void MethodDelegate_getFlags__wrapper(int serverID);

    void MethodDelegate_setData__wrapper(int serverID);

    void MethodDelegate_columnCount__wrapper(int serverID);

    void createSubclassed__wrapper();

    int __register();
}
