using Letgo.Entity;

namespace Letgo.Business.Services
{
	public class UserService : BaseService<User>
	{
		public bool IsEmailExist(User user)
		{
			var result = GetAll().Any(b => b.Email == user.Email);
			return result;
		}

		public User GetUserForEmail(string email)
		{
			var result = GetAll().FirstOrDefault(b => b.Email == email);
			return result ?? new User();
		}
	}
}
