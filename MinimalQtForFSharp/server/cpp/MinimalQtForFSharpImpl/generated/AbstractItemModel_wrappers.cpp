#include "../support/NativeImplServer.h"
#include "AbstractItemModel_wrappers.h"
#include "AbstractItemModel.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "ModelIndex_wrappers.h"
using namespace ::ModelIndex;

#include "PersistentModelIndex_wrappers.h"
using namespace ::PersistentModelIndex;

#include "Variant_wrappers.h"
using namespace ::Variant;

namespace AbstractItemModel
{
    void __PersistentModelIndex_Handle_Array__push(std::vector<PersistentModelIndex::HandleRef> items, bool isReturn) {
        ni_pushPtrArray((void**)items.data(), items.size());
    }

    std::vector<PersistentModelIndex::HandleRef> __PersistentModelIndex_Handle_Array__pop() {
        PersistentModelIndex::HandleRef *values;
        size_t count;
        ni_popPtrArray((void***)&values, &count);
        return std::vector<PersistentModelIndex::HandleRef>(values, values + count);
    }
    void __ItemDataRole_Array__push(std::vector<ItemDataRole> values, bool isReturn) {
        std::vector<int8_t> intValues;
        for (auto i = values.begin(); i != values.end(); i++) {
            intValues.push_back((int8_t)*i);
        }
        pushInt8ArrayInternal(intValues);
    }

    std::vector<ItemDataRole> __ItemDataRole_Array__pop() {
        auto intValues = popInt8ArrayInternal();
        std::vector<ItemDataRole> __ret;
        for (auto i = intValues.begin(); i != intValues.end(); i++) {
            __ret.push_back((ItemDataRole)*i);
        }
        return __ret;
    }
    ni_InterfaceMethodRef signalHandler_destroyed;
    ni_InterfaceMethodRef signalHandler_objectNameChanged;
    ni_InterfaceMethodRef signalHandler_columnsAboutToBeInserted;
    ni_InterfaceMethodRef signalHandler_columnsAboutToBeMoved;
    ni_InterfaceMethodRef signalHandler_columnsAboutToBeRemoved;
    ni_InterfaceMethodRef signalHandler_columnsInserted;
    ni_InterfaceMethodRef signalHandler_columnsMoved;
    ni_InterfaceMethodRef signalHandler_columnsRemoved;
    ni_InterfaceMethodRef signalHandler_dataChanged;
    ni_InterfaceMethodRef signalHandler_headerDataChanged;
    ni_InterfaceMethodRef signalHandler_layoutAboutToBeChanged;
    ni_InterfaceMethodRef signalHandler_layoutChanged;
    ni_InterfaceMethodRef signalHandler_modelAboutToBeReset;
    ni_InterfaceMethodRef signalHandler_modelReset;
    ni_InterfaceMethodRef signalHandler_rowsAboutToBeInserted;
    ni_InterfaceMethodRef signalHandler_rowsAboutToBeMoved;
    ni_InterfaceMethodRef signalHandler_rowsAboutToBeRemoved;
    ni_InterfaceMethodRef signalHandler_rowsInserted;
    ni_InterfaceMethodRef signalHandler_rowsMoved;
    ni_InterfaceMethodRef signalHandler_rowsRemoved;
    void SignalMask__push(SignalMask value) {
        ni_pushInt32(value);
    }

    SignalMask SignalMask__pop() {
        return ni_popInt32();
    }
    void LayoutChangeHint__push(LayoutChangeHint value) {
        ni_pushInt32((int32_t)value);
    }

    LayoutChangeHint LayoutChangeHint__pop() {
        auto tag = ni_popInt32();
        return (LayoutChangeHint)tag;
    }
    static std::map<SignalHandler*, std::weak_ptr<Pushable>> __signalHandlerToPushable;

