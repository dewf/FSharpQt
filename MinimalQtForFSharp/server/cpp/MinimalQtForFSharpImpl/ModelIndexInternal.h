#pragma once

namespace ModelIndex {
    QModelIndex fromDeferred(const std::shared_ptr<ModelIndex::Deferred::Base>& deferred);
}
