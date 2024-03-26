using SB_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using SB_Utility.Initializing;
using SB_Utility;
using SB_DataAccess.Repository.Interface;
using SB_DataAccess.Repository;
using OLX_Scout_bot.Services.IServices;
using OLX_Scout_bot.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInitializingBot, InitializingBot>();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITelegramBotClient>(x => { return new TelegramBotClient("6913696850:AAEkdNGUJNz4_Hu4EYmROy1fKjuc1vzs4Yo"); });
builder.Services.AddSingleton<ISubscriptionService, SubscriptionService>();
//builder.Services.AddHttpsRedirection(options =>
//{
//    options.HttpsPort = 8443;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<IInitializingBot>().Start();
}

app.Run();
