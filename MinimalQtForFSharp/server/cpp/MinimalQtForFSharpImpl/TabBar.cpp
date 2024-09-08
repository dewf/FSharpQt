#include "generated/TabBar.h"

#include <QObject>
#include <QTabBar>
#include <utility>

#include "util/SignalStuff.h"
#include "util/convert.h"
#include "IconInternal.h"

#define THIS ((TabBarWithHandler*)_this)

namespace TabBar
{
    class TabBarWithHandler : public QTabBar {
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
            // TabBar:
            { SignalMaskFlags::CurrentChanged, SIGNAL(currentChanged(int)), SLOT(onCurrentChanged(int)) },
            { SignalMaskFlags::TabBarClicked, SIGNAL(tabBarClicked(int)), SLOT(onTabBarClicked(int)) },
            { SignalMaskFlags::TabBarDoubleClicked, SIGNAL(tabBarDoubleClicked(int)), SLOT(onTabBarDoubleClicked(int)) },
            { SignalMaskFlags::TabCloseRequested, SIGNAL(tabCloseRequested(int)), SLOT(onTabCloseRequested(int)) },
            { SignalMaskFlags::TabMoved, SIGNAL(tabMoved(int,int)), SLOT(onTabMoved(int,int)) }
        };
    public:
        explicit TabBarWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // TabBar ==================
        void onCurrentChanged(int index) {
            handler->currentChanged(index);
        }
        void onTabBarClicked(int index) {
            handler->tabBarClicked(index);
        }
        void onTabBarDoubleClicked(int index) {
            handler->tabBarDoubleClicked(index);
        }
        void onTabCloseRequested(int index) {
            handler->tabCloseRequested(index);
        }
        void onTabMoved(int fromIndex, int toIndex) {
            handler->tabMoved(fromIndex, toIndex);
        }
    };

    void Handle_setAutoHide(HandleRef _this, bool value) {
        THIS->setAutoHide(value);
    }

    void Handle_setChangeCurrentOnDrag(HandleRef _this, bool value) {
        THIS->setChangeCurrentOnDrag(value);
    }

    int32_t Handle_count(HandleRef _this) {
        return THIS->count();
    }

    void Handle_setCurrentIndex(HandleRef _this, int32_t value) {
        THIS->setCurrentIndex(value);
    }

    int32_t Handle_currentIndex(HandleRef _this) {
        return THIS->currentIndex();
    }

    void Handle_setDocumentMode(HandleRef _this, bool value) {
        THIS->setDocumentMode(value);
    }

    void Handle_setDrawBase(HandleRef _this, bool value) {
        THIS->setDrawBase(value);
    }

    void Handle_setElideMode(HandleRef _this, TextElideMode mode) {
        THIS->setElideMode((Qt::TextElideMode)mode);
    }

    void Handle_setExpanding(HandleRef _this, bool value) {
        THIS->setExpanding(value);
    }

    void Handle_setIconSize(HandleRef _this, Size size) {
        THIS->setIconSize(toQSize(size));
    }

    void Handle_setMovable(HandleRef _this, bool value) {
        THIS->setMovable(value);
    }

    void Handle_setSelectionBehaviorOnRemove(HandleRef _this, SelectionBehavior value) {
        THIS->setSelectionBehaviorOnRemove((QTabBar::SelectionBehavior)value);
    }

    void Handle_setShape(HandleRef _this, Shape shape) {
        THIS->setShape((QTabBar::Shape)shape);
    }

    void Handle_setTabsClosable(HandleRef _this, bool value) {
        THIS->setTabsClosable(value);
    }

    void Handle_setUsesScrollButtons(HandleRef _this, bool value) {
        THIS->setUsesScrollButtons(value);
    }

    void Handle_removeAllTabs(HandleRef _this) {
        auto count = THIS->count();
        for (auto i=0; i< count; i++) {
            THIS->removeTab(0);
        }
    }

    int32_t Handle_addTab(HandleRef _this, std::string text) {
        return THIS->addTab(QString::fromStdString(text));
    }

    int32_t Handle_addTab(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon, std::string text) {
        return THIS->addTab(Icon::fromDeferred(icon), QString::fromStdString(text));
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new TabBarWithHandler(std::move(handler));
    }
}

#include "TabBar.moc"
