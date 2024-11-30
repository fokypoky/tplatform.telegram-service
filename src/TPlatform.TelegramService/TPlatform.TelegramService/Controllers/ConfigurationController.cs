using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TPlatform.TelegramService.Dto;
using TPlatform.TelegramService.Services.Interfaces;

namespace TPlatform.TelegramService.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        #region Allowed channels
        [HttpGet("allowedchannels/{id}")]
        public async Task<IActionResult> GetAllowedChannelById([FromRoute] int id)
        {
            var result = await _configurationService.GetAllowedChannelByIdAsync(id);
            return MapResponse(result);
        }

        [HttpGet("allowedchannels/all")]
        public async Task<IActionResult> GetAllAllowedChannels()
        {
            var result = await _configurationService.GetAllAllowedChannelsAsync();
            return MapResponse(result);
        }

        [HttpPost("allowedchannels/add/{channelId}")]
        public async Task<IActionResult> AddAllowedChannel([FromRoute] long channelId)
        {
            var result = await _configurationService.AddAllowedChannelAsync(channelId);
            return MapResponse(result);
        }

        [HttpDelete("allowedchannels/remove/{id}")]
        public async Task<IActionResult> RemoveAllowedChannel([FromRoute] int id)
        {
            var result = await _configurationService.RemoveAllowedChannelByIdAsync(id);
            return MapResponse(result);
        }
        #endregion

        #region Allowed users
        [HttpGet("allowedusers/{id}")]
        public async Task<IActionResult> GetAllowedUserById([FromRoute] int id)
        {
            var result = await _configurationService.GetAllowedUserByIdAsync(id);
            return MapResponse(result);
        }

        [HttpGet("allowedusers/all")]
        public async Task<IActionResult> GetAllAllowedUsers()
        {
            var result = await _configurationService.GetAllAllowedUsersAsync();
            return MapResponse(result);
        }

        [HttpPost("allowedusers/add/{userId}")]
        public async Task<IActionResult> AddAllowedUser([FromRoute] long userId)
        {
            var result = await _configurationService.AddAllowedUserAsync(userId);
            return MapResponse(result);
        }

        [HttpDelete("allowedusers/remove/{id}")]
        public async Task<IActionResult> RemoveAllowedUser([FromRoute] int id)
        {
            var result = await _configurationService.RemoveAllowedUserByIdAsync(id);
            return MapResponse(result);
        }
        #endregion

        private IActionResult MapResponse<T>(ServiceResult<T> result)
        {
            return result.ResultType == ServiceResultType.Ok
                ? (result.Result == null ? Ok() : Ok(JsonConvert.SerializeObject(result.Result)))
                : StatusCode((int)result.ResultType, JsonConvert.SerializeObject(result.ErrorMessage));
        }
    }
}

