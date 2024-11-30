using TPlatform.TelegramService.Services.Adapters;
using TPlatform.TelegramService.Services.Implementation;
using TPlatform.TelegramService.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IApplicationContext, ApplicationContext>(_ => new ApplicationContext(
    host: builder.Configuration.GetValue<string>("DB_HOST")!,
    port: builder.Configuration.GetValue<string>("DB_PORT")!,
    user: builder.Configuration.GetValue<string>("DB_USER")!,
    password: builder.Configuration.GetValue<string>("DB_PASSWORD")!,
    database: builder.Configuration.GetValue<string>("DB_NAME")!
));
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddSingleton<ITelegramAdapter, TelegramAdapter>(_ => new TelegramAdapter(builder.Configuration.GetValue<string>("TELEGRAM_BOT_KEY")!));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

