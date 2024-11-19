#pragma once
#include "Image.h"

namespace Image
{

    void __SizeT_Option__push(std::optional<size_t> value, bool isReturn);
    std::optional<size_t> __SizeT_Option__pop();

    inline void __Native_UInt8_Buffer__push(std::shared_ptr<NativeBuffer<uint8_t>> buf, bool isReturn);
    std::shared_ptr<NativeBuffer<uint8_t>> __Native_UInt8_Buffer__pop();

    void __String_Option__push(std::optional<std::string> value, bool isReturn);
    std::optional<std::string> __String_Option__pop();

    void Format__push(Format value);
    Format Format__pop();

    void ScaledOptions__push(ScaledOptions value, bool isReturn);
    ScaledOptions ScaledOptions__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_scaled__wrapper();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    void realize__wrapper();

    int __register();
}
