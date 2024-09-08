#include "NativeImplCore.h"

#include <cstdio>
#include <map>
#include <string>
#include <stack>
#include <exception>
#include <functional>
#include <memory>
#include <vector>
#include <assert.h>

// private opaque definitions
struct ni_Module {
	std::string name;
	std::map<std::string, ni_ModuleMethodRef> methods;
	std::map<std::string, ni_InterfaceRef> interfaces;
	std::map<std::string, ni_ExceptionRef> exceptions;
	ConstantsFunc constantsFunc = nullptr;
};

struct ni_ModuleMethod {
	ni_ModuleRef _module = nullptr;
	std::string name;
	ModuleFunc func;
};

struct ni_Interface {
	ni_ModuleRef _module = nullptr;
	std::string name;
	std::map<std::string, ni_InterfaceMethodRef> methods;
};

struct ni_InterfaceMethod {
	ni_InterfaceRef _iface = nullptr;
	std::string name;
	InterfaceFunc ifunc;
};

struct ni_Exception {
	ni_ModuleRef _module = nullptr;
	std::string name;
	ExceptionBuilder buildAndThrow;
};

// exceptions
class WrongTypeException : public std::exception {
	virtual const char* what() const throw() {
		return "Wrong Stack Item Type";
	}
};

// stack types =======================
class StackItem {
public:
	virtual void* ptrValue() { throw WrongTypeException(); }
	virtual std::vector<void*> ptrArray() { throw WrongTypeException(); }

	virtual size_t sizeTValue() { throw WrongTypeException(); }
	virtual std::vector<size_t> sizeTArray() { throw WrongTypeException(); }

	virtual bool boolValue() { throw WrongTypeException(); }
	virtual std::vector<uint8_t> boolArray() { throw WrongTypeException(); } // using 'bool' doesn't allow .data access

	virtual int8_t int8Value() { throw WrongTypeException(); }
	virtual std::vector<int8_t> int8Array() { throw WrongTypeException(); }

	virtual uint8_t uint8Value() { throw WrongTypeException(); }
	virtual std::vector<uint8_t> uint8Array() { throw WrongTypeException(); }

	virtual int16_t int16Value() { throw WrongTypeException(); }
	virtual std::vector<int16_t> int16Array() { throw WrongTypeException(); }

	virtual uint16_t uint16Value() { throw WrongTypeException(); }
	virtual std::vector<uint16_t> uint16Array() { throw WrongTypeException(); }

	virtual int32_t int32Value() { throw WrongTypeException(); }
	virtual std::vector<int32_t> int32Array() { throw WrongTypeException(); }

	virtual uint32_t uint32Value() { throw WrongTypeException(); }
	virtual std::vector<uint32_t> uint32Array() { throw WrongTypeException(); }

	virtual int64_t int64Value() { throw WrongTypeException(); }
	virtual std::vector<int64_t> int64Array() { throw WrongTypeException(); }

	virtual uint64_t uint64Value() { throw WrongTypeException(); }
	virtual std::vector<uint64_t> uint64Array() { throw WrongTypeException(); }

	virtual float floatValue() { throw WrongTypeException(); }
	virtual std::vector<float> floatArray() { throw WrongTypeException(); }

	virtual double doubleValue() { throw WrongTypeException(); }
	virtual std::vector<double> doubleArray() { throw WrongTypeException(); }

	virtual std::string stringValue() { throw WrongTypeException(); }
	virtual std::vector<std::string> stringArray() { throw WrongTypeException(); }

	virtual int clientFuncId() { throw WrongTypeException(); }
	virtual int serverFuncId() { throw WrongTypeException(); }
	virtual int instanceId(bool* isClientId) { throw WrongTypeException(); }

	virtual void buffer(int *id, bool* isClientId, ni_BufferDescriptor* descriptor) { throw WrongTypeException(); }
};

class NullItem : public StackItem {
public:
	NullItem() {}
	// should work for any reference type:
	void* ptrValue() override {
		return nullptr;
	}
	int instanceId(bool* isClientId) override {
		*isClientId = false;
		return 0;
	}
	void buffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor) override {
		*id = 0;
		*isClientId = false;
		descriptor->start = nullptr;
	}
};

