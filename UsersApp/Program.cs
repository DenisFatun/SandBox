using Microsoft.EntityFrameworkCore;
using UsersApp.Data;
using UsersApp.Extensions;
using SandBoxLib.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitServices(builder.Configuration);

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<SandBoxDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));


var app = builder.Build();

app.InitApplication();
app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var sandBoxDbContext = scope.ServiceProvider.GetRequiredService<SandBoxDbContext>();
    sandBoxDbContext.Database.Migrate();
}

app.Run();
