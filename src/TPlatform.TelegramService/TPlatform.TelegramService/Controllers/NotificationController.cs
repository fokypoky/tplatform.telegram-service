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

        [HttpPost("info")]
        public async Task<IActionResult> NotifyInfo([FromBody] NotificationRequest request)
        {
            var result = await _notificationService.NotifyAsync(GetNotificationMessage(request, "info"));
            return MapResponse(result);
        }

        [HttpPost("warning")]
        public async Task<IActionResult> NotifyWarning([FromBody] NotificationRequest request)
        {
            var result = await _notificationService.NotifyAsync(GetNotificationMessage(request, "warning"));
            return MapResponse(result);
        }

        [HttpPost("error")]
        public async Task<IActionResult> NotifyError([FromBody] NotificationRequest request)
        {
            var result = await _notificationService.NotifyAsync(GetNotificationMessage(request, "error"));
            return MapResponse(result);
        }

        private IActionResult MapResponse<T>(ServiceResult<T> result)
        {
            return result.ResultType == ServiceResultType.Ok
                ? Ok()
                : StatusCode((int)result.ResultType, JsonConvert.SerializeObject(result.ErrorMessage));
        }
        
        private string GetNotificationMessage(NotificationRequest request, string logType)
        {
            if (!request.Tags.Contains(logType, StringComparer.OrdinalIgnoreCase))
            {
                request.Tags.Insert(0, logType);
            }
            
            var builder = new StringBuilder();
            if (request.TargetSystem != null)
            {
                builder.Append($"#{request.TargetSystem} ");
            }
            
            request.Tags.ForEach(tag =>
            {
                builder.Append($"#{tag} ");
            });

            builder.Append($"\n\n{request.Message}");

            return builder.ToString();
        }
    }
}

