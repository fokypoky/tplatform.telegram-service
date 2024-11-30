using Microsoft.EntityFrameworkCore;
using TPlatform.TelegramService.Dto;
using TPlatform.TelegramService.Services.Interfaces;

namespace TPlatform.TelegramService.Services.Implementation
{
	public class ConfigurationService : IConfigurationService
	{
        private readonly IApplicationContext _applicationContext;

		public ConfigurationService(IApplicationContext applicationContext)
		{
            _applicationContext = applicationContext;
		}

        public async Task<ServiceResult<AllowedChannel>> AddAllowedChannelAsync(long channelId)
        {
            var existingChannel = await _applicationContext.AllowedChannels
                .FirstOrDefaultAsync(channel => channel.ChannelId == channelId);

            if (existingChannel != null)
            {
                return new ServiceResult<AllowedChannel>
                {
                    ResultType = ServiceResultType.BadRequest,
                    ErrorMessage = $"Channel with id {channelId} already exists"
                };
            }

            var allowedChannel = new AllowedChannel
            {
                ChannelId = channelId
            };

            await _applicationContext.AllowedChannels.AddAsync(allowedChannel);
            await _applicationContext.SaveChangesAsync();

            return new ServiceResult<AllowedChannel>
            {
                Result = allowedChannel,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<AllowedUser>> AddAllowedUserAsync(long userId)
        {
            var existingUser = await _applicationContext.AllowedUsers
                .FirstOrDefaultAsync(user => user.UserId == userId);

            if(existingUser != null)
            {
                return new ServiceResult<AllowedUser>
                {
                    ResultType = ServiceResultType.BadRequest,
                    ErrorMessage = $"User with id {userId} already exists"
                };
            }

            var allowedUser = new AllowedUser
            {
                UserId = userId
            };

            await _applicationContext.AllowedUsers.AddAsync(allowedUser);
            await _applicationContext.SaveChangesAsync();

            return new ServiceResult<AllowedUser>
            {
                Result = allowedUser,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<bool>> RemoveAllowedUserByIdAsync(int id)
        {
            var existingUser = await _applicationContext.AllowedUsers
                .FirstOrDefaultAsync(u => u.Id == id);

            if(existingUser == null)
            {
                return new ServiceResult<bool>
                {
                    Result = false,
                    ResultType = ServiceResultType.NotFound,
                    ErrorMessage = $"User with id {id} not exists"
                };
            }

            _applicationContext.AllowedUsers.Remove(existingUser);
            await _applicationContext.SaveChangesAsync();

            return new ServiceResult<bool>
            {
                Result = true,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<bool>> RemoveAllowedChannelByIdAsync(int id)
        {
            var existingChannel = await _applicationContext.AllowedChannels
                .FirstOrDefaultAsync(c => c.Id == id);

            if(existingChannel == null)
            {
                return new ServiceResult<bool>
                {
                    Result = false,
                    ResultType = ServiceResultType.NotFound,
                    ErrorMessage = $"Channel with id {id} not exists"
                };
            }

            _applicationContext.AllowedChannels.Remove(existingChannel);
            await _applicationContext.SaveChangesAsync();

            return new ServiceResult<bool>
            {
                Result = true,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<List<AllowedChannel>>> GetAllAllowedChannelsAsync()
        {
            var allowedChannels = await _applicationContext.AllowedChannels.ToListAsync();
            return new ServiceResult<List<AllowedChannel>>
            {
                Result = allowedChannels,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<List<AllowedUser>>> GetAllAllowedUsersAsync()
        {
            var allowedUsers = await _applicationContext.AllowedUsers.ToListAsync();
            return new ServiceResult<List<AllowedUser>>
            {
                Result = allowedUsers,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<AllowedChannel>> GetAllowedChannelByIdAsync(int id)
        {
            var allowedChannel = await _applicationContext.AllowedChannels
                .FirstOrDefaultAsync(c => c.Id == id);

            if(allowedChannel == null)
            {
                return new ServiceResult<AllowedChannel>
                {
                    ResultType = ServiceResultType.NotFound,
                    ErrorMessage = $"Allowed channel with id {id} not exists"
                };
            }

            return new ServiceResult<AllowedChannel>
            {
                Result = allowedChannel,
                ResultType = ServiceResultType.Ok
            };
        }

        public async Task<ServiceResult<AllowedUser>> GetAllowedUserByIdAsync(int id)
        {
            var allowedUser = await _applicationContext.AllowedUsers
                .FirstOrDefaultAsync(u => u.Id == id);

            if(allowedUser == null)
            {
                return new ServiceResult<AllowedUser>
                {
                    ResultType = ServiceResultType.NotFound,
                    ErrorMessage = $"Allowed user with id {id} not exists"
                };
            }

            return new ServiceResult<AllowedUser>
            {
                Result = allowedUser,
                ResultType = ServiceResultType.Ok
            };
        }
    }
}

