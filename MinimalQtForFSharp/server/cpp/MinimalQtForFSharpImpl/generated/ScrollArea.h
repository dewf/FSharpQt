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
#include "AbstractScrollArea.h"
using namespace ::AbstractScrollArea;

namespace ScrollArea
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractScrollArea::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // Widget::SignalMask:
        CustomContextMenuRequested = 1 << 2,
        WindowIconChanged = 1 << 3,
        WindowTitleChanged = 1 << 4,
        // Frame::SignalMask:
        // AbstractScrollArea::SignalMask:
        // SignalMask:
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
    };

    void Handle_setAlignment(HandleRef _this, Alignment align);
    void Handle_setWidgetResizable(HandleRef _this, bool resizable);
    void Handle_setWidget(HandleRef _this, Widget::HandleRef widget);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
