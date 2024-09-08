#pragma once

#include <Qt>
#include <QRect>
#include <QKeySequence>
#include <QStringList>

#include "../generated/Common.h"
using namespace Common;
#include "../generated/Enums.h"
using namespace Enums;

#define STRUCT_CAST(b,a) *((b*)&a)

inline QStringList toQStringList(const std::vector<std::string>& strings) {
    QStringList ret;
    for (auto & str : strings) {
        ret.push_back(QString::fromStdString(str));
    }
    return ret;
}

inline QSize toQSize(const Size& sz) {
    return {sz.width, sz.height };
}

inline Size toSize(const QSize& sz) {
    return { sz.width(), sz.height() };
}

inline Point toPoint(const QPoint& qPoint) {
    return { qPoint.x(), qPoint.y() };
}

inline PointF toPointF(const QPointF& qPointF) {
    return { qPointF.x(), qPointF.y() };
}

inline QPoint toQPoint(const Point& p) {
    return QPoint(p.x, p.y);
}

inline QPointF toQPointF(const PointF& p) {
    return QPointF(p.x, p.y);
}

inline Rect toRect(const QRect& qRect) {
    return { qRect.left(), qRect.top(), qRect.width(), qRect.height() };
}

inline QRect toQRect(const Rect& r) {
    return {r.x, r.y, r.width, r.height};
}

inline QRectF toQRectF(const RectF& r) {
    return { r.x, r.y, r.width, r.height };
}
