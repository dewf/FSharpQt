#include "DirectExport.h"

#include "core/NativeImplCore.h"

int nativeImplInit(
    niClientFuncExec clientFuncExec,
    niClientMethodExec clientMethodExec,
    niClientResourceRelease clientResourceRelease,
    niClientClearSafetyArea clientClearSafetyArea)
{
    return ni_nativeImplInit(
        clientFuncExec,
        clientMethodExec,
        clientResourceRelease,
        clientClearSafetyArea);
}

void nativeImplShutdown()
{
    return ni_nativeImplShutdown();
}

ni_ModuleRef getModule(const char* name)
{
    return ni_getModule(name);
}

ni_ModuleMethodRef getModuleMethod(ni_ModuleRef m, const char* name)
{
    return ni_getModuleMethod(m, name);
}

ni_InterfaceRef getInterface(ni_ModuleRef m, const char* name)
{
    return ni_getInterface(m, name);
}

ni_InterfaceMethodRef getInterfaceMethod(ni_InterfaceRef iface, const char* name)
{
    return ni_getInterfaceMethod(iface, name);
}

ni_ExceptionRef getException(ni_ModuleRef m, const char* name)
{
    return ni_getException(m, name);
}

void pushModuleConstants(ni_ModuleRef m)
{
    ni_pushModuleConstants(m);
}

void invokeModuleMethod(ni_ModuleMethodRef method)
{
    ni_invokeModuleMethod(method);
}

ni_ExceptionRef invokeModuleMethodWithExceptions(ni_ModuleMethodRef method)
{
    return ni_invokeModuleMethodWithExceptions(method);
}

void pushPtr(void* value)
{
    ni_pushPtr(value);
}

void* popPtr()
{
    return ni_popPtr();
}

void pushPtrArray(void** values, size_t count)
{
    ni_pushPtrArray(values, count);
}

void popPtrArray(void*** values, size_t* count)
{
    ni_popPtrArray(values, count);
}

void pushSizeT(size_t value)
{
    ni_pushSizeT(value);
}

size_t popSizeT()
{
    return ni_popSizeT();
}

void pushSizeTArray(size_t* values, size_t count)
{
    ni_pushSizeTArray(values, count);
}

void popSizeTArray(size_t** values, size_t* count)
{
    ni_popSizeTArray(values, count);
}

void pushBool(bool value)
{
    ni_pushBool(value);
}

bool popBool()
{
    return ni_popBool();
}

void pushBoolArray(bool* values, size_t count)
{
    ni_pushBoolArray(values, count);
}

void popBoolArray(bool** values, size_t* count)
{
    ni_popBoolArray(values, count);
}

void pushInt8(int8_t x)
{
    ni_pushInt8(x);
}

int8_t popInt8()
{
    return ni_popInt8();
}

void pushInt8Array(int8_t* values, size_t count)
{
    ni_pushInt8Array(values, count);
}

void popInt8Array(int8_t** values, size_t* count)
{
    ni_popInt8Array(values, count);
}

void pushUInt8(uint8_t x)
{
    ni_pushUInt8(x);
}

uint8_t popUInt8()
{
    return ni_popUInt8();
}

void pushUInt8Array(uint8_t* values, size_t count)
{
    ni_pushUInt8Array(values, count);
}

void popUInt8Array(uint8_t** values, size_t* count)
{
    ni_popUInt8Array(values, count);
}

void pushInt16(int16_t x)
{
    ni_pushInt16(x);
}

int16_t popInt16()
{
    return ni_popInt16();
}

void pushInt16Array(int16_t* values, size_t count)
{
    ni_pushInt16Array(values, count);
}

void popInt16Array(int16_t** values, size_t* count)
{
    ni_popInt16Array(values, count);
}

void pushUInt16(uint16_t x)
{
    ni_pushUInt16(x);
}

uint16_t popUInt16()
{
    return ni_popUInt16();
}

void pushUInt16Array(uint16_t* values, size_t count)
{
    ni_pushUInt16Array(values, count);
}

void popUInt16Array(uint16_t** values, size_t* count)
{
    ni_popUInt16Array(values, count);
}

