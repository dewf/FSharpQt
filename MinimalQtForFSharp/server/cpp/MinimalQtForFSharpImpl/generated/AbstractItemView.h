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
#include "AbstractScrollArea.h"
using namespace ::AbstractScrollArea;
#include "AbstractItemModel.h"
using namespace ::AbstractItemModel;
#include "ModelIndex.h"
using namespace ::ModelIndex;
#include "AbstractItemDelegate.h"
using namespace ::AbstractItemDelegate;

namespace AbstractItemView
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
        Activated = 1 << 5,
        Clicked = 1 << 6,
        DoubleClickedBit = 1 << 7,
        Entered = 1 << 8,
        IconSizeChanged = 1 << 9,
        Pressed = 1 << 10,
        ViewportEntered = 1 << 11
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
    };

    enum class DragDropMode {
        NoDragDrop,
        DragOnly,
        DropOnly,
        DragDrop,
        InternalMove
    };

    typedef int32_t EditTriggers;
    enum EditTriggersFlags : int32_t {
        NoEditTriggers = 0,
        CurrentChanged = 1,
        DoubleClicked = 2,
        SelectedClicked = 4,
        EditKeyPressed = 8,
        AnyKeyPressed = 16,
        AllEditTriggers = 31
    };

    enum class ScrollMode {
        ScrollPerItem,
        ScrollPerPixel
    };

    enum class SelectionBehavior {
        SelectItems,
        SelectRows,
        SelectColumns
    };

    enum class SelectionMode {
        NoSelection,
        SingleSelection,
        MultiSelection,
        ExtendedSelection,
        ContiguousSelection
    };

    void Handle_setAlternatingRowColors(HandleRef _this, bool state);
    void Handle_setAutoScroll(HandleRef _this, bool state);
    void Handle_setAutoScrollMargin(HandleRef _this, int32_t margin);
    void Handle_setDefaultDropAction(HandleRef _this, Enums::DropAction action);
    void Handle_setDragDropMode(HandleRef _this, DragDropMode mode);
    void Handle_setDragDropOverwriteMode(HandleRef _this, bool mode);
    void Handle_setDragEnabled(HandleRef _this, bool enabled);
    void Handle_setEditTriggers(HandleRef _this, EditTriggers triggers);
    void Handle_setHorizontalScrollMode(HandleRef _this, ScrollMode mode);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setSelectionBehavior(HandleRef _this, SelectionBehavior behavior);
    void Handle_setSelectionMode(HandleRef _this, SelectionMode mode);
    void Handle_setDropIndicatorShown(HandleRef _this, bool state);
    void Handle_setTabKeyNavigation(HandleRef _this, bool state);
    void Handle_setTextElideMode(HandleRef _this, Enums::TextElideMode mode);
    void Handle_setVerticalScrollMode(HandleRef _this, ScrollMode mode);
    void Handle_setModel(HandleRef _this, AbstractItemModel::HandleRef model);
    void Handle_setItemDelegate(HandleRef _this, AbstractItemDelegate::HandleRef itemDelegate);
    void Handle_setItemDelegateForColumn(HandleRef _this, int32_t column, AbstractItemDelegate::HandleRef itemDelegate);
    void Handle_setItemDelegateForRow(HandleRef _this, int32_t row, AbstractItemDelegate::HandleRef itemDelegate);
}
