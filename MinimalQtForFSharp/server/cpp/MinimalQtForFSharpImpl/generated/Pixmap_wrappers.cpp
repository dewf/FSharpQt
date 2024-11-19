#include "../support/NativeImplServer.h"
#include "Pixmap_wrappers.h"
#include "Pixmap.h"

#include "PaintDevice_wrappers.h"
using namespace ::PaintDevice;

#include "Common_wrappers.h"
using namespace ::Common;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "Image_wrappers.h"
using namespace ::Image;

namespace Pixmap
{

    void __ImageConversionFlags_Option__push(std::optional<ImageConversionFlags> value, bool isReturn) {
        if (value.has_value()) {
            ImageConversionFlags__push(value.value());
            ni_pushBool(true);
        }
        else {
            ni_pushBool(false);
        }
    }

    std::optional<ImageConversionFlags> __ImageConversionFlags_Option__pop() {
        std::optional<ImageConversionFlags> maybeValue;
        auto hasValue = ni_popBool();
        if (hasValue) {
            maybeValue =  ImageConversionFlags__pop();
        }
        return maybeValue;
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_width__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_width(_this));
    }

    void Handle_height__wrapper() {
        auto _this = Handle__pop();
        ni_pushInt32(Handle_height(_this));
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
    void FilenameOptions__push(FilenameOptions value, bool isReturn) {
        ImageConversionFlags imageConversionFlags;
        if (value.hasImageConversionFlags(&imageConversionFlags)) {
            ImageConversionFlags__push(imageConversionFlags);
        }
        std::string format;
        if (value.hasFormat(&format)) {
            pushStringInternal(format);
        }
        ni_pushInt32(value.getUsedFields());
    }

    FilenameOptions FilenameOptions__pop() {
        FilenameOptions value = {};
        value._usedFields =  ni_popInt32();
        if (value._usedFields & FilenameOptions::Fields::FormatField) {
            auto x = popStringInternal();
            value.setFormat(x);
        }
        if (value._usedFields & FilenameOptions::Fields::ImageConversionFlagsField) {
            auto x = ImageConversionFlags__pop();
            value.setImageConversionFlags(x);
        }
        return value;
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
            ni_pushInt32(fromWidthHeightValue->height);
            ni_pushInt32(fromWidthHeightValue->width);
            // kind:
            ni_pushInt32(1);
        }
        void onFromFilename(const Deferred::FromFilename* fromFilenameValue) override {
            FilenameOptions__push(fromFilenameValue->opts, isReturn);
            pushStringInternal(fromFilenameValue->filename);
            // kind:
            ni_pushInt32(2);
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
            __ret = new Deferred::FromWidthHeight(width, height);
            break;
        }
        case 2: {
            auto filename = popStringInternal();
            auto opts = FilenameOptions__pop();
            __ret = new Deferred::FromFilename(filename, opts);
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

    void fromImage__wrapper() {
        auto image = Image::Deferred__pop();
        auto imageConversionFlags = __ImageConversionFlags_Option__pop();
        Owned__push(fromImage(image, imageConversionFlags));
    }

    int __register() {
        auto m = ni_registerModule("Pixmap");
        ni_registerModuleMethod(m, "realize", &realize__wrapper);
        ni_registerModuleMethod(m, "fromImage", &fromImage__wrapper);
        ni_registerModuleMethod(m, "Handle_width", &Handle_width__wrapper);
        ni_registerModuleMethod(m, "Handle_height", &Handle_height__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
