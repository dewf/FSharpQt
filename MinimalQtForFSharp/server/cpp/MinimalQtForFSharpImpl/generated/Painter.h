#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

#include "Common.h"
using namespace ::Common;
#include "PaintResources.h"
using namespace ::PaintResources;
#include "Enums.h"
using namespace ::Enums;
#include "Color.h"
using namespace ::Color;
#include "Pixmap.h"
using namespace ::Pixmap;

namespace Painter
{

    struct __Handle; typedef struct __Handle* HandleRef;

    enum class RenderHint {
        Antialiasing = 0x01,
        TextAntialiasing = 0x02,
        SmoothPixmapTransform = 0x04,
        VerticalSubpixelPositioning = 0x08,
        LosslessImageRendering = 0x40,
        NonCosmeticBrushPatterns = 0x80
    };

    typedef int32_t RenderHintSet;
    enum RenderHintSetFlags : int32_t {
        Antialiasing = 0x01,
        TextAntialiasing = 0x02,
        SmoothPixmapTransform = 0x04,
        VerticalSubpixelPositioning = 0x08,
        LosslessImageRendering = 0x40,
        NonCosmeticBrushPatterns = 0x80
    };

    void Handle_setRenderHint(HandleRef _this, RenderHint hint, bool on);
    void Handle_setRenderHints(HandleRef _this, RenderHintSet hints, bool on);
    void Handle_setPen(HandleRef _this, PenRef pen);
    void Handle_setBrush(HandleRef _this, BrushRef brush);
    void Handle_setFont(HandleRef _this, FontRef font);
    void Handle_drawText(HandleRef _this, Rect rect, Alignment align, std::string text);
    void Handle_fillPath(HandleRef _this, PainterPathRef path, BrushRef brush);
    void Handle_strokePath(HandleRef _this, PainterPathRef path, PenRef pen);
    void Handle_fillRect(HandleRef _this, Rect rect, BrushRef brush);
    void Handle_fillRect(HandleRef _this, Rect rect, std::shared_ptr<Deferred::Base> color);
    void Handle_drawRect(HandleRef _this, Rect rect);
    void Handle_drawRect(HandleRef _this, RectF rect);
    void Handle_drawRect(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height);
    void Handle_drawEllipse(HandleRef _this, RectF rectangle);
    void Handle_drawEllipse(HandleRef _this, Rect rectangle);
    void Handle_drawEllipse(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height);
    void Handle_drawEllipse(HandleRef _this, PointF center, double rx, double ry);
    void Handle_drawEllipse(HandleRef _this, Point center, int32_t rx, int32_t ry);
    void Handle_drawPolyline(HandleRef _this, std::vector<PointF> points);
    void Handle_drawPolyline(HandleRef _this, std::vector<Point> points);
    void Handle_drawPixmap(HandleRef _this, RectF target, Pixmap::HandleRef pixmap, RectF source);
    void Handle_drawPixmap(HandleRef _this, Point point, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, PointF point, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, Rect rect, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, Point point, Pixmap::HandleRef pixmap, Rect source);
    void Handle_drawPixmap(HandleRef _this, PointF point, Pixmap::HandleRef pixmap, RectF source);
    void Handle_drawPixmap(HandleRef _this, Rect target, Pixmap::HandleRef pixmap, Rect source);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, Pixmap::HandleRef pixmap, int32_t sx, int32_t sy, int32_t sw, int32_t sh);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, int32_t w, int32_t h, Pixmap::HandleRef pixmap, int32_t sx, int32_t sy, int32_t sw, int32_t sh);
}
