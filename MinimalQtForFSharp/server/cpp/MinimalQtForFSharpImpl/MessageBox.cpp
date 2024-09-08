#include "generated/MessageBox.h"

#include <QMessageBox>

#include "util/SignalStuff.h"
#include "util/convert.h"
#include "IconInternal.h"

#define THIS ((MessageBoxWithHandler*)_this)

namespace MessageBox
{
    class MessageBoxWithHandler : public QMessageBox {
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
            // Dialog:
            { SignalMaskFlags::Accepted, SIGNAL(accepted()), SLOT(onAccepted()) },
            { SignalMaskFlags::Finished, SIGNAL(finished(int)), SLOT(onFinished(int)) },
            { SignalMaskFlags::Rejected, SIGNAL(rejected()), SLOT(onRejected()) },
            // MessageBox:
            { SignalMaskFlags::ButtonClicked, SIGNAL(buttonClicked(QAbstractButton)), SLOT(onButtonClicked(QAbstractButton)) }
        };
    public:
        MessageBoxWithHandler(QWidget *parent, const std::shared_ptr<SignalHandler> &handler) : handler(handler), QMessageBox(parent) {}
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
        // Widget =================
        void onCustomContextMenuRequested(const QPoint& pos) {
            handler->customContextMenuRequested(toPoint(pos));
        }
        void onWindowIconChanged(const QIcon& icon) {
            handler->windowIconChanged((::Icon::HandleRef)&icon);
        }
        void onWindowTitleChanged(const QString& title) {
            handler->windowTitleChanged(title.toStdString());
        }
        // Dialog =================
        void onAccepted() {
            handler->accepted();
        }
        void onFinished(int result) {
            handler->finished(result);
        }
        void onRejected() {
            handler->rejected();
        }
        // MessageBox ==============
        void onButtonClicked(QAbstractButton *button) {
            handler->buttonClicked((AbstractButton::HandleRef)button);
        }
    };

    void Handle_setDetailedText(HandleRef _this, std::string text) {
        THIS->setDetailedText(QString::fromStdString(text));
    }

    void Handle_setIcon(HandleRef _this, MessageBoxIcon icon) {
        THIS->setIcon((QMessageBox::Icon)icon);
    }

    void Handle_setInformativeText(HandleRef _this, std::string text) {
        THIS->setInformativeText(QString::fromStdString(text));
    }

    void Handle_setOptions(HandleRef _this, Options opts) {
        THIS->setOptions((QMessageBox::Options)opts);
    }

    void Handle_setStandardButtons(HandleRef _this, StandardButtonSet buttons) {
        THIS->setStandardButtons((QMessageBox::StandardButtons)buttons);
    }

    void Handle_setText(HandleRef _this, std::string text) {
        THIS->setText(QString::fromStdString(text));
    }

    void Handle_setTextFormat(HandleRef _this, TextFormat format) {
        THIS->setTextFormat((Qt::TextFormat)format);
    }

    void Handle_setTextInteractionFlags(HandleRef _this, TextInteractionFlags tiFlags) {
        THIS->setTextInteractionFlags((Qt::TextInteractionFlags)tiFlags);
    }

    void Handle_setDefaultButton(HandleRef _this, StandardButton button) {
        THIS->setDefaultButton((QMessageBox::StandardButton)button);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(Widget::HandleRef parent, std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new MessageBoxWithHandler((QWidget*)parent, handler);
    }
}

#include "MessageBox.moc"
