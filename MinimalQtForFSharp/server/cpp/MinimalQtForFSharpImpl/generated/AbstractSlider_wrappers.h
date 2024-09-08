#pragma once
#include "AbstractSlider.h"

namespace AbstractSlider
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SliderAction__push(SliderAction value);
    SliderAction SliderAction__pop();

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

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setInvertedAppearance__wrapper();

    void Handle_setInvertedControls__wrapper();

    void Handle_setMaximum__wrapper();

    void Handle_setMinimum__wrapper();

    void Handle_setOrientation__wrapper();

    void Handle_setPageStep__wrapper();

    void Handle_setSingleStep__wrapper();

    void Handle_setSliderDown__wrapper();

    void Handle_setSliderPosition__wrapper();

    void Handle_setTracking__wrapper();

    void Handle_setValue__wrapper();

    void Handle_setRange__wrapper();

    void Handle_dispose__wrapper();

    int __register();
}
