#include "generated/GroupBox.h"

#include <QGroupBox>
#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((GroupBoxWithHandler*)_this)

namespace GroupBox {
    class GroupBoxWithHandler : public QGroupBox {
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
            // GroupBox:
            { SignalMaskFlags::Clicked, SIGNAL(clicked(bool)), SLOT(onClicked(bool)) },
            { SignalMaskFlags::Toggled, SIGNAL(toggled(bool)), SLOT(onToggled(bool)) }
        };
    public:
        explicit GroupBoxWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // GroupBox ===================
        void onClicked(bool checked_) {
            handler->clicked(checked_);
        }
        void onToggled(bool state) {
            handler->toggled(state);
        }
    };

    void Handle_setAlignment(HandleRef _this, Alignment align) {
        THIS->setAlignment((Qt::Alignment)align);
    }

    void Handle_setCheckable(HandleRef _this, bool state) {
        THIS->setCheckable(state);
    }

    void Handle_setChecked(HandleRef _this, bool state) {
        THIS->setChecked(state);
    }

    void Handle_setFlat(HandleRef _this, bool state) {
        THIS->setFlat(state);
    }

    void Handle_setTitle(HandleRef _this, std::string title) {
        THIS->setTitle(QString::fromStdString(title));
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new GroupBoxWithHandler(std::move(handler));
    }
}

#include "GroupBox.moc"
