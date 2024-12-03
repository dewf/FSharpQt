#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

#include "Object.h"
using namespace ::Object;
#include "Enums.h"
using namespace ::Enums;
#include "ModelIndex.h"
using namespace ::ModelIndex;
#include "PersistentModelIndex.h"
using namespace ::PersistentModelIndex;
#include "Variant.h"
using namespace ::Variant;

namespace AbstractItemModel
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Object::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // SignalMask:
        ColumnsAboutToBeInserted = 1 << 2,
        ColumnsAboutToBeMoved = 1 << 3,
        ColumnsAboutToBeRemoved = 1 << 4,
        ColumnsInserted = 1 << 5,
        ColumnsMoved = 1 << 6,
        ColumnsRemoved = 1 << 7,
        DataChanged = 1 << 8,
        HeaderDataChanged = 1 << 9,
        LayoutAboutToBeChanged = 1 << 10,
        LayoutChanged = 1 << 11,
        ModelAboutToBeReset = 1 << 12,
        ModelReset = 1 << 13,
        RowsAboutToBeInserted = 1 << 14,
        RowsAboutToBeMoved = 1 << 15,
        RowsAboutToBeRemoved = 1 << 16,
        RowsInserted = 1 << 17,
        RowsMoved = 1 << 18,
        RowsRemoved = 1 << 19
    };

    enum class LayoutChangeHint {
        NoLayoutChangeHint,
        VerticalSortHint,
        HorizontalSortHint
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void columnsAboutToBeInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void columnsAboutToBeMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationColumn) = 0;
        virtual void columnsAboutToBeRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void columnsInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void columnsMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationColumn) = 0;
        virtual void columnsRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void dataChanged(ModelIndex::HandleRef topLeft, ModelIndex::HandleRef bottomRight, std::vector<ItemDataRole> roles) = 0;
        virtual void headerDataChanged(Orientation orientation, int32_t first, int32_t last) = 0;
        virtual void layoutAboutToBeChanged(std::vector<PersistentModelIndex::HandleRef> parents, LayoutChangeHint hint) = 0;
        virtual void layoutChanged(std::vector<PersistentModelIndex::HandleRef> parents, LayoutChangeHint hint) = 0;
        virtual void modelAboutToBeReset() = 0;
        virtual void modelReset() = 0;
        virtual void rowsAboutToBeInserted(ModelIndex::HandleRef parent, int32_t start, int32_t end) = 0;
        virtual void rowsAboutToBeMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationRow) = 0;
        virtual void rowsAboutToBeRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void rowsInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void rowsMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationRow) = 0;
        virtual void rowsRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
    };

    ModelIndex::OwnedRef Handle_index(HandleRef _this, int32_t row, int32_t column);
    ModelIndex::OwnedRef Handle_index(HandleRef _this, int32_t row, int32_t column, ModelIndex::HandleRef parent);
    bool Handle_setData(HandleRef _this, ModelIndex::HandleRef index, std::shared_ptr<Variant::Deferred::Base> value);
    bool Handle_setData(HandleRef _this, ModelIndex::HandleRef index, std::shared_ptr<Variant::Deferred::Base> value, ItemDataRole role);
    Variant::OwnedRef Handle_data(HandleRef _this, ModelIndex::HandleRef index);
    Variant::OwnedRef Handle_data(HandleRef _this, ModelIndex::HandleRef index, ItemDataRole role);
    void Handle_sort(HandleRef _this, int32_t column);
    void Handle_sort(HandleRef _this, int32_t column, SortOrder order);
}
