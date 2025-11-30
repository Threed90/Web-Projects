using ProductCatalog.Data.StaticData;
using ProductCatalog.Models.DTO;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Services.Services
{
    public class ProductService : IProductService
    {
        public async Task<List<ProductDTO>> GetAll()
        {
            var data = JsonData.GetData("product_data");
            return data;
        }
    }
}
