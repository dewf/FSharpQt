#include "generated/Label.h"

#include <QLabel>
#include <utility>
#include "util/convert.h"

#include "util/SignalStuff.h"
#include "PixmapInternal.h"

#define THIS ((LabelWithHandler*)_this)

namespace Label
{
    class LabelWithHandler : public QLabel {
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
            // Label:
            { SignalMaskFlags::LinkActivated, SIGNAL(linkActivated(QString)), SLOT(onLinkActivated(QString)) },
            { SignalMaskFlags::LinkHovered, SIGNAL(linkHovered(QString)), SLOT(onLinkHovered(QString)) },
        };
    public:
        explicit LabelWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // ...... (none)
        // Label ====================
        void onLinkActivated(const QString& link) {
            handler->linkActivated(link.toStdString());
        }
        void onLinkHovered(const QString& link) {
            handler->linkHovered(link.toStdString());
        }
    };

    void Handle_setAlignment(HandleRef _this, Alignment align) {
        THIS->setAlignment((Qt::AlignmentFlag)align);
    }

    bool Handle_hasSelectedText(HandleRef _this) {
        return THIS->hasSelectedText();
    }

    void Handle_setIndent(HandleRef _this, int32_t indent) {
        THIS->setIndent(indent);
    }

    void Handle_setMargin(HandleRef _this, int32_t margin) {
        THIS->setMargin(margin);
    }

    void Handle_setOpenExternalLinks(HandleRef _this, bool state) {
        THIS->setOpenExternalLinks(state);
    }

    void Handle_setPixmap(HandleRef _this, std::shared_ptr<Pixmap::Deferred::Base> pixmap) {
        THIS->setPixmap(Pixmap::fromDeferred(pixmap));
    }

    void Handle_setScaledContents(HandleRef _this, bool state) {
        THIS->setScaledContents(state);
    }

    std::string Handle_selectedText(HandleRef _this) {
        return THIS->selectedText().toStdString();
    }

    void Handle_setText(HandleRef _this, std::string text) {
        THIS->setText(QString::fromStdString(text));
    }

    void Handle_setTextFormat(HandleRef _this, TextFormat format) {
        THIS->setTextFormat((Qt::TextFormat)format);
    }

    void Handle_setTextInteractionFlags(HandleRef _this, TextInteractionFlags interactionFlags) {
        THIS->setTextInteractionFlags((Qt::TextInteractionFlags)interactionFlags);
    }

    void Handle_setWordWrap(HandleRef _this, bool state) {
        THIS->setWordWrap(state);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new LabelWithHandler(std::move(handler));
    }

    HandleRef createNoHandler() {
        return (HandleRef) new QLabel();
    }
}

#include "Label.moc"
