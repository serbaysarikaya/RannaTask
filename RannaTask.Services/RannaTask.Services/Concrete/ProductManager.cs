using AutoMapper;
using RannaTask.Data.Abstract;
using RannaTask.Entities.Concrete;
using RannaTask.Entities.Dtos;
using RannaTask.Services.Abstract;
using RannaTask.Services.Utilities;
using RannaTask.Shared.Utilities.Results.Abstract;
using RannaTask.Shared.Utilities.Results.ComplexTypes;
using RannaTask.Shared.Utilities.Results.Concrete;
using static RannaTask.Services.Utilities.Messages;

namespace RannaTask.Services.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> AddAsync(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.MessagesProduct.Add(product.Code));
        }



        public async Task<IDataResult<ProductListDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GelAllAsync();
            if (products.Count > -1)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, Messages.MessagesProduct.NotFound(isPlural: true), null);

        }

        public async Task<IDataResult<ProductListDto>> GetAllByNonDeleteAsync()
        {
            var products = await _unitOfWork.Products.GelAllAsync(p => p.IsDeleted != false);
            if (products.Count > -1)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, Messages.MessagesProduct.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ProductDto>> GetAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
            if (product != null)
            {
                return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
                {
                    Product = product,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProductDto>(ResultStatus.Error, Messages.MessagesProduct.NotFound(isPlural: false), null);

        }

        public async Task<IResult> HardDeleteAsync(int productId)
        {
            var result = await _unitOfWork.Products.AnyAsync(p => p.Id == productId);
            if (result)
            {
                var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);

                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.MessagesProduct.Update(product.Name));


            }

            return new Result(ResultStatus.Error, Messages.MessagesProduct.NotFound(isPlural: false));

        }


        public async Task<IDataResult<ProductDto>> DeleteAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
            if (product != null)
            {
                product.IsDeleted = true;

                product.ModifiedDate = DateTime.Now;
                var deletedProduct = await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProductDto>(ResultStatus.Success, Messages.MessagesProduct.Delete(deletedProduct.Code), new ProductDto
                {
                    Product = deletedProduct,
                    ResultStatus = ResultStatus.Success,
                    Message = Messages.MessagesProduct.Delete(deletedProduct.Code)
                });
            }
            return new DataResult<ProductDto>(ResultStatus.Error, Messages.MessagesProduct.NotFound(isPlural: false), new ProductDto
            {
                Product = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.MessagesProduct.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var oldProduct = await _unitOfWork.Products.GetAsync(p => p.Id == productUpdateDto.Id);
            var product = _mapper.Map<ProductUpdateDto, Product>(productUpdateDto, oldProduct);
            var updatedProduct = await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProductDto>(ResultStatus.Success, Messages.MessagesProduct.Update(updatedProduct.Name), new ProductDto
            {
                Product = updatedProduct,
                ResultStatus = ResultStatus.Success,
                Message = MessagesProduct.Update(updatedProduct.Name)
            });
        }

        public async Task<IDataResult<ProductUpdateDto>> GetProductUpdateDtoAsync(int productId)
        {
            var result = await _unitOfWork.Products.AnyAsync(p => p.Id == productId);
            if (result)
            {
                var product = await _unitOfWork.Products.GetAsync(p => p.Id == productId);
                var productUpdateDto = _mapper.Map<ProductUpdateDto>(product);
                return new DataResult<ProductUpdateDto>(ResultStatus.Success, productUpdateDto);
            }
            else
                return new DataResult<ProductUpdateDto>(ResultStatus.Error, Messages.MessagesProduct.NotFound(isPlural: false), null);


        }
    }
}
