using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.ProductModel;
using PharmacyOnline.Services.Product;
using System;

namespace PharmacyOnline.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("add/product/tablet")]
        public async Task<IActionResult> CreateProductTablet([FromForm]ProductTablet model)
        {
            #region HANLDE FILE IMAGE
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var upload = Path.Combine(filePath, filename);

            model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

            string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

            #endregion

            return Ok(await _productRepo.AddProductTablet(model, url));

        }


        [HttpPost, Authorize(Roles = "Admin")]
        [Route("add/product/capsule")]
        public async Task<IActionResult> CreateProductCapsule([FromForm] ProductCapsule model)
        {
            #region HANLDE FILE IMAGE
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var upload = Path.Combine(filePath, filename);

            model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

            string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

            #endregion

            return Ok(await _productRepo.AddProductCapsule(model, url));

        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("add/product/liquid")]
        public async Task<IActionResult> CreateProductLiquid([FromForm] ProductLiquid model)
        {
            #region HANLDE FILE IMAGE
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var upload = Path.Combine(filePath, filename);

            model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

            string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

            #endregion

            return Ok(await _productRepo.AddProductLiquid(model, url));

        }


        [HttpGet]
        [Route("listProduct")]
        public async Task<IActionResult> getListProduct(int page, int pagesize)
        {
            return Ok( await _productRepo.getProductPaginate(page, pagesize) );
        }


        [HttpGet]
        [Route("get/product/detail")]
        public async Task<IActionResult> getDetail(int idProduct)
        {
            return Ok( await _productRepo.detailProduct(idProduct) );
        }


        [HttpPost, Authorize(Roles = "Admin")]
        [Route("update/tablet")]
        public async Task<IActionResult> updateTablet([FromForm] ProductTablet2 model)
        {
            if ( model.thumbnail != null )
            {
                #region HANLDE FILE IMAGE
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var upload = Path.Combine(filePath, filename);

                model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

                #endregion

                return Ok(await _productRepo.UpdateProductTablet(model, url));
            }

            return Ok(await _productRepo.UpdateProductTablet(model, ""));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("update/capsule")]
        public async Task<IActionResult> updateCapsule([FromForm] ProductCapsule2 model)
        {
            if (model.thumbnail != null)
            {
                #region HANLDE FILE IMAGE
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var upload = Path.Combine(filePath, filename);

                model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

                #endregion

                return Ok(await _productRepo.UpdateProductCapsule(model, url));
            }

            return Ok(await _productRepo.UpdateProductCapsule(model, ""));
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("update/solid")]
        public async Task<IActionResult> updateSolid([FromForm] ProductLiquid2 model )
        {
            if ( model.thumbnail != null )
            {
                #region HANLDE FILE IMAGE
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var upload = Path.Combine(filePath, filename);

                model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                string url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

                #endregion

                return Ok(await _productRepo.UpdateProductLiquid(model, url));
            }
            return Ok(await _productRepo.UpdateProductLiquid(model, ""));

        }

        [HttpGet, Authorize(Roles = "Admin")]
        [Route("delete/product")]
        public async Task<IActionResult> deleteP(int productId)
        {
            return Ok( await _productRepo.DeleteProduct(productId) );
        }

        [HttpGet]
        [Route("search/product")]
        public async Task<IActionResult> searchP(string search, int page = 1)
        {
            return Ok(await _productRepo.searchP( search, page));
        }

        [HttpGet]
        [Route("filter/cate/product")]
        public async Task<IActionResult> filter(int? cate, int page = 1)
        {
            return Ok(await _productRepo.filterCate(cate, page));
        }

        [HttpGet]
        [Route("sort/filter/paginate/product")]
        public async Task<IActionResult> sort(int? cate, string? sorting, int page = 1)
        {
            return Ok( await _productRepo.sortFilterPagin(cate, sorting, page));
        }

        [HttpGet]
        [Route("getAll/categories")]
        public async Task<IActionResult> category()
        {
            return Ok(await _productRepo.getAllCategories());
        }

    }


}
