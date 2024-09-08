using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CSharpFunctionalExtensions;
using Org.Whatever.MinimalQtForFSharp.Support;
using ModuleHandle = Org.Whatever.MinimalQtForFSharp.Support.ModuleHandle;

using static Org.Whatever.MinimalQtForFSharp.Object;
using static Org.Whatever.MinimalQtForFSharp.Enums;
using static Org.Whatever.MinimalQtForFSharp.AbstractItemModel;
using static Org.Whatever.MinimalQtForFSharp.AbstractProxyModel;
using static Org.Whatever.MinimalQtForFSharp.RegularExpression;
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;
using static Org.Whatever.MinimalQtForFSharp.PersistentModelIndex;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class SortFilterProxyModel
    {
        private static ModuleHandle _module;

        internal static void __PersistentModelIndex_Handle_Array__Push(PersistentModelIndex.Handle[] items)
        {
            var ptrs = items.Select(item => item.NativeHandle).ToArray();
            NativeImplClient.PushPtrArray(ptrs);
        }
        internal static PersistentModelIndex.Handle[] __PersistentModelIndex_Handle_Array__Pop()
        {
            return NativeImplClient.PopPtrArray()
                .Select(ptr => ptr != IntPtr.Zero ? new PersistentModelIndex.Handle(ptr) : null)
                .ToArray();
        }

        internal static void __ItemDataRole_Array__Push(ItemDataRole[] items)
        {
            var intValues = items.Select(i => (sbyte)i).ToArray();
            NativeImplClient.PushInt8Array(intValues);
        }

        internal static ItemDataRole[] __ItemDataRole_Array__Pop()
        {
            var intValues = NativeImplClient.PopInt8Array();
            return intValues.Select(i => (ItemDataRole)i).ToArray();
        }
        internal static ModuleMethodHandle _create;
        internal static ModuleMethodHandle _createSubclassed;
        internal static ModuleMethodHandle _handle_setAutoAcceptChildRows;
        internal static ModuleMethodHandle _handle_setDynamicSortFilter;
        internal static ModuleMethodHandle _handle_setFilterCaseSensitivity;
        internal static ModuleMethodHandle _handle_setFilterKeyColumn;
        internal static ModuleMethodHandle _handle_setFilterRegularExpression;
        internal static ModuleMethodHandle _handle_setFilterRole;
        internal static ModuleMethodHandle _handle_setSortLocaleAware;
        internal static ModuleMethodHandle _handle_setRecursiveFilteringEnabled;
        internal static ModuleMethodHandle _handle_setSortCaseSensitivity;
        internal static ModuleMethodHandle _handle_setSortRole;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _handle_dispose;
        internal static ModuleMethodHandle _interior_invalidateColumnsFilter;
        internal static ModuleMethodHandle _interior_invalidateRowsFilter;
        internal static ModuleMethodHandle _interior_invalidateFilter;
        internal static ModuleMethodHandle _interior_dispose;
        internal static InterfaceHandle _signalHandler;
        internal static InterfaceMethodHandle _signalHandler_destroyed;
        internal static InterfaceMethodHandle _signalHandler_objectNameChanged;
        internal static InterfaceMethodHandle _signalHandler_columnsAboutToBeInserted;
        internal static InterfaceMethodHandle _signalHandler_columnsAboutToBeMoved;
        internal static InterfaceMethodHandle _signalHandler_columnsAboutToBeRemoved;
        internal static InterfaceMethodHandle _signalHandler_columnsInserted;
        internal static InterfaceMethodHandle _signalHandler_columnsMoved;
        internal static InterfaceMethodHandle _signalHandler_columnsRemoved;
        internal static InterfaceMethodHandle _signalHandler_dataChanged;
        internal static InterfaceMethodHandle _signalHandler_headerDataChanged;
        internal static InterfaceMethodHandle _signalHandler_layoutAboutToBeChanged;
        internal static InterfaceMethodHandle _signalHandler_layoutChanged;
        internal static InterfaceMethodHandle _signalHandler_modelAboutToBeReset;
        internal static InterfaceMethodHandle _signalHandler_modelReset;
        internal static InterfaceMethodHandle _signalHandler_rowsAboutToBeInserted;
        internal static InterfaceMethodHandle _signalHandler_rowsAboutToBeMoved;
        internal static InterfaceMethodHandle _signalHandler_rowsAboutToBeRemoved;
        internal static InterfaceMethodHandle _signalHandler_rowsInserted;
        internal static InterfaceMethodHandle _signalHandler_rowsMoved;
        internal static InterfaceMethodHandle _signalHandler_rowsRemoved;
        internal static InterfaceMethodHandle _signalHandler_sourceModelChanged;
        internal static InterfaceMethodHandle _signalHandler_autoAcceptChildRowsChanged;
        internal static InterfaceMethodHandle _signalHandler_filterCaseSensitivityChanged;
        internal static InterfaceMethodHandle _signalHandler_filterRoleChanged;
        internal static InterfaceMethodHandle _signalHandler_recursiveFilteringEnabledChanged;
        internal static InterfaceMethodHandle _signalHandler_sortCaseSensitivityChanged;
        internal static InterfaceMethodHandle _signalHandler_sortLocaleAwareChanged;
        internal static InterfaceMethodHandle _signalHandler_sortRoleChanged;
        internal static InterfaceHandle _methodDelegate;
        internal static InterfaceMethodHandle _methodDelegate_filterAcceptsColumn;
        internal static InterfaceMethodHandle _methodDelegate_filterAcceptsRow;
        internal static InterfaceMethodHandle _methodDelegate_lessThan;

        public static Handle Create(SignalHandler handler)
        {
            SignalHandler__Push(handler, false);
            NativeImplClient.InvokeModuleMethod(_create);
            return Handle__Pop();
        }

        public static Interior CreateSubclassed(SignalHandler handler, MethodDelegate methodDelegate, MethodMask methodMask)
        {
            MethodMask__Push(methodMask);
            MethodDelegate__Push(methodDelegate, false);
            SignalHandler__Push(handler, false);
            NativeImplClient.InvokeModuleMethod(_createSubclassed);
            return Interior__Pop();
        }
        [Flags]
        public enum SignalMask
        {
            // Object.SignalMask:
            Destroyed = 1 << 0,
            ObjectNameChanged = 1 << 1,
            // AbstractItemModel.SignalMask:
            ColumnsAboutToBeInserted = 1 << 2,
            ColumnsAboutToBeMoved = 1 << 3,
            ColumnsAboutToBeRemoved = 1 << 4,
            ColumnsInserted = 1 << 5,
            ColumnsMoved = 1 << 6,
            ColumnsRemoved = 1 << 7,
            DataChanged = 1 << 8,
            HeaderDataChanged = 1 << 9,
            LayoutAboutToBeChanged = 1 << 10,
            LayoutChanged = 1 << 11,
            ModelAboutToBeReset = 1 << 12,
            ModelReset = 1 << 13,
            RowsAboutToBeInserted = 1 << 14,
            RowsAboutToBeMoved = 1 << 15,
            RowsAboutToBeRemoved = 1 << 16,
            RowsInserted = 1 << 17,
            RowsMoved = 1 << 18,
            RowsRemoved = 1 << 19,
            // AbstractProxyModel.SignalMask:
            SourceModelChanged = 1 << 20,
            // SignalMask:
            AutoAcceptChildRowsChanged = 1 << 21,
            FilterCaseSensitivityChanged = 1 << 22,
            FilterRoleChanged = 1 << 23,
            RecursiveFilteringEnabledChanged = 1 << 24,
            SortCaseSensitivityChanged = 1 << 25,
            SortLocaleAwareChanged = 1 << 26,
            SortRoleChanged = 1 << 27
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SignalMask__Push(SignalMask value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignalMask SignalMask__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (SignalMask)ret;
        }

        public interface SignalHandler : IDisposable
        {
            void IDisposable.Dispose()
            {
                // nothing by default
            }
            void Destroyed(Object.Handle obj);
            void ObjectNameChanged(string objectName);
            void ColumnsAboutToBeInserted(ModelIndex.Handle parent, int first, int last);
            void ColumnsAboutToBeMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationColumn);
            void ColumnsAboutToBeRemoved(ModelIndex.Handle parent, int first, int last);
            void ColumnsInserted(ModelIndex.Handle parent, int first, int last);
            void ColumnsMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationColumn);
            void ColumnsRemoved(ModelIndex.Handle parent, int first, int last);
            void DataChanged(ModelIndex.Handle topLeft, ModelIndex.Handle bottomRight, ItemDataRole[] roles);
            void HeaderDataChanged(Orientation orientation, int first, int last);
            void LayoutAboutToBeChanged(PersistentModelIndex.Handle[] parents, LayoutChangeHint hint);
            void LayoutChanged(PersistentModelIndex.Handle[] parents, LayoutChangeHint hint);
            void ModelAboutToBeReset();
            void ModelReset();
            void RowsAboutToBeInserted(ModelIndex.Handle parent, int start, int end);
            void RowsAboutToBeMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationRow);
            void RowsAboutToBeRemoved(ModelIndex.Handle parent, int first, int last);
            void RowsInserted(ModelIndex.Handle parent, int first, int last);
            void RowsMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationRow);
            void RowsRemoved(ModelIndex.Handle parent, int first, int last);
            void SourceModelChanged();
            void AutoAcceptChildRowsChanged(bool autoAcceptChildRows);
            void FilterCaseSensitivityChanged(CaseSensitivity filterCaseSensitivity);
            void FilterRoleChanged(ItemDataRole filterRole);
            void RecursiveFilteringEnabledChanged(bool recursiveFilteringEnabled);
            void SortCaseSensitivityChanged(CaseSensitivity sortCaseSensitivity);
            void SortLocaleAwareChanged(bool sortLocaleAware);
            void SortRoleChanged(ItemDataRole sortRole);
        }

        private static Dictionary<SignalHandler, IPushable> __SignalHandlerToPushable = new();
        internal class __SignalHandlerWrapper : ClientInterfaceWrapper<SignalHandler>
        {
            public __SignalHandlerWrapper(SignalHandler rawInterface) : base(rawInterface)
            {
            }
            protected override void ReleaseExtra()
            {
                // remove the raw interface from the lookup table, no longer needed
                __SignalHandlerToPushable.Remove(RawInterface);
            }
        }

        internal static void SignalHandler__Push(SignalHandler thing, bool isReturn)
        {
            if (thing != null)
            {
                if (__SignalHandlerToPushable.TryGetValue(thing, out var pushable))
                {
                    // either an already-known client thing, or a server thing
                    pushable.Push(isReturn);
                }
                else
                {
                    // as-yet-unknown client thing - wrap and add to lookup table
                    pushable = new __SignalHandlerWrapper(thing);
                    __SignalHandlerToPushable.Add(thing, pushable);
                }
                pushable.Push(isReturn);
            }
            else
            {
                NativeImplClient.PushNull();
            }
        }

        internal static SignalHandler SignalHandler__Pop()
        {
            NativeImplClient.PopInstanceId(out var id, out var isClientId);
            if (id != 0)
            {
                if (isClientId)
                {
                    // we must have sent it over originally, so wrapper must exist
                    var wrapper = (__SignalHandlerWrapper)ClientObject.GetById(id);
                    return wrapper.RawInterface;
                }
                else // server ID
                {
                    var thing = new ServerSignalHandler(id);
                    // add to lookup table before returning
                    __SignalHandlerToPushable.Add(thing, thing);
                    return thing;
                }
            }
            else
            {
                return null;
            }
        }

        private class ServerSignalHandler : ServerObject, SignalHandler
        {
            public ServerSignalHandler(int id) : base(id)
            {
            }

            public void Destroyed(Object.Handle obj)
            {
                Object.Handle__Push(obj);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_destroyed, Id);
            }

            public void ObjectNameChanged(string objectName)
            {
                NativeImplClient.PushString(objectName);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_objectNameChanged, Id);
            }

            public void ColumnsAboutToBeInserted(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_columnsAboutToBeInserted, Id);
            }

            public void ColumnsAboutToBeMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationColumn)
            {
                NativeImplClient.PushInt32(destinationColumn);
                ModelIndex.Handle__Push(destinationParent);
                NativeImplClient.PushInt32(sourceEnd);
                NativeImplClient.PushInt32(sourceStart);
                ModelIndex.Handle__Push(sourceParent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_columnsAboutToBeMoved, Id);
            }

            public void ColumnsAboutToBeRemoved(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_columnsAboutToBeRemoved, Id);
            }

            public void ColumnsInserted(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_columnsInserted, Id);
            }

            public void ColumnsMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationColumn)
            {
                NativeImplClient.PushInt32(destinationColumn);
                ModelIndex.Handle__Push(destinationParent);
                NativeImplClient.PushInt32(sourceEnd);
                NativeImplClient.PushInt32(sourceStart);
                ModelIndex.Handle__Push(sourceParent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_columnsMoved, Id);
            }

            public void ColumnsRemoved(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_columnsRemoved, Id);
            }

            public void DataChanged(ModelIndex.Handle topLeft, ModelIndex.Handle bottomRight, ItemDataRole[] roles)
            {
                __ItemDataRole_Array__Push(roles);
                ModelIndex.Handle__Push(bottomRight);
                ModelIndex.Handle__Push(topLeft);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_dataChanged, Id);
            }

            public void HeaderDataChanged(Orientation orientation, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                Orientation__Push(orientation);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_headerDataChanged, Id);
            }

            public void LayoutAboutToBeChanged(PersistentModelIndex.Handle[] parents, LayoutChangeHint hint)
            {
                LayoutChangeHint__Push(hint);
                __PersistentModelIndex_Handle_Array__Push(parents);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_layoutAboutToBeChanged, Id);
            }

            public void LayoutChanged(PersistentModelIndex.Handle[] parents, LayoutChangeHint hint)
            {
                LayoutChangeHint__Push(hint);
                __PersistentModelIndex_Handle_Array__Push(parents);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_layoutChanged, Id);
            }

            public void ModelAboutToBeReset()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_modelAboutToBeReset, Id);
            }

            public void ModelReset()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_modelReset, Id);
            }

            public void RowsAboutToBeInserted(ModelIndex.Handle parent, int start, int end)
            {
                NativeImplClient.PushInt32(end);
                NativeImplClient.PushInt32(start);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rowsAboutToBeInserted, Id);
            }

            public void RowsAboutToBeMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationRow)
            {
                NativeImplClient.PushInt32(destinationRow);
                ModelIndex.Handle__Push(destinationParent);
                NativeImplClient.PushInt32(sourceEnd);
                NativeImplClient.PushInt32(sourceStart);
                ModelIndex.Handle__Push(sourceParent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rowsAboutToBeMoved, Id);
            }

            public void RowsAboutToBeRemoved(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rowsAboutToBeRemoved, Id);
            }

            public void RowsInserted(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rowsInserted, Id);
            }

            public void RowsMoved(ModelIndex.Handle sourceParent, int sourceStart, int sourceEnd, ModelIndex.Handle destinationParent, int destinationRow)
            {
                NativeImplClient.PushInt32(destinationRow);
                ModelIndex.Handle__Push(destinationParent);
                NativeImplClient.PushInt32(sourceEnd);
                NativeImplClient.PushInt32(sourceStart);
                ModelIndex.Handle__Push(sourceParent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rowsMoved, Id);
            }

            public void RowsRemoved(ModelIndex.Handle parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_rowsRemoved, Id);
            }

            public void SourceModelChanged()
            {
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sourceModelChanged, Id);
            }

            public void AutoAcceptChildRowsChanged(bool autoAcceptChildRows)
            {
                NativeImplClient.PushBool(autoAcceptChildRows);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_autoAcceptChildRowsChanged, Id);
            }

            public void FilterCaseSensitivityChanged(CaseSensitivity filterCaseSensitivity)
            {
                CaseSensitivity__Push(filterCaseSensitivity);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_filterCaseSensitivityChanged, Id);
            }

            public void FilterRoleChanged(ItemDataRole filterRole)
            {
                ItemDataRole__Push(filterRole);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_filterRoleChanged, Id);
            }

            public void RecursiveFilteringEnabledChanged(bool recursiveFilteringEnabled)
            {
                NativeImplClient.PushBool(recursiveFilteringEnabled);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_recursiveFilteringEnabledChanged, Id);
            }

            public void SortCaseSensitivityChanged(CaseSensitivity sortCaseSensitivity)
            {
                CaseSensitivity__Push(sortCaseSensitivity);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sortCaseSensitivityChanged, Id);
            }

            public void SortLocaleAwareChanged(bool sortLocaleAware)
            {
                NativeImplClient.PushBool(sortLocaleAware);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sortLocaleAwareChanged, Id);
            }

            public void SortRoleChanged(ItemDataRole sortRole)
            {
                ItemDataRole__Push(sortRole);
                NativeImplClient.InvokeInterfaceMethod(_signalHandler_sortRoleChanged, Id);
            }

            protected override void ReleaseExtra()
            {
                // remove from lookup table
                __SignalHandlerToPushable.Remove(this);
            }

            public void Dispose()
            {
                // will invoke ReleaseExtra() for us
                ServerDispose();
            }
        }
        public class Handle : AbstractProxyModel.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public override void Dispose()
            {
                if (!_disposed)
                {
                    Handle__Push(this);
                    NativeImplClient.InvokeModuleMethod(_handle_dispose);
                    _disposed = true;
                }
            }
            public void SetAutoAcceptChildRows(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setAutoAcceptChildRows);
            }
            public void SetDynamicSortFilter(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setDynamicSortFilter);
            }
            public void SetFilterCaseSensitivity(CaseSensitivity sensitivity)
            {
                CaseSensitivity__Push(sensitivity);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFilterCaseSensitivity);
            }
            public void SetFilterKeyColumn(int filterKeyColumn)
            {
                NativeImplClient.PushInt32(filterKeyColumn);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFilterKeyColumn);
            }
            public void SetFilterRegularExpression(RegularExpression.Deferred regex)
            {
                RegularExpression.Deferred__Push(regex, false);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFilterRegularExpression);
            }
            public void SetFilterRole(ItemDataRole filterRole)
            {
                ItemDataRole__Push(filterRole);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setFilterRole);
            }
            public void SetSortLocaleAware(bool state)
            {
                NativeImplClient.PushBool(state);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSortLocaleAware);
            }
            public void SetRecursiveFilteringEnabled(bool enabled)
            {
                NativeImplClient.PushBool(enabled);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setRecursiveFilteringEnabled);
            }
            public void SetSortCaseSensitivity(CaseSensitivity sensitivity)
            {
                CaseSensitivity__Push(sensitivity);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSortCaseSensitivity);
            }
            public void SetSortRole(ItemDataRole sortRole)
            {
                ItemDataRole__Push(sortRole);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSortRole);
            }
            public void SetSignalMask(SignalMask mask)
            {
                SignalMask__Push(mask);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSignalMask);
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
        public class Interior : Handle
        {
            internal Interior(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public override void Dispose()
            {
                if (!_disposed)
                {
                    Interior__Push(this);
                    NativeImplClient.InvokeModuleMethod(_interior_dispose);
                    _disposed = true;
                }
            }
            public void InvalidateColumnsFilter()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_invalidateColumnsFilter);
            }
            public void InvalidateRowsFilter()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_invalidateRowsFilter);
            }
            public void InvalidateFilter()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_invalidateFilter);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Interior__Push(Interior thing)
        {
            NativeImplClient.PushPtr(thing?.NativeHandle ?? IntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Interior Interior__Pop()
        {
            var ptr = NativeImplClient.PopPtr();
            return ptr != IntPtr.Zero ? new Interior(ptr) : null;
        }
        [Flags]
        public enum MethodMask
        {
            // MethodMask:
            FilterAcceptsColumn = 1 << 0,
            FilterAcceptsRow = 1 << 1,
            LessThan = 1 << 2
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MethodMask__Push(MethodMask value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodMask MethodMask__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (MethodMask)ret;
        }

        public interface MethodDelegate : IDisposable
        {
            void IDisposable.Dispose()
            {
                // nothing by default
            }
            bool FilterAcceptsColumn(int sourceColumn, ModelIndex.Handle sourceParent);
            bool FilterAcceptsRow(int sourceRow, ModelIndex.Handle sourceParent);
            bool LessThan(ModelIndex.Handle sourceLeft, ModelIndex.Handle sourceRight);
        }

        private static Dictionary<MethodDelegate, IPushable> __MethodDelegateToPushable = new();
        internal class __MethodDelegateWrapper : ClientInterfaceWrapper<MethodDelegate>
        {
            public __MethodDelegateWrapper(MethodDelegate rawInterface) : base(rawInterface)
            {
            }
            protected override void ReleaseExtra()
            {
                // remove the raw interface from the lookup table, no longer needed
                __MethodDelegateToPushable.Remove(RawInterface);
            }
        }

        internal static void MethodDelegate__Push(MethodDelegate thing, bool isReturn)
        {
            if (thing != null)
            {
                if (__MethodDelegateToPushable.TryGetValue(thing, out var pushable))
                {
                    // either an already-known client thing, or a server thing
                    pushable.Push(isReturn);
                }
                else
                {
                    // as-yet-unknown client thing - wrap and add to lookup table
                    pushable = new __MethodDelegateWrapper(thing);
                    __MethodDelegateToPushable.Add(thing, pushable);
                }
                pushable.Push(isReturn);
            }
            else
            {
                NativeImplClient.PushNull();
            }
        }

        internal static MethodDelegate MethodDelegate__Pop()
        {
            NativeImplClient.PopInstanceId(out var id, out var isClientId);
            if (id != 0)
            {
                if (isClientId)
                {
                    // we must have sent it over originally, so wrapper must exist
                    var wrapper = (__MethodDelegateWrapper)ClientObject.GetById(id);
                    return wrapper.RawInterface;
                }
                else // server ID
                {
                    var thing = new ServerMethodDelegate(id);
                    // add to lookup table before returning
                    __MethodDelegateToPushable.Add(thing, thing);
                    return thing;
                }
            }
            else
            {
                return null;
            }
        }

        private class ServerMethodDelegate : ServerObject, MethodDelegate
        {
            public ServerMethodDelegate(int id) : base(id)
            {
            }

            public bool FilterAcceptsColumn(int sourceColumn, ModelIndex.Handle sourceParent)
            {
                ModelIndex.Handle__Push(sourceParent);
                NativeImplClient.PushInt32(sourceColumn);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_filterAcceptsColumn, Id);
                return NativeImplClient.PopBool();
            }

            public bool FilterAcceptsRow(int sourceRow, ModelIndex.Handle sourceParent)
            {
                ModelIndex.Handle__Push(sourceParent);
                NativeImplClient.PushInt32(sourceRow);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_filterAcceptsRow, Id);
                return NativeImplClient.PopBool();
            }

            public bool LessThan(ModelIndex.Handle sourceLeft, ModelIndex.Handle sourceRight)
            {
                ModelIndex.Handle__Push(sourceRight);
                ModelIndex.Handle__Push(sourceLeft);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_lessThan, Id);
                return NativeImplClient.PopBool();
            }

            protected override void ReleaseExtra()
            {
                // remove from lookup table
                __MethodDelegateToPushable.Remove(this);
            }

            public void Dispose()
            {
                // will invoke ReleaseExtra() for us
                ServerDispose();
            }
        }

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("SortFilterProxyModel");
            // assign module handles
            _create = NativeImplClient.GetModuleMethod(_module, "create");
            _createSubclassed = NativeImplClient.GetModuleMethod(_module, "createSubclassed");
            _handle_setAutoAcceptChildRows = NativeImplClient.GetModuleMethod(_module, "Handle_setAutoAcceptChildRows");
            _handle_setDynamicSortFilter = NativeImplClient.GetModuleMethod(_module, "Handle_setDynamicSortFilter");
            _handle_setFilterCaseSensitivity = NativeImplClient.GetModuleMethod(_module, "Handle_setFilterCaseSensitivity");
            _handle_setFilterKeyColumn = NativeImplClient.GetModuleMethod(_module, "Handle_setFilterKeyColumn");
            _handle_setFilterRegularExpression = NativeImplClient.GetModuleMethod(_module, "Handle_setFilterRegularExpression");
            _handle_setFilterRole = NativeImplClient.GetModuleMethod(_module, "Handle_setFilterRole");
            _handle_setSortLocaleAware = NativeImplClient.GetModuleMethod(_module, "Handle_setSortLocaleAware");
            _handle_setRecursiveFilteringEnabled = NativeImplClient.GetModuleMethod(_module, "Handle_setRecursiveFilteringEnabled");
            _handle_setSortCaseSensitivity = NativeImplClient.GetModuleMethod(_module, "Handle_setSortCaseSensitivity");
            _handle_setSortRole = NativeImplClient.GetModuleMethod(_module, "Handle_setSortRole");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _handle_dispose = NativeImplClient.GetModuleMethod(_module, "Handle_dispose");
            _interior_invalidateColumnsFilter = NativeImplClient.GetModuleMethod(_module, "Interior_invalidateColumnsFilter");
            _interior_invalidateRowsFilter = NativeImplClient.GetModuleMethod(_module, "Interior_invalidateRowsFilter");
            _interior_invalidateFilter = NativeImplClient.GetModuleMethod(_module, "Interior_invalidateFilter");
            _interior_dispose = NativeImplClient.GetModuleMethod(_module, "Interior_dispose");
            _signalHandler = NativeImplClient.GetInterface(_module, "SignalHandler");
            _signalHandler_destroyed = NativeImplClient.GetInterfaceMethod(_signalHandler, "destroyed");
            _signalHandler_objectNameChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "objectNameChanged");
            _signalHandler_columnsAboutToBeInserted = NativeImplClient.GetInterfaceMethod(_signalHandler, "columnsAboutToBeInserted");
            _signalHandler_columnsAboutToBeMoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "columnsAboutToBeMoved");
            _signalHandler_columnsAboutToBeRemoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "columnsAboutToBeRemoved");
            _signalHandler_columnsInserted = NativeImplClient.GetInterfaceMethod(_signalHandler, "columnsInserted");
            _signalHandler_columnsMoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "columnsMoved");
            _signalHandler_columnsRemoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "columnsRemoved");
            _signalHandler_dataChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "dataChanged");
            _signalHandler_headerDataChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "headerDataChanged");
            _signalHandler_layoutAboutToBeChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "layoutAboutToBeChanged");
            _signalHandler_layoutChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "layoutChanged");
            _signalHandler_modelAboutToBeReset = NativeImplClient.GetInterfaceMethod(_signalHandler, "modelAboutToBeReset");
            _signalHandler_modelReset = NativeImplClient.GetInterfaceMethod(_signalHandler, "modelReset");
            _signalHandler_rowsAboutToBeInserted = NativeImplClient.GetInterfaceMethod(_signalHandler, "rowsAboutToBeInserted");
            _signalHandler_rowsAboutToBeMoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "rowsAboutToBeMoved");
            _signalHandler_rowsAboutToBeRemoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "rowsAboutToBeRemoved");
            _signalHandler_rowsInserted = NativeImplClient.GetInterfaceMethod(_signalHandler, "rowsInserted");
            _signalHandler_rowsMoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "rowsMoved");
            _signalHandler_rowsRemoved = NativeImplClient.GetInterfaceMethod(_signalHandler, "rowsRemoved");
            _signalHandler_sourceModelChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "sourceModelChanged");
            _signalHandler_autoAcceptChildRowsChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "autoAcceptChildRowsChanged");
            _signalHandler_filterCaseSensitivityChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "filterCaseSensitivityChanged");
            _signalHandler_filterRoleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "filterRoleChanged");
            _signalHandler_recursiveFilteringEnabledChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "recursiveFilteringEnabledChanged");
            _signalHandler_sortCaseSensitivityChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "sortCaseSensitivityChanged");
            _signalHandler_sortLocaleAwareChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "sortLocaleAwareChanged");
            _signalHandler_sortRoleChanged = NativeImplClient.GetInterfaceMethod(_signalHandler, "sortRoleChanged");
            NativeImplClient.SetClientMethodWrapper(_signalHandler_destroyed, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var obj = Object.Handle__Pop();
                inst.Destroyed(obj);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_objectNameChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var objectName = NativeImplClient.PopString();
                inst.ObjectNameChanged(objectName);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_columnsAboutToBeInserted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.ColumnsAboutToBeInserted(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_columnsAboutToBeMoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sourceParent = ModelIndex.Handle__Pop();
                var sourceStart = NativeImplClient.PopInt32();
                var sourceEnd = NativeImplClient.PopInt32();
                var destinationParent = ModelIndex.Handle__Pop();
                var destinationColumn = NativeImplClient.PopInt32();
                inst.ColumnsAboutToBeMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_columnsAboutToBeRemoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.ColumnsAboutToBeRemoved(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_columnsInserted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.ColumnsInserted(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_columnsMoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sourceParent = ModelIndex.Handle__Pop();
                var sourceStart = NativeImplClient.PopInt32();
                var sourceEnd = NativeImplClient.PopInt32();
                var destinationParent = ModelIndex.Handle__Pop();
                var destinationColumn = NativeImplClient.PopInt32();
                inst.ColumnsMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_columnsRemoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.ColumnsRemoved(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_dataChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var topLeft = ModelIndex.Handle__Pop();
                var bottomRight = ModelIndex.Handle__Pop();
                var roles = __ItemDataRole_Array__Pop();
                inst.DataChanged(topLeft, bottomRight, roles);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_headerDataChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var orientation = Orientation__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.HeaderDataChanged(orientation, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_layoutAboutToBeChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parents = __PersistentModelIndex_Handle_Array__Pop();
                var hint = LayoutChangeHint__Pop();
                inst.LayoutAboutToBeChanged(parents, hint);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_layoutChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parents = __PersistentModelIndex_Handle_Array__Pop();
                var hint = LayoutChangeHint__Pop();
                inst.LayoutChanged(parents, hint);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_modelAboutToBeReset, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.ModelAboutToBeReset();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_modelReset, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.ModelReset();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rowsAboutToBeInserted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var start = NativeImplClient.PopInt32();
                var end = NativeImplClient.PopInt32();
                inst.RowsAboutToBeInserted(parent, start, end);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rowsAboutToBeMoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sourceParent = ModelIndex.Handle__Pop();
                var sourceStart = NativeImplClient.PopInt32();
                var sourceEnd = NativeImplClient.PopInt32();
                var destinationParent = ModelIndex.Handle__Pop();
                var destinationRow = NativeImplClient.PopInt32();
                inst.RowsAboutToBeMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rowsAboutToBeRemoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.RowsAboutToBeRemoved(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rowsInserted, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.RowsInserted(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rowsMoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sourceParent = ModelIndex.Handle__Pop();
                var sourceStart = NativeImplClient.PopInt32();
                var sourceEnd = NativeImplClient.PopInt32();
                var destinationParent = ModelIndex.Handle__Pop();
                var destinationRow = NativeImplClient.PopInt32();
                inst.RowsMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_rowsRemoved, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                var first = NativeImplClient.PopInt32();
                var last = NativeImplClient.PopInt32();
                inst.RowsRemoved(parent, first, last);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sourceModelChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                inst.SourceModelChanged();
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_autoAcceptChildRowsChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var autoAcceptChildRows = NativeImplClient.PopBool();
                inst.AutoAcceptChildRowsChanged(autoAcceptChildRows);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_filterCaseSensitivityChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var filterCaseSensitivity = CaseSensitivity__Pop();
                inst.FilterCaseSensitivityChanged(filterCaseSensitivity);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_filterRoleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var filterRole = ItemDataRole__Pop();
                inst.FilterRoleChanged(filterRole);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_recursiveFilteringEnabledChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var recursiveFilteringEnabled = NativeImplClient.PopBool();
                inst.RecursiveFilteringEnabledChanged(recursiveFilteringEnabled);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sortCaseSensitivityChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sortCaseSensitivity = CaseSensitivity__Pop();
                inst.SortCaseSensitivityChanged(sortCaseSensitivity);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sortLocaleAwareChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sortLocaleAware = NativeImplClient.PopBool();
                inst.SortLocaleAwareChanged(sortLocaleAware);
            });
            NativeImplClient.SetClientMethodWrapper(_signalHandler_sortRoleChanged, delegate(ClientObject __obj)
            {
                var inst = ((__SignalHandlerWrapper)__obj).RawInterface;
                var sortRole = ItemDataRole__Pop();
                inst.SortRoleChanged(sortRole);
            });
            _methodDelegate = NativeImplClient.GetInterface(_module, "MethodDelegate");
            _methodDelegate_filterAcceptsColumn = NativeImplClient.GetInterfaceMethod(_methodDelegate, "filterAcceptsColumn");
            _methodDelegate_filterAcceptsRow = NativeImplClient.GetInterfaceMethod(_methodDelegate, "filterAcceptsRow");
            _methodDelegate_lessThan = NativeImplClient.GetInterfaceMethod(_methodDelegate, "lessThan");
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_filterAcceptsColumn, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var sourceColumn = NativeImplClient.PopInt32();
                var sourceParent = ModelIndex.Handle__Pop();
                NativeImplClient.PushBool(inst.FilterAcceptsColumn(sourceColumn, sourceParent));
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_filterAcceptsRow, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var sourceRow = NativeImplClient.PopInt32();
                var sourceParent = ModelIndex.Handle__Pop();
                NativeImplClient.PushBool(inst.FilterAcceptsRow(sourceRow, sourceParent));
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_lessThan, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var sourceLeft = ModelIndex.Handle__Pop();
                var sourceRight = ModelIndex.Handle__Pop();
                NativeImplClient.PushBool(inst.LessThan(sourceLeft, sourceRight));
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
