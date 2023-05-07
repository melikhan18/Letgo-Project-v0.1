using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Services
{
	public class RoleService : BaseService<Role>
	{
		public bool IsRoleNameExist(Role role)
		{
			var result = GetAll().Any(b => b.Name == role.Name);
			return result;
		}
	}
}
