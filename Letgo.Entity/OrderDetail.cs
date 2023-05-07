namespace Letgo.Entity
{
	public class OrderDetail
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Amount { get; set; }

		// Other relational properties
		public virtual Order Order { get; set; }
		public virtual Product Product { get; set; }
	}
}
