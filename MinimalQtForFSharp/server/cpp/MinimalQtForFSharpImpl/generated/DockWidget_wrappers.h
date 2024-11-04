#pragma once
#include "DockWidget.h"

namespace DockWidget
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_nothingYet__wrapper();

    int __register();
}
