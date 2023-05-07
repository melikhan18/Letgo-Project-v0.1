using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letgo.DataAccess.EntityConfiguration
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			builder.ToTable("OrderDetails");
			builder.HasKey(od => od.Id);
			builder.Property(od => od.Quantity).IsRequired();
			builder.Property(od => od.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
			builder.Property(od => od.Amount).HasColumnType("decimal(18,2)").IsRequired();

			builder.HasOne(od => od.Order)
				.WithMany(o => o.OrderDetails)
				.HasForeignKey(od => od.OrderId)
				.IsRequired();

			builder.HasOne(od => od.Product)
				.WithMany(p => p.OrderDetails)
				.HasForeignKey(od => od.ProductId)
				.IsRequired();
		}
	}
}
