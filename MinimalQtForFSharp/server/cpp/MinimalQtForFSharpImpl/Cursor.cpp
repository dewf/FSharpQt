#include "generated/Cursor.h"

#include <QCursor>
#include "CursorInternal.h"

#define THIS ((QCursor*)_this)

namespace Cursor
{
    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }
}
