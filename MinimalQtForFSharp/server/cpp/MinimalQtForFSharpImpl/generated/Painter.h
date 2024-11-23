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
    void Handle_setPen(HandleRef _this, PaintResources::PenRef pen);
    void Handle_setBrush(HandleRef _this, PaintResources::BrushRef brush);
    void Handle_setFont(HandleRef _this, PaintResources::FontRef font);
    void Handle_drawText(HandleRef _this, Common::Rect rect, Enums::Alignment align, std::string text);
    void Handle_fillPath(HandleRef _this, PaintResources::PainterPathRef path, PaintResources::BrushRef brush);
    void Handle_strokePath(HandleRef _this, PaintResources::PainterPathRef path, PaintResources::PenRef pen);
    void Handle_fillRect(HandleRef _this, Common::Rect rect, PaintResources::BrushRef brush);
    void Handle_fillRect(HandleRef _this, Common::Rect rect, Color::HandleRef color);
    void Handle_drawRect(HandleRef _this, Common::Rect rect);
    void Handle_drawRect(HandleRef _this, Common::RectF rect);
    void Handle_drawRect(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height);
    void Handle_drawEllipse(HandleRef _this, Common::RectF rectangle);
    void Handle_drawEllipse(HandleRef _this, Common::Rect rectangle);
    void Handle_drawEllipse(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height);
    void Handle_drawEllipse(HandleRef _this, Common::PointF center, double rx, double ry);
    void Handle_drawEllipse(HandleRef _this, Common::Point center, int32_t rx, int32_t ry);
    void Handle_drawPolyline(HandleRef _this, std::vector<Common::PointF> points);
    void Handle_drawPolyline(HandleRef _this, std::vector<Common::Point> points);
    void Handle_drawPixmap(HandleRef _this, Common::RectF target, Pixmap::HandleRef pixmap, Common::RectF source);
    void Handle_drawPixmap(HandleRef _this, Common::Point point, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, Common::PointF point, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, Common::Rect rect, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, Common::Point point, Pixmap::HandleRef pixmap, Common::Rect source);
    void Handle_drawPixmap(HandleRef _this, Common::PointF point, Pixmap::HandleRef pixmap, Common::RectF source);
    void Handle_drawPixmap(HandleRef _this, Common::Rect target, Pixmap::HandleRef pixmap, Common::Rect source);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height, Pixmap::HandleRef pixmap);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, Pixmap::HandleRef pixmap, int32_t sx, int32_t sy, int32_t sw, int32_t sh);
    void Handle_drawPixmap(HandleRef _this, int32_t x, int32_t y, int32_t w, int32_t h, Pixmap::HandleRef pixmap, int32_t sx, int32_t sy, int32_t sw, int32_t sh);
}
