namespace Org.Whatever.MinimalQtForFSharp.Support
{
    internal class ServerBuffer<T> : ServerResource, INativeBuffer<T>
    {
        private NativeMethods.BufferDescriptor _desc;
        
        public ServerBuffer(int id, NativeMethods.BufferDescriptor desc) : base(id)
        {
            _desc = desc;
        }
        
        protected override void NativePush()
        {
            NativeMethods.pushBuffer(Id, false, ref _desc);
        }

        public void Dispose()
        {
            ServerDispose();
        }

        public Span<T> GetSpan(out int length)
        {
            unsafe
            {
                var ptr = _desc.Start.ToPointer();
                length = (int)_desc.TotalCount;
                return new Span<T>(ptr, length);
            }
        }
    }
}
