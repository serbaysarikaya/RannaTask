using RannaTask.Entities.Dtos;

namespace RannaTask.MVC.Areas.Admin.Models
{
    public class ProductAddAjaxViewModel
    {
        public ProductAddDto ProductAddDto { get; set; }
        public string ProductAddPartial { get; set; }
        public ProductDto ProductDto { get; set; }

    }
}
