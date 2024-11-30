using TPlatform.TelegramService.Dto;

namespace TPlatform.TelegramService.Services.Interfaces
{
	public interface IConfigurationService
	{
		Task<ServiceResult<AllowedChannel>> AddAllowedChannelAsync(long channelId);
		Task<ServiceResult<AllowedUser>> AddAllowedUserAsync(long userId);
		Task<ServiceResult<bool>> RemoveAllowedUserByIdAsync(int id);
		Task<ServiceResult<bool>> RemoveAllowedChannelByIdAsync(int id);
		Task<ServiceResult<List<AllowedChannel>>> GetAllAllowedChannelsAsync();
		Task<ServiceResult<List<AllowedUser>>> GetAllAllowedUsersAsync();
		Task<ServiceResult<AllowedChannel>> GetAllowedChannelByIdAsync(int id);
		Task<ServiceResult<AllowedUser>> GetAllowedUserByIdAsync(int id);
	}
}

