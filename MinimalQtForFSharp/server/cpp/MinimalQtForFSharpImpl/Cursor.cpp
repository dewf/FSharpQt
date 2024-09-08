#include "generated/Cursor.h"

#include <QCursor>
#include "CursorInternal.h"

#define THIS ((QCursor*)_this)

namespace Cursor
{
    void Handle_dispose(HandleRef _this) {
        printf("Cursor Handle_dispose: should never be called (not owned)\n");
    }

    void OwnedHandle_dispose(OwnedHandleRef _this) {
        delete THIS;
    }
}
