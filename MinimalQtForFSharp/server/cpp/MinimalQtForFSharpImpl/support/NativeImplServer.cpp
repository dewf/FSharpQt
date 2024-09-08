#include "NativeImplServer.h"
#include <functional>
#include <map>
#include <memory>
#include <vector>
#include <assert.h>

static int nextServerObjId = 1;
static std::map<int, std::shared_ptr<ServerResource>> serverResources;
static std::map<size_t, int> reverseServerFuncVals; // to attempt to de-duplicate identical std::functions

void ni_dumpTables() {
	ServerResource::dumpTables();
}

void ni_execServerFunc(int id)
{
	ServerFuncVal::getById(id)->wrapper();
}

ni_ExceptionRef ni_execServerFuncWithExceptions(int id) {
	ServerFuncVal::getById(id)->wrapper();
	return ni_getAndClearException();
}

void pushServerFuncVal(std::function<void()> wrapper, size_t key) {
	auto funcVal = ServerFuncVal::create(wrapper, key);
	funcVal->push(funcVal, false); // false = "not applicable" - server func vals would never be in the safety area, that only applies to client stuff
}

void pushStringInternal(std::string str)
{
	ni_pushString(str.c_str(), str.size());
}

std::string popStringInternal()
{
	const char* ptr;
	size_t length;
	ni_popString(&ptr, &length);
	std::string ret;
	ret.assign(ptr, length);
	return ret;
}

void pushBoolArrayInternal(std::vector<bool> values)
{
	auto unpacked = new bool[values.size()];
	std::copy(values.begin(), values.end(), unpacked);
	ni_pushBoolArray(unpacked, values.size());
	delete[] unpacked;
}

std::vector<bool> popBoolArrayInternal()
{
	bool* values;
	size_t count;
	ni_popBoolArray(&values, &count);
	std::vector<bool> result;
	result.assign(values, values + count);
	return result;
}

// INT8 =====

void pushInt8ArrayInternal(std::vector<int8_t> values)
{
	ni_pushInt8Array(values.data(), values.size());
}

