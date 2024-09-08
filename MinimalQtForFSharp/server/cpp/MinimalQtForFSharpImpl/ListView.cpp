#include "generated/ListView.h"

#include <QListView>
#include <utility>
#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((ListViewWithHandler*)_this)

namespace ListView
{
    class ListViewWithHandler : public QListView {
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
            // ListView:
            { SignalMaskFlags::IndexesMoved, SIGNAL(indexesMoved(QModelIndexList)), SLOT(onIndexesMoved(QModelIndexList)) },
        };
    public:
        explicit ListViewWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // ListView ========================
        void onIndexesMoved(const QModelIndexList &indexes) {
            std::vector<ModelIndex::HandleRef> indexes2;
            for (auto &index : indexes) {
                // will this work? seems sketchy
                indexes2.push_back((ModelIndex::HandleRef)&index);
            }
            handler->indexesMoved(indexes2);
        }
    };

    void Handle_setBatchSize(HandleRef _this, int32_t size) {
        THIS->setBatchSize(size);
    }

    void Handle_setFlow(HandleRef _this, Flow flow) {
        THIS->setFlow((QListView::Flow)flow);
    }

    void Handle_setGridSize(HandleRef _this, Size size) {
        THIS->setGridSize(toQSize(size));
    }

    void Handle_setWrapping(HandleRef _this, bool wrapping) {
        THIS->setWrapping(wrapping);
    }

    void Handle_setItemAlignment(HandleRef _this, Alignment align) {
        THIS->setItemAlignment((Qt::Alignment)align);
    }

    void Handle_setLayoutMode(HandleRef _this, LayoutMode mode) {
        THIS->setLayoutMode((QListView::LayoutMode)mode);
    }

    void Handle_setModelColumn(HandleRef _this, int32_t column) {
        THIS->setModelColumn(column);
    }

    void Handle_setMovement(HandleRef _this, Movement movement) {
        THIS->setMovement((QListView::Movement)movement);
    }

    void Handle_setResizeMode(HandleRef _this, ResizeMode mode) {
        THIS->setResizeMode((QListView::ResizeMode)mode);
    }

    void Handle_setSelectionRectVisible(HandleRef _this, bool visible) {
        THIS->setSelectionRectVisible(visible);
    }

    void Handle_setSpacing(HandleRef _this, int32_t spacing) {
        THIS->setSpacing(spacing);
    }

    void Handle_setUniformItemSizes(HandleRef _this, bool state) {
        THIS->setUniformItemSizes(state);
    }

    void Handle_setViewMode(HandleRef _this, ViewMode mode) {
        THIS->setViewMode((QListView::ViewMode)mode);
    }

    void Handle_setWordWrap(HandleRef _this, bool state) {
        THIS->setWordWrap(state);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new ListViewWithHandler(std::move(handler));
    }
}

#include "ListView.moc"
