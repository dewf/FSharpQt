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
#include "AbstractProxyModel.h"
using namespace ::AbstractProxyModel;
#include "RegularExpression.h"
using namespace ::RegularExpression;
#include "ModelIndex.h"
using namespace ::ModelIndex;
#include "PersistentModelIndex.h"
using namespace ::PersistentModelIndex;

namespace SortFilterProxyModel
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractProxyModel::HandleRef
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
        // AbstractProxyModel::SignalMask:
        SourceModelChanged = 1 << 20,
        // SignalMask:
        AutoAcceptChildRowsChanged = 1 << 21,
        FilterCaseSensitivityChanged = 1 << 22,
        FilterRoleChanged = 1 << 23,
        RecursiveFilteringEnabledChanged = 1 << 24,
        SortCaseSensitivityChanged = 1 << 25,
        SortLocaleAwareChanged = 1 << 26,
        SortRoleChanged = 1 << 27
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
        virtual void sourceModelChanged() = 0;
        virtual void autoAcceptChildRowsChanged(bool autoAcceptChildRows) = 0;
        virtual void filterCaseSensitivityChanged(Enums::CaseSensitivity filterCaseSensitivity) = 0;
        virtual void filterRoleChanged(Enums::ItemDataRole filterRole) = 0;
        virtual void recursiveFilteringEnabledChanged(bool recursiveFilteringEnabled) = 0;
        virtual void sortCaseSensitivityChanged(Enums::CaseSensitivity sortCaseSensitivity) = 0;
        virtual void sortLocaleAwareChanged(bool sortLocaleAware) = 0;
        virtual void sortRoleChanged(Enums::ItemDataRole sortRole) = 0;
    };

    void Handle_setAutoAcceptChildRows(HandleRef _this, bool state);
    void Handle_setDynamicSortFilter(HandleRef _this, bool state);
    void Handle_setFilterCaseSensitivity(HandleRef _this, Enums::CaseSensitivity sensitivity);
    void Handle_setFilterKeyColumn(HandleRef _this, int32_t filterKeyColumn);
    void Handle_setFilterRegularExpression(HandleRef _this, std::shared_ptr<RegularExpression::Deferred::Base> regex);
    void Handle_setFilterRole(HandleRef _this, Enums::ItemDataRole filterRole);
    void Handle_setSortLocaleAware(HandleRef _this, bool state);
    void Handle_setRecursiveFilteringEnabled(HandleRef _this, bool enabled);
    void Handle_setSortCaseSensitivity(HandleRef _this, Enums::CaseSensitivity sensitivity);
    void Handle_setSortRole(HandleRef _this, Enums::ItemDataRole sortRole);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);

    void Interior_invalidateColumnsFilter(InteriorRef _this);
    void Interior_invalidateRowsFilter(InteriorRef _this);
    void Interior_invalidateFilter(InteriorRef _this);
    void Interior_dispose(InteriorRef _this);

    typedef int32_t MethodMask;
    enum MethodMaskFlags: int32_t {
        // MethodMask:
        FilterAcceptsColumn = 1 << 0,
        FilterAcceptsRow = 1 << 1,
        LessThan = 1 << 2
    };

    class MethodDelegate {
    public:
        virtual bool filterAcceptsColumn(int32_t sourceColumn, ModelIndex::HandleRef sourceParent) = 0;
        virtual bool filterAcceptsRow(int32_t sourceRow, ModelIndex::HandleRef sourceParent) = 0;
        virtual bool lessThan(ModelIndex::HandleRef sourceLeft, ModelIndex::HandleRef sourceRight) = 0;
    };
    InteriorRef createSubclassed(std::shared_ptr<SignalHandler> handler, std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask);
}
