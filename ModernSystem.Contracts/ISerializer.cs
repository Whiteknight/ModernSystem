namespace ModernSystem.Contracts
{
    public interface ISerializer<T>
    {
        byte[] Serialize(T dataObject);
        T Deserialize(byte[] bytes, int start, int length);
        T Deserialize(byte[] bytes);
    }
}
