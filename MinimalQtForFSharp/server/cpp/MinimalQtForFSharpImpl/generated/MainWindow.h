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
#include "Widget.h"
using namespace ::Widget;
#include "Layout.h"
using namespace ::Layout;
#include "MenuBar.h"
using namespace ::MenuBar;
#include "Icon.h"
using namespace ::Icon;
#include "DockWidget.h"
using namespace ::DockWidget;
#include "Enums.h"
using namespace ::Enums;
#include "ToolBar.h"
using namespace ::ToolBar;
#include "StatusBar.h"
using namespace ::StatusBar;
#include "TabWidget.h"
using namespace ::TabWidget;

namespace MainWindow
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
        IconSizeChanged = 1 << 5,
        TabifiedDockWidgetActivated = 1 << 6,
        ToolButtonStyleChanged = 1 << 7,
        WindowClosed = 1 << 8
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void iconSizeChanged(Common::Size iconSize) = 0;
        virtual void tabifiedDockWidgetActivated(DockWidget::HandleRef dockWidget) = 0;
        virtual void toolButtonStyleChanged(Enums::ToolButtonStyle style) = 0;
        virtual void windowClosed() = 0;
    };

    typedef int32_t DockOptions;
    enum DockOptionsFlags : int32_t {
        AnimatedDocks = 0x01,
        AllowNestedDocks = 0x02,
        AllowTabbedDocks = 0x04,
        ForceTabbedDocks = 0x08,
        VerticalTabs = 0x10,
        GroupedDragging = 0x20
    };

    void Handle_setAnimated(HandleRef _this, bool state);
    void Handle_setDockNestingEnabled(HandleRef _this, bool state);
    void Handle_setDockOptions(HandleRef _this, DockOptions dockOptions);
    void Handle_setDocumentMode(HandleRef _this, bool state);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setTabShape(HandleRef _this, TabWidget::TabShape tabShape);
    void Handle_setToolButtonStyle(HandleRef _this, Enums::ToolButtonStyle style);
    void Handle_setUnifiedTitleAndToolBarOnMac(HandleRef _this, bool state);
    void Handle_setCentralWidget(HandleRef _this, Widget::HandleRef widget);
    void Handle_setMenuBar(HandleRef _this, MenuBar::HandleRef menubar);
    void Handle_setStatusBar(HandleRef _this, StatusBar::HandleRef statusbar);
    void Handle_addToolBar(HandleRef _this, ToolBar::HandleRef toolbar);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
