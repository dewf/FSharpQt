#include "generated/Pixmap.h"

#include <QPixmap>

#include "ImageInternal.h"
#include "PixmapInternal.h"

namespace Pixmap
{
    int32_t Handle_width(HandleRef _this) {
        return _this->qPixmap.width();
    }

    int32_t Handle_height(HandleRef _this) {
        return _this->qPixmap.height();
    }

    void Owned_dispose(OwnedRef _this) {
        delete _this;
    }

    OwnedRef create(int32_t width, int32_t height) {
        return new __Owned { QPixmap(width, height) };
    }

    OwnedRef create(std::string filename, FilenameOptions opts) {
        const char *qFormat = nullptr;
        std::string format;
        if (opts.hasFormat(&format)) {
            qFormat = format.c_str();
        }
        const auto qImageConversionFlags = static_cast<Qt::ImageConversionFlags>(opts.getOrDefaultImageConversionFlags(AutoColor));
        return new __Owned { QPixmap(filename.c_str(), qFormat, qImageConversionFlags) };
    }

    OwnedRef fromImage(Image::HandleRef image, std::optional<ImageConversionFlags> imageConversionFlags) {
        const auto qFlags = static_cast<Qt::ImageConversionFlags>(imageConversionFlags.value_or(AutoColor));
        return new __Owned { QPixmap::fromImage(image->qImage, qFlags) };
    }
}
