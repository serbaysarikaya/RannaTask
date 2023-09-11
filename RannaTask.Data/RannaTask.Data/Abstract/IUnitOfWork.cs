namespace RannaTask.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IProductRepository Products { get; }
        Task<int> SaveAsync();
    }
}
