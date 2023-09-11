using RannaTask.Data.Abstract;
using RannaTask.Data.Concrete.EntityFramework.Contexts;
using RannaTask.Data.Concrete.EntityFramework.Repositories;
namespace RannaTask.Data.Concrete.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RannaTaskContext _context;
        private EfProductRespository _productRespository;

        public UnitOfWork(RannaTaskContext context)
        {
            _context = context;
        }


        public IProductRepository Products => _productRespository ?? new EfProductRespository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
