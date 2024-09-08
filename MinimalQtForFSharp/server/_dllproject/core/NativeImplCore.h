#pragma once

#include <cstdint>
#include <stddef.h>

#define NIHANDLE(x) struct ni_##x; typedef struct ni_##x* ni_##x##Ref

NIHANDLE(Module);
NIHANDLE(ModuleMethod);
NIHANDLE(Interface);
NIHANDLE(InterfaceMethod);
NIHANDLE(Object);
NIHANDLE(Exception);

typedef void (*niClientFuncExec)(int id);
typedef void (*niClientMethodExec)(ni_InterfaceMethodRef method, int objID);
typedef void (*niClientResourceRelease)(int id);
typedef void (*niClientClearSafetyArea)();

// server registration methods (server-facing only, not visible through DLL)
extern "C" {
	typedef void (*ModuleFunc)();
	typedef void (*InterfaceFunc)(int serverID);
	typedef void (*ConstantsFunc)();
	typedef void (*ExceptionBuilder)(); // more of a 'pop and throw' than building, per se

	ni_ModuleRef ni_registerModule(const char *name);
	ni_ModuleMethodRef ni_registerModuleMethod(ni_ModuleRef m, const char *name, ModuleFunc func);
	void ni_registerModuleConstants(ni_ModuleRef m, ConstantsFunc func);
	ni_InterfaceRef ni_registerInterface(ni_ModuleRef m, const char *name);
	ni_InterfaceMethodRef ni_registerInterfaceMethod(ni_InterfaceRef iface, const char *name, InterfaceFunc ifunc);
	ni_ExceptionRef ni_registerException(ni_ModuleRef m, const char *name, ExceptionBuilder builder);
}

// imported back from server (during linking process, against static server library)
extern "C" {
	extern int nativeLibraryInit();		 // must be defined by library itself, to register methods, start runtime, etc
	extern void nativeLibraryShutdown(); // any native cleanup (runtime shutdown etc)
	extern void ni_dumpTables();
	extern void ni_execServerFunc(int id);
	extern ni_ExceptionRef ni_execServerFuncWithExceptions(int id);
	extern void ni_releaseServerResource(int id);
}

