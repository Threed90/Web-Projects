using ProductCatalog.Data.StaticData;
using ProductCatalog.Models.DTO;
using ProductCatalog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Services.Services
{
    public class ProductService : IProductService
    {
        public List<ProductDTO> GetAll()
        {
            return JsonData.GetData("product_data");
        }
    }
}
