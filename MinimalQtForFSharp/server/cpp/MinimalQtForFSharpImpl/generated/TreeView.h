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
#include "AbstractItemView.h"
using namespace ::AbstractItemView;
#include "ModelIndex.h"
using namespace ::ModelIndex;

namespace TreeView
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractItemView::HandleRef

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
        // AbstractItemView::SignalMask:
        Activated = 1 << 5,
        Clicked = 1 << 6,
        DoubleClickedBit = 1 << 7,
        Entered = 1 << 8,
        IconSizeChanged = 1 << 9,
        Pressed = 1 << 10,
        ViewportEntered = 1 << 11,
        // SignalMask:
        Collapsed = 1 << 12,
        Expanded = 1 << 13
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void activated(ModelIndex::HandleRef index) = 0;
        virtual void clicked(ModelIndex::HandleRef index) = 0;
        virtual void doubleClicked(ModelIndex::HandleRef index) = 0;
        virtual void entered(ModelIndex::HandleRef index) = 0;
        virtual void iconSizeChanged(Size size) = 0;
        virtual void pressed(ModelIndex::HandleRef index) = 0;
        virtual void viewportEntered() = 0;
        virtual void collapsed(ModelIndex::HandleRef index) = 0;
        virtual void expanded(ModelIndex::HandleRef index) = 0;
    };

    void Handle_setAllColumnsShowFocus(HandleRef _this, bool value);
    void Handle_setAnimated(HandleRef _this, bool value);
    void Handle_setAutoExpandDelay(HandleRef _this, int32_t value);
    void Handle_setExpandsOnDoubleClick(HandleRef _this, bool value);
    void Handle_setHeaderHidden(HandleRef _this, bool value);
    void Handle_setIndentation(HandleRef _this, int32_t value);
    void Handle_setItemsExpandable(HandleRef _this, bool value);
    void Handle_setRootIsDecorated(HandleRef _this, bool value);
    void Handle_setSortingEnabled(HandleRef _this, bool value);
    void Handle_setUniformRowHeights(HandleRef _this, bool value);
    void Handle_setWordWrap(HandleRef _this, bool value);
    void Handle_resizeColumnToContents(HandleRef _this, int32_t column);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
