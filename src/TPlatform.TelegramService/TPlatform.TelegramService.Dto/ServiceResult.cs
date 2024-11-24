namespace TPlatform.TelegramService.Dto
{
	public class ServiceResult<T>
	{
		public T? Result { get; set; }
		public ServiceResultType ResultType { get; set; }
		public string? ErrorMessage { get; set; }
	}
}

