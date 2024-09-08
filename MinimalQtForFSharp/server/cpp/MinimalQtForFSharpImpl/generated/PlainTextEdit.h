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

#include "Object.h"
using namespace ::Object;
#include "Common.h"
using namespace ::Common;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;
#include "AbstractScrollArea.h"
using namespace ::AbstractScrollArea;
#include "TextOption.h"
using namespace ::TextOption;

namespace PlainTextEdit
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractScrollArea::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // Frame::SignalMask:
        // AbstractScrollArea::SignalMask:
        // SignalMask:
        BlockCountChanged = 1 << 5,
        CopyAvailable = 1 << 6,
        CursorPositionChanged = 1 << 7,
        ModificationChanged = 1 << 8,
        RedoAvailable = 1 << 9,
        SelectionChanged = 1 << 10,
        TextChanged = 1 << 11,
        UndoAvailable = 1 << 12,
        UpdateRequest = 1 << 13
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void blockCountChanged(int32_t newBlockCount) = 0;
        virtual void copyAvailable(bool yes) = 0;
        virtual void cursorPositionChanged() = 0;
        virtual void modificationChanged(bool changed) = 0;
        virtual void redoAvailable(bool available) = 0;
        virtual void selectionChanged() = 0;
        virtual void textChanged() = 0;
        virtual void undoAvailable(bool available) = 0;
        virtual void updateRequest(Common::Rect rect, int32_t dy) = 0;
    };

    enum class LineWrapMode {
        NoWrap,
        WidgetWidth
    };

    void Handle_setBackgroundVisible(HandleRef _this, bool visible);
    int32_t Handle_blockCount(HandleRef _this);
    void Handle_setCenterOnScroll(HandleRef _this, bool state);
    void Handle_setCursorWidth(HandleRef _this, int32_t width);
    void Handle_setDocumentTitle(HandleRef _this, std::string title);
    void Handle_setLineWrapMode(HandleRef _this, LineWrapMode mode);
    void Handle_setMaximumBlockCount(HandleRef _this, int32_t count);
    void Handle_setOverwriteMode(HandleRef _this, bool overwrite);
    void Handle_setPlaceholderText(HandleRef _this, std::string text);
    void Handle_setPlainText(HandleRef _this, std::string text);
    void Handle_setReadOnly(HandleRef _this, bool state);
    void Handle_setTabChangesFocus(HandleRef _this, bool state);
    void Handle_setTabStopDistance(HandleRef _this, double distance);
    void Handle_setTextInteractionFlags(HandleRef _this, Enums::TextInteractionFlags tiFlags);
    void Handle_setUndoRedoEnabled(HandleRef _this, bool enabled);
    void Handle_setWordWrapMode(HandleRef _this, TextOption::WrapMode mode);
    std::string Handle_toPlainText(HandleRef _this);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