class PtrItem : public StackItem {
public:
	void* value;
	PtrItem(void* value) : value(value) {}
	void* ptrValue() override {
		return value;
	}
};

class PtrArray : public StackItem {
public:
	std::vector<void*> values;
	PtrArray(void** ptr, size_t count) {
		values.assign(ptr, ptr + count);
	}
	std::vector<void*> ptrArray() override {
		return values;
	}
};

class SizeTItem : public StackItem {
public:
	size_t value;
	SizeTItem(size_t value) : value(value) {}
	size_t sizeTValue() override {
		return value;
	}
};

class SizeTArray : public StackItem {
public:
	std::vector<size_t> values;
	SizeTArray(size_t* ptr, size_t count) {
		values.assign(ptr, ptr + count);
	}
	std::vector<size_t> sizeTArray() override {
		return values;
	}
};

class BoolItem : public StackItem {
public:
	bool value;
	BoolItem(bool value) : value(value) {}
	bool boolValue() override {
		return value;
	}
};

class BoolArrayItem : public StackItem {
public:
	std::vector<uint8_t> values;		     // using std::vector<bool> doesn't allow .data access, lame
	BoolArrayItem(bool* ptr, size_t count) {
		values.assign(ptr, ptr + count);
	}
	std::vector<uint8_t> boolArray() override {
		return values;
	}
};

// INT8 ====

class Int8Item : public StackItem {
public:
	int8_t x;
	Int8Item(int8_t x) : x(x) {}
	int8_t int8Value() override {
		return x;
	}
};

class UInt8Item : public StackItem {
public:
	uint8_t x;
	UInt8Item(uint8_t x) : x(x) {}
	uint8_t uint8Value() override {
		return x;
	}
};

