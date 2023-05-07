namespace Letgo.Entity
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public string Url { get; set; }

        // Other relational properties
        public virtual ICollection<Product> Products { get; set; }
	}
}
