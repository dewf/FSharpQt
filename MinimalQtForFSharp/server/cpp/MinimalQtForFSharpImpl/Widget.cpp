#include "generated/Widget.h"

#include <QObject>
#include <QWidget>
#include <QLayout>

#include <QPainter>
#include <QPaintEvent>

#include <QMimeData>
#include <QDrag>
#include <utility>

#include "util/SignalStuff.h"
#include "util/convert.h"
#include "IconInternal.h"
#include "SizePolicyInternal.h"

#define THIS ((WidgetWithHandler*)_this)

namespace Widget
{
    class WidgetWithHandler : public QWidget {
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
        };
    public:
        explicit WidgetWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
    };

    const int32_t WIDGET_SIZE_MAX = QWIDGETSIZE_MAX;

    void Handle_setAcceptDrops(HandleRef _this, bool state) {
        THIS->setAcceptDrops(state);
    }

    void Handle_setAccessibleDescription(HandleRef _this, std::string desc) {
        THIS->setAccessibleDescription(QString::fromStdString(desc));
    }

    void Handle_setAccessibleName(HandleRef _this, std::string name) {
        THIS->setAccessibleName(QString::fromStdString(name));
    }

    void Handle_setAutoFillBackground(HandleRef _this, bool state) {
        THIS->setAutoFillBackground(state);
    }

    void Handle_setBaseSize(HandleRef _this, Size size) {
        THIS->setBaseSize(toQSize(size));
    }

    Rect Handle_childrenRect(HandleRef _this) {
        return toRect(THIS->childrenRect());
    }

    Region::OwnedHandleRef Handle_childrenRegion(HandleRef _this) {
        auto ret = THIS->childrenRegion();
        return (Region::OwnedHandleRef)new QRegion(ret);
    }

    void Handle_setContextMenuPolicy(HandleRef _this, ContextMenuPolicy policy) {
        THIS->setContextMenuPolicy((Qt::ContextMenuPolicy)policy);
    }

    Cursor::OwnedHandleRef Handle_getCursor(HandleRef _this) {
        auto ret = THIS->cursor();
        return (Cursor::OwnedHandleRef)new QCursor(ret);
    }

    void Handle_setEnabled(HandleRef _this, bool enabled) {
        THIS->setEnabled(enabled);
    }

    bool Handle_hasFocus(HandleRef _this) {
        return THIS->hasFocus();
    }

    void Handle_setFocusPolicy(HandleRef _this, FocusPolicy policy) {
        THIS->setFocusPolicy((Qt::FocusPolicy)policy);
    }

    Rect Handle_frameGeometry(HandleRef _this) {
        return toRect(THIS->frameGeometry());
    }

    Size Handle_frameSize(HandleRef _this) {
        return toSize(THIS->frameSize());
    }

    bool Handle_isFullscreen(HandleRef _this) {
        return THIS->isFullScreen();
    }

    void Handle_setGeometry(HandleRef _this, Rect rect) {
        THIS->setGeometry(toQRect(rect));
    }

    void Handle_setGeometry(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height) {
        THIS->setGeometry(x, y, width, height);
    }

    int32_t Handle_height(HandleRef _this) {
        return THIS->height();
    }

    void Handle_setInputMethodHints(HandleRef _this, InputMethodHints hints) {
        THIS->setInputMethodHints((Qt::InputMethodHints)hints);
    }

    bool Handle_isActiveWindow(HandleRef _this) {
        return THIS->isActiveWindow();
    }

    void Handle_setLayoutDirection(HandleRef _this, LayoutDirection direction) {
        THIS->setLayoutDirection((Qt::LayoutDirection)direction);
    }

    bool Handle_isMaximized(HandleRef _this) {
        return THIS->isMaximized();
    }

    void Handle_setMaximumHeight(HandleRef _this, int32_t height) {
        THIS->setMaximumHeight(height);
    }

    void Handle_setMaximumWidth(HandleRef _this, int32_t width) {
        THIS->setMaximumWidth(width);
    }

    void Handle_setMaximumSize(HandleRef _this, Size size) {
        THIS->setMaximumSize(toQSize(size));
    }

    bool Handle_isMinimized(HandleRef _this) {
        return THIS->isMinimized();
    }

    void Handle_setMinimumHeight(HandleRef _this, int32_t height) {
        THIS->setMinimumHeight(height);
    }

    void Handle_setMinimumSize(HandleRef _this, Size size) {
        THIS->setMinimumSize(toQSize(size));
    }

    Size Handle_minimumSizeHint(HandleRef _this) {
        return toSize(THIS->minimumSizeHint());
    }

    void Handle_setMinimumWidth(HandleRef _this, int32_t width) {
        THIS->setMinimumWidth(width);
    }

    bool Handle_isModal(HandleRef _this) {
        return THIS->isModal();
    }

    void Handle_setMouseTracking(HandleRef _this, bool state) {
        THIS->setMouseTracking(state);
    }

    Rect Handle_normalGeometry(HandleRef _this) {
        return toRect(THIS->normalGeometry());
    }

    void Handle_move(HandleRef _this, Point p) {
        THIS->move(toQPoint(p));
    }

    void Handle_move(HandleRef _this, int32_t x, int32_t y) {
        THIS->move(x, y);
    }

    Rect Handle_rect(HandleRef _this) {
        return toRect(THIS->rect());
    }

    void Handle_resize(HandleRef _this, Size size) {
        THIS->resize(toQSize(size));
    }

    void Handle_resize(HandleRef _this, int32_t width, int32_t height) {
        THIS->resize(width, height);
    }

    Size Handle_sizeHint(HandleRef _this) {
        return toSize(THIS->sizeHint());
    }

    void Handle_setSizeIncrement(HandleRef _this, Common::Size size) {
        THIS->setSizeIncrement(toQSize(size));
    }

    void Handle_setSizeIncrement(HandleRef _this, int32_t w, int32_t h) {
        THIS->setSizeIncrement(w, h);
    }

    void Handle_setSizePolicy(HandleRef _this, std::shared_ptr<SizePolicy::Deferred::Base> policy) {
        THIS->setSizePolicy(SizePolicy::fromDeferred(policy));
    }

    void Handle_setSizePolicy(HandleRef _this, Policy hPolicy, Policy vPolicy) {
        THIS->setSizePolicy((QSizePolicy::Policy)hPolicy, (QSizePolicy::Policy)vPolicy);
    }

    void Handle_setStatusTip(HandleRef _this, std::string tip) {
        THIS->setStatusTip(QString::fromStdString(tip));
    }

    void Handle_setStyleSheet(HandleRef _this, std::string styles) {
        THIS->setStyleSheet(QString::fromStdString(styles));
    }

    void Handle_setTabletTracking(HandleRef _this, bool state) {
        THIS->setTabletTracking(state);
    }

    void Handle_setToolTip(HandleRef _this, std::string tip) {
        THIS->setToolTip(QString::fromStdString(tip));
    }

    void Handle_setToolTipDuration(HandleRef _this, int32_t duration) {
        THIS->setToolTipDuration(duration);
    }

    void Handle_setUpdatesEnabled(HandleRef _this, bool enabled) {
        THIS->setUpdatesEnabled(enabled);
    }

    void Handle_setVisible(HandleRef _this, bool visible) {
        THIS->setVisible(visible);
    }

    void Handle_setWhatsThis(HandleRef _this, std::string text) {
        THIS->setWhatsThis(QString::fromStdString(text));
    }

    int32_t Handle_width(HandleRef _this) {
        return THIS->width();
    }

    void Handle_setWindowFilePath(HandleRef _this, std::string path) {
        THIS->setWindowFilePath(QString::fromStdString(path));
    }

    void Handle_setWindowFlags(HandleRef _this, WindowFlags flags_) {
        THIS->setWindowFlags((Qt::WindowFlags)flags_);
    }

    void Handle_setWindowIcon(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon) {
        THIS->setWindowIcon(Icon::fromDeferred(icon));
    }

    void Handle_setWindowModality(HandleRef _this, WindowModality modality) {
        THIS->setWindowModality((Qt::WindowModality)modality);
    }

    void Handle_setWindowModified(HandleRef _this, bool modified) {
        THIS->setWindowModified(modified);
    }

    void Handle_setWindowOpacity(HandleRef _this, double opacity) {
        THIS->setWindowOpacity(opacity);
    }

    void Handle_setWindowTitle(HandleRef _this, std::string title) {
        THIS->setWindowTitle(QString::fromStdString(title));
    }

    int32_t Handle_x(HandleRef _this) {
        return THIS->x();
    }

    int32_t Handle_y(HandleRef _this) {
        return THIS->y();
    }

    void Handle_addAction(HandleRef _this, Action::HandleRef action) {
        THIS->addAction((QAction*)action);
    }

    void Handle_setParent(HandleRef _this, HandleRef parent) {
        THIS->setParent((QWidget*)parent);
    }

    HandleRef Handle_getWindow(HandleRef _this) {
        return (HandleRef)THIS->window();
    }

    void Handle_updateGeometry(HandleRef _this) {
        THIS->updateGeometry();
    }

    void Handle_adjustSize(HandleRef _this) {
        THIS->adjustSize();
    }

    void Handle_setFixedWidth(HandleRef _this, int32_t width) {
        THIS->setFixedWidth(width);
    }

    void Handle_setFixedHeight(HandleRef _this, int32_t height) {
        THIS->setFixedHeight(height);
    }

    void Handle_setFixedSize(HandleRef _this, int32_t width, int32_t height) {
        THIS->setFixedSize(width, height);
    }

    void Handle_show(HandleRef _this) {
        THIS->show();
    }

    void Handle_hide(HandleRef _this) {
        THIS->hide();
    }

    void Handle_update(HandleRef _this) {
        THIS->update();
    }

    void Handle_update(HandleRef _this, int32_t x, int32_t y, int32_t width, int32_t height) {
        THIS->update(x, y, width, height);
    }

    void Handle_update(HandleRef _this, Rect rect) {
        THIS->update(toQRect(rect));
    }

    void Handle_setLayout(HandleRef _this, Layout::HandleRef layout) {
        THIS->setLayout((QLayout*)layout);
    }

    Layout::HandleRef Handle_getLayout(HandleRef _this) {
        return (Layout::HandleRef)THIS->layout();
    }

    Point Handle_mapToGlobal(HandleRef _this, Point p) {
        auto p2 = THIS->mapToGlobal(toQPoint(p));
        return toPoint(p2);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new WidgetWithHandler(std::move(handler));
    }

    HandleRef createNoHandler() {
        return (HandleRef) new QWidget();
    }

    // =========================================================================