class Int8ArrayItem : public StackItem {
public:
	std::vector<int8_t> values;
	Int8ArrayItem(int8_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<int8_t> int8Array() override {
		return values;
	}
};

class UInt8ArrayItem : public StackItem {
public:
	std::vector<uint8_t> values;
	UInt8ArrayItem(uint8_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<uint8_t> uint8Array() override {
		return values;
	}
};

// INT16 ====

class Int16Item : public StackItem {
public:
	int16_t x;
	Int16Item(int16_t x) : x(x) {}
	int16_t int16Value() override {
		return x;
	}
};

class UInt16Item : public StackItem {
public:
	uint16_t x;
	UInt16Item(uint16_t x) : x(x) {}
	uint16_t uint16Value() override {
		return x;
	}
};

class Int16ArrayItem : public StackItem {
public:
	std::vector<int16_t> values;
	Int16ArrayItem(int16_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<int16_t> int16Array() override {
		return values;
	}
};

class UInt16ArrayItem : public StackItem {
public:
	std::vector<uint16_t> values;
	UInt16ArrayItem(uint16_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<uint16_t> uint16Array() override {
		return values;
	}
};

// INT32 ====

class Int32Item : public StackItem {
public:
	int32_t x;
	Int32Item(int32_t x) : x(x) {}
	int32_t int32Value() override {
		return x;
	}
};

class UInt32Item : public StackItem {
public:
	uint32_t x;
	UInt32Item(uint32_t x) : x(x) {}
	uint32_t uint32Value() override {
		return x;
	}
};

class Int32ArrayItem : public StackItem {
public:
	std::vector<int32_t> values;
	Int32ArrayItem(int32_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<int32_t> int32Array() override {
		return values;
	}
};

class UInt32ArrayItem : public StackItem {
public:
	std::vector<uint32_t> values;
	UInt32ArrayItem(uint32_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<uint32_t> uint32Array() override {
		return values;
	}
};

// INT64 ====

class Int64Item : public StackItem {
public:
	int64_t x;
	Int64Item(int64_t x) : x(x) {}
	int64_t int64Value() override {
		return x;
	}
};

class UInt64Item : public StackItem {
public:
	uint64_t x;
	UInt64Item(uint64_t x) : x(x) {}
	uint64_t uint64Value() override {
		return x;
	}
};

class Int64ArrayItem : public StackItem {
public:
	std::vector<int64_t> values;
	Int64ArrayItem(int64_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<int64_t> int64Array() override {
		return values;
	}
};

class UInt64ArrayItem : public StackItem {
public:
	std::vector<uint64_t> values;
	UInt64ArrayItem(uint64_t* ptr, size_t count) {
		if (count > 0) {
			values.assign(ptr, ptr + count);
		}
	}
	std::vector<uint64_t> uint64Array() override {
		return values;
	}
};

class FloatItem : public StackItem {
public:
	float x;
	FloatItem(float x) : x(x) {}
	float floatValue() override {
		return x;
	}
};

class FloatArrayItem : public StackItem {
public:
	std::vector<float> values;
	FloatArrayItem(float* ptr, size_t count) {
		values.assign(ptr, ptr + count);
	}
	std::vector<float> floatArray() override {
		return values;
	}
};

class DoubleItem : public StackItem {
public:
	double x;
	DoubleItem(double x) : x(x) {}
	double doubleValue() override {
		return x;
	}
};

class DoubleArrayItem : public StackItem {
public:
	std::vector<double> values;
	DoubleArrayItem(double* ptr, size_t count) {
		values.assign(ptr, ptr + count);
	}
	std::vector<double> doubleArray() override {
		return values;
	}
};

class StringItem : public StackItem {
public:
	std::string str;
	StringItem(const char* raw, size_t length) {
		if (length > 0) {
			str.assign(raw, length);
		}
        // else keep empty
	}
	std::string stringValue() override {
		return str;
	}
};

class StringArrayItem : public StackItem {
public:
	std::vector<std::string> values;
	StringArrayItem(const char** ptrs, size_t* lengths, size_t count) {
		for (auto i = 0; i < count; i++) {
			std::string str;
			if (lengths[i] > 0) {
				str.assign(ptrs[i], lengths[i]);
			}
            // else keep empty
			values.push_back(str);
		}
	}
	std::vector<std::string> stringArray() override {
		return values;
	}
};

// client func val
class ClientFuncItem : public StackItem {
public:
	int id;
	ClientFuncItem(int id) : id(id) {}
	int clientFuncId() override {
		return id;
	}
};

// server func val
class ServerFuncItem : public StackItem {
public:
	int id;
	ServerFuncItem(int id) : id(id) {}
	int serverFuncId() override {
		return id;
	}
};

// client inst ID
class InstanceItem : public StackItem {
public:
	int id;
	bool isClientId;
	InstanceItem(int id, bool isClientId) :
		id(id),
		isClientId(isClientId) {}
	int instanceId(bool* isClientId) override {
		*isClientId = this->isClientId;
		return id;
	}
};

class BufferItem : public StackItem {
public:
	int id;
	bool isClientId;
	ni_BufferDescriptor descriptor;
	BufferItem(int id, bool isClientId, ni_BufferDescriptor* descriptor)
		: id(id), isClientId(isClientId)
	{
		this->descriptor = *descriptor;
	}
	void buffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor) override {
		*id = this->id;
		*isClientId = this->isClientId;
		*descriptor = this->descriptor;
	}
};

// null methods for callback defaults (also assigned when client is shut down, to prevent dtors from calling back into client)
static void nullIntMethod(int x) {}
static void nullVoidMethod() {}
static void nullMethodExec(ni_InterfaceMethodRef method, int id) {}

// private global stuff ============================
static std::map<std::string, ni_ModuleRef> modules;

niClientFuncExec ni_clientFuncExec;
niClientMethodExec ni_clientMethodExec;
niClientResourceRelease ni_clientResourceRelease;
niClientClearSafetyArea ni_clientClearSafetyArea;

struct ThreadLocal {
	std::stack<std::unique_ptr<StackItem>> vstack;
	ni_ExceptionRef currentException = nullptr;

	std::vector<void*> lastPtrArray;
	std::vector<size_t> lastSizeTArray;
	std::vector<uint8_t> lastBoolArray;
	std::vector<int8_t> lastInt8Array;
	std::vector<uint8_t> lastUInt8Array;
	std::vector<int16_t> lastInt16Array;
	std::vector<uint16_t> lastUInt16Array;
	std::vector<int32_t> lastInt32Array;
	std::vector<uint32_t> lastUInt32Array;
	std::vector<int64_t> lastInt64Array;
	std::vector<uint64_t> lastUInt64Array;
	std::vector<float> lastFloatArray;
	std::vector<double> lastDoubleArray;
	std::string lastPoppedString; // pointers gotta point to something ...
	std::vector<std::string> lastPoppedStringArray;
	const char** lastPoppedStrs = nullptr;
	size_t* lastPoppedStrLengths = nullptr;

