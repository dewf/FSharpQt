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

    void create__wrapper();

    void create_overload1__wrapper();

    void create_overload2__wrapper();

    void create_overload3__wrapper();

    void create_overload4__wrapper();

    int __register();
}
