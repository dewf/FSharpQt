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

namespace Image
{

    struct __Handle; typedef struct __Handle* HandleRef; // extends PaintDevice::HandleRef
    struct __Owned; typedef struct __Owned* OwnedRef; // extends HandleRef

    enum class Format {
        Invalid,
        Mono,
        MonoLSB,
        Indexed8,
        RGB32,
        ARGB32,
        ARGB32_Premultiplied,
        RGB16,
        ARGB8565_Premultiplied,
        RGB666,
        ARGB6666_Premultiplied,
        RGB555,
        ARGB8555_Premultiplied,
        RGB888,
        RGB444,
        ARGB4444_Premultiplied,
        RGBX8888,
        RGBA8888,
        RGBA8888_Premultiplied,
        BGR30,
        A2BGR30_Premultiplied,
        RGB30,
        A2RGB30_Premultiplied,
        Alpha8,
        Grayscale8,
        RGBX64,
        RGBA64,
        RGBA64_Premultiplied,
        Grayscale16,
        BGR888,
        RGBX16FPx4,
        RGBA16FPx4,
        RGBA16FPx4_Premultiplied,
        RGBX32FPx4,
        RGBA32FPx4,
        RGBA32FPx4_Premultiplied,
        CMYK8888,
        NImageFormats
    };

    struct ScaledOptions {
    private:
        enum Fields {
            AspectModeField = 1,
            TransformModeField = 2
        };
        int32_t _usedFields = 0;
        Enums::AspectRatioMode _aspectMode;
        Enums::TransformationMode _transformMode;
    protected:
        int32_t getUsedFields() {
            return _usedFields;
        }
        friend void ScaledOptions__push(ScaledOptions value, bool isReturn);
        friend ScaledOptions ScaledOptions__pop();
    public:
        void setAspectMode(Enums::AspectRatioMode value) {
            _aspectMode = value;
            _usedFields |= Fields::AspectModeField;
        }
        bool hasAspectMode(Enums::AspectRatioMode *value) const {
            if (_usedFields & Fields::AspectModeField) {
                *value = _aspectMode;
                return true;
            }
            return false;
        }
        [[nodiscard]] Enums::AspectRatioMode getOrDefaultAspectMode(Enums::AspectRatioMode defaultValue) const {
            if (_usedFields & Fields::AspectModeField) {
                return _aspectMode;
            }
            return defaultValue;
        }
        void setTransformMode(Enums::TransformationMode value) {
            _transformMode = value;
            _usedFields |= Fields::TransformModeField;
        }
        bool hasTransformMode(Enums::TransformationMode *value) const {
            if (_usedFields & Fields::TransformModeField) {
                *value = _transformMode;
                return true;
            }
            return false;
        }
        [[nodiscard]] Enums::TransformationMode getOrDefaultTransformMode(Enums::TransformationMode defaultValue) const {
            if (_usedFields & Fields::TransformModeField) {
                return _transformMode;
            }
            return defaultValue;
        }
    };

    OwnedRef Handle_scaled(HandleRef _this, int32_t width, int32_t height, ScaledOptions opts);

    void Owned_dispose(OwnedRef _this);
    OwnedRef create(int32_t width, int32_t height, Format format);
    OwnedRef create(std::string filename, std::optional<std::string> format);
    OwnedRef create(std::shared_ptr<NativeBuffer<uint8_t>> data, int32_t width, int32_t height, Format format, std::optional<size_t> bytesPerLine);
}
