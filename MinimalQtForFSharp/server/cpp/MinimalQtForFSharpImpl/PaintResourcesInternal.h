#pragma once

#include <QColor>
#include <QGradient>
#include <QRadialGradient>
#include <QLinearGradient>
#include <QBrush>
#include <QPen>
#include <QFont>
#include <QPainterPath>
#include <QPainterPathStroker>
#include <utility>

#include "generated/PaintResources.h"

#include "CommonInternal.h" // for PaintStackItem
using namespace Common;

namespace PaintResources
{
    // PaintStackItem moved to CommonInternal, since other modules (eg Color, Pixmap) will need to inherit from it

    // __Color now Color::__Handle

    struct __Gradient : public PaintStackItem {
        QGradient qGradPtr; // I guess this holds all types? so we don't have to worry about object slicing on the stack? seems weird
        explicit __Gradient(const QGradient& actual) : qGradPtr(actual) {}
    };

    struct __RadialGradient : public __Gradient {
        explicit __RadialGradient(const QRadialGradient& qGrad)
            : __Gradient(qGrad) {}
    };

    struct __LinearGradient : public __Gradient {
        explicit __LinearGradient(const QLinearGradient& qLinear)
            : __Gradient(qLinear) {}
    };

    struct __Brush : public PaintStackItem {
        QBrush qBrush;
        explicit __Brush(const QBrush& qBrush) : qBrush(qBrush) {}
    };

    struct __Pen : public PaintStackItem {
        QPen qPen;
        explicit __Pen(QPen qPen) : qPen(std::move(qPen)) {}
    };

    struct __Font : public PaintStackItem {
        QFont qFont;
        explicit __Font(const QFont& qFont) : qFont(qFont) {}
    };

    struct __PainterPath : public PaintStackItem {
        QPainterPath qPath;
        __PainterPath() = default;
        explicit __PainterPath(const QPainterPath& qPath) : qPath(qPath) {}
    };

    struct __PainterPathStroker : public PaintStackItem {
        QPainterPathStroker qStroker;
        HandleRef resources; // need to know this for .createStroke calls (to add them to the 'items')
        explicit __PainterPathStroker(HandleRef resources) : resources(resources) {}
    };
}
