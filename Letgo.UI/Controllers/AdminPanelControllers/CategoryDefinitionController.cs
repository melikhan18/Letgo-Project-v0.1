using Letgo.Business.Services;
using Letgo.Business.Validations;
using Letgo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers.AdminPanelControllers
{
	public class CategoryDefinitionController : Controller
	{
		CategoryService _categoryService = new CategoryService();
		public IActionResult Index()
		{
			var categoryList = _categoryService.GetAll();
			return View(categoryList);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			var validation = new CategoryValidation(category, Reason.New);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_categoryService.Create(category);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(category);
		}
		public IActionResult Edit(int id)
		{
			var category = _categoryService.GetById(id);
			if (category == null)
			{
				TempData["ErrorMessage"] = "Aradığın Marka Bulunamadı";
				return RedirectToAction("Index", "CategoryDefinition");
			}
			return View(category);
		}
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			var validation = new CategoryValidation(category, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage;
				_categoryService.Update(category);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(category);
		}
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var category = _categoryService.GetById(id);
			var validation = new CategoryValidation(category, Reason.Delete);
			if (validation.IsValid)
			{
				_categoryService.Delete(category.Id);
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				return RedirectToAction("Index");
			}
			TempData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return RedirectToAction("Index");
		}


	}
}
