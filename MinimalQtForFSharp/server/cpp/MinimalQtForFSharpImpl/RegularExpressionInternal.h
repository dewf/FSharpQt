#pragma once

#include <QRegularExpression>
#include "generated/RegularExpression.h"

namespace RegularExpression {
    class FromDeferred : public RegularExpression::Deferred::Visitor {
    private:
        QRegularExpression &qRegex;
    public:
        explicit FromDeferred(QRegularExpression &qRegex) : qRegex(qRegex) {}

        void onEmpty(const Deferred::Empty *empty) override {
            qRegex = QRegularExpression("");
        }

        void onRegex(const Deferred::Regex *regex) override {
            auto opts = (int)regex->opts;
            qRegex = QRegularExpression(QString::fromStdString(regex->pattern), (QRegularExpression::PatternOptions)opts);
        }
    };

    QRegularExpression fromDeferred(const std::shared_ptr<Deferred::Base>& deferred);
}

