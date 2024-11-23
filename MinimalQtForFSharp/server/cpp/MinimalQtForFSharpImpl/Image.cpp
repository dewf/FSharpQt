#include "generated/Image.h"

#include "ImageInternal.h"

namespace Image
{
    OwnedRef Handle_scaled(HandleRef _this, int32_t width, int32_t height, ScaledOptions opts) {
        const auto qAspectMode =
            static_cast<Qt::AspectRatioMode>(
                opts.getOrDefaultAspectMode(AspectRatioMode::IgnoreAspectRatio));

        const auto qTransformMode =
            static_cast<Qt::TransformationMode>(
                opts.getOrDefaultTransformMode(TransformationMode::FastTransformation));

        return new __Owned { _this->qImage.scaled(width, height, qAspectMode, qTransformMode) };
    }

    void Owned_dispose(OwnedRef _this) {
        delete _this;
    }

    OwnedRef create(int32_t width, int32_t height, Format format) {
        return new __Owned { QImage(width, height, static_cast<QImage::Format>(format)) };
    }

    OwnedRef create(std::string filename, std::optional<std::string> format) {
        const char *qformat = nullptr;
        if (format.has_value()) {
            qformat = format->c_str();
        }
        return new __Owned { QImage(filename.c_str(), qformat) };
    }

    struct CleanupInfo {
        // keep the buffer alive until the Image is done with it
        std::shared_ptr<NativeBuffer<uint8_t>> buffer;
    };

    static void cleanupFunc(void *info) {
        const auto ptr = static_cast<CleanupInfo*>(info);
        delete ptr;
    }

    OwnedRef create(std::shared_ptr<NativeBuffer<uint8_t>> data, int32_t width, int32_t height, Format format, std::optional<size_t> bytesPerLine) {
        size_t length;
        const auto qdata = data->getSpan(&length);
        const auto qformat = static_cast<QImage::Format>(format);
        const auto cleanupPtr = new CleanupInfo { data };
        if (bytesPerLine.has_value()) {
            const auto qbytesPerLine = static_cast<qsizetype>(bytesPerLine.value());
            return new __Owned { QImage(qdata, width, height, qbytesPerLine, qformat, cleanupFunc, cleanupPtr) };
        } else {
            return new __Owned { QImage(qdata, width, height, qformat, cleanupFunc, cleanupPtr) };
        }
    }
}
