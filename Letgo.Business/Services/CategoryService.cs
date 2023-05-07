using Letgo.DataAccess;
using Letgo.Entity;
using Microsoft.EntityFrameworkCore;

namespace Letgo.Business.Services
{
	public class CategoryService : BaseService<Category>
	{
		LetgoContext _dbContext = new LetgoContext();
		public Category GetProductsByCategoryId(int categoryId)
		{
			var category = _dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == categoryId);
			if (category == null || category.Products == null)
			{
				category = new Category();
			}
			return category;

		}
		public bool IsCategoryNameExist(Category category)
		{
			var result = GetAll().Any(b => b.Name == category.Name);
			return result;
		}
	}
}
