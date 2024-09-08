#pragma once
#include "Common.h"

namespace Common
{

    void Point__push(Point value, bool isReturn);
    Point Point__pop();

    void PointF__push(PointF value, bool isReturn);
    PointF PointF__pop();

    void Size__push(Size value, bool isReturn);
    Size Size__pop();

    void Rect__push(Rect value, bool isReturn);
    Rect Rect__pop();

    void RectF__push(RectF value, bool isReturn);
    RectF RectF__pop();

    int __register();
}
