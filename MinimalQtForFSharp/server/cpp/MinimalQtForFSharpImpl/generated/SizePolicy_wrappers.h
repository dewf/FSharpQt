#pragma once
#include "SizePolicy.h"

namespace SizePolicy
{

    void Policy__push(Policy value);
    Policy Policy__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
