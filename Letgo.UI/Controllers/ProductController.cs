using Letgo.Business.Services;
using Letgo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers
{
	public class ProductController : Controller
	{
		ProductService _productService = new ProductService();
		[Route("product/{id?}")]
		public IActionResult Index(int id)
		{
			var product = _productService.GetById(id);
			if (product.Id <= 0 || id <= 0)
			{
				return RedirectToAction("404Page", "Home");
			}
			var products = _productService.GetFourProductsByCategoryId(product.CategoryId).ToList();
			if (products == null || products.Count <= 0)
			{
				products = new List<Product>();
				return View(products);
			}
			ViewData["Products"] = products;
			return View(product);
		}
	}
}
