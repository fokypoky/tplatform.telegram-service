using System.Net;
using System.Text;
using Newtonsoft.Json;
using TPlatform.TelegramService.Dto;
using TPlatform.TelegramService.Dto.Telegram;

namespace TPlatform.TelegramService.Services.Adapters
{
	public class TelegramAdapter : ITelegramAdapter
	{
		private readonly string _botToken;
		private readonly HttpClient _httpClient;

		public TelegramAdapter(string botToken)
		{
			_botToken = botToken;

			_httpClient = new HttpClient();
			// _httpClient.BaseAddress = new Uri($"https://api.telegram.org/bot{botToken}");

		}

		public async Task<ServiceResult<bool>> SendMessageAsync(string message, long chatId)
		{
			try
			{
				var request = new SendMessageRequest()
				{
					ChatId = chatId, Text = message
				};

				await SendMessageAsync(request);

				return new ServiceResult<bool>()
				{
					Result = true,
					ResultType = ServiceResultType.Ok
				};
			}
			catch(HttpRequestException ex)
			{
				return new ServiceResult<bool>()
				{
					ResultType = (ServiceResultType)(int)ex.StatusCode!,
					ErrorMessage = ex.Message
				};
			}
		}

        #region private methods
		private async Task SendMessageAsync(SendMessageRequest request)
		{
			var endpoint = $"https://api.telegram.org/bot{_botToken}/sendMessage";
			var data = JsonConvert.SerializeObject(request);

			var content = new StringContent(data, Encoding.UTF8, "application/json");
			
			var response = await _httpClient.PostAsync(endpoint, content);

			if(!response.IsSuccessStatusCode)
			{
				var errorMessage = await response.Content.ReadAsStringAsync();
				throw new HttpRequestException(errorMessage, null, response.StatusCode);
			}
		}
        #endregion
    }
}

