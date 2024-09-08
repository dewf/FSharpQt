#include "generated/Object.h"

#include <QObject>

#define THIS ((QObject*)_this)

namespace Object
{
    void Handle_setObjectName(HandleRef _this, std::string name) {
        THIS->setObjectName(QString::fromStdString(name));
    }

    void Handle_dumpObjectTree(HandleRef _this) {
        THIS->dumpObjectTree();
    }

    void Handle_dispose(HandleRef _this) {
        printf("Object.Handle_dispose() called - shouldn't happen\n");
    }
}
