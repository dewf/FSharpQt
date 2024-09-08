#include "generated/Painter.h"

#include <QPainter>

#include "util/convert.h"
#include "PaintResourcesInternal.h" // for struct definitions

#define THIS ((QPainter*)_this)

namespace Painter
{
    void Handle_setRenderHint(HandleRef _this, RenderHint hint, bool on) {
        THIS->setRenderHint((QPainter::RenderHint)hint, on);
    }

    void Handle_setRenderHints(HandleRef _this, RenderHintSet hints, bool on) {
        auto qtHints = QPainter::RenderHints(hints);
        THIS->setRenderHints(qtHints, on);
    }

    void Handle_setPen(HandleRef _this, PenRef pen) {
        THIS->setPen(pen->qPen);
    }

    void Handle_setBrush(HandleRef _this, BrushRef brush) {
        THIS->setBrush(brush->qBrush);
    }

    void Handle_setFont(HandleRef _this, FontRef font) {
        THIS->setFont(font->qFont);
    }

    void Handle_drawText(HandleRef _this, Common::Rect rect, Enums::Alignment align, std::string text) {
        THIS->drawText(toQRect(rect), (Qt::AlignmentFlag)align, QString::fromStdString(text));
    }

    void Handle_fillPath(HandleRef _this, PainterPathRef path, BrushRef brush) {
        THIS->fillPath(path->qPath, brush->qBrush);
    }

    void Handle_strokePath(HandleRef _this, PainterPathRef path, PenRef pen) {
        THIS->strokePath(path->qPath, pen->qPen);
    }

    void Handle_fillRect(HandleRef _this, Rect rect, BrushRef brush) {
        THIS->fillRect(toQRect(rect), brush->qBrush);
    }

    void Handle_fillRect(HandleRef _this, Rect rect, ColorRef color) {
        THIS->fillRect(toQRect(rect), color->qColor);
    }

    void Handle_drawRect(HandleRef _this, Rect rect) {
        THIS->drawRect(toQRect(rect));
    }

    void Handle_drawRect(HandleRef _this, RectF rect) {
        THIS->drawRect(toQRectF(rect));
    }

    void Handle_drawRect(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height) {
        THIS->drawRect(x, y, width, height);
    }

    void Handle_drawEllipse(HandleRef _this, RectF rectangle) {
        THIS->drawEllipse(toQRectF(rectangle));
    }

    void Handle_drawEllipse(HandleRef _this, Rect rectangle) {
        THIS->drawEllipse(toQRect(rectangle));
    }

    void Handle_drawEllipse(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height) {
        THIS->drawEllipse(x, y, width, height);
    }

    void Handle_drawEllipse(HandleRef _this, PointF center, double rx, double ry) {
        THIS->drawEllipse(toQPointF(center), rx, ry);
    }

    void Handle_drawEllipse(HandleRef _this, Point center, int32_t rx, int32_t ry) {
        THIS->drawEllipse(toQPoint(center), rx, ry);
    }

    void Handle_drawPolyline(HandleRef _this, std::vector<PointF> points) {
        std::vector<QPointF> qPoints;
        for (auto &p : points) {
            qPoints.push_back(toQPointF(p));
        }
        THIS->drawPolyline(qPoints.data(), (int)qPoints.size());
    }

    void Handle_drawPolyline(HandleRef _this, std::vector<Point> points) {
        std::vector<QPoint> qPoints;
        for (auto &p : points) {
            qPoints.push_back(toQPoint(p));
        }
        THIS->drawPolyline(qPoints.data(), (int)qPoints.size());
    }
}
