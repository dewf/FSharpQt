namespace Org.Whatever.MinimalQtForFSharp.Support
{
    internal delegate void FuncValWrapper();
    
    internal class ClientFuncVal : ClientResource
    {
        private static readonly Dictionary<IntPtr, int> ReverseFuncValDict = new ();
        public readonly FuncValWrapper Wrapper;
        private readonly IntPtr _key;
        
        private ClientFuncVal(FuncValWrapper wrapper, IntPtr key)
        {
            Wrapper = wrapper;
            _key = key;
        }
        
        protected override void NativePush()
        {
            NativeMethods.pushClientFunc(Id);
            // NOW we can put in the Reverse dictionary! we didn't have the ID until this point ...
            ReverseFuncValDict[_key] = Id;
        }
        
        protected override void ReleaseExtra()
        {
            ReverseFuncValDict.Remove(_key);
        }
        
        public static ClientFuncVal AddFuncVal(FuncValWrapper wrapper, IntPtr uniqueKey)
        {
            Console.WriteLine($"Func Val Unique Key: {uniqueKey}");
            if (ReverseFuncValDict.TryGetValue(uniqueKey, out var id))
            {
                // return existing
                return (ClientFuncVal)GetResourceById(id);
            }
            // else create anew
            // note, we still need to add to reverse dictionary in NativePush, because we don't yet have the generated .Id to do so
            return new ClientFuncVal(wrapper, uniqueKey);
        }
    }
}
