#pragma once

namespace Common {

    // things that can go on the paint stack inherit this
    // but we didn't want to create a circular dependency situation, for example between PaintResources and Color
    // Pixmap etc will also inherit from this

    struct PaintStackItem {
    };
}

