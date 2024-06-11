using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Seeding;
using System.Text;
using System.Text.Json;

namespace MVCIntroDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("My-Products")]
        public IActionResult All(string keyword) 
        {
            if(keyword != null)
            {
                var foundProducts = ProductsData.Products
                    .Where(p => p.Name
                                    .ToLower()
                                    .Contains(keyword.ToString()));
                return View("All", foundProducts);
            }
            return View("All", ProductsData.Products);
        }

        public IActionResult ById(int id) 
        {
            var model = ProductsData.Products.FirstOrDefault(p => p.Id == id);  
            if (model == null) 
            {
                return BadRequest();
            }
            return View(model);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            return Json(ProductsData.Products, options);
        }

        public IActionResult AllAsText()
        {
            var text = string.Empty;
            foreach (var product in ProductsData.Products) 
            {
                text += $"Product {product.Id}: {product.Name} - {product.Price} lv.";
                text += "\r\n";
            }

            return Content(text);
        }

        public IActionResult AllAsTextFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var product in ProductsData.Products)
            {
                sb.AppendLine($"Product: {product.Id}: {product.Name} - {product.Price:F2} lv.");
            }

            Response.Headers.Add(HeaderNames.ContentDisposition,
                @"attachment;filename=products.txt");

            return File(Encoding.UTF8.GetBytes(sb.ToString().Trim()), "text/plain");
        }
    }
}
