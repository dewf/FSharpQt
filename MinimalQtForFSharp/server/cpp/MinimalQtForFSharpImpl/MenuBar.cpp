#include "generated/MenuBar.h"

#include <QObject>
#include <QMenuBar>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((MenuBarWithHandler*)_this)

namespace MenuBar
{
    class MenuBarWithHandler : public QMenuBar {
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
            // MenuBar:
            { SignalMaskFlags::Hovered, SIGNAL(hovered(QAction*)), SLOT(onHovered(QAction*)) },
            { SignalMaskFlags::Triggered, SIGNAL(triggered(QAction*)), SLOT(onTriggered(QAction*))}
        };
    public:
        explicit MenuBarWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // MenuBar =================
        void onHovered(QAction* action) {
            handler->hovered((Action::HandleRef)action);
        }
        void onTriggered(QAction* action) {
            handler->triggered((Action::HandleRef)action);
        }
    };

    void Handle_setDefaultUp(HandleRef _this, bool state) {
        THIS->setDefaultUp(state);
    }

    void Handle_setNativeMenuBar(HandleRef _this, bool state) {
        THIS->setNativeMenuBar(state);
    }

    void Handle_clear(HandleRef _this) {
        THIS->clear();
    }

    void Handle_addMenu(HandleRef _this, Menu::HandleRef menu) {
        THIS->addMenu((QMenu*)menu);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new MenuBarWithHandler(std::move(handler));
    }
}

#include "MenuBar.moc"
