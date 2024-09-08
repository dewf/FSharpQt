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

namespace Frame
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
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
    };

    enum class Shape {
        NoFrame = 0,
        Box = 0x1,
        Panel = 0x2,
        StyledPanel = 0x6,
        HLine = 0x4,
        VLine = 0x5,
        WinPanel = 0x3
    };

    enum class Shadow {
        Plain = 0x10,
        Raised = 0x20,
        Sunken = 0x30
    };

    void Handle_setFrameRect(HandleRef _this, Common::Rect rect);
    void Handle_setFrameShadow(HandleRef _this, Shadow shadow);
    void Handle_setFrameShape(HandleRef _this, Shape shape);
    int32_t Handle_frameWidth(HandleRef _this);
    void Handle_setLineWidth(HandleRef _this, int32_t width);
    void Handle_setMidLineWidth(HandleRef _this, int32_t width);
    void Handle_setFrameStyle(HandleRef _this, Shape shape, Shadow shadow);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
