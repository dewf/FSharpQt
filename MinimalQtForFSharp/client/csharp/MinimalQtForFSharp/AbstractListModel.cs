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
using static Org.Whatever.MinimalQtForFSharp.Variant;
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;
using static Org.Whatever.MinimalQtForFSharp.PersistentModelIndex;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class AbstractListModel
    {
        private static ModuleHandle _module;

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
        internal static ModuleMethodHandle _createSubclassed;
        internal static ModuleMethodHandle _handle_setSignalMask;
        internal static ModuleMethodHandle _interior_emitDataChanged;
        internal static ModuleMethodHandle _interior_emitHeaderDataChanged;
        internal static ModuleMethodHandle _interior_beginInsertRows;
        internal static ModuleMethodHandle _interior_endInsertRows;
        internal static ModuleMethodHandle _interior_beginRemoveRows;
        internal static ModuleMethodHandle _interior_endRemoveRows;
        internal static ModuleMethodHandle _interior_beginResetModel;
        internal static ModuleMethodHandle _interior_endResetModel;
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
        internal static InterfaceHandle _methodDelegate;
        internal static InterfaceMethodHandle _methodDelegate_rowCount;
        internal static InterfaceMethodHandle _methodDelegate_data;
        internal static InterfaceMethodHandle _methodDelegate_headerData;
        internal static InterfaceMethodHandle _methodDelegate_getFlags;
        internal static InterfaceMethodHandle _methodDelegate_setData;
        internal static InterfaceMethodHandle _methodDelegate_columnCount;

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
            // SignalMask:
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
        public class Handle : AbstractItemModel.Handle
        {
            internal Handle(IntPtr nativeHandle) : base(nativeHandle)
            {
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
        public class Interior : Handle, IDisposable
        {
            protected bool _disposed;
            internal Interior(IntPtr nativeHandle) : base(nativeHandle)
            {
            }
            public virtual void Dispose()
            {
                if (!_disposed)
                {
                    Interior__Push(this);
                    NativeImplClient.InvokeModuleMethod(_interior_dispose);
                    _disposed = true;
                }
            }
            public void EmitDataChanged(ModelIndex.Deferred topLeft, ModelIndex.Deferred bottomRight, ItemDataRole[] roles)
            {
                __ItemDataRole_Array__Push(roles);
                ModelIndex.Deferred__Push(bottomRight, false);
                ModelIndex.Deferred__Push(topLeft, false);
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_emitDataChanged);
            }
            public void EmitHeaderDataChanged(Orientation orientation, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                Orientation__Push(orientation);
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_emitHeaderDataChanged);
            }
            public void BeginInsertRows(ModelIndex.Deferred parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Deferred__Push(parent, false);
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_beginInsertRows);
            }
            public void EndInsertRows()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_endInsertRows);
            }
            public void BeginRemoveRows(ModelIndex.Deferred parent, int first, int last)
            {
                NativeImplClient.PushInt32(last);
                NativeImplClient.PushInt32(first);
                ModelIndex.Deferred__Push(parent, false);
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_beginRemoveRows);
            }
            public void EndRemoveRows()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_endRemoveRows);
            }
            public void BeginResetModel()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_beginResetModel);
            }
            public void EndResetModel()
            {
                Interior__Push(this);
                NativeImplClient.InvokeModuleMethod(_interior_endResetModel);
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
        public enum ItemFlags
        {
            NoItemFlags = 0,
            ItemIsSelectable = 1,
            ItemIsEditable = 2,
            ItemIsDragEnabled = 4,
            ItemIsDropEnabled = 8,
            ItemIsUserCheckable = 16,
            ItemIsEnabled = 32,
            ItemIsAutoTristate = 64,
            ItemNeverHasChildren = 128,
            ItemIsUserTristate = 256
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ItemFlags__Push(ItemFlags value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ItemFlags ItemFlags__Pop()
        {
            var ret = NativeImplClient.PopInt32();
            return (ItemFlags)ret;
        }
        [Flags]
        public enum MethodMask
        {
            HeaderData = 1,
            Flags = 1 << 1,
            SetData = 1 << 2,
            ColumnCount = 1 << 3
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void MethodMask__Push(MethodMask value)
        {
            NativeImplClient.PushInt32((int)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static MethodMask MethodMask__Pop()
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
            int RowCount(ModelIndex.Handle parent);
            Variant.Deferred Data(ModelIndex.Handle index, ItemDataRole role);
            Variant.Deferred HeaderData(int section, Orientation orientation, ItemDataRole role);
            ItemFlags GetFlags(ModelIndex.Handle index, ItemFlags baseFlags);
            bool SetData(ModelIndex.Handle index, Variant.Handle value, ItemDataRole role);
            int ColumnCount(ModelIndex.Handle parent);
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

            public int RowCount(ModelIndex.Handle parent)
            {
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_rowCount, Id);
                return NativeImplClient.PopInt32();
            }

            public Variant.Deferred Data(ModelIndex.Handle index, ItemDataRole role)
            {
                ItemDataRole__Push(role);
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_data, Id);
                return Variant.Deferred__Pop();
            }

            public Variant.Deferred HeaderData(int section, Orientation orientation, ItemDataRole role)
            {
                ItemDataRole__Push(role);
                Orientation__Push(orientation);
                NativeImplClient.PushInt32(section);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_headerData, Id);
                return Variant.Deferred__Pop();
            }

            public ItemFlags GetFlags(ModelIndex.Handle index, ItemFlags baseFlags)
            {
                ItemFlags__Push(baseFlags);
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_getFlags, Id);
                return ItemFlags__Pop();
            }

            public bool SetData(ModelIndex.Handle index, Variant.Handle value, ItemDataRole role)
            {
                ItemDataRole__Push(role);
                Variant.Handle__Push(value);
                ModelIndex.Handle__Push(index);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_setData, Id);
                return NativeImplClient.PopBool();
            }

            public int ColumnCount(ModelIndex.Handle parent)
            {
                ModelIndex.Handle__Push(parent);
                NativeImplClient.InvokeInterfaceMethod(_methodDelegate_columnCount, Id);
                return NativeImplClient.PopInt32();
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
            _module = NativeImplClient.GetModule("AbstractListModel");
            // assign module handles
            _createSubclassed = NativeImplClient.GetModuleMethod(_module, "createSubclassed");
            _handle_setSignalMask = NativeImplClient.GetModuleMethod(_module, "Handle_setSignalMask");
            _interior_emitDataChanged = NativeImplClient.GetModuleMethod(_module, "Interior_emitDataChanged");
            _interior_emitHeaderDataChanged = NativeImplClient.GetModuleMethod(_module, "Interior_emitHeaderDataChanged");
            _interior_beginInsertRows = NativeImplClient.GetModuleMethod(_module, "Interior_beginInsertRows");
            _interior_endInsertRows = NativeImplClient.GetModuleMethod(_module, "Interior_endInsertRows");
            _interior_beginRemoveRows = NativeImplClient.GetModuleMethod(_module, "Interior_beginRemoveRows");
            _interior_endRemoveRows = NativeImplClient.GetModuleMethod(_module, "Interior_endRemoveRows");
            _interior_beginResetModel = NativeImplClient.GetModuleMethod(_module, "Interior_beginResetModel");
            _interior_endResetModel = NativeImplClient.GetModuleMethod(_module, "Interior_endResetModel");
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
            _methodDelegate = NativeImplClient.GetInterface(_module, "MethodDelegate");
            _methodDelegate_rowCount = NativeImplClient.GetInterfaceMethod(_methodDelegate, "rowCount");
            _methodDelegate_data = NativeImplClient.GetInterfaceMethod(_methodDelegate, "data");
            _methodDelegate_headerData = NativeImplClient.GetInterfaceMethod(_methodDelegate, "headerData");
            _methodDelegate_getFlags = NativeImplClient.GetInterfaceMethod(_methodDelegate, "getFlags");
            _methodDelegate_setData = NativeImplClient.GetInterfaceMethod(_methodDelegate, "setData");
            _methodDelegate_columnCount = NativeImplClient.GetInterfaceMethod(_methodDelegate, "columnCount");
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_rowCount, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                NativeImplClient.PushInt32(inst.RowCount(parent));
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_data, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                var role = ItemDataRole__Pop();
                Variant.Deferred__Push(inst.Data(index, role), true);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_headerData, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var section = NativeImplClient.PopInt32();
                var orientation = Orientation__Pop();
                var role = ItemDataRole__Pop();
                Variant.Deferred__Push(inst.HeaderData(section, orientation, role), true);
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_getFlags, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                var baseFlags = ItemFlags__Pop();
                ItemFlags__Push(inst.GetFlags(index, baseFlags));
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_setData, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var index = ModelIndex.Handle__Pop();
                var value = Variant.Handle__Pop();
                var role = ItemDataRole__Pop();
                NativeImplClient.PushBool(inst.SetData(index, value, role));
            });
            NativeImplClient.SetClientMethodWrapper(_methodDelegate_columnCount, delegate(ClientObject __obj)
            {
                var inst = ((__MethodDelegateWrapper)__obj).RawInterface;
                var parent = ModelIndex.Handle__Pop();
                NativeImplClient.PushInt32(inst.ColumnCount(parent));
            });

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
