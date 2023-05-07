using Letgo.Business.Services;
using Letgo.Business.Validations;
using Letgo.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers.AdminPanelControllers
{
	public class UserDefinitionController : Controller
	{
		UserService _userService = new UserService();

		public IActionResult Index()
		{
			var userList = _userService.GetAll();
			return View(userList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(User user)
		{
			var validation = new UserValidation(user, Reason.New);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_userService.Create(user);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(user);
		}

		public IActionResult Edit(int id)
		{
			var user = _userService.GetById(id);
			if (user == null)
			{
				TempData["ErrorMessage"] = "Aradığın kullanıcı bulunamadı";
				return RedirectToAction("Index", "User");
			}
			return View(user);
		}

		[HttpPost]
		public IActionResult Edit(User user)
		{
			var validation = new UserValidation(user, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage;
				_userService.Update(user);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(user);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var user = _userService.GetById(id);
			var validation = new UserValidation(user, Reason.Delete);
			if (validation.IsValid)
			{
				_userService.Delete(user.Id);
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				return RedirectToAction("Index");
			}
			TempData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return RedirectToAction("Index");
		}
	}

}
