namespace Letgo.Entity
{
	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		private string _fullName;
		public string ProfilePhoto { get; set; }
		public string FullName
		{
			get { return _fullName; }
			set { _fullName = FirstName + " " + LastName; }
		}
		public bool isLogin { get; set; }

		public string Address { get; set; }
		public string City { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public int RoleId { get; set; }

		// Other relational properties
		public ICollection<Like> Likes { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual Role Role { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}
