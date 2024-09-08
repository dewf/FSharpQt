#include "generated/Application.h"

#include <QApplication>
#include <QStyleFactory>

#define THIS ((QApplication*)_this)

namespace Application
{
    void Handle_setQuitOnLastWindowClosed(HandleRef _this, bool state) {
        THIS->setQuitOnLastWindowClosed(state);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    void setStyle(std::string name) {
        QApplication::setStyle(QString::fromStdString(name));
    }

    int32_t exec() {
        return QApplication::exec();
    }

    void quit() {
        QApplication::quit();
    }

    std::vector<std::string> availableStyles() {
        std::vector<std::string> result;
        for (auto & key : QStyleFactory::keys()) {
            result.push_back(key.toStdString());
        }
        return result;
    }

    HandleRef create(std::vector<std::string> args) {
        auto argc = (int)args.size();
        auto argv = new char*[argc];
        for (int i = 0; i< argc; i++) {
            argv[i] = const_cast<char*>(args[i].c_str());
        }
        auto ret = new QApplication(argc, argv);
        delete[] argv;
        return (HandleRef)ret;
    }

    // "execute on main thread" stuff ======================================

    static QEvent::Type mainThreadRunEventType = (QEvent::Type)QEvent::registerEventType();

    class MainThreadRunEvent : public QEvent {
    private:
        std::function<MainThreadFunc> func;
    public:
        explicit MainThreadRunEvent(const std::function<MainThreadFunc> &func) : QEvent(mainThreadRunEventType), func(func) {}
        void execute() {
            func();
        }
    };

    class MainThreadRunner : public QObject {
    public:
        bool event(QEvent *event) override {
            if (event->type() == mainThreadRunEventType) {
                ((MainThreadRunEvent*)event)->execute();
                return true;
            } else {
                return QObject::event(event);
            }
        }
    };

    MainThreadRunner* runnerInstance = new MainThreadRunner(); // just being superstitious about heap-allocating this, would probably be fine anyway

    void executeOnMainThread(std::function<MainThreadFunc> func) {
        // must be heap-allocated, owned by Qt after posting
        auto event = new MainThreadRunEvent(func);
        QCoreApplication::postEvent(runnerInstance, event);
    }
}
