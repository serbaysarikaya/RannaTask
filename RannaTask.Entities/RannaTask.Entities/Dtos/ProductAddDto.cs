using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RannaTask.Entities.Dtos
{
    public class ProductAddDto
    {
        [DisplayName("Ürün Kodu")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(255, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(2, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string Code { get; set; }

        [DisplayName("Ürün Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(255, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(2, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string Name { get; set; }

        [DisplayName("Fiyat")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public decimal Price { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "Lütfen, bir {0} seçiniz.")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Picture { get; set; } = string.Empty;
    }
}
