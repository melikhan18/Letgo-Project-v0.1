using Letgo.Business.Services;
using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Validations
{
	public class RoleValidation : BaseValidation<Role>
	{
		private readonly RoleService _roleService = new RoleService();

		public RoleValidation(Role role, Reason reason)
		{
			Validate(role, reason);
		}

		public override void Validate(Role role, Reason reason)
		{
			base.Validate(role, reason);

			if (role is null)
			{
				SetInvalid("Entity boş gönderildi!!!");
			}
			if (reason == Reason.New || reason == Reason.Update)
			{
				if (string.IsNullOrWhiteSpace(role.Name))
				{
					SetInvalid("Rol adı zorunludur.");
					return;
				}

				if (role.Name.Length <= 2)
				{
					SetInvalid("Rol adı 3 karakterden uzun olmalıdır!");
					return;
				}

				if (_roleService.IsRoleNameExist(role))
				{
					SetInvalid($"'{role.Name}' adında bir rol zaten mevcut.");
					return;
				}

				SetValid("Rol başarıyla kaydedildi.");
			}
			if (reason == Reason.Delete)
			{
				if (role.Id <= 0)
				{
					SetInvalid("Seçilen rol bulunamadı!");
				}
				SetValid("Rol başarıyla silindi.");
			}

		}
	}
}
