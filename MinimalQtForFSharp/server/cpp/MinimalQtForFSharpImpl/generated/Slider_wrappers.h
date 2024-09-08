#pragma once
#include "Slider.h"

namespace Slider
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

    void SignalHandler_actionTriggered__wrapper(int serverID);

    void SignalHandler_rangeChanged__wrapper(int serverID);

    void SignalHandler_sliderMoved__wrapper(int serverID);

    void SignalHandler_sliderPressed__wrapper(int serverID);

    void SignalHandler_sliderReleased__wrapper(int serverID);

    void SignalHandler_valueChanged__wrapper(int serverID);

    void TickPosition__push(TickPosition value);
    TickPosition TickPosition__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setTickInterval__wrapper();

    void Handle_setTickPosition__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
