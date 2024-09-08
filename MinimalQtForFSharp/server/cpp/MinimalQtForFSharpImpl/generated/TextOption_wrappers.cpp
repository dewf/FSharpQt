#include "../support/NativeImplServer.h"
#include "TextOption_wrappers.h"
#include "TextOption.h"

namespace TextOption
{
    void WrapMode__push(WrapMode value) {
        ni_pushInt32((int32_t)value);
    }

    WrapMode WrapMode__pop() {
        auto tag = ni_popInt32();
        return (WrapMode)tag;
    }

    int __register() {
        auto m = ni_registerModule("TextOption");
        return 0; // = OK
    }
}
