namespace Letgo.Entity.Messages
{
	public class ProductsFilterBySearchCriteriasRequest
	{
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		private Color _color;

		public Color Color
		{
			get { return _color; }
			set { _color = value; }
		}

		public int BrandId { get; set; }
	}
}
