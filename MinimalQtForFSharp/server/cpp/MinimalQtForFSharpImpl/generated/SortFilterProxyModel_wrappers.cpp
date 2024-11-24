#include "../support/NativeImplServer.h"
#include "SortFilterProxyModel_wrappers.h"
#include "SortFilterProxyModel.h"

#include "Object_wrappers.h"
using namespace ::Object;

#include "Enums_wrappers.h"
using namespace ::Enums;

#include "AbstractItemModel_wrappers.h"
using namespace ::AbstractItemModel;

#include "AbstractProxyModel_wrappers.h"
using namespace ::AbstractProxyModel;

#include "RegularExpression_wrappers.h"
using namespace ::RegularExpression;

#include "ModelIndex_wrappers.h"
using namespace ::ModelIndex;

#include "PersistentModelIndex_wrappers.h"
using namespace ::PersistentModelIndex;

namespace SortFilterProxyModel
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
    ni_InterfaceMethodRef signalHandler_sourceModelChanged;
    ni_InterfaceMethodRef signalHandler_autoAcceptChildRowsChanged;
    ni_InterfaceMethodRef signalHandler_filterCaseSensitivityChanged;
    ni_InterfaceMethodRef signalHandler_filterRoleChanged;
    ni_InterfaceMethodRef signalHandler_recursiveFilteringEnabledChanged;
    ni_InterfaceMethodRef signalHandler_sortCaseSensitivityChanged;
    ni_InterfaceMethodRef signalHandler_sortLocaleAwareChanged;
    ni_InterfaceMethodRef signalHandler_sortRoleChanged;
    ni_InterfaceMethodRef methodDelegate_filterAcceptsColumn;
    ni_InterfaceMethodRef methodDelegate_filterAcceptsRow;
    ni_InterfaceMethodRef methodDelegate_lessThan;
    void SignalMask__push(SignalMask value) {
        ni_pushInt32(value);
    }

    SignalMask SignalMask__pop() {
        return ni_popInt32();
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
        void sourceModelChanged() override {
            invokeMethod(signalHandler_sourceModelChanged);
        }
        void autoAcceptChildRowsChanged(bool autoAcceptChildRows) override {
            ni_pushBool(autoAcceptChildRows);
            invokeMethod(signalHandler_autoAcceptChildRowsChanged);
        }
        void filterCaseSensitivityChanged(CaseSensitivity filterCaseSensitivity) override {
            CaseSensitivity__push(filterCaseSensitivity);
            invokeMethod(signalHandler_filterCaseSensitivityChanged);
        }
        void filterRoleChanged(ItemDataRole filterRole) override {
            ItemDataRole__push(filterRole);
            invokeMethod(signalHandler_filterRoleChanged);
        }
        void recursiveFilteringEnabledChanged(bool recursiveFilteringEnabled) override {
            ni_pushBool(recursiveFilteringEnabled);
            invokeMethod(signalHandler_recursiveFilteringEnabledChanged);
        }
        void sortCaseSensitivityChanged(CaseSensitivity sortCaseSensitivity) override {
            CaseSensitivity__push(sortCaseSensitivity);
            invokeMethod(signalHandler_sortCaseSensitivityChanged);
        }
        void sortLocaleAwareChanged(bool sortLocaleAware) override {
            ni_pushBool(sortLocaleAware);
            invokeMethod(signalHandler_sortLocaleAwareChanged);
        }
        void sortRoleChanged(ItemDataRole sortRole) override {
            ItemDataRole__push(sortRole);
            invokeMethod(signalHandler_sortRoleChanged);
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

    void SignalHandler_sourceModelChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        inst->sourceModelChanged();
    }

    void SignalHandler_autoAcceptChildRowsChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto autoAcceptChildRows = ni_popBool();
        inst->autoAcceptChildRowsChanged(autoAcceptChildRows);
    }

    void SignalHandler_filterCaseSensitivityChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto filterCaseSensitivity = CaseSensitivity__pop();
        inst->filterCaseSensitivityChanged(filterCaseSensitivity);
    }

    void SignalHandler_filterRoleChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto filterRole = ItemDataRole__pop();
        inst->filterRoleChanged(filterRole);
    }

    void SignalHandler_recursiveFilteringEnabledChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto recursiveFilteringEnabled = ni_popBool();
        inst->recursiveFilteringEnabledChanged(recursiveFilteringEnabled);
    }

    void SignalHandler_sortCaseSensitivityChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sortCaseSensitivity = CaseSensitivity__pop();
        inst->sortCaseSensitivityChanged(sortCaseSensitivity);
    }

    void SignalHandler_sortLocaleAwareChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sortLocaleAware = ni_popBool();
        inst->sortLocaleAwareChanged(sortLocaleAware);
    }

    void SignalHandler_sortRoleChanged__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerSignalHandlerWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sortRole = ItemDataRole__pop();
        inst->sortRoleChanged(sortRole);
    }
    void Handle__push(HandleRef value) {
        ni_pushPtr(value);
    }

    HandleRef Handle__pop() {
        return (HandleRef)ni_popPtr();
    }

    void Handle_setAutoAcceptChildRows__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setAutoAcceptChildRows(_this, state);
    }

    void Handle_setDynamicSortFilter__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setDynamicSortFilter(_this, state);
    }

    void Handle_setFilterCaseSensitivity__wrapper() {
        auto _this = Handle__pop();
        auto sensitivity = CaseSensitivity__pop();
        Handle_setFilterCaseSensitivity(_this, sensitivity);
    }

    void Handle_setFilterKeyColumn__wrapper() {
        auto _this = Handle__pop();
        auto filterKeyColumn = ni_popInt32();
        Handle_setFilterKeyColumn(_this, filterKeyColumn);
    }

    void Handle_setFilterRegularExpression__wrapper() {
        auto _this = Handle__pop();
        auto regex = Deferred__pop();
        Handle_setFilterRegularExpression(_this, regex);
    }

    void Handle_setFilterRole__wrapper() {
        auto _this = Handle__pop();
        auto filterRole = ItemDataRole__pop();
        Handle_setFilterRole(_this, filterRole);
    }

    void Handle_setSortLocaleAware__wrapper() {
        auto _this = Handle__pop();
        auto state = ni_popBool();
        Handle_setSortLocaleAware(_this, state);
    }

    void Handle_setRecursiveFilteringEnabled__wrapper() {
        auto _this = Handle__pop();
        auto enabled = ni_popBool();
        Handle_setRecursiveFilteringEnabled(_this, enabled);
    }

    void Handle_setSortCaseSensitivity__wrapper() {
        auto _this = Handle__pop();
        auto sensitivity = CaseSensitivity__pop();
        Handle_setSortCaseSensitivity(_this, sensitivity);
    }

    void Handle_setSortRole__wrapper() {
        auto _this = Handle__pop();
        auto sortRole = ItemDataRole__pop();
        Handle_setSortRole(_this, sortRole);
    }

    void Handle_setSignalMask__wrapper() {
        auto _this = Handle__pop();
        auto mask = SignalMask__pop();
        Handle_setSignalMask(_this, mask);
    }

    void Handle_dispose__wrapper() {
        auto _this = Handle__pop();
        Handle_dispose(_this);
    }

    void create__wrapper() {
        auto handler = SignalHandler__pop();
        Handle__push(create(handler));
    }
    void Interior__push(InteriorRef value) {
        ni_pushPtr(value);
    }

    InteriorRef Interior__pop() {
        return (InteriorRef)ni_popPtr();
    }

    void Interior_invalidateColumnsFilter__wrapper() {
        auto _this = Interior__pop();
        Interior_invalidateColumnsFilter(_this);
    }

    void Interior_invalidateRowsFilter__wrapper() {
        auto _this = Interior__pop();
        Interior_invalidateRowsFilter(_this);
    }

    void Interior_invalidateFilter__wrapper() {
        auto _this = Interior__pop();
        Interior_invalidateFilter(_this);
    }

    void Interior_dispose__wrapper() {
        auto _this = Interior__pop();
        Interior_dispose(_this);
    }
    void MethodMask__push(MethodMask value) {
        ni_pushInt32(value);
    }

    MethodMask MethodMask__pop() {
        return ni_popInt32();
    }
    static std::map<MethodDelegate*, std::weak_ptr<Pushable>> __methodDelegateToPushable;

    class ServerMethodDelegateWrapper : public ServerObject {
    public:
        std::shared_ptr<MethodDelegate> rawInterface;
    private:
        ServerMethodDelegateWrapper(std::shared_ptr<MethodDelegate> raw) {
            this->rawInterface = raw;
        }
        void releaseExtra() override {
            __methodDelegateToPushable.erase(rawInterface.get());
        }
    public:
        static std::shared_ptr<ServerMethodDelegateWrapper> wrapAndRegister(std::shared_ptr<MethodDelegate> raw) {
            auto ret = std::shared_ptr<ServerMethodDelegateWrapper>(new ServerMethodDelegateWrapper(raw));
            __methodDelegateToPushable[raw.get()] = ret;
            return ret;
        }
    };
    class ClientMethodDelegate : public ClientObject, public MethodDelegate {
    public:
        ClientMethodDelegate(int id) : ClientObject(id) {}
        ~ClientMethodDelegate() override {
            __methodDelegateToPushable.erase(this);
        }
        bool filterAcceptsColumn(int32_t sourceColumn, ModelIndex::HandleRef sourceParent) override {
            ModelIndex::Handle__push(sourceParent);
            ni_pushInt32(sourceColumn);
            invokeMethod(methodDelegate_filterAcceptsColumn);
            return ni_popBool();
        }
        bool filterAcceptsRow(int32_t sourceRow, ModelIndex::HandleRef sourceParent) override {
            ModelIndex::Handle__push(sourceParent);
            ni_pushInt32(sourceRow);
            invokeMethod(methodDelegate_filterAcceptsRow);
            return ni_popBool();
        }
        bool lessThan(ModelIndex::HandleRef sourceLeft, ModelIndex::HandleRef sourceRight) override {
            ModelIndex::Handle__push(sourceRight);
            ModelIndex::Handle__push(sourceLeft);
            invokeMethod(methodDelegate_lessThan);
            return ni_popBool();
        }
    };

    void MethodDelegate__push(std::shared_ptr<MethodDelegate> inst, bool isReturn) {
        if (inst != nullptr) {
            auto found = __methodDelegateToPushable.find(inst.get());
            if (found != __methodDelegateToPushable.end()) {
                auto pushable = found->second.lock();
                pushable->push(pushable, isReturn);
            }
            else {
                auto pushable = ServerMethodDelegateWrapper::wrapAndRegister(inst);
                pushable->push(pushable, isReturn);
            }
        }
        else {
            ni_pushNull();
        }
    }

    std::shared_ptr<MethodDelegate> MethodDelegate__pop() {
        bool isClientID;
        auto id = ni_popInstance(&isClientID);
        if (id != 0) {
            if (isClientID) {
                auto ret = std::shared_ptr<MethodDelegate>(new ClientMethodDelegate(id));
                __methodDelegateToPushable[ret.get()] = std::dynamic_pointer_cast<Pushable>(ret);
                return ret;
            }
            else {
                auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(id));
                return wrapper->rawInterface;
            }
        }
        else {
            return std::shared_ptr<MethodDelegate>();
        }
    }

    void MethodDelegate_filterAcceptsColumn__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceColumn = ni_popInt32();
        auto sourceParent = ModelIndex::Handle__pop();
        ni_pushBool(inst->filterAcceptsColumn(sourceColumn, sourceParent));
    }

    void MethodDelegate_filterAcceptsRow__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceRow = ni_popInt32();
        auto sourceParent = ModelIndex::Handle__pop();
        ni_pushBool(inst->filterAcceptsRow(sourceRow, sourceParent));
    }

    void MethodDelegate_lessThan__wrapper(int serverID) {
        auto wrapper = std::static_pointer_cast<ServerMethodDelegateWrapper>(ServerObject::getByID(serverID));
        auto inst = wrapper->rawInterface;
        auto sourceLeft = ModelIndex::Handle__pop();
        auto sourceRight = ModelIndex::Handle__pop();
        ni_pushBool(inst->lessThan(sourceLeft, sourceRight));
    }

    void createSubclassed__wrapper() {
        auto handler = SignalHandler__pop();
        auto methodDelegate = MethodDelegate__pop();
        auto methodMask = MethodMask__pop();
        Interior__push(createSubclassed(handler, methodDelegate, methodMask));
    }

    int __register() {
        auto m = ni_registerModule("SortFilterProxyModel");
        ni_registerModuleMethod(m, "create", &create__wrapper);
        ni_registerModuleMethod(m, "createSubclassed", &createSubclassed__wrapper);
        ni_registerModuleMethod(m, "Handle_setAutoAcceptChildRows", &Handle_setAutoAcceptChildRows__wrapper);
        ni_registerModuleMethod(m, "Handle_setDynamicSortFilter", &Handle_setDynamicSortFilter__wrapper);
        ni_registerModuleMethod(m, "Handle_setFilterCaseSensitivity", &Handle_setFilterCaseSensitivity__wrapper);
        ni_registerModuleMethod(m, "Handle_setFilterKeyColumn", &Handle_setFilterKeyColumn__wrapper);
        ni_registerModuleMethod(m, "Handle_setFilterRegularExpression", &Handle_setFilterRegularExpression__wrapper);
        ni_registerModuleMethod(m, "Handle_setFilterRole", &Handle_setFilterRole__wrapper);
        ni_registerModuleMethod(m, "Handle_setSortLocaleAware", &Handle_setSortLocaleAware__wrapper);
        ni_registerModuleMethod(m, "Handle_setRecursiveFilteringEnabled", &Handle_setRecursiveFilteringEnabled__wrapper);
        ni_registerModuleMethod(m, "Handle_setSortCaseSensitivity", &Handle_setSortCaseSensitivity__wrapper);
        ni_registerModuleMethod(m, "Handle_setSortRole", &Handle_setSortRole__wrapper);
        ni_registerModuleMethod(m, "Handle_setSignalMask", &Handle_setSignalMask__wrapper);
        ni_registerModuleMethod(m, "Handle_dispose", &Handle_dispose__wrapper);
        ni_registerModuleMethod(m, "Interior_invalidateColumnsFilter", &Interior_invalidateColumnsFilter__wrapper);
        ni_registerModuleMethod(m, "Interior_invalidateRowsFilter", &Interior_invalidateRowsFilter__wrapper);
        ni_registerModuleMethod(m, "Interior_invalidateFilter", &Interior_invalidateFilter__wrapper);
        ni_registerModuleMethod(m, "Interior_dispose", &Interior_dispose__wrapper);
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
        signalHandler_sourceModelChanged = ni_registerInterfaceMethod(signalHandler, "sourceModelChanged", &SignalHandler_sourceModelChanged__wrapper);
        signalHandler_autoAcceptChildRowsChanged = ni_registerInterfaceMethod(signalHandler, "autoAcceptChildRowsChanged", &SignalHandler_autoAcceptChildRowsChanged__wrapper);
        signalHandler_filterCaseSensitivityChanged = ni_registerInterfaceMethod(signalHandler, "filterCaseSensitivityChanged", &SignalHandler_filterCaseSensitivityChanged__wrapper);
        signalHandler_filterRoleChanged = ni_registerInterfaceMethod(signalHandler, "filterRoleChanged", &SignalHandler_filterRoleChanged__wrapper);
        signalHandler_recursiveFilteringEnabledChanged = ni_registerInterfaceMethod(signalHandler, "recursiveFilteringEnabledChanged", &SignalHandler_recursiveFilteringEnabledChanged__wrapper);
        signalHandler_sortCaseSensitivityChanged = ni_registerInterfaceMethod(signalHandler, "sortCaseSensitivityChanged", &SignalHandler_sortCaseSensitivityChanged__wrapper);
        signalHandler_sortLocaleAwareChanged = ni_registerInterfaceMethod(signalHandler, "sortLocaleAwareChanged", &SignalHandler_sortLocaleAwareChanged__wrapper);
        signalHandler_sortRoleChanged = ni_registerInterfaceMethod(signalHandler, "sortRoleChanged", &SignalHandler_sortRoleChanged__wrapper);
        auto methodDelegate = ni_registerInterface(m, "MethodDelegate");
        methodDelegate_filterAcceptsColumn = ni_registerInterfaceMethod(methodDelegate, "filterAcceptsColumn", &MethodDelegate_filterAcceptsColumn__wrapper);
        methodDelegate_filterAcceptsRow = ni_registerInterfaceMethod(methodDelegate, "filterAcceptsRow", &MethodDelegate_filterAcceptsRow__wrapper);
        methodDelegate_lessThan = ni_registerInterfaceMethod(methodDelegate, "lessThan", &MethodDelegate_lessThan__wrapper);
        return 0; // = OK
    }
}
