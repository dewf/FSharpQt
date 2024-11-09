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

namespace Color
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

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


    void Owned_dispose(OwnedRef _this);

    namespace Deferred {
        class FromConstant;
        class FromHandle;
        class FromRGB;
        class FromRGBA;
        class FromRGBF;
        class FromRGBAF;

        class Visitor {
        public:
            virtual void onFromConstant(const FromConstant* value) = 0;
            virtual void onFromHandle(const FromHandle* value) = 0;
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

        class FromHandle : public Base {
        public:
            const HandleRef color;
            FromHandle(HandleRef color) : color(color) {}
            void accept(Visitor* visitor) override {
                visitor->onFromHandle(this);
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
