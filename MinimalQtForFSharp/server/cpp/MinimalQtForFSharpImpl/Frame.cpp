#include "generated/Frame.h"

#include <QFrame>

#include "util/SignalStuff.h"
#include "util/convert.h"
#include "IconInternal.h"

#define THIS ((FrameWithHandler*)_this)

namespace Frame
{
    class FrameWithHandler : public QFrame {
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
        };
    public:
        explicit FrameWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // (none)
    };

    void Handle_setFrameRect(HandleRef _this, Rect rect) {
        THIS->setFrameRect(toQRect(rect));
    }

    void Handle_setFrameShadow(HandleRef _this, Shadow shadow) {
        THIS->setFrameShadow((QFrame::Shadow)shadow);
    }

    void Handle_setFrameShape(HandleRef _this, Shape shape) {
        THIS->setFrameShape((QFrame::Shape)shape);
    }

    int32_t Handle_frameWidth(HandleRef _this) {
        return THIS->frameWidth();
    }

    void Handle_setLineWidth(HandleRef _this, int32_t width) {
        THIS->setLineWidth(width);
    }

    void Handle_setMidLineWidth(HandleRef _this, int32_t width) {
        THIS->setMidLineWidth(width);
    }

    void Handle_setFrameStyle(HandleRef _this, Shape shape, Shadow shadow) {
        THIS->setFrameStyle((QFrame::Shape)shape | (QFrame::Shadow)shadow);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new FrameWithHandler(std::move(handler));
    }
}

#include "Frame.moc"
