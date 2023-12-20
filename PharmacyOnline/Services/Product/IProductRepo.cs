using PharmacyOnline.DTO.ProductDTO;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Models.ProductModel;

namespace PharmacyOnline.Services.Product
{
    public interface IProductRepo
    {
        // create:
        Task<ProductTabletDTO> AddProductTablet(ProductTablet model, string url);

        Task<ProductCapsuleDTO> AddProductCapsule (ProductCapsule model, string url);

        Task<ProductLiquidDTO> AddProductLiquid(ProductLiquid model, string url);

        // render products:
        Task<List<getAll>> getProductPaginate(int page, int pagesize);

        // watch detail:
        Task<Object> detailProduct( int idProduct);

        Task<ProductTabletDTO> UpdateProductTablet(ProductTablet2 model, string url);
        Task<ProductCapsuleDTO> UpdateProductCapsule(ProductCapsule2 model, string url);
        Task<ProductLiquidDTO> UpdateProductLiquid(ProductLiquid2 model, string url);

        Task<result> DeleteProduct(int ProductId);

        Task<List<getAll>> searchP(string search, int page, int pagesize = 10);

        Task<List<getAll>> filterCate(int? cate, int page, int pagesize = 10);

        Task<List<getAll>> sort(string? sorting, int page, int pagesize = 10);

        Task<List<CategoryGet2>> getAllCategories();

    }
}
