using TPlatform.TelegramService.Dto;

namespace TPlatform.TelegramService.Services.Adapters
{
	public interface ITelegramAdapter
	{
		Task<ServiceResult<bool>> SendMessageAsync(string message, long chatId);
	}
}

