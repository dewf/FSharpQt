#pragma once
#include "Icon.h"

namespace Icon
{

    void Mode__push(Mode value);
    Mode Mode__pop();

    void State__push(State value);
    State State__pop();

    void ThemeIcon__push(ThemeIcon value);
    ThemeIcon ThemeIcon__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
