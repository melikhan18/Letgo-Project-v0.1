using Letgo.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letgo.DataAccess.EntityConfiguration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(c => c.Id);
			builder.Property(c => c.FullName).IsRequired().HasColumnType("varchar(150)");
			builder.Property(c => c.Email).IsRequired().HasColumnType("varchar(150)");
		}
	}
}
