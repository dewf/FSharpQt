namespace Org.Whatever.MinimalQtForFSharp.Support
{
    internal class NativeHandle
    {
        internal IntPtr Handle;
        protected NativeHandle(IntPtr handle)
        {
            Handle = handle;
        }
    }

    internal class ModuleHandle : NativeHandle
    {
        internal ModuleHandle(IntPtr handle) : base(handle)
        {
        }
    }

    internal class ModuleMethodHandle : NativeHandle
    {
        internal ModuleMethodHandle(IntPtr handle) : base(handle)
        {
        }
    }

    internal class InterfaceHandle : NativeHandle
    {
        internal InterfaceHandle(IntPtr handle) : base(handle)
        {
        }
    }

    internal class InterfaceMethodHandle : NativeHandle
    {
        internal InterfaceMethodHandle(IntPtr handle) : base(handle)
        {
        }
    }

    internal class ExceptionHandle : NativeHandle
    {
        internal ExceptionHandle(IntPtr handle) : base(handle)
        {
        }
    }
}