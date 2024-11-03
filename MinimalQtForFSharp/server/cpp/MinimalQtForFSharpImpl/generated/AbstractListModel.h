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
#include "AbstractItemModel.h"
using namespace ::AbstractItemModel;
#include "Variant.h"
using namespace ::Variant;
#include "ModelIndex.h"
using namespace ::ModelIndex;
#include "PersistentModelIndex.h"
using namespace ::PersistentModelIndex;

namespace AbstractListModel
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractItemModel::HandleRef
    struct __Interior; typedef struct __Interior* InteriorRef; // extends HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // AbstractItemModel::SignalMask:
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
        RowsRemoved = 1 << 19,
        // SignalMask:
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
        virtual void dataChanged(ModelIndex::HandleRef topLeft, ModelIndex::HandleRef bottomRight, std::vector<Enums::ItemDataRole> roles) = 0;
        virtual void headerDataChanged(Enums::Orientation orientation, int32_t first, int32_t last) = 0;
        virtual void layoutAboutToBeChanged(std::vector<PersistentModelIndex::HandleRef> parents, AbstractItemModel::LayoutChangeHint hint) = 0;
        virtual void layoutChanged(std::vector<PersistentModelIndex::HandleRef> parents, AbstractItemModel::LayoutChangeHint hint) = 0;
        virtual void modelAboutToBeReset() = 0;
        virtual void modelReset() = 0;
        virtual void rowsAboutToBeInserted(ModelIndex::HandleRef parent, int32_t start, int32_t end) = 0;
        virtual void rowsAboutToBeMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationRow) = 0;
        virtual void rowsAboutToBeRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void rowsInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
        virtual void rowsMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationRow) = 0;
        virtual void rowsRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) = 0;
    };

    void Handle_setSignalMask(HandleRef _this, SignalMask mask);

    void Interior_emitDataChanged(InteriorRef _this, std::shared_ptr<ModelIndex::Deferred::Base> topLeft, std::shared_ptr<ModelIndex::Deferred::Base> bottomRight, std::vector<Enums::ItemDataRole> roles);
    void Interior_emitHeaderDataChanged(InteriorRef _this, Enums::Orientation orientation, int32_t first, int32_t last);
    void Interior_beginInsertRows(InteriorRef _this, std::shared_ptr<ModelIndex::Deferred::Base> parent, int32_t first, int32_t last);
    void Interior_endInsertRows(InteriorRef _this);
    void Interior_beginRemoveRows(InteriorRef _this, std::shared_ptr<ModelIndex::Deferred::Base> parent, int32_t first, int32_t last);
    void Interior_endRemoveRows(InteriorRef _this);
    void Interior_beginResetModel(InteriorRef _this);
    void Interior_endResetModel(InteriorRef _this);
    void Interior_dispose(InteriorRef _this);

    typedef int32_t ItemFlags;
    enum ItemFlagsFlags : int32_t {
        NoItemFlags = 0,
        ItemIsSelectable = 1,
        ItemIsEditable = 2,
        ItemIsDragEnabled = 4,
        ItemIsDropEnabled = 8,
        ItemIsUserCheckable = 16,
        ItemIsEnabled = 32,
        ItemIsAutoTristate = 64,
        ItemNeverHasChildren = 128,
        ItemIsUserTristate = 256
    };

    typedef int32_t MethodMask;
    enum MethodMaskFlags : int32_t {
        HeaderData = 1,
        Flags = 1 << 1,
        SetData = 1 << 2,
        ColumnCount = 1 << 3
    };

    class MethodDelegate {
    public:
        virtual int32_t rowCount(ModelIndex::HandleRef parent) = 0;
        virtual std::shared_ptr<Variant::Deferred::Base> data(ModelIndex::HandleRef index, Enums::ItemDataRole role) = 0;
        virtual std::shared_ptr<Variant::Deferred::Base> headerData(int32_t section, Enums::Orientation orientation, Enums::ItemDataRole role) = 0;
        virtual ItemFlags getFlags(ModelIndex::HandleRef index, ItemFlags baseFlags) = 0;
        virtual bool setData(ModelIndex::HandleRef index, Variant::HandleRef value, Enums::ItemDataRole role) = 0;
        virtual int32_t columnCount(ModelIndex::HandleRef parent) = 0;
    };
    InteriorRef createSubclassed(std::shared_ptr<SignalHandler> handler, std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask);
}
