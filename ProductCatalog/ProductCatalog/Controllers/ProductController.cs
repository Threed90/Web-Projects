using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models.DTO;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("/Products")]
        public async Task<IActionResult> Products()
        {
            return View(await this.productService.GetAll());
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await this.productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Id = Guid.Parse(id);
            return View(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await this.productService.DeleteProduct(id);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("Products");
        }

        [HttpGet()]
        public async Task<IActionResult> Add()
        {
            return View(new ProductDTO());
        }

        [HttpPost()]
        public async Task<IActionResult> Add(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await this.productService.AddProduct(product);
            return RedirectToAction("Products");
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(string id, ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var updatedId = await this.productService.UpdateProduct(id, product);
            if (updatedId == "no such product" || string.IsNullOrWhiteSpace(updatedId))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id = updatedId });
        }
    }
}
