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

namespace PaintResources
{

    struct __Color; typedef struct __Color* ColorRef;
    struct __Gradient; typedef struct __Gradient* GradientRef;
    struct __RadialGradient; typedef struct __RadialGradient* RadialGradientRef; // extends GradientRef
    struct __LinearGradient; typedef struct __LinearGradient* LinearGradientRef; // extends GradientRef
    struct __Brush; typedef struct __Brush* BrushRef;
    struct __Pen; typedef struct __Pen* PenRef;
    struct __Font; typedef struct __Font* FontRef;
    struct __PainterPath; typedef struct __PainterPath* PainterPathRef;
    struct __PainterPathStroker; typedef struct __PainterPathStroker* PainterPathStrokerRef;
    struct __Handle; typedef struct __Handle* HandleRef;

    namespace Color {

        namespace Deferred {
            class Base;
        }

        enum class Constant {
            Black,
            White,
            DarkGray,
            Gray,
            LightGray,
            Red,
            Green,
            Blue,
            Cyan,
            Magenta,
            Yellow,
            DarkRed,
            DarkGreen,
            DarkBlue,
            DarkCyan,
            DarkMagenta,
            DarkYellow,
            Transparent
        };

        namespace Deferred {
            class FromConstant;
            class FromRGB;
            class FromRGBA;
            class FromRGBF;
            class FromRGBAF;

            class Visitor {
            public:
                virtual void onFromConstant(const FromConstant* value) = 0;
                virtual void onFromRGB(const FromRGB* value) = 0;
                virtual void onFromRGBA(const FromRGBA* value) = 0;
                virtual void onFromRGBF(const FromRGBF* value) = 0;
                virtual void onFromRGBAF(const FromRGBAF* value) = 0;
            };

            class Base {
            public:
                virtual void accept(Visitor* visitor) = 0;
            };

            class FromConstant : public Base {
            public:
                const Constant name;
                FromConstant(Constant name) : name(name) {}
                void accept(Visitor* visitor) override {
                    visitor->onFromConstant(this);
                }
            };

            class FromRGB : public Base {
            public:
                const int32_t r;
                const int32_t g;
                const int32_t b;
                FromRGB(int32_t r, int32_t g, int32_t b) : r(r), g(g), b(b) {}
                void accept(Visitor* visitor) override {
                    visitor->onFromRGB(this);
                }
            };

            class FromRGBA : public Base {
            public:
                const int32_t r;
                const int32_t g;
                const int32_t b;
                const int32_t a;
                FromRGBA(int32_t r, int32_t g, int32_t b, int32_t a) : r(r), g(g), b(b), a(a) {}
                void accept(Visitor* visitor) override {
                    visitor->onFromRGBA(this);
                }
            };

            class FromRGBF : public Base {
            public:
                const float r;
                const float g;
                const float b;
                FromRGBF(float r, float g, float b) : r(r), g(g), b(b) {}
                void accept(Visitor* visitor) override {
                    visitor->onFromRGBF(this);
                }
            };

            class FromRGBAF : public Base {
            public:
                const float r;
                const float g;
                const float b;
                const float a;
                FromRGBAF(float r, float g, float b, float a) : r(r), g(g), b(b), a(a) {}
                void accept(Visitor* visitor) override {
                    visitor->onFromRGBAF(this);
                }
            };
        }
    }

    void Gradient_setColorAt(GradientRef _this, double location, ColorRef color);



    namespace Brush {

        enum class Style {
            NoBrush = 0,
            SolidPattern = 1,
            Dense1Pattern = 2,
            Dense2Pattern = 3,
            Dense3Pattern = 4,
            Dense4Pattern = 5,
            Dense5Pattern = 6,
            Dense6Pattern = 7,
            Dense7Pattern = 8,
            HorPattern = 9,
            VerPattern = 10,
            CrossPattern = 11,
            BDiagPattern = 12,
            FDiagPattern = 13,
            DiagCrossPattern = 14,
            LinearGradientPattern = 15,
            ConicalGradientPattern = 17,
            RadialGradientPattern = 16,
            TexturePattern = 24
        };
    }

    namespace Pen {

