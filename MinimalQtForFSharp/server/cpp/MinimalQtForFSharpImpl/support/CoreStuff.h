#pragma once

#include <string>

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

// our imports from core (we could include directly, but this lets us simulate what it will be like for other languages)
extern "C" {
	// instead of accessing these directly, perhaps they should be behind functions exported from core?
	extern niClientFuncExec ni_clientFuncExec;
	extern niClientMethodExec ni_clientMethodExec;
	extern niClientResourceRelease ni_clientResourceRelease;
	extern niClientClearSafetyArea ni_clientClearSafetyArea;

	typedef void (*ModuleFunc)();
	typedef void (*InterfaceFunc)(int serverID);
	typedef void (*ConstantsFunc)();
	typedef void (*ExceptionBuilder)(); // more of a 'pop and throw' than building, per se

	extern ni_ModuleRef ni_registerModule(const char *name);
	extern ni_ModuleMethodRef ni_registerModuleMethod(ni_ModuleRef m, const char* name, ModuleFunc func);
	extern void ni_registerModuleConstants(ni_ModuleRef m, ConstantsFunc func);
	extern ni_InterfaceRef ni_registerInterface(ni_ModuleRef m, const char* name);
	extern ni_InterfaceMethodRef ni_registerInterfaceMethod(ni_InterfaceRef iface, const char* name, InterfaceFunc ifunc);
	extern ni_ExceptionRef ni_registerException(ni_ModuleRef m, const char* name, ExceptionBuilder builder);

	extern void ni_pushPtr(void* value);
	extern void* ni_popPtr();
	extern void ni_pushPtrArray(void** values, size_t count);
	extern void ni_popPtrArray(void*** values, size_t* count);

	extern void ni_pushSizeT(size_t value);
	extern size_t ni_popSizeT();
	extern void ni_pushSizeTArray(size_t* values, size_t count);
	extern void ni_popSizeTArray(size_t** values, size_t* count);

	extern void ni_pushBool(bool b);
	extern bool ni_popBool();
	extern void ni_pushBoolArray(bool* values, size_t count);
	extern void ni_popBoolArray(bool** values, size_t* count);

	extern void ni_pushInt8(int8_t x);
	extern int8_t ni_popInt8();
	extern void ni_pushInt8Array(int8_t* values, size_t count);
	extern void ni_popInt8Array(int8_t** values, size_t* count);

	extern void ni_pushUInt8(uint8_t x);
	extern uint8_t ni_popUInt8();
	extern void ni_pushUInt8Array(uint8_t* values, size_t count);
	extern void ni_popUInt8Array(uint8_t** values, size_t* count);

	extern void ni_pushInt16(int16_t x);
	extern int16_t ni_popInt16();
	extern void ni_pushInt16Array(int16_t* values, size_t count);
	extern void ni_popInt16Array(int16_t** values, size_t* count);

	extern void ni_pushUInt16(uint16_t x);
	extern uint16_t ni_popUInt16();
	extern void ni_pushUInt16Array(uint16_t* values, size_t count);
	extern void ni_popUInt16Array(uint16_t** values, size_t* count);

	extern void ni_pushInt32(int32_t x);
	extern int32_t ni_popInt32();
	extern void ni_pushInt32Array(int32_t* values, size_t count);
	extern void ni_popInt32Array(int32_t** values, size_t* count);

	extern void ni_pushUInt32(uint32_t x);
	extern uint32_t ni_popUInt32();
	extern void ni_pushUInt32Array(uint32_t* values, size_t count);
	extern void ni_popUInt32Array(uint32_t** values, size_t* count);

	extern void ni_pushInt64(int64_t x);
	extern int64_t ni_popInt64();
	extern void ni_pushInt64Array(int64_t* values, size_t count);
	extern void ni_popInt64Array(int64_t** values, size_t* count);

	extern void ni_pushUInt64(uint64_t x);
	extern uint64_t ni_popUInt64();
	extern void ni_pushUInt64Array(uint64_t* values, size_t count);
	extern void ni_popUInt64Array(uint64_t** values, size_t* count);

	extern void ni_pushFloat(float x);
	extern float ni_popFloat();
	extern void ni_pushFloatArray(float* values, size_t count);
	extern void ni_popFloatArray(float** values, size_t* count);

	extern void ni_pushDouble(double x);
	extern double ni_popDouble();
	extern void ni_pushDoubleArray(double* values, size_t count);
	extern void ni_popDoubleArray(double** values, size_t* count);

	extern void ni_pushString(const char* str, size_t length);
	extern void ni_popString(const char** strPtr, size_t* length);
	extern void ni_pushStringArray(const char** strs, size_t* lengths, size_t count);
	extern void ni_popStringArray(const char*** strs, size_t** lengths, size_t* count);

	struct ni_BufferDescriptor {
		// TODO: provisions for multi-dimensional, indirect pointers, instead of contiguous, stride, etc?
		void* start;
		int elementSize;
		size_t totalCount;
		size_t totalSize;
	};
	extern void ni_pushBuffer(int id, bool isClientId, ni_BufferDescriptor* descriptor);
	extern void ni_popBuffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor);

	extern int ni_popClientFunc();
	extern void ni_pushServerFunc(int id);

	extern void ni_pushInstance(int id, bool isClientId);
	extern int ni_popInstance(bool* isClientId);

	extern void ni_pushNull();

	extern void ni_setException(ni_ExceptionRef e);
	extern ni_ExceptionRef ni_getAndClearException();
	extern void ni_processExceptions(); // formerly buildAndThrow

	extern void ni_clearClientSafetyArea();
}
