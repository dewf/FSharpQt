#include "../support/NativeImplServer.h"
#include "Painter_wrappers.h"
#include "Painter.h"

#include "Common_wrappers.h"
using namespace ::Common;

#include "PaintResources_wrappers.h"
using namespace ::PaintResources;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace Painter
{
    // built-in array type: std::vector<int32_t>
    void __Point_Array__push(std::vector<Point> values, bool isReturn) {
        std::vector<int32_t> y_values;
        std::vector<int32_t> x_values;
        for (auto v = values.begin(); v != values.end(); v++) {
            y_values.push_back(v->y);
            x_values.push_back(v->x);
        }
        pushInt32ArrayInternal(y_values);
        pushInt32ArrayInternal(x_values);
    }

    std::vector<Point> __Point_Array__pop() {
        auto x_values = popInt32ArrayInternal();
        auto y_values = popInt32ArrayInternal();
        std::vector<Point> __ret;
        for (auto i = 0; i < x_values.size(); i++) {
            Point __value;
            __value.x = x_values[i];
            __value.y = y_values[i];
            __ret.push_back(__value);
        }
        return __ret;
    }
    // built-in array type: std::vector<double>
    void __PointF_Array__push(std::vector<PointF> values, bool isReturn) {
        std::vector<double> y_values;
        std::vector<double> x_values;
        for (auto v = values.begin(); v != values.end(); v++) {
            y_values.push_back(v->y);
            x_values.push_back(v->x);
        }
        pushDoubleArrayInternal(y_values);
        pushDoubleArrayInternal(x_values);
    }

    std::vector<PointF> __PointF_Array__pop() {
        auto x_values = popDoubleArrayInternal();
        auto y_values = popDoubleArrayInternal();
        std::vector<PointF> __ret;
        for (auto i = 0; i < x_values.size(); i++) {
            PointF __value;
            __value.x = x_values[i];
            __value.y = y_values[i];
            __ret.push_back(__value);
        }
        return __ret;
    }
    void RenderHint__push(RenderHint value) {
        ni_pushInt32((int32_t)value);
    }

    RenderHint RenderHint__pop() {
        auto tag = ni_popInt32();
        return (RenderHint)tag;
    }
    void RenderHintSet__push(RenderHintSet value) {
        ni_pushInt32(value);
    }

    RenderHintSet RenderHintSet__pop() {
        return ni_popInt32();
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setRenderHint__wrapper() {
        auto _this = Handle__pop();
        auto hint = RenderHint__pop();
        auto on = ni_popBool();
        Handle_setRenderHint(_this, hint, on);
    }

    void Handle_setRenderHints__wrapper() {
        auto _this = Handle__pop();
        auto hints = RenderHintSet__pop();
        auto on = ni_popBool();
        Handle_setRenderHints(_this, hints, on);
    }

    void Handle_setPen__wrapper() {
        auto _this = Handle__pop();
        auto pen = Pen__pop();
        Handle_setPen(_this, pen);
    }

    void Handle_setBrush__wrapper() {
        auto _this = Handle__pop();
        auto brush = Brush__pop();
        Handle_setBrush(_this, brush);
    }

    void Handle_setFont__wrapper() {
        auto _this = Handle__pop();
        auto font = Font__pop();
        Handle_setFont(_this, font);
    }

    void Handle_drawText__wrapper() {
        auto _this = Handle__pop();
        auto rect = Rect__pop();
        auto align = Alignment__pop();
        auto text = popStringInternal();
        Handle_drawText(_this, rect, align, text);
    }

    void Handle_fillPath__wrapper() {
        auto _this = Handle__pop();
        auto path = PainterPath__pop();
        auto brush = Brush__pop();
        Handle_fillPath(_this, path, brush);
    }

    void Handle_strokePath__wrapper() {
        auto _this = Handle__pop();
        auto path = PainterPath__pop();
        auto pen = Pen__pop();
        Handle_strokePath(_this, path, pen);
    }

    void Handle_fillRect__wrapper() {
        auto _this = Handle__pop();
        auto rect = Rect__pop();
        auto brush = Brush__pop();
        Handle_fillRect(_this, rect, brush);
    }

    void Handle_fillRect_overload1__wrapper() {
        auto _this = Handle__pop();
        auto rect = Rect__pop();
        auto color = Color__pop();
        Handle_fillRect(_this, rect, color);
    }

    void Handle_drawRect__wrapper() {
        auto _this = Handle__pop();
        auto rect = Rect__pop();
        Handle_drawRect(_this, rect);
    }

    void Handle_drawRect_overload1__wrapper() {
        auto _this = Handle__pop();
        auto rect = RectF__pop();
        Handle_drawRect(_this, rect);
    }

    void Handle_drawRect_overload2__wrapper() {
        auto _this = Handle__pop();
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Handle_drawRect(_this, x, y, width, height);
    }

    void Handle_drawEllipse__wrapper() {
        auto _this = Handle__pop();
        auto rectangle = RectF__pop();
        Handle_drawEllipse(_this, rectangle);
    }

    void Handle_drawEllipse_overload1__wrapper() {
        auto _this = Handle__pop();
        auto rectangle = Rect__pop();
        Handle_drawEllipse(_this, rectangle);
    }

    void Handle_drawEllipse_overload2__wrapper() {
        auto _this = Handle__pop();
        auto x = ni_popInt32();
        auto y = ni_popInt32();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Handle_drawEllipse(_this, x, y, width, height);
    }

    void Handle_drawEllipse_overload3__wrapper() {
        auto _this = Handle__pop();
        auto center = PointF__pop();
        auto rx = ni_popDouble();
        auto ry = ni_popDouble();
        Handle_drawEllipse(_this, center, rx, ry);
    }

    void Handle_drawEllipse_overload4__wrapper() {
        auto _this = Handle__pop();
        auto center = Point__pop();
        auto rx = ni_popInt32();
        auto ry = ni_popInt32();
        Handle_drawEllipse(_this, center, rx, ry);
    }

    void Handle_drawPolyline__wrapper() {
        auto _this = Handle__pop();
        auto points = __PointF_Array__pop();
        Handle_drawPolyline(_this, points);
    }

    void Handle_drawPolyline_overload1__wrapper() {
        auto _this = Handle__pop();
        auto points = __Point_Array__pop();
        Handle_drawPolyline(_this, points);
    }

    int __register() {
        auto m = ni_registerModule("Painter");
        ni_registerModuleMethod(m, "Handle_setRenderHint", &Handle_setRenderHint__wrapper);
        ni_registerModuleMethod(m, "Handle_setRenderHints", &Handle_setRenderHints__wrapper);
        ni_registerModuleMethod(m, "Handle_setPen", &Handle_setPen__wrapper);
        ni_registerModuleMethod(m, "Handle_setBrush", &Handle_setBrush__wrapper);
        ni_registerModuleMethod(m, "Handle_setFont", &Handle_setFont__wrapper);
        ni_registerModuleMethod(m, "Handle_drawText", &Handle_drawText__wrapper);
        ni_registerModuleMethod(m, "Handle_fillPath", &Handle_fillPath__wrapper);
        ni_registerModuleMethod(m, "Handle_strokePath", &Handle_strokePath__wrapper);
        ni_registerModuleMethod(m, "Handle_fillRect", &Handle_fillRect__wrapper);
        ni_registerModuleMethod(m, "Handle_fillRect_overload1", &Handle_fillRect_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_drawRect", &Handle_drawRect__wrapper);
        ni_registerModuleMethod(m, "Handle_drawRect_overload1", &Handle_drawRect_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_drawRect_overload2", &Handle_drawRect_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_drawEllipse", &Handle_drawEllipse__wrapper);
        ni_registerModuleMethod(m, "Handle_drawEllipse_overload1", &Handle_drawEllipse_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_drawEllipse_overload2", &Handle_drawEllipse_overload2__wrapper);
        ni_registerModuleMethod(m, "Handle_drawEllipse_overload3", &Handle_drawEllipse_overload3__wrapper);
        ni_registerModuleMethod(m, "Handle_drawEllipse_overload4", &Handle_drawEllipse_overload4__wrapper);
        ni_registerModuleMethod(m, "Handle_drawPolyline", &Handle_drawPolyline__wrapper);
        ni_registerModuleMethod(m, "Handle_drawPolyline_overload1", &Handle_drawPolyline_overload1__wrapper);
        return 0; // = OK
    }
}
