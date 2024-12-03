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

#include "Icon.h"
using namespace ::Icon;
#include "Common.h"
using namespace ::Common;
#include "Enums.h"
using namespace ::Enums;
#include "Color.h"
using namespace ::Color;

namespace Variant
{

    struct __Handle; typedef struct __Handle* HandleRef;
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    namespace ServerValue {
        class Base;
    }
    namespace Deferred {
        class Base;
    }

    namespace ServerValue {
        class Bool;
        class String;
        class Int;
        class Unknown;

        class Visitor {
        public:
            virtual void onBool(const Bool* value) = 0;
            virtual void onString(const String* value) = 0;
            virtual void onInt(const Int* value) = 0;
            virtual void onUnknown(const Unknown* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class Bool : public Base {
        public:
            const bool value;
            Bool(bool value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onBool(this);
            }
        };

        class String : public Base {
        public:
            const std::string value;
            String(std::string value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onString(this);
            }
        };

        class Int : public Base {
        public:
            const int32_t value;
            Int(int32_t value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onInt(this);
            }
        };

        class Unknown : public Base {
        public:
            Unknown() {}
            void accept(Visitor* visitor) override {
                visitor->onUnknown(this);
            }
        };
    }

    class VariantConversionFailure {
    public:
        VariantConversionFailure() {}
    };

    bool Handle_isValid(HandleRef _this);
    bool Handle_toBool(HandleRef _this);
    std::string Handle_toString2(HandleRef _this);
    int32_t Handle_toInt(HandleRef _this);
    Size Handle_toSize(HandleRef _this);
    CheckState Handle_toCheckState(HandleRef _this);
    Color::OwnedRef Handle_toColor(HandleRef _this);
    std::shared_ptr<ServerValue::Base> Handle_toServerValue(HandleRef _this);

    void Owned_dispose(OwnedRef _this);

    namespace Deferred {
        class Empty;
        class FromBool;
        class FromString;
        class FromInt;
        class FromCheckState;
        class FromSize;
        class FromIcon;
        class FromColor;
        class FromAligment;

        class Visitor {
        public:
            virtual void onEmpty(const Empty* value) = 0;
            virtual void onFromBool(const FromBool* value) = 0;
            virtual void onFromString(const FromString* value) = 0;
            virtual void onFromInt(const FromInt* value) = 0;
            virtual void onFromCheckState(const FromCheckState* value) = 0;
            virtual void onFromSize(const FromSize* value) = 0;
            virtual void onFromIcon(const FromIcon* value) = 0;
            virtual void onFromColor(const FromColor* value) = 0;
            virtual void onFromAligment(const FromAligment* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class Empty : public Base {
        public:
            Empty() {}
            void accept(Visitor* visitor) override {
                visitor->onEmpty(this);
            }
        };

        class FromBool : public Base {
        public:
            const bool value;
            FromBool(bool value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromBool(this);
            }
        };

        class FromString : public Base {
        public:
            const std::string value;
            FromString(std::string value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromString(this);
            }
        };

        class FromInt : public Base {
        public:
            const int32_t value;
            FromInt(int32_t value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromInt(this);
            }
        };

        class FromCheckState : public Base {
        public:
            const CheckState value;
            FromCheckState(CheckState value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromCheckState(this);
            }
        };

        class FromSize : public Base {
        public:
            const Size size;
            FromSize(Size size) : size(size) {}
            void accept(Visitor* visitor) override {
                visitor->onFromSize(this);
            }
        };

        class FromIcon : public Base {
        public:
            const Icon::HandleRef value;
            FromIcon(Icon::HandleRef value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromIcon(this);
            }
        };

        class FromColor : public Base {
        public:
            const std::shared_ptr<Color::Deferred::Base> value;
            FromColor(std::shared_ptr<Color::Deferred::Base> value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromColor(this);
            }
        };

        class FromAligment : public Base {
        public:
            const Alignment value;
            FromAligment(Alignment value) : value(value) {}
            void accept(Visitor* visitor) override {
                visitor->onFromAligment(this);
            }
        };
    }
}
