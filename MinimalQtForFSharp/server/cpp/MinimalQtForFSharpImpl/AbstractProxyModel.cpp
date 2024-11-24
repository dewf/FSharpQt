#include "generated/AbstractProxyModel.h"

#include <QAbstractProxyModel>
#include <QModelIndex>

#include "ModelIndexInternal.h"

#define THIS ((QAbstractProxyModel*)_this)

namespace AbstractProxyModel
{
    AbstractItemModel::HandleRef Handle_sourceModel(HandleRef _this) {
        return reinterpret_cast<AbstractItemModel::HandleRef>(THIS->sourceModel());
    }

    void Handle_setSourceModel(HandleRef _this, AbstractItemModel::HandleRef sourceModel) {
        THIS->setSourceModel(reinterpret_cast<QAbstractItemModel*>(sourceModel));
    }

    ModelIndex::OwnedRef Handle_mapToSource(HandleRef _this, ModelIndex::HandleRef proxyIndex) {
        auto retValue = THIS->mapToSource(MODELINDEX_VALUE(proxyIndex));
        return MODELINDEX_HEAP_COPY(retValue);
    }
}
