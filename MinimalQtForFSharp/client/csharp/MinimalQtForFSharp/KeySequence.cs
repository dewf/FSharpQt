using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Enums;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class KeySequence
    {
        private static ModuleHandle _module;
        public enum StandardKey
        {
            UnknownKey,
            HelpContents,
            WhatsThis,
            Open,
            Close,
            Save,
            New,
            Delete,
            Cut,
            Copy,
            Paste,
            Undo,
            Redo,
            Back,
            Forward,
            Refresh,
            ZoomIn,
            ZoomOut,
            Print,
            AddTab,
            NextChild,
            PreviousChild,
            Find,
            FindNext,
            FindPrevious,
            Replace,
            SelectAll,
            Bold,
            Italic,
            Underline,
            MoveToNextChar,
            MoveToPreviousChar,
            MoveToNextWord,
            MoveToPreviousWord,
            MoveToNextLine,
            MoveToPreviousLine,
            MoveToNextPage,
            MoveToPreviousPage,
            MoveToStartOfLine,
            MoveToEndOfLine,
            MoveToStartOfBlock,
            MoveToEndOfBlock,
            MoveToStartOfDocument,
            MoveToEndOfDocument,
            SelectNextChar,
            SelectPreviousChar,
            SelectNextWord,
            SelectPreviousWord,
            SelectNextLine,
            SelectPreviousLine,
            SelectNextPage,
            SelectPreviousPage,
            SelectStartOfLine,
            SelectEndOfLine,
            SelectStartOfBlock,
            SelectEndOfBlock,
            SelectStartOfDocument,
            SelectEndOfDocument,
            DeleteStartOfWord,
            DeleteEndOfWord,
            DeleteEndOfLine,
            InsertParagraphSeparator,
            InsertLineSeparator,
            SaveAs,
            Preferences,
            Quit,
            FullScreen,
            Deselect,
            DeleteCompleteLine,
            Backspace,
            Cancel
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void StandardKey__Push(StandardKey value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static StandardKey StandardKey__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (StandardKey)ret;
        }
        public abstract record Deferred
        {
            internal abstract void Push(bool isReturn);
            internal static Deferred Pop()
            {
                return NativeImplClient.PopInt32() switch
                {
                    0 => FromString.PopDerived(),
                    1 => FromStandard.PopDerived(),
                    2 => FromKey.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record FromString(string S) : Deferred
            {
                public string S { get; } = S;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushString(S);
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static FromString PopDerived()
                {
                    var s = NativeImplClient.PopString();
                    return new FromString(s);
                }
            }
            public sealed record FromStandard(StandardKey Key) : Deferred
            {
                public StandardKey Key { get; } = Key;
                internal override void Push(bool isReturn)
                {
                    StandardKey__Push(Key);
                    // kind
                    NativeImplClient.PushInt32(1);
                }
                internal static FromStandard PopDerived()
                {
                    var key = StandardKey__Pop();
                    return new FromStandard(key);
                }
            }
            public sealed record FromKey(Key Key, Modifiers Modifiers) : Deferred
            {
                public Key Key { get; } = Key;
                public Modifiers Modifiers { get; } = Modifiers;
                internal override void Push(bool isReturn)
                {
                    Modifiers__Push(Modifiers);
                    Key__Push(Key);
                    // kind
                    NativeImplClient.PushInt32(2);
                }
                internal static FromKey PopDerived()
                {
                    var key = Key__Pop();
                    var modifiers = Modifiers__Pop();
                    return new FromKey(key, modifiers);
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
            _module = NativeImplClient.GetModule("KeySequence");
            // assign module handles

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
