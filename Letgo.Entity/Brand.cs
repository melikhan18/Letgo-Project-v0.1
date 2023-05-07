namespace Letgo.Entity
{
	public class Brand
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		// Other relational properties
		public virtual ICollection<Product> Products { get; set; }
	}
}
