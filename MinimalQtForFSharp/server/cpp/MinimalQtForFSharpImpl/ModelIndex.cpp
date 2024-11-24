#include "generated/ModelIndex.h"

#include <QModelIndex>
#include "ModelIndexInternal.h"
#include "VariantInternal.h"

#define THIS ((QModelIndex*)_this) // save a bit of typing

namespace ModelIndex
{
    bool Handle_isValid(HandleRef _this) {
        return THIS->isValid();
   }

    int32_t Handle_row(HandleRef _this) {
        return THIS->row();
    }

    int32_t Handle_column(HandleRef _this) {
        return THIS->column();
    }

    Variant::OwnedRef Handle_data(HandleRef _this) {
        auto value = THIS->data();
        return VARIANT_HEAP_COPY(value);
    }

    Variant::OwnedRef Handle_data(HandleRef _this, ItemDataRole role) {
        auto qrole = static_cast<Qt::ItemDataRole>(role);
        auto value = THIS->data(qrole);
        return VARIANT_HEAP_COPY(value);
    }

    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }

    OwnedRef create() {
        return reinterpret_cast<OwnedRef>(new QModelIndex());
    }
}
