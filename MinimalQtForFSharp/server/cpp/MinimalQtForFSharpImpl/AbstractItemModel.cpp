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
        return MODELINDEX_HEAP_COPY(value);
    }

    ModelIndex::OwnedRef Handle_index(HandleRef _this, int32_t row, int32_t column, ModelIndex::HandleRef parent) {
        auto value = THIS->index(row, column, MODELINDEX_VALUE(parent));
        return MODELINDEX_HEAP_COPY(value);
    }

    bool Handle_setData(HandleRef _this, ModelIndex::HandleRef index, std::shared_ptr<Variant::Deferred::Base> value) {
        auto value2 = Variant::fromDeferred(value);
        return THIS->setData(MODELINDEX_VALUE(index), value2);
    }

    bool Handle_setData(HandleRef _this, ModelIndex::HandleRef index, std::shared_ptr<Variant::Deferred::Base> value, ItemDataRole role) {
        auto value2 = Variant::fromDeferred(value);
        auto qrole = static_cast<Qt::ItemDataRole>(role);
        return THIS->setData(MODELINDEX_VALUE(index), value2, qrole);
    }

    Variant::OwnedRef Handle_data(HandleRef _this, ModelIndex::HandleRef index) {
        auto value = THIS->data(MODELINDEX_VALUE(index));
        return VARIANT_HEAP_COPY(value);
    }

    Variant::OwnedRef Handle_data(HandleRef _this, ModelIndex::HandleRef index, ItemDataRole role) {
        auto qrole = static_cast<Qt::ItemDataRole>(role);
        auto value = THIS->data(MODELINDEX_VALUE(index), qrole);
        return VARIANT_HEAP_COPY(value);
    }

    void Handle_sort(HandleRef _this, int32_t column) {
        THIS->sort(column);
    }

    void Handle_sort(HandleRef _this, int32_t column, SortOrder order) {
        auto qorder = static_cast<Qt::SortOrder>(order);
        THIS->sort(column, qorder);
    }
}
