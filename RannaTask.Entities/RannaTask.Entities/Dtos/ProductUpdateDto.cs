using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RannaTask.Entities.Dtos
{
    public class ProductUpdateDto
    {
        [Required]
        [DisplayName("Id")]
        public int Id { get; set; }


        [DisplayName("ProductCode")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(255, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string Code { get; set; }

        [DisplayName("ProductName")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(255, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalı")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalı")]
        public string Name { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public decimal Price { get; set; }


        [DisplayName("Silindi mi?")]
        [Required(ErrorMessage = "{0} Boş geçilmemelidir.")]
        public bool IsDeleted { get; set; }

        [DisplayName("Resim")]

        [DataType(DataType.Upload)]
        public IFormFile? PictureFile { get; set; }

        public string Picture { get; set; }
    }
}

