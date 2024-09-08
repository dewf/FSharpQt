#pragma once
#include "TextOption.h"

namespace TextOption
{

    void WrapMode__push(WrapMode value);
    WrapMode WrapMode__pop();

    int __register();
}
