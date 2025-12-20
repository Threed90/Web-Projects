using ProductCatalog.Models.DTO;

namespace ProductCatalog.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDTO>> GetAll();

        public Task<ProductDTO> GetById(string productId);

        public Task AddProduct(ProductDTO product);

        public Task<string> UpdateProduct(string productId, ProductDTO product);

        public Task<bool> DeleteProduct(string productId);
    }
}
