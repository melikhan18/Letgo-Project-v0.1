using Letgo.Business.Services;
using Letgo.Entity;
using Letgo.Entity.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		ProductService productService = new ProductService();
		[Route("ProductsFilterBySearchCriterias")]
		[HttpPost]
		public async Task<List<Product>> ProductsFilterBySearchCriterias(ProductsFilterBySearchCriteriasRequest request)
		{
			var result = productService.GetAll().ToList();
			if (request.Color > 0)
			{
				result = result.Where(x => x.Color == request.Color).ToList();
			}
			if (request.Price > 0)
			{
				result = result.Where(x => x.Price <= request.Price).ToList();
			}
			if (request.CategoryId > 0)
			{
				result = result.Where(x => x.CategoryId == request.CategoryId).ToList();
			}
			if (request.BrandId > 0)
			{
				result = result.Where(x => x.BrandId == request.BrandId).ToList();
			}
			return result;
		}
		[Route("GetTwoProductFromStockLowToHight")]
		public List<Product> GetTwoProductFromStockLowToHight()
		{
			var result = productService.GetAll().OrderBy(p => p.StockQuantity).Take(2).ToList();
			return result;
		}

	}
}
