#pragma once
#include "Color.h"

namespace Color
{

    void Constant__push(Constant value);
    Constant Constant__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    void realize__wrapper();

    int __register();
}
