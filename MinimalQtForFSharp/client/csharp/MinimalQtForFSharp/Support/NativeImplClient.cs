using System.Runtime.InteropServices;
using System.Text;

namespace Org.Whatever.MinimalQtForFSharp.Support
{
    internal delegate void MethodWrapper(ClientObject obj);
    internal delegate void ExceptionBuildAndThrowDelegate();
    
    internal static class NativeImplClient
    {
        // these MUST be saved, else GC will take the delegates out from under us. ohnononono!
        private static NativeMethods.ExecFunctionDelegate _savedExecFunctionDelegate;
        private static NativeMethods.ClientMethodDelegate _savedClientMethodDelegate;
        private static NativeMethods.ClientResourceReleaseDelegate _savedClientResourceReleaseDelegate;
        private static NativeMethods.ClientClearSafetyAreaDelegate _savedClientClearSafetyAreaDelegate;

        public static bool RemoteIsShutdown;
        private static readonly Dictionary<IntPtr, MethodWrapper> MethodWrappers = new();
        public static readonly Dictionary<IntPtr, ExceptionBuildAndThrowDelegate> ExceptionBuildAndThrowDelegates = new();

        public static void PushClientFuncVal(FuncValWrapper wrapper, IntPtr uniqueKey)
        {
            var funcVal = ClientFuncVal.AddFuncVal(wrapper, uniqueKey);
            ((IPushable)funcVal).Push(false); // isReturn = false (more correctly, "not applicable") - client resources don't need to be protected on return
        }
        
        private static void FuncValExecutor(int id)
        {
            ((ClientFuncVal)ClientResource.GetResourceById(id)).Wrapper();
        }
        
        private static void ReleaseFuncVal(int id)
        {
            ClientResource.GetResourceById(id).Release();
        }
        
        public static int Init()
        {
            // delegates must be saved from GC until the end of the program, kthx
            _savedExecFunctionDelegate = FuncValExecutor;
            _savedClientMethodDelegate = InvokeClientMethod;
            _savedClientResourceReleaseDelegate = ReleaseClientResourceId;
            _savedClientClearSafetyAreaDelegate = ClientClearSafetyArea;

            return NativeMethods.nativeImplInit(
                _savedExecFunctionDelegate,
                _savedClientMethodDelegate,
                _savedClientResourceReleaseDelegate,
                _savedClientClearSafetyAreaDelegate
                );
        }

        public static void Shutdown()
        {
            RemoteIsShutdown = true; // don't free any remote delegates after this!
            Console.WriteLine("C# NativeImplClient Shutdown (before native)");
            NativeMethods.nativeImplShutdown();
            Console.WriteLine("C# NativeImplClient Shutdown (after native)");
        }

        public static ModuleHandle GetModule(string name)
        {
            return new ModuleHandle(NativeMethods.getModule(name));
        }

        public static ModuleMethodHandle GetModuleMethod(ModuleHandle module, string name)
        {
            return new ModuleMethodHandle(NativeMethods.getModuleMethod(module.Handle, name));
        }

        public static InterfaceHandle GetInterface(ModuleHandle module, string name)
        {
            return new InterfaceHandle(NativeMethods.getInterface(module.Handle, name));
        }

        public static InterfaceMethodHandle GetInterfaceMethod(InterfaceHandle iface, string name)
        {
            return new InterfaceMethodHandle(NativeMethods.getInterfaceMethod(iface.Handle, name));
        }

        public static ExceptionHandle GetException(ModuleHandle module, string name)
        {
            return new ExceptionHandle(NativeMethods.getException(module.Handle, name));
        }

        public static void PushModuleConstants(ModuleHandle module)
        {
            NativeMethods.pushModuleConstants(module.Handle);
        }

        public static void InvokeModuleMethod(ModuleMethodHandle method)
        {
            NativeMethods.invokeModuleMethod(method.Handle);
        }

        public static void InvokeModuleMethodWithExceptions(ModuleMethodHandle method)
        {
            var exceptionHandle = NativeMethods.invokeModuleMethodWithExceptions(method.Handle);
            if (exceptionHandle != IntPtr.Zero)
            {
                ExceptionBuildAndThrowDelegates[exceptionHandle]();
            }
        }

        public static void PushPtr(IntPtr value)
        {
            NativeMethods.pushPtr(value);
        }
        
