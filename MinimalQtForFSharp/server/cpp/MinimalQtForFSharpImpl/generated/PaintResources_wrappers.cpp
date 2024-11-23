#include "../support/NativeImplServer.h"
#include "PaintResources_wrappers.h"
#include "PaintResources.h"

#include "Common_wrappers.h"
using namespace ::Common;

#include "Color_wrappers.h"
using namespace ::Color;

namespace PaintResources
{
    // built-in array type: std::vector<double>
    void Gradient__push(GradientRef value) {
        ni_pushPtr(value);
    }

    GradientRef Gradient__pop() {
        return (GradientRef)ni_popPtr();
    }

    void Gradient_setColorAt__wrapper() {
        auto _this = Gradient__pop();
        auto location = ni_popDouble();
        auto color = Color::Handle__pop();
        Gradient_setColorAt(_this, location, color);
    }
    void RadialGradient__push(RadialGradientRef value) {
        ni_pushPtr(value);
    }

    RadialGradientRef RadialGradient__pop() {
        return (RadialGradientRef)ni_popPtr();
    }
    void LinearGradient__push(LinearGradientRef value) {
        ni_pushPtr(value);
    }

    LinearGradientRef LinearGradient__pop() {
        return (LinearGradientRef)ni_popPtr();
    }
    void Brush__push(BrushRef value) {
        ni_pushPtr(value);
    }

    BrushRef Brush__pop() {
        return (BrushRef)ni_popPtr();
    }
    namespace Brush {
        void Style__push(Style value) {
            ni_pushInt32((int32_t)value);
        }

        Style Style__pop() {
            auto tag = ni_popInt32();
            return (Style)tag;
        }
    }
    void Pen__push(PenRef value) {
        ni_pushPtr(value);
    }

    PenRef Pen__pop() {
        return (PenRef)ni_popPtr();
    }
    namespace Pen {
        void Style__push(Style value) {
            ni_pushInt32((int32_t)value);
        }

        Style Style__pop() {
            auto tag = ni_popInt32();
            return (Style)tag;
        }
        void CapStyle__push(CapStyle value) {
            ni_pushInt32((int32_t)value);
        }

        CapStyle CapStyle__pop() {
            auto tag = ni_popInt32();
            return (CapStyle)tag;
        }
        void JoinStyle__push(JoinStyle value) {
            ni_pushInt32((int32_t)value);
        }

        JoinStyle JoinStyle__pop() {
            auto tag = ni_popInt32();
            return (JoinStyle)tag;
        }
    }

    void Pen_setBrush__wrapper() {
        auto _this = Pen__pop();
        auto brush = Brush__pop();
        Pen_setBrush(_this, brush);
    }

    void Pen_setWidth__wrapper() {
        auto _this = Pen__pop();
        auto width = ni_popInt32();
        Pen_setWidth(_this, width);
    }

    void Pen_setWidth_overload1__wrapper() {
        auto _this = Pen__pop();
        auto width = ni_popDouble();
        Pen_setWidth(_this, width);
    }
    void Font__push(FontRef value) {
        ni_pushPtr(value);
    }

    FontRef Font__pop() {
        return (FontRef)ni_popPtr();
    }
    namespace Font {
        void Weight__push(Weight value) {
            ni_pushInt32((int32_t)value);
        }

        Weight Weight__pop() {
            auto tag = ni_popInt32();
            return (Weight)tag;
        }
    }
    void PainterPath__push(PainterPathRef value) {
        ni_pushPtr(value);
    }

    PainterPathRef PainterPath__pop() {
        return (PainterPathRef)ni_popPtr();
    }

    void PainterPath_moveTo__wrapper() {
        auto _this = PainterPath__pop();
        auto p = PointF__pop();
        PainterPath_moveTo(_this, p);
    }

    void PainterPath_moveTo_overload1__wrapper() {
        auto _this = PainterPath__pop();
        auto x = ni_popDouble();
        auto y = ni_popDouble();
        PainterPath_moveTo(_this, x, y);
    }

