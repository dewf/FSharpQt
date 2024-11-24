#pragma once

#include <QIcon>

// from a Icon::Handle to a QModelIndex*
#define AS_ICON_PTR(x) (reinterpret_cast<QIcon*>(x))

// from a Icon::Handle to a Icon value
#define ICON_VALUE(x) (*AS_ICON_PTR(x))

// create an owned heap-allocated copy of an existing QIcon value
#define ICON_HEAP_COPY(x) (reinterpret_cast<OwnedRef>(new QIcon(x)))

namespace Icon {
    // no inner structure, we are just casting QIcon* directly
}
