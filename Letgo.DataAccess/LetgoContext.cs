using Letgo.DataAccess.EntityConfiguration;
using Letgo.Entity;
using Microsoft.EntityFrameworkCore;

namespace Letgo.DataAccess
{
	public class LetgoContext : DbContext
	{

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<ProductTag> ProductTags { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=Letgo;Integrated Security=true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Like>()
			.HasKey(l => new { l.UserId, l.ProductId });

			modelBuilder.Entity<Like>()
				.HasOne(l => l.User)
				.WithMany(u => u.Likes)
				.HasForeignKey(l => l.UserId);

			modelBuilder.Entity<Like>()
				.HasOne(l => l.Product)
				.WithMany(p => p.Likes)
				.HasForeignKey(l => l.ProductId);

			modelBuilder.Entity<ProductTag>()
		   .HasKey(pt => new { pt.ProductId, pt.TagId });

			modelBuilder.Entity<ProductTag>()
				.HasOne(pt => pt.Product)
				.WithMany(p => p.ProductTags)
				.HasForeignKey(pt => pt.ProductId);

			modelBuilder.Entity<ProductTag>()
				.HasOne(pt => pt.Tag)
				.WithMany(t => t.ProductTags)
				.HasForeignKey(pt => pt.TagId);


			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
			modelBuilder.ApplyConfiguration(new BrandConfiguration());
		}
	}
}
