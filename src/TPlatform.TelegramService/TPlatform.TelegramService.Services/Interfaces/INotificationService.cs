using System;
using TPlatform.TelegramService.Dto;

namespace TPlatform.TelegramService.Services.Interfaces
{
	public interface INotificationService
	{
		Task<ServiceResult<bool>> NotifyAsync(string message);
	}
}

