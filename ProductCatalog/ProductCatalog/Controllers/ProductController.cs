using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;

        public ProductController(IProductService productService )
        {
            this.productService = productService;
        }

        [HttpGet("/Products")]
        public async Task<IActionResult> Products()
        {
            return View(await this.productService.GetAll());
        }
    }
}
