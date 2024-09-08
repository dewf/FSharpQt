#include "generated/ToolBar.h"

#include <QToolBar>
#include <utility>
#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((ToolBarWithHandler*)_this)

namespace ToolBar
{
    class ToolBarWithHandler : public QToolBar {
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
            // ToolBar:
            { SignalMaskFlags::ActionTriggered, SIGNAL(actionTriggered(QAction)), SLOT(onActionTriggered(QAction)) },
            { SignalMaskFlags::AllowedAreasChanged, SIGNAL(allowedAreasChanged(Qt::ToolBarAreas)), SLOT(onAllowedAreasChanged(Qt::ToolBarAreas)) },
            { SignalMaskFlags::IconSizeChanged, SIGNAL(iconSizeChanged(QSize)), SLOT(onIconSizeChanged(QSize)) },
            { SignalMaskFlags::MovableChanged, SIGNAL(movableChanged(bool)), SLOT(onMovableChanged(bool)) },
            { SignalMaskFlags::OrientationChanged, SIGNAL(orientationChanged(Qt::Orientation)), SLOT(onOrientationChanged(Qt::Orientation)) },
            { SignalMaskFlags::ToolButtonStyleChanged, SIGNAL(toolButtonStyleChanged(Qt::ToolButtonStyle)), SLOT(onToolButtonStyleChanged(Qt::ToolButtonStyle)) },
            { SignalMaskFlags::TopLevelChanged, SIGNAL(topLevelChanged(bool)), SLOT(onTopLevelChanged(bool)) },
            { SignalMaskFlags::VisibilityChanged, SIGNAL(visibilityChanged(bool)), SLOT(onVisibilityChanged(bool)) },
        };
    public:
        explicit ToolBarWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // ToolBar =================
        void onActionTriggered(QAction *action) {
            handler->actionTriggered((Action::HandleRef)action);
        };
        void onAllowedAreasChanged(Qt::ToolBarAreas allowed) {
            handler->allowedAreasChanged(allowed);
        }
        void onIconSizeChanged(const QSize& size) {
            handler->iconSizeChanged(toSize(size));
        }
        void onMovableChanged(bool movable) {
            handler->movableChanged(movable);
        }
        void onOrientationChanged(Qt::Orientation value) {
            handler->orientationChanged((Enums::Orientation)value);
        }
        void onToolButtonStyleChanged(Qt::ToolButtonStyle style) {
            handler->toolButtonStyleChanged((ToolButtonStyle)style);
        }
        void onTopLevelChanged(bool topLevel) {
            handler->topLevelChanged(topLevel);
        }
        void onVisibilityChanged(bool visible) {
            handler->visibilityChanged(visible);
        }
    };

    void Handle_setAllowedAreas(HandleRef _this, Enums::ToolBarAreas allowed) {
        THIS->setAllowedAreas((Qt::ToolBarAreas)allowed);
    }

    void Handle_setFloatable(HandleRef _this, bool floatable) {
        THIS->setFloatable(floatable);
    }

    bool Handle_isFloating(HandleRef _this) {
        return THIS->isFloating();
    }

    void Handle_setIconSize(HandleRef _this, Size size) {
        THIS->setIconSize(toQSize(size));
    }

    void Handle_setMovable(HandleRef _this, bool value) {
        THIS->setMovable(value);
    }

    void Handle_setOrientation(HandleRef _this, Orientation value) {
        THIS->setOrientation((Qt::Orientation)value);
    }

    void Handle_setToolButtonStyle(HandleRef _this, ToolButtonStyle style) {
        THIS->setToolButtonStyle((Qt::ToolButtonStyle)style);
    }

    Action::HandleRef Handle_addSeparator(HandleRef _this) {
        return (Action::HandleRef)THIS->addSeparator();
    }

    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget) {
        THIS->addWidget((QWidget*)widget);
    }

    void Handle_clear(HandleRef _this) {
        THIS->clear();
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new ToolBarWithHandler(std::move(handler));
    }
}

#include "ToolBar.moc"
