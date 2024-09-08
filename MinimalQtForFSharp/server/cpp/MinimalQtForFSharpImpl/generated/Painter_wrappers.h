#pragma once
#include "Painter.h"

namespace Painter
{

    void RenderHint__push(RenderHint value);
    RenderHint RenderHint__pop();

    void RenderHintSet__push(RenderHintSet value);
    RenderHintSet RenderHintSet__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setRenderHint__wrapper();

    void Handle_setRenderHints__wrapper();

    void Handle_setPen__wrapper();

    void Handle_setBrush__wrapper();

    void Handle_setFont__wrapper();

    void Handle_drawText__wrapper();

    void Handle_fillPath__wrapper();

    void Handle_strokePath__wrapper();

    void Handle_fillRect__wrapper();

    void Handle_fillRect_overload1__wrapper();

    void Handle_drawRect__wrapper();

    void Handle_drawRect_overload1__wrapper();

    void Handle_drawRect_overload2__wrapper();

    void Handle_drawEllipse__wrapper();

    void Handle_drawEllipse_overload1__wrapper();

    void Handle_drawEllipse_overload2__wrapper();

    void Handle_drawEllipse_overload3__wrapper();

    void Handle_drawEllipse_overload4__wrapper();

    void Handle_drawPolyline__wrapper();

    void Handle_drawPolyline_overload1__wrapper();

    int __register();
}
