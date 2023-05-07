using Letgo.Business.Services;
using Letgo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers
{
	public class TagController : Controller
	{
		ProductService productService = new ProductService();
		ProductTagService productTagService = new ProductTagService();

		[Route("tag/{id?}")]
		public IActionResult Index(int id)
		
		{
			var tag = productTagService.GetAll().Where(x => x.TagId == id).ToList();
			var productIdList = new List<int>();
			foreach (var item in tag)
			{
				productIdList.Add(item.ProductId);
			}
			List<Product> tagProducts = productService.GetAll().Where(p => productIdList.Contains(p.Id)).ToList();

			return View(tagProducts);
		}
	}
}
