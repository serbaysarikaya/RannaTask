using Microsoft.EntityFrameworkCore;
using RannaTask.Data.Abstract;
using RannaTask.Data.Concrete.EntityFramework.Contexts;
using RannaTask.Entities.Concrete;
using RannaTask.Shared.Data.Concrete.EntityFramework;

namespace RannaTask.Data.Concrete.EntityFramework.Repositories
{
    public class EfProductRespository : EfEntityRepositoryBase<Product>, IProductRepository
    {
        public EfProductRespository(DbContext context) : base(context)
        {
        }

        public async Task<Product> GetById(int productId)
        {
            return await RannaTaskContext.Products.SingleOrDefaultAsync(c => c.Id == productId);
        }
        private RannaTaskContext RannaTaskContext
        {
            get
            {
                return _context as RannaTaskContext;
            }
        }
    }
}
