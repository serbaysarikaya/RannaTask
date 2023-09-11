using RannaTask.Entities.Dtos;

namespace RannaTask.MVC.Areas.Admin.Models
{
    public class ProductUpdateAjaxViewModel
    {
        public ProductUpdateDto ProductUpdateDto { get; set; }
        public string ProductUpdatePartial { get; set; }
        public ProductDto ProductDto { get; set; }
    }
}