#define EVENTTHIS ((QEvent*)_this)

    void Event_accept(EventRef _this) {
        EVENTTHIS->accept();
    }

    void Event_ignore(EventRef _this) {
        EVENTTHIS->ignore();
    }

//    void Event_dispose(EventRef _this) {
//        // not owned, do nothing
//    }

#define MIMETHIS ((QMimeData*)_this)

    std::vector<std::string> MimeData_formats(MimeDataRef _this) {
        std::vector<std::string> result;
        for (auto & fmt : MIMETHIS->formats()) {
            result.push_back(fmt.toStdString());
        }
        return result;
    }

    bool MimeData_hasFormat(MimeDataRef _this, std::string mimeType) {
        return MIMETHIS->hasFormat(QString::fromStdString(mimeType));
    }

    std::string MimeData_text(MimeDataRef _this) {
        return MIMETHIS->text().toStdString();
    }

    void MimeData_setText(MimeDataRef _this, std::string text) {
        MIMETHIS->setText(QString::fromStdString(text));
    }

    std::vector<std::string> MimeData_urls(MimeDataRef _this) {
        std::vector<std::string> result;
        for (auto & url : MIMETHIS->urls()) {
            result.push_back(url.toString().toStdString());
        }
        return result;
    }

    void MimeData_setUrls(MimeDataRef _this, std::vector<std::string> urls) {
        QList<QUrl> qUrls;
        for (auto & url : urls) {
            qUrls.append(QUrl(QString::fromStdString(url)));
        }
        MIMETHIS->setUrls(qUrls);
    }

