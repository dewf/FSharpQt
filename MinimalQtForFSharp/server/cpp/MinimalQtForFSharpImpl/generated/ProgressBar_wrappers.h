#pragma once
#include "ProgressBar.h"

namespace ProgressBar
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

    void SignalHandler_valueChanged__wrapper(int serverID);

    void Direction__push(Direction value);
    Direction Direction__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAlignment__wrapper();

    void Handle_setFormat__wrapper();

    void Handle_setInvertedAppearance__wrapper();

    void Handle_setMaximum__wrapper();

    void Handle_setMinimum__wrapper();

    void Handle_setOrientation__wrapper();

    void Handle_text__wrapper();

    void Handle_setTextDirection__wrapper();

    void Handle_setTextVisible__wrapper();

    void Handle_setValue__wrapper();

    void Handle_setRange__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
