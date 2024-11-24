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
using static Org.Whatever.MinimalQtForFSharp.ModelIndex;
using static Org.Whatever.MinimalQtForFSharp.PersistentModelIndex;

namespace Org.Whatever.MinimalQtForFSharp
{
    public static class AbstractProxyModel
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
        internal static ModuleMethodHandle _handle_sourceModel;
        internal static ModuleMethodHandle _handle_setSourceModel;
        internal static ModuleMethodHandle _handle_mapToSource;
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
            SourceModelChanged = 1 << 20
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
            public AbstractItemModel.Handle SourceModel()
            {
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_sourceModel);
                return AbstractItemModel.Handle__Pop();
            }
            public void SetSourceModel(AbstractItemModel.Handle sourceModel)
            {
                AbstractItemModel.Handle__Push(sourceModel);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_setSourceModel);
            }
            public Owned MapToSource(ModelIndex.Handle proxyIndex)
            {
                ModelIndex.Handle__Push(proxyIndex);
                Handle__Push(this);
                NativeImplClient.InvokeModuleMethod(_handle_mapToSource);
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

        internal static void __Init()
        {
            _module = NativeImplClient.GetModule("AbstractProxyModel");
            // assign module handles
            _handle_sourceModel = NativeImplClient.GetModuleMethod(_module, "Handle_sourceModel");
            _handle_setSourceModel = NativeImplClient.GetModuleMethod(_module, "Handle_setSourceModel");
            _handle_mapToSource = NativeImplClient.GetModuleMethod(_module, "Handle_mapToSource");
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

            // no static init
        }

        internal static void __Shutdown()
        {
            // no static shutdown
        }
    }
}
