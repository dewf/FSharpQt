#include "generated/Color.h"

#include <QColor>
#include "ColorInternal.h"

// #define THIS ((QColor*)_this)

namespace Color
{
    QColor fromConstant(const Constant name) {
        switch (name) {
            case Constant::Black:
                return QColorConstants::Black;
            case Constant::White:
                return QColorConstants::White;
            case Constant::DarkGray:
                return QColorConstants::DarkGray;
            case Constant::Gray:
                return QColorConstants::Gray;
            case Constant::LightGray:
                return QColorConstants::LightGray;
            case Constant::Red:
                return QColorConstants::Red;
            case Constant::Green:
                return QColorConstants::Green;
            case Constant::Blue:
                return QColorConstants::Blue;
            case Constant::Cyan:
                return QColorConstants::Cyan;
            case Constant::Magenta:
                return QColorConstants::Magenta;
            case Constant::Yellow:
                return QColorConstants::Yellow;
            case Constant::DarkRed:
                return QColorConstants::DarkRed;
            case Constant::DarkGreen:
                return QColorConstants::DarkGreen;
            case Constant::DarkBlue:
                return QColorConstants::DarkBlue;
            case Constant::DarkCyan:
                return QColorConstants::DarkCyan;
            case Constant::DarkMagenta:
                return QColorConstants::DarkMagenta;
            case Constant::DarkYellow:
                return QColorConstants::DarkYellow;
            case Constant::Transparent:
                return QColorConstants::Transparent;
            default:
                printf("Painter.cpp Color::fromConstant - unhandled value\n");
        }
        return QColorConstants::Black;
    }

    class FromDeferred final : public Deferred::Visitor {
    private:
        QColor &ret;
    public:
        explicit FromDeferred(QColor &ret)
            : ret(ret) {
        }
        void onFromHandle(const Deferred::FromHandle *value) override {
            ret = value->value->qColor;
        }
        void onFromConstant(const Deferred::FromConstant *value) override {
            ret = fromConstant(value->name);
        }
        void onFromRGB(const Deferred::FromRGB *value) override {
            ret = QColor(value->r, value->g, value->b);
        }
        void onFromRGBA(const Deferred::FromRGBA *value) override {
            ret = QColor(value->r, value->g, value->b, value->a);
        }
        void onFromFloatRGB(const Deferred::FromFloatRGB *value) override {
            ret = QColor::fromRgbF(value->r, value->g, value->b);
        }
        void onFromFloatRGBA(const Deferred::FromFloatRGBA *value) override {
            ret = QColor::fromRgbF(value->r, value->g, value->b, value->a);
        }
    };

    QColor fromDeferred(const std::shared_ptr<Deferred::Base> &deferred) {
        QColor ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }

    void Owned_dispose(OwnedRef _this) {
        delete _this;
    }

    OwnedRef create(std::shared_ptr<Deferred::Base> deferred) {
        return new __Owned { fromDeferred(deferred) };
    }
}
