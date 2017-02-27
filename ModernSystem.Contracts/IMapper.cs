namespace ModernSystem.Contracts
{
    public interface IMapper<TSource, TTarget>
    {
        void Map(TSource source, TTarget target);
    }
}
