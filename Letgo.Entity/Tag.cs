using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Entity
{
	public class Tag
	{
		public int TagId { get; set; }
		public string TagName { get; set; }
		// Diğer etiket özellikleri

		public ICollection<ProductTag> ProductTags { get; set; }
	}
}
