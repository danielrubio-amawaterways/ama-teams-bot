using AmaTeamsBot;
using AmaTeamsBot.Bots;
using AmaTeamsBot.Config;
using AmaTeamsBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ServiceDeskOptions>(
    builder.Configuration.GetSection("ServiceDesk"));

builder.Services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();
builder.Services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();
builder.Services.AddSingleton<ITicketDetectionService, TicketDetectionService>();
builder.Services.AddTransient<IBot, ServiceDeskBot>();

var app = builder.Build();

app.MapPost("/api/messages", async (IBotFrameworkHttpAdapter adapter, IBot bot, HttpContext context) =>
{
    await adapter.ProcessAsync(context.Request, context.Response, bot);
});

app.Run();
