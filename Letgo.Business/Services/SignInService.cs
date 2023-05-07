using Letgo.Business.Validations;
using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Letgo.Business.Services
{
	public class SignInService : BaseService<User>
	{
		public bool isEmailAndPasswordCorrect(string email, string password)
		{
			if (email == null || password == null)
			{
				return false;
			}
			var user = GetAll().Any(u => u.Email == email && u.Password == password);
			return user;
		}
	}
}
