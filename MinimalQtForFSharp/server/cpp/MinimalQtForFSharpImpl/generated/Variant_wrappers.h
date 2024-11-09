#pragma once
#include "Variant.h"

namespace Variant
{
    void ServerValue__push(std::shared_ptr<ServerValue::Base> value, bool isReturn);
    std::shared_ptr<ServerValue::Base> ServerValue__pop();

    void VariantConversionFailure__push(VariantConversionFailure e);
    void VariantConversionFailure__buildAndThrow();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_isValid__wrapper();

    void Handle_toBool__wrapper();

    void Handle_toString2__wrapper();

    void Handle_toInt__wrapper();

    void Handle_toSize__wrapper();

    void Handle_toCheckState__wrapper();

    void Handle_toColor__wrapper();

    void Handle_toServerValue__wrapper();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();
    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn);
    std::shared_ptr<Deferred::Base> Deferred__pop();

    int __register();
}
