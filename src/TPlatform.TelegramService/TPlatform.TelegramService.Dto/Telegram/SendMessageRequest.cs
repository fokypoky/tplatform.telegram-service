using Newtonsoft.Json;
namespace TPlatform.TelegramService.Dto.Telegram
{
	public class SendMessageRequest
	{
		[JsonProperty("chat_id")]
		public long ChatId { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}

