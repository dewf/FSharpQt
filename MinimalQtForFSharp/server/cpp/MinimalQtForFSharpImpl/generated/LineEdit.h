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
#include "Widget.h"
using namespace ::Widget;
#include "Common.h"
using namespace ::Common;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;

namespace LineEdit
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Widget::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // SignalMask:
        CursorPositionChanged = 1 << 5,
        EditingFinished = 1 << 6,
        InputRejected = 1 << 7,
        ReturnPressed = 1 << 8,
        SelectionChanged = 1 << 9,
        TextChanged = 1 << 10,
        TextEdited = 1 << 11
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void cursorPositionChanged(int32_t oldPos, int32_t newPos) = 0;
        virtual void editingFinished() = 0;
        virtual void inputRejected() = 0;
        virtual void returnPressed() = 0;
        virtual void selectionChanged() = 0;
        virtual void textChanged(std::string text) = 0;
        virtual void textEdited(std::string text) = 0;
    };

    enum class EchoMode {
        Normal,
        NoEcho,
        Password,
        PasswordEchoOnEdit
    };

    bool Handle_hasAcceptableInput(HandleRef _this);
    void Handle_setAlignment(HandleRef _this, Enums::Alignment align);
    void Handle_setClearButtonEnabled(HandleRef _this, bool enabled);
    void Handle_setCursorMoveStyle(HandleRef _this, Enums::CursorMoveStyle style);
    void Handle_setCursorPosition(HandleRef _this, int32_t pos);
    std::string Handle_displayText(HandleRef _this);
    void Handle_setDragEnabled(HandleRef _this, bool enabled);
    void Handle_setEchoMode(HandleRef _this, EchoMode mode);
    void Handle_setFrame(HandleRef _this, bool enabled);
    bool Handle_hasSelectedText(HandleRef _this);
    void Handle_setInputMask(HandleRef _this, std::string mask);
    void Handle_setMaxLength(HandleRef _this, int32_t length);
    bool Handle_isModified(HandleRef _this);
    void Handle_setModified(HandleRef _this, bool modified);
    void Handle_setPlaceholderText(HandleRef _this, std::string text);
    void Handle_setReadOnly(HandleRef _this, bool value);
    bool Handle_isRedoAvailable(HandleRef _this);
    std::string Handle_selectedText(HandleRef _this);
    void Handle_setText(HandleRef _this, std::string text);
    std::string Handle_text(HandleRef _this);
    bool Handle_isUndoAvailable(HandleRef _this);
    void Handle_clear(HandleRef _this);
    void Handle_copy(HandleRef _this);
    void Handle_cut(HandleRef _this);
    void Handle_paste(HandleRef _this);
    void Handle_redo(HandleRef _this);
    void Handle_selectAll(HandleRef _this);
    void Handle_undo(HandleRef _this);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
