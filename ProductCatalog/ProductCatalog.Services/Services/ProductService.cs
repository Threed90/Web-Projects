using Microsoft.EntityFrameworkCore;
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

        public async Task AddProduct(ProductDTO product)
        {
            
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity
            };

            await _repository.AddAsync(newProduct);

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var productForDelete = await this._repository
                .All<Product>()
                .Where(p => p.Id.ToString() == productId)
                .FirstOrDefaultAsync();

            if(productForDelete == null)
            {
                return false;
            }

            await this._repository.DeleteAsync<Product>(productForDelete.Id);
            await this._repository.SaveChangesAsync();
            return true;
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

        public async Task<ProductDTO> GetById(string productId)
        {
            var requiredProduct = await _repository
                .AllReadonly<Product>()
                .Where(p => p.Id.ToString() == productId)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity
                })
                .FirstOrDefaultAsync();

            return requiredProduct;
        }

        public async Task<string> UpdateProduct(string productId, ProductDTO product)
        {
            var productForUpdate = await _repository
                .All<Product>()
                .Where(p => p.Id.ToString() == productId)
                .FirstOrDefaultAsync();

            if (productForUpdate != null)
            {
                productForUpdate.Name = product.Name;
                productForUpdate.Price = product.Price;
                productForUpdate.Quantity = product.Quantity;

                _repository.Update(productForUpdate);
                await _repository.SaveChangesAsync();
                return productId;
            }
            else
            {
                return "no such product";
            }
            
        }
    }
}
