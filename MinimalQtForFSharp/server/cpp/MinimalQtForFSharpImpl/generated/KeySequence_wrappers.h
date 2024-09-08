#pragma once
#include "KeySequence.h"

namespace KeySequence
{

    void StandardKey__push(StandardKey value);
    StandardKey StandardKey__pop();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
