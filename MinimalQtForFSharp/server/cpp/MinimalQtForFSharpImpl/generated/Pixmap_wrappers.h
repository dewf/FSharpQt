#pragma once
#include "Pixmap.h"

namespace Pixmap
{

    void __ImageConversionFlags_Option__push(std::optional<ImageConversionFlags> value, bool isReturn);
    std::optional<ImageConversionFlags> __ImageConversionFlags_Option__pop();

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Owned__push(OwnedRef value);
    OwnedRef Owned__pop();

    void Owned_dispose__wrapper();

    void FilenameOptions__push(FilenameOptions value, bool isReturn);
    FilenameOptions FilenameOptions__pop();

    void create__wrapper();

    void create_overload1__wrapper();

    void fromImage__wrapper();

    int __register();
}
