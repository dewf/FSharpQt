#pragma once
#include "PaintDevice.h"

namespace PaintDevice
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    int __register();
}
