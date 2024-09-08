using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Object;
using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Icon;
using static Org.Whatever.MinimalQtForFSharp.Dialog;
using static Org.Whatever.MinimalQtForFSharp.Widget;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class FileDialog
    {
        private static ModuleHandle _module;

        // built-in array type: string[]
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _handle_setAcceptMode;
        internal static ModuleMethodHandle _handle_setDefaultSuffix;
        internal static ModuleMethodHandle _handle_setFileMode;
        internal static ModuleMethodHandle _handle_setOptions;
        internal static ModuleMethodHandle _handle_setSupportedSchemes;
        internal static ModuleMethodHandle _handle_setViewMode;
        internal static ModuleMethodHandle _handle_setNameFilter;
        internal static ModuleMethodHandle _handle_setNameFilters;
        internal static ModuleMethodHandle _handle_setMimeTypeFilters;
        internal static ModuleMethodHandle _handle_setDirectory;
        internal static ModuleMethodHandle _handle_selectFile;
        internal static ModuleMethodHandle _handle_selectedFiles;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_customContextMenuRequested;
        internal static InterfaceMethodHandle _signalHandler_windowIconChanged;
        internal static InterfaceMethodHandle _signalHandler_windowTitleChanged;
        internal static InterfaceMethodHandle _signalHandler_accepted;
        internal static InterfaceMethodHandle _signalHandler_finished;
        internal static InterfaceMethodHandle _signalHandler_rejected;
        internal static InterfaceMethodHandle _signalHandler_currentChanged;
        internal static InterfaceMethodHandle _signalHandler_currentUrlChanged;
        internal static InterfaceMethodHandle _signalHandler_directoryEntered;
        internal static InterfaceMethodHandle _signalHandler_directoryUrlEntered;
        internal static InterfaceMethodHandle _signalHandler_fileSelected;
        internal static InterfaceMethodHandle _signalHandler_filesSelected;
        internal static InterfaceMethodHandle _signalHandler_filterSelected;
        internal static InterfaceMethodHandle _signalHandler_urlSelected;
        internal static InterfaceMethodHandle _signalHandler_urlsSelected;

        public static Handle Create(Widget.Handle parent, SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            Widget.Handle__Push(parent);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // Widget.SignalMask:
            CustomContextMenuRequested = 1 << 2,
            WindowIconChanged = 1 << 3,
            WindowTitleChanged = 1 << 4,
            // Dialog.SignalMask:
            Accepted = 1 << 5,
            Finished = 1 << 6,
            Rejected = 1 << 7,
            // SignalMask:
            CurrentChanged = 1 << 8,
            CurrentUrlChanged = 1 << 9,
            DirectoryEntered = 1 << 10,
            DirectoryUrlEntered = 1 << 11,
            FileSelected = 1 << 12,
            FilesSelected = 1 << 13,
            FilterSelected = 1 << 14,
            UrlSelected = 1 << 15,
            UrlsSelected = 1 << 16
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SignalMask__Push(SignalMask value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignalMask SignalMask__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SignalMask)ret;
        }

        public interface SignalHandler : IDisposable
        {
            void IDisposable.Dispose()
            {
                // nothing by default
            }
            void Destroyed(Object.Handle obj);
            void ObjectNameChanged(string objectName);
            void CustomContextMenuRequested(Point pos);
            void WindowIconChanged(Icon.Handle icon);
            void WindowTitleChanged(string title);
            void Accepted();
            void Finished(int result);
            void Rejected();
            void CurrentChanged(string path);
            void CurrentUrlChanged(string url);
            void DirectoryEntered(string dir);
            void DirectoryUrlEntered(string url);
            void FileSelected(string file);
            void FilesSelected(string[] selected);
            void FilterSelected(string filter);
            void UrlSelected(string url);
            void UrlsSelected(string[] urls);
        }

        private static Dictionary<SignalHandler, IPushable> __SignalHandlerToPushable = new();
        internal class __SignalHandlerWrapper : ClientInterfaceWrapper<SignalHandler>
        {
            public __SignalHandlerWrapper(SignalHandler rawInterface) : base(rawInterface)
            {
            }
            protected override void ReleaseExtra()
            {
                // remove the raw interface from the lookup table, no longer needed
                __SignalHandlerToPushable.Remove(RawInterface);
            }
        }

        internal static void SignalHandler__Push(SignalHandler thing, bool isReturn)
        {
            if (thing != null)
            {
                if (__SignalHandlerToPushable.TryGetValue(thing, out var pushable))
                {
                    // either an already-known client thing, or a server thing
                    pushable.Push(isReturn);
                }
                else
                {
                    // as-yet-unknown client thing - wrap and add to lookup table
                    pushable = new __SignalHandlerWrapper(thing);
                    __SignalHandlerToPushable.Add(thing, pushable);
                }
                pushable.Push(isReturn);
            }
            else
            {
                NativeImplClient.PushNull();
            }
        }

        internal static SignalHandler SignalHandler__Pop()
        {
            NativeImplClient.PopInstanceId(out var id, out var isClientId);
            if (id != 0)
            {
                if (isClientId)
                {
                    // we must have sent it over originally, so wrapper must exist
                    var wrapper = (__SignalHandlerWrapper)ClientObject.GetById(id);
                    return wrapper.RawInterface;
                }
                else // server ID
                {
                    var thing = new ServerSignalHandler(id);
                    // add to lookup table before returning
                    __SignalHandlerToPushable.Add(thing, thing);
                    return thing;
                }
            }
            else
            {
                return null;
            }
        }

        private class ServerSignalHandler : ServerObject, SignalHandler
        {
            public ServerSignalHandler(int id) : base(id)
            {
            }

            public void Destroyed(Object.Handle obj)
            {
                Object.Handle__Push(obj);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_destroyed, Id);
            }

            public void ObjectNameChanged(string objectName)
            {
                NativeImplClient.PushString(objectName);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_objectNameChanged, Id);
            }

            public void CustomContextMenuRequested(Point pos)
            {
                Point__Push(pos, false);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_customContextMenuRequested, Id);
            }

            public void WindowIconChanged(Icon.Handle icon)
            {
                Icon.Handle__Push(icon);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_windowIconChanged, Id);
            }

            public void WindowTitleChanged(string title)
            {
                NativeImplClient.PushString(title);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_windowTitleChanged, Id);
            }

            public void Accepted()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_accepted, Id);
            }

            public void Finished(int result)
            {
                NativeImplClient.PushInt32(result);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_finished, Id);
            }

            public void Rejected()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rejected, Id);
            }

            public void CurrentChanged(string path)
            {
                NativeImplClient.PushString(path);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_currentChanged, Id);
            }

            public void CurrentUrlChanged(string url)
            {
                NativeImplClient.PushString(url);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_currentUrlChanged, Id);
            }

            public void DirectoryEntered(string dir)
            {
                NativeImplClient.PushString(dir);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_directoryEntered, Id);
            }

            public void DirectoryUrlEntered(string url)
            {
                NativeImplClient.PushString(url);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_directoryUrlEntered, Id);
            }

            public void FileSelected(string file)
            {
                NativeImplClient.PushString(file);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_fileSelected, Id);
            }

            public void FilesSelected(string[] selected)
            {
                NativeImplClient.PushStringArray(selected);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_filesSelected, Id);
            }

            public void FilterSelected(string filter)
            {
                NativeImplClient.PushString(filter);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_filterSelected, Id);
            }

            public void UrlSelected(string url)
            {
                NativeImplClient.PushString(url);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_urlSelected, Id);
            }

            public void UrlsSelected(string[] urls)
            {
                NativeImplClient.PushStringArray(urls);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_urlsSelected, Id);
            }

            protected override void ReleaseExtra()
            {
                // remove from lookup table
                __SignalHandlerToPushable.Remove(this);
            }

            public void Dispose()
            {
                // will invoke ReleaseExtra() for us
                ServerDispose();
            }
        }
        public enum FileMode
        {
            AnyFile,
            ExistingFile,
            Directory,
            ExistingFiles
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FileMode__Push(FileMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static FileMode FileMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (FileMode)ret;
        }
        public enum ViewMode
        {
            Detail,
            List
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ViewMode__Push(ViewMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ViewMode ViewMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (ViewMode)ret;
        }
        public enum AcceptMode
        {
            Open,
            Save
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AcceptMode__Push(AcceptMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static AcceptMode AcceptMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (AcceptMode)ret;
        }
        [Flags]
        public enum Options
        {
            ShowDirsOnly = 0x00000001,
            DontResolveSymlinks = 0x00000002,
            DontConfirmOverwrite = 0x00000004,
            DontUseNativeDialog = 0x00000008,
            ReadOnly = 0x00000010,
            HideNameFilterDetails = 0x00000020,
            DontUseCustomDirectoryIcons = 0x00000040
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Options__Push(Options value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Options Options__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Options)ret;
        }
        public class Handle : Dialog.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public override void Dispose()
            {
                if (!_disposed)
                {
                    Handle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_handle_dispose);
                    _disposed = true;
                }
            }
            public void SetAcceptMode(AcceptMode mode)
            {
                AcceptMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAcceptMode);
            }
            public void SetDefaultSuffix(string suffix)
            {
                NativeImplClient.PushString(suffix);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDefaultSuffix);
            }
            public void SetFileMode(FileMode mode)
            {
                FileMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFileMode);
            }
            public void SetOptions(Options opts)
            {
                Options__Push(opts);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setOptions);
            }
            public void SetSupportedSchemes(string[] schemes)
            {
                NativeImplClient.PushStringArray(schemes);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSupportedSchemes);
            }
            public void SetViewMode(ViewMode mode)
            {
                ViewMode__Push(mode);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setViewMode);
            }
            public void SetNameFilter(string filter)
            {
                NativeImplClient.PushString(filter);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setNameFilter);
            }
            public void SetNameFilters(string[] filters)
            {
                NativeImplClient.PushStringArray(filters);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setNameFilters);
            }
            public void SetMimeTypeFilters(string[] filters)
            {
                NativeImplClient.PushStringArray(filters);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setMimeTypeFilters);
            }
            public void SetDirectory(string dir)
            {
                NativeImplClient.PushString(dir);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDirectory);
            }
            public void SelectFile(string file)
            {
                NativeImplClient.PushString(file);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_selectFile);
            }
            public string[] SelectedFiles()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_selectedFiles);
                return NativeImplClient.PopStringArray();
            }
            public void SetSignalMask(SignalMask mask)
            {
                SignalMask__Push(mask);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSignalMask);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Handle__Push(Handle thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Handle Handle__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Handle(ptr) : null;
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("FileDialog");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _handle_setAcceptMode = NativeImplClient.GetModuleMethod(_module, "Handle_setAcceptMode");
            _handle_setDefaultSuffix = NativeImplClient.GetModuleMethod(_module, "Handle_setDefaultSuffix");
            _handle_setFileMode = NativeImplClient.GetModuleMethod(_module, "Handle_setFileMode");
            _handle_setOptions = NativeImplClient.GetModuleMethod(_module, "Handle_setOptions");
            _handle_setSupportedSchemes = NativeImplClient.GetModuleMethod(_module, "Handle_setSupportedSchemes");
            _handle_setViewMode = NativeImplClient.GetModuleMethod(_module, "Handle_setViewMode");
            _handle_setNameFilter = NativeImplClient.GetModuleMethod(_module, "Handle_setNameFilter");
            _handle_setNameFilters = NativeImplClient.GetModuleMethod(_module, "Handle_setNameFilters");
            _handle_setMimeTypeFilters = NativeImplClient.GetModuleMethod(_module, "Handle_setMimeTypeFilters");
            _handle_setDirectory = NativeImplClient.GetModuleMethod(_module, "Handle_setDirectory");
            _handle_selectFile = NativeImplClient.GetModuleMethod(_module, "Handle_selectFile");
            _handle_selectedFiles = NativeImplClient.GetModuleMethod(_module, "Handle_selectedFiles");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_customContextMenuRequested = NativeImplClient.GetInterfaceMethod(_signalHandler, "customContextMenuRequested");
            _signalHandler_windowIconChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowIconChanged");
            _signalHandler_windowTitleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "windowTitleChanged");
            _signalHandler_accepted = NativeImplClient.GetInterfaceMethod(_signalHandler, "accepted");
            _signalHandler_finished = NativeImplClient.GetInterfaceMethod(_signalHandler, "finished");
            _signalHandler_rejected = NativeImplClient.GetInterfaceMethod(_signalHandler, "rejected");
            _signalHandler_currentChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "currentChanged");
            _signalHandler_currentUrlChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "currentUrlChanged");
            _signalHandler_directoryEntered = NativeImplClient.GetInterfaceMethod(_signalHandler, "directoryEntered");
            _signalHandler_directoryUrlEntered = NativeImplClient.GetInterfaceMethod(_signalHandler, "directoryUrlEntered");
            _signalHandler_fileSelected = NativeImplClient.GetInterfaceMethod(_signalHandler, "fileSelected");
            _signalHandler_filesSelected = NativeImplClient.GetInterfaceMethod(_signalHandler, "filesSelected");
            _signalHandler_filterSelected = NativeImplClient.GetInterfaceMethod(_signalHandler, "filterSelected");
            _signalHandler_urlSelected = NativeImplClient.GetInterfaceMethod(_signalHandler, "urlSelected");
            _signalHandler_urlsSelected = NativeImplClient.GetInterfaceMethod(_signalHandler, "urlsSelected");
            NativeImplClient.SetClientMethodWrapper(_signalHandler_destroyed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var obj = Object.Handle__Pop();
                inst.Destroyed(obj);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_objectNameChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var objectName = NativeImplClient.PopString();
                inst.ObjectNameChanged(objectName);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_customContextMenuRequested, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var pos = Point__Pop();
                inst.CustomContextMenuRequested(pos);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_windowIconChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var icon = Icon.Handle__Pop();
                inst.WindowIconChanged(icon);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_windowTitleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var title = NativeImplClient.PopString();
                inst.WindowTitleChanged(title);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_accepted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Accepted();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_finished, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var result = NativeImplClient.PopInt32();
                inst.Finished(result);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rejected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.Rejected();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_currentChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var path = NativeImplClient.PopString();
                inst.CurrentChanged(path);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_currentUrlChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var url = NativeImplClient.PopString();
                inst.CurrentUrlChanged(url);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_directoryEntered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var dir = NativeImplClient.PopString();
                inst.DirectoryEntered(dir);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_directoryUrlEntered, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var url = NativeImplClient.PopString();
                inst.DirectoryUrlEntered(url);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_fileSelected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var file = NativeImplClient.PopString();
                inst.FileSelected(file);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_filesSelected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var selected = NativeImplClient.PopStringArray();
                inst.FilesSelected(selected);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_filterSelected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var filter = NativeImplClient.PopString();
                inst.FilterSelected(filter);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_urlSelected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var url = NativeImplClient.PopString();
                inst.UrlSelected(url);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_urlsSelected, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var urls = NativeImplClient.PopStringArray();
                inst.UrlsSelected(urls);
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
