using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letgo.DataAccess.EntityConfiguration
{
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			// Table name
			builder.ToTable("Brands");

			// Primary key
			builder.HasKey(b => b.Id);

			// Property configurations
			builder.Property(b => b.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(b => b.Description)
				.HasMaxLength(500);

			// Relationship configurations
			builder.HasMany(b => b.Products)
				.WithOne(p => p.Brand)
				.HasForeignKey(p => p.BrandId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
