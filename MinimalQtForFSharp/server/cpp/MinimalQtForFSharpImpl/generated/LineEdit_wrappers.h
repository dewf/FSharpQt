#pragma once
#include "LineEdit.h"

namespace LineEdit
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_customContextMenuRequested__wrapper(int serverID);

    void SignalHandler_windowIconChanged__wrapper(int serverID);

    void SignalHandler_windowTitleChanged__wrapper(int serverID);

    void SignalHandler_cursorPositionChanged__wrapper(int serverID);

    void SignalHandler_editingFinished__wrapper(int serverID);

    void SignalHandler_inputRejected__wrapper(int serverID);

    void SignalHandler_returnPressed__wrapper(int serverID);

    void SignalHandler_selectionChanged__wrapper(int serverID);

    void SignalHandler_textChanged__wrapper(int serverID);

    void SignalHandler_textEdited__wrapper(int serverID);

    void EchoMode__push(EchoMode value);
    EchoMode EchoMode__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_hasAcceptableInput__wrapper();

    void Handle_setAlignment__wrapper();

    void Handle_setClearButtonEnabled__wrapper();

    void Handle_setCursorMoveStyle__wrapper();

    void Handle_setCursorPosition__wrapper();

    void Handle_displayText__wrapper();

    void Handle_setDragEnabled__wrapper();

    void Handle_setEchoMode__wrapper();

    void Handle_setFrame__wrapper();

    void Handle_hasSelectedText__wrapper();

    void Handle_setInputMask__wrapper();

    void Handle_setMaxLength__wrapper();

    void Handle_isModified__wrapper();

    void Handle_setModified__wrapper();

    void Handle_setPlaceholderText__wrapper();

    void Handle_setReadOnly__wrapper();

    void Handle_isRedoAvailable__wrapper();

    void Handle_selectedText__wrapper();

    void Handle_setText__wrapper();

    void Handle_text__wrapper();

    void Handle_isUndoAvailable__wrapper();

    void Handle_clear__wrapper();

    void Handle_copy__wrapper();

    void Handle_cut__wrapper();

    void Handle_paste__wrapper();

    void Handle_redo__wrapper();

    void Handle_selectAll__wrapper();

    void Handle_undo__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
