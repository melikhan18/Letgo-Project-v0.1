namespace Letgo.Entity
{
	public class Cart
	{
		public List<Product> Items { get; set; }
		public Cart()
		{
			Items = new List<Product>();
		}
	}
}
