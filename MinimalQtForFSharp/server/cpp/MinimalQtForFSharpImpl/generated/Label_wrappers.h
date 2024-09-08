#pragma once
#include "Label.h"

namespace Label
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

    void SignalHandler_linkActivated__wrapper(int serverID);

    void SignalHandler_linkHovered__wrapper(int serverID);

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAlignment__wrapper();

    void Handle_hasSelectedText__wrapper();

    void Handle_setIndent__wrapper();

    void Handle_setMargin__wrapper();

    void Handle_setOpenExternalLinks__wrapper();

    void Handle_setPixmap__wrapper();

    void Handle_setScaledContents__wrapper();

    void Handle_selectedText__wrapper();

    void Handle_setText__wrapper();

    void Handle_setTextFormat__wrapper();

    void Handle_setTextInteractionFlags__wrapper();

    void Handle_setWordWrap__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    void createNoHandler__wrapper();

    int __register();
}
