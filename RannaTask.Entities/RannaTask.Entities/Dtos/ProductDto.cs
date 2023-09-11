using RannaTask.Entities.Concrete;
using RannaTask.Shared.Entities.Abstract;

namespace RannaTask.Entities.Dtos
{
    public class ProductDto : DtoGetBase
    {
        public Product Product { get; set; }
    }
}
