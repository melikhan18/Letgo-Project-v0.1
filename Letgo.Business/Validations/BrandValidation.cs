using Letgo.Business.Services;
using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Validations
{
	public class BrandValidation : BaseValidation<Brand>
	{
		private readonly BrandService _brandService = new BrandService();

		public BrandValidation(Brand brand, Reason reason)
		{
			Validate(brand, reason);
		}

		public override void Validate(Brand brand, Reason reason)
		{
			base.Validate(brand, reason);

			if (brand is null)
			{
				SetInvalid("Entity boş gönderildi!!!");
			}
			if (reason == Reason.New || reason == Reason.Update)
			{
				if (string.IsNullOrWhiteSpace(brand.Name))
				{
					SetInvalid("Marka adı zorunludur.");
					return;
				}

				if (brand.Name.Length <= 2)
				{
					SetInvalid("Marka adı 3 karakterden uzun olmalıdır!");
					return;
				}

				if (_brandService.IsBrandNameExist(brand))
				{
					SetInvalid($"'{brand.Name}' adında bir marka zaten mevcut.");
					return;
				}

				SetValid("Marka başarıyla kaydedildi.");
			}
			if (reason == Reason.Delete)
			{
				if (brand.Id <= 0)
				{
					SetInvalid("Seçilen marka bulunamadı!");
				}
				SetValid("Marka başarıyla silindi.");
			}

		}
	}

}
