#include "generated/StyledItemDelegate.h"

#include <QStyledItemDelegate>
#include "util/SignalStuff.h"

#define THIS ((StyledItemDelegateWithHandler*)_this)

namespace StyledItemDelegate
{
    class StyledItemDelegateWithHandler : public QStyledItemDelegate {
        Q_OBJECT
    private:
        std::shared_ptr<SignalHandler> handler;
        SignalMask lastMask = 0;
        std::vector<SignalMapItem<SignalMaskFlags>> signalMap = {
            // Object:
            { SignalMaskFlags::Destroyed, SIGNAL(destroyed(QObject)), SLOT(onDestroyed(QObject)) },
            { SignalMaskFlags::ObjectNameChanged, SIGNAL(objectNameChanged(QString)), SLOT(onObjectNameChanged(QString)) },
            // AbstractItemDelegate:
            { SignalMaskFlags::CloseEditor, SIGNAL(closeEditor(QWidget, EndEditHint)), SLOT(onCloseEditor(QWidget,EndEditHint)) },
            { SignalMaskFlags::CommitData, SIGNAL(commitData(QWidget)), SLOT(onCommitData(QWidget)) },
            { SignalMaskFlags::SizeHintChanged, SIGNAL(sizeHintChanged(QModelIndex)), SLOT(onSizeHintChanged(QModelIndex)) }
            // StyledItemDelegateWithHandler:
            // (none)
        };
    public:
        explicit StyledItemDelegateWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // AbstractItemDelegate =================
        void onCloseEditor(QWidget *editor, QAbstractItemDelegate::EndEditHint hint = NoHint) {
            handler->closeEditor((Widget::HandleRef)editor, (AbstractItemDelegate::EndEditHint)hint);
        }
        void onCommitData(QWidget *editor) {
            handler->commitData((Widget::HandleRef)editor);
        }
        void onSizeHintChanged(const QModelIndex &index) {
            handler->sizeHintChanged((ModelIndex::HandleRef)&index);
        }
    };

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    // subclass stuff ============================================

    class StyledItemDelegateSubclass : public StyledItemDelegateWithHandler {
    private:
        std::shared_ptr<MethodDelegate> methodDelegate;
        MethodMask methodMask;
    public:
        StyledItemDelegateSubclass(
                const std::shared_ptr<MethodDelegate> &methodDelegate,
                MethodMask methodMask,
                const std::shared_ptr<SignalHandler> &handler)
                : methodDelegate(methodDelegate),
                  methodMask(methodMask),
                  StyledItemDelegateWithHandler(handler) {}
    protected:
        QWidget *createEditor(QWidget *parent, const QStyleOptionViewItem &option, const QModelIndex &index) const override {
            if (methodMask & MethodMaskFlags::CreateEditor) {
                return (QWidget*)methodDelegate->createEditor((Widget::HandleRef)parent, (StyleOptionViewItem::HandleRef)&option, (ModelIndex::HandleRef)&index);
            } else {
                return QStyledItemDelegate::createEditor(parent, option, index);
            }
        }

        void setEditorData(QWidget *editor, const QModelIndex &index) const override {
            if (methodMask & MethodMaskFlags::SetEditorData) {
                methodDelegate->setEditorData((Widget::HandleRef)editor, (ModelIndex::HandleRef)&index);
            } else {
                QStyledItemDelegate::setEditorData(editor, index);
            }
        }

        void setModelData(QWidget *editor, QAbstractItemModel *model, const QModelIndex &index) const override {
            if (methodMask & MethodMaskFlags::SetModelData) {
                methodDelegate->setModelData((Widget::HandleRef)editor, (AbstractItemModel::HandleRef)model, (ModelIndex::HandleRef)&index);
            } else {
                QStyledItemDelegate::setModelData(editor, model, index);
            }
        }

        void destroyEditor(QWidget *editor, const QModelIndex &index) const override {
            if (methodMask & MethodMaskFlags::DestroyEditor) {
                methodDelegate->destroyEditor((Widget::HandleRef)editor, (ModelIndex::HandleRef)&index);
            } else {
                QStyledItemDelegate::destroyEditor(editor, index);
            }
        }
    };

    HandleRef createdSubclassed(std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask, std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new StyledItemDelegateSubclass(methodDelegate, methodMask, handler);
    }
}

#include "StyledItemDelegate.moc"
