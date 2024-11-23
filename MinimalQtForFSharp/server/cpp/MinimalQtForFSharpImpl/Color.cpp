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

    void Owned_dispose(OwnedRef _this) {
        delete _this;
    }

    OwnedRef create(Constant name) {
        return new __Owned { fromConstant(name) };
    }

    OwnedRef create(int32_t r, int32_t g, int32_t b) {
        return new __Owned { QColor::fromRgb(r, g, b) };
    }

    OwnedRef create(int32_t r, int32_t g, int32_t b, int32_t a) {
        return new __Owned { QColor::fromRgb(r, g, b, a) };
    }

    OwnedRef create(float r, float g, float b) {
        return new __Owned { QColor::fromRgbF(r, g, b) };
    }

    OwnedRef create(float r, float g, float b, float a) {
        return new __Owned { QColor::fromRgbF(r, g, b, a) };
    }
}
