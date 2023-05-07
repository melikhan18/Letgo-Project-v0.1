using Letgo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letgo.Business.Services
{
	public class LikeService : BaseService<Like>
	{
		public bool AddUserLike(int userId, int productId)
		{
			var like = new Like();
			like.UserId = userId;
			like.ProductId = productId;
			var result = Create(like);
			return result.IsSuccess;
		}
	}
}
