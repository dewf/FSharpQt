#include "generated/Menu.h"

#include <QObject>
#include <QMenu>

#include "util/convert.h"
#include "util/SignalStuff.h"
#include "IconInternal.h"

#define THIS ((MenuWithHandler*)_this)

namespace Menu
{
    class MenuWithHandler : public QMenu {
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
            // Menu:
            { SignalMaskFlags::AboutToHide, SIGNAL(aboutToHide()), SLOT(onAboutToHide()) },
            { SignalMaskFlags::AboutToShow, SIGNAL(aboutToShow()), SLOT(onAboutToShow()) },
            { SignalMaskFlags::Hovered, SIGNAL(hovered(QAction*)), SLOT(onHovered(QAction*)) },
            { SignalMaskFlags::Triggered, SIGNAL(triggered(QAction*)), SLOT(onTriggered(QAction*)) }
        };
    public:
        explicit MenuWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // Menu ====================
        void onAboutToHide() {
            handler->aboutToHide();
        }
        void onAboutToShow() {
            handler->aboutToShow();
        }
        void onHovered(QAction *action) {
            handler->hovered((Action::HandleRef)action);
        }
        void onTriggered(QAction *action) {
            handler->triggered((Action::HandleRef)action);
        }
    };

    void Handle_setIcon(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon) {
        THIS->setIcon(Icon::fromDeferred(icon));
    }

    void Handle_setSeparatorsCollapsible(HandleRef _this, bool state) {
        THIS->setSeparatorsCollapsible(state);
    }

    void Handle_setTearOffEnabled(HandleRef _this, bool state) {
        THIS->setTearOffEnabled(state);
    }

    void Handle_setTitle(HandleRef _this, std::string title) {
        THIS->setTitle(QString::fromStdString(title));
    }

    void Handle_setToolTipsVisible(HandleRef _this, bool visible) {
        THIS->setToolTipsVisible(visible);
    }

    void Handle_clear(HandleRef _this) {
        THIS->clear();
    }

    Action::HandleRef Handle_addSeparator(HandleRef _this) {
        // should we be concerned about ownership here? I presume the Menu owns it, and we're probably not going to do anything else with it ...
        return (Action::HandleRef) THIS->addSeparator();
    }

    void Handle_popup(HandleRef _this, Point p) {
        THIS->popup(toQPoint(p));
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new MenuWithHandler(std::move(handler));
    }
}

#include "Menu.moc"
