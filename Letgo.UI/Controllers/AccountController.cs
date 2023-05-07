using Letgo.Business.Services;
using Letgo.Business.Validations;
using Letgo.Entity;
using Letgo.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Letgo.UI.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInService _signInService = new SignInService();
		private readonly UserService _userService = new UserService();
		private readonly RoleService _roleService;
		private readonly CategoryService _categoryService;
		private readonly BrandService _brandService;
		private readonly ProductService _productService;
		private readonly CartService _cartService;

		public AccountController(RoleService roleService, CategoryService categoryService, BrandService brandService, ProductService productService, CartService cartService)
		{
			_roleService = roleService;
			_categoryService = categoryService;
			_brandService = brandService;
			_productService = productService;
			_cartService = cartService;
		}

		public IActionResult Index()
		{
			var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
			var user = _userService.GetById(userId);
			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login");
			}
			return View(user);
		}

		public IActionResult Login()
		{
			ClaimsPrincipal claimsUser = HttpContext.User;
			if (claimsUser.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(User user)
		{

			if (_signInService.isEmailAndPasswordCorrect(user.Email, user.Password))
			{
				user = _userService.GetUserForEmail(user.Email);
				var age = (DateTime.Now.Year - user.DateOfBirth.Year).ToString();
				List<Claim> claims = new List<Claim>
				{
					new Claim("Email", string.IsNullOrWhiteSpace(user.Email) ? string.Empty : user.Email),
					new Claim("Name" , string.IsNullOrWhiteSpace(user.FirstName) ? string.Empty : user.FirstName),
					new Claim("Address" , string.IsNullOrWhiteSpace(user.Address) ? string.Empty : user.Address),
					new Claim("FullName" , string.IsNullOrWhiteSpace(user.FullName) ? string.Empty : user.FullName),
					new Claim("City" , string.IsNullOrWhiteSpace(user.City) ? string.Empty : user.City),
					new Claim("Age" , string.IsNullOrWhiteSpace(age) ? string.Empty : age),
					new Claim("ProfilePhoto" , string.IsNullOrWhiteSpace(user?.ProfilePhoto) ? "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png" : user.ProfilePhoto),
					new Claim("UserId" , string.IsNullOrWhiteSpace(user?.Id.ToString()) ? string.Empty : user.Id.ToString()),
				};

				ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				AuthenticationProperties properties = new AuthenticationProperties
				{
					AllowRefresh = true,
					IsPersistent = user.isLogin
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(identity),
					properties);

				return RedirectToAction("Index", "Home");
			}
			TempData["ErrorMessage"] = "user not found!";
			return View();
		}

		public IActionResult Register()
		{

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(User user)
		{
			var validation = new UserValidation(user, Reason.New);
			if (validation.IsValid)
			{
				var role = _roleService.GetAll().FirstOrDefault(x => x.Name == "user");
				user.RoleId = role.Id;
				user.FullName = user.FirstName + " " + user.LastName;

				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_userService.Create(user);
				return RedirectToAction("Index");
			}
			ViewData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return View(user);
		}

		public async Task<IActionResult> Logout()
		{
			_cartService.ClearCart();
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}

		public IActionResult CreateProduct()
		{
			ViewData["Categories"] = _categoryService.GetAll();
			ViewData["Brands"] = _brandService.GetAll();

			return View();
		}

		[HttpPost]
		public IActionResult CreateProduct(Product product)
		{

			product.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
			var validation = new ProductValidation(product, Reason.New);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_productService.Create(product);
				return RedirectToAction("MyProducts");
			}
			ViewData["Categories"] = _categoryService.GetAll();
			ViewData["Brands"] = _brandService.GetAll();
			ViewData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return View(product);
		}

		public IActionResult MyProducts()
		{
			var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
			var result = _productService.GetProductsByUserId(userId);
			return View(result);
		}
		public IActionResult EditProduct(int id)
		{
			if (id <= 0)
			{
				return RedirectToAction("Index");
			}
			var product = _productService.GetById(id);
			if (product == null)
			{
				TempData["ErrorMessage"] = "Seçilen ürün bulunamadı!";
				return RedirectToAction("MyProducts");
			}
			if (product.UserId != int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value))
			{
				TempData["ErrorMessage"] = "Bu ürünü güncellemeye yetkiniz yok !";
				return RedirectToAction("MyProducts");
			}
			ViewData["Categories"] = _categoryService.GetAll();
			ViewData["Brands"] = _brandService.GetAll();
			return View(product);
		}
		[HttpPost]
		public IActionResult EditProduct(Product product)
		{
			product.UserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
			var validation = new ProductValidation(product, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				_productService.Update(product);
				return RedirectToAction("MyProducts");
			}
			ViewData["Categories"] = _categoryService.GetAll();
			ViewData["Brands"] = _brandService.GetAll();
			ViewData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return View(product);
		}
		public IActionResult DeleteProduct(int id)
		{
			var product = _productService.GetById(id);
			var validation = new ProductValidation(product, Reason.Delete);
			if (validation.IsValid)
			{
				_productService.Delete(product.Id);
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				return RedirectToAction("MyProducts");
			}
			TempData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return RedirectToAction("MyProducts");
		}
		public IActionResult EditProfile()
		{
			var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
			var user = _userService.GetById(userId);
			if (user == null)
			{
				TempData["ErrorMessage"] = "Kullanıcı Bulunamadı!";
				return RedirectToAction("Index");
			}
			return View(user);
		}
		[HttpPost]
		public IActionResult EditProfile(User user)
		{
			var validation = new UserValidation(user, Reason.Update);
			if (validation.IsValid)
			{
				TempData["SuccessMessage"] = validation.SuccessMessage.ToString();
				user.FullName = user.FirstName + " " + user.LastName;
				user.RoleId = _roleService.GetAll().FirstOrDefault(x => x.Name == "user").Id;
				_userService.Update(user);
				return RedirectToAction("Index");
			}
			ViewData["ErrorMessage"] = validation.ErrorMessage.ToString();
			return View(user);
		}
	}
}
