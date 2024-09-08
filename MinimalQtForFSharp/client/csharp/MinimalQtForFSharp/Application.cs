using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Application
    {
        private static ModuleHandle _module;

        // built-in array type: string[]
        internal static ModuleMethodHandle _setStyle;
        internal static ModuleMethodHandle _exec;
        internal static ModuleMethodHandle _quit;
        internal static ModuleMethodHandle _availableStyles;
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _executeOnMainThread;
        internal static ModuleMethodHandle _handle_setQuitOnLastWindowClosed;
        internal static ModuleMethodHandle _handle_dispose;

        public static void SetStyle(string name)
        {
            NativeImplClient.PushString(name);
            NativeImplClient.InvokeModuleMethod(_setStyle);
        }

        public static int Exec()
        {
            NativeImplClient.InvokeModuleMethod(_exec);
            return NativeImplClient.PopInt32();
        }

        public static void Quit()
        {
            NativeImplClient.InvokeModuleMethod(_quit);
        }

        public static string[] AvailableStyles()
        {
            NativeImplClient.InvokeModuleMethod(_availableStyles);
            return NativeImplClient.PopStringArray();
        }

        public static Handle Create(string[] args)
        {
            NativeImplClient.PushStringArray(args);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }

        public static void ExecuteOnMainThread(MainThreadFunc func)
        {
            MainThreadFunc__Push(func);
            NativeImplClient.InvokeModuleMethod(_executeOnMainThread);
        }
        public class Handle : IDisposable, IComparable
        {
            internal readonly IntPtr NativeHandle;
            protected bool _disposed;
            internal Handle(IntPtr nativeHandle)
            {
                NativeHandle = nativeHandle;
            }
            public int CompareTo(object obj)
            {
                if (obj is Handle other)
                {
                    return NativeHandle.CompareTo(other.NativeHandle);
                }
                throw new Exception("CompareTo: wrong type");
            }
            public virtual void Dispose()
            {
                if (!_disposed)
                {
                    Handle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_handle_dispose);
                    _disposed = true;
                }
            }
            public void SetQuitOnLastWindowClosed(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setQuitOnLastWindowClosed);
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
        public delegate void MainThreadFunc();

        internal static void MainThreadFunc__Push(MainThreadFunc callback)
        {
            void CallbackWrapper()
            {
                callback();
            }
            NativeImplClient.PushClientFuncVal(CallbackWrapper, Marshal.GetFunctionPointerForDelegate(callback));
        }

        internal static MainThreadFunc MainThreadFunc__Pop()
        {
            var id = NativeImplClient.PopServerFuncValId();
            var remoteFunc = new ServerFuncVal(id);
            void Wrapper()
            {
                remoteFunc.Exec();
            }
            return Wrapper;
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Application");
            // assign module handles
            _setStyle = NativeImplClient.GetModuleMethod(_module, "setStyle");
            _exec = NativeImplClient.GetModuleMethod(_module, "exec");
            _quit = NativeImplClient.GetModuleMethod(_module, "quit");
            _availableStyles = NativeImplClient.GetModuleMethod(_module, "availableStyles");
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _executeOnMainThread = NativeImplClient.GetModuleMethod(_module, "executeOnMainThread");
            _handle_setQuitOnLastWindowClosed = NativeImplClient.GetModuleMethod(_module, "Handle_setQuitOnLastWindowClosed");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
