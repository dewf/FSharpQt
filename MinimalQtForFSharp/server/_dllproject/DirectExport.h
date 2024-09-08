#pragma once

#include "core/NativeImplCore.h"
#include <cstdint>

#ifdef _WIN32
#define QTTESTINGSERVER_EXPORT __declspec(dllexport)
#else
#define QTTESTINGSERVER_EXPORT __attribute__ ((visibility ("default")))
#endif

extern "C" {
    QTTESTINGSERVER_EXPORT int nativeImplInit(
		niClientFuncExec clientFuncExec,
		niClientMethodExec clientMethodExec,
		niClientResourceRelease clientResourceRelease,
		niClientClearSafetyArea clientClearSafetyArea
	);
	QTTESTINGSERVER_EXPORT void nativeImplShutdown();

	QTTESTINGSERVER_EXPORT ni_ModuleRef getModule(const char* name);
	QTTESTINGSERVER_EXPORT ni_ModuleMethodRef getModuleMethod(ni_ModuleRef m, const char* name);
	QTTESTINGSERVER_EXPORT ni_InterfaceRef getInterface(ni_ModuleRef m, const char* name);
	QTTESTINGSERVER_EXPORT ni_InterfaceMethodRef getInterfaceMethod(ni_InterfaceRef iface, const char* name);
	QTTESTINGSERVER_EXPORT ni_ExceptionRef getException(ni_ModuleRef m, const char* name);
	QTTESTINGSERVER_EXPORT void pushModuleConstants(ni_ModuleRef m);

	QTTESTINGSERVER_EXPORT void invokeModuleMethod(ni_ModuleMethodRef method);
	QTTESTINGSERVER_EXPORT ni_ExceptionRef invokeModuleMethodWithExceptions(ni_ModuleMethodRef method);

	QTTESTINGSERVER_EXPORT void pushPtr(void* value);
	QTTESTINGSERVER_EXPORT void* popPtr();
	QTTESTINGSERVER_EXPORT void pushPtrArray(void** values, size_t count);
	QTTESTINGSERVER_EXPORT void popPtrArray(void*** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushSizeT(size_t value);
	QTTESTINGSERVER_EXPORT size_t popSizeT();
	QTTESTINGSERVER_EXPORT void pushSizeTArray(size_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popSizeTArray(size_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushBool(bool value);
	QTTESTINGSERVER_EXPORT bool popBool();
	QTTESTINGSERVER_EXPORT void pushBoolArray(bool* values, size_t count);
	QTTESTINGSERVER_EXPORT void popBoolArray(bool** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushInt8(int8_t x);
	QTTESTINGSERVER_EXPORT int8_t popInt8();
	QTTESTINGSERVER_EXPORT void pushInt8Array(int8_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popInt8Array(int8_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushUInt8(uint8_t x);
	QTTESTINGSERVER_EXPORT uint8_t popUInt8();
	QTTESTINGSERVER_EXPORT void pushUInt8Array(uint8_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popUInt8Array(uint8_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushInt16(int16_t x);
	QTTESTINGSERVER_EXPORT int16_t popInt16();
	QTTESTINGSERVER_EXPORT void pushInt16Array(int16_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popInt16Array(int16_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushUInt16(uint16_t x);
	QTTESTINGSERVER_EXPORT uint16_t popUInt16();
	QTTESTINGSERVER_EXPORT void pushUInt16Array(uint16_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popUInt16Array(uint16_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushInt32(int32_t x);
	QTTESTINGSERVER_EXPORT int32_t popInt32();
	QTTESTINGSERVER_EXPORT void pushInt32Array(int32_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popInt32Array(int32_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushUInt32(uint32_t x);
	QTTESTINGSERVER_EXPORT uint32_t popUInt32();
	QTTESTINGSERVER_EXPORT void pushUInt32Array(uint32_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popUInt32Array(uint32_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushInt64(int64_t x);
	QTTESTINGSERVER_EXPORT int64_t popInt64();
	QTTESTINGSERVER_EXPORT void pushInt64Array(int64_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popInt64Array(int64_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushUInt64(uint64_t x);
	QTTESTINGSERVER_EXPORT uint64_t popUInt64();
	QTTESTINGSERVER_EXPORT void pushUInt64Array(uint64_t* values, size_t count);
	QTTESTINGSERVER_EXPORT void popUInt64Array(uint64_t** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushFloat(float x);
	QTTESTINGSERVER_EXPORT float popFloat();
	QTTESTINGSERVER_EXPORT void pushFloatArray(float* values, size_t count);
	QTTESTINGSERVER_EXPORT void popFloatArray(float** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushDouble(double x);
	QTTESTINGSERVER_EXPORT double popDouble();
	QTTESTINGSERVER_EXPORT void pushDoubleArray(double* values, size_t count);
	QTTESTINGSERVER_EXPORT void popDoubleArray(double** values, size_t* count);

	QTTESTINGSERVER_EXPORT void pushString(const char* str, size_t length);
	QTTESTINGSERVER_EXPORT void popString(const char** strPtr, size_t* length);
	QTTESTINGSERVER_EXPORT void pushStringArray(const char** strs, size_t* lengths, size_t count);
	QTTESTINGSERVER_EXPORT void popStringArray(const char*** strs, size_t** lengths, size_t* count);

	QTTESTINGSERVER_EXPORT void pushBuffer(int id, bool isClientId, ni_BufferDescriptor* descriptor);
	QTTESTINGSERVER_EXPORT void popBuffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor);

	QTTESTINGSERVER_EXPORT void pushClientFunc(int id);
	QTTESTINGSERVER_EXPORT int popServerFunc();

	QTTESTINGSERVER_EXPORT void execServerFunc(int id);
	QTTESTINGSERVER_EXPORT ni_ExceptionRef execServerFuncWithExceptions(int id);

	QTTESTINGSERVER_EXPORT void invokeInterfaceMethod(ni_InterfaceMethodRef method, int serverID);
	QTTESTINGSERVER_EXPORT ni_ExceptionRef invokeInterfaceMethodWithExceptions(ni_InterfaceMethodRef method, int serverID);

	QTTESTINGSERVER_EXPORT void pushInstance(int id, bool isClientId);
	QTTESTINGSERVER_EXPORT int popInstance(bool* isClientId);

	QTTESTINGSERVER_EXPORT void pushNull();

	QTTESTINGSERVER_EXPORT void releaseServerResource(int id);

	QTTESTINGSERVER_EXPORT void clearServerSafetyArea(); // clear server resources from safety area (protects values being returned with no other shared_ptrs existing)

	QTTESTINGSERVER_EXPORT void dumpTables();

	QTTESTINGSERVER_EXPORT void setException(ni_ExceptionRef e);
}
