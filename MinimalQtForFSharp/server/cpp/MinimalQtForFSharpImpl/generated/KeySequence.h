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

#include "Enums.h"
using namespace ::Enums;

namespace KeySequence
{

    namespace Deferred {
        class Base;
    }

    enum class StandardKey {
        UnknownKey,
        HelpContents,
        WhatsThis,
        Open,
        Close,
        Save,
        New,
        Delete,
        Cut,
        Copy,
        Paste,
        Undo,
        Redo,
        Back,
        Forward,
        Refresh,
        ZoomIn,
        ZoomOut,
        Print,
        AddTab,
        NextChild,
        PreviousChild,
        Find,
        FindNext,
        FindPrevious,
        Replace,
        SelectAll,
        Bold,
        Italic,
        Underline,
        MoveToNextChar,
        MoveToPreviousChar,
        MoveToNextWord,
        MoveToPreviousWord,
        MoveToNextLine,
        MoveToPreviousLine,
        MoveToNextPage,
        MoveToPreviousPage,
        MoveToStartOfLine,
        MoveToEndOfLine,
        MoveToStartOfBlock,
        MoveToEndOfBlock,
        MoveToStartOfDocument,
        MoveToEndOfDocument,
        SelectNextChar,
        SelectPreviousChar,
        SelectNextWord,
        SelectPreviousWord,
        SelectNextLine,
        SelectPreviousLine,
        SelectNextPage,
        SelectPreviousPage,
        SelectStartOfLine,
        SelectEndOfLine,
        SelectStartOfBlock,
        SelectEndOfBlock,
        SelectStartOfDocument,
        SelectEndOfDocument,
        DeleteStartOfWord,
        DeleteEndOfWord,
        DeleteEndOfLine,
        InsertParagraphSeparator,
        InsertLineSeparator,
        SaveAs,
        Preferences,
        Quit,
        FullScreen,
        Deselect,
        DeleteCompleteLine,
        Backspace,
        Cancel
    };

    namespace Deferred {
        class FromString;
        class FromStandard;
        class FromKey;

        class Visitor {
        public:
            virtual void onFromString(const FromString* value) = 0;
            virtual void onFromStandard(const FromStandard* value) = 0;
            virtual void onFromKey(const FromKey* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class FromString : public Base {
        public:
            const std::string s;
            FromString(std::string s) : s(s) {}
            void accept(Visitor* visitor) override {
                visitor->onFromString(this);
            }
        };

        class FromStandard : public Base {
        public:
            const StandardKey key;
            FromStandard(StandardKey key) : key(key) {}
            void accept(Visitor* visitor) override {
                visitor->onFromStandard(this);
            }
        };

        class FromKey : public Base {
        public:
            const Enums::Key key;
            const Enums::Modifiers modifiers;
            FromKey(Enums::Key key, Enums::Modifiers modifiers) : key(key), modifiers(modifiers) {}
            void accept(Visitor* visitor) override {
                visitor->onFromKey(this);
            }
        };
    }
}
