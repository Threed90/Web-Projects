using ProductCatalog.Models.DTO;

namespace ProductCatalog.Services.Interfaces
{
    public interface IProductService
    {
        public List<ProductDTO> GetAll();
    }
}
