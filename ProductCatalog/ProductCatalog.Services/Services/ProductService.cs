using ProductCatalog.Data.StaticData;
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
                .AllReadonly<ProductDTO>()
                .ToList();
            return data;
        }
    }
}
