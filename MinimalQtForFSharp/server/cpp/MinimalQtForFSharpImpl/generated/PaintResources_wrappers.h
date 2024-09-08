#pragma once
#include "PaintResources.h"

namespace PaintResources
{

    void Color__push(ColorRef value);
    ColorRef Color__pop();
    namespace Color {

        void Constant__push(Constant value);
        Constant Constant__pop();
        void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
        std::shared_ptr<Deferred::Base> Deferred__pop();
    }

    void Gradient__push(GradientRef value);
    GradientRef Gradient__pop();

    void Gradient_setColorAt__wrapper();

    void RadialGradient__push(RadialGradientRef value);
    RadialGradientRef RadialGradient__pop();

    void LinearGradient__push(LinearGradientRef value);
    LinearGradientRef LinearGradient__pop();

    void Brush__push(BrushRef value);
    BrushRef Brush__pop();
    namespace Brush {

        void Style__push(Style value);
        Style Style__pop();
    }

    void Pen__push(PenRef value);
    PenRef Pen__pop();
    namespace Pen {

        void Style__push(Style value);
        Style Style__pop();

        void CapStyle__push(CapStyle value);
        CapStyle CapStyle__pop();

        void JoinStyle__push(JoinStyle value);
        JoinStyle JoinStyle__pop();
    }

    void Pen_setBrush__wrapper();

    void Pen_setWidth__wrapper();

    void Pen_setWidth_overload1__wrapper();

    void Font__push(FontRef value);
    FontRef Font__pop();
    namespace Font {

        void Weight__push(Weight value);
        Weight Weight__pop();
    }

    void PainterPath__push(PainterPathRef value);
    PainterPathRef PainterPath__pop();

    void PainterPath_moveTo__wrapper();

    void PainterPath_moveTo_overload1__wrapper();

    void PainterPath_lineto__wrapper();

    void PainterPath_lineTo_overload1__wrapper();

    void PainterPath_cubicTo__wrapper();

    void PainterPath_cubicTo_overload1__wrapper();

    void PainterPathStroker__push(PainterPathStrokerRef value);
    PainterPathStrokerRef PainterPathStroker__pop();

    void PainterPathStroker_setWidth__wrapper();

    void PainterPathStroker_setJoinStyle__wrapper();

    void PainterPathStroker_setCapStyle__wrapper();

    void PainterPathStroker_setDashPattern__wrapper();

    void PainterPathStroker_setDashPattern_overload1__wrapper();

    void PainterPathStroker_createStroke__wrapper();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_createColor__wrapper();

    void Handle_createColor_overload1__wrapper();

    void Handle_createColor_overload2__wrapper();

    void Handle_createColor_overload3__wrapper();

    void Handle_createColor_overload4__wrapper();

    void Handle_createRadialGradient__wrapper();

    void Handle_createLinearGradient__wrapper();

    void Handle_createLinearGradient_overload1__wrapper();

    void Handle_createBrush__wrapper();

    void Handle_createBrush_overload1__wrapper();

    void Handle_createBrush_overload2__wrapper();

    void Handle_createPen__wrapper();

    void Handle_createPen_overload1__wrapper();

    void Handle_createPen_overload2__wrapper();

    void Handle_createPen_overload3__wrapper();

    void Handle_createFont__wrapper();

    void Handle_createFont_overload1__wrapper();

    void Handle_createFont_overload2__wrapper();

    void Handle_createPainterPath__wrapper();

    void Handle_createPainterPathStroker__wrapper();

    void Handle_createStrokeInternal__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
