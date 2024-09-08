#include "generated/SortFilterProxyModel.h"

#include <QSortFilterProxyModel>
#include <utility>
#include "util/SignalStuff.h"
#include "util/Macros.h"

#include "RegularExpressionInternal.h"

#define THIS ((SortFilterProxyModelWithHandler*)_this)

namespace SortFilterProxyModel
{
    class SortFilterProxyModelWithHandler : public QSortFilterProxyModel {
        Q_OBJECT
    private:
        std::shared_ptr<SignalHandler> handler;
        SignalMask lastMask = 0;
        std::vector<SignalMapItem<SignalMaskFlags>> signalMap = {
            // Object:
            { SignalMaskFlags::Destroyed, SIGNAL(destroyed(QObject)), SLOT(onDestroyed(QObject)) },
            { SignalMaskFlags::ObjectNameChanged, SIGNAL(objectNameChanged(QString)), SLOT(onObjectNameChanged(QString)) },
            // AbstractItemModel:
            { SignalMaskFlags::ColumnsAboutToBeInserted, SIGNAL(columnsAboutToBeInserted(QModelIndex,int,int)), SLOT(onColumnsAboutToBeInserted(QModelIndex,int,int)) },
            { SignalMaskFlags::ColumnsAboutToBeMoved, SIGNAL(columnsAboutToBeMoved(QModelIndex,int,QModelIndex,int)), SLOT(onColumnsAboutToBeMoved(QModelIndex,int,QModelIndex,int)) },
            { SignalMaskFlags::ColumnsAboutToBeRemoved, SIGNAL(columnsAboutToBeRemoved(QModelIndex,int,int)), SLOT(onColumnsAboutToBeRemoved(QModelIndex,int,int)) },
            { SignalMaskFlags::ColumnsInserted, SIGNAL(columnsInserted(QModelIndex,int,int)), SLOT(onColumnsInserted(QModelIndex,int,int)) },
            { SignalMaskFlags::ColumnsMoved, SIGNAL(columnsMoved(QModelIndex,int,int,QModelIndex,int)), SLOT(onColumnsMoved(QModelIndex,int,int,QModelIndex,int)) },
            { SignalMaskFlags::ColumnsRemoved, SIGNAL(columnsRemoved(QModelIndex,int,int)), SLOT(onColumnsRemoved(QModelIndex,int,int)) },
            { SignalMaskFlags::DataChanged, SIGNAL(dataChanged(QModelIndex,QModelIndex,QList<int>)), SLOT(onDataChanged(QModelIndex,QModelIndex,QList<int>)) },
            { SignalMaskFlags::HeaderDataChanged, SIGNAL(headerDataChanged(Orientation,int,int)), SLOT(onHeaderDataChanged(Orientation,int,int)) },
            { SignalMaskFlags::LayoutAboutToBeChanged, SIGNAL(layoutAboutToBeChanged(QList<QPersistentModelIndex>,LayoutChangeHint)), SLOT(onLayoutAboutToBeChanged(QList<QPersistentModelIndex>,LayoutChangeHint)) },
            { SignalMaskFlags::LayoutChanged, SIGNAL(layoutChanged(QList<QPersistentModelIndex>,LayoutChangeHint)), SLOT(onLayoutChanged(QList<QPersistentModelIndex>,LayoutChangeHint)) },
            { SignalMaskFlags::ModelAboutToBeReset, SIGNAL(modelAboutToBeReset()), SLOT(onModelAboutToBeReset()) },
            { SignalMaskFlags::ModelReset, SIGNAL(modelReset()), SLOT(onModelReset()) },
            { SignalMaskFlags::RowsAboutToBeInserted, SIGNAL(rowsAboutToBeInserted(QModelIndex,int,int)), SLOT(onRowsAboutToBeInserted(QModelIndex,int,int)) },
            { SignalMaskFlags::RowsAboutToBeMoved, SIGNAL(rowsAboutToBeMoved(ModelIndex,int,int,ModelIndex,int)), SLOT(onRowsAboutToBeMoved(ModelIndex,int,int,ModelIndex,int)) },
            { SignalMaskFlags::RowsAboutToBeRemoved, SIGNAL(rowsAboutToBeRemoved(QModelIndex,int,int)), SLOT(onRowsAboutToBeRemoved(QModelIndex,int,int)) },
            { SignalMaskFlags::RowsInserted, SIGNAL(rowsInserted(QModelIndex,int,int)), SLOT(onRowsInserted(QModelIndex,int,int)) },
            { SignalMaskFlags::RowsMoved, SIGNAL(rowsMoved(QModelIndex,int,int,QModelIndex,int)), SLOT(onRowsMoved(QModelIndex,int,int,QModelIndex,int)) },
            { SignalMaskFlags::RowsRemoved, SIGNAL(rowsRemoved(QModelIndex,int,int)), SLOT(onRowsRemoved(QModelIndex,int,int)) },
            // AbstractProxyModel:
            { SignalMaskFlags::SourceModelChanged, SIGNAL(sourceModelChanged()), SLOT(onSourceModelChanged()) },
            // SortFilterProxyModel:
            { SignalMaskFlags::AutoAcceptChildRowsChanged, SIGNAL(autoAcceptChildRowsChanged(bool)), SLOT(onAutoAcceptChildRowsChanged(bool)) },
            { SignalMaskFlags::FilterCaseSensitivityChanged, SIGNAL(filterCaseSensitivityChanged(CaseSensitivity)), SLOT(onFilterCaseSensitivityChanged(CaseSensitivity)) },
            { SignalMaskFlags::FilterRoleChanged, SIGNAL(filterRoleChanged(int)), SLOT(onFilterRoleChanged(int)) },
            { SignalMaskFlags::RecursiveFilteringEnabledChanged, SIGNAL(recursiveFilteringEnabledChanged(bool)), SLOT(onRecursiveFilteringEnabledChanged(bool)) },
            { SignalMaskFlags::SortCaseSensitivityChanged, SIGNAL(sortCaseSensitivityChanged(CaseSensitivity)), SLOT(onSortCaseSensitivityChanged(CaseSensitivity)) },
            { SignalMaskFlags::SortLocaleAwareChanged, SIGNAL(sortLocaleAwareChanged(bool)), SLOT(onSortLocaleAwareChanged(bool)) },
            { SignalMaskFlags::SortRoleChanged, SIGNAL(sortRoleChanged(int)), SLOT(onSortRoleChanged(int)) },
        };
    public:
        explicit SortFilterProxyModelWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
        void setSignalMask(SignalMask newMask) {
            if (newMask != lastMask) {
                processChanges(lastMask, newMask, signalMap, this);
                lastMask = newMask;
            }
        }
    public slots:
        // Object =================
        void onDestroyed(QObject *obj) {
            handler->destroyed((Object::HandleRef)obj);
        }
        void onObjectNameChanged(const QString& name) {
            handler->objectNameChanged(name.toStdString());
        }
        // AbstractItemModel ======
        void onColumnsAboutToBeInserted(const QModelIndex& parent, int first, int last) {
            handler->columnsAboutToBeInserted(MODELINDEX(parent), first, last);
        };
        void onColumnsAboutToBeMoved(const QModelIndex& sourceParent, int sourceStart, int sourceEnd, const QModelIndex& destinationParent, int destinationColumn) {
            handler->columnsAboutToBeMoved(MODELINDEX(sourceParent), sourceStart, sourceEnd, MODELINDEX(destinationParent), destinationColumn);
        };
        void onColumnsAboutToBeRemoved(const QModelIndex& parent, int first, int last) {
            handler->columnsAboutToBeRemoved(MODELINDEX(parent), first, last);
        };
        void onColumnsInserted(const QModelIndex& parent, int first, int last) {
            handler->columnsInserted(MODELINDEX(parent), first, last);
        };
        void onColumnsMoved(const QModelIndex& sourceParent, int sourceStart, int sourceEnd, const QModelIndex& destinationParent, int destinationColumn) {
            handler->columnsMoved(MODELINDEX(sourceParent), sourceStart, sourceEnd, MODELINDEX(destinationParent), destinationColumn);
        };
        void onColumnsRemoved(const QModelIndex& parent, int first, int last) {
            handler->columnsRemoved(MODELINDEX(parent), first, last);
        };
        void onDataChanged(const QModelIndex &topLeft, const QModelIndex &bottomRight, const QList<int> &roles = QList<int>()) {
            std::vector<ItemDataRole> roles2;
            for (auto &role : roles) {
                roles2.push_back((ItemDataRole)role);
            }
            handler->dataChanged(MODELINDEX(topLeft), MODELINDEX(bottomRight), roles2);
        }
        void onHeaderDataChanged(Qt::Orientation orientation, int first, int last) {
            handler->headerDataChanged((Enums::Orientation)orientation, first, last);
        };
        void onLayoutAboutToBeChanged(const QList<QPersistentModelIndex>& parents, QAbstractItemModel::LayoutChangeHint hint) {
            std::vector<PersistentModelIndex::HandleRef> parents2;
            for (auto &parent : parents) {
                parents2.push_back(PMODELINDEX(parent));
            }
            handler->layoutAboutToBeChanged(parents2, (AbstractItemModel::LayoutChangeHint)hint);
        };
        void onLayoutChanged(const QList<QPersistentModelIndex>& parents, QAbstractItemModel::LayoutChangeHint hint) {
            std::vector<PersistentModelIndex::HandleRef> parents2;
            for (auto &parent : parents) {
                parents2.push_back(PMODELINDEX(parent));
            }
            handler->layoutChanged(parents2, (AbstractItemModel::LayoutChangeHint)hint);
        };
        void onModelAboutToBeReset() {
            handler->modelAboutToBeReset();
        };
        void onModelReset() {
            handler->modelReset();
        };
        void onRowsAboutToBeInserted(const QModelIndex& parent, int start, int end) {
            handler->rowsAboutToBeInserted(MODELINDEX(parent), start, end);
        };
        void onRowsAboutToBeMoved(const QModelIndex& sourceParent, int sourceStart, int sourceEnd, const QModelIndex& destinationParent, int destinationRow) {
            handler->rowsAboutToBeMoved(MODELINDEX(sourceParent), sourceStart, sourceEnd, MODELINDEX(destinationParent), destinationRow);
        };
        void onRowsAboutToBeRemoved(const QModelIndex& parent, int first, int last) {
            handler->rowsAboutToBeRemoved(MODELINDEX(parent), first, last);
        };
        void onRowsInserted(const QModelIndex& parent, int first, int last) {
            handler->rowsInserted(MODELINDEX(parent), first, last);
        };
        void onRowsMoved(const QModelIndex& sourceParent, int sourceStart, int sourceEnd, const QModelIndex& destinationParent, int destinationRow) {
            handler->rowsMoved(MODELINDEX(sourceParent), sourceStart, sourceEnd, MODELINDEX(destinationParent), destinationRow);
        };
        void onRowsRemoved(const QModelIndex& parent, int first, int last) {
            handler->rowsRemoved(MODELINDEX(parent), first, last);
        };
        // AbstractProxyModel =====
        void onSourceModelChanged() {
            handler->sourceModelChanged();
        };
        // SortFilterProxyModel ===
        void onAutoAcceptChildRowsChanged(bool autoAcceptChildRows) {
            handler->autoAcceptChildRowsChanged(autoAcceptChildRows);
        };
        void onFilterCaseSensitivityChanged(Qt::CaseSensitivity filterCaseSensitivity) {
            handler->filterCaseSensitivityChanged((Enums::CaseSensitivity)filterCaseSensitivity);
        };
        void onFilterRoleChanged(int filterRole) {
            handler->filterRoleChanged((ItemDataRole)filterRole);
        };
        void onRecursiveFilteringEnabledChanged(bool recursiveFilteringEnabled) {
            handler->recursiveFilteringEnabledChanged(recursiveFilteringEnabled);
        };
        void onSortCaseSensitivityChanged(Qt::CaseSensitivity sortCaseSensitivity) {
            handler->sortCaseSensitivityChanged((Enums::CaseSensitivity)sortCaseSensitivity);
        };
        void onSortLocaleAwareChanged(bool sortLocaleAware) {
            handler->sortLocaleAwareChanged(sortLocaleAware);
        };
        void onSortRoleChanged(int sortRole) {
            handler->sortRoleChanged((ItemDataRole)sortRole);
        };
    };

