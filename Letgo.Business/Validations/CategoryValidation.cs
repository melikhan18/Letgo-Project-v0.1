using Letgo.Business.Services;
using Letgo.Entity;

namespace Letgo.Business.Validations
{
	public class CategoryValidation : BaseValidation<Category>
	{
		private readonly CategoryService _categoryService = new CategoryService();

		public CategoryValidation(Category category, Reason reason)
		{
			Validate(category, reason);
		}

		public override void Validate(Category category, Reason reason)
		{
			base.Validate(category, reason);

			if (category is null)
			{
				SetInvalid("Entity boş gönderildi!!!");
			}
			if (reason == Reason.New || reason == Reason.Update)
			{
				if (string.IsNullOrWhiteSpace(category?.Name))
				{
					SetInvalid("Kategori adı zorunludur.");
					return;
				}

				if (category.Name.Length <= 2)
				{
					SetInvalid("Kategori adı 3 karakterden uzun olmalıdır!");
					return;
				}

				if (_categoryService.IsCategoryNameExist(category))
				{
					SetInvalid($"'{category.Name}' adında bir kategori zaten mevcut.");
					return;
				}

				SetValid("Kategori başarıyla kaydedildi.");
			}
			if (reason == Reason.Delete)
			{
				if (category?.Id <= 0)
				{
					SetInvalid("Seçilen kategori bulunamadı!");
				}
				SetValid("Kategori başarıyla silindi.");
			}

		}
	}
}
