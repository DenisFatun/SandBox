using TokensApp.Config;
using TokensApp.Implemention.GRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitServices(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<TokenService>();

app.Run();