    void PainterPath_lineto__wrapper() {
        auto _this = PainterPath__pop();
        auto p = PointF__pop();
        PainterPath_lineto(_this, p);
    }

    void PainterPath_lineTo_overload1__wrapper() {
        auto _this = PainterPath__pop();
        auto x = ni_popDouble();
        auto y = ni_popDouble();
        PainterPath_lineTo(_this, x, y);
    }

    void PainterPath_cubicTo__wrapper() {
        auto _this = PainterPath__pop();
        auto c1 = PointF__pop();
        auto c2 = PointF__pop();
        auto endPoint = PointF__pop();
        PainterPath_cubicTo(_this, c1, c2, endPoint);
    }

    void PainterPath_cubicTo_overload1__wrapper() {
        auto _this = PainterPath__pop();
        auto c1X = ni_popDouble();
        auto c1Y = ni_popDouble();
        auto c2X = ni_popDouble();
        auto c2Y = ni_popDouble();
        auto endPointX = ni_popDouble();
        auto endPointY = ni_popDouble();
        PainterPath_cubicTo(_this, c1X, c1Y, c2X, c2Y, endPointX, endPointY);
    }
    void PainterPathStroker__push(PainterPathStrokerRef value) {
        ni_pushPtr(value);
    }

    PainterPathStrokerRef PainterPathStroker__pop() {
        return (PainterPathStrokerRef)ni_popPtr();
    }

    void PainterPathStroker_setWidth__wrapper() {
        auto _this = PainterPathStroker__pop();
        auto width = ni_popDouble();
        PainterPathStroker_setWidth(_this, width);
    }

    void PainterPathStroker_setJoinStyle__wrapper() {
        auto _this = PainterPathStroker__pop();
        auto style = Pen::JoinStyle__pop();
        PainterPathStroker_setJoinStyle(_this, style);
    }

    void PainterPathStroker_setCapStyle__wrapper() {
        auto _this = PainterPathStroker__pop();
        auto style = Pen::CapStyle__pop();
        PainterPathStroker_setCapStyle(_this, style);
    }

    void PainterPathStroker_setDashPattern__wrapper() {
        auto _this = PainterPathStroker__pop();
        auto style = Pen::Style__pop();
        PainterPathStroker_setDashPattern(_this, style);
    }

    void PainterPathStroker_setDashPattern_overload1__wrapper() {
        auto _this = PainterPathStroker__pop();
        auto dashPattern = popDoubleArrayInternal();
        PainterPathStroker_setDashPattern(_this, dashPattern);
    }

