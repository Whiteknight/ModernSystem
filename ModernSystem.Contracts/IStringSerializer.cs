namespace ModernSystem.Contracts
{
    public interface IStringSerializer<T>
    {
        string Serialize(T dataObject);
        T Deserialize(string serialized);
    }
}