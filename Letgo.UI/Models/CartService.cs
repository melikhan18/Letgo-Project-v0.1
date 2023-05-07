using Letgo.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Letgo.UI.Models
{
	public class CartService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CartService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public Cart GetCart()
		{
			var session = _httpContextAccessor.HttpContext.Session;
			var cartJson = session.GetString("Cart");

			if (!string.IsNullOrEmpty(cartJson))
			{
				return JsonConvert.DeserializeObject<Cart>(cartJson);
			}

			var cart = new Cart();
			SaveCart(cart);

			return cart;
		}

		public void AddToCart(Product product)
		{
			var cart = GetCart();
			if (!cart.Items.Any(x => x.Id == product.Id))
			{
				cart.Items.Add(product);
				SaveCart(cart);
				
			}

		}
		public void RemoveItemFromCart(int itemId)
		{
			var session = _httpContextAccessor.HttpContext.Session;
			var cart = session.GetString("Cart");
			var cartDesJson = JsonConvert.DeserializeObject<Cart>(cart);
			var itemToRemove = cartDesJson.Items.FirstOrDefault(item => item.Id == itemId);
			if (itemToRemove != null)
			{
				cartDesJson.Items.Remove(itemToRemove);
				var cartJson = JsonConvert.SerializeObject(cartDesJson);
				session.SetString("Cart", cartJson);
			}
		}
		private void SaveCart(Cart cart)
		{
			var session = _httpContextAccessor.HttpContext.Session;
			var cartJson = JsonConvert.SerializeObject(cart);
			session.SetString("Cart", cartJson);
		}
		public void ClearCart()
		{
			var session = _httpContextAccessor.HttpContext.Session;
			session.SetString("Cart", "");

		}
	}
}
