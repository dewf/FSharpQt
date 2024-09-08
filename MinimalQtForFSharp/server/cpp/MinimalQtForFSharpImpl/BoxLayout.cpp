#include "generated/BoxLayout.h"

#include <QBoxLayout>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((BoxLayoutWithHandler*)_this)

namespace BoxLayout
{
    class BoxLayoutWithHandler : public QBoxLayout {
        Q_OBJECT
    private:
        std::shared_ptr<SignalHandler> handler;
        SignalMask lastMask = 0;
        std::vector<SignalMapItem<SignalMaskFlags>> signalMap = {
            // Object:
            { SignalMaskFlags::Destroyed, SIGNAL(destroyed(QObject)), SLOT(onDestroyed(QObject)) },
            { SignalMaskFlags::ObjectNameChanged, SIGNAL(objectNameChanged(QString)), SLOT(onObjectNameChanged(QString)) },
            // Layout: (none)
            // BoxLayout: (none)
        };
    public:
        explicit BoxLayoutWithHandler(QBoxLayout::Direction direction, std::shared_ptr<SignalHandler> handler) : QBoxLayout(direction), handler(std::move(handler)) {}
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
        // Layout (none)
        // BoxLayout (none)
    };

    void Handle_setDirection(HandleRef _this, Direction dir) {
        auto qDir = (QBoxLayout::Direction)dir;
        THIS->setDirection(qDir);
    }

    void Handle_addSpacing(HandleRef _this, int32_t size) {
        THIS->addSpacing(size);
    }

    void Handle_addStretch(HandleRef _this, int32_t stretch) {
        THIS->addStretch(stretch);
    }

    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget) {
        THIS->addWidget((QWidget*)widget);
    }

    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget, int32_t stretch) {
        THIS->addWidget((QWidget*)widget, stretch);
    }

    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget, int32_t stretch, Enums::Alignment align) {
        THIS->addWidget((QWidget*)widget, stretch, (Qt::AlignmentFlag)align);
    }

    void Handle_addLayout(HandleRef _this, Layout::HandleRef layout) {
        THIS->addLayout((QLayout*)layout);
    }

    void Handle_addLayout(HandleRef _this, Layout::HandleRef layout, int32_t stretch) {
        THIS->addLayout((QLayout*)layout, stretch);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(Direction dir, std::shared_ptr<SignalHandler> handler) {
        auto qDir = (QBoxLayout::Direction)dir;
        return (HandleRef) new BoxLayoutWithHandler(qDir, std::move(handler));
    }

    HandleRef createNoHandler(Direction dir) {
        return (HandleRef) new QBoxLayout((QBoxLayout::Direction)dir);
    }
}

#include "BoxLayout.moc"
