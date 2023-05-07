using Letgo.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Letgo.UI.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		UserService _userService = new UserService();
		public IActionResult Index()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
			{
				var email = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var user = _userService.GetUserForEmail(email);
				if (user == null)
				{
					return View("Error");
				}

				return View(user);
			}
			return View();
		}
	}
}
