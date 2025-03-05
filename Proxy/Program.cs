using Proxy.Extensions;
using SandBoxLib.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitServices(builder.Configuration);

var app = builder.Build();

app.InitApplication();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();
