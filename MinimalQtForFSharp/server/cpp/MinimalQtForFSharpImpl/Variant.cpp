#include "generated/Variant.h"

#include "VariantInternal.h"
#include "PaintResourcesInternal.h"

#include <QVariant>
#define THIS ((QVariant*)_this)
#include "util/convert.h"

namespace Variant
{
    bool Handle_isValid(HandleRef _this) {
        return THIS->isValid();
    }

    bool Handle_toBool(HandleRef _this) {
        return THIS->toBool();
    }

    std::string Handle_toString2(HandleRef _this) {
        return THIS->toString().toStdString();
    }

    int32_t Handle_toInt(HandleRef _this) {
        return THIS->toInt();
    }

    Size Handle_toSize(HandleRef _this) {
        return toSize(THIS->toSize());
    }

    CheckState Handle_toCheckState(HandleRef _this) {
        switch ((Qt::CheckState)THIS->toInt()) {
            case Qt::Unchecked:
                return CheckState::Unchecked;
            case Qt::PartiallyChecked:
                return CheckState::PartiallyChecked;
            case Qt::Checked:
                return CheckState::Checked;
            default:
                printf("Variant::Handle_toCheckState - unknown input value %d\n", THIS->toInt());
        }
        return CheckState::Unchecked;
    }

    std::shared_ptr<ServerValue::Base> Handle_toServerValue(HandleRef _this) {
        ServerValue::Base *value;
        switch((QMetaType::Type)THIS->typeId()) {
            case QMetaType::Bool:
                value = new ServerValue::Bool(THIS->toBool());
                break;
            case QMetaType::Int:
                value = new ServerValue::Int(THIS->toInt());
                break;
            case QMetaType::QString:
                value = new ServerValue::String(THIS->toString().toStdString());
                break;
            default:
                value = new ServerValue::Unknown();
                break;
        }
        return std::shared_ptr<ServerValue::Base>(value);
    }

    void Handle_dispose(HandleRef _this) {
        printf("Variant::Handle_dispose() - should never be called (in fact should be @nodispose, long story)\n");
    }

    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }

    // deferred stuff ========================================
    class FromDeferred : public Deferred::Visitor {
    private:
        QVariant &variant;
    public:
        explicit FromDeferred(QVariant &variant) : variant(variant) {}

        void onEmpty(const Deferred::Empty *value) override {
            variant = QVariant();
        }

        void onFromBool(const Deferred::FromBool *value) override {
            variant = value->value;
        }

        void onFromString(const Deferred::FromString *value) override {
            variant = QString::fromStdString(value->value);
        }

        void onFromInt(const Deferred::FromInt *value) override {
            variant = value->value;
        }

        void onFromSize(const Deferred::FromSize *value) override {
            variant = toQSize(value->size);
        }

        void onFromCheckState(const Deferred::FromCheckState *value) override {
            variant = (int)value->value;
        }

        void onFromIcon(const Deferred::FromIcon *value) override {
            variant = Icon::fromDeferred(value->value);
        }

        void onFromColor(const Deferred::FromColor *value) override {
            variant = Color::fromDeferred(value->value);
        }

        void onFromAligment(const Deferred::FromAligment *value) override {
            variant = value->value;
        }
    };

    QVariant fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QVariant ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }
}
