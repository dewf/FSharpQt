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

namespace RegularExpression
{

    namespace Deferred {
        class Base;
    }

    typedef int32_t PatternOptions;
    enum PatternOptionsFlags : int32_t {
        NoPatternOption = 0x0000,
        CaseInsensitiveOption = 0x0001,
        DotMatchesEverythingOption = 0x0002,
        MultilineOption = 0x0004,
        ExtendedPatternSyntaxOption = 0x0008,
        InvertedGreedinessOption = 0x0010,
        DontCaptureOption = 0x0020,
        UseUnicodePropertiesOption = 0x0040
    };

    namespace Deferred {
        class Empty;
        class Regex;

        class Visitor {
        public:
            virtual void onEmpty(const Empty* value) = 0;
            virtual void onRegex(const Regex* value) = 0;
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

        class Regex : public Base {
        public:
            const std::string pattern;
            const PatternOptions opts;
            Regex(std::string pattern, PatternOptions opts) : pattern(pattern), opts(opts) {}
            void accept(Visitor* visitor) override {
                visitor->onRegex(this);
            }
        };
    }
}
