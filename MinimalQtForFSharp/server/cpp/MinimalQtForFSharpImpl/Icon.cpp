#include "generated/Icon.h"

#include <QIcon>
#include "IconInternal.h"

#define THIS ((QIcon*)_this)

namespace Icon
{
    // no opaque methods yet, if ever
    // currently we only need this for a widget signal (WindowIconChanged), might not for anything else

    // F# API will be designed around Deferred version

    class FromDeferred : public Deferred::Visitor {
    private:
        QIcon &icon;
    public:
        explicit FromDeferred(QIcon &icon) : icon(icon) {}

        void onEmpty(const Deferred::Empty *empty) override {
            icon = QIcon();
        }

        void onFromThemeIcon(const Deferred::FromThemeIcon *fromThemeIcon) override {
            icon = QIcon::fromTheme((QIcon::ThemeIcon)fromThemeIcon->themeIcon);
        }

        void onFromFilename(const Deferred::FromFilename *fromFilename) override {
            icon = QIcon(QString::fromStdString(fromFilename->filename));
        }
    };

    QIcon fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QIcon icon;
        FromDeferred visitor(icon);
        deferred->accept(&visitor);
        return icon;
    }
}
