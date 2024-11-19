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

    namespace Deferred {
        class Base;
    }

    int32_t Handle_width(HandleRef _this);
    int32_t Handle_height(HandleRef _this);

    void Owned_dispose(OwnedRef _this);

    struct FilenameOptions {
    private:
        enum Fields {
            FormatField = 1,
            ImageConversionFlagsField = 2
        };
        int32_t _usedFields = 0;
        std::string _format;
        Enums::ImageConversionFlags _imageConversionFlags;
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
        void setImageConversionFlags(Enums::ImageConversionFlags value) {
            _imageConversionFlags = value;
            _usedFields |= Fields::ImageConversionFlagsField;
        }
        bool hasImageConversionFlags(Enums::ImageConversionFlags *value) const {
            if (_usedFields & Fields::ImageConversionFlagsField) {
                *value = _imageConversionFlags;
                return true;
            }
            return false;
        }
        [[nodiscard]] Enums::ImageConversionFlags getOrDefaultImageConversionFlags(Enums::ImageConversionFlags defaultValue) const {
            if (_usedFields & Fields::ImageConversionFlagsField) {
                return _imageConversionFlags;
            }
            return defaultValue;
        }
    };

    namespace Deferred {
        class FromHandle;
        class FromWidthHeight;
        class FromFilename;

        class Visitor {
        public:
            virtual void onFromHandle(const FromHandle* value) = 0;
            virtual void onFromWidthHeight(const FromWidthHeight* value) = 0;
            virtual void onFromFilename(const FromFilename* value) = 0;
        };

        class Base {
        public:
            virtual void accept(Visitor* visitor) = 0;
        };

        class FromHandle : public Base {
        public:
            const HandleRef handle;
            FromHandle(HandleRef handle) : handle(handle) {}
            void accept(Visitor* visitor) override {
                visitor->onFromHandle(this);
            }
        };

        class FromWidthHeight : public Base {
        public:
            const int32_t width;
            const int32_t height;
            FromWidthHeight(int32_t width, int32_t height) : width(width), height(height) {}
            void accept(Visitor* visitor) override {
                visitor->onFromWidthHeight(this);
            }
        };

        class FromFilename : public Base {
        public:
            const std::string filename;
            const FilenameOptions opts;
            FromFilename(std::string filename, FilenameOptions opts) : filename(filename), opts(opts) {}
            void accept(Visitor* visitor) override {
                visitor->onFromFilename(this);
            }
        };
    }
    OwnedRef realize(std::shared_ptr<Deferred::Base> deferred);
    OwnedRef fromImage(std::shared_ptr<Image::Deferred::Base> image, std::optional<Enums::ImageConversionFlags> imageConversionFlags);
}
