using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Entity
{
	public class Role
	{
		public int Id { get; set; }
		public string Name { get; set; } // Rolün adı (örn. Admin, Editor, User)
		public string Description { get; set; } // Rolün açıklaması

		// Kullanıcılara göre ilişkiler
		public virtual ICollection<User> Users { get; set; }
	}
}
