namespace Org.Whatever.MinimalQtForFSharp.Support
{
    public abstract class ClientResource : IPushable
    {
        private static int _nextClientResourceId = 1;
        private static readonly Dictionary<int, ClientResource> ClientResources = new();

        private int? _id;
        internal int Id
        {
            get
            {
                if (!_id.HasValue)
                {
                    throw new Exception("ClientResource: attempted to use .Id before it has been set!");
                }
                return _id.Value;
            }
        }
        private int RefCount { get; set; }
        
        protected abstract void NativePush();
        protected virtual void ReleaseExtra()
        {
            // anything else you might want to do here, when refcount = 0? (used by ClientFunc to remove from reverse dict)
        }

        void IPushable.Push(bool isReturn) // isReturn unused here
        {
            if (!_id.HasValue)
            {
                // unset as yet
                _id = _nextClientResourceId++;
                RefCount = 1;
                ClientResources[Id] = this;
                Console.WriteLine($"C# ClientObject: pushed {Id}");
            }
            else
            {
                // just increase refcount as we pass it over
                RefCount++;
                Console.WriteLine($"C# ClientObject: increased refcount to {RefCount}");
            }
            NativePush();
        }

        public void Release()
        {
            RefCount -= 1;
            if (RefCount == 0)
            {
                Console.WriteLine($"C# Release on ClientResource {Id} - refCount 0, removing from clientObjects table");
                ClientResources.Remove(Id);
                _id = null; // reset ID, since we're not in the object table anymore
                // all this does is remove this object from the 'checked-out' table -
                // things we were keeping alive as long as the remote (server) side had reference to them
                // but that doesn't mean we're ready to dispose it - might still be useful to us on this side!
                
                ReleaseExtra(); // currently just for clientfuncval, which needs to remove the reverse mapping
            }
        }
        
        public static ClientResource GetResourceById(int id)
        {
            return ClientResources[id];
        }
        
        public static void DumpTables()
        {
            foreach (var (key, value) in ClientResources)
            {
                Console.WriteLine($" - obj {value.Id}: refcount {value.RefCount}");
            }
        }
    }
}
