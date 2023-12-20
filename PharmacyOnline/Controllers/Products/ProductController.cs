using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.ProductModel;
using PharmacyOnline.Services.Product;

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

        [HttpPost]
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


        [HttpPost]
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

        [HttpPost]
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
        [Route("get/detail")]
        public async Task<IActionResult> getDetail(int idProduct)
        {
            return Ok( await _productRepo.detailProduct(idProduct) );
        }


        [HttpPost]
        [Route("update/tablet")]
        public async Task<IActionResult> updateTablet([FromForm] ProductTablet2 model)
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

            return Ok( await _productRepo.UpdateProductTablet(model, url) );
        }

        [HttpPost]
        [Route("update/capsule")]
        public async Task<IActionResult> updateCapsule([FromForm] ProductCapsule2 model)
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

            return Ok( await _productRepo.UpdateProductCapsule( model, url ) );
        }

        [HttpPost]
        [Route("update/solid")]
        public async Task<IActionResult> updateSolid([FromForm] ProductLiquid2 model )
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

            return Ok( await _productRepo.UpdateProductLiquid(model, url) );

        }

        [HttpGet]
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
        [Route("sort/product")]
        public async Task<IActionResult> sort(string? sorting, int page = 1)
        {
            return Ok( await _productRepo.sort(sorting, page));
        }

        [HttpGet]
        [Route("getall/categories")]
        public async Task<IActionResult> category()
        {
            return Ok(await _productRepo.getAllCategories());
        }

    }


}
