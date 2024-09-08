#pragma once
#include "FileDialog.h"

namespace FileDialog
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

    void SignalHandler_currentChanged__wrapper(int serverID);

    void SignalHandler_currentUrlChanged__wrapper(int serverID);

    void SignalHandler_directoryEntered__wrapper(int serverID);

    void SignalHandler_directoryUrlEntered__wrapper(int serverID);

    void SignalHandler_fileSelected__wrapper(int serverID);

    void SignalHandler_filesSelected__wrapper(int serverID);

    void SignalHandler_filterSelected__wrapper(int serverID);

    void SignalHandler_urlSelected__wrapper(int serverID);

    void SignalHandler_urlsSelected__wrapper(int serverID);

    void FileMode__push(FileMode value);
    FileMode FileMode__pop();

    void ViewMode__push(ViewMode value);
    ViewMode ViewMode__pop();

    void AcceptMode__push(AcceptMode value);
    AcceptMode AcceptMode__pop();

    void Options__push(Options value);
    Options Options__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setAcceptMode__wrapper();

    void Handle_setDefaultSuffix__wrapper();

    void Handle_setFileMode__wrapper();

    void Handle_setOptions__wrapper();

    void Handle_setSupportedSchemes__wrapper();

    void Handle_setViewMode__wrapper();

    void Handle_setNameFilter__wrapper();

    void Handle_setNameFilters__wrapper();

    void Handle_setMimeTypeFilters__wrapper();

    void Handle_setDirectory__wrapper();

    void Handle_selectFile__wrapper();

    void Handle_selectedFiles__wrapper();

    void Handle_setSignalMask__wrapper();

    void Handle_dispose__wrapper();

    void create__wrapper();

    int __register();
}