//    void MimeData_dispose(MimeDataRef _this) {
//        // if it was created on a drop, we're not responsible for it
//        // if we created it for a drag, we're also not responsible for releasing it
//    }

    MimeDataRef createMimeData() {
        return (MimeDataRef) new QMimeData();
    }

#define DRAGTHIS ((QDrag*)_this)

    void Drag_setMimeData(DragRef _this, MimeDataRef data) {
        DRAGTHIS->setMimeData((QMimeData*)data);
    }

    DropAction Drag_exec(DragRef _this, DropActionSet supportedActions, DropAction defaultAction) {
        auto qDefault = (Qt::DropAction)defaultAction;
        auto qSupported = QFlags<Qt::DropAction>(supportedActions);
        return (DropAction) DRAGTHIS->exec(qSupported, qDefault);
    }

//    void Drag_dispose(DragRef _this) {
//        // we're not responsible for deleting these (if they are exec'ed)
//    }

    DragRef createDrag(HandleRef parent) {
        return (DragRef) new QDrag((QObject*)parent);
    }

#define DRAGMOVETHIS ((QDragMoveEvent*)_this)

    DropAction DragMoveEvent_proposedAction(DragMoveEventRef _this) {
        return (DropAction) DRAGMOVETHIS->proposedAction();
    }

    void DragMoveEvent_acceptProposedAction(DragMoveEventRef _this) {
        DRAGMOVETHIS->acceptProposedAction();
    }

    DropActionSet DragMoveEvent_possibleActions(DragMoveEventRef _this) {
        return (DropActionSet) DRAGMOVETHIS->possibleActions();
    }

    void DragMoveEvent_acceptDropAction(DragMoveEventRef _this, DropAction action) {
        auto qtAction = (Qt::DropAction)action;
        if (qtAction == DRAGMOVETHIS->proposedAction()) {
            DRAGMOVETHIS->acceptProposedAction();
        } else {
            if (DRAGMOVETHIS->possibleActions().testFlag(qtAction)) {
                DRAGMOVETHIS->setDropAction(qtAction);
                DRAGMOVETHIS->accept();
            } else {
                printf("DragMoveEvent_acceptDropAction: specified action was not in allowed set\n");
            }
        }
    }

