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
#include "Action.h"
using namespace ::Action;

namespace ToolBar
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
        ActionTriggered = 1 << 5,
        AllowedAreasChanged = 1 << 6,
        IconSizeChanged = 1 << 7,
        MovableChanged = 1 << 8,
        OrientationChanged = 1 << 9,
        ToolButtonStyleChanged = 1 << 10,
        TopLevelChanged = 1 << 11,
        VisibilityChanged = 1 << 12
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void actionTriggered(Action::HandleRef action) = 0;
        virtual void allowedAreasChanged(Enums::ToolBarAreas allowed) = 0;
        virtual void iconSizeChanged(Common::Size size) = 0;
        virtual void movableChanged(bool movable) = 0;
        virtual void orientationChanged(Enums::Orientation value) = 0;
        virtual void toolButtonStyleChanged(Enums::ToolButtonStyle style) = 0;
        virtual void topLevelChanged(bool topLevel) = 0;
        virtual void visibilityChanged(bool visible) = 0;
    };

    void Handle_setAllowedAreas(HandleRef _this, Enums::ToolBarAreas allowed);
    void Handle_setFloatable(HandleRef _this, bool floatable);
    bool Handle_isFloating(HandleRef _this);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setMovable(HandleRef _this, bool value);
    void Handle_setOrientation(HandleRef _this, Enums::Orientation value);
    void Handle_setToolButtonStyle(HandleRef _this, Enums::ToolButtonStyle style);
    Action::HandleRef Handle_addSeparator(HandleRef _this);
    void Handle_addWidget(HandleRef _this, Widget::HandleRef widget);
    void Handle_clear(HandleRef _this);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
