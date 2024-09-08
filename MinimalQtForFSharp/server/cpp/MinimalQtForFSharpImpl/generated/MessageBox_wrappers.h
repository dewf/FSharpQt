#pragma once
#include "MessageBox.h"

namespace MessageBox
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

    void SignalHandler_accepted__wrapper(int serverID);

    void SignalHandler_finished__wrapper(int serverID);

    void SignalHandler_rejected__wrapper(int serverID);

    void SignalHandler_buttonClicked__wrapper(int serverID);

    void StandardButton__push(StandardButton value);
    StandardButton StandardButton__pop();

    void StandardButtonSet__push(StandardButtonSet value);
    StandardButtonSet StandardButtonSet__pop();

    void MessageBoxIcon__push(MessageBoxIcon value);
    MessageBoxIcon MessageBoxIcon__pop();

    void Options__push(Options value);
    Options Options__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setDetailedText__wrapper();

    void Handle_setIcon__wrapper();

    void Handle_setInformativeText__wrapper();

    void Handle_setOptions__wrapper();

    void Handle_setStandardButtons__wrapper();

    void Handle_setText__wrapper();

    void Handle_setTextFormat__wrapper();

    void Handle_setTextInteractionFlags__wrapper();

    void Handle_setDefaultButton__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
