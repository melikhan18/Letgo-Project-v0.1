using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letgo.DataAccess.EntityConfiguration
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.Name).IsRequired().HasColumnType("varchar(50)");
			builder.Property(c => c.Description).HasColumnType("varchar(150)");

			builder.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId)
				.IsRequired();
		}
	}
}
