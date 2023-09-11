using RannaTask.Entities.Dtos;
using RannaTask.Shared.Utilities.Results.Abstract;

namespace RannaTask.Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<ProductDto>> GetAsync(int productId);
        Task<IDataResult<ProductListDto>> GetAllAsync();
        Task<IDataResult<ProductListDto>> GetAllByNonDeleteAsync();
        Task<IResult> AddAsync(ProductAddDto productAddDto);
        Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<IDataResult<ProductDto>> DeleteAsync(int productId);
        Task<IResult> HardDeleteAsync(int productId);
        Task<IDataResult<ProductUpdateDto>> GetProductUpdateDtoAsync(int productId);




        //Task<IDataResult<int>> CountAsync();
        //Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}