//    void DragMoveEvent_dispose(DragMoveEventRef _this) {
//        // not owned
//    }

    // subclass stuff ==========================================================

    class WidgetSubclass : public WidgetWithHandler {
    private:
        std::shared_ptr<MethodDelegate> methodDelegate;
        MethodMask methodMask;
    public:
        WidgetSubclass(std::shared_ptr<MethodDelegate> &methodDelegate, MethodMask methodMask, std::shared_ptr<SignalHandler> handler) :
            methodDelegate(methodDelegate),
            methodMask(methodMask),
            WidgetWithHandler(std::move(handler))
            {}
    protected:
        void paintEvent(QPaintEvent *event) override {
            if (methodMask & MethodMaskFlags::PaintEvent) {
                QPainter painter(this);
                methodDelegate->paintEvent((Painter::HandleRef)&painter, toRect(event->rect()));
                // prevent it from propagating:
                // do we allow this from the method delegate somehow?
                event->accept();
            } else {
                QWidget::paintEvent(event);
            }
        }
        void mousePressEvent(QMouseEvent *event) override {
            if (methodMask & MethodMaskFlags::MousePressEvent) {
                auto pos = toPoint(event->pos());
                auto button = (MouseButton)event->button();
                auto modifiers = event->modifiers();
                methodDelegate->mousePressEvent(pos, button, modifiers);
                // prevent from propagating, see notes above
                event->accept();
            } else {
                QWidget::mousePressEvent(event);
            }
        }

        void mouseMoveEvent(QMouseEvent *event) override {
            if (methodMask & MethodMaskFlags::MouseMoveEvent) {
                auto pos = toPoint(event->pos());
                auto buttons = event->buttons();
                auto modifiers = event->modifiers();
                methodDelegate->mouseMoveEvent(pos, buttons, modifiers);
                // prevent from propagating, see notes above
                event->accept();
            } else {
                QWidget::mouseMoveEvent(event);
            }
        }

        void mouseReleaseEvent(QMouseEvent *event) override {
            if (methodMask & MethodMaskFlags::MouseReleaseEvent) {
                auto pos = toPoint(event->pos());
                auto button = (MouseButton)event->button();
                auto modifiers = event->modifiers();
                methodDelegate->mouseReleaseEvent(pos, button, modifiers);
                event->accept();
            } else {
                QWidget::mouseReleaseEvent(event);
            }
        }

        void enterEvent(QEnterEvent *event) override {
            if (methodMask & MethodMaskFlags::EnterEvent) {
                auto pos = toPoint(event->position().toPoint());
                methodDelegate->enterEvent(pos);
                event->accept();
            } else {
                QWidget::enterEvent(event);
            }
        }

        void leaveEvent(QEvent *event) override {
            if (methodMask & MethodMaskFlags::LeaveEvent) {
                methodDelegate->leaveEvent();
                event->accept();
            } else {
                QWidget::leaveEvent(event);
            }
        }

        void resizeEvent(QResizeEvent *event) override {
            if (methodMask & MethodMaskFlags::ResizeEvent) {
                methodDelegate->resizeEvent(toSize(event->oldSize()), toSize(event->size()));
                event->accept();
            } else {
                QWidget::resizeEvent(event);
            }
        }

        void dragEnterEvent(QDragEnterEvent *event) override {
            if (methodMask & MethodMaskFlags::DropEvents) {
                auto pos = toPoint(event->position().toPoint());
                auto modifiers = event->modifiers();
                auto mimeOpaque = (MimeDataRef)event->mimeData();
                auto moveEvent = (DragMoveEventRef)event;
                methodDelegate->dragMoveEvent(pos, modifiers, mimeOpaque, moveEvent, true);
                // other side needs to call acceptProposedAction if it's OK, otherwise ... .ignore?
            } else {
                QWidget::dragEnterEvent(event);
            }
        }

        void dragMoveEvent(QDragMoveEvent *event) override {
            if (methodMask & MethodMaskFlags::DropEvents) {
                auto pos = toPoint(event->position().toPoint());
                auto modifiers = event->modifiers();
                auto mimeOpaque = (MimeDataRef)event->mimeData();
                auto moveEvent = (DragMoveEventRef)event;
                methodDelegate->dragMoveEvent(pos, modifiers, mimeOpaque, moveEvent, false);
                // other side needs to call acceptProposedAction if it's OK, otherwise ... .ignore?
            } else {
                QWidget::dragMoveEvent(event);
            }
        }

        void dragLeaveEvent(QDragLeaveEvent *event) override {
            if (methodMask & MethodMaskFlags::DropEvents) {
                methodDelegate->dragLeaveEvent();
                event->accept();
            } else {
                QWidget::dragLeaveEvent(event);
            }
        }

        void dropEvent(QDropEvent *event) override {
            if (methodMask & MethodMaskFlags::DropEvents) {
                auto pos = toPoint(event->position().toPoint());
                auto modifiers = event->modifiers();
                auto mimeOpaque = (MimeDataRef)event->mimeData();
                auto action = (DropAction)event->proposedAction();
                methodDelegate->dropEvent(pos, modifiers, mimeOpaque, action);
                event->acceptProposedAction();
            } else {
                QWidget::dropEvent(event);
            }
        }

    public:
        [[nodiscard]] QSize sizeHint() const override {
            if (methodMask & MethodMaskFlags::SizeHint) {
                auto size = methodDelegate->sizeHint();
                return toQSize(size);
            } else {
                return QWidget::sizeHint();
            }
        }
    };

    HandleRef createSubclassed(std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask, std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new WidgetSubclass(methodDelegate, methodMask, std::move(handler));
    }
}

#include "Widget.moc"
