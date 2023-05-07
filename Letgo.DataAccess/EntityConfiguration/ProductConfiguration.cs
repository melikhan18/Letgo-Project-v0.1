using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letgo.DataAccess.EntityConfiguration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");
			builder.HasKey(p => p.Id);
			builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(150)");
			builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(2000)");
			builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
			builder.Property(p => p.StockQuantity).IsRequired();
			builder.Property(p => p.Color).IsRequired().HasConversion<string>();
			builder.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.IsRequired()
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(p => p.User)
				.WithMany(u => u.Products)
				.HasForeignKey(u => u.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
