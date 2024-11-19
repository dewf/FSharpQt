#include "generated/Image.h"

#include "ImageInternal.h"

namespace Image
{
    class FromDeferred : public Deferred::Visitor {
        QImage &image;
    public:
        explicit FromDeferred(QImage &image) : image(image) {}

        void onFromHandle(const Deferred::FromHandle *value) override {
            image = value->handle->qImage;
        }

        void onFromWidthHeight(const Deferred::FromWidthHeight *value) override {
            image = QImage(value->width, value->height, static_cast<QImage::Format>(value->format));
        }

        void onFromFilename(const Deferred::FromFilename *value) override {
            const char *format = nullptr;
            if (value->format.has_value()) {
                format = value->format->c_str();
            }
            image = QImage(value->filename.c_str(), format);
        }

        struct CleanupInfo {
            // keep the buffer alive until the Image is done with it
            std::shared_ptr<NativeBuffer<uint8_t>> buffer;
        };

        static void cleanupFunc(void *info) {
            const auto ptr = static_cast<CleanupInfo*>(info);
            delete ptr;
        }

        void onFromData(const Deferred::FromData *value) override {
            size_t length;
            const auto data = value->data->getSpan(&length);
            const auto format = static_cast<QImage::Format>(value->format);
            const auto cleanupPtr = new CleanupInfo { value->data };
            if (value->bytesPerLine.has_value()) {
                const auto bytesPerLine = static_cast<qsizetype>(value->bytesPerLine.value());
                image = QImage(data, value->width, value->height, bytesPerLine, format, cleanupFunc, cleanupPtr);
            } else {
                image = QImage(data, value->width, value->height, format, cleanupFunc, cleanupPtr);
            }
        }
    };

    QImage fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QImage image;
        FromDeferred visitor(image);
        deferred->accept(&visitor);
        return image;
    }

    // ===========================================================

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

    OwnedRef realize(std::shared_ptr<Deferred::Base> deferred) {
        return new __Owned { fromDeferred(deferred) };
    }
}