    void Handle_setAutoAcceptChildRows(HandleRef _this, bool state) {
        THIS->setAutoAcceptChildRows(state);
    }

    void Handle_setDynamicSortFilter(HandleRef _this, bool state) {
        THIS->setDynamicSortFilter(state);
    }

    void Handle_setFilterCaseSensitivity(HandleRef _this, CaseSensitivity sensitivity) {
        THIS->setFilterCaseSensitivity((Qt::CaseSensitivity)sensitivity);
    }

    void Handle_setFilterKeyColumn(HandleRef _this, int32_t filterKeyColumn) {
        THIS->setFilterKeyColumn(filterKeyColumn);
    }

    void Handle_setFilterRegularExpression(HandleRef _this, std::shared_ptr<RegularExpression::Deferred::Base> regex) {
        THIS->setFilterRegularExpression(RegularExpression::fromDeferred(regex));
    }

    void Handle_setFilterRole(HandleRef _this, ItemDataRole filterRole) {
        THIS->setFilterRole((int)filterRole);
    }

    void Handle_setSortLocaleAware(HandleRef _this, bool state) {
        THIS->setSortLocaleAware(state);
    }

    void Handle_setRecursiveFilteringEnabled(HandleRef _this, bool enabled) {
        THIS->setRecursiveFilteringEnabled(enabled);
    }

