#include "../support/NativeImplServer.h"
#include "Common_wrappers.h"
#include "Common.h"

namespace Common
{
    void Point__push(Point value, bool isReturn) {
        ni_pushInt32(value.y);
        ni_pushInt32(value.x);
    }

    Point Point__pop() {
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        return Point { x, y };
    }
    void PointF__push(PointF value, bool isReturn) {
        ni_pushDouble(value.y);
        ni_pushDouble(value.x);
    }

    PointF PointF__pop() {
        auto x = ni_popDouble();
        auto y = ni_popDouble();
        return PointF { x, y };
    }
    void Size__push(Size value, bool isReturn) {
        ni_pushInt32(value.height);
        ni_pushInt32(value.width);
    }

    Size Size__pop() {
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        return Size { width, height };
    }
    void Rect__push(Rect value, bool isReturn) {
        ni_pushInt32(value.height);
        ni_pushInt32(value.width);
        ni_pushInt32(value.y);
        ni_pushInt32(value.x);
    }

    Rect Rect__pop() {
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        return Rect { x, y, width, height };
    }
    void RectF__push(RectF value, bool isReturn) {
        ni_pushDouble(value.height);
        ni_pushDouble(value.width);
        ni_pushDouble(value.y);
        ni_pushDouble(value.x);
    }

    RectF RectF__pop() {
        auto x = ni_popDouble();
        auto y = ni_popDouble();
        auto width = ni_popDouble();
        auto height = ni_popDouble();
        return RectF { x, y, width, height };
    }

    int __register() {
        auto m = ni_registerModule("Common");
        return 0; // = OK
    }
}
