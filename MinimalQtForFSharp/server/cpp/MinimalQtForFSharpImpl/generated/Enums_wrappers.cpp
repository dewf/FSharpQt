#include "../support/NativeImplServer.h"
#include "Enums_wrappers.h"
#include "Enums.h"

namespace Enums
{
    void WindowFlags__push(WindowFlags value) {
        ni_pushUInt32(value);
    }

    WindowFlags WindowFlags__pop() {
        return ni_popUInt32();
    }
    void LayoutDirection__push(LayoutDirection value) {
        ni_pushInt32((int32_t)value);
    }

    LayoutDirection LayoutDirection__pop() {
        auto tag = ni_popInt32();
        return (LayoutDirection)tag;
    }
    void Alignment__push(Alignment value) {
        ni_pushInt32(value);
    }

    Alignment Alignment__pop() {
        return ni_popInt32();
    }
    void Orientation__push(Orientation value) {
        ni_pushInt32((int32_t)value);
    }

    Orientation Orientation__pop() {
        auto tag = ni_popInt32();
        return (Orientation)tag;
    }
    void ToolBarAreas__push(ToolBarAreas value) {
        ni_pushInt32(value);
    }

    ToolBarAreas ToolBarAreas__pop() {
        return ni_popInt32();
    }
    void ToolButtonStyle__push(ToolButtonStyle value) {
        ni_pushInt32((int32_t)value);
    }

    ToolButtonStyle ToolButtonStyle__pop() {
        auto tag = ni_popInt32();
        return (ToolButtonStyle)tag;
    }
    void WindowModality__push(WindowModality value) {
        ni_pushInt32((int32_t)value);
    }

    WindowModality WindowModality__pop() {
        auto tag = ni_popInt32();
        return (WindowModality)tag;
    }
    void ContextMenuPolicy__push(ContextMenuPolicy value) {
        ni_pushInt32((int32_t)value);
    }

    ContextMenuPolicy ContextMenuPolicy__pop() {
        auto tag = ni_popInt32();
        return (ContextMenuPolicy)tag;
    }
    void CursorMoveStyle__push(CursorMoveStyle value) {
        ni_pushInt32((int32_t)value);
    }

    CursorMoveStyle CursorMoveStyle__pop() {
        auto tag = ni_popInt32();
        return (CursorMoveStyle)tag;
    }
    void TextFormat__push(TextFormat value) {
        ni_pushInt32((int32_t)value);
    }

    TextFormat TextFormat__pop() {
        auto tag = ni_popInt32();
        return (TextFormat)tag;
    }
    void QDayOfWeek__push(QDayOfWeek value) {
        ni_pushInt32((int32_t)value);
    }

    QDayOfWeek QDayOfWeek__pop() {
        auto tag = ni_popInt32();
        return (QDayOfWeek)tag;
    }
    void TextInteractionFlags__push(TextInteractionFlags value) {
        ni_pushInt32(value);
    }

    TextInteractionFlags TextInteractionFlags__pop() {
        return ni_popInt32();
    }
    void FocusPolicy__push(FocusPolicy value) {
        ni_pushInt32(value);
    }

    FocusPolicy FocusPolicy__pop() {
        return ni_popInt32();
    }
    void ShortcutContext__push(ShortcutContext value) {
        ni_pushInt32((int32_t)value);
    }

    ShortcutContext ShortcutContext__pop() {
        auto tag = ni_popInt32();
        return (ShortcutContext)tag;
    }
    void DropAction__push(DropAction value) {
        ni_pushInt32((int32_t)value);
    }

    DropAction DropAction__pop() {
        auto tag = ni_popInt32();
        return (DropAction)tag;
    }
    void DropActionSet__push(DropActionSet value) {
        ni_pushInt32(value);
    }

    DropActionSet DropActionSet__pop() {
        return ni_popInt32();
    }
    void TextElideMode__push(TextElideMode value) {
        ni_pushInt32((int32_t)value);
    }

    TextElideMode TextElideMode__pop() {
        auto tag = ni_popInt32();
        return (TextElideMode)tag;
    }
    void CaseSensitivity__push(CaseSensitivity value) {
        ni_pushInt32((int32_t)value);
    }

    CaseSensitivity CaseSensitivity__pop() {
        auto tag = ni_popInt32();
        return (CaseSensitivity)tag;
    }
    void InputMethodHints__push(InputMethodHints value) {
        ni_pushUInt32(value);
    }

    InputMethodHints InputMethodHints__pop() {
        return ni_popUInt32();
    }
    void CheckState__push(CheckState value) {
        ni_pushInt32((int32_t)value);
    }

    CheckState CheckState__pop() {
        auto tag = ni_popInt32();
        return (CheckState)tag;
    }
    void SortOrder__push(SortOrder value) {
        ni_pushInt32((int32_t)value);
    }

    SortOrder SortOrder__pop() {
        auto tag = ni_popInt32();
        return (SortOrder)tag;
    }
    void ItemDataRole__push(ItemDataRole value) {
        ni_pushInt32((int32_t)value);
    }

    ItemDataRole ItemDataRole__pop() {
        auto tag = ni_popInt32();
        return (ItemDataRole)tag;
    }
    void TimerType__push(TimerType value) {
        ni_pushInt32((int32_t)value);
    }

    TimerType TimerType__pop() {
        auto tag = ni_popInt32();
        return (TimerType)tag;
    }
    void MouseButton__push(MouseButton value) {
        ni_pushInt32((int32_t)value);
    }

    MouseButton MouseButton__pop() {
        auto tag = ni_popInt32();
        return (MouseButton)tag;
    }
    void MouseButtonSet__push(MouseButtonSet value) {
        ni_pushInt32(value);
    }

    MouseButtonSet MouseButtonSet__pop() {
        return ni_popInt32();
    }
    void Modifiers__push(Modifiers value) {
        ni_pushUInt32(value);
    }

    Modifiers Modifiers__pop() {
        return ni_popUInt32();
    }
    void ImageConversionFlags__push(ImageConversionFlags value) {
        ni_pushInt32(value);
    }

    ImageConversionFlags ImageConversionFlags__pop() {
        return ni_popInt32();
    }
    void Key__push(Key value) {
        ni_pushInt32((int32_t)value);
    }

    Key Key__pop() {
        auto tag = ni_popInt32();
        return (Key)tag;
    }

    int __register() {
        auto m = ni_registerModule("Enums");
        return 0; // = OK
    }
}