        public static IntPtr PopPtr()
        {
            return NativeMethods.popPtr();
        }
        
        public static void PushPtrArray(IntPtr[] values)
        {
            NativeMethods.pushPtrArray(values, (IntPtr)values.Length);
        }
        
        public static IntPtr[] PopPtrArray()
        {
            NativeMethods.popPtrArray(out var valuesPtr, out var count);
            var ret = new IntPtr[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }
        
        public static void PushSizeT(IntPtr value)
        {
            NativeMethods.pushSizeT(value);
        }

        public static IntPtr PopSizeT()
        {
            return NativeMethods.popSizeT();
        }

        public static void PushSizeTArray(IntPtr[] values)
        {
            NativeMethods.pushSizeTArray(values, (IntPtr)values.Length);
        }

        public static IntPtr[] PopSizeTArray()
        {
            NativeMethods.popSizeTArray(out var valuesPtr, out var count);
            var ret = new IntPtr[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }

        public static void PushBool(bool b)
        {
            NativeMethods.pushBool(b);
        }

        public static bool PopBool()
        {
            return NativeMethods.popBool();
        }

        public static void PushBoolArray(bool[] values)
        {
            var bytes = values.Select(b => (byte)(b ? 1 : 0)).ToArray();
            NativeMethods.pushBoolArray(bytes, (IntPtr)values.Length);
        }

        public static bool[] PopBoolArray()
        {
            NativeMethods.popBoolArray(out var valuesPtr, out var count);
            var ret = new byte[(int)count]; // bool[] won't work for the Copy below
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret.Select(v => v != 0).ToArray();
        }
        
        // INT8 =========================================
        public static void PushInt8(sbyte x)
        {
            NativeMethods.pushInt8(x);
        }

        public static sbyte PopInt8()
        {
            return NativeMethods.popInt8();
        }

        public static void PushInt8Array(sbyte[] values)
        {
            NativeMethods.pushInt8Array(values, (IntPtr)values.Length);
        }

        public static sbyte[] PopInt8Array()
        {
            NativeMethods.popInt8Array(out var valuesPtr, out var count);
            var ret = new sbyte[(int)count];
            if (count != IntPtr.Zero)
            {
                var temp = new byte[(int)count];
                Marshal.Copy(valuesPtr, temp, 0, (int)count);
                Buffer.BlockCopy(temp, 0, ret, 0, (int)count);
            }
            return ret;
        }
        
        public static void PushUInt8(byte x)
        {
            NativeMethods.pushUInt8(x);
        }

        public static byte PopUInt8()
        {
            return NativeMethods.popUInt8();
        }

        public static void PushUInt8Array(byte[] values)
        {
            NativeMethods.pushUInt8Array(values, (IntPtr)values.Length);
        }

        public static byte[] PopUInt8Array()
        {
            NativeMethods.popUInt8Array(out var valuesPtr, out var count);
            var ret = new byte[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }
        
        // INT16 =========================================
        public static void PushInt16(short x)
        {
            NativeMethods.pushInt16(x);
        }

        public static short PopInt16()
        {
            return NativeMethods.popInt16();
        }

        public static void PushInt16Array(short[] values)
        {
            NativeMethods.pushInt16Array(values, (IntPtr)values.Length);
        }

        public static short[] PopInt16Array()
        {
            NativeMethods.popInt16Array(out var valuesPtr, out var count);
            var ret = new short[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }
        
        public static void PushUInt16(ushort x)
        {
            NativeMethods.pushUInt16(x);
        }

        public static ushort PopUInt16()
        {
            return NativeMethods.popUInt16();
        }

        public static void PushUInt16Array(ushort[] values)
        {
            NativeMethods.pushUInt16Array(values, (IntPtr)values.Length);
        }

        public static ushort[] PopUInt16Array()
        {
            NativeMethods.popUInt16Array(out var valuesPtr, out var count);
            var ret = new ushort[(int)count];
            if (count != IntPtr.Zero)
            {
                var temp = new short[(int)count];
                Marshal.Copy(valuesPtr, temp, 0, (int)count);
                Buffer.BlockCopy(temp, 0, ret, 0, (int)count);
            }
            return ret;
        }
        
        // INT32 =========================================
        public static void PushInt32(int x)
        {
            NativeMethods.pushInt32(x);
        }

        public static int PopInt32()
        {
            return NativeMethods.popInt32();
        }

        public static void PushInt32Array(int[] values)
        {
            NativeMethods.pushInt32Array(values, (IntPtr)values.Length);
        }

        public static int[] PopInt32Array()
        {
            NativeMethods.popInt32Array(out var valuesPtr, out var count);
            var ret = new int[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }
        
        public static void PushUInt32(uint x)
        {
            NativeMethods.pushUInt32(x);
        }

        public static uint PopUInt32()
        {
            return NativeMethods.popUInt32();
        }

        public static void PushUInt32Array(uint[] values)
        {
            NativeMethods.pushUInt32Array(values, (IntPtr)values.Length);
        }

        public static uint[] PopUInt32Array()
        {
            NativeMethods.popUInt32Array(out var valuesPtr, out var count);
            var ret = new uint[(int)count];
            if (count != IntPtr.Zero)
            {
                var temp = new int[(int)count];
                Marshal.Copy(valuesPtr, temp, 0, (int)count);
                Buffer.BlockCopy(temp, 0, ret, 0, (int)count);
            }
            return ret;
        }
        
        // INT64 =========================================
        public static void PushInt64(long x)
        {
            NativeMethods.pushInt64(x);
        }

        public static long PopInt64()
        {
            return NativeMethods.popInt64();
        }

        public static void PushInt64Array(long[] values)
        {
            NativeMethods.pushInt64Array(values, (IntPtr)values.Length);
        }

        public static long[] PopInt64Array()
        {
            NativeMethods.popInt64Array(out var valuesPtr, out var count);
            var ret = new long[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }
        
        public static void PushUInt64(ulong x)
        {
            NativeMethods.pushUInt64(x);
        }

        public static ulong PopUInt64()
        {
            return NativeMethods.popUInt64();
        }

        public static void PushUInt64Array(ulong[] values)
        {
            NativeMethods.pushUInt64Array(values, (IntPtr)values.Length);
        }

        public static ulong[] PopUInt64Array()
        {
            NativeMethods.popUInt64Array(out var valuesPtr, out var count);
            var ret = new ulong[(int)count];
            if (count != IntPtr.Zero)
            {
                var temp = new long[(int)count];
                Marshal.Copy(valuesPtr, temp, 0, (int)count);
                Buffer.BlockCopy(temp, 0, ret, 0, (int)count);
            }
            return ret;
        }
        
        // ===========================
        
        public static void PushFloat(float x)
        {
            NativeMethods.pushFloat(x);
        }

        public static float PopFloat()
        {
            return NativeMethods.popFloat();
        }

        public static void PushFloatArray(float[] values)
        {
            NativeMethods.pushFloatArray(values, (IntPtr)values.Length);
        }

        public static float[] PopFloatArray()
        {
            NativeMethods.popFloatArray(out var valuesPtr, out var count);
            var ret = new float[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }
        
        public static void PushDouble(double x)
        {
            NativeMethods.pushDouble(x);
        }
        
        public static double PopDouble()
        {
            return NativeMethods.popDouble();
        }
        
        public static void PushDoubleArray(double[] values)
        {
            NativeMethods.pushDoubleArray(values, (IntPtr)values.Length);
        }
        
        public static double[] PopDoubleArray()
        {
            NativeMethods.popDoubleArray(out var valuesPtr, out var count);
            var ret = new double[(int)count];
            if (count != IntPtr.Zero)
            {
                Marshal.Copy(valuesPtr, ret, 0, (int)count);
            }
            return ret;
        }

        public static void PushString(string str)
        {
            if (str.Length > 0)
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                var ptr = Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, ptr, bytes.Length);
                NativeMethods.pushString(ptr, bytes.Length);
                Marshal.FreeHGlobal(ptr);
            }
            else
            {
                NativeMethods.pushString(0, 0);
            }
        }

        public static string PopString()
        {
            NativeMethods.popString(out var ptr, out var length);
            return length > 0 ? Marshal.PtrToStringUTF8(ptr, (int)length) : "";
        }

        public static void PushStringArray(string[] strs)
        {
            var pointers = new IntPtr[strs.Length];
            var lengths = new IntPtr[strs.Length];
            for (var i = 0; i < strs.Length; i++)
            {
                if (strs[i].Length > 0)
                {
                    var bytes = Encoding.UTF8.GetBytes(strs[i]);
                    var ptr = Marshal.AllocHGlobal(bytes.Length);
                    Marshal.Copy(bytes, 0, ptr, bytes.Length);
                    pointers[i] = ptr;
                    lengths[i] = bytes.Length;
                }
                else
                {
                    pointers[i] = 0;
                    lengths[i] = 0;
                }
            }
            NativeMethods.pushStringArray(pointers, lengths, strs.Length);
            foreach (var p in pointers)
            {
                if (p != 0)
                {
                    Marshal.FreeHGlobal(p);
                }
            }
        }

        public static string[] PopStringArray()
        {
            NativeMethods.popStringArray(out var ptrsPtr, out var lengthsPtr, out var count);
            var intCount = (int)count;
            var ptrs = new IntPtr[intCount];
            var lengths = new IntPtr[intCount];
            Marshal.Copy(ptrsPtr, ptrs, 0, intCount);
            Marshal.Copy(lengthsPtr, lengths, 0, intCount);
            var result = new string[intCount];
            for (var i = 0; i < intCount; i++)
            {
                if (lengths[i] > 0)
                {
                    result[i] = Marshal.PtrToStringUTF8(ptrs[i], (int)lengths[i]);
                }
                else
                {
                    result[i] = "";
                }
            }
            return result;
        }
        
        public static int PopServerFuncValId()
        {
            return NativeMethods.popServerFunc();
        }

        public static void PushNull()
        {
            NativeMethods.pushNull();
        }

        public static void PopInstanceId(out int id, out bool isClientId)
        {
            id = NativeMethods.popInstance(out isClientId);
        }

        public static void InvokeInterfaceMethod(InterfaceMethodHandle method, int serverId)
        {
            NativeMethods.invokeInterfaceMethod(method.Handle, serverId);
        }

        public static void InvokeInterfaceMethodWithExceptions(InterfaceMethodHandle method, int serverId)
        {
            var exceptionHandle = NativeMethods.invokeInterfaceMethodWithExceptions(method.Handle, serverId);
            if (exceptionHandle != IntPtr.Zero)
            {
                ExceptionBuildAndThrowDelegates[exceptionHandle]();
            }
        }

        public static void SetClientMethodWrapper(InterfaceMethodHandle method, MethodWrapper wrapper)
        {
            MethodWrappers[method.Handle] = wrapper;
        }

        // these two need to be passed to remote
        private static void ReleaseClientResourceId(int id) // called from remote
        {
            // should we check for _remoteIsShutdown here?
            ClientResource.GetResourceById(id).Release();
        }
        
        private static void InvokeClientMethod(IntPtr methodHandle, int clientId)
        {
            var obj = ClientObject.GetById(clientId);
            MethodWrappers[methodHandle](obj);
        }

        // call this after receiving an interface (maybe even a function, eventually?) as a return value
        public static void ServerClearSafetyArea()
        {
            // TODO: in the future, perhaps check to see if the remote safety area is 'dirty' -- we'll know from the flags returned on popInstace()
            // (have given that some thought and will probably go in the direction of not even having the 'inSafetyArea' flag on resource pops,
            // could potentially require lots of branching for return types with lots of resource refs, is it really worth it compared to the unconditional call?
            NativeMethods.clearServerSafetyArea();
        }

        private static void ClientClearSafetyArea()
        {
            Console.WriteLine("C#: before ClientClearSafetyArea");
            ServerResource.ClearSafeArea();
            Console.WriteLine("C#: after ClientClearSafetyArea");
        }
        
        public static void DumpTables()
        {
            Console.WriteLine("============Dump Tables==================");
            Console.WriteLine("Native tables----------------------------");
            NativeMethods.dumpTables();
            
            Console.WriteLine("Local tables-----------------------------");
            
            Console.WriteLine("== C# client resources:");
            ClientResource.DumpTables();
            
            Console.WriteLine("=========================================");
        }
        
        public static void SetExceptionBuilder(ExceptionHandle eh, ExceptionBuildAndThrowDelegate builder)
        {
            ExceptionBuildAndThrowDelegates[eh.Handle] = builder;
        }
        
        public static void SetException(ExceptionHandle eh)
        {
            NativeMethods.setException(eh.Handle);
        }
    }
}
