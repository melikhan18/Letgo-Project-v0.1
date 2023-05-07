using Letgo.Business.Services;
using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Validations
{
	public class UserValidation : BaseValidation<User>
	{
		private readonly UserService _userService = new UserService();
		public UserValidation(User user, Reason reason)
		{
			Validate(user, reason);
		}

		public override void Validate(User user, Reason reason)
		{
			base.Validate(user, reason);

			if (user is null)
			{
				SetInvalid("Entity boş gönderildi!!!");
			}
			if (reason == Reason.New || reason == Reason.Update)
			{
				if (string.IsNullOrWhiteSpace(user?.FirstName))
				{
					SetInvalid("Ad zorunludur.");
					return;
				}
				if (string.IsNullOrWhiteSpace(user?.LastName))
				{
					SetInvalid("Soyad zorunludur.");
					return;
				}

				if (user.FirstName.Length <= 2)
				{
					SetInvalid("Ad 3 karakterden uzun olmalıdır!");
					return;
				}
				if (reason == Reason.New && _userService.IsEmailExist(user))
				{
					SetInvalid($"'{user.Email}' epostalı bir kullanıcı zaten mevcut.");
					return;
				}
				SetValid("Kullanıcı başarıyla kaydedildi.");
			}




			if (reason == Reason.Delete)
			{
				if (user.Id <= 0)
				{
					SetInvalid("Seçilen kullanıcı bulunamadı!");
				}
				SetValid("Kullanıcı başarıyla silindi.");
			}

		}
	}
}
