#include "generated/RegularExpression.h"

#include "RegularExpressionInternal.h"

namespace RegularExpression
{
    QRegularExpression fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QRegularExpression ret;
        FromDeferred visitor(ret);
        deferred->accept(&visitor);
        return ret;
    }
}
