#include "generated/SizePolicy.h"

#include <QSizePolicy>
#include "SizePolicyInternal.h"

#define THIS ((QSizePolicy*)_this)

namespace SizePolicy
{
    void Handle_dispose(HandleRef _this) {
        printf("SizePolicy Handle_dispose: should never see this (unowned handle)\n");
    }

    void OwnedHandle_dispose(OwnedHandleRef _this) {
        delete THIS;
    }

    QSizePolicy fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        // TODO
        return {};
    }
}
