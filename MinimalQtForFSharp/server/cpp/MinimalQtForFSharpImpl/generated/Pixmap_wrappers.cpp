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

    void create__wrapper() {
        auto width = ni_popInt32();
        auto height = ni_popInt32();
        Owned__push(create(width, height));
    }

    void create_overload1__wrapper() {
        auto filename = popStringInternal();
        auto opts = FilenameOptions__pop();
        Owned__push(create(filename, opts));
    }

    void fromImage__wrapper() {
        auto image = Image::Handle__pop();
        auto imageConversionFlags = __ImageConversionFlags_Option__pop();
        Owned__push(fromImage(image, imageConversionFlags));
    }

    int __register() {
        auto m = ni_registerModule("Pixmap");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "create_overload1", &create_overload1__wrapper);
        ni_registerModuleMethod(m, "fromImage", &fromImage__wrapper);
        ni_registerModuleMethod(m, "Handle_width", &Handle_width__wrapper);
        ni_registerModuleMethod(m, "Handle_height", &Handle_height__wrapper);
        ni_registerModuleMethod(m, "Owned_dispose", &Owned_dispose__wrapper);
        return 0; // = OK
    }
}
