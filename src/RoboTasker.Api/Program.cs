using Hangfire;
using RoboTasker.Api;
using RoboTasker.Api.Extensions;
using RoboTasker.Application;
using RoboTasker.Infrastructure;
using RoboTasker.Infrastructure.Chatting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddUi();

MapsterConfig.CreateConfig();

var app = builder.Build();

await app.MigrateDatabase();
await app.EnsureSuperAdminCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseExceptionHandler(options => {});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseBackgroundJobsDashboard();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.MapGet("ping", () => "Hello World!");

app.Run();