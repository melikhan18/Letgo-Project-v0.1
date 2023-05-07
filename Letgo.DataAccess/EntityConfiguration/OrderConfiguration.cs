using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letgo.DataAccess.EntityConfiguration
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable("Orders");
			builder.HasKey(o => o.Id);
			builder.Property(o => o.Date).HasColumnType("datetime").IsRequired();
			builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
			builder.Property(o => o.Status).IsRequired();

			builder.HasOne(o => o.User)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.UserId)
				.IsRequired();
		}
	}
}