// visible to both client and server
extern "C" {
	// will there be any problems with allowing direct access to these?
	extern niClientFuncExec ni_clientFuncExec;
	extern niClientMethodExec ni_clientMethodExec;
	extern niClientResourceRelease ni_clientResourceRelease;
	extern niClientClearSafetyArea ni_clientClearSafetyArea;
	extern ni_ExceptionRef ni_currentException;

	// mostly client stuff, but some used by server as well
	int ni_nativeImplInit(
		niClientFuncExec clientFuncExec,
		niClientMethodExec clientMethodExec,
		niClientResourceRelease clientResourceRelease,
		niClientClearSafetyArea clientClearSafetyArea
	);
	void ni_nativeImplShutdown();

	ni_ModuleRef ni_getModule(const char* name);
	ni_ModuleMethodRef ni_getModuleMethod(ni_ModuleRef m, const char* name);
	ni_InterfaceRef ni_getInterface(ni_ModuleRef m, const char* name);
	ni_InterfaceMethodRef ni_getInterfaceMethod(ni_InterfaceRef iface, const char* name);
	ni_ExceptionRef ni_getException(ni_ModuleRef m, const char* name);
	void ni_pushModuleConstants(ni_ModuleRef m);

	void ni_invokeModuleMethod(ni_ModuleMethodRef method);
	ni_ExceptionRef ni_invokeModuleMethodWithExceptions(ni_ModuleMethodRef method);

	void ni_pushPtr(void* value);
	void* ni_popPtr();
	void ni_pushPtrArray(void** values, size_t count);
	void ni_popPtrArray(void*** values, size_t* count);

	void ni_pushSizeT(size_t value);
	size_t ni_popSizeT();
	void ni_pushSizeTArray(size_t* values, size_t count);
	void ni_popSizeTArray(size_t** values, size_t* count);

	void ni_pushBool(bool value);
	bool ni_popBool();
	void ni_pushBoolArray(bool* values, size_t count);
	void ni_popBoolArray(bool** values, size_t* count);

	void ni_pushInt8(int8_t x);
	int8_t ni_popInt8();
	void ni_pushInt8Array(int8_t* values, size_t count);
	void ni_popInt8Array(int8_t** values, size_t* count);

	void ni_pushUInt8(uint8_t x);
	uint8_t ni_popUInt8();
	void ni_pushUInt8Array(uint8_t* values, size_t count);
	void ni_popUInt8Array(uint8_t** values, size_t* count);

	void ni_pushInt16(int16_t x);
	int16_t ni_popInt16();
	void ni_pushInt16Array(int16_t* values, size_t count);
	void ni_popInt16Array(int16_t** values, size_t* count);

	void ni_pushUInt16(uint16_t x);
	uint16_t ni_popUInt16();
	void ni_pushUInt16Array(uint16_t* values, size_t count);
	void ni_popUInt16Array(uint16_t** values, size_t* count);

	void ni_pushInt32(int32_t x);
	int32_t ni_popInt32();
	void ni_pushInt32Array(int32_t* values, size_t count);
	void ni_popInt32Array(int32_t** values, size_t* count);

	void ni_pushUInt32(uint32_t x);
	uint32_t ni_popUInt32();
	void ni_pushUInt32Array(uint32_t* values, size_t count);
	void ni_popUInt32Array(uint32_t** values, size_t* count);

	void ni_pushInt64(int64_t x);
	int64_t ni_popInt64();
	void ni_pushInt64Array(int64_t* values, size_t count);
	void ni_popInt64Array(int64_t** values, size_t* count);

	void ni_pushUInt64(uint64_t x);
	uint64_t ni_popUInt64();
	void ni_pushUInt64Array(uint64_t* values, size_t count);
	void ni_popUInt64Array(uint64_t** values, size_t* count);

	void ni_pushFloat(float x);
	float ni_popFloat();
	void ni_pushFloatArray(float* values, size_t count);
	void ni_popFloatArray(float** values, size_t* count);

	void ni_pushDouble(double x);
	double ni_popDouble();
	void ni_pushDoubleArray(double* values, size_t count);
	void ni_popDoubleArray(double** values, size_t* count);

	void ni_pushString(const char* str, size_t length);     // length is optional
	void ni_popString(const char** strPtr, size_t* length); // result guaranteed until the next popString
	void ni_pushStringArray(const char** strs, size_t* lengths, size_t count);
	void ni_popStringArray(const char*** strs, size_t** lengths, size_t* count);

	struct ni_BufferDescriptor {
		// TODO: provisions for multi-dimensional, indirect pointers, instead of contiguous, stride, etc?
		void* start;
		int elementSize;
		size_t totalCount;
		size_t totalSize;
	};
	void ni_pushBuffer(int id, bool isClientId, ni_BufferDescriptor* descriptor);
	void ni_popBuffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor);

	void ni_pushClientFunc(int id);
	void ni_pushServerFunc(int id);
	// the reason for two separate pops is that, unlike interfaces, we have no way of "short-circuiting" these
	// (ie, if a client sends a function value to the server, and the server returns it, it's always wrapped anew as a server function)
	// this was simpler at the time than using single-method-interfaces which could carry information about their origin
	// perhaps to be revisited at some point, but the idea behind function values was more for simple lambda type stuff,
	// not meant to make roundtrips between client and server, the way interfaces might
	int ni_popClientFunc();
	int ni_popServerFunc();

	void ni_invokeInterfaceMethod(ni_InterfaceMethodRef method, int serverID);
	ni_ExceptionRef ni_invokeInterfaceMethodWithExceptions(ni_InterfaceMethodRef method, int serverID);

	// TODO: get rid of the safety area flag, it was there so we could only conditionally clear the safety area,
	// but I think all the ifs and checking (conditional setting of 'remotesafetyareadirty' or whatever) will end up
	// being equivalent to simply clearing the remote safety area unconditionally at the end of every invocation returning (something containing) an interface reference
	void ni_pushInstance(int id, bool isClientId);
	int ni_popInstance(bool* isClientID);

	// should work for all reference types: object instances, opaque handles, buffers, etc
	void ni_pushNull();

	// safety area functions: the act of pushing remote interfaces can destroy the local proxy (if no other local proxies exist),
	// prematurely dereferencing the remote side
	// eg, server pushing a client instance (converting to a simple ID on the stack), would instantly destroy the proxy
	//   before the client has a chance to grab it off the stack and save it from a potentially fatal deref.
	// the safety area protects until a return value (containing interface references) is fully grabbed
	void ni_clearClientSafetyArea();
	void ni_clearServerSafetyArea();

	void ni_dumpTables();

	void ni_setException(ni_ExceptionRef e);
	ni_ExceptionRef ni_getAndClearException();
	void ni_processExceptions(); // formerly buildAndThrow
}
