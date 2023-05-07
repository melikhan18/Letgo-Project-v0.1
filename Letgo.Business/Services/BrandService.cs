using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Services
{
	public class BrandService : BaseService<Brand>
	{
		public bool IsBrandNameExist(Brand brand)
		{
			var result = GetAll().Any(b => b.Name == brand.Name);
			return result;
		}
	}
}
