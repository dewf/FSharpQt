#pragma once
#include "Date.h"

namespace Date
{

    void YearMonthDay__push(YearMonthDay value, bool isReturn);
    YearMonthDay YearMonthDay__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_toYearMonthDay__wrapper();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
