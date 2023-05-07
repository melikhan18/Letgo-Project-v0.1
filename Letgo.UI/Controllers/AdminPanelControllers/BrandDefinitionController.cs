using Letgo.Business.Services;
using Letgo.Business.Validations;
using Letgo.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Letgo.UI.Controllers.AdminPanelControllers
{
	[Authorize]
	public class BrandDefinitionController : Controller
	{
		BrandService _brandService = new BrandService();
		public IActionResult Index()
		{
			var brandList = _brandService.GetAll();
			return View(brandList);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Brand brand)
		{
			var validation = new BrandValidation(brand, Reason.New);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_brandService.Create(brand);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(brand);
		}
		public IActionResult Edit(int id)
		{
			var brand = _brandService.GetById(id);
			if (brand == null)
			{
				TempData["ErrorMessage"] = "Aradığın Marka Bulunamadı";
				return RedirectToAction("Index", "BrandDefinition");
			}
			return View(brand);
		}
		[HttpPost]
		public IActionResult Edit(Brand brand)
		{
			var validation = new BrandValidation(brand, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage;
				_brandService.Update(brand);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(brand);
		}
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var brand = _brandService.GetById(id);
			var validation = new BrandValidation(brand, Reason.Delete);
			if (validation.IsValid)
			{
				_brandService.Delete(brand.Id);
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				return RedirectToAction("Index");
			}
			TempData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return RedirectToAction("Index");
		}


	}
}
