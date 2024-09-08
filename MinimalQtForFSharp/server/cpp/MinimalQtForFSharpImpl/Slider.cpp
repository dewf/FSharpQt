#include "generated/Slider.h"

#include <QSlider>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((SliderWithHandler*)_this)

namespace Slider
{
    class SliderWithHandler : public QSlider {
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
            // AbstractSlider:
            { SignalMaskFlags::ActionTriggered, SIGNAL(actionTriggered(int)), SLOT(onActionTriggered(int)) },
            { SignalMaskFlags::RangeChanged, SIGNAL(rangeChanged(int, int)), SLOT(onRangeChanged(int,int)) },
            { SignalMaskFlags::SliderMoved, SIGNAL(sliderMoved(int)), SLOT(onSliderMoved(int)) },
            { SignalMaskFlags::SliderPressed, SIGNAL(sliderPressed()), SLOT(onSliderPressed()) },
            { SignalMaskFlags::SliderReleased, SIGNAL(sliderReleased()), SLOT(onSliderReleased()) },
            { SignalMaskFlags::ValueChanged, SIGNAL(valueChanged(int)), SLOT(onValueChanged(int)) },
            // Slider:
            // .... (none)
        };
    public:
        explicit SliderWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // AbstractSlider ==========
        void onActionTriggered(int action) {
            handler->actionTriggered((AbstractSlider::SliderAction)action);
        };
        void onRangeChanged(int min, int max) {
            handler->rangeChanged(min, max);
        }
        void onSliderMoved(int value) {
            handler->sliderMoved(value);
        }
        void onSliderPressed() {
            handler->sliderPressed();
        }
        void onSliderReleased() {
            handler->sliderReleased();
        }
        void onValueChanged(int value) {
            handler->valueChanged(value);
        }
        // Slider ==================
        // .... (none)
    };

    void Handle_setTickInterval(HandleRef _this, int32_t interval) {
        THIS->setTickInterval(interval);
    }

    void Handle_setTickPosition(HandleRef _this, TickPosition tpos) {
        THIS->setTickPosition((QSlider::TickPosition)tpos);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new SliderWithHandler(std::move(handler));
    }
}

#include "Slider.moc"
