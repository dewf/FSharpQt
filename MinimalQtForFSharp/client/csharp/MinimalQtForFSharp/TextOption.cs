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
    public static class TextOption
    {
        private static ModuleHandle _module;
        public enum WrapMode
        {
            NoWrap,
            WordWrap,
            ManualWrap,
            WrapAnywhere,
            WrapAtWordBoundaryOrAnywhere
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void WrapMode__Push(WrapMode value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static WrapMode WrapMode__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (WrapMode)ret;
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("TextOption");
            // assign module handles

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
