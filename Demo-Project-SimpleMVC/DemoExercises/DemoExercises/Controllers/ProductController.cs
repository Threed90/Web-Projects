using DemoExercises.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DemoExercises.Controllers
{
    public class ProductController : Controller
    {

        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>
        {
            new ProductViewModel { Id = 1, Name = "Laptop", Price = 999.99m },
            new ProductViewModel { Id = 2, Name = "Smartphone", Price = 499.99m },
            new ProductViewModel { Id = 3, Name = "Tablet", Price = 299.99m }
        };

        [ActionName("My-Products")]
        public IActionResult All(string productName)
        {
            if(!string.IsNullOrEmpty(productName))
            {
                var filteredProducts = this.products
                    .Where(p => p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                return View(filteredProducts);
            }

            return View(this.products);
        }

        [HttpGet]
        public IActionResult ById()
        {
            return View(new ProductViewModel
            {
                Id = -256,
                Name = "Invalid Product",
                Price = 0.0m
            }); ;
        }

        [HttpPost]
        public IActionResult ById(int id)
        {
            var product = this.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public IActionResult AllAsJSON()
        {
            return Json(this.products);
        }

        public IActionResult AllAsText()
        {
            var result = string.Join(Environment.NewLine, this.products.Select(p => $"Id: {p.Id}, Name: {p.Name}, Price: {p.Price:F2}"));
            return Content(result, "text/plain");
        }

        public IActionResult AllAsTextFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var product in this.products)
            {
                sb.AppendLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price:F2}");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/plain", "products.txt");
        }
    }
}
