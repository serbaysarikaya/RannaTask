using RannaTask.Entities.Dtos;
using RannaTask.Shared.Utilities.Results.Abstract;

namespace Blog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<ImageUploadedDto>> UploadProductImage(string productCode, IFormFile pictureFile, string folderName = "productImages");
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
