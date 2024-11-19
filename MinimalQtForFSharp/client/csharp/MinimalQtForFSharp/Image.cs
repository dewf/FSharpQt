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
        internal static ModuleMethodHandle _realize;
        internal static ModuleMethodHandle _handle_scaled;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Realize(Deferred deferred)
        {
            Deferred__Push(deferred, false);
            NativeImplClient.InvokeModuleMethod(_realize);
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
        public abstract record Deferred
        {
            internal abstract void Push(bool isReturn);
            internal static Deferred Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => FromHandle.PopDerived(),
                    1 => FromWidthHeight.PopDerived(),
                    2 => FromFilename.PopDerived(),
                    3 => FromData.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record FromHandle(Handle Handle) : Deferred
            {
                public Handle Handle { get; } = Handle;
                internal override void Push(bool isReturn)
                {
                    Handle__Push(Handle);
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static FromHandle PopDerived()
                {
                    var handle = Handle__Pop();
                    return new FromHandle(handle);
                }
            }
            public sealed record FromWidthHeight(int Width, int Height, Format Format) : Deferred
            {
                public int Width { get; } = Width;
                public int Height { get; } = Height;
                public Format Format { get; } = Format;
                internal override void Push(bool isReturn)
                {
                    Format__Push(Format);
                    NativeImplClient.PushInt32(Height);
                    NativeImplClient.PushInt32(Width);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromWidthHeight PopDerived()
                {
                    var width = NativeImplClient.PopInt32();
                    var height = NativeImplClient.PopInt32();
                    var format = Format__Pop();
                    return new FromWidthHeight(width, height, format);
                }
            }
            public sealed record FromFilename(string Filename, Maybe<string> Format) : Deferred
            {
                public string Filename { get; } = Filename;
                public Maybe<string> Format { get; } = Format;
                internal override void Push(bool isReturn)
                {
                    __String_Option__Push(Format, isReturn);
                    NativeImplClient.PushString(Filename);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromFilename PopDerived()
                {
                    var filename = NativeImplClient.PopString();
                    var format = __String_Option__Pop();
                    return new FromFilename(filename, format);
                }
            }
            public sealed record FromData(INativeBuffer<byte> Data, int Width, int Height, Format Format, Maybe<IntPtr> BytesPerLine) : Deferred
            {
                public INativeBuffer<byte> Data { get; } = Data;
                public int Width { get; } = Width;
                public int Height { get; } = Height;
                public Format Format { get; } = Format;
                public Maybe<IntPtr> BytesPerLine { get; } = BytesPerLine;
                internal override void Push(bool isReturn)
                {
                    __SizeT_Option__Push(BytesPerLine, isReturn);
                    Format__Push(Format);
                    NativeImplClient.PushInt32(Height);
                    NativeImplClient.PushInt32(Width);
                    __Native_Byte_Buffer__Push(Data, isReturn);
                    // kind
                    NativeImplClient.PushInt32(3);
                }
                internal static FromData PopDerived()
                {
                    var data = __Native_Byte_Buffer__Pop();
                    var width = NativeImplClient.PopInt32();
                    var height = NativeImplClient.PopInt32();
                    var format = Format__Pop();
                    var bytesPerLine = __SizeT_Option__Pop();
                    return new FromData(data, width, height, format, bytesPerLine);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Deferred__Push(Deferred thing, bool isReturn)
        {
            thing.Push(isReturn);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Deferred Deferred__Pop()
        {
            return Deferred.Pop();
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Image");
            // assign module handles
            _realize = NativeImplClient.GetModuleMethod(_module, "realize");
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
