namespace Org.Whatever.MinimalQtForFSharp.Support
{
    internal class ServerFuncVal : ServerResource
    {
        public ServerFuncVal(int id) : base(id)
        {
        }
        
        protected override void NativePush()
        {
            // ServerFuncs currently will never be pushed - we don't support "short-circuiting" of function values,
            // they are always wrapped afresh from each side (so a server function returned from our side,
            // becomes a client function - use a single method interface for that kind of thing)
            throw new NotImplementedException();
        }
        
        public void Exec()
        {
            NativeMethods.execServerFunc(Id);
        }
        
        public void ExecWithExceptions()
        {
            var exceptionHandle = NativeMethods.execServerFuncWithExceptions(Id);
            if (exceptionHandle != IntPtr.Zero)
            {
                NativeImplClient.ExceptionBuildAndThrowDelegates[exceptionHandle]();
            }
        }
    }
}
