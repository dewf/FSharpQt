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
using static Org.Whatever.MinimalQtForFSharp.Image;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class Pixmap
    {
        private static ModuleHandle _module;

        private static void __ImageConversionFlags_Option__Push(Maybe<ImageConversionFlags> maybeValue, bool isReturn)
        {
            if (maybeValue.TryGetValue(out var value))
            {
                ImageConversionFlags__Push(value);
                NativeImplClient.PushBool(true);
            }
            else
            {
                NativeImplClient.PushBool(false);
            }
        }

        private static Maybe<ImageConversionFlags> __ImageConversionFlags_Option__Pop()
        {
            var hasValue = NativeImplClient.PopBool();
            if (hasValue)
            {
                return ImageConversionFlags__Pop();
            }
            return Maybe<ImageConversionFlags>.None;
        }
        internal static ModuleMethodHandle _realize;
        internal static ModuleMethodHandle _fromImage;
        internal static ModuleMethodHandle _handle_width;
        internal static ModuleMethodHandle _handle_height;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Realize(Deferred deferred)
        {
            Deferred__Push(deferred, false);
            NativeImplClient.InvokeModuleMethod(_realize);
            return Owned__Pop();
        }

        public static Owned FromImage(Image.Deferred image, Maybe<ImageConversionFlags> imageConversionFlags)
        {
            __ImageConversionFlags_Option__Push(imageConversionFlags, false);
            Image.Deferred__Push(image, false);
            NativeImplClient.InvokeModuleMethod(_fromImage);
            return Owned__Pop();
        }
        public class Handle : PaintDevice.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public int Width()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_width);
                return NativeImplClient.PopInt32();
            }
            public int Height()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_height);
                return NativeImplClient.PopInt32();
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
        public struct FilenameOptions
        {
            [Flags]
            internal enum Fields
            {
                Format = 1,
                ImageConversionFlags = 2
            }
            internal Fields UsedFields;

            private string _format;
            public string Format
            {
                set
                {
                    _format = value;
                    UsedFields |= Fields.Format;
                }
            }
            public readonly bool HasFormat(out string value)
            {
                if (UsedFields.HasFlag(Fields.Format))
                {
                    value = _format;
                    return true;
                }
                value = default;
                return false;
            }
            private ImageConversionFlags _imageConversionFlags;
            public ImageConversionFlags ImageConversionFlags
            {
                set
                {
                    _imageConversionFlags = value;
                    UsedFields |= Fields.ImageConversionFlags;
                }
            }
            public readonly bool HasImageConversionFlags(out ImageConversionFlags value)
            {
                if (UsedFields.HasFlag(Fields.ImageConversionFlags))
                {
                    value = _imageConversionFlags;
                    return true;
                }
                value = default;
                return false;
            }
        }
        internal static void FilenameOptions__Push(FilenameOptions value, bool isReturn)
        {
            if (value.HasImageConversionFlags(out var imageConversionFlags))
            {
                ImageConversionFlags__Push(imageConversionFlags);
            }
            if (value.HasFormat(out var format))
            {
                NativeImplClient.PushString(format);
            }
            NativeImplClient.PushInt32((int)value.UsedFields);
        }
        internal static FilenameOptions FilenameOptions__Pop()
        {
            var opts = new FilenameOptions
            {
                UsedFields = (FilenameOptions.Fields)NativeImplClient.PopInt32()
            };
            if (opts.UsedFields.HasFlag(FilenameOptions.Fields.Format))
            {
                opts.Format = NativeImplClient.PopString();
            }
            if (opts.UsedFields.HasFlag(FilenameOptions.Fields.ImageConversionFlags))
            {
                opts.ImageConversionFlags = ImageConversionFlags__Pop();
            }
            return opts;
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
            public sealed record FromWidthHeight(int Width, int Height) : Deferred
            {
                public int Width { get; } = Width;
                public int Height { get; } = Height;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushInt32(Height);
                    NativeImplClient.PushInt32(Width);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromWidthHeight PopDerived()
                {
                    var width = NativeImplClient.PopInt32();
                    var height = NativeImplClient.PopInt32();
                    return new FromWidthHeight(width, height);
                }
            }
            public sealed record FromFilename(string Filename, FilenameOptions Opts) : Deferred
            {
                public string Filename { get; } = Filename;
                public FilenameOptions Opts { get; } = Opts;
                internal override void Push(bool isReturn)
                {
                    FilenameOptions__Push(Opts, isReturn);
                    NativeImplClient.PushString(Filename);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromFilename PopDerived()
                {
                    var filename = NativeImplClient.PopString();
                    var opts = FilenameOptions__Pop();
                    return new FromFilename(filename, opts);
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
            _module = NativeImplClient.GetModule("Pixmap");
            // assign module handles
            _realize = NativeImplClient.GetModuleMethod(_module, "realize");
            _fromImage = NativeImplClient.GetModuleMethod(_module, "fromImage");
            _handle_width = NativeImplClient.GetModuleMethod(_module, "Handle_width");
            _handle_height = NativeImplClient.GetModuleMethod(_module, "Handle_height");
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
