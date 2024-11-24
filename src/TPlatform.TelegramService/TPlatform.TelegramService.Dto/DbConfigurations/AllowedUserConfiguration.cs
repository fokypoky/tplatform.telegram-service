using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TPlatform.TelegramService.Dto.DbConfigurations
{
	public class AllowedUserConfiguration : IEntityTypeConfiguration<AllowedUser>
	{
		public void Configure(EntityTypeBuilder<AllowedUser> builder)
		{
			builder.HasKey(e => e.Id);
			builder.ToTable("allowedusers");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.UserId).HasColumnName("user_id");
		}
	}
}

