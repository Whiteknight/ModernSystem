namespace ModernSystem.Contracts
{

    public interface ITransformer<TSource, TTarget>
    {
        TTarget Transform(TSource source);
    }
}
