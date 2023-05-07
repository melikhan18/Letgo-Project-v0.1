namespace Letgo.Entity
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public string ImageUrl { get; set; }
		public int CategoryId { get; set; }
		public Color Color { get; set; }
		public int BrandId { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;

		// Other relational properties
		public ICollection<ProductTag> ProductTags { get; set; }
		public ICollection<Like> Likes { get; set; }
		public virtual Category Category { get; set; }
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
		public virtual Brand Brand { get; set; }
		public virtual User User { get; set; }
	}
}
