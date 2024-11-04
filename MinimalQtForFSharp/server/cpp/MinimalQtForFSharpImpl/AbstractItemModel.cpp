#include "generated/AbstractItemModel.h"

#include <QAbstractItemModel>
#include <QModelIndex>

#include "ModelIndexInternal.h"
#include "VariantInternal.h"

#define THIS ((QAbstractItemModel*)_this)

namespace AbstractItemModel
{
    ModelIndex::OwnedRef Handle_index(HandleRef _this, int32_t row, int32_t column) {
        auto value = THIS->index(row, column);
        return (ModelIndex::OwnedRef) new QModelIndex(value);
    }

    ModelIndex::OwnedRef Handle_index(HandleRef _this, int32_t row, int32_t column, std::shared_ptr<ModelIndex::Deferred::Base> parent) {
        auto value = THIS->index(row, column, ModelIndex::fromDeferred(parent));
        return (ModelIndex::OwnedRef) new QModelIndex(value);
    }

    bool Handle_setData(HandleRef _this, std::shared_ptr<ModelIndex::Deferred::Base> index, std::shared_ptr<Variant::Deferred::Base> value) {
        auto index2 = ModelIndex::fromDeferred(index);
        auto value2 = Variant::fromDeferred(value);
        return THIS->setData(index2, value2);
    }

    bool Handle_setData(HandleRef _this, std::shared_ptr<ModelIndex::Deferred::Base> index, std::shared_ptr<Variant::Deferred::Base> value, ItemDataRole role) {
        auto index2 = ModelIndex::fromDeferred(index);
        auto value2 = Variant::fromDeferred(value);
        return THIS->setData(index2, value2, (int)role);
    }

    Variant::OwnedHandleRef Handle_data(HandleRef _this, std::shared_ptr<ModelIndex::Deferred::Base> index) {
        auto value = THIS->data(ModelIndex::fromDeferred(index));
        return (Variant::OwnedHandleRef) new QVariant(value);
    }

    Variant::OwnedHandleRef Handle_data(HandleRef _this, std::shared_ptr<ModelIndex::Deferred::Base> index, ItemDataRole role) {
        auto value = THIS->data(ModelIndex::fromDeferred(index), (Qt::ItemDataRole)role);
        return (Variant::OwnedHandleRef) new QVariant(value);
    }

    void Handle_sort(HandleRef _this, int32_t column) {
        THIS->sort(column);
    }

    void Handle_sort(HandleRef _this, int32_t column, SortOrder order) {
        THIS->sort(column, (Qt::SortOrder)order);
    }
}
