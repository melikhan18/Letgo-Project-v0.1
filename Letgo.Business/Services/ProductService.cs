using Letgo.Entity;

namespace Letgo.Business.Services
{
	public class ProductService : BaseService<Product>
	{
		public IEnumerable<Product> GetAmountLessThenFiveProducts()
		{
			var products = GetAll().Where(x => x.StockQuantity <= 5).Take(5).ToList();
			return products;
		}
		public IEnumerable<Product> GetProductsByPriceFilter(decimal price)
		{
			var products = GetAll().Where(x => x.Price <= price).ToList();
			return products;
		}
		public bool IsProductNameExist(Product product)
		{
			var result = GetAll().Any(b => b.Name == product.Name);
			return result;
		}
		public List<Product> GetProductsByUserId(int userId)
		{
			if (userId <= 0)
			{
				return new List<Product>();
			}
			var result = GetAll().Where(x => x.UserId == userId).ToList();
			if (result.Count <= 0)
			{
				return new List<Product>();
			}
			return result;
		}
		public IEnumerable<Product> GetFourProductsByCategoryId(int categoryId)
		{
			List<Product> products = new List<Product>();
			if (categoryId < 0)
			{
				return products;
			}
			var results = GetAll().Where(x => x.CategoryId == categoryId).Take(4).ToList();
			if (results?.Count < 0 || results is null)
			{
				return products;
			}
			return results;
		}
	}
}
