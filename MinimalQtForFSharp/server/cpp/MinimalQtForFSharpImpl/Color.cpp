#include "generated/Color.h"

#include <QColor>
#include "ColorInternal.h"

// #define THIS ((QColor*)_this)

namespace Color
{
    QColor fromConstant(Constant name) {
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

    // deferred stuff =====================================
    class FromDeferred : public Deferred::Visitor {
    private:
        QColor &color;
    public:
        explicit FromDeferred(QColor &color) : color(color) {}
        void onFromHandle(const Deferred::FromHandle *value) override {
            color = value->color->qColor;
        }
        void onFromConstant(const Deferred::FromConstant *fromConstant) override {
            color = Color::fromConstant(fromConstant->name);
        }
        void onFromRGB(const Deferred::FromRGB *fromRGB) override {
            color = QColor::fromRgb(fromRGB->r, fromRGB->g, fromRGB->b);
        }
        void onFromRGBA(const Deferred::FromRGBA *fromRGBA) override {
            color = QColor::fromRgb(fromRGBA->r, fromRGBA->g, fromRGBA->b, fromRGBA->a);
        }
        void onFromRGBF(const Deferred::FromRGBF *fromRGBF) override {
            color = QColor::fromRgbF(fromRGBF->r, fromRGBF->g, fromRGBF->b);
        }
        void onFromRGBAF(const Deferred::FromRGBAF *fromRGBAF) override {
            color = QColor::fromRgbF(fromRGBAF->r, fromRGBAF->g, fromRGBAF->b, fromRGBAF->a);
        }
    };

    QColor fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QColor ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }

    // actual module methods =============================================

    void Owned_dispose(OwnedRef _this) {
        delete _this;
    }

    OwnedRef realize(std::shared_ptr<Deferred::Base> deferred) {
        QColor ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return new __Owned { ret };
    }
}
