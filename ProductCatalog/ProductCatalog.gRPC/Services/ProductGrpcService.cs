using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.gRPC.Services
{
    public class ProductGrpcService : ProductCatalog.ProductCatalogBase
    {

        private readonly IProductService productService;

        public ProductGrpcService(IProductService productService)
        {
            this.productService = productService;
        }

        public override async Task<ProductList> GetAll(Empty request, ServerCallContext context)
        {
            ProductList result = new ProductList();
            var products = await productService.GetAll();
            result.Items.AddRange(products.Select(p => new ProductItem
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Price = (double)p.Price,
                Quantity = p.Quantity
            }));

            return result;
        }
    }
}
