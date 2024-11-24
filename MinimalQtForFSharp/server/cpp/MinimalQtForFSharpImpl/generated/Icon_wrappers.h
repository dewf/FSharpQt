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

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();

    void create__wrapper();

    void create_overload1__wrapper();

    void create_overload2__wrapper();

    void create_overload3__wrapper();

    int __register();
}
