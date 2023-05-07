using Letgo.Business.Services;
using Letgo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers
{
	public class HomeController : Controller
	{
		ProductService _productService;
		CategoryService _categoryService;
		LikeService _likeService;

		public HomeController(CategoryService categoryService, ProductService productService, LikeService likeService)
		{
			_categoryService = categoryService;
			_productService = productService;
			_likeService = likeService;
		}


		public IActionResult Index()
		{
			IEnumerable<Product> products = _productService.GetAll();
			IEnumerable<Category> categories = _categoryService.GetAll();
			ViewBag.Categories = categories;

			return View(products);
		}
		public IActionResult DecreasingProduct()
		{
			var products = _productService.GetAll().Where(x => x.StockQuantity <= 10).Take(50).ToList();
			return View(products);
		}
		public IActionResult AddUserLikeForSelectedProduct(int id)
		{
			var likeService = new LikeService();
			var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
			if (likeService.GetAll().Any(x => x.UserId == userId && x.ProductId == id))
			{
				TempData["ErrorMessage"] = "Beğeni başarısız. Daha önce beğenmiş olabilirsin !";
				return RedirectToAction("Index");
			}
			var result = likeService.AddUserLike(userId, id);
			if (!result)
			{
				TempData["ErrorMessage"] = "Beğeni başarısız. Bir hata oluştu!";
				return RedirectToAction("Index");
			}
			TempData["SuccessMessage"] = "Başarı ile beğenildi";
			return RedirectToAction("Index");
		}
		public IActionResult MostLikedProducts()
		{
			var likes = _likeService.GetAll();
			var productIdList = new List<int>();
			foreach (var item in likes)
			{
				productIdList.Add(item.ProductId);
			}
			List<Product> filteredProducts = _productService.GetAll().Where(p => productIdList.Contains(p.Id)).ToList();
			return View(filteredProducts);
		}
		public IActionResult NewestProduct()
		{
			DateTime now = DateTime.Now;

			List<Product> products = _productService.GetAll().Where(p => p.CreatedDate <= now).Take(12).ToList();
			return View(products);
		}
	}
}