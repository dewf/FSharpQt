#include "../support/NativeImplServer.h"
#include "Image_wrappers.h"
#include "Image.h"

#include "PaintDevice_wrappers.h"
using namespace ::PaintDevice;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

namespace Image
{

    void __SizeT_Option__push(std::optional<size_t> value, bool isReturn) {
        if (value.has_value()) {
            ni_pushSizeT(value.value());
            ni_pushBool(true);
        }
        else {
            ni_pushBool(false);
        }
    }

    std::optional<size_t> __SizeT_Option__pop() {
        std::optional<size_t> maybeValue;
        auto hasValue = ni_popBool();
        if (hasValue) {
            maybeValue =  ni_popSizeT();
        }
        return maybeValue;
    }
    inline void __Native_UInt8_Buffer__push(std::shared_ptr<NativeBuffer<uint8_t>> buf, bool isReturn) {
        if (buf != nullptr) {
            auto pushable = std::dynamic_pointer_cast<Pushable>(buf);
            pushable->push(pushable, isReturn);
        }
        else {
            ni_pushNull();
        }
    }

    std::shared_ptr<NativeBuffer<uint8_t>> __Native_UInt8_Buffer__pop() {
        int id;
        bool isClientId;
        ni_BufferDescriptor descriptor;
        ni_popBuffer(&id, &isClientId, &descriptor);
        if (id != 0) {
            if (isClientId) {
                auto buf = new ClientBuffer<uint8_t>(id, descriptor);
                return std::shared_ptr<NativeBuffer<uint8_t>>(buf);
            }
            else {
                return ServerBuffer<uint8_t>::getByID(id);
            }
        }
        else {
            return std::shared_ptr<NativeBuffer<uint8_t>>();
        }
    }

    void __String_Option__push(std::optional<std::string> value, bool isReturn) {
        if (value.has_value()) {
            pushStringInternal(value.value());
            ni_pushBool(true);
        }
        else {
            ni_pushBool(false);
        }
    }

    std::optional<std::string> __String_Option__pop() {
        std::optional<std::string> maybeValue;
        auto hasValue = ni_popBool();
        if (hasValue) {
            maybeValue =  popStringInternal();
        }
        return maybeValue;
    }
    void Format__push(Format value) {
        ni_pushInt32((int32_t)value);
    }

    Format Format__pop() {
        auto tag = ni_popInt32();
        return (Format)tag;
    }
    void ScaledOptions__push(ScaledOptions value, bool isReturn) {
        TransformationMode transformMode;
        if (value.hasTransformMode(&transformMode)) {
            TransformationMode__push(transformMode);
        }
        AspectRatioMode aspectMode;
        if (value.hasAspectMode(&aspectMode)) {
            AspectRatioMode__push(aspectMode);
        }
        ni_pushInt32(value.getUsedFields());
    }

    ScaledOptions ScaledOptions__pop() {
        ScaledOptions value = {};
        value._usedFields =  ni_popInt32();
        if (value._usedFields & ScaledOptions::Fields::AspectModeField) {
            auto x = AspectRatioMode__pop();
            value.setAspectMode(x);
        }
        if (value._usedFields & ScaledOptions::Fields::TransformModeField) {
            auto x = TransformationMode__pop();
            value.setTransformMode(x);
        }
        return value;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_scaled__wrapper() {
        auto _this = Handle__pop();
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        auto opts = ScaledOptions__pop();
        Owned__push(Handle_scaled(_this, width, height, opts));
    }
    void Owned__push(OwnedRef value) {
        ni_pushPtr(value);
    }

    OwnedRef Owned__pop() {
        return (OwnedRef)ni_popPtr();
    }

    void Owned_dispose__wrapper() {
        auto _this = Owned__pop();
        Owned_dispose(_this);
    }

    class Deferred_PushVisitor : public Deferred::Visitor {
    private:
        bool isReturn;
    public:
        Deferred_PushVisitor(bool isReturn) : isReturn(isReturn) {}
        void onFromHandle(const Deferred::FromHandle* fromHandleValue) override {
            Handle__push(fromHandleValue->handle);
            // kind:
            ni_pushInt32(0);
        }
        void onFromWidthHeight(const Deferred::FromWidthHeight* fromWidthHeightValue) override {
            Format__push(fromWidthHeightValue->format);
            ni_pushInt32(fromWidthHeightValue->height);
            ni_pushInt32(fromWidthHeightValue->width);
            // kind:
            ni_pushInt32(1);
        }
        void onFromFilename(const Deferred::FromFilename* fromFilenameValue) override {
            __String_Option__push(fromFilenameValue->format, isReturn);
            pushStringInternal(fromFilenameValue->filename);
            // kind:
            ni_pushInt32(2);
        }
        void onFromData(const Deferred::FromData* fromDataValue) override {
            __SizeT_Option__push(fromDataValue->bytesPerLine, isReturn);
            Format__push(fromDataValue->format);
            ni_pushInt32(fromDataValue->height);
            ni_pushInt32(fromDataValue->width);
            __Native_UInt8_Buffer__push(fromDataValue->data, isReturn);
            // kind:
            ni_pushInt32(3);
        }
    };

    void Deferred__push(std::shared_ptr<Deferred::Base> value, bool isReturn) {
        Deferred_PushVisitor v(isReturn);
        value->accept((Deferred::Visitor*)&v);
    }

    std::shared_ptr<Deferred::Base> Deferred__pop() {
        Deferred::Base* __ret = nullptr;
        switch (ni_popInt32()) {
        case 0: {
            auto handle = Handle__pop();
            __ret = new Deferred::FromHandle(handle);
            break;
        }
        case 1: {
            auto width = ni_popInt32();
            auto height = ni_popInt32();
            auto format = Format__pop();
            __ret = new Deferred::FromWidthHeight(width, height, format);
            break;
        }
        case 2: {
            auto filename = popStringInternal();
            auto format = __String_Option__pop();
            __ret = new Deferred::FromFilename(filename, format);
            break;
        }
        case 3: {
            auto data = __Native_UInt8_Buffer__pop();
            auto width = ni_popInt32();
            auto height = ni_popInt32();
            auto format = Format__pop();
            auto bytesPerLine = __SizeT_Option__pop();
            __ret = new Deferred::FromData(data, width, height, format, bytesPerLine);
            break;
        }
        default:
            printf("C++ Deferred__pop() - unknown kind! returning null\n");
        }
        return std::shared_ptr<Deferred::Base>(__ret);
    }

    void realize__wrapper() {
        auto deferred = Deferred__pop();
        Owned__push(realize(deferred));
    }

    int __register() {
        auto m = ni_registerModule("Image");
        ni_registerModuleMethod(m, "realize", &realize__wrapper);
        ni_registerModuleMethod(m, "Handle_scaled", &Handle_scaled__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
