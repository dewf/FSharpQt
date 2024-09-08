using System.Runtime.InteropServices;

namespace Org.Whatever.MinimalQtForFSharp.Support
{
    public class ClientBuffer<T> : ClientResource, INativeBuffer<T>
    {
        private IntPtr _block;
        private bool _disposed;
        private readonly int _elementSize;
        private readonly int[] _dims;
        private readonly long _totalCount;
        private readonly long _totalSize;
        public ClientBuffer(params int[] dims)
        {
            _dims = dims;
            _elementSize = Marshal.SizeOf<T>();
            _totalCount = dims.Aggregate(1L, (product, i) => product * i);
            _totalSize = _totalCount * _elementSize;
            _block = Marshal.AllocHGlobal((IntPtr)_totalSize);
            GC.AddMemoryPressure(_totalSize);
            Console.WriteLine("ClientBuffer {0} allocated", _block);
        }
        
        public Span<T> GetSpan(out int length)
        {
            unsafe
            {
                var ptr = _block.ToPointer();
                length = (int)_totalCount;
                return new Span<T>(ptr, length);
            }
        }
        
        protected override void NativePush()
        {
            NativeMethods.BufferDescriptor descriptor;
            descriptor.Start = _block;
            descriptor.ElementSize = _elementSize;
            descriptor.TotalCount = (IntPtr)_totalCount;
            descriptor.TotalSize = (IntPtr)_totalSize;
            NativeMethods.pushBuffer(Id, true, ref descriptor);
        }

        ~ClientBuffer()
        {
            if (!_disposed) Dispose();
        }
        
        public void Dispose()
        {
            if (_disposed) return;
            Marshal.FreeHGlobal(_block);
            GC.RemoveMemoryPressure(_totalSize);
            Console.WriteLine("ClientBuffer {0} disposed", _block);
            _block = IntPtr.Zero;
            _disposed = true;
        }

        public static ClientBuffer<T> GetById(int id)
        {
            return (ClientBuffer<T>)GetResourceById(id);
        }
    }
}
