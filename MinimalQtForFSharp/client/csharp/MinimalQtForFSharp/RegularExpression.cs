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
    public static class RegularExpression
    {
        private static ModuleHandle _module;
        [Flags]
        public enum PatternOptions
        {
            NoPatternOption = 0x0000,
            CaseInsensitiveOption = 0x0001,
            DotMatchesEverythingOption = 0x0002,
            MultilineOption = 0x0004,
            ExtendedPatternSyntaxOption = 0x0008,
            InvertedGreedinessOption = 0x0010,
            DontCaptureOption = 0x0020,
            UseUnicodePropertiesOption = 0x0040
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void PatternOptions__Push(PatternOptions value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static PatternOptions PatternOptions__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (PatternOptions)ret;
        }
        public abstract record Deferred
        {
            internal abstract void Push(bool isReturn);
            internal static Deferred Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => Empty.PopDerived(),
                    1 => Regex.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record Empty : Deferred
            {
                internal override void Push(bool isReturn)
                {
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static Empty PopDerived()
                {
                    return new Empty();
                }
            }
            public sealed record Regex(string Pattern, PatternOptions Opts) : Deferred
            {
                public string Pattern { get; } = Pattern;
                public PatternOptions Opts { get; } = Opts;
                internal override void Push(bool isReturn)
                {
                    PatternOptions__Push(Opts);
                    NativeImplClient.PushString(Pattern);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static Regex PopDerived()
                {
                    var pattern = NativeImplClient.PopString();
                    var opts = PatternOptions__Pop();
                    return new Regex(pattern, opts);
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
            _module = NativeImplClient.GetModule("RegularExpression");
            // assign module handles

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
