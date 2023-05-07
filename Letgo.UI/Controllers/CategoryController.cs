using Letgo.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers
{
	public class CategoryController : Controller
	{
		CategoryService categoryService = new CategoryService();
		BrandService _brandServie = new BrandService();

		[Route("category/{id?}")]
		public IActionResult Index(int id)
		{
			ViewData["Brands"] = _brandServie.GetAll();
			var category = categoryService.GetProductsByCategoryId(id);
			if (category.Id <= 0)
			{
				return RedirectToAction("404Page", "Home");
			}
			return View(category);
		}
	}
}
