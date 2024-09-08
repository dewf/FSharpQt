#pragma once
#include "Action.h"

namespace Action
{

    void SignalMask__push(SignalMask value);
    SignalMask SignalMask__pop();

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn);
    std::shared_ptr<SignalHandler> SignalHandler__pop();

    void SignalHandler_destroyed__wrapper(int serverID);

    void SignalHandler_objectNameChanged__wrapper(int serverID);

    void SignalHandler_changed__wrapper(int serverID);

    void SignalHandler_checkableChanged__wrapper(int serverID);

    void SignalHandler_enabledChanged__wrapper(int serverID);

    void SignalHandler_hovered__wrapper(int serverID);

    void SignalHandler_toggled__wrapper(int serverID);

    void SignalHandler_triggered__wrapper(int serverID);

    void SignalHandler_visibleChanged__wrapper(int serverID);

    void MenuRole__push(MenuRole value);
    MenuRole MenuRole__pop();

    void Priority__push(Priority value);
    Priority Priority__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAutoRepeat__wrapper();

    void Handle_setCheckable__wrapper();

    void Handle_setChecked__wrapper();

    void Handle_setEnabled__wrapper();

    void Handle_setIcon__wrapper();

    void Handle_setIconText__wrapper();

    void Handle_setIconVisibleInMenu__wrapper();

    void Handle_setMenuRole__wrapper();

    void Handle_setPriority__wrapper();

    void Handle_setShortcut__wrapper();

    void Handle_setShortcutContext__wrapper();

    void Handle_setShortcutVisibleInContextMenu__wrapper();

    void Handle_setStatusTip__wrapper();

    void Handle_setText__wrapper();

    void Handle_setToolTip__wrapper();

    void Handle_setVisible__wrapper();

    void Handle_setWhatsThis__wrapper();

    void Handle_setSeparator__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
