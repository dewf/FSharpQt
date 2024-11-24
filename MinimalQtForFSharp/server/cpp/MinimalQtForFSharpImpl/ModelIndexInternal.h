#pragma once

#include "generated/ModelIndex.h"
#include <QModelIndex>

// from a ModelIndex::Handle to a QModelIndex*
#define AS_MODELINDEX_PTR(x) (reinterpret_cast<QModelIndex*>(x))

// from a ModelIndex::Handle to a QModelIndex value
#define MODELINDEX_VALUE(x) (*AS_MODELINDEX_PTR(x))

// create an owned heap-allocated copy of an existing QModelIndex value
#define MODELINDEX_HEAP_COPY(x) (reinterpret_cast<ModelIndex::OwnedRef>(new QModelIndex(x)))

namespace ModelIndex {
    // as of now we're just wrapping raw QModelIndex pointers
    // because it's easier just to send pointers back to the C# side for callbacks etc
    // saves unnecessary copying in places like AbstractListModel
}
