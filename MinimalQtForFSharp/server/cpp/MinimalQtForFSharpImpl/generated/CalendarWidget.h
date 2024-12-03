#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

#include "Common.h"
using namespace ::Common;
#include "Icon.h"
using namespace ::Icon;
#include "Object.h"
using namespace ::Object;
#include "Widget.h"
using namespace ::Widget;
#include "Date.h"
using namespace ::Date;
#include "Enums.h"
using namespace ::Enums;

namespace CalendarWidget
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Widget::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // SignalMask:
        Activated = 1 << 5,
        Clicked = 1 << 6,
        CurrentPageChanged = 1 << 7,
        SelectionChanged = 1 << 8
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void activated(Date::HandleRef date) = 0;
        virtual void clicked(Date::HandleRef date) = 0;
        virtual void currentPageChanged(int32_t year, int32_t month) = 0;
        virtual void selectionChanged() = 0;
    };

    enum class HorizontalHeaderFormat {
        NoHorizontalHeader,
        SingleLetterDayNames,
        ShortDayNames,
        LongDayNames
    };

    enum class VerticalHeaderFormat {
        NoVerticalHeader,
        ISOWeekNumbers
    };

    enum class SelectionMode {
        NoSelection,
        SingleSelection
    };

    void Handle_setDateEditAcceptDelay(HandleRef _this, int32_t value);
    void Handle_setDateEditEnabled(HandleRef _this, bool enabled);
    void Handle_setFirstDayOfWeek(HandleRef _this, QDayOfWeek value);
    void Handle_setGridVisible(HandleRef _this, bool visible);
    void Handle_setHorizontalHeaderFormat(HandleRef _this, HorizontalHeaderFormat format);
    void Handle_setMaximumDate(HandleRef _this, std::shared_ptr<Date::Deferred::Base> value);
    void Handle_setMinimumDate(HandleRef _this, std::shared_ptr<Date::Deferred::Base> value);
    void Handle_setNavigationBarVisible(HandleRef _this, bool visible);
    Date::OwnedRef Handle_selectedDate(HandleRef _this);
    void Handle_setSelectedDate(HandleRef _this, std::shared_ptr<Date::Deferred::Base> selected);
    void Handle_setSelectionMode(HandleRef _this, SelectionMode mode);
    void Handle_setVerticalHeaderFormat(HandleRef _this, VerticalHeaderFormat format);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
