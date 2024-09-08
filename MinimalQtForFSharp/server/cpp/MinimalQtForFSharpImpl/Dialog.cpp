#include "generated/Dialog.h"

#include <QObject>
#include <QDialog>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((DialogWithHandler*)_this)

namespace Dialog
{
    class DialogWithHandler : public QDialog {
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
        };
    public:
        DialogWithHandler(QWidget *parent, const std::shared_ptr<SignalHandler> &handler) : handler(handler), QDialog(parent) {}
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
            handler->windowIconChanged((Icon::HandleRef)&icon);
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
    };

    void Handle_setModal(HandleRef _this, bool state) {
        THIS->setModal(state);
    }

    void Handle_setSizeGripEnabled(HandleRef _this, bool enabled) {
        THIS->setSizeGripEnabled(enabled);
    }

    void Handle_setParentDialogFlags(HandleRef _this, Widget::HandleRef parent) {
        THIS->setParent((QWidget*)parent, Qt::Dialog);
    }

    void Handle_accept(HandleRef _this) {
        THIS->accept();
    }

    void Handle_reject(HandleRef _this) {
        THIS->reject();
    }

    int Handle_exec(HandleRef _this) {
        return THIS->exec();
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(Widget::HandleRef parent, std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new DialogWithHandler((QWidget*)parent, handler);
    }
}

#include "Dialog.moc"
