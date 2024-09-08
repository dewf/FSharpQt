#pragma once
#include "RegularExpression.h"

namespace RegularExpression
{

    void PatternOptions__push(PatternOptions value);
    PatternOptions PatternOptions__pop();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
