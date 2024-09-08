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
#include "Widget.h"
using namespace ::Widget;
#include "Enums.h"
using namespace ::Enums;

namespace TabBar
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
        TabCloseRequested = 1 << 8,
        TabMoved = 1 << 9
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
        virtual void tabMoved(int32_t fromIndex, int32_t toIndex) = 0;
    };

    enum class ButtonPosition {
        LeftSide,
        RightSide
    };

    enum class SelectionBehavior {
        SelectLeftTab,
        SelectRightTab,
        SelectPreviousTab
    };

    enum class Shape {
        RoundedNorth,
        RoundedSouth,
        RoundedWest,
        RoundedEast,
        TriangularNorth,
        TriangularSouth,
        TriangularWest,
        TriangularEast
    };

    void Handle_setAutoHide(HandleRef _this, bool value);
    void Handle_setChangeCurrentOnDrag(HandleRef _this, bool value);
    int32_t Handle_count(HandleRef _this);
    void Handle_setCurrentIndex(HandleRef _this, int32_t value);
    int32_t Handle_currentIndex(HandleRef _this);
    void Handle_setDocumentMode(HandleRef _this, bool value);
    void Handle_setDrawBase(HandleRef _this, bool value);
    void Handle_setElideMode(HandleRef _this, Enums::TextElideMode mode);
    void Handle_setExpanding(HandleRef _this, bool value);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setMovable(HandleRef _this, bool value);
    void Handle_setSelectionBehaviorOnRemove(HandleRef _this, SelectionBehavior value);
    void Handle_setShape(HandleRef _this, Shape shape);
    void Handle_setTabsClosable(HandleRef _this, bool value);
    void Handle_setUsesScrollButtons(HandleRef _this, bool value);
    void Handle_removeAllTabs(HandleRef _this);
    int32_t Handle_addTab(HandleRef _this, std::string text);
    int32_t Handle_addTab(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon, std::string text);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
