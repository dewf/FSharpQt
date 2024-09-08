#include "generated/TreeView.h"

#include <QTreeView>
#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((TreeViewWithHandler*)_this)

namespace TreeView
{
    class TreeViewWithHandler : public QTreeView {
        Q_OBJECT
    private:
        std::shared_ptr<SignalHandler> handler;
        SignalMask lastMask = 0;
        std::vector<SignalMapItem<SignalMaskFlags>> signalMap = {
            // Object:
            { SignalMaskFlags::Destroyed, SIGNAL(destroyed(QObject)), SLOT(onDestroyed(QObject)) },
            { SignalMaskFlags::ObjectNameChanged, SIGNAL(objectNameChanged(QString)), SLOT(onObjectNameChanged(QString)) },
            // Widget:
            { SignalMaskFlags::CustomContextMenuRequested, SIGNAL(customContextMenuRequested(QPoint)), SLOT(onCustomContextMenuRequested(QPoint)) },
            { SignalMaskFlags::WindowIconChanged, SIGNAL(windowIconChanged(QIcon)), SLOT(onWindowIconChanged(QIcon)) },
            { SignalMaskFlags::WindowTitleChanged, SIGNAL(windowTitleChanged(QString)), SLOT(onWindowTitleChanged(QString)) },
            // Frame:
            // ..... (none)
            // AbstractScrollArea:
            // ..... (none)
            // AbstractItemView:
            { SignalMaskFlags::Activated, SIGNAL(activated(QModelIndex)), SLOT(onActivated(QModelIndex)) },
            { SignalMaskFlags::Clicked, SIGNAL(clicked(QModelIndex)), SLOT(onClicked(QModelIndex)) },
            { SignalMaskFlags::DoubleClickedBit, SIGNAL(doubleClicked(QModelIndex)), SLOT(onDoubleClicked(QModelIndex)) },
            { SignalMaskFlags::Entered, SIGNAL(entered(QModelIndex)), SLOT(onEntered(QModelIndex)) },
            { SignalMaskFlags::IconSizeChanged, SIGNAL(iconSizeChanged(QSize)), SLOT(onIconSizeChanged(QSize)) },
            { SignalMaskFlags::Pressed, SIGNAL(pressed(QModelIndex)), SLOT(onPressed(QModelIndex)) },
            { SignalMaskFlags::ViewportEntered, SIGNAL(viewportEntered()), SLOT(onViewportEntered) },
            // TreeView:
            { SignalMaskFlags::Collapsed, SIGNAL(collapsed(QModelIndex)), SLOT(onCollapsed(QModelIndex)) },
            { SignalMaskFlags::Expanded, SIGNAL(expanded(QModelIndex)), SLOT(onExpanded(QModelIndex)) },
        };
    public:
        explicit TreeViewWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // Widget ==================
        void onCustomContextMenuRequested(const QPoint& pos) {
            handler->customContextMenuRequested(toPoint(pos));
        }
        void onWindowIconChanged(const QIcon& icon) {
            handler->windowIconChanged((Icon::HandleRef)&icon);
        }
        void onWindowTitleChanged(const QString& title) {
            handler->windowTitleChanged(title.toStdString());
        }
        // Frame ====================
        // .... (none)

        // AbstractScrollArea =======
        // .... (none)

        // AbstractItemView =========
        void onActivated(const QModelIndex& index) {
            handler->activated((ModelIndex::HandleRef)&index);
        }
        void onClicked(const QModelIndex& index) {
            handler->clicked((ModelIndex::HandleRef)&index);
        }
        void onDoubleClicked(const QModelIndex& index) {
            handler->doubleClicked((ModelIndex::HandleRef)&index);
        }
        void onEntered(QModelIndex& index) {
            handler->entered((ModelIndex::HandleRef)&index);
        }
        void onIconSizeChanged(const QSize& size) {
            handler->iconSizeChanged(toSize(size));
        }
        void onPressed(const QModelIndex& index) {
            handler->pressed((ModelIndex::HandleRef)&index);
        }
        void onViewportEntered() {
            handler->viewportEntered();
        }
        // TreeView =================
        void onCollapsed(const QModelIndex& index) {
            handler->collapsed((ModelIndex::HandleRef)&index);
        }
        void onExpanded(const QModelIndex& index) {
            handler->expanded((ModelIndex::HandleRef)&index);
        }
    };

    void Handle_setAllColumnsShowFocus(HandleRef _this, bool value) {
        THIS->setAllColumnsShowFocus(value);
    }

    void Handle_setAnimated(HandleRef _this, bool value) {
        THIS->setAnimated(value);
    }

    void Handle_setAutoExpandDelay(HandleRef _this, int32_t value) {
        THIS->setAutoExpandDelay(value);
    }

    void Handle_setExpandsOnDoubleClick(HandleRef _this, bool value) {
        THIS->setExpandsOnDoubleClick(value);
    }

    void Handle_setHeaderHidden(HandleRef _this, bool value) {
        THIS->setHeaderHidden(value);
    }

    void Handle_setIndentation(HandleRef _this, int32_t value) {
        THIS->setIndentation(value);
    }

    void Handle_setItemsExpandable(HandleRef _this, bool value) {
        THIS->setItemsExpandable(value);
    }

    void Handle_setRootIsDecorated(HandleRef _this, bool value) {
        THIS->setRootIsDecorated(value);
    }

    void Handle_setSortingEnabled(HandleRef _this, bool value) {
        THIS->setSortingEnabled(value);
    }

    void Handle_setUniformRowHeights(HandleRef _this, bool value) {
        THIS->setUniformRowHeights(value);
    }

    void Handle_setWordWrap(HandleRef _this, bool value) {
        THIS->setWordWrap(value);
    }

    void Handle_resizeColumnToContents(HandleRef _this, int32_t column) {
        THIS->resizeColumnToContents(column);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new TreeViewWithHandler(std::move(handler));
    }
}

#include "TreeView.moc"
