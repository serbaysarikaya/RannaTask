using RannaTask.Entities.Concrete;
using RannaTask.Shared.Data.Abstract;

namespace RannaTask.Data.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
    }
}
