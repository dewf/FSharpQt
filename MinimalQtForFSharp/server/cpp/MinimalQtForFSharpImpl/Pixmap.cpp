#include "generated/Pixmap.h"

#include <QPixmap>
#include "PixmapInternal.h"

namespace Pixmap
{
    class FromDeferred : public Pixmap::Deferred::Visitor {
    private:
        QPixmap& pixmap;
    public:
        explicit FromDeferred(QPixmap &pixmap) : pixmap(pixmap) {}
        void onEmpty(const Deferred::Empty *empty) override {
            pixmap = QPixmap();
        }
    };

    QPixmap fromDeferred(const std::shared_ptr<Pixmap::Deferred::Base>& deferred) {
        QPixmap ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }
}
