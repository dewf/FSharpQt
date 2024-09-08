#pragma once
#include "StyleOptionViewItem.h"

namespace StyleOptionViewItem
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_dispose__wrapper();

    int __register();
}
