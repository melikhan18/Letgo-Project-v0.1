using Letgo.Business.Services;
using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Validations
{
	public class ProductValidation : BaseValidation<Product>
	{
		private readonly ProductService _productService = new ProductService();

		public ProductValidation(Product product, Reason reason)
		{
			Validate(product, reason);
		}

		public override void Validate(Product product, Reason reason)
		{
			base.Validate(product, reason);

			if (product is null)
			{
				SetInvalid("Entity boş gönderildi!!!");
			}

			if (reason == Reason.New || reason == Reason.Update)
			{
				if (string.IsNullOrWhiteSpace(product.Name))
				{
					SetInvalid("Ürün adı zorunludur.");
					return;
				}

				if (product.Name.Length <= 2)
				{
					SetInvalid("Ürün adı 3 karakterden uzun olmalıdır!");
					return;
				}

				if (product.CategoryId <= 0)
				{
					SetInvalid("Ürün kategorisi zorunludur.");
					return;
				}

				if (product.Price <= 0)
				{
					SetInvalid("Ürün fiyatı geçerli değil.");
					return;
				}
				if (product.UserId <= 0)
				{
					SetInvalid("Ürün Satıcısı geçerli değil.");
					return;
				}

				SetValid("Ürün başarıyla kaydedildi.");
			}
			if (reason == Reason.Delete)
			{
				if (product.Id <= 0)
				{
					SetInvalid("Seçilen ürün bulunamadı!");
				}
				SetValid("Ürün başarıyla silindi.");
			}

		}
	}
}