    void PainterPathStroker_createStroke__wrapper() {
        auto _this = PainterPathStroker__pop();
        auto path = PainterPath__pop();
        PainterPath__push(PainterPathStroker_createStroke(_this, path));
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_createColor__wrapper() {
        auto _this = Handle__pop();
        auto name = Constant__pop();
        Color::Handle__push(Handle_createColor(_this, name));
    }

    void Handle_createColor_overload1__wrapper() {
        auto _this = Handle__pop();
        auto r = ni_popInt32();
        auto g = ni_popInt32();
        auto b = ni_popInt32();
        Color::Handle__push(Handle_createColor(_this, r, g, b));
    }

    void Handle_createColor_overload2__wrapper() {
        auto _this = Handle__pop();
        auto r = ni_popInt32();
        auto g = ni_popInt32();
        auto b = ni_popInt32();
        auto a = ni_popInt32();
        Color::Handle__push(Handle_createColor(_this, r, g, b, a));
    }

    void Handle_createColor_overload3__wrapper() {
        auto _this = Handle__pop();
        auto r = ni_popFloat();
        auto g = ni_popFloat();
        auto b = ni_popFloat();
        Color::Handle__push(Handle_createColor(_this, r, g, b));
    }

    void Handle_createColor_overload4__wrapper() {
        auto _this = Handle__pop();
        auto r = ni_popFloat();
        auto g = ni_popFloat();
        auto b = ni_popFloat();
        auto a = ni_popFloat();
        Color::Handle__push(Handle_createColor(_this, r, g, b, a));
    }

    void Handle_createRadialGradient__wrapper() {
        auto _this = Handle__pop();
        auto center = PointF__pop();
        auto radius = ni_popDouble();
        RadialGradient__push(Handle_createRadialGradient(_this, center, radius));
    }

    void Handle_createLinearGradient__wrapper() {
        auto _this = Handle__pop();
        auto start = PointF__pop();
        auto stop = PointF__pop();
        LinearGradient__push(Handle_createLinearGradient(_this, start, stop));
    }

    void Handle_createLinearGradient_overload1__wrapper() {
        auto _this = Handle__pop();
        auto x1 = ni_popDouble();
        auto y1 = ni_popDouble();
        auto x2 = ni_popDouble();
        auto y2 = ni_popDouble();
        LinearGradient__push(Handle_createLinearGradient(_this, x1, y1, x2, y2));
    }

    void Handle_createBrush__wrapper() {
        auto _this = Handle__pop();
        auto style = Brush::Style__pop();
        Brush__push(Handle_createBrush(_this, style));
    }

    void Handle_createBrush_overload1__wrapper() {
        auto _this = Handle__pop();
        auto color = Color::Handle__pop();
        Brush__push(Handle_createBrush(_this, color));
    }

    void Handle_createBrush_overload2__wrapper() {
        auto _this = Handle__pop();
        auto gradient = Gradient__pop();
        Brush__push(Handle_createBrush(_this, gradient));
    }

    void Handle_createPen__wrapper() {
        auto _this = Handle__pop();
        Pen__push(Handle_createPen(_this));
    }

    void Handle_createPen_overload1__wrapper() {
        auto _this = Handle__pop();
        auto style = Pen::Style__pop();
        Pen__push(Handle_createPen(_this, style));
    }

    void Handle_createPen_overload2__wrapper() {
        auto _this = Handle__pop();
        auto color = Color::Handle__pop();
        Pen__push(Handle_createPen(_this, color));
    }

    void Handle_createPen_overload3__wrapper() {
        auto _this = Handle__pop();
        auto brush = Brush__pop();
        auto width = ni_popDouble();
        auto style = Pen::Style__pop();
        auto cap = Pen::CapStyle__pop();
        auto join = Pen::JoinStyle__pop();
        Pen__push(Handle_createPen(_this, brush, width, style, cap, join));
    }

    void Handle_createFont__wrapper() {
        auto _this = Handle__pop();
        auto family = popStringInternal();
        auto pointSize = ni_popInt32();
        Font__push(Handle_createFont(_this, family, pointSize));
    }

    void Handle_createFont_overload1__wrapper() {
        auto _this = Handle__pop();
        auto family = popStringInternal();
        auto pointSize = ni_popInt32();
        auto weight = Font::Weight__pop();
        Font__push(Handle_createFont(_this, family, pointSize, weight));
    }

    void Handle_createFont_overload2__wrapper() {
        auto _this = Handle__pop();
        auto family = popStringInternal();
        auto pointSize = ni_popInt32();
        auto weight = Font::Weight__pop();
        auto italic = ni_popBool();
        Font__push(Handle_createFont(_this, family, pointSize, weight, italic));
    }

    void Handle_createPainterPath__wrapper() {
        auto _this = Handle__pop();
        PainterPath__push(Handle_createPainterPath(_this));
    }

    void Handle_createPainterPathStroker__wrapper() {
        auto _this = Handle__pop();
        PainterPathStroker__push(Handle_createPainterPathStroker(_this));
    }

    void Handle_createStrokeInternal__wrapper() {
        auto _this = Handle__pop();
        auto stroker = PainterPathStroker__pop();
        auto path = PainterPath__pop();
        PainterPath__push(Handle_createStrokeInternal(_this, stroker, path));
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    void create__wrapper() {
        Handle__push(create());
    }

    int __register() {
        auto m = ni_registerModule("PaintResources");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "Gradient_setColorAt", &Gradient_setColorAt__wrapper);
        ni_registerModuleMethod(m, "Pen_setBrush", &Pen_setBrush__wrapper);
        ni_registerModuleMethod(m, "Pen_setWidth", &Pen_setWidth__wrapper);
        ni_registerModuleMethod(m, "Pen_setWidth_overload1", &Pen_setWidth_overload1__wrapper);
        ni_registerModuleMethod(m, "PainterPath_moveTo", &PainterPath_moveTo__wrapper);
        ni_registerModuleMethod(m, "PainterPath_moveTo_overload1", &PainterPath_moveTo_overload1__wrapper);
        ni_registerModuleMethod(m, "PainterPath_lineto", &PainterPath_lineto__wrapper);
        ni_registerModuleMethod(m, "PainterPath_lineTo_overload1", &PainterPath_lineTo_overload1__wrapper);
        ni_registerModuleMethod(m, "PainterPath_cubicTo", &PainterPath_cubicTo__wrapper);
        ni_registerModuleMethod(m, "PainterPath_cubicTo_overload1", &PainterPath_cubicTo_overload1__wrapper);
        ni_registerModuleMethod(m, "PainterPathStroker_setWidth", &PainterPathStroker_setWidth__wrapper);
        ni_registerModuleMethod(m, "PainterPathStroker_setJoinStyle", &PainterPathStroker_setJoinStyle__wrapper);
        ni_registerModuleMethod(m, "PainterPathStroker_setCapStyle", &PainterPathStroker_setCapStyle__wrapper);
        ni_registerModuleMethod(m, "PainterPathStroker_setDashPattern", &PainterPathStroker_setDashPattern__wrapper);
        ni_registerModuleMethod(m, "PainterPathStroker_setDashPattern_overload1", &PainterPathStroker_setDashPattern_overload1__wrapper);
        ni_registerModuleMethod(m, "PainterPathStroker_createStroke", &PainterPathStroker_createStroke__wrapper);
        ni_registerModuleMethod(m, "Handle_createColor", &Handle_createColor__wrapper);
        ni_registerModuleMethod(m, "Handle_createColor_overload1", &Handle_createColor_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_createColor_overload2", &Handle_createColor_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_createColor_overload3", &Handle_createColor_overload3__wrapper);
        ni_registerModuleMethod(m, "Handle_createColor_overload4", &Handle_createColor_overload4__wrapper);
        ni_registerModuleMethod(m, "Handle_createRadialGradient", &Handle_createRadialGradient__wrapper);
        ni_registerModuleMethod(m, "Handle_createLinearGradient", &Handle_createLinearGradient__wrapper);
        ni_registerModuleMethod(m, "Handle_createLinearGradient_overload1", &Handle_createLinearGradient_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_createBrush", &Handle_createBrush__wrapper);
        ni_registerModuleMethod(m, "Handle_createBrush_overload1", &Handle_createBrush_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_createBrush_overload2", &Handle_createBrush_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_createPen", &Handle_createPen__wrapper);
        ni_registerModuleMethod(m, "Handle_createPen_overload1", &Handle_createPen_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_createPen_overload2", &Handle_createPen_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_createPen_overload3", &Handle_createPen_overload3__wrapper);
        ni_registerModuleMethod(m, "Handle_createFont", &Handle_createFont__wrapper);
        ni_registerModuleMethod(m, "Handle_createFont_overload1", &Handle_createFont_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_createFont_overload2", &Handle_createFont_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_createPainterPath", &Handle_createPainterPath__wrapper);
        ni_registerModuleMethod(m, "Handle_createPainterPathStroker", &Handle_createPainterPathStroker__wrapper);
        ni_registerModuleMethod(m, "Handle_createStrokeInternal", &Handle_createStrokeInternal__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        return 0; // = OK
    }
}
