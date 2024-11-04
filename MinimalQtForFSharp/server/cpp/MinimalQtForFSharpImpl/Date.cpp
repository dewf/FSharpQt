#include "generated/Date.h"

#include <QDate>
#include "DateInternal.h"

#define THIS ((QDate*)_this)

namespace Date
{
    class FromDeferred : public Deferred::Visitor {
    private:
        QDate &date;
    public:
        explicit FromDeferred(QDate &date) : date(date) {}

        void onFromYearMonthDay(const Deferred::FromYearMonthDay *fromYearMonthDay) override {
            date = QDate(fromYearMonthDay->year, fromYearMonthDay->month, fromYearMonthDay->day);
        }
    };

    QDate fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QDate result;
        FromDeferred visitor(result);
        deferred->accept(&visitor);
        return result;
    }

    YearMonthDay Handle_toYearMonthDay(HandleRef _this) {
        return YearMonthDay{ THIS->year(), THIS->month(), THIS->day() };
    }

    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }
}