std::vector<int8_t> popInt8ArrayInternal()
{
	std::vector<int8_t> ret;
	int8_t* values;
	size_t count;
	ni_popInt8Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

void pushUInt8ArrayInternal(std::vector<uint8_t> values)
{
	ni_pushUInt8Array(values.data(), values.size());
}

std::vector<uint8_t> popUInt8ArrayInternal() {
	std::vector<uint8_t> ret;
	uint8_t* values;
	size_t count;
	ni_popUInt8Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

// INT16 =====

void pushInt16ArrayInternal(std::vector<int16_t> values)
{
	ni_pushInt16Array(values.data(), values.size());
}

std::vector<int16_t> popInt16ArrayInternal()
{
	std::vector<int16_t> ret;
	int16_t* values;
	size_t count;
	ni_popInt16Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

void pushUInt16ArrayInternal(std::vector<uint16_t> values)
{
	ni_pushUInt16Array(values.data(), values.size());
}

std::vector<uint16_t> popUInt16ArrayInternal() {
	std::vector<uint16_t> ret;
	uint16_t* values;
	size_t count;
	ni_popUInt16Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

// INT32 =====

void pushInt32ArrayInternal(std::vector<int32_t> values)
{
	ni_pushInt32Array(values.data(), values.size());
}

std::vector<int32_t> popInt32ArrayInternal()
{
	std::vector<int32_t> ret;
	int32_t* values;
	size_t count;
	ni_popInt32Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

void pushUInt32ArrayInternal(std::vector<uint32_t> values)
{
	ni_pushUInt32Array(values.data(), values.size());
}

std::vector<uint32_t> popUInt32ArrayInternal() {
	std::vector<uint32_t> ret;
	uint32_t* values;
	size_t count;
	ni_popUInt32Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

// INT64 =====

void pushInt64ArrayInternal(std::vector<int64_t> values)
{
	ni_pushInt64Array(values.data(), values.size());
}

std::vector<int64_t> popInt64ArrayInternal()
{
	std::vector<int64_t> ret;
	int64_t* values;
	size_t count;
	ni_popInt64Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

void pushUInt64ArrayInternal(std::vector<uint64_t> values)
{
	ni_pushUInt64Array(values.data(), values.size());
}

std::vector<uint64_t> popUInt64ArrayInternal() {
	std::vector<uint64_t> ret;
	uint64_t* values;
	size_t count;
	ni_popUInt64Array(&values, &count);
	if (count > 0) {
		ret.assign(values, values + count);
	}
	return ret;
}

// end ints =====

void pushStringArrayInternal(std::vector<std::string> values)
{
	auto count = values.size();
	auto ptrs = new const char*[count];
	auto lengths = new size_t[count];
	for (auto i = 0; i < count; i++) {
		ptrs[i] = values[i].data();
		lengths[i] = values[i].size();
	}
	ni_pushStringArray(ptrs, lengths, count);
	delete[] ptrs;
	delete[] lengths;
}

std::vector<std::string> popStringArrayInternal()
{
	const char** strs;
	size_t* lengths;
	size_t count;
	ni_popStringArray(&strs, &lengths, &count);
	std::vector<std::string> ret;
	for (auto i = 0; i < count; i++) {
		std::string str;
		str.assign(strs[i], strs[i] + lengths[i]);
		ret.push_back(str);
	}
	return ret;
}

void pushSizeTArrayInternal(std::vector<size_t> values)
{
	ni_pushSizeTArray(values.data(), values.size());
}

std::vector<size_t> popSizeTArrayInternal()
{
	std::vector<size_t> ret;
	size_t* values;
	size_t count;
	ni_popSizeTArray(&values, &count);
	ret.assign(values, values + count);
	return ret;
}

void pushFloatArrayInternal(std::vector<float> values)
{
	ni_pushFloatArray(values.data(), values.size());
}

std::vector<float> popFloatArrayInternal()
{
	std::vector<float> ret;
	float* values;
	size_t count;
	ni_popFloatArray(&values, &count);
	ret.assign(values, values + count);
	return ret;
}

void pushDoubleArrayInternal(std::vector<double> values)
{
	ni_pushDoubleArray(values.data(), values.size());
}

std::vector<double> popDoubleArrayInternal()
{
	std::vector<double> ret;
	double* values;
	size_t count;
	ni_popDoubleArray(&values, &count);
	ret.assign(values, values + count);
	return ret;
}

std::shared_ptr<ServerResource> ServerResource::getResourceById(int id) {
	return serverResources[id];
}

void ServerResource::dumpTables()
{
	printf("== C++ server resources:\n");
	for (auto i = serverResources.begin(); i != serverResources.end(); i++) {
		printf(" - id %d (%s): refcount %d\n",  i->first, i->second->kind, i->second->refCount);
	}
}

ServerResource::~ServerResource()
{
	releaseExtra();
}

void ServerResource::release()
{
	refCount -= 1;
	if (refCount == 0) {
		printf("C++: releasing server obj %d\n", id); // only removes from table, still might exist on this side somewhere
		serverResources.erase(id);
	}
	else {
		printf("C++: releaseServerInstID(%d) - refCount still %d\n", id, refCount);
	}
}

void ServerResource::push(std::shared_ptr<Pushable> outer, bool isReturn) {
	// isReturn not used for server stuff, that's just to help out with the safe area / protecting client objects
	if (id == -1) { // unset as of yet
		id = nextServerObjId++;
		refCount = 1;
		serverResources[id] = std::dynamic_pointer_cast<ServerResource>(outer);
		//printf("ServerObject::soPush()ed into serverObjects[%d]\n", id);
	}
	else {
		// add ref to existing
		refCount++; // this assumes that it's going across a boundary which will be balanced by an eventual deref. is that always true?
		printf("bumped server ID %d refcount to %d\n", id, refCount);
	}
	nativePush();
}

std::shared_ptr<ServerObject> ServerObject::getByID(int id)
{
	auto res = ServerResource::getResourceById(id);
	return std::dynamic_pointer_cast<ServerObject>(res);
}

void ServerObject::nativePush()
{
	ni_pushInstance(id, false);
}

void ni_releaseServerResource(int id)
{
	ServerResource::getResourceById(id)->release();
}

// while server objects are kept alive ("checked out") via the serverObjects/refcount,
// any client objects that currently only exist on this side, need to be protected during returns to keep their shared_ptr from going away
// and releasing them prematurely (dropping remote refcount to 0)
// the other side will call clearServerSafetyArea when it's gotten the return value
// each side has its own safety area, to protect the other's resources during function return handoff
static std::vector<std::shared_ptr<ClientResource>> safeArea;

// clear server resources from safety area (protects values being returned with no other shared_ptrs existing on this side)
void ni_clearServerSafetyArea() {
	printf("C++ before safety clear =====\n");
	safeArea.clear();
	printf("C++ after safety clear!! ====\n");
}

void ClientResource::push(std::shared_ptr<Pushable> outer, bool isReturn) {
	if (isReturn) {
		// for return values, to keep them alive until other side has taken them
		printf("C++ ClientObject %d safepush\n", id);
		// preserve for return values etc
		auto clientOuter = std::dynamic_pointer_cast<ClientResource>(outer);
		safeArea.push_back(clientOuter);
	}
	nativePush();
}

ClientResource::~ClientResource() {
	printf("C++ ClientResource::dtor (id %d), remote releasing\n", id);
	fflush(stdout);
	::ni_clientResourceRelease(id);
}

void ClientObject::nativePush()
{
	ni_pushInstance(id, true);
}

void ClientObject::invokeMethod(ni_InterfaceMethodRef method) {
	::ni_clientMethodExec(method, id);
}

void ClientObject::invokeMethodWithExceptions(ni_InterfaceMethodRef method) {
	::ni_clientMethodExec(method, id);
	ni_processExceptions();
}

void ClientFuncVal::remoteExec()
{
	ni_clientFuncExec(id);
}

void ClientFuncVal::remoteExecWithExceptions()
{
	ni_clientFuncExec(id);
	ni_processExceptions();
}

void ClientFuncVal::nativePush()
{
	// currently this should never be called, only server func values can be pushed from server
	// (a client function returned would be wrapped as a server func)
	throw "ClientFuncVal::nativePush - oops";
}

void ServerFuncVal::nativePush()
{
	ni_pushServerFunc(id);
}

std::shared_ptr<ServerFuncVal> ServerFuncVal::getById(int id)
{
	return std::dynamic_pointer_cast<ServerFuncVal>(ServerResource::getResourceById(id));
}

std::shared_ptr<ServerFuncVal> ServerFuncVal::create(std::function<void()> wrapper, size_t key)
{
	if (key != 0) {
		auto found = reverseServerFuncVals.find(key);
		if (found != reverseServerFuncVals.end()) {
			// reuse existing
			auto id = found->second;
			return ServerFuncVal::getById(id);
		}
	}
	// either no key provided, or not found: create anew
	auto funcVal = new ServerFuncVal(wrapper, key);
	if (key != 0) {
		// no point stuffing keyless things in the reverse map, all on top of each other!
		reverseServerFuncVals[key] = funcVal->id;
	}
	return std::shared_ptr<ServerFuncVal>(funcVal);
}

void ServerFuncVal::releaseExtra()
{
	printf("server releaseFuncVal(%d): refcount 0, releasing\n", id);
	reverseServerFuncVals.erase(uniqueKey);
}
