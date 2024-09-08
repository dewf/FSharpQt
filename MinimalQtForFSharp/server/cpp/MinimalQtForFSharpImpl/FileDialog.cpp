#include "generated/FileDialog.h"

#include <QFileDialog>

#include "util/SignalStuff.h"
#include "util/convert.h"

#define THIS ((FileDialogWithHandler*)_this)

namespace FileDialog
{
    class FileDialogWithHandler : public QFileDialog {
        Q_OBJECT
    private:
        std::shared_ptr<SignalHandler> handler;
        SignalMask lastMask = 0;
        std::vector<SignalMapItem<SignalMaskFlags>> signalMap = {
            // Object:
            { SignalMaskFlags::Destroyed, SIGNAL(destroyed(QObject)), SLOT(onDestroyed(QObject)) },
            { SignalMaskFlags::ObjectNameChanged, SIGNAL(objectNameChanged(QString)), SLOT(onObjectNameChanged(QString)) },
            // Widget:
            { SignalMaskFlags::CustomContextMenuRequested, SIGNAL(customContextMenuRequested(QPoint)), SLOT(onCustomContextMenuRequested(QPoint)) },
            { SignalMaskFlags::WindowIconChanged, SIGNAL(windowIconChanged(QIcon)), SLOT(onWindowIconChanged(QIcon)) },
            { SignalMaskFlags::WindowTitleChanged, SIGNAL(windowTitleChanged(QString)), SLOT(onWindowTitleChanged(QString)) },
            // Dialog:
            { SignalMaskFlags::Accepted, SIGNAL(accepted()), SLOT(onAccepted()) },
            { SignalMaskFlags::Finished, SIGNAL(finished(int)), SLOT(onFinished(int)) },
            { SignalMaskFlags::Rejected, SIGNAL(rejected()), SLOT(onRejected()) },
            // FileDialog:
            { SignalMaskFlags::CurrentChanged, SIGNAL(currentChanged(QString)), SLOT(onCurrentChanged(QString)) },
            { SignalMaskFlags::CurrentUrlChanged, SIGNAL(currentUrlChanged(QUrl)), SLOT(onCurrentUrlChanged(QUrl)) },
            { SignalMaskFlags::DirectoryEntered, SIGNAL(directoryEntered(QString)), SLOT(onDirectoryEntered(QString)) },
            { SignalMaskFlags::DirectoryUrlEntered, SIGNAL(directoryUrlEntered(QUrl)), SLOT(onDirectoryUrlEntered(QUrl)) },
            { SignalMaskFlags::FileSelected, SIGNAL(fileSelected(QString)), SLOT(onFileSelected(QString)) },
            { SignalMaskFlags::FilesSelected, SIGNAL(filesSelected(QStringList)), SLOT(onFilesSelected(QStringList)) },
            { SignalMaskFlags::FilterSelected, SIGNAL(filterSelected(QString)), SLOT(onFilterSelected(QString)) },
            { SignalMaskFlags::UrlSelected, SIGNAL(urlSelected(QUrl)), SLOT(onUrlSelected(QUrl)) },
            { SignalMaskFlags::UrlsSelected, SIGNAL(urlsSelected(QList<QUrl>)), SLOT(onUrlsSelected(QList<QUrl>)) },
        };
    public:
        explicit FileDialogWithHandler(QWidget *parent, const std::shared_ptr<SignalHandler> &handler) : handler(handler), QFileDialog(parent) {}
        void setSignalMask(SignalMask newMask) {
            if (newMask != lastMask) {
                processChanges(lastMask, newMask, signalMap, this);
                lastMask = newMask;
            }
        }
    public slots:
        // Object =================
        void onDestroyed(QObject *obj) {
            handler->destroyed((Object::HandleRef)obj);
        }
        void onObjectNameChanged(const QString& name) {
            handler->objectNameChanged(name.toStdString());
        }
        // Widget =================
        void onCustomContextMenuRequested(const QPoint& pos) {
            handler->customContextMenuRequested(toPoint(pos));
        }
        void onWindowIconChanged(const QIcon& icon) {
            handler->windowIconChanged((Icon::HandleRef)&icon);
        }
        void onWindowTitleChanged(const QString& title) {
            handler->windowTitleChanged(title.toStdString());
        }
        // Dialog =================
        void onAccepted() {
            handler->accepted();
        }
        void onFinished(int result) {
            handler->finished(result);
        }
        void onRejected() {
            handler->rejected();
        }
        // FileDialog:
        void onCurrentChanged(const QString &path) {
            handler->currentChanged(path.toStdString());
        }
        void onCurrentUrlChanged(const QUrl &url) {
            handler->currentUrlChanged(url.toString().toStdString());
        }
        void onDirectoryEntered(const QString &directory) {
            handler->directoryEntered(directory.toStdString());
        }
        void onDirectoryUrlEntered(const QUrl &directory) {
            handler->directoryUrlEntered(directory.toString().toStdString());
        }
        void onFileSelected(const QString &file) {
            handler->fileSelected(file.toStdString());
        }
        void onFilesSelected(const QStringList &selected) {
            std::vector<std::string> selected2;
            for (auto &str : selected) {
                selected2.push_back(str.toStdString());
            }
            handler->filesSelected(selected2);
        }
        void onFilterSelected(const QString &filter) {
            handler->filterSelected(filter.toStdString());
        }
        void onUrlSelected(const QUrl &url) {
            handler->urlSelected(url.toString().toStdString());
        }
        void onUrlsSelected(const QList<QUrl> &urls) {
            std::vector<std::string> urls2;
            for (auto &url: urls) {
                urls2.push_back(url.toString().toStdString());
            }
            handler->urlsSelected(urls2);
        }
    };

