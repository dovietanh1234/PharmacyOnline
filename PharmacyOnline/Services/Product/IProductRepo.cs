using PharmacyOnline.DTO.ProductDTO;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Models.ProductModel;

namespace PharmacyOnline.Services.Product
{
    public interface IProductRepo
    {
        // create:
        Task<object> AddProductTablet(ProductTablet model, string url);

        Task<object> AddProductCapsule (ProductCapsule model, string url);

        Task<object> AddProductLiquid(ProductLiquid model, string url);

        // render products:
        Task<List<getAll>> getProductPaginate(int page, int pagesize);

        // watch detail:
        Task<object> detailProduct( int idProduct);

        Task<object> UpdateProductTablet(ProductTablet2 model, string url);
        Task<object> UpdateProductCapsule(ProductCapsule2 model, string url);
        Task<object> UpdateProductLiquid(ProductLiquid2 model, string url);

        Task<result> DeleteProduct(int ProductId);

        Task<List<getAll>> searchP(string search, int page, int pagesize = 10);

        Task<List<getAll>> filterCate(int? cate, int page, int pagesize = 10);

        Task<List<getAll>> sortFilterPagin(int? cate, string? sorting, int page, int pagesize = 10);

        Task<List<CategoryGet2>> getAllCategories();

    }
}
