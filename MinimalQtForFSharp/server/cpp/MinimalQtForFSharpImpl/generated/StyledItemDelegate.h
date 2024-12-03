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

#include "AbstractItemDelegate.h"
using namespace ::AbstractItemDelegate;
#include "Object.h"
using namespace ::Object;
#include "Common.h"
using namespace ::Common;
#include "Widget.h"
using namespace ::Widget;
#include "AbstractItemModel.h"
using namespace ::AbstractItemModel;
#include "ModelIndex.h"
using namespace ::ModelIndex;
#include "StyleOptionViewItem.h"
using namespace ::StyleOptionViewItem;

namespace StyledItemDelegate
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends AbstractItemDelegate::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // AbstractItemDelegate::SignalMask:
        CloseEditor = 1 << 2,
        CommitData = 1 << 3,
        SizeHintChanged = 1 << 4,
        // SignalMask:
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void closeEditor(Widget::HandleRef editor, EndEditHint hint) = 0;
        virtual void commitData(Widget::HandleRef editor) = 0;
        virtual void sizeHintChanged(ModelIndex::HandleRef index) = 0;
    };

    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);

    typedef int32_t MethodMask;
    enum MethodMaskFlags: int32_t {
        // MethodMask:
        CreateEditor = 1 << 0,
        SetEditorData = 1 << 1,
        SetModelData = 1 << 2,
        DestroyEditor = 1 << 3
    };

    class MethodDelegate {
    public:
        virtual Widget::HandleRef createEditor(Widget::HandleRef parent, StyleOptionViewItem::HandleRef option, ModelIndex::HandleRef index) = 0;
        virtual void setEditorData(Widget::HandleRef editor, ModelIndex::HandleRef index) = 0;
        virtual void setModelData(Widget::HandleRef editor, AbstractItemModel::HandleRef model, ModelIndex::HandleRef index) = 0;
        virtual void destroyEditor(Widget::HandleRef editor, ModelIndex::HandleRef index) = 0;
    };
    HandleRef createdSubclassed(std::shared_ptr<MethodDelegate> methodDelegate, MethodMask methodMask, std::shared_ptr<SignalHandler> handler);
}
