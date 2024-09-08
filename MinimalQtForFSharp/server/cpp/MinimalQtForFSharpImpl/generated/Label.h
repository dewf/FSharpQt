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
#include "Frame.h"
using namespace ::Frame;
#include "Icon.h"
using namespace ::Icon;
#include "Enums.h"
using namespace ::Enums;
#include "Pixmap.h"
using namespace ::Pixmap;

namespace Label
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends Frame::HandleRef

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
        // SignalMask:
        LinkActivated = 1 << 5,
        LinkHovered = 1 << 6
    };

    class SignalHandler {
    public:
        virtual void destroyed(Object::HandleRef obj) = 0;
        virtual void objectNameChanged(std::string objectName) = 0;
        virtual void customContextMenuRequested(Common::Point pos) = 0;
        virtual void windowIconChanged(Icon::HandleRef icon) = 0;
        virtual void windowTitleChanged(std::string title) = 0;
        virtual void linkActivated(std::string link) = 0;
        virtual void linkHovered(std::string link) = 0;
    };

    void Handle_setAlignment(HandleRef _this, Enums::Alignment align);
    bool Handle_hasSelectedText(HandleRef _this);
    void Handle_setIndent(HandleRef _this, int32_t indent);
    void Handle_setMargin(HandleRef _this, int32_t margin);
    void Handle_setOpenExternalLinks(HandleRef _this, bool state);
    void Handle_setPixmap(HandleRef _this, std::shared_ptr<Pixmap::Deferred::Base> pixmap);
    void Handle_setScaledContents(HandleRef _this, bool state);
    std::string Handle_selectedText(HandleRef _this);
    void Handle_setText(HandleRef _this, std::string text);
    void Handle_setTextFormat(HandleRef _this, Enums::TextFormat format);
    void Handle_setTextInteractionFlags(HandleRef _this, Enums::TextInteractionFlags interactionFlags);
    void Handle_setWordWrap(HandleRef _this, bool state);
    void Handle_setSignalMask(HandleRef _this, SignalMask mask);
    void Handle_dispose(HandleRef _this);
    HandleRef create(std::shared_ptr<SignalHandler> handler);
    HandleRef createNoHandler();
}