        enum class Style {
            NoPen,
            SolidLine,
            DashLine,
            DotLine,
            DashDotLine,
            DashDotDotLine,
            CustomDashLine
        };

        enum class CapStyle {
            Flat,
            Square,
            Round
        };

        enum class JoinStyle {
            Miter,
            Bevel,
            Round,
            SvgMiter
        };
    }
    void Pen_setBrush(PenRef _this, BrushRef brush);
    void Pen_setWidth(PenRef _this, int32_t width);
    void Pen_setWidth(PenRef _this, double width);

    namespace Font {

        enum class Weight {
            Thin = 100,
            ExtraLight = 200,
            Light = 300,
            Normal = 400,
            Medium = 500,
            DemiBold = 600,
            Bold = 700,
            ExtraBold = 800,
            Black = 900
        };
    }

    void PainterPath_moveTo(PainterPathRef _this, Common::PointF p);
    void PainterPath_moveTo(PainterPathRef _this, double x, double y);
    void PainterPath_lineto(PainterPathRef _this, Common::PointF p);
    void PainterPath_lineTo(PainterPathRef _this, double x, double y);
    void PainterPath_cubicTo(PainterPathRef _this, Common::PointF c1, Common::PointF c2, Common::PointF endPoint);
    void PainterPath_cubicTo(PainterPathRef _this, double c1X, double c1Y, double c2X, double c2Y, double endPointX, double endPointY);

    void PainterPathStroker_setWidth(PainterPathStrokerRef _this, double width);
    void PainterPathStroker_setJoinStyle(PainterPathStrokerRef _this, Pen::JoinStyle style);
    void PainterPathStroker_setCapStyle(PainterPathStrokerRef _this, Pen::CapStyle style);
    void PainterPathStroker_setDashPattern(PainterPathStrokerRef _this, Pen::Style style);
    void PainterPathStroker_setDashPattern(PainterPathStrokerRef _this, std::vector<double> dashPattern);
    PainterPathRef PainterPathStroker_createStroke(PainterPathStrokerRef _this, PainterPathRef path);

    ColorRef Handle_createColor(HandleRef _this, Color::Constant name);
    ColorRef Handle_createColor(HandleRef _this, int32_t r, int32_t g, int32_t b);
    ColorRef Handle_createColor(HandleRef _this, int32_t r, int32_t g, int32_t b, int32_t a);
    ColorRef Handle_createColor(HandleRef _this, float r, float g, float b);
    ColorRef Handle_createColor(HandleRef _this, float r, float g, float b, float a);
    RadialGradientRef Handle_createRadialGradient(HandleRef _this, Common::PointF center, double radius);
    LinearGradientRef Handle_createLinearGradient(HandleRef _this, Common::PointF start, Common::PointF stop);
    LinearGradientRef Handle_createLinearGradient(HandleRef _this, double x1, double y1, double x2, double y2);
    BrushRef Handle_createBrush(HandleRef _this, Brush::Style style);
    BrushRef Handle_createBrush(HandleRef _this, ColorRef color);
    BrushRef Handle_createBrush(HandleRef _this, GradientRef gradient);
    PenRef Handle_createPen(HandleRef _this);
    PenRef Handle_createPen(HandleRef _this, Pen::Style style);
    PenRef Handle_createPen(HandleRef _this, ColorRef color);
    PenRef Handle_createPen(HandleRef _this, BrushRef brush, double width, Pen::Style style, Pen::CapStyle cap, Pen::JoinStyle join);
    FontRef Handle_createFont(HandleRef _this, std::string family, int32_t pointSize);
    FontRef Handle_createFont(HandleRef _this, std::string family, int32_t pointSize, Font::Weight weight);
    FontRef Handle_createFont(HandleRef _this, std::string family, int32_t pointSize, Font::Weight weight, bool italic);
    PainterPathRef Handle_createPainterPath(HandleRef _this);
    PainterPathStrokerRef Handle_createPainterPathStroker(HandleRef _this);
    PainterPathRef Handle_createStrokeInternal(HandleRef _this, PainterPathStrokerRef stroker, PainterPathRef path);
    void Handle_dispose(HandleRef _this);
    HandleRef create();
}
