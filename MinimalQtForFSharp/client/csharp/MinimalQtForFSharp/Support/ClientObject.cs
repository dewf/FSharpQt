namespace Org.Whatever.MinimalQtForFSharp.Support;

internal abstract class ClientObject : ClientResource
{
    protected override void NativePush()
    {
        NativeMethods.pushInstance(Id, true);
    }
    public static ClientObject GetById(int id)
    {
        return (ClientObject)GetResourceById(id);
    }
}

internal class ClientInterfaceWrapper<T> : ClientObject
{
    public T RawInterface { get; }
    internal ClientInterfaceWrapper(T rawInterface)
    {
        RawInterface = rawInterface;
    }
}
