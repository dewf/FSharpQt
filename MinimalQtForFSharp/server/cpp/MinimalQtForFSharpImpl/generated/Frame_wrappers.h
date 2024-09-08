#pragma once
#include "Frame.h"

namespace Frame
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

    void Shape__push(Shape value);
    Shape Shape__pop();

    void Shadow__push(Shadow value);
    Shadow Shadow__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setFrameRect__wrapper();

    void Handle_setFrameShadow__wrapper();

    void Handle_setFrameShape__wrapper();

    void Handle_frameWidth__wrapper();

    void Handle_setLineWidth__wrapper();

    void Handle_setMidLineWidth__wrapper();

    void Handle_setFrameStyle__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
