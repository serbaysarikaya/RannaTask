using RannaTask.Entities.Concrete;
using RannaTask.Shared.Entities.Abstract;

namespace RannaTask.Entities.Dtos
{
    public class ProductListDto : DtoGetBase
    {
        public IList<Product> Products { get; set; }
    }
}
