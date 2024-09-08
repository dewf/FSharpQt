#include "generated/TabWidget.h"

#include <QObject>
#include <QTabWidget>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((TabWidgetWithHandler*)_this)

namespace TabWidget
{
    class TabWidgetWithHandler : public QTabWidget {
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
            // TabWidget:
            { SignalMaskFlags::CurrentChanged, SIGNAL(currentChanged(int)), SLOT(onCurrentChanged(int)) },
            { SignalMaskFlags::TabBarClicked, SIGNAL(tabBarClicked(int)), SLOT(onTabBarClicked(int)) },
            { SignalMaskFlags::TabBarDoubleClicked, SIGNAL(tabBarDoubleClicked(int)), SLOT(onTabBarDoubleClicked(int)) },
            { SignalMaskFlags::TabCloseRequested, SIGNAL(tabCloseRequested(int)), SLOT(onTabCloseRequested(int)) }
        };
    public:
        explicit TabWidgetWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // TabWidget ===============
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
    };

    int32_t Handle_count(HandleRef _this) {
        return THIS->count();
    }

    void Handle_setCurrentIndex(HandleRef _this, int32_t index) {
        THIS->setCurrentIndex(index);
    }

    void Handle_setDocumentMode(HandleRef _this, bool state) {
        THIS->setDocumentMode(state);
    }

    void Handle_setElideMode(HandleRef _this, TextElideMode mode) {
        THIS->setElideMode((Qt::TextElideMode)mode);
    }

    void Handle_setIconSize(HandleRef _this, Size size) {
        THIS->setIconSize(toQSize(size));
    }

    void Handle_setMovable(HandleRef _this, bool state) {
        THIS->setMovable(state);
    }

    void Handle_setTabBarAutoHide(HandleRef _this, bool state) {
        THIS->setTabBarAutoHide(state);
    }

    void Handle_setTabPosition(HandleRef _this, TabPosition position) {
        THIS->setTabPosition((QTabWidget::TabPosition)position);
    }

    void Handle_setTabShape(HandleRef _this, TabShape shape) {
        THIS->setTabShape((QTabWidget::TabShape)shape);
    }

    void Handle_setTabsClosable(HandleRef _this, bool state) {
        THIS->setTabsClosable(state);
    }

    void Handle_setUsesScrollButtons(HandleRef _this, bool state) {
        THIS->setUsesScrollButtons(state);
    }

    void Handle_addTab(HandleRef _this, Widget::HandleRef page, std::string label) {
        THIS->addTab((QWidget*)page, QString::fromStdString(label));
    }

    void Handle_insertTab(HandleRef _this, int32_t index, Widget::HandleRef page, std::string label) {
        THIS->insertTab(index, (QWidget*)page, QString::fromStdString(label));
    }

    Widget::HandleRef Handle_widgetAt(HandleRef _this, int32_t index) {
        return (Widget::HandleRef)THIS->widget(index);
    }

    void Handle_clear(HandleRef _this) {
        THIS->clear();
    }

    void Handle_removeTab(HandleRef _this, int32_t index) {
        THIS->removeTab(index);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new TabWidgetWithHandler(std::move(handler));
    }
}

#include "TabWidget.moc"
