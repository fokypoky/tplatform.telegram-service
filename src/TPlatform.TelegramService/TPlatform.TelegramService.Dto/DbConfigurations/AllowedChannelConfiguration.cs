
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TPlatform.TelegramService.Dto.DbConfigurations
{
	public class AllowedChannelConfiguration : IEntityTypeConfiguration<AllowedChannel>
	{
		public void Configure(EntityTypeBuilder<AllowedChannel> builder)
		{
			builder.HasKey(e => e.Id);
			builder.ToTable("allowedchannels");

			builder.Property(e => e.Id).HasColumnName("id");
			builder.Property(e => e.ChannelId).HasColumnName("channel_id");
		}
	}
}