void pushInt32(int32_t x)
{
    ni_pushInt32(x);
}

int32_t popInt32()
{
    return ni_popInt32();
}

void pushInt32Array(int32_t* values, size_t count)
{
    ni_pushInt32Array(values, count);
}

void popInt32Array(int32_t** values, size_t* count)
{
    ni_popInt32Array(values, count);
}

void pushUInt32(uint32_t x)
{
    ni_pushUInt32(x);
}

uint32_t popUInt32()
{
    return ni_popUInt32();
}

void pushUInt32Array(uint32_t* values, size_t count)
{
    ni_pushUInt32Array(values, count);
}

void popUInt32Array(uint32_t** values, size_t* count)
{
    ni_popUInt32Array(values, count);
}

void pushInt64(int64_t x)
{
    ni_pushInt64(x);
}

int64_t popInt64()
{
    return ni_popInt64();
}

void pushInt64Array(int64_t* values, size_t count)
{
    ni_pushInt64Array(values, count);
}

void popInt64Array(int64_t** values, size_t* count)
{
    ni_popInt64Array(values, count);
}

void pushUInt64(uint64_t x)
{
    ni_pushUInt64(x);
}

uint64_t popUInt64()
{
    return ni_popUInt64();
}

void pushUInt64Array(uint64_t* values, size_t count)
{
    ni_pushUInt64Array(values, count);
}

void popUInt64Array(uint64_t** values, size_t* count)
{
    ni_popUInt64Array(values, count);
}

void pushFloat(float x)
{
    ni_pushFloat(x);
}

float popFloat()
{
    return ni_popFloat();
}

void pushFloatArray(float* values, size_t count)
{
    ni_pushFloatArray(values, count);
}

void popFloatArray(float** values, size_t* count)
{
    ni_popFloatArray(values, count);
}

void pushDouble(double x)
{
    ni_pushDouble(x);
}

double popDouble()
{
    return ni_popDouble();
}

void pushDoubleArray(double* values, size_t count)
{
    ni_pushDoubleArray(values, count);
}

void popDoubleArray(double** values, size_t* count)
{
    ni_popDoubleArray(values, count);
}

void pushString(const char* str, size_t length) {
    ni_pushString(str, length);
}

void popString(const char** strPtr, size_t* length) {
    ni_popString(strPtr, length);
}

void pushStringArray(const char** strs, size_t* lengths, size_t count)
{
    ni_pushStringArray(strs, lengths, count);
}

void popStringArray(const char*** strs, size_t** lengths, size_t* count)
{
    ni_popStringArray(strs, lengths, count);
}

void pushBuffer(int id, bool isClientId, ni_BufferDescriptor* descriptor)
{
    ni_pushBuffer(id, isClientId, descriptor);
}

void popBuffer(int* id, bool* isClientId, ni_BufferDescriptor* descriptor)
{
    ni_popBuffer(id, isClientId, descriptor);
}

void pushClientFunc(int id)
{
    ni_pushClientFunc(id);
}

int popServerFunc()
{
    return ni_popServerFunc();
}

void execServerFunc(int id)
{
    ni_execServerFunc(id);
}

ni_ExceptionRef execServerFuncWithExceptions(int id)
{
    return ni_execServerFuncWithExceptions(id);
}

void invokeInterfaceMethod(ni_InterfaceMethodRef method, int serverID)
{
    ni_invokeInterfaceMethod(method, serverID);
}

ni_ExceptionRef invokeInterfaceMethodWithExceptions(ni_InterfaceMethodRef method, int serverID)
{
    return ni_invokeInterfaceMethodWithExceptions(method, serverID);
}

void pushInstance(int id, bool isClientId)
{
    ni_pushInstance(id, isClientId);
}

int popInstance(bool* isClientID)
{
    return ni_popInstance(isClientID);
}

void pushNull()
{
    ni_pushNull();
}

void releaseServerResource(int id)
{
    ni_releaseServerResource(id);
}

void clearServerSafetyArea()
{
    ni_clearServerSafetyArea();
}

void dumpTables()
{
    ni_dumpTables();
}

void setException(ni_ExceptionRef e)
{
    ni_setException(e);
}
