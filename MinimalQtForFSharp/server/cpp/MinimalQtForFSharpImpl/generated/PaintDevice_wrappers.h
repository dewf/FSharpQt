#pragma once
#include "PaintDevice.h"

namespace PaintDevice
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_width__wrapper();

    void Handle_height__wrapper();

    int __register();
}