    class ServerSignalHandlerWrapper : public ServerObject {
    public:
        std::shared_ptr<SignalHandler> rawInterface;
    private:
        ServerSignalHandlerWrapper(std::shared_ptr<SignalHandler> raw) {
            this->rawInterface = raw;
        }
        void releaseExtra() override {
            __signalHandlerToPushable.erase(rawInterface.get());
        }
    public:
        static std::shared_ptr<ServerSignalHandlerWrapper> wrapAndRegister(std::shared_ptr<SignalHandler> raw) {
            auto ret = std::shared_ptr<ServerSignalHandlerWrapper>(new ServerSignalHandlerWrapper(raw));
            __signalHandlerToPushable[raw.get()] = ret;
            return ret;
        }
    };
    class ClientSignalHandler : public ClientObject, public SignalHandler {
    public:
        ClientSignalHandler(int id) : ClientObject(id) {}
        ~ClientSignalHandler() override {
            __signalHandlerToPushable.erase(this);
        }
        void destroyed(Object::HandleRef obj) override {
            Object::Handle__push(obj);
            invokeMethod(signalHandler_destroyed);
        }
        void objectNameChanged(std::string objectName) override {
            pushStringInternal(objectName);
            invokeMethod(signalHandler_objectNameChanged);
        }
        void columnsAboutToBeInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_columnsAboutToBeInserted);
        }
        void columnsAboutToBeMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationColumn) override {
            ni_pushInt32(destinationColumn);
            ModelIndex::Handle__push(destinationParent);
            ni_pushInt32(sourceEnd);
            ni_pushInt32(sourceStart);
            ModelIndex::Handle__push(sourceParent);
            invokeMethod(signalHandler_columnsAboutToBeMoved);
        }
        void columnsAboutToBeRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_columnsAboutToBeRemoved);
        }
        void columnsInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_columnsInserted);
        }
        void columnsMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationColumn) override {
            ni_pushInt32(destinationColumn);
            ModelIndex::Handle__push(destinationParent);
            ni_pushInt32(sourceEnd);
            ni_pushInt32(sourceStart);
            ModelIndex::Handle__push(sourceParent);
            invokeMethod(signalHandler_columnsMoved);
        }
        void columnsRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_columnsRemoved);
        }
        void dataChanged(ModelIndex::HandleRef topLeft, ModelIndex::HandleRef bottomRight, std::vector<ItemDataRole> roles) override {
            __ItemDataRole_Array__push(roles, false);
            ModelIndex::Handle__push(bottomRight);
            ModelIndex::Handle__push(topLeft);
            invokeMethod(signalHandler_dataChanged);
        }
        void headerDataChanged(Orientation orientation, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            Orientation__push(orientation);
            invokeMethod(signalHandler_headerDataChanged);
        }
        void layoutAboutToBeChanged(std::vector<PersistentModelIndex::HandleRef> parents, LayoutChangeHint hint) override {
            LayoutChangeHint__push(hint);
            __PersistentModelIndex_Handle_Array__push(parents, false);
            invokeMethod(signalHandler_layoutAboutToBeChanged);
        }
        void layoutChanged(std::vector<PersistentModelIndex::HandleRef> parents, LayoutChangeHint hint) override {
            LayoutChangeHint__push(hint);
            __PersistentModelIndex_Handle_Array__push(parents, false);
            invokeMethod(signalHandler_layoutChanged);
        }
        void modelAboutToBeReset() override {
            invokeMethod(signalHandler_modelAboutToBeReset);
        }
        void modelReset() override {
            invokeMethod(signalHandler_modelReset);
        }
        void rowsAboutToBeInserted(ModelIndex::HandleRef parent, int32_t start, int32_t end) override {
            ni_pushInt32(end);
            ni_pushInt32(start);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_rowsAboutToBeInserted);
        }
        void rowsAboutToBeMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationRow) override {
            ni_pushInt32(destinationRow);
            ModelIndex::Handle__push(destinationParent);
            ni_pushInt32(sourceEnd);
            ni_pushInt32(sourceStart);
            ModelIndex::Handle__push(sourceParent);
            invokeMethod(signalHandler_rowsAboutToBeMoved);
        }
        void rowsAboutToBeRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_rowsAboutToBeRemoved);
        }
        void rowsInserted(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_rowsInserted);
        }
        void rowsMoved(ModelIndex::HandleRef sourceParent, int32_t sourceStart, int32_t sourceEnd, ModelIndex::HandleRef destinationParent, int32_t destinationRow) override {
            ni_pushInt32(destinationRow);
            ModelIndex::Handle__push(destinationParent);
            ni_pushInt32(sourceEnd);
            ni_pushInt32(sourceStart);
            ModelIndex::Handle__push(sourceParent);
            invokeMethod(signalHandler_rowsMoved);
        }
        void rowsRemoved(ModelIndex::HandleRef parent, int32_t first, int32_t last) override {
            ni_pushInt32(last);
            ni_pushInt32(first);
            ModelIndex::Handle__push(parent);
            invokeMethod(signalHandler_rowsRemoved);
        }
    };

    void SignalHandler__push(std::shared_ptr<SignalHandler> inst, bool isReturn) {
        if (inst != nullptr) {
            auto found = __signalHandlerToPushable.find(inst.get());
            if (found != __signalHandlerToPushable.end()) {
                auto pushable = found->second.lock();
                pushable->push(pushable, isReturn);
            }
            else {
                auto pushable = ServerSignalHandlerWrapper::wrapAndRegister(inst);
                pushable->push(pushable, isReturn);
            }
        }
        else {
            ni_pushNull();
        }
    }

    std::shared_ptr<SignalHandler> SignalHandler__pop() {
        bool isClientID;
        auto id = ni_popInstance(&isClientID);
        if (id != 0) {
            if (isClientID) {
                auto ret = std::shared_ptr<SignalHandler>(new ClientSignalHandler(id));
                __signalHandlerToPushable[ret.get()] = std::dynamic_pointer_cast<Pushable>(ret);
                return ret;
            }
            else {
                auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(id));
                return wrapper->rawInterface;
            }
        }
        else {
            return std::shared_ptr<SignalHandler>();
        }
    }

    void SignalHandler_destroyed__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto obj = Object::Handle__pop();
        inst->destroyed(obj);
    }

    void SignalHandler_objectNameChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto objectName = popStringInternal();
        inst->objectNameChanged(objectName);
    }

    void SignalHandler_columnsAboutToBeInserted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->columnsAboutToBeInserted(parent, first, last);
    }

    void SignalHandler_columnsAboutToBeMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceParent = ModelIndex::Handle__pop();
        auto sourceStart = ni_popInt32();
        auto sourceEnd = ni_popInt32();
        auto destinationParent = ModelIndex::Handle__pop();
        auto destinationColumn = ni_popInt32();
        inst->columnsAboutToBeMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn);
    }

    void SignalHandler_columnsAboutToBeRemoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->columnsAboutToBeRemoved(parent, first, last);
    }

    void SignalHandler_columnsInserted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->columnsInserted(parent, first, last);
    }

    void SignalHandler_columnsMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceParent = ModelIndex::Handle__pop();
        auto sourceStart = ni_popInt32();
        auto sourceEnd = ni_popInt32();
        auto destinationParent = ModelIndex::Handle__pop();
        auto destinationColumn = ni_popInt32();
        inst->columnsMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationColumn);
    }

    void SignalHandler_columnsRemoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->columnsRemoved(parent, first, last);
    }

    void SignalHandler_dataChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto topLeft = ModelIndex::Handle__pop();
        auto bottomRight = ModelIndex::Handle__pop();
        auto roles = __ItemDataRole_Array__pop();
        inst->dataChanged(topLeft, bottomRight, roles);
    }

    void SignalHandler_headerDataChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto orientation = Orientation__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->headerDataChanged(orientation, first, last);
    }

    void SignalHandler_layoutAboutToBeChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parents = __PersistentModelIndex_Handle_Array__pop();
        auto hint = LayoutChangeHint__pop();
        inst->layoutAboutToBeChanged(parents, hint);
    }

    void SignalHandler_layoutChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parents = __PersistentModelIndex_Handle_Array__pop();
        auto hint = LayoutChangeHint__pop();
        inst->layoutChanged(parents, hint);
    }

    void SignalHandler_modelAboutToBeReset__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->modelAboutToBeReset();
    }

    void SignalHandler_modelReset__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->modelReset();
    }

    void SignalHandler_rowsAboutToBeInserted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto start = ni_popInt32();
        auto end = ni_popInt32();
        inst->rowsAboutToBeInserted(parent, start, end);
    }

    void SignalHandler_rowsAboutToBeMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceParent = ModelIndex::Handle__pop();
        auto sourceStart = ni_popInt32();
        auto sourceEnd = ni_popInt32();
        auto destinationParent = ModelIndex::Handle__pop();
        auto destinationRow = ni_popInt32();
        inst->rowsAboutToBeMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow);
    }

    void SignalHandler_rowsAboutToBeRemoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->rowsAboutToBeRemoved(parent, first, last);
    }

    void SignalHandler_rowsInserted__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->rowsInserted(parent, first, last);
    }

    void SignalHandler_rowsMoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceParent = ModelIndex::Handle__pop();
        auto sourceStart = ni_popInt32();
        auto sourceEnd = ni_popInt32();
        auto destinationParent = ModelIndex::Handle__pop();
        auto destinationRow = ni_popInt32();
        inst->rowsMoved(sourceParent, sourceStart, sourceEnd, destinationParent, destinationRow);
    }

    void SignalHandler_rowsRemoved__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto parent = ModelIndex::Handle__pop();
        auto first = ni_popInt32();
        auto last = ni_popInt32();
        inst->rowsRemoved(parent, first, last);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_index__wrapper() {
        auto _this = Handle__pop();
        auto row = ni_popInt32();
        auto column = ni_popInt32();
        Owned__push(Handle_index(_this, row, column));
    }

    void Handle_index_overload1__wrapper() {
        auto _this = Handle__pop();
        auto row = ni_popInt32();
        auto column = ni_popInt32();
        auto parent = ModelIndex::Deferred__pop();
        Owned__push(Handle_index(_this, row, column, parent));
    }

    void Handle_setData__wrapper() {
        auto _this = Handle__pop();
        auto index = ModelIndex::Deferred__pop();
        auto value = Variant::Deferred__pop();
        ni_pushBool(Handle_setData(_this, index, value));
    }

    void Handle_setData_overload1__wrapper() {
        auto _this = Handle__pop();
        auto index = ModelIndex::Deferred__pop();
        auto value = Variant::Deferred__pop();
        auto role = ItemDataRole__pop();
        ni_pushBool(Handle_setData(_this, index, value, role));
    }

    void Handle_data__wrapper() {
        auto _this = Handle__pop();
        auto index = ModelIndex::Deferred__pop();
        OwnedHandle__push(Handle_data(_this, index));
    }

    void Handle_data_overload1__wrapper() {
        auto _this = Handle__pop();
        auto index = ModelIndex::Deferred__pop();
        auto role = ItemDataRole__pop();
        OwnedHandle__push(Handle_data(_this, index, role));
    }

    void Handle_sort__wrapper() {
        auto _this = Handle__pop();
        auto column = ni_popInt32();
        Handle_sort(_this, column);
    }

    void Handle_sort_overload1__wrapper() {
        auto _this = Handle__pop();
        auto column = ni_popInt32();
        auto order = SortOrder__pop();
        Handle_sort(_this, column, order);
    }

    int __register() {
        auto m = ni_registerModule("AbstractItemModel");
        ni_registerModuleMethod(m, "Handle_index", &Handle_index__wrapper);
        ni_registerModuleMethod(m, "Handle_index_overload1", &Handle_index_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_setData", &Handle_setData__wrapper);
        ni_registerModuleMethod(m, "Handle_setData_overload1", &Handle_setData_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_data", &Handle_data__wrapper);
        ni_registerModuleMethod(m, "Handle_data_overload1", &Handle_data_overload1__wrapper);
        ni_registerModuleMethod(m, "Handle_sort", &Handle_sort__wrapper);
        ni_registerModuleMethod(m, "Handle_sort_overload1", &Handle_sort_overload1__wrapper);
        auto signalHandler = ni_registerInterface(m, "SignalHandler");
        signalHandler_destroyed = ni_registerInterfaceMethod(signalHandler, "destroyed", &SignalHandler_destroyed__wrapper);
        signalHandler_objectNameChanged = ni_registerInterfaceMethod(signalHandler, "objectNameChanged", &SignalHandler_objectNameChanged__wrapper);
        signalHandler_columnsAboutToBeInserted = ni_registerInterfaceMethod(signalHandler, "columnsAboutToBeInserted", &SignalHandler_columnsAboutToBeInserted__wrapper);
        signalHandler_columnsAboutToBeMoved = ni_registerInterfaceMethod(signalHandler, "columnsAboutToBeMoved", &SignalHandler_columnsAboutToBeMoved__wrapper);
        signalHandler_columnsAboutToBeRemoved = ni_registerInterfaceMethod(signalHandler, "columnsAboutToBeRemoved", &SignalHandler_columnsAboutToBeRemoved__wrapper);
        signalHandler_columnsInserted = ni_registerInterfaceMethod(signalHandler, "columnsInserted", &SignalHandler_columnsInserted__wrapper);
        signalHandler_columnsMoved = ni_registerInterfaceMethod(signalHandler, "columnsMoved", &SignalHandler_columnsMoved__wrapper);
        signalHandler_columnsRemoved = ni_registerInterfaceMethod(signalHandler, "columnsRemoved", &SignalHandler_columnsRemoved__wrapper);
        signalHandler_dataChanged = ni_registerInterfaceMethod(signalHandler, "dataChanged", &SignalHandler_dataChanged__wrapper);
        signalHandler_headerDataChanged = ni_registerInterfaceMethod(signalHandler, "headerDataChanged", &SignalHandler_headerDataChanged__wrapper);
        signalHandler_layoutAboutToBeChanged = ni_registerInterfaceMethod(signalHandler, "layoutAboutToBeChanged", &SignalHandler_layoutAboutToBeChanged__wrapper);
        signalHandler_layoutChanged = ni_registerInterfaceMethod(signalHandler, "layoutChanged", &SignalHandler_layoutChanged__wrapper);
        signalHandler_modelAboutToBeReset = ni_registerInterfaceMethod(signalHandler, "modelAboutToBeReset", &SignalHandler_modelAboutToBeReset__wrapper);
        signalHandler_modelReset = ni_registerInterfaceMethod(signalHandler, "modelReset", &SignalHandler_modelReset__wrapper);
        signalHandler_rowsAboutToBeInserted = ni_registerInterfaceMethod(signalHandler, "rowsAboutToBeInserted", &SignalHandler_rowsAboutToBeInserted__wrapper);
        signalHandler_rowsAboutToBeMoved = ni_registerInterfaceMethod(signalHandler, "rowsAboutToBeMoved", &SignalHandler_rowsAboutToBeMoved__wrapper);
        signalHandler_rowsAboutToBeRemoved = ni_registerInterfaceMethod(signalHandler, "rowsAboutToBeRemoved", &SignalHandler_rowsAboutToBeRemoved__wrapper);
        signalHandler_rowsInserted = ni_registerInterfaceMethod(signalHandler, "rowsInserted", &SignalHandler_rowsInserted__wrapper);
        signalHandler_rowsMoved = ni_registerInterfaceMethod(signalHandler, "rowsMoved", &SignalHandler_rowsMoved__wrapper);
        signalHandler_rowsRemoved = ni_registerInterfaceMethod(signalHandler, "rowsRemoved", &SignalHandler_rowsRemoved__wrapper);
        return 0; // = OK
    }
}
