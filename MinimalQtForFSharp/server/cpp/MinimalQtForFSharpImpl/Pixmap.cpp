#include "generated/Pixmap.h"

#include <QPixmap>

#include "ImageInternal.h"
#include "PixmapInternal.h"

namespace Pixmap
{
    class FromDeferred : public Pixmap::Deferred::Visitor {
    private:
        QPixmap& pixmap;
    public:
        explicit FromDeferred(QPixmap &pixmap) : pixmap(pixmap) {}

        void onFromHandle(const Deferred::FromHandle *value) override {
            pixmap = value->handle->qPixmap;
        }

        void onFromWidthHeight(const Deferred::FromWidthHeight *value) override {
            pixmap = QPixmap(value->width, value->height);
        }

        void onFromFilename(const Deferred::FromFilename *value) override {
            const char *qFormat = nullptr;
            std::string format;
            if (value->opts.hasFormat(&format)) {
                qFormat = format.c_str();
            }
            const auto qImageConversionFlags = static_cast<Qt::ImageConversionFlags>(value->opts.getOrDefaultImageConversionFlags(AutoColor));
            pixmap = QPixmap(value->filename.c_str(), qFormat, qImageConversionFlags);
        }
    };

    QPixmap fromDeferred(const std::shared_ptr<Pixmap::Deferred::Base>& deferred) {
        QPixmap ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }

    // ==============================================================

    int32_t Handle_width(HandleRef _this) {
        return _this->qPixmap.width();
    }

    int32_t Handle_height(HandleRef _this) {
        return _this->qPixmap.height();
    }

    void Owned_dispose(OwnedRef _this) {
        delete _this;
    }

    OwnedRef realize(std::shared_ptr<Deferred::Base> deferred) {
        return new __Owned { fromDeferred(deferred) };
    }

    OwnedRef fromImage(std::shared_ptr<Image::Deferred::Base> image, std::optional<ImageConversionFlags> imageConversionFlags) {
        const auto qFlags = static_cast<Qt::ImageConversionFlags>(imageConversionFlags.value_or(AutoColor));
        return new __Owned { QPixmap::fromImage(Image::fromDeferred(image), qFlags) };
    }
}
