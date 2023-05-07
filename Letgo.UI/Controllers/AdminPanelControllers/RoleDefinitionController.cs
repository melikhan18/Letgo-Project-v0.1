using Letgo.Business.Services;
using Letgo.Business.Validations;
using Letgo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers.AdminPanelControllers
{
	public class RoleDefinitionController : Controller
	{
		private readonly RoleService _roleService;

		public RoleDefinitionController()
		{
			_roleService = new RoleService();
		}

		public IActionResult Index()
		{
			var roleList = _roleService.GetAll();
			return View(roleList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Role role)
		{
			var validation = new RoleValidation(role, Reason.New);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_roleService.Create(role);
				return RedirectToAction("Index");
			}

			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(role);
		}

		public IActionResult Edit(int id)
		{
			var role = _roleService.GetById(id);
			if (role == null)
			{
				TempData["ErrorMessage"] = "Aradığın Rol Bulunamadı";
				return RedirectToAction("Index");
			}
			return View(role);
		}

		[HttpPost]
		public IActionResult Edit(Role role)
		{
			var validation = new RoleValidation(role, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage;
				_roleService.Update(role);
				return RedirectToAction("Index");
			}

			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(role);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var role = _roleService.GetById(id);
			var validation = new RoleValidation(role, Reason.Delete);
			if (validation.IsValid)
			{
				_roleService.Delete(role.Id);
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				return RedirectToAction("Index");
			}

			TempData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return RedirectToAction("Index");
		}
	}

}
