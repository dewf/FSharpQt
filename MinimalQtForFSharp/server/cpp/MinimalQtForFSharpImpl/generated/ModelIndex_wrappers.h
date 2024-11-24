#pragma once
#include "ModelIndex.h"

namespace ModelIndex
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_isValid__wrapper();

    void Handle_row__wrapper();

    void Handle_column__wrapper();

    void Handle_data__wrapper();

    void Handle_data_overload1__wrapper();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();

    void create__wrapper();

    int __register();
}
