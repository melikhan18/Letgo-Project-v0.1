namespace Letgo.Entity
{
	public class Order
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime Date { get; set; }
		public decimal TotalAmount { get; set; }
		public string Status { get; set; }

		// Other relational properties
		public virtual User User { get; set; }
		public virtual ICollection<OrderDetail> OrderDetails { get; set; }
	}
}
