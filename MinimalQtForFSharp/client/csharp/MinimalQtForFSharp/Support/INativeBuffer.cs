namespace Org.Whatever.MinimalQtForFSharp.Support
{
    public interface INativeBuffer<T> : IDisposable
    {
        public Span<T> GetSpan(out int length);
    }
}
