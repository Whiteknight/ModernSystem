namespace ModernSystem.Contracts
{
    public interface IFactory<TTarget>
    {
        TTarget Create();
    }

    public interface IFactory<TTarget, TArg>
    {
        TTarget Create(TArg argument);
    }
}
