#pragma once
#include "Enums.h"

namespace Enums
{

    void WindowFlags__push(WindowFlags value);
    WindowFlags WindowFlags__pop();

    void LayoutDirection__push(LayoutDirection value);
    LayoutDirection LayoutDirection__pop();

    void Alignment__push(Alignment value);
    Alignment Alignment__pop();

    void Orientation__push(Orientation value);
    Orientation Orientation__pop();

    void ToolBarAreas__push(ToolBarAreas value);
    ToolBarAreas ToolBarAreas__pop();

    void ToolButtonStyle__push(ToolButtonStyle value);
    ToolButtonStyle ToolButtonStyle__pop();

    void WindowModality__push(WindowModality value);
    WindowModality WindowModality__pop();

    void ContextMenuPolicy__push(ContextMenuPolicy value);
    ContextMenuPolicy ContextMenuPolicy__pop();

    void CursorMoveStyle__push(CursorMoveStyle value);
    CursorMoveStyle CursorMoveStyle__pop();

    void TextFormat__push(TextFormat value);
    TextFormat TextFormat__pop();

    void QDayOfWeek__push(QDayOfWeek value);
    QDayOfWeek QDayOfWeek__pop();

    void TextInteractionFlags__push(TextInteractionFlags value);
    TextInteractionFlags TextInteractionFlags__pop();

    void FocusPolicy__push(FocusPolicy value);
    FocusPolicy FocusPolicy__pop();

    void ShortcutContext__push(ShortcutContext value);
    ShortcutContext ShortcutContext__pop();

    void DropAction__push(DropAction value);
    DropAction DropAction__pop();

    void DropActionSet__push(DropActionSet value);
    DropActionSet DropActionSet__pop();

    void TextElideMode__push(TextElideMode value);
    TextElideMode TextElideMode__pop();

    void CaseSensitivity__push(CaseSensitivity value);
    CaseSensitivity CaseSensitivity__pop();

    void InputMethodHints__push(InputMethodHints value);
    InputMethodHints InputMethodHints__pop();

    void CheckState__push(CheckState value);
    CheckState CheckState__pop();

    void SortOrder__push(SortOrder value);
    SortOrder SortOrder__pop();

    void ItemDataRole__push(ItemDataRole value);
    ItemDataRole ItemDataRole__pop();

    void TimerType__push(TimerType value);
    TimerType TimerType__pop();

    void MouseButton__push(MouseButton value);
    MouseButton MouseButton__pop();

    void MouseButtonSet__push(MouseButtonSet value);
    MouseButtonSet MouseButtonSet__pop();

    void Modifiers__push(Modifiers value);
    Modifiers Modifiers__pop();

    void ImageConversionFlags__push(ImageConversionFlags value);
    ImageConversionFlags ImageConversionFlags__pop();

    void Key__push(Key value);
    Key Key__pop();

    int __register();
}
