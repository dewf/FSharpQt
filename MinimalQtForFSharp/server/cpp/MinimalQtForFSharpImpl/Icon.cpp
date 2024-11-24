#include "generated/Icon.h"

#include <QIcon>
#include "IconInternal.h"
#include "PixmapInternal.h"

#define THIS ((QIcon*)_this)

namespace Icon
{
    void Owned_dispose(OwnedRef _this) {
        delete THIS;
    }

    OwnedRef create() {
        return reinterpret_cast<OwnedRef>(new QIcon());
    }

    OwnedRef create(ThemeIcon themeIcon) {
        auto qThemeIcon = static_cast<QIcon::ThemeIcon>(themeIcon);
        return ICON_HEAP_COPY(QIcon::fromTheme(qThemeIcon));
    }

    OwnedRef create(std::string filename) {
        return ICON_HEAP_COPY(QString::fromStdString(filename));
    }

    OwnedRef create(Pixmap::HandleRef pixmap) {
        return ICON_HEAP_COPY(pixmap->qPixmap);
    }
}
