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
    public static class Date
    {
        private static ModuleHandle _module;
        internal static ModuleMethodHandle _handle_toYearMonthDay;
        internal static ModuleMethodHandle _owned_dispose;
        public struct YearMonthDay {
            public int Year;
            public int Month;
            public int Day;
            public YearMonthDay(int year, int month, int day)
            {
                this.Year = year;
                this.Month = month;
                this.Day = day;
            }
        }

        internal static void YearMonthDay__Push(YearMonthDay value, bool isReturn)
        {
            NativeImplClient.PushInt32(value.Day);
            NativeImplClient.PushInt32(value.Month);
            NativeImplClient.PushInt32(value.Year);
        }

        internal static YearMonthDay YearMonthDay__Pop()
        {
            var year = NativeImplClient.PopInt32();
            var month = NativeImplClient.PopInt32();
            var day = NativeImplClient.PopInt32();
            return new YearMonthDay(year, month, day);
        }
        public class Handle : IComparable
        {
            internal readonly IntPtr NativeHandle;
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
            public YearMonthDay ToYearMonthDay()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_toYearMonthDay);
                return YearMonthDay__Pop();
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
                    0 => FromYearMonthDay.PopDerived(),
                    _ => throw new Exception("Deferred.Pop() - unknown tag!")
                };
            }
            public sealed record FromYearMonthDay(int Year, int Month, int Day) : Deferred
            {
                public int Year { get; } = Year;
                public int Month { get; } = Month;
                public int Day { get; } = Day;
                internal override void Push(bool isReturn)
                {
                    NativeImplClient.PushInt32(Day);
                    NativeImplClient.PushInt32(Month);
                    NativeImplClient.PushInt32(Year);
                    // kind
                    NativeImplClient.PushInt32(0);
                }
                internal static FromYearMonthDay PopDerived()
                {
                    var year = NativeImplClient.PopInt32();
                    var month = NativeImplClient.PopInt32();
                    var day = NativeImplClient.PopInt32();
                    return new FromYearMonthDay(year, month, day);
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
            _module = NativeImplClient.GetModule("Date");
            // assign module handles
            _handle_toYearMonthDay = NativeImplClient.GetModuleMethod(_module, "Handle_toYearMonthDay");
            _owned_dispose = NativeImplClient.GetModuleMethod(_module, "Owned_dispose");

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