    void Handle_setAcceptMode(HandleRef _this, AcceptMode mode) {
        THIS->setAcceptMode((QFileDialog::AcceptMode)mode);
    }

    void Handle_setDefaultSuffix(HandleRef _this, std::string suffix) {
        THIS->setDefaultSuffix(QString::fromStdString(suffix));
    }

    void Handle_setFileMode(HandleRef _this, FileMode mode) {
        THIS->setFileMode((QFileDialog::FileMode)mode);
    }

    void Handle_setOptions(HandleRef _this, Options opts) {
        THIS->setOptions((QFileDialog::Options)opts);
    }

    void Handle_setSupportedSchemes(HandleRef _this, std::vector<std::string> schemes) {
        THIS->setSupportedSchemes(toQStringList(schemes));
    }

    void Handle_setViewMode(HandleRef _this, ViewMode mode) {
        THIS->setViewMode((QFileDialog::ViewMode)mode);
    }

    void Handle_setNameFilter(HandleRef _this, std::string filter) {
        THIS->setNameFilter(QString::fromStdString(filter));
    }

    void Handle_setNameFilters(HandleRef _this, std::vector<std::string> filters) {
        QStringList qFilters;
        for (auto &filter: filters) {
            qFilters.append(QString::fromStdString(filter));
        }
        THIS->setNameFilters(qFilters);
    }

    void Handle_setMimeTypeFilters(HandleRef _this, std::vector<std::string> filters) {
        QStringList qFilters;
        for (auto &filter: filters) {
            qFilters.append(QString::fromStdString(filter));
        }
        THIS->setMimeTypeFilters(qFilters);
    }

    void Handle_setDirectory(HandleRef _this, std::string dir) {
        THIS->setDirectory(QString::fromStdString(dir));
    }

    void Handle_selectFile(HandleRef _this, std::string file) {
        THIS->selectFile(QString::fromStdString(file));
    }

    std::vector<std::string> Handle_selectedFiles(HandleRef _this) {
        std::vector<std::string> ret;
        for (auto &file : THIS->selectedFiles()) {
            ret.push_back(file.toStdString());
        }
        return ret;
    }

    void Handle_setSignalMask(HandleRef _this, SignalMask mask) {
        THIS->setSignalMask(mask);
    }

    void Handle_dispose(HandleRef _this) {
        delete THIS;
    }

    HandleRef create(Widget::HandleRef parent, std::shared_ptr<SignalHandler> handler) {
        return (HandleRef) new FileDialogWithHandler((QWidget*)parent, handler);
    }
}

#include "FileDialog.moc"
