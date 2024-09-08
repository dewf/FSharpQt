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

#include "Object.h"
using namespace ::Object;
#include "Common.h"
using namespace ::Common;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;
#include "Widget.h"
using namespace ::Widget;

namespace TabWidget
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
        CurrentChanged = 1 << 5,
        TabBarClicked = 1 << 6,
        TabBarDoubleClicked = 1 << 7,
        TabCloseRequested = 1 << 8
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void currentChanged(int32_t index) = 0;
        virtual void tabBarClicked(int32_t index) = 0;
        virtual void tabBarDoubleClicked(int32_t index) = 0;
        virtual void tabCloseRequested(int32_t index) = 0;
    };

    enum class TabShape {
        Rounded,
        Triangular
    };

    enum class TabPosition {
        North,
        South,
        West,
        East
    };

    int32_t Handle_count(HandleRef _this);
    void Handle_setCurrentIndex(HandleRef _this, int32_t index);
    void Handle_setDocumentMode(HandleRef _this, bool state);
    void Handle_setElideMode(HandleRef _this, Enums::TextElideMode mode);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setMovable(HandleRef _this, bool state);
    void Handle_setTabBarAutoHide(HandleRef _this, bool state);
    void Handle_setTabPosition(HandleRef _this, TabPosition position);
    void Handle_setTabShape(HandleRef _this, TabShape shape);
    void Handle_setTabsClosable(HandleRef _this, bool state);
    void Handle_setUsesScrollButtons(HandleRef _this, bool state);
    void Handle_addTab(HandleRef _this, Widget::HandleRef page, std::string label);
    void Handle_insertTab(HandleRef _this, int32_t index, Widget::HandleRef page, std::string label);
    Widget::HandleRef Handle_widgetAt(HandleRef _this, int32_t index);
    void Handle_clear(HandleRef _this);
    void Handle_removeTab(HandleRef _this, int32_t index);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
