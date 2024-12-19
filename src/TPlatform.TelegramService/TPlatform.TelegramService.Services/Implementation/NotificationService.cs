using Microsoft.EntityFrameworkCore;
using TPlatform.TelegramService.Dto;
using TPlatform.TelegramService.Services.Adapters;
using TPlatform.TelegramService.Services.Interfaces;

namespace TPlatform.TelegramService.Services.Implementation
{
	public class NotificationService : INotificationService
	{
		private readonly IApplicationContext _applicationContext;
		private readonly ITelegramAdapter _telegramAdapter;

		public NotificationService(IApplicationContext applicationContext, ITelegramAdapter telegramAdapter)
		{
			_applicationContext = applicationContext;
			_telegramAdapter = telegramAdapter;
		}

		public async Task<ServiceResult<bool>> NotifyAsync(string message, long? chatId)
		{
			//var channels = await  _applicationContext.AllowedChannels.ToListAsync();
			List<AllowedChannel> channels;

			if (chatId != null)
			{
				channels = new List<AllowedChannel>()
				{
					new AllowedChannel() { Id = 0, ChannelId = chatId.Value }
				};
			}
			else
			{
				channels = await _applicationContext.AllowedChannels.ToListAsync();
			}

			foreach (var channel in channels)
			{
				var response = await _telegramAdapter.SendMessageAsync(message, channel.ChannelId);
				if (response.ResultType != ServiceResultType.Ok)
				{
					return new ServiceResult<bool>()
					{
						ResultType = response.ResultType,
						ErrorMessage = $"Can't send to channel {channel.ChannelId}. {response.ErrorMessage}"
					};
				}
			}

			return new ServiceResult<bool>()
			{
				Result = true,
				ResultType = ServiceResultType.Ok
			};
		}
	}
}

