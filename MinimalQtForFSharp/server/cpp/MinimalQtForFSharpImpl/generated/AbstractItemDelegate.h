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
#include "Widget.h"
using namespace ::Widget;
#include "ModelIndex.h"
using namespace ::ModelIndex;

namespace AbstractItemDelegate
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Object::HandleRef

    typedef int32_t SignalMask;
    enum SignalMaskFlags: int32_t {
        // Object::SignalMask:
        Destroyed = 1 << 0,
        ObjectNameChanged = 1 << 1,
        // SignalMask:
        CloseEditor = 1 << 2,
        CommitData = 1 << 3,
        SizeHintChanged = 1 << 4
    };

    enum class EndEditHint {
        NoHint,
        EditNextItem,
        EditPreviousItem,
        SubmitModelCache,
        RevertModelCache
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void closeEditor(Widget::HandleRef editor, EndEditHint hint) = 0;
        virtual void commitData(Widget::HandleRef editor) = 0;
        virtual void sizeHintChanged(ModelIndex::HandleRef index) = 0;
    };

    void Handle_dispose(HandleRef _this);
}
