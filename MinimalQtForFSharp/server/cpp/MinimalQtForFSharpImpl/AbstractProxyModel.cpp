#include "generated/AbstractProxyModel.h"

#include <QAbstractProxyModel>
#include <QModelIndex>

#include "ModelIndexInternal.h"

#define THIS ((QAbstractProxyModel*)_this)

namespace AbstractProxyModel
{
    AbstractItemModel::HandleRef Handle_sourceModel(HandleRef _this) {
        return (AbstractItemModel::HandleRef)THIS->sourceModel();
    }

    void Handle_setSourceModel(HandleRef _this, AbstractItemModel::HandleRef sourceModel) {
        THIS->setSourceModel((QAbstractItemModel*)sourceModel);
    }

    ModelIndex::OwnedRef Handle_mapToSource(HandleRef _this, std::shared_ptr<ModelIndex::Deferred::Base> proxyIndex) {
        auto retValue = THIS->mapToSource(ModelIndex::fromDeferred(proxyIndex));
        // most model indexes are pointers to stack-allocated stuff on the C++ side, but this one we are responsible for!
        return (ModelIndex::OwnedRef) new QModelIndex(retValue);
    }
}
