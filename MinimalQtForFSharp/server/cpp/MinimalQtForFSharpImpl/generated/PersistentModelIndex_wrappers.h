#pragma once
#include "PersistentModelIndex.h"

namespace PersistentModelIndex
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_dispose__wrapper();

    int __register();
}
