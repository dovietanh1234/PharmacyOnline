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
        [Route("admin/create/product/tablet")]
        public async Task<IActionResult> CreateProductTablet([FromForm]ProductTablet model)
        {
            #region HANLDE FILE IMAGE
            string url = "https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg";
            if ( model.thumbnail != null )
            {

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var upload = Path.Combine(filePath, filename);

            model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

             url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";

            }
            #endregion


            return Ok(await _productRepo.AddProductTablet(model, url));

        }


        [HttpPost, Authorize(Roles = "Admin")]
        [Route("admin/create/product/capsule")]
        public async Task<IActionResult> CreateProductCapsule([FromForm] ProductCapsule model)
        {
            #region HANLDE FILE IMAGE
            string url = "https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg";
            if (model.thumbnail != null)
            {
                
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var upload = Path.Combine(filePath, filename);

                model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";
            }
             #endregion

            return Ok(await _productRepo.AddProductCapsule(model, url));

        }

        [HttpPost, Authorize(Roles = "Admin")]
        [Route("admin/create/product/liquid")]
        public async Task<IActionResult> CreateProductLiquid([FromForm] ProductLiquid model)
        {
            #region HANLDE FILE IMAGE
            string url = "https://phutungnhapkhauchinhhang.com/wp-content/uploads/2020/06/default-thumbnail.jpg";
            if (model.thumbnail != null)
            {

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(model.thumbnail.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads/ProductImage");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var upload = Path.Combine(filePath, filename);

                model.thumbnail.CopyTo(new FileStream(upload, FileMode.Create));

                url = $"{Request.Scheme}://{Request.Host}/Uploads/ProductImage/{filename}";
            }
            #endregion

            return Ok(await _productRepo.AddProductLiquid(model, url));

        }


        [HttpPost, Authorize(Roles = "Admin")]
        [Route("admin/update/product/tablet")]
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
        [Route("admin/update/product/capsule")]
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
        [Route("admin/update/product/liquid")]
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

        // gộp list product
        [HttpGet]
        [Route("getdetail")]
        public async Task<IActionResult> getDetail(int idProduct)
        {
            return Ok(await _productRepo.detailProduct(idProduct));
        }



        [HttpGet, Authorize(Roles = "Admin")]
        [Route("admin/delete")]
        public async Task<IActionResult> deleteP(int productId)
        {
            return Ok( await _productRepo.DeleteProduct(productId) );
        }

        // gộp list product
        //search=conmeo&cate=3&sorting=NAME_DESC&page=1
        [HttpGet]
        public async Task<IActionResult> sort(string? isNewest = null, string? search = null, int? cate = null, string? sorting = null, int page = 1)
        {
            return Ok( await _productRepo.sortSearchFilterPagin(isNewest, search, cate, sorting, page));
        }

        // gộp list product
        [HttpGet]
        [Route("getall/category")]
        public async Task<IActionResult> category()
        {
            return Ok(await _productRepo.getAllCategories());
        }

    }


}