	inline void push(StackItem* item) {
		vstack.push(std::unique_ptr<StackItem>(item));
	}

	inline std::unique_ptr<StackItem> pop() {
		auto ret = std::move(vstack.top());
		vstack.pop();
		return ret;
	}
};
// todo: require formal threadInit() and allocate manually? is that more efficient?
static thread_local ThreadLocal thlocal;

ni_ModuleRef ni_registerModule(const char *name)
{
	// make sure name doesn't already exist
	auto found = modules.find(name);
	if (found != modules.end()) {
		printf("registerModule: module name [%s] already exists!\n", name);
		return nullptr;
	}
	auto mod = new ni_Module{ name };
	modules[name] = mod;
	return mod;
}

ni_ModuleMethodRef ni_registerModuleMethod(ni_ModuleRef m, const char *name, ModuleFunc func)
{
	auto found = m->methods.find(name);
	if (found != m->methods.end()) {
		printf("registerModuleMethod: method name [%s] already exists!\n", name);
		return nullptr;
	}
	auto method = new ni_ModuleMethod{ m, name, func };
	m->methods[name] = method;
	return method;
}

void ni_registerModuleConstants(ni_ModuleRef m, ConstantsFunc func) {
	if (m->constantsFunc != nullptr) {
		printf("Module %s ConstantsFunc already defined\n", m->name.c_str());
	}
	else {
		m->constantsFunc = func;
	}
}

ni_InterfaceRef ni_registerInterface(ni_ModuleRef m, const char *name)
{
	auto found = m->interfaces.find(name);
	if (found != m->interfaces.end()) {
		printf("registerModuleInterface: interface name [%s] already exists!\n", name);
		return nullptr;
	}
	auto iface = new ni_Interface{ m, name };
	m->interfaces[name] = iface;
	return iface;
}

ni_InterfaceMethodRef ni_registerInterfaceMethod(ni_InterfaceRef iface, const char *name, InterfaceFunc ifunc)
{
	auto found = iface->methods.find(name);
	if (found != iface->methods.end()) {
		printf("registerInterfaceMethod: iface method name [%s] already exists!\n", name);
		return nullptr;
	}
	auto method = new ni_InterfaceMethod{ iface, name, ifunc };
	iface->methods[name] = method;
	return method;
}

ni_ExceptionRef ni_registerException(ni_ModuleRef m, const char *name, ExceptionBuilder buildAndThrow) {
	auto found = m->exceptions.find(name);
	if (found != m->exceptions.end()) {
		printf("registerException: method exception name [%s] already exists!\n", name);
		return nullptr;
	}
	auto exception = new ni_Exception{ m, name, buildAndThrow };
	m->exceptions[name] = exception;
	return exception;
}

// client methods (some used by server as well) ===============================

int ni_nativeImplInit(
	niClientFuncExec clientFuncExec,
	niClientMethodExec clientMethodExec,
	niClientResourceRelease clientResourceRelease,
	niClientClearSafetyArea clientClearSafetyArea
	)
{
	::ni_clientFuncExec = clientFuncExec;
	::ni_clientMethodExec = clientMethodExec;
	::ni_clientResourceRelease = clientResourceRelease;
	::ni_clientClearSafetyArea = clientClearSafetyArea;

	// library-specific registrations etc
	return nativeLibraryInit();
}

void ni_nativeImplShutdown()
{
	printf("nativeImplShutdown\n");

	// de-fang the callbacks so they don't wreak havoc
	ni_clientFuncExec = &nullIntMethod;
	ni_clientMethodExec = &nullMethodExec;
	ni_clientResourceRelease = &nullIntMethod;
	ni_clientClearSafetyArea = &nullVoidMethod;

	printf(" - callbacks disabled\n");
	fflush(stdout);

	nativeLibraryShutdown();
}

ni_ModuleRef ni_getModule(const char* name) {
	auto found = modules.find(std::string(name));
	if (found != modules.end()) {
		return found->second;
	}
	return nullptr;
}

ni_ModuleMethodRef ni_getModuleMethod(ni_ModuleRef m, const char* name) {
	auto found = m->methods.find(std::string(name));
	if (found != m->methods.end()) {
		return found->second;
	}
	return nullptr;
}

ni_InterfaceRef ni_getInterface(ni_ModuleRef m, const char* name) {
	auto found = m->interfaces.find(std::string(name));
	if (found != m->interfaces.end()) {
		return found->second;
	}
	return nullptr;
}

ni_InterfaceMethodRef ni_getInterfaceMethod(ni_InterfaceRef iface, const char* name) {
	auto found = iface->methods.find(std::string(name));
	if (found != iface->methods.end()) {
		return found->second;
	}
	return nullptr;
}

ni_ExceptionRef ni_getException(ni_ModuleRef m, const char* name) {
	auto found = m->exceptions.find(std::string(name));
	if (found != m->exceptions.end()) {
		return found->second;
	}
	return nullptr;
}

void ni_pushModuleConstants(ni_ModuleRef m) {
	m->constantsFunc();
}

void ni_invokeModuleMethod(ni_ModuleMethodRef method) {
	method->func();
	assert(thlocal.currentException == nullptr); // this should never be used for methods potentially throwing exceptions!
}

ni_ExceptionRef ni_invokeModuleMethodWithExceptions(ni_ModuleMethodRef method) {
	assert(thlocal.currentException == nullptr);
	method->func();
	auto ret = thlocal.currentException;
	thlocal.currentException = nullptr;
	return ret;
}

void ni_pushPtr(void* value)
{
	thlocal.push(new PtrItem(value));
}

void* ni_popPtr()
{
	return thlocal.pop()->ptrValue();
}

void ni_pushPtrArray(void** values, size_t count)
{
	thlocal.push(new PtrArray(values, count));
}

void ni_popPtrArray(void*** values, size_t* count)
{
	thlocal.lastPtrArray = thlocal.pop()->ptrArray();
	*values = thlocal.lastPtrArray.data();
	*count = thlocal.lastPtrArray.size();
}

void ni_pushSizeT(size_t value) {
	thlocal.push(new SizeTItem(value));
}

size_t ni_popSizeT() {
	return thlocal.pop()->sizeTValue();
}

void ni_pushSizeTArray(size_t* values, size_t count) {
	thlocal.push(new SizeTArray(values, count));
}

void ni_popSizeTArray(size_t** values, size_t* count) {
	thlocal.lastSizeTArray = thlocal.pop()->sizeTArray();
	*values = thlocal.lastSizeTArray.data();
	*count = thlocal.lastSizeTArray.size();
}

void ni_pushBool(bool value) {
	thlocal.push(new BoolItem(value));
}

bool ni_popBool() {
	return thlocal.pop()->boolValue();
}

void ni_pushBoolArray(bool* values, size_t count)
{
	thlocal.push(new BoolArrayItem(values, count));
}

void ni_popBoolArray(bool** values, size_t* count)
{
	thlocal.lastBoolArray = thlocal.pop()->boolArray();
	*((uint8_t**)values) = thlocal.lastBoolArray.data();
	*count = thlocal.lastBoolArray.size();
}

// INT8 stuff ===============================

void ni_pushInt8(int8_t x)
{
	thlocal.push(new Int8Item(x));
}

int8_t ni_popInt8()
{
	return thlocal.pop()->int8Value();
}

void ni_pushInt8Array(int8_t* values, size_t count)
{
	thlocal.push(new Int8ArrayItem(values, count));
}

void ni_popInt8Array(int8_t** values, size_t* count)
{
	thlocal.lastInt8Array = thlocal.pop()->int8Array();
	*values = thlocal.lastInt8Array.data();
	*count = thlocal.lastInt8Array.size();
}

void ni_pushUInt8(uint8_t x)
{
	thlocal.push(new UInt8Item(x));
}

uint8_t ni_popUInt8()
{
	return thlocal.pop()->uint8Value();
}

void ni_pushUInt8Array(uint8_t* values, size_t count)
{
	thlocal.push(new UInt8ArrayItem(values, count));
}

void ni_popUInt8Array(uint8_t** values, size_t* count)
{
	thlocal.lastUInt8Array = thlocal.pop()->uint8Array();
	*values = thlocal.lastUInt8Array.data();
	*count = thlocal.lastUInt8Array.size();
}

// INT16 stuff ===============================

void ni_pushInt16(int16_t x)
{
	thlocal.push(new Int16Item(x));
}

int16_t ni_popInt16()
{
	return thlocal.pop()->int16Value();
}

void ni_pushInt16Array(int16_t* values, size_t count)
{
	thlocal.push(new Int16ArrayItem(values, count));
}

void ni_popInt16Array(int16_t** values, size_t* count)
{
	thlocal.lastInt16Array = thlocal.pop()->int16Array();
	*values = thlocal.lastInt16Array.data();
	*count = thlocal.lastInt16Array.size();
}

void ni_pushUInt16(uint16_t x)
{
	thlocal.push(new UInt16Item(x));
}

uint16_t ni_popUInt16()
{
	return thlocal.pop()->uint16Value();
}

void ni_pushUInt16Array(uint16_t* values, size_t count)
{
	thlocal.push(new UInt16ArrayItem(values, count));
}

void ni_popUInt16Array(uint16_t** values, size_t* count)
{
	thlocal.lastUInt16Array = thlocal.pop()->uint16Array();
	*values = thlocal.lastUInt16Array.data();
	*count = thlocal.lastUInt16Array.size();
}

// INT32 stuff ===============================

void ni_pushInt32(int32_t x)
{
	thlocal.push(new Int32Item(x));
}

int32_t ni_popInt32()
{
	return thlocal.pop()->int32Value();
}

void ni_pushInt32Array(int32_t* values, size_t count)
{
	thlocal.push(new Int32ArrayItem(values, count));
}

void ni_popInt32Array(int32_t** values, size_t* count)
{
	thlocal.lastInt32Array = thlocal.pop()->int32Array();
	*values = thlocal.lastInt32Array.data();
	*count = thlocal.lastInt32Array.size();
}

void ni_pushUInt32(uint32_t x)
{
	thlocal.push(new UInt32Item(x));
}

uint32_t ni_popUInt32()
{
	return thlocal.pop()->uint32Value();
}

void ni_pushUInt32Array(uint32_t* values, size_t count)
{
	thlocal.push(new UInt32ArrayItem(values, count));
}

void ni_popUInt32Array(uint32_t** values, size_t* count)
{
	thlocal.lastUInt32Array = thlocal .pop()->uint32Array();
	*values = thlocal.lastUInt32Array.data();
	*count = thlocal.lastUInt32Array.size();
}

// INT64 stuff ===============================

void ni_pushInt64(int64_t x)
{
	thlocal.push(new Int64Item(x));
}

int64_t ni_popInt64()
{
	return thlocal.pop()->int64Value();
}

void ni_pushInt64Array(int64_t* values, size_t count)
{
	thlocal.push(new Int64ArrayItem(values, count));
}

void ni_popInt64Array(int64_t** values, size_t* count)
{
	thlocal.lastInt64Array = thlocal.pop()->int64Array();
	*values = thlocal.lastInt64Array.data();
	*count = thlocal.lastInt64Array.size();
}

void ni_pushUInt64(uint64_t x)
{
	thlocal.push(new UInt64Item(x));
}

uint64_t ni_popUInt64()
{
	return thlocal.pop()->uint64Value();
}

void ni_pushUInt64Array(uint64_t* values, size_t count)
{
	thlocal.push(new UInt64ArrayItem(values, count));
}

void ni_popUInt64Array(uint64_t** values, size_t* count)
{
	thlocal.lastUInt64Array = thlocal.pop()->uint64Array();
	*values = thlocal.lastUInt64Array.data();
	*count = thlocal.lastUInt64Array.size();
}

// ==== end ints, sigh =======================================

void ni_pushFloat(float x)
{
	thlocal.push(new FloatItem(x));
}

float ni_popFloat()
{
	return thlocal.pop()->floatValue();
}

void ni_pushFloatArray(float* values, size_t count)
{
	thlocal.push(new FloatArrayItem(values, count));
}

void ni_popFloatArray(float** values, size_t* count)
{
	thlocal.lastFloatArray = thlocal.pop()->floatArray();
	*values = thlocal.lastFloatArray.data();
	*count = thlocal.lastFloatArray.size();
}

void ni_pushDouble(double x)
{
	thlocal.push(new DoubleItem(x));
}

double ni_popDouble()
{
	return thlocal.pop()->doubleValue();
}

void ni_pushDoubleArray(double* values, size_t count)
{
	thlocal.push(new DoubleArrayItem(values, count));
}

void ni_popDoubleArray(double** values, size_t* count)
{
	thlocal.lastDoubleArray = thlocal.pop()->doubleArray();
	*values = thlocal.lastDoubleArray.data();
	*count = thlocal.lastDoubleArray.size();
}

void ni_pushString(const char* str, size_t length)
{
	thlocal.push(new StringItem(str, length));
}

void ni_popString(const char** strPtr, size_t* length)
{
	thlocal.lastPoppedString = thlocal.pop()->stringValue();
	*strPtr = thlocal.lastPoppedString.c_str();
	*length = thlocal.lastPoppedString.size();
}

void ni_pushStringArray(const char** strs, size_t* lengths, size_t count)
{
	thlocal.push(new StringArrayItem(strs, lengths, count));
}

void ni_popStringArray(const char*** strs, size_t** lengths, size_t* count)
{
	thlocal.lastPoppedStringArray = thlocal.pop()->stringArray();
	//
	delete[] thlocal.lastPoppedStrs;
	delete[] thlocal.lastPoppedStrLengths;
	auto _count = thlocal.lastPoppedStringArray.size();
	thlocal.lastPoppedStrs = new const char*[_count];
	thlocal.lastPoppedStrLengths = new size_t[_count];
	for (auto i = 0; i < _count; i++) {
		thlocal.lastPoppedStrs[i] = thlocal.lastPoppedStringArray[i].data();
		thlocal.lastPoppedStrLengths[i] = thlocal.lastPoppedStringArray[i].size();
	}
	*strs = thlocal.lastPoppedStrs;
	*lengths = thlocal.lastPoppedStrLengths;
	*count = _count;
}

void ni_pushBuffer(int id, bool isClientId, ni_BufferDescriptor* descriptor)
{
	thlocal.push(new BufferItem(id, isClientId, descriptor));
}

void ni_popBuffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor)
{
	thlocal.pop()->buffer(id, isClientId, descriptor);
}

void ni_pushClientFunc(int id)
{
	thlocal.push(new ClientFuncItem(id));
}

void ni_pushServerFunc(int id)
{
	thlocal.push(new ServerFuncItem(id));
}

int ni_popServerFunc() {
	return thlocal.pop()->serverFuncId();
}

void ni_invokeInterfaceMethod(ni_InterfaceMethodRef method, int serverID)
{
	method->ifunc(serverID);
	assert(thlocal.currentException == nullptr); // use below when exceptions are involved
}

ni_ExceptionRef ni_invokeInterfaceMethodWithExceptions(ni_InterfaceMethodRef method, int serverID) {
	assert(thlocal.currentException == nullptr);
	method->ifunc(serverID);
	auto ret = thlocal.currentException;
	thlocal.currentException = nullptr;
	return ret;
}

void ni_pushInstance(int id, bool isClientId)
{
	thlocal.push(new InstanceItem(id, isClientId));
}

int ni_popInstance(bool* isClientID) {
	return thlocal.pop()->instanceId(isClientID);
}

void ni_pushNull()
{
	thlocal.push(new NullItem());
}

ni_ExceptionRef ni_getAndClearException() {
	auto ret = thlocal.currentException;
	thlocal.currentException = nullptr;
	return ret;
}

void ni_setException(ni_ExceptionRef e) {
	thlocal.currentException = e;
}

// visible to server only ====================
int ni_popClientFunc() {
	return thlocal.pop()->clientFuncId();
}

void ni_clearClientSafetyArea() {
	::ni_clientClearSafetyArea(); // ugh this confusing naming ...
}

void ni_processExceptions() {
	if (thlocal.currentException != nullptr) {
		auto e = thlocal.currentException;
		thlocal.currentException = nullptr;
		e->buildAndThrow();
	}
}
