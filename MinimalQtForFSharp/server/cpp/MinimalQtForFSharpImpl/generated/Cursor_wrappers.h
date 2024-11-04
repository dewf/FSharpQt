#pragma once
#include "Cursor.h"

namespace Cursor
{

    void Unowned__push(UnownedRef value);
    UnownedRef Unowned__pop();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
