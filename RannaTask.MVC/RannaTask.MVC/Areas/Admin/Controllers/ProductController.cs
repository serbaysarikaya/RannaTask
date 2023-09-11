using AutoMapper;
using Blog.Mvc.Helpers.Abstract;
using Microsoft.AspNetCore.Mvc;
using RannaTask.Entities.Concrete;
using RannaTask.Entities.Dtos;
using RannaTask.MVC.Areas.Admin.Models;
using RannaTask.Services.Abstract;
using RannaTask.Services.Concrete;
using RannaTask.Shared.Utilities.Extenstion;
using RannaTask.Shared.Utilities.Results.ComplexTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RannaTask.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IImageHelper _imageHelper;
        private readonly IMapper _mapper;
        private readonly ProductManager _productManager;

        public ProductController(IImageHelper imageHelper, IMapper mapper, IProductService productService, ProductManager productManager)
        {
            _imageHelper = imageHelper;
            _mapper = mapper;
            _productService = productService;
            _productManager = productManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllByNonDeleteAsync();
            return View(result.Data);
        }

        public async Task<JsonResult> GetAllProducts()
        {
            var result = await _productService.GetAllByNonDeleteAsync();
            var products = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_ProductAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            if (!ModelState.IsValid || productAddDto.PictureFile == null)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Count > 0)
                    {
                        foreach (var error in state.Errors)
                        {
                            var errorMessage = error.ErrorMessage;
                            var exception = error.Exception;
                        }
                    }
                }
                var productAddAjaxErrorModel = JsonSerializer.Serialize(new ProductAddAjaxViewModel
                {
                    ProductAddPartial = await this.RenderViewToStringAsync("_ProductAddPartial", productAddDto)
                });
                return Json(productAddAjaxErrorModel);
            }
            else
            {
                var uploadedImgDtoResult = await _imageHelper.UploadProductImage(productAddDto.Code, productAddDto.PictureFile);
                productAddDto.Picture = uploadedImgDtoResult.ResultStatus == ResultStatus.Success ? uploadedImgDtoResult.Data.FullName : "productImages/defaultProduct.png";
                var product = _mapper.Map<Product>(productAddDto);
                await _productService.AddAsync(productAddDto);

                var productAddAjaxModel = JsonSerializer.Serialize(new ProductAddAjaxViewModel
                {
                    ProductDto = new ProductDto
                    {
                        ResultStatus = ResultStatus.Success,
                        Message = $"{product.Code} ürün eklenmiştir"
                    },
                    ProductAddPartial = await this.RenderViewToStringAsync("_ProductAddPartial", productAddDto)
                });
                return Json(productAddAjaxModel);
            }
        }

        public async Task<JsonResult> Delete(int productId)
        {
            var result = await _productService.DeleteAsync(productId);
            var deleteProduct = JsonSerializer.Serialize(result.Data);
            return Json(deleteProduct);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int productId)
        {
            var result = await _productService.GetProductUpdateDtoAsync(productId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_ProductUpdatePartial", result.Data);
            }
            else
                return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            //var Old = await _productService.GetAsync(p => p.Id == productUpdateDto.Id);
            var product = _mapper.Map<Product>(productUpdateDto);
            var result = await _productService.UpdateAsync(productUpdateDto);
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;

                // Eski resim yolunu alın
                var oldProductPicture = product.Picture;

                if (productUpdateDto.PictureFile != null)
                {
                    var uploadedImageDtoResult = await _imageHelper.UploadProductImage(productUpdateDto.Code, productUpdateDto.PictureFile);
                    productUpdateDto.Picture = uploadedImageDtoResult.ResultStatus == ResultStatus.Success ? uploadedImageDtoResult.Data.FullName : "productImages/defaultProduct.png";

                    // Eski resim yolunu karşılaştırın
                    if (!string.IsNullOrEmpty(oldProductPicture) && oldProductPicture != "productImages/defaultProduct.png")
                    {
                        isNewPictureUploaded = true;
                    }
                }

                if (result.ResultStatus == ResultStatus.Success)
                {
                    if (isNewPictureUploaded)
                    {
                        _imageHelper.Delete(oldProductPicture);
                    }

                    var productUpdateAjaxViewModel = JsonSerializer.Serialize(new ProductUpdateAjaxViewModel
                    {
                        ProductDto = result.Data,
                        ProductUpdatePartial = await this.RenderViewToStringAsync("_ProductUpdatePartial", productUpdateDto)
                    });

                    return Json(productUpdateAjaxViewModel);
                }
            }

            var productUpdateAjaxErrorModel = JsonSerializer.Serialize(new ProductUpdateAjaxViewModel
            {
                ProductUpdatePartial = await this.RenderViewToStringAsync("_ProductUpdatePartial", productUpdateDto)
            });

            return Json(productUpdateAjaxErrorModel);
        }



    }
}

//[HttpPost]
//public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
//{
//    if (ModelState.IsValid)
//    {


//        bool isNewPictureUploaded = false;
//        var result = await _productService.UpdateAsync(productUpdateDto);
//        var product = _mapper.Map<Product>(result.Data);
//        var oldProductPicture = product.Picture;
//        if (productUpdateDto.PictureFile != null)
//        {
//            var uploadedImageDtoResult = await _imageHelper.UploadProductImage(productUpdateDto.Code, productUpdateDto.PictureFile);
//            productUpdateDto.Picture = uploadedImageDtoResult.ResultStatus == ResultStatus.Success ? uploadedImageDtoResult.Data.FullName : "productImages/defaultProduct.png";
//            if (oldProductPicture != "productImages/defaultProduct.png")
//            {
//                isNewPictureUploaded = true;
//            }
//        }

//        if (result.ResultStatus == ResultStatus.Success)
//        {
//            if (isNewPictureUploaded)
//            {
//                _imageHelper.Delete(oldProductPicture);
//            }
//            var productUpdateAjaxViewModel = JsonSerializer.Serialize(new ProductUpdateAjaxViewModel
//            {
//                ProductDto = result.Data,
//                ProductUpdatePartial = await this.RenderViewToStringAsync("_ProductUpdatePartial", productUpdateDto)
//            });
//            return Json(productUpdateAjaxViewModel);
//        }
//    }
//    var productUpdateAjaxErrorModel = JsonSerializer.Serialize(new ProductUpdateAjaxViewModel
//    {
//        ProductUpdatePartial = await this.RenderViewToStringAsync("_ProductUpdatePartial", productUpdateDto)
//    });
//    return Json(productUpdateAjaxErrorModel);
//}

