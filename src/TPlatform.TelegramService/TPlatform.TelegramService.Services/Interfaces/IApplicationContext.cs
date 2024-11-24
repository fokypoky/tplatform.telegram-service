
using Microsoft.EntityFrameworkCore;
using TPlatform.TelegramService.Dto;

namespace TPlatform.TelegramService.Services.Interfaces
{
	public interface IApplicationContext
	{
		public DbSet<AllowedChannel> AllowedChannels { get; set; }
		public DbSet<AllowedUser> AllowedUsers { get; set; }
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}

