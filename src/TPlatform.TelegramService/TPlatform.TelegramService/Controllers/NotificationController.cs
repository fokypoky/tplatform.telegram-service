using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TPlatform.TelegramService.Api.Models;
using TPlatform.TelegramService.Dto;
using TPlatform.TelegramService.Services.Interfaces;

namespace TPlatform.TelegramService.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class NotificationController : Controller
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Notify([FromBody] NotificationRequest request)
        {
            var result = await _notificationService.NotifyAsync(GetNotificationMessage(request));
            return MapResponse(result);
        }

        private string GetNotificationMessage(NotificationRequest request)
        {
            var builder = new StringBuilder($"#{request.TargetSystem.Replace(" ", "_")} ");

            request.Tags.ForEach(tag =>
            {
                builder.Append($"#{tag} ");
            });

            builder.Append($"\n\n{request.Message}");

            return builder.ToString();
        }

        private IActionResult MapResponse<T>(ServiceResult<T> result)
        {
            return result.ResultType == ServiceResultType.Ok
                ? Ok()
                : StatusCode((int)result.ResultType, JsonConvert.SerializeObject(result.ErrorMessage));
        }
    }
}