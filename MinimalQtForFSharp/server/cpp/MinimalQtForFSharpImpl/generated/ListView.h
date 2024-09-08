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
#include "Enums.h"
using namespace ::Enums;
#include "Icon.h"
using namespace ::Icon;
#include "AbstractListModel.h"
using namespace ::AbstractListModel;
#include "ModelIndex.h"
using namespace ::ModelIndex;
#include "AbstractItemView.h"
using namespace ::AbstractItemView;

namespace ListView
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
        IndexesMoved = 1 << 12
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void activated(ModelIndex::HandleRef index) = 0;
        virtual void clicked(ModelIndex::HandleRef index) = 0;
        virtual void doubleClicked(ModelIndex::HandleRef index) = 0;
        virtual void entered(ModelIndex::HandleRef index) = 0;
        virtual void iconSizeChanged(Common::Size size) = 0;
        virtual void pressed(ModelIndex::HandleRef index) = 0;
        virtual void viewportEntered() = 0;
        virtual void indexesMoved(std::vector<ModelIndex::HandleRef> indexes) = 0;
    };

    enum class Movement {
        Static,
        Free,
        Snap
    };

    enum class Flow {
        LeftToRight,
        TopToBottom
    };

    enum class ResizeMode {
        Fixed,
        Adjust
    };

    enum class LayoutMode {
        SinglePass,
        Batched
    };

    enum class ViewMode {
        ListMode,
        IconMode
    };

    void Handle_setBatchSize(HandleRef _this, int32_t size);
    void Handle_setFlow(HandleRef _this, Flow flow);
    void Handle_setGridSize(HandleRef _this, Common::Size size);
    void Handle_setWrapping(HandleRef _this, bool wrapping);
    void Handle_setItemAlignment(HandleRef _this, Enums::Alignment align);
    void Handle_setLayoutMode(HandleRef _this, LayoutMode mode);
    void Handle_setModelColumn(HandleRef _this, int32_t column);
    void Handle_setMovement(HandleRef _this, Movement value);
    void Handle_setResizeMode(HandleRef _this, ResizeMode mode);
    void Handle_setSelectionRectVisible(HandleRef _this, bool visible);
    void Handle_setSpacing(HandleRef _this, int32_t spacing);
    void Handle_setUniformItemSizes(HandleRef _this, bool state);
    void Handle_setViewMode(HandleRef _this, ViewMode mode);
    void Handle_setWordWrap(HandleRef _this, bool state);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
}
