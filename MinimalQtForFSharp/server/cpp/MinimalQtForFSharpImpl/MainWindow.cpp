#include "generated/MainWindow.h"

#include <QMainWindow>
#include <utility>

#include "util/SignalStuff.h"
#include "util/convert.h"

#include "IconInternal.h"

#define THIS ((MainWindowWithHandler*)_this)

namespace MainWindow
{
    class MainWindowWithHandler : public QMainWindow {
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
            // MainWindow:
            { SignalMaskFlags::IconSizeChanged, SIGNAL(iconSizeChanged(QSize)), SLOT(onIconSizeChanged(QSize)) },
            { SignalMaskFlags::TabifiedDockWidgetActivated, SIGNAL(tabifiedDockWidgetActivated(QDockWidget)), SLOT(onTabifiedDockWidgetActivated(QDockWidget)) },
            { SignalMaskFlags::ToolButtonStyleChanged, SIGNAL(toolButtonStyleChanged(Qt::ToolButtonStyle)), SLOT(onToolButtonStyleChanged(Qt::ToolButtonStyle)) },
            // MainWindow custom:
            { SignalMaskFlags::WindowClosed, SIGNAL(windowClosed()), SLOT(onWindowClosed()) }
        };
    public:
        explicit MainWindowWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
        void setSignalMask(SignalMask newMask) {
            if (newMask != lastMask) {
                processChanges(lastMask, newMask, signalMap, this);
                lastMask = newMask;
            }
        }
    signals:
        void windowClosed();
    protected:
        // something we're implementing ourselves since it's not a stock signal
        void closeEvent(QCloseEvent *event) override {
            QWidget::closeEvent(event);
            emit windowClosed();
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
        // MainWindow ===============
        void onIconSizeChanged(const QSize& size) {
            handler->iconSizeChanged(toSize(size));
        }
        void onTabifiedDockWidgetActivated(QDockWidget* dockWidget) {
            handler->tabifiedDockWidgetActivated((DockWidget::HandleRef)dockWidget);
        }
        void onToolButtonStyleChanged(Qt::ToolButtonStyle style) {
            handler->toolButtonStyleChanged((Enums::ToolButtonStyle)style);
        }
        // MainWindow (custom) ======
        void onWindowClosed() {
            handler->windowClosed();
        }
    };

    void Handle_setAnimated(HandleRef _this, bool state) {
        THIS->setAnimated(state);
    }

    void Handle_setDockNestingEnabled(HandleRef _this, bool state) {
        THIS->setDockNestingEnabled(state);
    }

    void Handle_setDockOptions(HandleRef _this, DockOptions dockOptions) {
        THIS->setDockOptions((QMainWindow::DockOptions)dockOptions);
    }

    void Handle_setDocumentMode(HandleRef _this, bool state) {
        THIS->setDocumentMode(state);
    }

    void Handle_setIconSize(HandleRef _this, Size size) {
        THIS->setIconSize(toQSize(size));
    }

    void Handle_setTabShape(HandleRef _this, TabShape tabShape) {
        THIS->setTabShape((QTabWidget::TabShape)tabShape);
    }

    void Handle_setToolButtonStyle(HandleRef _this, ToolButtonStyle style) {
        THIS->setToolButtonStyle((Qt::ToolButtonStyle)style);
    }

    void Handle_setUnifiedTitleAndToolBarOnMac(HandleRef _this, bool state) {
        THIS->setUnifiedTitleAndToolBarOnMac(state);
    }

    void Handle_setCentralWidget(HandleRef _this, Widget::HandleRef widget) {
        THIS->setCentralWidget((QWidget*)widget);
    }

    void Handle_setMenuBar(HandleRef _this, MenuBar::HandleRef menubar) {
        THIS->setMenuBar((QMenuBar*)menubar);
    }

    void Handle_setStatusBar(HandleRef _this, StatusBar::HandleRef statusbar) {
        THIS->setStatusBar((QStatusBar*)statusbar);
    }

    void Handle_addToolBar(HandleRef _this, ToolBar::HandleRef toolbar) {
        THIS->addToolBar((QToolBar*)toolbar);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new MainWindowWithHandler(std::move(handler));
    }
}

#include "MainWindow.moc"
