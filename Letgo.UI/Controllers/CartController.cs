using Letgo.Entity;
using Letgo.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Letgo.UI.Controllers
{
	public class CartController : Controller
	{
		private readonly CartService _cartService;

		public CartController(CartService cartService)
		{
			_cartService = cartService;
		}

		public IActionResult Index()
		{
			var cart = _cartService.GetCart();
			return View(cart);
		}
		public IActionResult RemoveItemFromCart(int id)
		{
			_cartService.RemoveItemFromCart(id);
			string referer = Request.Headers["Referer"].ToString();
			return Redirect(referer);
		}
		public IActionResult AddToCart(Product product)
		{
			_cartService.AddToCart(product);
			string referer = Request.Headers["Referer"].ToString();
			return Redirect(referer);
		}
		public IActionResult Payment()
		{
			var cart = _cartService.GetCart();
			return View(cart);
		}
	}
}
