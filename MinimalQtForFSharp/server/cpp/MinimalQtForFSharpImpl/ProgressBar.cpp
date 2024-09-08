#include "generated/ProgressBar.h"

#include <QProgressBar>

#include "util/SignalStuff.h"
#include "util/convert.h"
#include "IconInternal.h"

#define THIS ((ProgressBarWithHandler*)_this)

namespace ProgressBar
{
    class ProgressBarWithHandler : public QProgressBar {
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
            // ProgressBar:
            { SignalMaskFlags::ValueChanged, SIGNAL(valueChanged(int)), SLOT(onValueChanged(int)) },
        };
    public:
        explicit ProgressBarWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // ProgressBar =============
        void onValueChanged(int value) {
            handler->valueChanged(value);
        }
    };

    void Handle_setAlignment(HandleRef _this, Alignment align) {
        THIS->setAlignment((Qt::Alignment)align);
    }

    void Handle_setFormat(HandleRef _this, std::string format) {
        THIS->setFormat(QString::fromStdString(format));
    }

    void Handle_setInvertedAppearance(HandleRef _this, bool invert) {
        THIS->setInvertedAppearance(invert);
    }

    void Handle_setMaximum(HandleRef _this, int32_t value) {
        THIS->setMaximum(value);
    }

    void Handle_setMinimum(HandleRef _this, int32_t value) {
        THIS->setMinimum(value);
    }

    void Handle_setOrientation(HandleRef _this, Orientation orient) {
        THIS->setOrientation((Qt::Orientation)orient);
    }

    std::string Handle_text(HandleRef _this) {
        return THIS->text().toStdString();
    }

    void Handle_setTextDirection(HandleRef _this, Direction direction) {
        THIS->setTextDirection((QProgressBar::Direction)direction);
    }

    void Handle_setTextVisible(HandleRef _this, bool visible) {
        THIS->setTextVisible(visible);
    }

    void Handle_setValue(HandleRef _this, int32_t value) {
        THIS->setValue(value);
    }

    void Handle_setRange(HandleRef _this, int32_t min, int32_t max) {
        THIS->setRange(min, max);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new ProgressBarWithHandler(std::move(handler));
    }
}

#include "ProgressBar.moc"
