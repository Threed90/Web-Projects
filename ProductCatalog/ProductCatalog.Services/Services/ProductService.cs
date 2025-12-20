using ProductCatalog.Data.StaticData;
using ProductCatalog.Models.DB_Models;
using ProductCatalog.Models.DTO;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Services.Services
{
    public class ProductService : IProductService
    {
        private IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            var data = _repository
                .AllReadonly<Product>()
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .ToList();
            return data;
        }
    }
}
