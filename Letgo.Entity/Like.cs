using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Entity
{
	public class Like
	{
		public int UserId { get; set; }
		public User User { get; set; }

		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
