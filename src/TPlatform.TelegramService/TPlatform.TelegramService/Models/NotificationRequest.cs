namespace TPlatform.TelegramService.Api.Models
{
	public class NotificationRequest
	{
		public List<string> Tags { get; set; }
		public string TargetSystem { get; set; }
		public string Message { get; set; }
	}
}

