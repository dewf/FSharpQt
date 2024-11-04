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
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
