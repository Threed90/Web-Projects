using ProductCatalog.Models.DTO;

namespace ProductCatalog.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDTO>> GetAll();
    }
}
