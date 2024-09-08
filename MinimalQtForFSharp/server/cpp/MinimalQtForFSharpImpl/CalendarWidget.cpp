#include "generated/CalendarWidget.h"

#include <QCalendarWidget>
#include <utility>

#include "util/SignalStuff.h"
#include "util/convert.h"
#include "IconInternal.h"
#include "DateInternal.h"

#define THIS ((CalendarWidgetWithHandler*)_this)

namespace CalendarWidget
{
    class CalendarWidgetWithHandler : public QCalendarWidget {
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
            // CalendarWidget:
            { SignalMaskFlags::Activated, SIGNAL(activated(QDate)), SLOT(onActivated(QDate)) },
            { SignalMaskFlags::Clicked, SIGNAL(clicked(QDate)), SLOT(onClicked(QDate)) },
            { SignalMaskFlags::CurrentPageChanged, SIGNAL(currentPageChanged(int,int)), SLOT(onCurrentPageChanged(int,int)) },
            { SignalMaskFlags::SelectionChanged, SIGNAL(selectionChanged()), SLOT(onSelectionChanged()) }
        };
    public:
        explicit CalendarWidgetWithHandler(std::shared_ptr<SignalHandler> handler) : handler(std::move(handler)) {}
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
        // CalendarWidget ===========
        void onActivated(const QDate& date) {
            handler->activated((Date::HandleRef)&date);
        }
        void onClicked(const QDate& date) {
            handler->clicked((Date::HandleRef)&date);
        }
        void onCurrentPageChanged(int year, int month) {
            handler->currentPageChanged(year, month);
        }
        void onSelectionChanged() {
            handler->selectionChanged();
        }
    };

    void Handle_setDateEditAcceptDelay(HandleRef _this, int32_t value) {
        THIS->setDateEditAcceptDelay(value);
    }

    void Handle_setDateEditEnabled(HandleRef _this, bool enabled) {
        THIS->setDateEditEnabled(enabled);
    }

    void Handle_setFirstDayOfWeek(HandleRef _this, QDayOfWeek value) {
        THIS->setFirstDayOfWeek((Qt::DayOfWeek)value);
    }

    void Handle_setGridVisible(HandleRef _this, bool visible) {
        THIS->setGridVisible(visible);
    }

    void Handle_setHorizontalHeaderFormat(HandleRef _this, HorizontalHeaderFormat format) {
        THIS->setHorizontalHeaderFormat((QCalendarWidget::HorizontalHeaderFormat)format);
    }

    void Handle_setMaximumDate(HandleRef _this, std::shared_ptr<Date::Deferred::Base> value) {
        THIS->setMaximumDate(Date::fromDeferred(value));
    }

    void Handle_setMinimumDate(HandleRef _this, std::shared_ptr<Date::Deferred::Base> value) {
        THIS->setMinimumDate(Date::fromDeferred(value));
    }

    void Handle_setNavigationBarVisible(HandleRef _this, bool visible) {
        THIS->setNavigationBarVisible(visible);
    }

    Date::OwnedHandleRef Handle_selectedDate(HandleRef _this) {
        return (Date::OwnedHandleRef)new QDate(THIS->selectedDate());
    }

    void Handle_setSelectedDate(HandleRef _this, std::shared_ptr<Date::Deferred::Base> selected) {
        THIS->setSelectedDate(Date::fromDeferred(selected));
    }

    void Handle_setSelectionMode(HandleRef _this, SelectionMode mode) {
        THIS->setSelectionMode((QCalendarWidget::SelectionMode)mode);
    }

    void Handle_setVerticalHeaderFormat(HandleRef _this, VerticalHeaderFormat format) {
        THIS->setVerticalHeaderFormat((QCalendarWidget::VerticalHeaderFormat)format);
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new CalendarWidgetWithHandler(std::move(handler));
    }
}

#include "CalendarWidget.moc"
