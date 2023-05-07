using Letgo.Business.Services;
using Letgo.Business.Validations;
using Letgo.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers.AdminPanelControllers
{
	public class ProductDefinitionController : Controller
	{
		ProductService _productService = new ProductService();
		CategoryService _categoryService = new CategoryService();
		BrandService _brandService = new BrandService();
		public IActionResult Index()
		{
			var productList = _productService.GetAll();
			return View(productList);
		}
		public IActionResult Create()
		{
			ComboItemsNeeded();
			return View();
		}

		private void ComboItemsNeeded()
		{
			ViewBag.Categories = _categoryService.GetAll();
			ViewBag.Brands = _brandService.GetAll();
			ViewBag.Colors = Enum.GetValues(typeof(Color)).Cast<Color>().ToList();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			var validation = new ProductValidation(product, Reason.New);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_productService.Create(product);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			ComboItemsNeeded();
			return View(product);
		}
		public IActionResult Edit(int id)
		{
			var product = _productService.GetById(id);
			if (product == null)
			{
				TempData["ErrorMessage"] = "Aradığın Ürün Bulunamadı";
				return RedirectToAction("Index", "ProductDefinition");
			}
			return View(product);
		}
		[HttpPost]
		public IActionResult Edit(Product product)
		{
			var validation = new ProductValidation(product, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage;
				_productService.Update(product);
				return RedirectToAction("Index");
			}
			ViewBag.ErrorMessage = validation.ErrorMessage.ToString();
			return View(product);
		}
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var product = _productService.GetById(id);
			var validation = new ProductValidation(product, Reason.Delete);
			if (validation.IsValid)
			{
				_productService.Delete(product.Id);
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				return RedirectToAction("Index");
			}
			TempData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return RedirectToAction("Index");
		}


	}
}