#include "generated/PaintResources.h"

#include "PaintResourcesInternal.h"
#include "ColorInternal.h"

#include "util/convert.h"

namespace PaintResources
{
    // gradient ====================
    void Gradient_setColorAt(GradientRef _this, double location, std::shared_ptr<Deferred::Base> color) {
        _this->qGradPtr.setColorAt(location, Color::fromDeferred(color));
    }

    // radial gradient =============

    // linear gradient =============

    // brush ======================

    // pen =========================
    namespace Pen {
        inline Qt::PenStyle toQtStyle(Style style) {
            return (Qt::PenStyle)style;
        }

        inline Qt::PenCapStyle toQtCapStyle(CapStyle capStyle) {
            return (Qt::PenCapStyle)capStyle;
        }

        inline Qt::PenJoinStyle toQtJoinStyle(JoinStyle joinStyle) {
            return (Qt::PenJoinStyle)joinStyle;
        }
    }

    void Pen_setBrush(PenRef _this, BrushRef brush) {
        _this->qPen.setBrush(brush->qBrush);
    }

    void Pen_setWidth(PenRef _this, int32_t width) {
        _this->qPen.setWidth(width);
    }

    void Pen_setWidth(PenRef _this, double width) {
        _this->qPen.setWidthF(width);
    }

    // font =====================================
    namespace Font {
        inline QFont::Weight toQtWeight(Weight weight) {
            // simple cast should be OK for now
            return (QFont::Weight)weight;
        }
    }

    // painter path =========================================
    void PainterPath_moveTo(PainterPathRef _this, PointF p) {
        _this->qPath.moveTo(toQPointF(p));
    }

    void PainterPath_moveTo(PainterPathRef _this, double x, double y) {
        _this->qPath.moveTo(x, y);
    }

    void PainterPath_lineto(PainterPathRef _this, PointF p) {
        _this->qPath.lineTo(toQPointF(p));
    }

    void PainterPath_lineTo(PainterPathRef _this, double x, double y) {
        _this->qPath.lineTo(x, y);
    }

    void PainterPath_cubicTo(PainterPathRef _this, PointF c1, PointF c2, PointF endPoint) {
        _this->qPath.cubicTo(toQPointF(c1), toQPointF(c2), toQPointF(endPoint));
    }

    void PainterPath_cubicTo(PainterPathRef _this, double c1X, double c1Y, double c2X, double c2Y, double endPointX, double endPointY) {
        _this->qPath.cubicTo(c1X, c1Y, c2X, c2Y, endPointX, endPointY);
    }

    // painter path stroker ==================================
    void PainterPathStroker_setWidth(PainterPathStrokerRef _this, double width) {
        _this->qStroker.setWidth(width);
    }

    void PainterPathStroker_setJoinStyle(PainterPathStrokerRef _this, Pen::JoinStyle style) {
        _this->qStroker.setJoinStyle(Pen::toQtJoinStyle(style));
    }

    void PainterPathStroker_setCapStyle(PainterPathStrokerRef _this, Pen::CapStyle style) {
        _this->qStroker.setCapStyle(Pen::toQtCapStyle(style));
    }

    void PainterPathStroker_setDashPattern(PainterPathStrokerRef _this, Pen::Style style) {
        _this->qStroker.setDashPattern(Pen::toQtStyle(style));
    }

    void PainterPathStroker_setDashPattern(PainterPathStrokerRef _this, std::vector<double> dashPattern) {
        QList<qreal> qPattern;
        for (auto &x : dashPattern) {
            qPattern.push_back(x);
        }
        _this->qStroker.setDashPattern(qPattern);
    }

    PainterPathRef PainterPathStroker_createStroke(PainterPathStrokerRef _this, PainterPathRef path) {
        // this is actually implemented in the items, because we need to place the result on the 'items'
        return Handle_createStrokeInternal(_this->resources, _this, path);
    }

    // =========== paint items object ========================================
    struct __Handle {
        std::vector<PaintStackItem*> items;
        ~__Handle() {
            for (auto item : items) {
                delete item;
            }
        }
    };

    Color::HandleRef Handle_createColor(HandleRef _this, Color::Constant name) {
        auto ret = new Color::__Handle { Color::fromConstant(name) };
        _this->items.push_back(ret);
        return ret;
    }

    Color::HandleRef Handle_createColor(HandleRef _this, int32_t r, int32_t g, int32_t b) {
        auto ret = new Color::__Handle { QColor::fromRgb(r, g, b) };
        _this->items.push_back(ret);
        return ret;
    }

    Color::HandleRef Handle_createColor(HandleRef _this, int32_t r, int32_t g, int32_t b, int32_t a) {
        auto ret = new Color::__Handle { QColor::fromRgb(r, g, b, a) };
        _this->items.push_back(ret);
        return ret;
    }

