using Microsoft.EntityFrameworkCore;
using TPlatform.TelegramService.Dto;
using TPlatform.TelegramService.Dto.DbConfigurations;
using TPlatform.TelegramService.Services.Interfaces;

namespace TPlatform.TelegramService.Services.Implementation
{
	public class ApplicationContext : DbContext, IApplicationContext
	{
		private readonly string _host;
		private readonly string _port;
		private readonly string _database;
		private readonly string _user;
		private readonly string _password;

		public virtual DbSet<AllowedChannel> AllowedChannels { get; set; }
		public virtual DbSet<AllowedUser> AllowedUsers { get; set; }

		public ApplicationContext()
		{
		}

		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}

        public ApplicationContext(string host, string port, string database, string user, string password)
        {
            _host = host;
            _port = port;
            _database = database;
            _user = user;
            _password = password;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseNpgsql(
				$"Server={_host};Port={_port};Username={_user};Password={_password};Database={_database}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfiguration(new AllowedUserConfiguration());
			modelBuilder.ApplyConfiguration(new AllowedChannelConfiguration());
        }
    }
}

