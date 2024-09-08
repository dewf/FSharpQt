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
#include "Widget.h"
using namespace ::Widget;
#include "Variant.h"
using namespace ::Variant;
#include "AbstractItemModel.h"
using namespace ::AbstractItemModel;
#include "Icon.h"
using namespace ::Icon;

namespace ComboBox
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
        CurrentIndexChanged = 1 << 6,
        CurrentTextChanged = 1 << 7,
        EditTextChanged = 1 << 8,
        Highlighted = 1 << 9,
        TextActivated = 1 << 10,
        TextHighlighted = 1 << 11
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void activated(int32_t index) = 0;
        virtual void currentIndexChanged(int32_t index) = 0;
        virtual void currentTextChanged(std::string text) = 0;
        virtual void editTextChanged(std::string text) = 0;
        virtual void highlighted(int32_t index) = 0;
        virtual void textActivated(std::string text) = 0;
        virtual void textHighlighted(std::string text) = 0;
    };

    enum class InsertPolicy {
        NoInsert,
        InsertAtTop,
        InsertAtCurrent,
        InsertAtBottom,
        InsertAfterCurrent,
        InsertBeforeCurrent,
        InsertAlphabetically
    };

    enum class SizeAdjustPolicy {
        AdjustToContents,
        AdjustToContentsOnFirstShow,
        AdjustToMinimumContentsLengthWithIcon
    };

    int32_t Handle_count(HandleRef _this);
    Variant::OwnedHandleRef Handle_currentData(HandleRef _this);
    Variant::OwnedHandleRef Handle_currentData(HandleRef _this, Enums::ItemDataRole role);
    int32_t Handle_currentIndex(HandleRef _this);
    void Handle_setCurrentIndex(HandleRef _this, int32_t index);
    void Handle_setCurrentText(HandleRef _this, std::string text);
    void Handle_setDuplicatesEnabled(HandleRef _this, bool enabled);
    void Handle_setEditable(HandleRef _this, bool editable);
    void Handle_setFrame(HandleRef _this, bool hasFrame);
    void Handle_setIconSize(HandleRef _this, Common::Size size);
    void Handle_setInsertPolicy(HandleRef _this, InsertPolicy policy);
    void Handle_setMaxCount(HandleRef _this, int32_t count);
    void Handle_setMaxVisibleItems(HandleRef _this, int32_t count);
    void Handle_setMinimumContentsLength(HandleRef _this, int32_t length);
    void Handle_setModelColumn(HandleRef _this, int32_t column);
    void Handle_setPlaceholderText(HandleRef _this, std::string text);
    void Handle_setSizeAdjustPolicy(HandleRef _this, SizeAdjustPolicy policy);
    void Handle_clear(HandleRef _this);
    void Handle_addItem(HandleRef _this, std::string text, std::shared_ptr<Variant::Deferred::Base> userData);
    void Handle_addItem(HandleRef _this, std::shared_ptr<Icon::Deferred::Base> icon, std::string text, std::shared_ptr<Variant::Deferred::Base> userData);
    void Handle_addItems(HandleRef _this, std::vector<std::string> texts);
    void Handle_setModel(HandleRef _this, AbstractItemModel::HandleRef model);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
    HandleRef downcastFrom(Widget::HandleRef widget);
}
