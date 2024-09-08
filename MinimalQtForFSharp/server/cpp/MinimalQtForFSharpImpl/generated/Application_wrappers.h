#pragma once
#include "Application.h"

namespace Application
{

    void Handle__push(HandleRef value);
    HandleRef Handle__pop();

    void Handle_setQuitOnLastWindowClosed__wrapper();

    void Handle_dispose__wrapper();

    void setStyle__wrapper();

    void exec__wrapper();

    void quit__wrapper();

    void availableStyles__wrapper();

    void create__wrapper();
    void MainThreadFunc__push(std::function<MainThreadFunc> f);
    std::function<MainThreadFunc> MainThreadFunc__pop();

    void executeOnMainThread__wrapper();

    int __register();
}
