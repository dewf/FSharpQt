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
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _create_overload1;
        internal static ModuleMethodHandle _fromImage;
        internal static ModuleMethodHandle _handle_width;
        internal static ModuleMethodHandle _handle_height;
        internal static ModuleMethodHandle _owned_dispose;

        public static Owned Create(int width, int height)
        {
            NativeImplClient.PushInt32(height);
            NativeImplClient.PushInt32(width);
            NativeImplClient.InvokeModuleMethod(_create);
            return Owned__Pop();
        }

        public static Owned Create(string filename, FilenameOptions opts)
        {
            FilenameOptions__Push(opts, false);
            NativeImplClient.PushString(filename);
            NativeImplClient.InvokeModuleMethod(_create_overload1);
            return Owned__Pop();
        }

        public static Owned FromImage(Image.Handle image, Maybe<ImageConversionFlags> imageConversionFlags)
        {
            __ImageConversionFlags_Option__Push(imageConversionFlags, false);
            Image.Handle__Push(image);
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

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("Pixmap");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _create_overload1 = NativeImplClient.GetModuleMethod(_module, "create_overload1");
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
