using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.PaintDevice;
using static Org.Whatever.MinimalQtForFSharp.Common;
using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Image
    {
        private static ModuleHandle _module;

        private static void __SizeT_Option__Push(Maybe<IntPtr> maybeValue, bool isReturn)
        {
            if (maybeValue.TryGetValue(out var value))
            {
                NativeImplClient.PushSizeT(value);
                NativeImplClient.PushBool(true);
            }
            else
            {
                NativeImplClient.PushBool(false);
            }
        }

        private static Maybe<IntPtr> __SizeT_Option__Pop()
        {
            var hasValue = NativeImplClient.PopBool();
            if (hasValue)
            {
                return NativeImplClient.PopSizeT();
            }
            return Maybe<IntPtr>.None;
        }

        internal static void __Native_Byte_Buffer__Push(INativeBuffer<byte> buf, bool isReturn)
        {
            if (buf != null)
            {
                ((IPushable)buf).Push(isReturn);
            }
            else
            {
                NativeImplClient.PushNull();
            }
        }

        internal static INativeBuffer<byte> __Native_Byte_Buffer__Pop()
        {
            NativeMethods.popBuffer(out var id, out var isClientId, out var bufferDescriptor);
            if (id != 0)
            {
                if (isClientId)
                {
                    return ClientBuffer<byte>.GetById(id);
                }
                else
                {
                    return new ServerBuffer<byte>(id, bufferDescriptor);
                }
            }
            else
            {
                return null;
            }
        }

        private static void __String_Option__Push(Maybe<string> maybeValue, bool isReturn)
        {
            if (maybeValue.TryGetValue(out var value))
            {
                NativeImplClient.PushString(value);
                NativeImplClient.PushBool(true);
            }
            else
            {
                NativeImplClient.PushBool(false);
            }
        }

        private static Maybe<string> __String_Option__Pop()
        {
            var hasValue = NativeImplClient.PopBool();
            if (hasValue)
            {
                return NativeImplClient.PopString();
            }
            return Maybe<string>.None;
        }
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _create_overload1;
        internal static ModuleMethodHandle _create_overload2;
        internal static ModuleMethodHandle _handle_scaled;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Create(int width, int height, Format format)
        {
            Format__Push(format);
            NativeImplClient.PushInt32(height);
            NativeImplClient.PushInt32(width);
            NativeImplClient.InvokeModuleMethod(_create);
            return Owned__Pop();
        }

        public static Owned Create(string filename, Maybe<string> format)
        {
            __String_Option__Push(format, false);
            NativeImplClient.PushString(filename);
            NativeImplClient.InvokeModuleMethod(_create_overload1);
            return Owned__Pop();
        }

        public static Owned Create(INativeBuffer<byte> data, int width, int height, Format format, Maybe<IntPtr> bytesPerLine)
        {
            __SizeT_Option__Push(bytesPerLine, false);
            Format__Push(format);
            NativeImplClient.PushInt32(height);
            NativeImplClient.PushInt32(width);
            __Native_Byte_Buffer__Push(data, false);
            NativeImplClient.InvokeModuleMethod(_create_overload2);
            return Owned__Pop();
        }
        public enum Format
        {
            Invalid,
            Mono,
            MonoLSB,
            Indexed8,
            RGB32,
            ARGB32,
            ARGB32_Premultiplied,
            RGB16,
            ARGB8565_Premultiplied,
            RGB666,
            ARGB6666_Premultiplied,
            RGB555,
            ARGB8555_Premultiplied,
            RGB888,
            RGB444,
            ARGB4444_Premultiplied,
            RGBX8888,
            RGBA8888,
            RGBA8888_Premultiplied,
            BGR30,
            A2BGR30_Premultiplied,
            RGB30,
            A2RGB30_Premultiplied,
            Alpha8,
            Grayscale8,
            RGBX64,
            RGBA64,
            RGBA64_Premultiplied,
            Grayscale16,
            BGR888,
            RGBX16FPx4,
            RGBA16FPx4,
            RGBA16FPx4_Premultiplied,
            RGBX32FPx4,
            RGBA32FPx4,
            RGBA32FPx4_Premultiplied,
            CMYK8888,
            NImageFormats
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Format__Push(Format value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Format Format__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (Format)ret;
        }
        public struct ScaledOptions
        {
            [Flags]
            internal enum Fields
            {
                AspectMode = 1,
                TransformMode = 2
            }
            internal Fields UsedFields;

            private AspectRatioMode _aspectMode;
            public AspectRatioMode AspectMode
            {
                set
                {
                    _aspectMode = value;
                    UsedFields |= Fields.AspectMode;
                }
            }
            public readonly bool HasAspectMode(out AspectRatioMode value)
            {
                if (UsedFields.HasFlag(Fields.AspectMode))
                {
                    value = _aspectMode;
                    return true;
                }
                value = default;
                return false;
            }
            private TransformationMode _transformMode;
            public TransformationMode TransformMode
            {
                set
                {
                    _transformMode = value;
                    UsedFields |= Fields.TransformMode;
                }
            }
            public readonly bool HasTransformMode(out TransformationMode value)
            {
                if (UsedFields.HasFlag(Fields.TransformMode))
                {
                    value = _transformMode;
                    return true;
                }
                value = default;
                return false;
            }
        }
        internal static void ScaledOptions__Push(ScaledOptions value, bool isReturn)
        {
            if (value.HasTransformMode(out var transformMode))
            {
                TransformationMode__Push(transformMode);
            }
            if (value.HasAspectMode(out var aspectMode))
            {
                AspectRatioMode__Push(aspectMode);
            }
            NativeImplClient.PushInt32((int)value.UsedFields);
        }
        internal static ScaledOptions ScaledOptions__Pop()
        {
            var opts = new ScaledOptions
            {
                UsedFields = (ScaledOptions.Fields)NativeImplClient.PopInt32()
            };
            if (opts.UsedFields.HasFlag(ScaledOptions.Fields.AspectMode))
            {
                opts.AspectMode = AspectRatioMode__Pop();
            }
            if (opts.UsedFields.HasFlag(ScaledOptions.Fields.TransformMode))
            {
                opts.TransformMode = TransformationMode__Pop();
            }
            return opts;
        }
        public class Handle : PaintDevice.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public Owned Scaled(int width, int height, ScaledOptions opts)
            {
                ScaledOptions__Push(opts, false);
                NativeImplClient.PushInt32(height);
                NativeImplClient.PushInt32(width);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_scaled);
                return Owned__Pop();
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
        public class Owned : Handle, IDisposable
        {
            protected bool _disposed;
            internal Owned(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public virtual void Dispose()
            {
                if (!_disposed)
                {
                    Owned__Push(this);
                    NativeImplClient.InvokeModuleMethod(_owned_dispose);
                    _disposed = true;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Owned__Push(Owned thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Owned Owned__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Owned(ptr) : null;
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Image");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _create_overload1 = NativeImplClient.GetModuleMethod(_module, "create_overload1");
            _create_overload2 = NativeImplClient.GetModuleMethod(_module, "create_overload2");
            _handle_scaled = NativeImplClient.GetModuleMethod(_module, "Handle_scaled");
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
