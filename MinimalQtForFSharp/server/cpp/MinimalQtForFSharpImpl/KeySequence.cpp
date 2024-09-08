#include "generated/KeySequence.h"

#include "KeySequenceInternal.h"

namespace KeySequence
{
    class FromDeferred : public KeySequence::Deferred::Visitor {
    private:
        QKeySequence &seq;
    public:
        explicit FromDeferred(QKeySequence &seq) : seq(seq) {}

        void onFromString(const KeySequence::Deferred::FromString *fromString) override {
            seq = { QString::fromStdString(fromString->s) };
        }

        void onFromStandard(const KeySequence::Deferred::FromStandard *fromStandard) override {
            seq = { (QKeySequence::StandardKey)fromStandard->key };
        }

        void onFromKey(const KeySequence::Deferred::FromKey *fromKey) override {
            // could have also used a QKeyCombination instead of 'key | mods'
            auto key = (Qt::Key)fromKey->key;
            auto mods = (Qt::Modifiers)fromKey->modifiers;
            seq = { key | mods };
        }
    };

    QKeySequence fromDeferred(const std::shared_ptr<Deferred::Base>& deferred) {
        QKeySequence seq;
        FromDeferred visitor(seq);
        deferred->accept(&visitor);
        return seq;
    }
}
