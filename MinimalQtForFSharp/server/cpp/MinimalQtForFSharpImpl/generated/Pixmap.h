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

#include "PaintDevice.h"
using namespace ::PaintDevice;
#include "Common.h"
using namespace ::Common;
#include "Enums.h"
using namespace ::Enums;
#include "Image.h"
using namespace ::Image;

namespace Pixmap
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends PaintDevice::HandleRef
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef


    void Owned_dispose(OwnedRef _this);

    struct FilenameOptions {
    private:
        enum Fields {
            FormatField = 1,
            ImageConversionFlagsField = 2
        };
        int32_t _usedFields = 0;
        std::string _format;
        ImageConversionFlags _imageConversionFlags;
    protected:
        int32_t getUsedFields() {
            return _usedFields;
        }
        friend void FilenameOptions__push(FilenameOptions value, bool isReturn);
        friend FilenameOptions FilenameOptions__pop();
    public:
        void setFormat(std::string value) {
            _format = value;
            _usedFields |= Fields::FormatField;
        }
        bool hasFormat(std::string *value) const {
            if (_usedFields & Fields::FormatField) {
                *value = _format;
                return true;
            }
            return false;
        }
        [[nodiscard]] std::string getOrDefaultFormat(std::string defaultValue) const {
            if (_usedFields & Fields::FormatField) {
                return _format;
            }
            return defaultValue;
        }
        void setImageConversionFlags(ImageConversionFlags value) {
            _imageConversionFlags = value;
            _usedFields |= Fields::ImageConversionFlagsField;
        }
        bool hasImageConversionFlags(ImageConversionFlags *value) const {
            if (_usedFields & Fields::ImageConversionFlagsField) {
                *value = _imageConversionFlags;
                return true;
            }
            return false;
        }
        [[nodiscard]] ImageConversionFlags getOrDefaultImageConversionFlags(ImageConversionFlags defaultValue) const {
            if (_usedFields & Fields::ImageConversionFlagsField) {
                return _imageConversionFlags;
            }
            return defaultValue;
        }
    };
    OwnedRef create(int32_t width, int32_t height);
    OwnedRef create(std::string filename, FilenameOptions opts);
    OwnedRef fromImage(Image::HandleRef image, std::optional<ImageConversionFlags> imageConversionFlags);
}
