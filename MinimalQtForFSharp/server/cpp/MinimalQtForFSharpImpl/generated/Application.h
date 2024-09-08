#pragma once

#include "../support/NativeImplServer.h"
#include <functional>
#include <memory>
#include <string>
#include <vector>
#include <map>
#include <tuple>
#include <set>
#include <optional>
#include "../support/result.h"

namespace Application
{

    struct __Handle; typedef struct __Handle* HandleRef;

    void Handle_setQuitOnLastWindowClosed(HandleRef _this, bool state);
    void Handle_dispose(HandleRef _this);
    void setStyle(std::string name);
    int32_t exec();
    void quit();
    std::vector<std::string> availableStyles();
    HandleRef create(std::vector<std::string> args);

    typedef void MainThreadFunc();
    void executeOnMainThread(std::function<MainThreadFunc> func);
}
