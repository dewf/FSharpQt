namespace Org.Whatever.MinimalQtForFSharp.Support
{
    internal abstract class ServerResource : IPushable
    {
        private static readonly List<ServerResource> SafeArea = new(); // place to stash server objects until the other side has finished popping them from stack (in case they only exist as a return value)
        public static void ClearSafeArea()
        {
            SafeArea.Clear();
        }
        
        protected readonly int Id;
        private bool _serverDisposed;
        protected ServerResource(int id)
        {
            Id = id;
        }

        ~ServerResource()
        {
            // backstop in case the derived class not manually disposed
            Console.WriteLine($"ServerResource id {Id} - finalized invoked, calling ServerDispose()");
            ServerDispose();
        }

        protected abstract void NativePush();

        protected virtual void ReleaseExtra()
        {
            // override this if there's anything else we need to do on ServerDispose()
        }

        protected void ServerDispose()
        {
            if (!_serverDisposed)
            {
                ReleaseExtra();
                if (!NativeImplClient.RemoteIsShutdown)
                {
                    NativeMethods.releaseServerResource(Id);
                }
                _serverDisposed = true;
            }
        }

        void IPushable.Push(bool isReturn)
        {
            if (isReturn)
            {
                // save this object to the stack safety area, until the other side has pulled it off
                // otherwise, if this is the last proxy on this side,
                // could remotely detonate the server's real copy before it's done taking 'ownership'
                // (dependent on probably-unlikely GC collection, but better safe than sorry)
                SafeArea.Add(this);
            }
            NativePush();
        }
    }
}