    Color::HandleRef Handle_createColor(HandleRef _this, float r, float g, float b) {
        auto ret = new Color::__Handle { QColor::fromRgbF(r, g, b) };
        _this->items.push_back(ret);
        return ret;
    }

    Color::HandleRef Handle_createColor(HandleRef _this, float r, float g, float b, float a) {
        auto ret = new Color::__Handle { QColor::fromRgbF(r, g, b, a) };
        _this->items.push_back(ret);
        return ret;
    }

    RadialGradientRef Handle_createRadialGradient(HandleRef _this, PointF center, double radius) {
        auto ret = new __RadialGradient { QRadialGradient(toQPointF(center), radius) };
        _this->items.push_back(ret);
        return ret;
    }

    LinearGradientRef Handle_createLinearGradient(HandleRef _this, PointF start, PointF stop) {
        auto ret = new __LinearGradient { QLinearGradient(toQPointF(start), toQPointF(stop)) };
        _this->items.push_back(ret);
        return ret;
    }

    LinearGradientRef Handle_createLinearGradient(HandleRef _this, double x1, double y1, double x2, double y2) {
        auto ret = new __LinearGradient { QLinearGradient(x1, y1, x2, y2) };
        _this->items.push_back(ret);
        return ret;
    }

    BrushRef Handle_createBrush(HandleRef _this, Brush::Style style) {
        auto ret = new __Brush { QBrush((Qt::BrushStyle)style) };
        _this->items.push_back(ret);
        return ret;
    }

    BrushRef Handle_createBrush(HandleRef _this, std::shared_ptr<Deferred::Base> color) {
        auto ret = new __Brush { Color::fromDeferred(color) };
        _this->items.push_back(ret);
        return ret;
    }

    BrushRef Handle_createBrush(HandleRef _this, GradientRef gradient) {
        auto ret = new __Brush { QBrush(gradient->qGradPtr) };
        _this->items.push_back(ret);
        return ret;
    }

    PenRef Handle_createPen(HandleRef _this) {
        auto ret = new __Pen { QPen() };
        _this->items.push_back(ret);
        return ret;
    }

    PenRef Handle_createPen(HandleRef _this, Pen::Style style) {
        auto ret = new __Pen { QPen(Pen::toQtStyle(style)) };
        _this->items.push_back(ret);
        return ret;
    }

    PenRef Handle_createPen(HandleRef _this, std::shared_ptr<Color::Deferred::Base> color) {
        auto ret = new __Pen { Color::fromDeferred(color) };
        _this->items.push_back(ret);
        return ret;
    }

    PenRef Handle_createPen(HandleRef _this, BrushRef brush, double width, Pen::Style style, Pen::CapStyle cap, Pen::JoinStyle join) {
        auto ret = new __Pen { QPen(brush->qBrush, width, Pen::toQtStyle(style), Pen::toQtCapStyle(cap), Pen::toQtJoinStyle(join)) };
        _this->items.push_back(ret);
        return ret;
    }

    FontRef Handle_createFont(HandleRef _this, std::string family, int32_t pointSize) {
        auto ret = new __Font { QFont(QString::fromStdString(family), pointSize) };
        _this->items.push_back(ret);
        return ret;
    }

    FontRef Handle_createFont(HandleRef _this, std::string family, int32_t pointSize, Font::Weight weight) {
        auto ret = new __Font { QFont(QString::fromStdString(family), pointSize, Font::toQtWeight(weight)) };
        _this->items.push_back(ret);
        return ret;
    }

    FontRef Handle_createFont(HandleRef _this, std::string family, int32_t pointSize, Font::Weight weight, bool italic) {
        auto ret = new __Font { QFont(QString::fromStdString(family), pointSize, Font::toQtWeight(weight), italic) };
        _this->items.push_back(ret);
        return ret;
    }

    PainterPathRef Handle_createPainterPath(HandleRef _this) {
        auto ret = new __PainterPath();
        _this->items.push_back(ret);
        return ret;
    }

    PainterPathStrokerRef Handle_createPainterPathStroker(HandleRef _this) {
        auto ret = new __PainterPathStroker(_this); // notice _this param
        _this->items.push_back(ret);
        return ret;
    }

    PainterPathRef Handle_createStrokeInternal(HandleRef _this, PainterPathStrokerRef stroker, PainterPathRef path) {
        auto qPath = stroker->qStroker.createStroke(path->qPath);
        auto ret = new __PainterPath { qPath };
        _this->items.push_back(ret);
        return ret;
    }

    void Handle_dispose(HandleRef _this) {
        delete _this;
    }

    HandleRef create() {
        return new __Handle();
    }
}