    void Handle_setSortCaseSensitivity(HandleRef _this, CaseSensitivity sensitivity) {
        THIS->setSortCaseSensitivity((Qt::CaseSensitivity)sensitivity);
    }

    void Handle_setSortRole(HandleRef _this, ItemDataRole sortRole) {
        THIS->setSortRole((int)sortRole);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new SortFilterProxyModelWithHandler(std::move(handler));
    }

    // subclass stuff ==================================================

#define SUBTHIS ((Subclassed*)_this)

    class Subclassed : public SortFilterProxyModelWithHandler {
    private:
        MethodMask methodMask;
        std::shared_ptr<MethodDelegate> methodDelegate;
    public:
        Subclassed(const std::shared_ptr<SignalHandler> &handler, const std::shared_ptr<MethodDelegate> &methodDelegate, MethodMask methodMask)
            : SortFilterProxyModelWithHandler(handler), methodMask(methodMask), methodDelegate(methodDelegate) {}
    protected:
        [[nodiscard]] bool filterAcceptsRow(int source_row, const QModelIndex &source_parent) const override {
            if (methodMask & MethodMaskFlags::FilterAcceptsRow) {
                return methodDelegate->filterAcceptsRow(source_row, MODELINDEX(source_parent));
            } else {
                return QSortFilterProxyModel::filterAcceptsRow(source_row, source_parent);
            }
        }
        [[nodiscard]] bool filterAcceptsColumn(int source_column, const QModelIndex &source_parent) const override {
            if (methodMask & MethodMaskFlags::FilterAcceptsColumn) {
                return methodDelegate->filterAcceptsColumn(source_column,MODELINDEX(source_parent));
            } else {
                return QSortFilterProxyModel::filterAcceptsColumn(source_column, source_parent);
            }
        }
        [[nodiscard]] bool lessThan(const QModelIndex &source_left, const QModelIndex &source_right) const override {
            if (methodMask & MethodMaskFlags::LessThan) {
                return methodDelegate->lessThan(MODELINDEX(source_left), MODELINDEX(source_right));
            } else {
                return QSortFilterProxyModel::lessThan(source_left, source_right);
            }
        }
    public:
        friend void Interior_invalidateColumnsFilter(InteriorRef _this);
        friend void Interior_invalidateRowsFilter(InteriorRef _this);
        friend void Interior_invalidateFilter(InteriorRef _this);
    };

    void Interior_invalidateColumnsFilter(InteriorRef _this) {
        SUBTHIS->invalidateColumnsFilter();
    }

    void Interior_invalidateRowsFilter(InteriorRef _this) {
        SUBTHIS->invalidateRowsFilter();
    }

    void Interior_invalidateFilter(InteriorRef _this) {
        SUBTHIS->invalidateFilter();
    }

    void Interior_dispose(InteriorRef _this) {
        delete THIS;
    }

    InteriorRef createSubclassed(std::shared_ptr<SignalHandler> handler, std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask) {
        return (InteriorRef) new Subclassed(handler, methodDelegate, methodMask);
    }

}

#include "SortFilterProxyModel.moc"
