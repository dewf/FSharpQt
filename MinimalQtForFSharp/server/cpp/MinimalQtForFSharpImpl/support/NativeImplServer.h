#pragma once

#include "CoreStuff.h"

#include <memory>
#include <functional>
#include <vector>

class Pushable {
public:
	virtual void push(std::shared_ptr<Pushable> outer, bool isReturn) = 0;
};

class ServerResource : public Pushable {
private:
	int refCount = 0;
	const char* kind = "unknown";
protected:
	int id = -1;
	ServerResource(const char* kind) : kind(kind) {}
public:
	void release();
	void push(std::shared_ptr<Pushable> outer, bool isReturn) override;
	~ServerResource();
	static std::shared_ptr<ServerResource> getResourceById(int id);
	static void dumpTables();
protected:
	virtual void nativePush() = 0; // implemented specifically for: objects, buffers, etc

	// anything else you might want to do here, when destroyed (eg used by ServerFuncVal to remove itself from reverse map, and server interface wrappers)
	// (tried virtual destructors but things were behaving strangely)
	virtual void releaseExtra() {}
};

class ClientResource : public Pushable {
protected:
	int id;
	virtual void nativePush() = 0;
public:
	ClientResource(int id) : id(id) {}
	void push(std::shared_ptr<Pushable> outer, bool isReturn) override;
	virtual ~ClientResource();
};

class ServerObject : public ServerResource {
public:
	ServerObject() : ServerResource("object") {}
	static std::shared_ptr<ServerObject> getByID(int id);
protected:
	void nativePush() override;
};

class ClientObject : public ClientResource {
public:
	ClientObject(int id) : ClientResource(id) {}
	void invokeMethod(ni_InterfaceMethodRef method);
	void invokeMethodWithExceptions(ni_InterfaceMethodRef method);
protected:
	void nativePush() override;
};

class ClientFuncVal : public ClientResource {
public:
	ClientFuncVal(int id) : ClientResource(id) {}
	void remoteExec();
	void remoteExecWithExceptions();
protected:
	void nativePush() override;
};

class ServerFuncVal : public ServerResource {
private:
	ServerFuncVal(std::function<void()> wrapper, size_t uniqueKey)
		: ServerResource("funcval"),
		wrapper(wrapper),
		uniqueKey(uniqueKey) {}
	size_t uniqueKey;
public:
	std::function<void()> wrapper;
	void nativePush() override;
	static std::shared_ptr<ServerFuncVal> getById(int id);
	static std::shared_ptr<ServerFuncVal> create(std::function<void()> wrapper, size_t key);
protected:
	void releaseExtra() override;
};

template <typename T>
class NativeBuffer {
public:
	virtual T* getSpan(size_t* length) = 0;
};

template <typename T>
class ClientBuffer : public ClientResource, public NativeBuffer<T> {
private:
	ni_BufferDescriptor descriptor;
public:
	ClientBuffer(int id, ni_BufferDescriptor& descriptor)
		: ClientResource(id), descriptor(descriptor) {}
	T* getSpan(size_t* length) override {
		*length = descriptor.totalCount;
		return (T*)descriptor.start;
	}
	void nativePush() override {
		ni_pushBuffer(id, true, &descriptor);
	}
};

template <typename T>
class ServerBuffer : public ServerResource, public NativeBuffer<T> {
private:
	void* block;
	int elementSize;
	size_t totalCount;
	size_t totalSize;
protected:
	void nativePush() override {
		ni_BufferDescriptor desc;
		desc.start = block;
		desc.elementSize = elementSize;
		desc.totalCount = totalCount;
		desc.totalSize = totalSize;
		ni_pushBuffer(id, false, &desc);
	}
	void releaseExtra() override {
		free(block);
		block = nullptr;
	}
public:
	ServerBuffer(std::vector<int> dims)
		: ServerResource("buffer")
	{
		elementSize = sizeof(T);
		totalCount = 1;
		for (auto i = dims.begin(); i != dims.end(); i++) {
			totalCount *= *i;
		}
		totalSize = totalCount * elementSize;
		block = malloc(totalSize);
	}
	T* getSpan(size_t* length) override {
		*length = totalCount;
		return (T*)block;
	}
	static std::shared_ptr<ServerBuffer<T>> getByID(int id) {
		auto res = ServerResource::getResourceById(id);
		return std::dynamic_pointer_cast<ServerBuffer<T>>(res);
	}
};


// our "exports" back to core (well, the DLL being built around core)
extern "C" {
	int nativeLibraryInit();
	void nativeLibraryShutdown();
	void ni_dumpTables();
	void ni_execServerFunc(int id);
	ni_ExceptionRef ni_execServerFuncWithExceptions(int id);
	void ni_releaseServerResource(int id);
	void ni_clearServerSafetyArea();
}

void pushServerFuncVal(std::function<void()> wrapper, size_t key);

void pushStringInternal(std::string str);
std::string popStringInternal();

void pushBoolArrayInternal(std::vector<bool> values);
std::vector<bool> popBoolArrayInternal();

void pushInt8ArrayInternal(std::vector<int8_t> values);
std::vector<int8_t> popInt8ArrayInternal();

void pushUInt8ArrayInternal(std::vector<uint8_t> values);
std::vector<uint8_t> popUInt8ArrayInternal();

void pushInt16ArrayInternal(std::vector<int16_t> values);
std::vector<int16_t> popInt16ArrayInternal();

void pushUInt16ArrayInternal(std::vector<uint16_t> values);
std::vector<uint16_t> popUInt16ArrayInternal();

void pushInt32ArrayInternal(std::vector<int32_t> values);
std::vector<int32_t> popInt32ArrayInternal();

void pushUInt32ArrayInternal(std::vector<uint32_t> values);
std::vector<uint32_t> popUInt32ArrayInternal();

void pushInt64ArrayInternal(std::vector<int64_t> values);
std::vector<int64_t> popInt64ArrayInternal();

void pushUInt64ArrayInternal(std::vector<uint64_t> values);
std::vector<uint64_t> popUInt64ArrayInternal();

void pushFloatArrayInternal(std::vector<float> values);
std::vector<float> popFloatArrayInternal();

void pushDoubleArrayInternal(std::vector<double> values);
std::vector<double> popDoubleArrayInternal();

void pushStringArrayInternal(std::vector<std::string> values);
std::vector<std::string> popStringArrayInternal();

void pushSizeTArrayInternal(std::vector<size_t> values);
std::vector<size_t> popSizeTArrayInternal();
