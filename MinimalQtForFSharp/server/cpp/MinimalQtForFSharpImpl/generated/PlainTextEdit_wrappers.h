#pragma once
#include "PlainTextEdit.h"

namespace PlainTextEdit
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

    void SignalHandler_blockCountChanged__wrapper(int serverID);

    void SignalHandler_copyAvailable__wrapper(int serverID);

    void SignalHandler_cursorPositionChanged__wrapper(int serverID);

    void SignalHandler_modificationChanged__wrapper(int serverID);

    void SignalHandler_redoAvailable__wrapper(int serverID);

    void SignalHandler_selectionChanged__wrapper(int serverID);

    void SignalHandler_textChanged__wrapper(int serverID);

    void SignalHandler_undoAvailable__wrapper(int serverID);

    void SignalHandler_updateRequest__wrapper(int serverID);

    void LineWrapMode__push(LineWrapMode value);
    LineWrapMode LineWrapMode__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setBackgroundVisible__wrapper();

    void Handle_blockCount__wrapper();

    void Handle_setCenterOnScroll__wrapper();

    void Handle_setCursorWidth__wrapper();

    void Handle_setDocumentTitle__wrapper();

    void Handle_setLineWrapMode__wrapper();

    void Handle_setMaximumBlockCount__wrapper();

    void Handle_setOverwriteMode__wrapper();

    void Handle_setPlaceholderText__wrapper();

    void Handle_setPlainText__wrapper();

    void Handle_setReadOnly__wrapper();

    void Handle_setTabChangesFocus__wrapper();

    void Handle_setTabStopDistance__wrapper();

    void Handle_setTextInteractionFlags__wrapper();

    void Handle_setUndoRedoEnabled__wrapper();

    void Handle_setWordWrapMode__wrapper();

    void Handle_toPlainText__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
