#include "generated/ModelIndex.h"

#include <QModelIndex>
#include "ModelIndexInternal.h"

// note that ModelIndex is usually stack allocated, but we deal with either pointers to Qt-owned stack indexes or heap-allocated ones of our own ("OwnedHandle")
#define THIS ((QModelIndex*)_this)

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

    Variant::OwnedHandleRef Handle_data(HandleRef _this) {
        auto value = THIS->data();
        return (Variant::OwnedHandleRef) new QVariant(value);
    }

    Variant::OwnedHandleRef Handle_data(HandleRef _this, ItemDataRole role) {
        auto value = THIS->data((Qt::ItemDataRole)role);
        return (Variant::OwnedHandleRef) new QVariant(value);
    }

    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }

    class FromDeferred : public ModelIndex::Deferred::Visitor {
    private:
        QModelIndex &modelIndex;
    public:
        explicit FromDeferred(QModelIndex &modelIndex)
                : modelIndex(modelIndex) {}

        void onEmpty(const Deferred::Empty *empty) override {
            modelIndex = QModelIndex();
        }

        void onFromHandle(const Deferred::FromHandle *fromHandle) override {
            modelIndex = *((QModelIndex*)fromHandle->handle);
        }

        void onFromOwned(const Deferred::FromOwned *fromOwned) override {
            modelIndex = *((QModelIndex*)fromOwned->owned);
        }
    };

    QModelIndex fromDeferred(const std::shared_ptr<ModelIndex::Deferred::Base>& deferred) {
        QModelIndex ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }
}
