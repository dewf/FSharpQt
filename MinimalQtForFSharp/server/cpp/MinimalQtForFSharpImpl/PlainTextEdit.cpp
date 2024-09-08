#include "generated/PlainTextEdit.h"

#include <QObject>
#include <QPlainTextEdit>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((PlainTextEditWithHandler*)_this)

namespace PlainTextEdit
{
    class PlainTextEditWithHandler : public QPlainTextEdit {
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
            // AbstractScrollArea:
            // ..... (none)
            // PlainTextEdit:
            { SignalMaskFlags::BlockCountChanged, SIGNAL(blockCountChanged(int)), SLOT(onBlockCountChanged(int)) },
            { SignalMaskFlags::CopyAvailable, SIGNAL(copyAvailable(bool)), SLOT(onCopyAvailable(bool)) },
            { SignalMaskFlags::CursorPositionChanged, SIGNAL(cursorPositionChanged()), SLOT(onCursorPositionChanged()) },
            { SignalMaskFlags::ModificationChanged, SIGNAL(modificationChanged(bool)), SLOT(onModificationChanged(bool)) },
            { SignalMaskFlags::RedoAvailable, SIGNAL(redoAvailable(bool)), SLOT(onRedoAvailable(bool)) },
            { SignalMaskFlags::SelectionChanged, SIGNAL(selectionChanged()), SLOT(onSelectionChanged()) },
            { SignalMaskFlags::TextChanged, SIGNAL(textChanged()), SLOT(onTextChanged()) },
            { SignalMaskFlags::UndoAvailable, SIGNAL(undoAvailable(bool)), SLOT(onUndoAvailable(bool)) },
            { SignalMaskFlags::UpdateRequest, SIGNAL(updateRequest(QRect,int)), SLOT(onUpdateRequest(QRect,int)) },
        };
    public:
        explicit PlainTextEditWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // .... (none)
        // AbstractScrollArea =======
        // .... (none)
        // PlainTextEdit ============
        void onBlockCountChanged(int newBlockCount) {
            handler->blockCountChanged(newBlockCount);
        }
        void onCopyAvailable(bool yes) {
            handler->copyAvailable(yes);
        }
        void onCursorPositionChanged() {
            handler->cursorPositionChanged();
        }
        void onModificationChanged(bool changed) {
            handler->modificationChanged(changed);
        }
        void onRedoAvailable(bool available) {
            handler->redoAvailable(available);
        }
        void onSelectionChanged() {
            handler->selectionChanged();
        }
        void onTextChanged() {
            handler->textChanged();
        }
        void onUndoAvailable(bool available) {
            handler->undoAvailable(available);
        }
        void onUpdateRequest(const QRect &rect, int dy) {
            handler->updateRequest(toRect(rect), dy);
        }
    };

    void Handle_setBackgroundVisible(HandleRef _this, bool visible) {
        THIS->setBackgroundVisible(visible);
    }

    int32_t Handle_blockCount(HandleRef _this) {
        return THIS->blockCount();
    }

    void Handle_setCenterOnScroll(HandleRef _this, bool state) {
        THIS->setCenterOnScroll(state);
    }

    void Handle_setCursorWidth(HandleRef _this, int32_t width) {
        THIS->setCursorWidth(width);
    }

    void Handle_setDocumentTitle(HandleRef _this, std::string title) {
        THIS->setDocumentTitle(QString::fromStdString(title));
    }

    void Handle_setLineWrapMode(HandleRef _this, LineWrapMode mode) {
        THIS->setLineWrapMode((QPlainTextEdit::LineWrapMode)mode);
    }

    void Handle_setMaximumBlockCount(HandleRef _this, int32_t count) {
        THIS->setMaximumBlockCount(count);
    }

    void Handle_setOverwriteMode(HandleRef _this, bool overwrite) {
        THIS->setOverwriteMode(overwrite);
    }

    void Handle_setPlaceholderText(HandleRef _this, std::string text) {
        THIS->setPlaceholderText(QString::fromStdString(text));
    }

    void Handle_setPlainText(HandleRef _this, std::string text) {
        THIS->setPlainText(QString::fromStdString(text));
    }

    void Handle_setReadOnly(HandleRef _this, bool state) {
        THIS->setReadOnly(state);
    }

    void Handle_setTabChangesFocus(HandleRef _this, bool state) {
        THIS->setTabChangesFocus(state);
    }

    void Handle_setTabStopDistance(HandleRef _this, double distance) {
        THIS->setTabStopDistance(distance);
    }

    void Handle_setTextInteractionFlags(HandleRef _this, TextInteractionFlags tiFlags) {
        THIS->setTextInteractionFlags((Qt::TextInteractionFlags)tiFlags);
    }

    void Handle_setUndoRedoEnabled(HandleRef _this, bool enabled) {
        THIS->setUndoRedoEnabled(enabled);
    }

    void Handle_setWordWrapMode(HandleRef _this, WrapMode mode) {
        THIS->setWordWrapMode((QTextOption::WrapMode)mode);
    }

    std::string Handle_toPlainText(HandleRef _this) {
        return THIS->toPlainText().toStdString();
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new PlainTextEditWithHandler(std::move(handler));
    }
}

#include "PlainTextEdit.moc"
